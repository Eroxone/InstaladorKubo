<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormUsuarioAdra
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
        Me.LBoxUsuarios = New System.Windows.Forms.ListBox()
        Me.LbUsuario = New System.Windows.Forms.Label()
        Me.BtLimpiar = New System.Windows.Forms.Button()
        Me.LbNotaria = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.BtRegistroInstalaciones = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LBoxUsuarios
        '
        Me.LBoxUsuarios.Font = New System.Drawing.Font("Lucida Bright", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBoxUsuarios.FormattingEnabled = True
        Me.LBoxUsuarios.ItemHeight = 17
        Me.LBoxUsuarios.Location = New System.Drawing.Point(25, 42)
        Me.LBoxUsuarios.Name = "LBoxUsuarios"
        Me.LBoxUsuarios.Size = New System.Drawing.Size(210, 89)
        Me.LBoxUsuarios.TabIndex = 0
        '
        'LbUsuario
        '
        Me.LbUsuario.AutoSize = True
        Me.LbUsuario.BackColor = System.Drawing.SystemColors.Control
        Me.LbUsuario.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LbUsuario.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.LbUsuario.Location = New System.Drawing.Point(113, 143)
        Me.LbUsuario.Name = "LbUsuario"
        Me.LbUsuario.Size = New System.Drawing.Size(109, 18)
        Me.LbUsuario.TabIndex = 1
        Me.LbUsuario.Text = "UsuarioAdra"
        Me.LbUsuario.Visible = False
        '
        'BtLimpiar
        '
        Me.BtLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtLimpiar.Font = New System.Drawing.Font("Lucida Bright", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtLimpiar.Location = New System.Drawing.Point(25, 225)
        Me.BtLimpiar.Name = "BtLimpiar"
        Me.BtLimpiar.Size = New System.Drawing.Size(116, 57)
        Me.BtLimpiar.TabIndex = 2
        Me.BtLimpiar.Text = "LIMPIAR"
        Me.BtLimpiar.UseVisualStyleBackColor = True
        '
        'LbNotaria
        '
        Me.LbNotaria.AutoSize = True
        Me.LbNotaria.BackColor = System.Drawing.SystemColors.Control
        Me.LbNotaria.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LbNotaria.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.LbNotaria.Location = New System.Drawing.Point(22, 143)
        Me.LbNotaria.Name = "LbNotaria"
        Me.LbNotaria.Size = New System.Drawing.Size(95, 18)
        Me.LbNotaria.TabIndex = 3
        Me.LbNotaria.Text = "NOTARIA\"
        Me.LbNotaria.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Lucida Bright", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(304, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Selecciona usuario AdRA del Equipo:"
        '
        'BtSalir
        '
        Me.BtSalir.Font = New System.Drawing.Font("Lucida Bright", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSalir.Location = New System.Drawing.Point(412, 245)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(81, 37)
        Me.BtSalir.TabIndex = 5
        Me.BtSalir.Text = "Salir"
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'BtRegistroInstalaciones
        '
        Me.BtRegistroInstalaciones.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.BtRegistroInstalaciones.Font = New System.Drawing.Font("Lucida Bright", 10.2!)
        Me.BtRegistroInstalaciones.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtRegistroInstalaciones.Location = New System.Drawing.Point(364, 42)
        Me.BtRegistroInstalaciones.Name = "BtRegistroInstalaciones"
        Me.BtRegistroInstalaciones.Size = New System.Drawing.Size(129, 28)
        Me.BtRegistroInstalaciones.TabIndex = 6
        Me.BtRegistroInstalaciones.Text = "Visualizar Log"
        Me.BtRegistroInstalaciones.UseVisualStyleBackColor = False
        '
        'FormUsuarioAdra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 294)
        Me.Controls.Add(Me.BtRegistroInstalaciones)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LbNotaria)
        Me.Controls.Add(Me.BtLimpiar)
        Me.Controls.Add(Me.LbUsuario)
        Me.Controls.Add(Me.LBoxUsuarios)
        Me.Name = "FormUsuarioAdra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormUsuarioAdra"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LBoxUsuarios As ListBox
    Friend WithEvents LbUsuario As Label
    Friend WithEvents BtLimpiar As Button
    Friend WithEvents LbNotaria As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents BtSalir As Button
    Friend WithEvents BtRegistroInstalaciones As Button
End Class
