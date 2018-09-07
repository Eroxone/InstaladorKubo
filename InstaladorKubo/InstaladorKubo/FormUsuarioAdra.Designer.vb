<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormUsuarioAdra
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormUsuarioAdra))
        Me.LBoxUsuarios = New System.Windows.Forms.ListBox()
        Me.LbUsuario = New System.Windows.Forms.Label()
        Me.BtLimpiar = New System.Windows.Forms.Button()
        Me.LbNotaria = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.BtRegistroInstalaciones = New System.Windows.Forms.Button()
        Me.BtCerrarsesion = New System.Windows.Forms.Button()
        Me.BtLimpiarIconos = New System.Windows.Forms.Button()
        Me.BtCopiarIconosNR = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
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
        Me.BtLimpiar.Font = New System.Drawing.Font("Lucida Bright", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtLimpiar.Image = CType(resources.GetObject("BtLimpiar.Image"), System.Drawing.Image)
        Me.BtLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtLimpiar.Location = New System.Drawing.Point(25, 243)
        Me.BtLimpiar.Name = "BtLimpiar"
        Me.BtLimpiar.Size = New System.Drawing.Size(166, 57)
        Me.BtLimpiar.TabIndex = 2
        Me.BtLimpiar.Text = "LIMPIAR"
        Me.BtLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.BtSalir.Location = New System.Drawing.Point(469, 265)
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
        Me.BtRegistroInstalaciones.Location = New System.Drawing.Point(25, 201)
        Me.BtRegistroInstalaciones.Name = "BtRegistroInstalaciones"
        Me.BtRegistroInstalaciones.Size = New System.Drawing.Size(129, 28)
        Me.BtRegistroInstalaciones.TabIndex = 6
        Me.BtRegistroInstalaciones.Text = "Visualizar Log"
        Me.BtRegistroInstalaciones.UseVisualStyleBackColor = False
        '
        'BtCerrarsesion
        '
        Me.BtCerrarsesion.BackColor = System.Drawing.Color.Bisque
        Me.BtCerrarsesion.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtCerrarsesion.Location = New System.Drawing.Point(260, 245)
        Me.BtCerrarsesion.Name = "BtCerrarsesion"
        Me.BtCerrarsesion.Size = New System.Drawing.Size(125, 57)
        Me.BtCerrarsesion.TabIndex = 7
        Me.BtCerrarsesion.Text = "Cerrar Sesión"
        Me.BtCerrarsesion.UseVisualStyleBackColor = False
        Me.BtCerrarsesion.Visible = False
        '
        'BtLimpiarIconos
        '
        Me.BtLimpiarIconos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtLimpiarIconos.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtLimpiarIconos.Location = New System.Drawing.Point(260, 74)
        Me.BtLimpiarIconos.Name = "BtLimpiarIconos"
        Me.BtLimpiarIconos.Size = New System.Drawing.Size(125, 57)
        Me.BtLimpiarIconos.TabIndex = 8
        Me.BtLimpiarIconos.Text = "1. LIMPIEZA Escritorio y Vínculos"
        Me.BtLimpiarIconos.UseVisualStyleBackColor = True
        '
        'BtCopiarIconosNR
        '
        Me.BtCopiarIconosNR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtCopiarIconosNR.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtCopiarIconosNR.Location = New System.Drawing.Point(425, 74)
        Me.BtCopiarIconosNR.Name = "BtCopiarIconosNR"
        Me.BtCopiarIconosNR.Size = New System.Drawing.Size(125, 57)
        Me.BtCopiarIconosNR.TabIndex = 9
        Me.BtCopiarIconosNR.Text = "2. COPIADO iconos NR al nuevo Perfil"
        Me.BtCopiarIconosNR.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Lucida Bright", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(257, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(293, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Tras Limpiar Perfil e Iniciar con tu Usuario:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(260, 146)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 15)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Explicación:"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Lucida Bright", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(345, 147)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(205, 88)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Tras Limpiar Perfil desde la cuenta Administrador inicia con tu Usuario del Domin" &
    "io y ejecuta estas dos funciones de tal forma que se reparen los iconos en tu Es" &
    "critorio. No afectará a otro proceso."
        '
        'FormUsuarioAdra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 322)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtCopiarIconosNR)
        Me.Controls.Add(Me.BtLimpiarIconos)
        Me.Controls.Add(Me.BtCerrarsesion)
        Me.Controls.Add(Me.BtRegistroInstalaciones)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LbNotaria)
        Me.Controls.Add(Me.BtLimpiar)
        Me.Controls.Add(Me.LbUsuario)
        Me.Controls.Add(Me.LBoxUsuarios)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormUsuarioAdra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gestión Usuario Adra"
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
    Friend WithEvents BtCerrarsesion As Button
    Friend WithEvents BtLimpiarIconos As Button
    Friend WithEvents BtCopiarIconosNR As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
