Imports System.IO
Imports System.Collections.ObjectModel

Namespace Core
    Class QuarantinePage
        Inherits Page

        Public Class QuarantineItem
            Public Property FileName As String
            Public Property FilePath As String
            Public Property DateQuarantined As String
        End Class

        Dim quarantineFilePath As String = "quarantine_data.txt"
        Dim QuarantineItems As New ObservableCollection(Of QuarantineItem)

        Public Sub New()
            InitializeComponent()
            LoadQuarantineList()
            QuarantineListView.ItemsSource = QuarantineItems
        End Sub

        Private Sub LoadQuarantineList()
            QuarantineItems.Clear()
            If File.Exists(quarantineFilePath) Then
                Dim lines = File.ReadAllLines(quarantineFilePath)
                For Each line In lines
                    Dim parts = line.Split("|"c)
                    If parts.Length >= 3 Then
                        QuarantineItems.Add(New QuarantineItem With {
                        .FileName = parts(0),
                        .FilePath = parts(1),
                        .DateQuarantined = parts(2)
                    })
                    End If
                Next
            End If
        End Sub

        Private Sub RestoreFile_Click(sender As Object, e As RoutedEventArgs)
            Dim selected = CType(QuarantineListView.SelectedItem, QuarantineItem)
            If selected Is Nothing Then
                MsgBox("Please select a file to restore.")
                Return
            End If

            Try
                ' Restore logic - move back the file
                Dim quarantineFolder = "Quarantine"
                Dim originalPath = selected.FilePath
                Dim fileName = selected.FileName
                Dim quarantineFile = System.IO.Path.Combine(quarantineFolder, fileName)

                If File.Exists(quarantineFile) Then
                    File.Move(quarantineFile, originalPath)
                    MsgBox("File restored to original location.")
                    RemoveFromList(selected)
                Else
                    MsgBox("Quarantined file not found.")
                End If
            Catch ex As Exception
                MsgBox("Error restoring file: " & ex.Message)
            End Try
        End Sub

        Private Sub DeleteFile_Click(sender As Object, e As RoutedEventArgs)
            Dim selected = CType(QuarantineListView.SelectedItem, QuarantineItem)
            If selected Is Nothing Then
                MsgBox("Please select a file to delete.")
                Return
            End If

            Try
                Dim quarantineFolder = "Quarantine"
                Dim fileName = selected.FileName
                Dim quarantineFile = System.IO.Path.Combine(quarantineFolder, fileName)

                If File.Exists(quarantineFile) Then
                    File.Delete(quarantineFile)
                End If
                MsgBox("File permanently deleted.")
                RemoveFromList(selected)
            Catch ex As Exception
                MsgBox("Error deleting file: " & ex.Message)
            End Try
        End Sub

        Private Sub RemoveFromList(item As QuarantineItem)
            QuarantineItems.Remove(item)
            ' Rewrite quarantine_data.txt
            Dim lines = QuarantineItems.Select(Function(q) $"{q.FileName}|{q.FilePath}|{q.DateQuarantined}").ToArray()
            File.WriteAllLines(quarantineFilePath, lines)
        End Sub
    End Class
End Namespace
