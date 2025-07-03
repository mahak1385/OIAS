Imports System.Collections.ObjectModel
Imports System.IO

Namespace Core
    Partial Public Class VirusDatabasePage
        Inherits Page

        Public Class VirusEntry
            Public Property Name As String
            Public Property Hash As String
            Public Property DateAdded As String
        End Class

        Private dbFilePath As String = "virus_db.txt"
        Private VirusList As New ObservableCollection(Of VirusEntry)

        Public Sub New()
            InitializeComponent()
            LoadVirusDatabase()
            VirusListView.ItemsSource = VirusList
        End Sub

        Private Sub LoadVirusDatabase()
            VirusList.Clear()

            If File.Exists(dbFilePath) Then
                Dim lines = File.ReadAllLines(dbFilePath)
                For Each line In lines
                    Dim parts = line.Split("|"c)
                    If parts.Length = 3 Then
                        VirusList.Add(New VirusEntry With {
                            .Name = parts(0),
                            .Hash = parts(1).ToLower(),
                            .DateAdded = parts(2)
                        })
                    End If
                Next
            End If
        End Sub

        Private Function LoadVirusHashes() As List(Of String)
            Dim hashes As New List(Of String)
            If File.Exists("virus_db.txt") Then
                For Each line In File.ReadAllLines("virus_db.txt")
                    Dim parts = line.Split("|"c)
                    If parts.Length >= 2 Then hashes.Add(parts(1).ToLower())
                Next
            End If
            Return hashes
        End Function


        Private Sub UpdateDatabase_Click(sender As Object, e As RoutedEventArgs)
            ' Simulate new virus entry
            Dim newVirus As New VirusEntry With {
                .Name = "Offline.Virus.Sample",
                .Hash = Guid.NewGuid().ToString("N").Substring(0, 32),
                .DateAdded = DateTime.Now.ToString("yyyy-MM-dd")
            }

            ' Add to collection
            VirusList.Add(newVirus)

            ' Append to file
            Using writer As StreamWriter = File.AppendText(dbFilePath)
                writer.WriteLine($"{newVirus.Name}|{newVirus.Hash}|{newVirus.DateAdded}")
            End Using

            MsgBox("Offline virus database updated (text file).")
        End Sub

        Private Sub AddVirus_Click(sender As Object, e As RoutedEventArgs)
            Dim name = VirusNameBox.Text.Trim()
            Dim hash = VirusHashBox.Text.Trim().ToLower()
            Dim dateStr = DateTime.Now.ToString("yyyy-MM-dd")

            If name = "" OrElse hash = "" Then
                MsgBox("Please enter both the virus name and hash.", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Check if hash already exists
            If VirusList.Any(Function(v) v.Hash = hash) Then
                MsgBox("This hash already exists in the database.", MsgBoxStyle.Information)
                Return
            End If

            ' Add to list and file
            Dim newEntry As New VirusEntry With {
        .Name = name,
        .Hash = hash,
        .DateAdded = dateStr
    }

            VirusList.Add(newEntry)
            Using writer As StreamWriter = File.AppendText(dbFilePath)
                writer.WriteLine($"{name}|{hash}|{dateStr}")
            End Using

            VirusNameBox.Clear()
            VirusHashBox.Clear()

            MsgBox("✅ Virus entry added successfully.")
        End Sub



    End Class
End Namespace
