Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports Core
Imports Core.Core


Class MainWindow
    Private ReadOnly _monitor As Core.RealTimeMonitor
    Private ReadOnly _scanner As Core.Scanner
    Private ReadOnly _signatureManager As Core.SignatureManager

    Public Sub New()
        InitializeComponent()

        _scanner = New Core.Scanner()
        _signatureManager = New Core.SignatureManager()
        _monitor = New Core.RealTimeMonitor()

        AddHandler _monitor.ThreatDetected, AddressOf OnThreatDetected

        MainFrame.Navigate(New HomePage(_monitor, _scanner))
    End Sub

    Private Sub OnThreatDetected(sender As Object, filePath As String, threatName As String)
        MessageBox.Show($"Threat detected!{Environment.NewLine}File: {filePath}{Environment.NewLine}Threat: {threatName}",
                       "Threat Alert", MessageBoxButton.OK, MessageBoxImage.Warning)
    End Sub

    Private Sub BtnHome_Click(sender As Object, e As RoutedEventArgs)
        MainFrame.Navigate(New HomePage(_monitor, _scanner))
    End Sub

    Private Sub BtnScan_Click(sender As Object, e As RoutedEventArgs)
        MainFrame.Navigate(New ScanPage())
    End Sub

    Private Sub BtnQuarantine_Click(sender As Object, e As RoutedEventArgs)

        Dim qPage As New QuarantinePage()
        MainFrame.Navigate(qPage)
        'MainFrame.Navigate(New QuarantinePage())
    End Sub

    Private Sub BtnDatabase_Click(sender As Object, e As RoutedEventArgs)
        MainFrame.Navigate(New VirusDatabasePage())
    End Sub

    Private Sub BtnSettings_Click(sender As Object, e As RoutedEventArgs)
        MainFrame.Navigate(New SettingsPage())
    End Sub

    Protected Overrides Sub OnClosing(e As ComponentModel.CancelEventArgs)
        _monitor.StopAllMonitoring()
        MyBase.OnClosing(e)
    End Sub
End Class