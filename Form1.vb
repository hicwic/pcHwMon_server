Imports OpenHardwareMonitor.Hardware
Imports System.IO
Imports System.IO.Ports
Imports System.ComponentModel
Imports RTSSSharedMemoryNET

Public Class FormMain

    ReadOnly cp As New Computer()

    ' Dim SerialPort1 As New SerialPort
    Dim ArduinoConnected As Boolean
    Dim RefreshTime As Integer

    'Keys
    Const KeyCPUName As Byte = 1
    Const KeyCPUTemp As Byte = 2
    Const KeyCPULoad As Byte = 3
    Const KeyRAMLoad As Byte = 4

    Const KeyGPUName As Byte = 5
    Const KeyGPUTemp As Byte = 6
    Const KeyGPULoad As Byte = 7
    Const KeyGPUFan As Byte = 8

    Const KeyMBDTemp As Byte = 9
    Const KeyDD1Temp As Byte = 10
    Const KeyDD2Temp As Byte = 11

    Const KeyFPS As Byte = 12

    'Buffers (values)
    Dim ValueCPUName As String = ""
    Dim ValueCPUTemp As SByte = -1
    Dim ValueCPULoad As SByte = -1
    Dim ValueRAMLoad As SByte = -1

    Dim ValueGPUName As String = ""
    Dim ValueGPUTemp As SByte = -1
    Dim ValueGPULoad As SByte = -1
    Dim ValueGPUFan As SByte = -1

    Dim ValueMBDTemp As SByte = -1
    Dim ValueDD1Temp As SByte = -1
    Dim ValueDD2Temp As SByte = -1

    Dim ValueFPS As Short = -1


    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim AtBoot As Boolean = False
        For Each st In My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", False).GetValueNames
            If st = Application.ProductName Then
                AtBoot = True
            End If
        Next

        ToolStripStartAtBoot.Checked = AtBoot

        'Me.WindowState = FormWindowState.Minimized
        ToolStripComboBoxRefreshTime.SelectedIndex = My.Settings.RefreshId
        ToolStripMenuAutoconnect.Checked = My.Settings.AutoConnect
        RefreshTime = (My.Settings.RefreshId + 1) * 1000 '= (ToolStripComboBoxRefreshTime.SelectedIndex + 1) * 1000
        LblRefresh.Text = "Refresh time: " + CStr(RefreshTime) + " milliseconds"
        Timer1.Interval = RefreshTime
        Timer1.Start()
        cp.GPUEnabled = True
        cp.CPUEnabled = True
        cp.RAMEnabled = True
        cp.MainboardEnabled = True
        'cp.FanControllerEnabled = True

        cp.Open()

        'retrieve components name, just once
        For Each hw In cp.Hardware
            Select Case hw.HardwareType
                Case HardwareType.CPU
                    ValueCPUName = hw.Name
                Case HardwareType.GpuNvidia
                    ValueGPUName = hw.Name
            End Select
        Next

    End Sub


    Private Sub Autoconnect()

        For Each sp As String In My.Computer.Ports.SerialPortNames
            Try
                SerialPort1.PortName = sp
                SerialPort1.BaudRate = 9600
                SerialPort1.DataBits = 8
                SerialPort1.Parity = Parity.None
                SerialPort1.StopBits = StopBits.One
                SerialPort1.Handshake = Handshake.None
                SerialPort1.Encoding = System.Text.Encoding.Default
                SerialPort1.ReadTimeout = 1000
                SerialPort1.WriteTimeout = 1000
                SerialPort1.Open()

                Dim ListeningWatch As New Stopwatch
                Dim serialMessage As String

                SerialPort1.Write("*****;")

                ListeningWatch.Start()
                While ListeningWatch.ElapsedMilliseconds < 3000 And ListeningWatch.IsRunning
                    serialMessage = SerialPort1.ReadLine()
                    Label1.Text = serialMessage
                    If serialMessage.Contains("R") Then
                        ListeningWatch.Stop()
                        ConnMenuToolStripMenuItem.Text = "Disconnect"
                        ArduinoConnected = True
                        Label1.Text = "Arduino Connected " + sp

                        SendDataToArduino(KeyCPUName, ValueCPUName)
                        SendDataToArduino(KeyGPUName, ValueGPUName)

                        Exit For
                    End If
                End While
            Catch ex As TimeoutException
                ArduinoConnected = False
                Exit Try
            Catch ey As InvalidOperationException
                ArduinoConnected = False
                Exit Try
            Catch ez As IOException
                ArduinoConnected = False
                Exit Try
            End Try

        Next
        If ArduinoConnected = False Then
            SerialPort1.Close()
        End If
    End Sub

    Private Function KeyToString(key As Byte)

        Select Case key
            Case KeyCPUTemp
                Return "CPU Temperature"
            Case KeyCPULoad
                Return "CPU Load"
            Case KeyGPUTemp
                Return "GPU Temperature"
            Case KeyGPULoad
                Return "GPU Load"
            Case KeyRAMLoad
                Return "RAM Load"
            Case Else
                Return "No Value"
        End Select

    End Function

    Private Sub SendDataToArduino(key As Byte, value As Object)

        If Me.WindowState = FormWindowState.Normal Then
            ListBox1.Items.Add("Updated " + KeyToString(key) + " to value " + CStr(value))
            For i = 0 To ListBox1.Items.Count - 50
                ListBox1.Items.RemoveAt(0)
            Next
            ListBox1.TopIndex = ListBox1.Items.Count - 1
        End If

        ' While SerialPort1.IsOpen
        If ArduinoConnected = True Then
            Try
                SerialPort1.Write(CStr(key) + ":" + CStr(value) + ";")
            Catch ex As InvalidOperationException
                Me.ConnMenuToolStripMenuItem.PerformClick()
            End Try
        End If


    End Sub


    Private Function FormatValue(InValue As Integer) As String
        'TypeFormat values:1=temp,2=load,3=fps
        Dim OutStr As String
        'add spaces to numeric strings, or set to "---" for invalid values
        Select Case InValue
            Case 0 To 9
                OutStr = "  " + CStr(InValue)
            Case 10 To 99
                OutStr = " " + CStr(InValue)
            Case 100 To 998
                OutStr = CStr(InValue)
            Case Else
                OutStr = "---"
        End Select

        Return OutStr
    End Function

    Private Function FormatNames(InValue As String) As String()
        Dim OutValue(1) As String
        Dim tempValue(1) As String
        Dim spaceIndex(0) As Integer
        Dim spaceCount As Integer
        Dim spacePos As Integer
        spaceCount = 0
        spacePos = 0
        'find all the spaces and make an index
        For Each c As Char In InValue
            If c = " " Then
                ReDim Preserve spaceIndex(spaceCount + 1)
                spaceIndex(spaceCount) = spacePos
                spaceCount += 1
            End If
            spacePos += 1
        Next
        Dim strLenHalf = Int(InValue.Length / 2)
        Dim spaceDistance As Integer
        Dim nearestSpace As Integer = InValue.Length
        Dim middleSpace As Integer
        For i As Integer = 0 To spaceCount
            spaceDistance = Math.Abs(spaceIndex(i) - strLenHalf)
            If spaceDistance <= nearestSpace Then
                nearestSpace = spaceDistance
                middleSpace = i
            End If
        Next
        OutValue(0) = InValue.Substring(0, spaceIndex(middleSpace)).Trim
        OutValue(1) = InValue.Substring(spaceIndex(middleSpace)).Trim
        For ii As Integer = 0 To 1
            Do Until OutValue(ii).Length = 14
                If OutValue(ii).Length Mod 2 = 0 Then
                    OutValue(ii) = OutValue(ii) + " "
                Else
                    OutValue(ii) = " " + OutValue(ii)
                End If
            Loop
        Next
        Return OutValue
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Interval = (My.Settings.RefreshId + 1) * 1000
        If ToolStripMenuAutoconnect.Checked = True And ArduinoConnected = False Then
            Autoconnect()
        End If

        For Each hw In cp.Hardware
            hw.Update()
            Select Case hw.HardwareType
                Case HardwareType.CPU   'CPU stuff
                    For Each sensor In hw.Sensors
                        Select Case sensor.SensorType
                            Case SensorType.Temperature
                                If sensor.Name.Contains("Package") And ValueCPUTemp <> CSByte(sensor.Value) Then
                                    ValueCPUTemp = sensor.Value 'CpuTemp
                                    SendDataToArduino(KeyCPUTemp, ValueCPUTemp)
                                End If
                            Case SensorType.Load
                                If sensor.Name.Contains("Total") And ValueCPULoad <> CSByte(sensor.Value) Then
                                    ValueCPULoad = sensor.Value 'CpuLoad
                                    SendDataToArduino(KeyCPULoad, ValueCPULoad)
                                End If
                        End Select
                    Next
                Case HardwareType.GpuNvidia   'GPU stuff
                    For Each sensor In hw.Sensors
                        Select Case sensor.SensorType
                            Case SensorType.Temperature
                                If CSByte(sensor.Value) <> ValueGPUTemp Then
                                    ValueGPUTemp = sensor.Value 'GpuTemp
                                    SendDataToArduino(KeyGPUTemp, ValueGPUTemp)
                                End If
                            Case SensorType.Load
                                If sensor.Name.Contains("Core") And CSByte(sensor.Value) <> ValueGPULoad Then
                                    ValueGPULoad = sensor.Value 'GpuLoad
                                    SendDataToArduino(KeyGPULoad, ValueGPULoad)
                                End If
                            Case SensorType.Fan
                                ValueGPUFan = sensor.Value 'GpuFan
                                SendDataToArduino(KeyGPUFan, ValueGPUFan)
                        End Select
                    Next
                Case HardwareType.RAM 'RAM stuff
                    For Each sensor In hw.Sensors
                        If sensor.SensorType = SensorType.Load And CSByte(sensor.Value) <> ValueRAMLoad Then
                            ValueRAMLoad = sensor.Value 'RAMLoad
                            SendDataToArduino(KeyRAMLoad, ValueRAMLoad)
                        End If
                    Next
                Case HardwareType.Mainboard   'MoBo stuff
                    For Each subHard In hw.SubHardware
                        subHard.Update()
                        For Each sensor In subHard.Sensors
                            If sensor.SensorType = 2 And sensor.Name.Contains("#2") And CSByte(sensor.Value) <> ValueMBDTemp Then
                                ValueMBDTemp = sensor.Value 'MoboTemp
                                SendDataToArduino(KeyMBDTemp, ValueMBDTemp)
                            End If
                        Next
                    Next
            End Select
        Next

        Try
            'FPS
            Dim appEntries = OSD.GetAppEntries()
            For Each app In appEntries
                If app.InstantaneousFrames > 0 Then
                    ValueFPS = CInt(app.InstantaneousFrames) 'FPS
                    SendDataToArduino(KeyFPS, ValueFPS)
                ElseIf ValueFPS <> -1 Then
                    ValueFPS = -1
                    SendDataToArduino(KeyFPS, ValueFPS)
                End If
            Next
        Catch ex As Exception
            If Me.WindowState = FormWindowState.Normal Then
                ListBox1.Items.Add("RTSS is missing... skipping FPS")
                For i = 0 To ListBox1.Items.Count - 50
                    ListBox1.Items.RemoveAt(0)
                Next
                ListBox1.TopIndex = ListBox1.Items.Count - 1
            End If
        End Try


    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.MinimMaximMenuToolStripMenuItem.PerformClick()
    End Sub

    Private Sub FormMain_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        Select Case Me.WindowState
            Case FormWindowState.Minimized
                MinimMaximMenuToolStripMenuItem.Text = "Maximize"
            Case FormWindowState.Normal
                MinimMaximMenuToolStripMenuItem.Text = "Minimize"
        End Select

    End Sub

    Private Sub ConnMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConnMenuToolStripMenuItem.Click
        If ArduinoConnected Then
            SerialPort1.Close()
            ConnMenuToolStripMenuItem.Text = "Connect"
            Label1.Text = "Disconnected"
            ArduinoConnected = False
        Else
            Autoconnect()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub MinimMaximMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MinimMaximMenuToolStripMenuItem.Click
        Select Case Me.WindowState
            Case FormWindowState.Minimized
                Me.WindowState = FormWindowState.Normal
            Case FormWindowState.Normal
                Me.WindowState = FormWindowState.Minimized
        End Select
    End Sub

    Private Sub ToolStripMenuAutoconnect_Click(sender As Object, e As EventArgs) Handles ToolStripMenuAutoconnect.Click
        ToolStripMenuAutoconnect.Checked = Not (ToolStripMenuAutoconnect.Checked)
        My.Settings.AutoConnect = ToolStripMenuAutoconnect.Checked
    End Sub

    Private Sub ToolStripComboBoxRefreshTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBoxRefreshTime.SelectedIndexChanged
        My.Settings.RefreshId = ToolStripComboBoxRefreshTime.SelectedIndex
    End Sub

    Private Sub ToolStripStartAtBoot_Click(sender As Object, e As EventArgs) Handles ToolStripStartAtBoot.Click
        If ToolStripStartAtBoot.Checked Then
            'disable atboot
            Dim taskDelCommand As String = "schtasks /delete /f /tn " + """Apps\PcHwMon_server"""
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
            Shell(taskDelCommand,, True)
            ToolStripStartAtBoot.Checked = False
        Else
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Environment.SystemDirectory + "\schtasks.exe /run /tn ""Apps\pcHwMon_server""")
            ToolStripStartAtBoot.Checked = True
            Dim fileContent As String
            Dim fileWriter As StreamWriter
            Dim taskCommandLine As String = "schtasks /create /xml " + """" + Application.StartupPath + "\temp\pcHwMon_server.xml" + """" + " /tn " + """Apps\PcHwMon_server"""
            My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\temp")
            fileContent = My.Resources.Resource1.String1 + My.User.Name +
                My.Resources.Resource1.String2 + System.Security.Principal.WindowsIdentity.GetCurrent().User.ToString +
                My.Resources.Resource1.String3 + Application.ExecutablePath +
                My.Resources.Resource1.String4
            fileWriter = New StreamWriter(Application.StartupPath + "\temp\pcHwMon_server.xml")
            fileWriter.Write(fileContent)
            fileWriter.Close()
            Shell(taskCommandLine,, True)
            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + "\temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
        My.Computer.Registry.CurrentUser.Close()

    End Sub
End Class
