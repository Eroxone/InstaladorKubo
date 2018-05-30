<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSQLInstalacion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSQLInstalacion))
        Me.BtDescargarSQL = New System.Windows.Forms.Button()
        Me.LbSQL2014 = New System.Windows.Forms.Label()
        Me.LbDatosUsuario = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TbBDUsuario = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TbBackup = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TlpInstalaSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'BtDescargarSQL
        '
        Me.BtDescargarSQL.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold)
        Me.BtDescargarSQL.Location = New System.Drawing.Point(214, 276)
        Me.BtDescargarSQL.Margin = New System.Windows.Forms.Padding(4)
        Me.BtDescargarSQL.Name = "BtDescargarSQL"
        Me.BtDescargarSQL.Size = New System.Drawing.Size(204, 61)
        Me.BtDescargarSQL.TabIndex = 0
        Me.BtDescargarSQL.Text = "COMENZAR"
        Me.BtDescargarSQL.UseVisualStyleBackColor = True
        '
        'LbSQL2014
        '
        Me.LbSQL2014.AutoSize = True
        Me.LbSQL2014.Font = New System.Drawing.Font("Lucida Bright", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.LbSQL2014.Location = New System.Drawing.Point(142, 9)
        Me.LbSQL2014.Name = "LbSQL2014"
        Me.LbSQL2014.Size = New System.Drawing.Size(427, 28)
        Me.LbSQL2014.TabIndex = 1
        Me.LbSQL2014.Text = "Instalación SQL 2014 Business x64"
        '
        'LbDatosUsuario
        '
        Me.LbDatosUsuario.AutoSize = True
        Me.LbDatosUsuario.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbDatosUsuario.Location = New System.Drawing.Point(28, 169)
        Me.LbDatosUsuario.Name = "LbDatosUsuario"
        Me.LbDatosUsuario.Size = New System.Drawing.Size(152, 23)
        Me.LbDatosUsuario.TabIndex = 3
        Me.LbDatosUsuario.Text = "B.D. USUARIO:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(28, 218)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 23)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "DB BACKUP:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(28, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 23)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "INSTANCIA:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "INSTALACIÓN:"
        '
        'TbBDUsuario
        '
        Me.TbBDUsuario.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TbBDUsuario.Location = New System.Drawing.Point(214, 166)
        Me.TbBDUsuario.Name = "TbBDUsuario"
        Me.TbBDUsuario.Size = New System.Drawing.Size(505, 31)
        Me.TbBDUsuario.TabIndex = 7
        Me.TbBDUsuario.Text = "G:\RESPALDO\F\NOTAWIN.NET"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TextBox1.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TextBox1.Location = New System.Drawing.Point(214, 70)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(505, 31)
        Me.TextBox1.TabIndex = 8
        Me.TextBox1.Text = "C:\Program Files\Microsoft SQL Server"
        '
        'TbBackup
        '
        Me.TbBackup.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TbBackup.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TbBackup.Location = New System.Drawing.Point(214, 215)
        Me.TbBackup.Name = "TbBackup"
        Me.TbBackup.ReadOnly = True
        Me.TbBackup.Size = New System.Drawing.Size(505, 31)
        Me.TbBackup.TabIndex = 9
        Me.TbBackup.Text = "G:\RESPALDO\F\NOTAWIN.NET\BACKUP"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TextBox3.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TextBox3.Location = New System.Drawing.Point(214, 119)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(505, 31)
        Me.TextBox3.TabIndex = 10
        Me.TextBox3.Text = "MSSQLSERVER"
        '
        'TlpInstalaSQL
        '
        Me.TlpInstalaSQL.IsBalloon = True
        Me.TlpInstalaSQL.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'FrmSQLInstalacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 359)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TbBackup)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TbBDUsuario)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LbDatosUsuario)
        Me.Controls.Add(Me.LbSQL2014)
        Me.Controls.Add(Me.BtDescargarSQL)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmSQLInstalacion"
        Me.Text = "SQLInstalacion"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtDescargarSQL As Button
    Friend WithEvents LbSQL2014 As Label
    Friend WithEvents LbDatosUsuario As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TbBDUsuario As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TbBackup As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TlpInstalaSQL As ToolTip
End Class
