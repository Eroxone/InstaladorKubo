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
        Me.TbInstancia = New System.Windows.Forms.TextBox()
        Me.TlpInstalaSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtDescargarSQL
        '
        Me.BtDescargarSQL.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtDescargarSQL.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold)
        Me.BtDescargarSQL.Location = New System.Drawing.Point(226, 230)
        Me.BtDescargarSQL.Name = "BtDescargarSQL"
        Me.BtDescargarSQL.Size = New System.Drawing.Size(153, 50)
        Me.BtDescargarSQL.TabIndex = 0
        Me.BtDescargarSQL.Text = "COMENZAR"
        Me.BtDescargarSQL.UseVisualStyleBackColor = True
        '
        'LbSQL2014
        '
        Me.LbSQL2014.AutoSize = True
        Me.LbSQL2014.Font = New System.Drawing.Font("Lucida Bright", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbSQL2014.Location = New System.Drawing.Point(156, 9)
        Me.LbSQL2014.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LbSQL2014.Name = "LbSQL2014"
        Me.LbSQL2014.Size = New System.Drawing.Size(377, 24)
        Me.LbSQL2014.TabIndex = 1
        Me.LbSQL2014.Text = "Instalación SQL 2014 Business x64"
        '
        'LbDatosUsuario
        '
        Me.LbDatosUsuario.AutoSize = True
        Me.LbDatosUsuario.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbDatosUsuario.Location = New System.Drawing.Point(21, 137)
        Me.LbDatosUsuario.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LbDatosUsuario.Name = "LbDatosUsuario"
        Me.LbDatosUsuario.Size = New System.Drawing.Size(124, 18)
        Me.LbDatosUsuario.TabIndex = 3
        Me.LbDatosUsuario.Text = "B.D. USUARIO:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 177)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "DB BACKUP:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(21, 99)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 18)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "INSTANCIA:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 59)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 18)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "INSTALACIÓN:"
        '
        'TbBDUsuario
        '
        Me.TbBDUsuario.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TbBDUsuario.Location = New System.Drawing.Point(160, 135)
        Me.TbBDUsuario.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TbBDUsuario.Name = "TbBDUsuario"
        Me.TbBDUsuario.Size = New System.Drawing.Size(380, 26)
        Me.TbBDUsuario.TabIndex = 7
        Me.TbBDUsuario.Text = "G:\RESPALDO\F\NOTAWIN.NET"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TextBox1.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TextBox1.Location = New System.Drawing.Point(160, 57)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(380, 26)
        Me.TextBox1.TabIndex = 8
        Me.TextBox1.Text = "C:\Program Files\Microsoft SQL Server"
        '
        'TbBackup
        '
        Me.TbBackup.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TbBackup.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TbBackup.Location = New System.Drawing.Point(160, 175)
        Me.TbBackup.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TbBackup.Name = "TbBackup"
        Me.TbBackup.ReadOnly = True
        Me.TbBackup.Size = New System.Drawing.Size(380, 26)
        Me.TbBackup.TabIndex = 9
        Me.TbBackup.Text = "G:\RESPALDO\F\NOTAWIN.NET\BACKUP"
        '
        'TbInstancia
        '
        Me.TbInstancia.BackColor = System.Drawing.SystemColors.Window
        Me.TbInstancia.Font = New System.Drawing.Font("Lucida Bright", 12.0!)
        Me.TbInstancia.Location = New System.Drawing.Point(160, 97)
        Me.TbInstancia.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TbInstancia.Name = "TbInstancia"
        Me.TbInstancia.Size = New System.Drawing.Size(380, 26)
        Me.TbInstancia.TabIndex = 10
        Me.TbInstancia.Text = "MSSQLSERVER"
        '
        'TlpInstalaSQL
        '
        Me.TlpInstalaSQL.IsBalloon = True
        Me.TlpInstalaSQL.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'BtSalir
        '
        Me.BtSalir.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold)
        Me.BtSalir.Location = New System.Drawing.Point(497, 246)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(94, 34)
        Me.BtSalir.TabIndex = 11
        Me.BtSalir.Text = "Salir"
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'FrmSQLInstalacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 292)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.TbInstancia)
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
        Me.Name = "FrmSQLInstalacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
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
    Friend WithEvents TbInstancia As TextBox
    Friend WithEvents TlpInstalaSQL As ToolTip
    Friend WithEvents BtSalir As Button
End Class
