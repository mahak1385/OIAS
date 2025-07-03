Imports System.Windows.Controls
Imports Core
Imports Core.Core

Public Class HomePage
    Inherits Page

    Private ReadOnly _monitor As RealTimeMonitor
    Private ReadOnly _scanner As Scanner

    Public Sub New(monitor As RealTimeMonitor, scanner As Scanner)
        InitializeComponent()
        _monitor = monitor
        _scanner = scanner
        UpdateStatus()
    End Sub

    Private Sub UpdateStatus()

        If _monitor IsNot Nothing AndAlso _monitor.MonitoredPaths IsNot Nothing Then
            txtMonitoringStatus.Text = "Real-time Protection: " &
                If(_monitor.MonitoredPaths.Count > 0, "On", "Off")
        Else
            txtMonitoringStatus.Text = "Real-time Protection: Off"
        End If


        txtLastScan.Text = "Last Scan: " & GetLastScanTime()
    End Sub

    Private Function GetLastScanTime() As String

        Return "Never"
    End Function
End Class
