Imports System.IO

Namespace Core
    Public Class RealTimeMonitor
        Private ReadOnly _scanner As Scanner
        Private ReadOnly _watchers As New List(Of FileSystemWatcher)
        Private ReadOnly _monitoredPaths As New List(Of String)

        Public Event ThreatDetected(sender As Object, filePath As String, threatName As String)

        Public Sub New()
            _scanner = New Scanner()
            AddHandler _scanner.ThreatDetected, AddressOf Scanner_ThreatDetected
        End Sub

        Public Sub StartMonitoring(path As String)
            If Not _monitoredPaths.Contains(path) Then
                _monitoredPaths.Add(path)
                CreateWatcher(path)
            End If
        End Sub

        Public Sub StopMonitoring(path As String)
            _monitoredPaths.Remove(path)
            Dim watcher = _watchers.FirstOrDefault(Function(w) w.Path = path)
            If watcher IsNot Nothing Then
                watcher.Dispose()
                _watchers.Remove(watcher)
            End If
        End Sub

        Public Sub StopAllMonitoring()
            For Each watcher In _watchers
                watcher.Dispose()
            Next
            _watchers.Clear()
            _monitoredPaths.Clear()
        End Sub

        Private Sub CreateWatcher(path As String)
            Dim watcher As New FileSystemWatcher()
            watcher.Path = path
            watcher.NotifyFilter = NotifyFilters.LastWrite Or NotifyFilters.FileName Or NotifyFilters.DirectoryName

            AddHandler watcher.Created, AddressOf OnFileChanged
            AddHandler watcher.Changed, AddressOf OnFileChanged

            watcher.EnableRaisingEvents = True
            _watchers.Add(watcher)
        End Sub

        Private Async Sub OnFileChanged(sender As Object, e As FileSystemEventArgs)

            Await Task.Delay(100)

            Try
                If File.Exists(e.FullPath) Then
                    Await _scanner.ScanFileAsync(e.FullPath)
                End If
            Catch ex As Exception
                Debug.WriteLine($"Error scanning file {e.FullPath}: {ex.Message}")
            End Try
        End Sub

        Private Sub Scanner_ThreatDetected(sender As Object, filePath As String, threatName As String)
            RaiseEvent ThreatDetected(Me, filePath, threatName)
            _scanner.QuarantineFile(filePath)
        End Sub

        Public ReadOnly Property MonitoredPaths As IReadOnlyList(Of String)
            Get
                Return _monitoredPaths
            End Get
        End Property
    End Class
End Namespace