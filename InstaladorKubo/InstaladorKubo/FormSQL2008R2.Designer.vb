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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.TlpDescargarSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpUpgradeSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpUpgradeLuegoSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtManualSQL = New System.Windows.Forms.Button()
        Me.TlpManualSQL = New System.Windows.Forms.ToolTip(Me.components)
        Me.LbRutaSQL = New System.Windows.Forms.Label()
        Me.TlpRutaSQL = New System.Windows.Forms.ToolTip(Me.components)
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
        Me.BtUpgrade.Text = "Ejecutar el Upgrade"
        Me.BtUpgrade.UseVisualStyleBackColor = True
        '
        'BtUpgradeLuego
        '
        Me.BtUpgradeLuego.Enabled = False
        Me.BtUpgradeLuego.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtUpgradeLuego.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtUpgradeLuego.Location = New System.Drawing.Point(399, 79)
        Me.BtUpgradeLuego.Name = "BtUpgradeLuego"
        Me.BtUpgradeLuego.Size = New System.Drawing.Size(127, 50)
        Me.BtUpgradeLuego.TabIndex = 2
        Me.BtUpgradeLuego.Text = "Upgrade a las 22:00 horas"
        Me.BtUpgradeLuego.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Lucida Bright", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.Label1.Location = New System.Drawing.Point(8, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(518, 24)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "INSTALACIÓN DESATENDIDA SQL 2008 R2 X86"
        '
        'BtSalir
        '
        Me.BtSalir.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtSalir.Location = New System.Drawing.Point(423, 231)
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
        Me.LbRutaSQL.Location = New System.Drawing.Point(9, 242)
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
        'FormSQL2008R2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(541, 278)
        Me.Controls.Add(Me.LbRutaSQL)
        Me.Controls.Add(Me.BtManualSQL)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.Label1)
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
    Friend WithEvents Label1 As Label
    Friend WithEvents BtSalir As Button
    Friend WithEvents TlpDescargarSQL As ToolTip
    Friend WithEvents TlpUpgradeSQL As ToolTip
    Friend WithEvents TlpUpgradeLuegoSQL As ToolTip
    Friend WithEvents BtManualSQL As Button
    Friend WithEvents TlpManualSQL As ToolTip
    Friend WithEvents LbRutaSQL As Label
    Friend WithEvents TlpRutaSQL As ToolTip
End Class
