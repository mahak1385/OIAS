Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading.Tasks
Imports System.Timers
Imports System.Text
Imports System.Diagnostics
Imports Core


Namespace Core
    Public Class Scanner
        Private ReadOnly _virusDatabase As New Dictionary(Of String, String)()
        Private ReadOnly _quarantinePath As String
        Private ReadOnly _databasePath As String
        Private _backgroundScanTimer As Timer
        Private _backgroundScanIntervalMinutes As Integer = 10
        Private _backgroundScanPath As String
        Private ReadOnly _backgroundScanLock As New Object()

        Public Event ScanProgressChanged(sender As Object, progress As Double)
        Public Event ThreatDetected(sender As Object, filePath As String, threatName As String)

        Public Property BackgroundScanIntervalMinutes As Integer
            Get
                Return _backgroundScanIntervalMinutes
            End Get
            Set(value As Integer)
                _backgroundScanIntervalMinutes = value
                If _backgroundScanTimer IsNot Nothing Then
                    _backgroundScanTimer.Interval = _backgroundScanIntervalMinutes * 60 * 1000
                End If
            End Set
        End Property

        Public Sub New()
            _quarantinePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quarantine")
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database")
            Directory.CreateDirectory(_quarantinePath)
            Directory.CreateDirectory(_databasePath)
            LoadVirusDatabase()
        End Sub

        Private Sub LoadVirusDatabase()
            Dim dbFile = Path.Combine(_databasePath, "signatures.txt")
            If File.Exists(dbFile) Then
                For Each line In File.ReadAllLines(dbFile)
                    Dim parts = line.Split("|"c)
                    If parts.Length = 2 Then
                        _virusDatabase(parts(0)) = parts(1)
                    End If
                Next
            End If
        End Sub

        Public Async Function ScanFileAsync(filePath As String) As Task(Of Boolean)
            Try
                Using stream As New FileStream(filePath, FileMode.Open, FileAccess.Read)

                    stream.Position = 0
                    Dim md5HashString As String
                    Using md5Alg As MD5 = MD5.Create()
                        Dim md5Hash = Await Task.Run(Function() md5Alg.ComputeHash(stream))
                        md5HashString = BitConverter.ToString(md5Hash).Replace("-", "")
                    End Using

                    Dim threatName As String = Nothing
                    If _virusDatabase.TryGetValue(md5HashString, threatName) Then
                        RaiseEvent ThreatDetected(Me, filePath, threatName)
                        Return True
                    End If


                    stream.Position = 0
                    Dim sha256HashString As String
                    Using sha256Alg As SHA256 = SHA256.Create()
                        Dim sha256Hash = Await Task.Run(Function() sha256Alg.ComputeHash(stream))
                        sha256HashString = BitConverter.ToString(sha256Hash).Replace("-", "")
                    End Using

                    If _virusDatabase.TryGetValue(sha256HashString, threatName) Then
                        RaiseEvent ThreatDetected(Me, filePath, threatName)
                        Return True
                    End If
                End Using
            Catch ex As Exception
                Debug.WriteLine($"Error scanning file {filePath}: {ex.Message}")
            End Try
            Return False
        End Function

        Public Async Function ScanDirectoryAsync(directoryPath As String) As Task(Of Integer)
            Dim threatCount As Integer = 0
            Dim files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
            Dim totalFiles = files.Length
            Dim filesScanned = 0

            For Each file In files
                If Await ScanFileAsync(file) Then
                    threatCount += 1
                End If
                filesScanned += 1
                RaiseEvent ScanProgressChanged(Me, filesScanned / totalFiles * 100)
            Next

            Return threatCount
        End Function

        Public Function QuarantineFile(filePath As String) As Boolean
            Try
                Dim fileName = Path.GetFileName(filePath)
                Dim quarantineFilePath = Path.Combine(_quarantinePath, $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}")
                File.Move(filePath, quarantineFilePath)
                Dim metadata = $"{filePath}|{quarantineFilePath}|{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                File.AppendAllText(Path.Combine(_quarantinePath, "metadata.txt"), metadata & Environment.NewLine)
                Return True
            Catch ex As Exception
                Debug.WriteLine($"Error quarantining file {filePath}: {ex.Message}")
                Return False
            End Try
        End Function

        Public Function RestoreFile(quarantineFilePath As String) As Boolean
            Try
                Dim metadataPath = Path.Combine(_quarantinePath, "metadata.txt")
                If Not File.Exists(metadataPath) Then Return False
                Dim lines = File.ReadAllLines(metadataPath).ToList()
                Dim line = lines.FirstOrDefault(Function(l) l.Contains(quarantineFilePath))
                If line Is Nothing Then Return False
                Dim originalPath = line.Split("|"c)(0)
                File.Move(quarantineFilePath, originalPath)
                lines.Remove(line)
                File.WriteAllLines(metadataPath, lines)
                Return True
            Catch ex As Exception
                Debug.WriteLine($"Error restoring file {quarantineFilePath}: {ex.Message}")
                Return False
            End Try
        End Function

        Public Sub StartBackgroundScan(scanPath As String)
            SyncLock _backgroundScanLock
                _backgroundScanPath = scanPath
                If _backgroundScanTimer Is Nothing Then
                    _backgroundScanTimer = New Timer(_backgroundScanIntervalMinutes * 60 * 1000)
                    AddHandler _backgroundScanTimer.Elapsed, AddressOf BackgroundScanTimer_Elapsed
                    _backgroundScanTimer.AutoReset = True
                End If
                _backgroundScanTimer.Enabled = True
            End SyncLock
        End Sub

        Public Sub StopBackgroundScan()
            SyncLock _backgroundScanLock
                If _backgroundScanTimer IsNot Nothing Then
                    _backgroundScanTimer.Enabled = False
                End If
            End SyncLock
        End Sub

        Private Async Sub BackgroundScanTimer_Elapsed(sender As Object, e As ElapsedEventArgs)
            If String.IsNullOrEmpty(_backgroundScanPath) Then Return
            Try
                Await ScanDirectoryAsync(_backgroundScanPath)
            Catch ex As Exception
                Debug.WriteLine($"Error during background scan: {ex.Message}")
            End Try
        End Sub
    End Class
End Namespace
