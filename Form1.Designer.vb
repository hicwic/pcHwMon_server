<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripStartAtBoot = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuAutoconnect = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnMenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshtimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripComboBoxRefreshTime = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MinimMaximMenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LblRefresh = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(47, 67)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(338, 212)
        Me.ListBox1.TabIndex = 1
        '
        'Timer1
        '
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "pcHwMon"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStartAtBoot, Me.ToolStripSeparator4, Me.ToolStripMenuAutoconnect, Me.ConnMenuToolStripMenuItem, Me.ToolStripSeparator2, Me.RefreshtimeToolStripMenuItem, Me.ToolStripSeparator3, Me.MinimMaximMenuToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(178, 160)
        '
        'ToolStripStartAtBoot
        '
        Me.ToolStripStartAtBoot.Name = "ToolStripStartAtBoot"
        Me.ToolStripStartAtBoot.Size = New System.Drawing.Size(177, 22)
        Me.ToolStripStartAtBoot.Text = "Start at boot"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(174, 6)
        '
        'ToolStripMenuAutoconnect
        '
        Me.ToolStripMenuAutoconnect.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton
        Me.ToolStripMenuAutoconnect.Name = "ToolStripMenuAutoconnect"
        Me.ToolStripMenuAutoconnect.Size = New System.Drawing.Size(177, 22)
        Me.ToolStripMenuAutoconnect.Text = "Autoconnect"
        '
        'ConnMenuToolStripMenuItem
        '
        Me.ConnMenuToolStripMenuItem.Name = "ConnMenuToolStripMenuItem"
        Me.ConnMenuToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ConnMenuToolStripMenuItem.Text = "ConnMenu"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(174, 6)
        '
        'RefreshtimeToolStripMenuItem
        '
        Me.RefreshtimeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBoxRefreshTime})
        Me.RefreshtimeToolStripMenuItem.Name = "RefreshtimeToolStripMenuItem"
        Me.RefreshtimeToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RefreshtimeToolStripMenuItem.Text = "Refresh Time"
        '
        'ToolStripComboBoxRefreshTime
        '
        Me.ToolStripComboBoxRefreshTime.Items.AddRange(New Object() {"1 second", "2 seconds", "3 seconds", "4 seconds", "5 seconds", "6 seconds", "7 seconds", "8 seconds", "9 seconds", "10 seconds"})
        Me.ToolStripComboBoxRefreshTime.Name = "ToolStripComboBoxRefreshTime"
        Me.ToolStripComboBoxRefreshTime.Size = New System.Drawing.Size(121, 23)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(174, 6)
        '
        'MinimMaximMenuToolStripMenuItem
        '
        Me.MinimMaximMenuToolStripMenuItem.Name = "MinimMaximMenuToolStripMenuItem"
        Me.MinimMaximMenuToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.MinimMaximMenuToolStripMenuItem.Text = "MinimMaximMenu"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(174, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'LblRefresh
        '
        Me.LblRefresh.AutoSize = True
        Me.LblRefresh.Location = New System.Drawing.Point(56, 330)
        Me.LblRefresh.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblRefresh.Name = "LblRefresh"
        Me.LblRefresh.Size = New System.Drawing.Size(39, 13)
        Me.LblRefresh.TabIndex = 4
        Me.LblRefresh.Text = "Label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(56, 379)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Disconnected"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 439)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblRefresh)
        Me.Controls.Add(Me.ListBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "pcHwMon_server"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents LblRefresh As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ConnMenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MinimMaximMenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshtimeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripComboBoxRefreshTime As ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuAutoconnect As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripStartAtBoot As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
End Class
