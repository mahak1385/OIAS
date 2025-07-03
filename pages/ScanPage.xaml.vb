Imports System.IO
Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Threading
Imports Core.Core.ScanPage

Namespace Core
    Partial Public Class ScanPage
        Inherits Page

        Public Class ScanResult
            Public Property File As String
            Public Property Status As String
        End Class

        Private VirusHashes As New Dictionary(Of String, String)
        Private Results As New ObservableCollection(Of ScanResult)
        Private VirusDBPath As String = "virus_db.txt"
        Private QuarantineFolder As String = "Quarantine"
        Private QuarantineDataFile As String = "quarantine_data.txt"

        Public Sub New()
            InitializeComponent()
            ScanResults.ItemsSource = Results
            LoadVirusDatabase()
            If Not Directory.Exists(QuarantineFolder) Then
                Directory.CreateDirectory(QuarantineFolder)
            End If
        End Sub

        Private Sub LoadVirusDatabase()
            VirusHashes.Clear()
            If File.Exists(VirusDBPath) Then
                Dim lines = File.ReadAllLines(VirusDBPath)
                For Each line In lines
                    Dim parts = line.Split("|"c)
                    If parts.Length >= 2 Then
                        Dim name = parts(0)
                        Dim hash = parts(1).ToLowerInvariant()
                        If Not VirusHashes.ContainsKey(hash) Then
                            VirusHashes.Add(hash, name)
                        End If
                    End If
                Next
            End If
        End Sub

        Private Function GetMD5Hash(filePath As String) As String
            Using md5 As MD5 = MD5.Create()
                Using stream = File.OpenRead(filePath)
                    Dim hash = md5.ComputeHash(stream)
                    Return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant()
                End Using
            End Using
        End Function

        Private Sub SelectFolder_Click(sender As Object, e As RoutedEventArgs)
            Dim dialog As New FolderBrowserDialog()
            If dialog.ShowDialog() = Forms.DialogResult.OK Then
                Dim folderPath = dialog.SelectedPath
                Results.Clear()
                StatusText.Text = "Scanning..."
                ResultCount.Text = ""
                ScanProgress.Value = 0
                Dim bgWorker As New BackgroundWorker()
                AddHandler bgWorker.DoWork, Sub() ScanFolder(folderPath)
                AddHandler bgWorker.RunWorkerCompleted, Sub() StatusText.Dispatcher.Invoke(Sub() StatusText.Text = "Scan complete.")
                bgWorker.RunWorkerAsync()
            End If
        End Sub

        Private Sub ScanFolder(folderPath As String)
            Try
                Dim files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                Dim total = files.Length
                Dim count = 0

                For Each file In files
                    Dim hash = GetMD5Hash(file)
                    Console.WriteLine("Scanning file: " & file)
                    Console.WriteLine("Computed hash: " & hash)

                    If VirusHashes.ContainsKey(hash) Then
                        Console.WriteLine("✓ Detected as: " & VirusHashes(hash))
                    Else
                        Console.WriteLine("✗ Not in virus_db.txt")
                    End If

                    Dim status As String
                    If VirusHashes.ContainsKey(hash) Then
                        Dim virusName = VirusHashes(hash)
                        status = $"INFECTED ({virusName})"
                        QuarantineFile(file, virusName)
                    Else
                        status = "Safe"
                    End If

                    Dispatcher.Invoke(Sub()
                                          Results.Add(New ScanResult With {.File = file, .Status = status})
                                          count += 1
                                          ScanProgress.Value = (count / total) * 100
                                          ResultCount.Text = $"Scanned: {count}/{total}"
                                      End Sub)
                    Thread.Sleep(10) ' Optional delay for progress
                Next

            Catch ex As Exception
                Dispatcher.Invoke(Sub() MsgBox("Scan error: " & ex.Message))
            End Try
        End Sub

        Private Sub QuarantineFile(filePath As String, virusName As String)
            Try
                Dim fileName = Path.GetFileName(filePath)
                Dim quarantinePath = Path.Combine(QuarantineFolder, fileName)

                If File.Exists(quarantinePath) Then
                    quarantinePath = Path.Combine(QuarantineFolder, Guid.NewGuid().ToString() & "_" & fileName)
                End If

                File.Move(filePath, quarantinePath)

                Dim dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim logEntry = $"{fileName}|{filePath}|{dateStr}|{virusName}"
                File.AppendAllLines(QuarantineDataFile, {logEntry})

            Catch ex As Exception
                Dispatcher.Invoke(Sub() MsgBox("Quarantine error: " & ex.Message))
            End Try
        End Sub
    End Class
End Namespace