Imports System.IO
Imports System.Net.Http
Imports System.Threading.Tasks

Namespace Core
    Public Class SignatureManager
        Private ReadOnly _databasePath As String
        Private ReadOnly _signatureFile As String

        Public Event UpdateProgress(sender As Object, progress As Double)

        Public Sub New()
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database")
            _signatureFile = Path.Combine(_databasePath, "signatures.txt")

            If Not Directory.Exists(_databasePath) Then
                Directory.CreateDirectory(_databasePath)
            End If
        End Sub

        Public Function LoadSignatures() As Dictionary(Of String, String)
            Dim signatures As New Dictionary(Of String, String)

            If File.Exists(_signatureFile) Then
                For Each line In File.ReadAllLines(_signatureFile)
                    Dim parts = line.Split("|"c)
                    If parts.Length = 2 Then
                        signatures(parts(0)) = parts(1)
                    End If
                Next
            End If

            Return signatures
        End Function

        Public Sub AddSignature(hash As String, virusName As String)
            File.AppendAllText(_signatureFile, $"{hash}|{virusName}{Environment.NewLine}")
        End Sub

        Public Sub RemoveSignature(hash As String)
            Dim signatures = LoadSignatures()
            signatures.Remove(hash)


            Dim lines = signatures.Select(Function(kvp) $"{kvp.Key}|{kvp.Value}")
            File.WriteAllLines(_signatureFile, lines)
        End Sub

        Public Function GetSignatureCount() As Integer
            Return LoadSignatures().Count
        End Function
    End Class
End Namespace