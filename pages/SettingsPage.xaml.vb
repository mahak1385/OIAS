Namespace Core

    Partial Public Class SettingsPage
        Inherits Page

        Public Sub New()
            InitializeComponent()
            LoadSettings()
        End Sub

        Private Sub LoadSettings()
            ' Load saved settings
            Dim scanType = My.Settings.ScanType
            Select Case scanType
                Case "Quick"
                    ScanTypeBox.SelectedIndex = 0
                Case "Full"
                    ScanTypeBox.SelectedIndex = 1
                Case "Custom"
                    ScanTypeBox.SelectedIndex = 2
                Case Else
                    ScanTypeBox.SelectedIndex = 0 ' default
            End Select

            AutoUpdateCheck.IsChecked = My.Settings.AutoUpdate
            ScanTimeBox.Text = My.Settings.ScheduledScanTime
        End Sub




        Private Sub SaveSettings_Click(sender As Object, e As RoutedEventArgs)
            Dim selectedScanItem = CType(ScanTypeBox.SelectedItem, ComboBoxItem)
            If selectedScanItem IsNot Nothing Then
                My.Settings.ScanType = selectedScanItem.Content.ToString()
            End If

            My.Settings.AutoUpdate = AutoUpdateCheck.IsChecked
            My.Settings.ScheduledScanTime = ScanTimeBox.Text
            My.Settings.Save()

            MsgBox("Settings saved successfully!", MsgBoxStyle.Information)
        End Sub


    End Class
End Namespace