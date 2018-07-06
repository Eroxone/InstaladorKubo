<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSQL2008R2
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.BtDescargarSQL = New System.Windows.Forms.Button()
        Me.BtUpgrade = New System.Windows.Forms.Button()
        Me.BtUpgradeLuego = New System.Windows.Forms.Button()
        Me.LbTitulo = New System.Windows.Forms.Label()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.TlpDescargarSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpUpgradeSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpUpgradeLuegoSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtManualSQL = New System.Windows.Forms.Button()
        Me.TlpManualSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.LbRutaSQL = New System.Windows.Forms.Label()
        Me.TlpRutaSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtLogSQLR2 = New System.Windows.Forms.Button()
        Me.LbUpgradeLuego = New System.Windows.Forms.Label()
        Me.LbUpgradeLuego2 = New System.Windows.Forms.Label()
        Me.LbUptime = New System.Windows.Forms.Label()
        Me.CBoxEmail = New System.Windows.Forms.ComboBox()
        Me.LbEmail = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtDescargarSQL
        '
        Me.BtDescargarSQL.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtDescargarSQL.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtDescargarSQL.Location = New System.Drawing.Point(12, 79)
        Me.BtDescargarSQL.Name = "BtDescargarSQL"
        Me.BtDescargarSQL.Size = New System.Drawing.Size(140, 50)
        Me.BtDescargarSQL.TabIndex = 0
        Me.BtDescargarSQL.Text = "Descargar SQL"
        Me.BtDescargarSQL.UseVisualStyleBackColor = True
        '
        'BtUpgrade
        '
        Me.BtUpgrade.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtUpgrade.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtUpgrade.Location = New System.Drawing.Point(211, 79)
        Me.BtUpgrade.Name = "BtUpgrade"
        Me.BtUpgrade.Size = New System.Drawing.Size(127, 50)
        Me.BtUpgrade.TabIndex = 1
        Me.BtUpgrade.Text = " Upgrade desatendido"
        Me.BtUpgrade.UseVisualStyleBackColor = True
        '
        'BtUpgradeLuego
        '
        Me.BtUpgradeLuego.BackColor = System.Drawing.SystemColors.Control
        Me.BtUpgradeLuego.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtUpgradeLuego.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtUpgradeLuego.Location = New System.Drawing.Point(399, 79)
        Me.BtUpgradeLuego.Name = "BtUpgradeLuego"
        Me.BtUpgradeLuego.Size = New System.Drawing.Size(127, 50)
        Me.BtUpgradeLuego.TabIndex = 2
        Me.BtUpgradeLuego.Text = "Upgrade a las 22:00 horas"
        Me.BtUpgradeLuego.UseVisualStyleBackColor = False
        '
        'LbTitulo
        '
        Me.LbTitulo.AutoSize = True
        Me.LbTitulo.Font = New System.Drawing.Font("Lucida Bright", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.LbTitulo.Location = New System.Drawing.Point(8, 26)
        Me.LbTitulo.Name = "LbTitulo"
        Me.LbTitulo.Size = New System.Drawing.Size(410, 24)
        Me.LbTitulo.TabIndex = 3
        Me.LbTitulo.Text = "ACTUALIZACIÓN A SQL 2008 R2 X86"
        '
        'BtSalir
        '
        Me.BtSalir.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtSalir.Location = New System.Drawing.Point(508, 257)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(103, 35)
        Me.BtSalir.TabIndex = 4
        Me.BtSalir.Text = "SALIR"
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'TlpDescargarSQL
        '
        Me.TlpDescargarSQL.IsBalloon = True
        Me.TlpDescargarSQL.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TlpUpgradeSQL
        '
        Me.TlpUpgradeSQL.IsBalloon = True
        Me.TlpUpgradeSQL.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TlpUpgradeLuegoSQL
        '
        Me.TlpUpgradeLuegoSQL.IsBalloon = True
        Me.TlpUpgradeLuegoSQL.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'BtManualSQL
        '
        Me.BtManualSQL.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtManualSQL.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtManualSQL.Location = New System.Drawing.Point(211, 150)
        Me.BtManualSQL.Name = "BtManualSQL"
        Me.BtManualSQL.Size = New System.Drawing.Size(127, 50)
        Me.BtManualSQL.TabIndex = 5
        Me.BtManualSQL.Text = "Actualización Manual"
        Me.BtManualSQL.UseVisualStyleBackColor = True
        '
        'TlpManualSQL
        '
        Me.TlpManualSQL.IsBalloon = True
        Me.TlpManualSQL.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'LbRutaSQL
        '
        Me.LbRutaSQL.AutoSize = True
        Me.LbRutaSQL.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.LbRutaSQL.Font = New System.Drawing.Font("Lucida Bright", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbRutaSQL.Location = New System.Drawing.Point(9, 227)
        Me.LbRutaSQL.Name = "LbRutaSQL"
        Me.LbRutaSQL.Size = New System.Drawing.Size(108, 15)
        Me.LbRutaSQL.TabIndex = 6
        Me.LbRutaSQL.Text = "Ruta de trabajo"
        '
        'TlpRutaSQL
        '
        Me.TlpRutaSQL.IsBalloon = True
        Me.TlpRutaSQL.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'BtLogSQLR2
        '
        Me.BtLogSQLR2.Font = New System.Drawing.Font("Lucida Bright", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtLogSQLR2.Location = New System.Drawing.Point(12, 155)
        Me.BtLogSQLR2.Name = "BtLogSQLR2"
        Me.BtLogSQLR2.Size = New System.Drawing.Size(90, 45)
        Me.BtLogSQLR2.TabIndex = 7
        Me.BtLogSQLR2.Text = "Visualizar Logs"
        Me.BtLogSQLR2.UseVisualStyleBackColor = True
        '
        'LbUpgradeLuego
        '
        Me.LbUpgradeLuego.AutoSize = True
        Me.LbUpgradeLuego.BackColor = System.Drawing.SystemColors.Control
        Me.LbUpgradeLuego.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbUpgradeLuego.ForeColor = System.Drawing.Color.Maroon
        Me.LbUpgradeLuego.Location = New System.Drawing.Point(396, 141)
        Me.LbUpgradeLuego.Name = "LbUpgradeLuego"
        Me.LbUpgradeLuego.Size = New System.Drawing.Size(184, 15)
        Me.LbUpgradeLuego.TabIndex = 8
        Me.LbUpgradeLuego.Text = "EJECUCIÓN PROGRAMADA"
        Me.LbUpgradeLuego.Visible = False
        '
        'LbUpgradeLuego2
        '
        Me.LbUpgradeLuego2.AutoSize = True
        Me.LbUpgradeLuego2.BackColor = System.Drawing.SystemColors.Control
        Me.LbUpgradeLuego2.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbUpgradeLuego2.ForeColor = System.Drawing.Color.Maroon
        Me.LbUpgradeLuego2.Location = New System.Drawing.Point(396, 169)
        Me.LbUpgradeLuego2.Name = "LbUpgradeLuego2"
        Me.LbUpgradeLuego2.Size = New System.Drawing.Size(195, 15)
        Me.LbUpgradeLuego2.TabIndex = 9
        Me.LbUpgradeLuego2.Text = "NO CIERRES ESTA VENTANA"
        Me.LbUpgradeLuego2.Visible = False
        '
        'LbUptime
        '
        Me.LbUptime.AutoSize = True
        Me.LbUptime.Font = New System.Drawing.Font("Lucida Bright", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LbUptime.Location = New System.Drawing.Point(396, 226)
        Me.LbUptime.Name = "LbUptime"
        Me.LbUptime.Size = New System.Drawing.Size(125, 16)
        Me.LbUptime.TabIndex = 10
        Me.LbUptime.Text = "Uptime Servidor"
        '
        'CBoxEmail
        '
        Me.CBoxEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBoxEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBoxEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CBoxEmail.Font = New System.Drawing.Font("Lucida Bright", 8.25!)
        Me.CBoxEmail.FormattingEnabled = True
        Me.CBoxEmail.Items.AddRange(New Object() {"atencionalcliente@notin.net", "carmona@notin.net", "clemente@notin.net", "cristian@notin.net", "dperez@notin.net", "extremadura@notin.net", "gerard@notin.net", "granada@notin.net", "jaime@notin.net", "jlozano@notin.net", "jonatan@notin.net", "jorge@notin.net", "josechumillas@notin.net", "jramon@notin.net", "juanjo@notin.net", "logistica@notin.net", "madrid@notin.net", "malaga@notin.net", "manolo@notin.net", "mariano@notin.net", "montes@notin.net", "noguera@notin.net", "nemo@notin.net", "noguera@notin.net", "oscar@notin.net", "pablo@notin.net", "pascual@notin.net", "roberto@notin.net", "ruben@notin.net", "sevilla@notin.net", "sistemas@notin.net", "taller@notin.net", "valencia@notin.net"})
        Me.CBoxEmail.Location = New System.Drawing.Point(199, 270)
        Me.CBoxEmail.Name = "CBoxEmail"
        Me.CBoxEmail.Size = New System.Drawing.Size(139, 22)
        Me.CBoxEmail.TabIndex = 50
        '
        'LbEmail
        '
        Me.LbEmail.AutoSize = True
        Me.LbEmail.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbEmail.Location = New System.Drawing.Point(66, 272)
        Me.LbEmail.Name = "LbEmail"
        Me.LbEmail.Size = New System.Drawing.Size(127, 20)
        Me.LbEmail.TabIndex = 51
        Me.LbEmail.Text = "Indica tu Email:"
        '
        'FormSQL2008R2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 301)
        Me.Controls.Add(Me.LbEmail)
        Me.Controls.Add(Me.CBoxEmail)
        Me.Controls.Add(Me.LbUptime)
        Me.Controls.Add(Me.LbUpgradeLuego2)
        Me.Controls.Add(Me.LbUpgradeLuego)
        Me.Controls.Add(Me.BtLogSQLR2)
        Me.Controls.Add(Me.LbRutaSQL)
        Me.Controls.Add(Me.BtManualSQL)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.LbTitulo)
        Me.Controls.Add(Me.BtUpgradeLuego)
        Me.Controls.Add(Me.BtUpgrade)
        Me.Controls.Add(Me.BtDescargarSQL)
        Me.Name = "FormSQL2008R2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización SQL 2008R2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtDescargarSQL As Button
    Friend WithEvents BtUpgrade As Button
    Friend WithEvents BtUpgradeLuego As Button
    Friend WithEvents LbTitulo As Label
    Friend WithEvents BtSalir As Button
    Friend WithEvents TlpDescargarSQL As ToolTip
    Friend WithEvents TlpUpgradeSQL As ToolTip
    Friend WithEvents TlpUpgradeLuegoSQL As ToolTip
    Friend WithEvents BtManualSQL As Button
    Friend WithEvents TlpManualSQL As ToolTip
    Friend WithEvents LbRutaSQL As Label
    Friend WithEvents TlpRutaSQL As ToolTip
    Friend WithEvents BtLogSQLR2 As Button
    Friend WithEvents LbUpgradeLuego As Label
    Friend WithEvents LbUpgradeLuego2 As Label
    Friend WithEvents LbUptime As Label
    Friend WithEvents CBoxEmail As ComboBox
    Friend WithEvents LbEmail As Label
End Class
