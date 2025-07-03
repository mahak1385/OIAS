Imports System.IO

Namespace Core
    Public Class Settings
        Private Shared ReadOnly _settingsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.ini")


        Public Shared Property AutoStartProtection As Boolean
            Get
                Return GetSetting("AutoStartProtection", "False")
            End Get
            Set(value As Boolean)
                SaveSetting("AutoStartProtection", value.ToString())
            End Set
        End Property

        Public Shared Property ScanArchives As Boolean
            Get
                Return GetSetting("ScanArchives", "True")
            End Get
            Set(value As Boolean)
                SaveSetting("ScanArchives", value.ToString())
            End Set
        End Property

        Public Shared Property DeepScan As Boolean
            Get
                Return GetSetting("DeepScan", "False")
            End Get
            Set(value As Boolean)
                SaveSetting("DeepScan", value.ToString())
            End Set
        End Property

        Public Shared Property AutoQuarantine As Boolean
            Get
                Return GetSetting("AutoQuarantine", "True")
            End Get
            Set(value As Boolean)
                SaveSetting("AutoQuarantine", value.ToString())
            End Set
        End Property

        Public Shared Property AutoUpdate As Boolean
            Get
                Return GetSetting("AutoUpdate", "True")
            End Get
            Set(value As Boolean)
                SaveSetting("AutoUpdate", value.ToString())
            End Set
        End Property

        Public Shared Property LastDatabaseUpdate As DateTime
            Get
                Dim dateStr = GetSetting("LastDatabaseUpdate", DateTime.MinValue.ToString())
                Return DateTime.Parse(dateStr)
            End Get
            Set(value As DateTime)
                SaveSetting("LastDatabaseUpdate", value.ToString())
            End Set
        End Property


        Private Shared Function GetSetting(key As String, defaultValue As String) As String
            Try
                If File.Exists(_settingsPath) Then
                    Dim lines = File.ReadAllLines(_settingsPath)
                    For Each line In lines
                        Dim parts = line.Split("="c)
                        If parts.Length = 2 AndAlso parts(0).Trim() = key Then
                            Return parts(1).Trim()
                        End If
                    Next
                End If
            Catch ex As Exception
                Debug.WriteLine($"Error reading setting {key}: {ex.Message}")
            End Try
            Return defaultValue
        End Function

        Private Shared Sub SaveSetting(key As String, value As String)
            Try
                Dim settings As New Dictionary(Of String, String)


                If File.Exists(_settingsPath) Then
                    For Each line In File.ReadAllLines(_settingsPath)
                        Dim parts = line.Split("="c)
                        If parts.Length = 2 Then
                            settings(parts(0).Trim()) = parts(1).Trim()
                        End If
                    Next
                End If


                settings(key) = value


                Using writer As New StreamWriter(_settingsPath, False)
                    For Each setting In settings
                        writer.WriteLine($"{setting.Key}={setting.Value}")
                    Next
                End Using
            Catch ex As Exception
                Debug.WriteLine($"Error saving setting {key}: {ex.Message}")
            End Try
        End Sub
    End Class
End Namespace