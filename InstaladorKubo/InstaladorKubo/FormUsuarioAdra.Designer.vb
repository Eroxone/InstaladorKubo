﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.LBoxUsuarios.ItemHeight = 22
        Me.LBoxUsuarios.Location = New System.Drawing.Point(33, 52)
        Me.LBoxUsuarios.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LBoxUsuarios.Name = "LBoxUsuarios"
        Me.LBoxUsuarios.Size = New System.Drawing.Size(279, 92)
        Me.LBoxUsuarios.TabIndex = 0
        '
        'LbUsuario
        '
        Me.LbUsuario.AutoSize = True
        Me.LbUsuario.BackColor = System.Drawing.SystemColors.Control
        Me.LbUsuario.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LbUsuario.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.LbUsuario.Location = New System.Drawing.Point(151, 176)
        Me.LbUsuario.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LbUsuario.Name = "LbUsuario"
        Me.LbUsuario.Size = New System.Drawing.Size(136, 23)
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
        Me.BtLimpiar.Location = New System.Drawing.Point(33, 299)
        Me.BtLimpiar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtLimpiar.Name = "BtLimpiar"
        Me.BtLimpiar.Size = New System.Drawing.Size(221, 70)
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
        Me.LbNotaria.Location = New System.Drawing.Point(29, 176)
        Me.LbNotaria.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LbNotaria.Name = "LbNotaria"
        Me.LbNotaria.Size = New System.Drawing.Size(117, 23)
        Me.LbNotaria.TabIndex = 3
        Me.LbNotaria.Text = "NOTARIA\"
        Me.LbNotaria.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Lucida Bright", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(382, 23)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Selecciona usuario AdRA del Equipo:"
        '
        'BtSalir
        '
        Me.BtSalir.Font = New System.Drawing.Font("Lucida Bright", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSalir.Location = New System.Drawing.Point(625, 326)
        Me.BtSalir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(108, 46)
        Me.BtSalir.TabIndex = 5
        Me.BtSalir.Text = "Salir"
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'BtRegistroInstalaciones
        '
        Me.BtRegistroInstalaciones.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.BtRegistroInstalaciones.Font = New System.Drawing.Font("Lucida Bright", 10.2!)
        Me.BtRegistroInstalaciones.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtRegistroInstalaciones.Location = New System.Drawing.Point(33, 247)
        Me.BtRegistroInstalaciones.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtRegistroInstalaciones.Name = "BtRegistroInstalaciones"
        Me.BtRegistroInstalaciones.Size = New System.Drawing.Size(172, 34)
        Me.BtRegistroInstalaciones.TabIndex = 6
        Me.BtRegistroInstalaciones.Text = "Visualizar Log"
        Me.BtRegistroInstalaciones.UseVisualStyleBackColor = False
        '
        'BtCerrarsesion
        '
        Me.BtCerrarsesion.BackColor = System.Drawing.Color.Bisque
        Me.BtCerrarsesion.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtCerrarsesion.Location = New System.Drawing.Point(347, 302)
        Me.BtCerrarsesion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtCerrarsesion.Name = "BtCerrarsesion"
        Me.BtCerrarsesion.Size = New System.Drawing.Size(167, 70)
        Me.BtCerrarsesion.TabIndex = 7
        Me.BtCerrarsesion.Text = "Cerrar Sesión"
        Me.BtCerrarsesion.UseVisualStyleBackColor = False
        Me.BtCerrarsesion.Visible = False
        '
        'BtLimpiarIconos
        '
        Me.BtLimpiarIconos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtLimpiarIconos.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtLimpiarIconos.Location = New System.Drawing.Point(347, 91)
        Me.BtLimpiarIconos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtLimpiarIconos.Name = "BtLimpiarIconos"
        Me.BtLimpiarIconos.Size = New System.Drawing.Size(167, 70)
        Me.BtLimpiarIconos.TabIndex = 8
        Me.BtLimpiarIconos.Text = "1. LIMPIEZA Escritorio y Vínculos"
        Me.BtLimpiarIconos.UseVisualStyleBackColor = True
        '
        'BtCopiarIconosNR
        '
        Me.BtCopiarIconosNR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtCopiarIconosNR.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtCopiarIconosNR.Location = New System.Drawing.Point(567, 91)
        Me.BtCopiarIconosNR.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtCopiarIconosNR.Name = "BtCopiarIconosNR"
        Me.BtCopiarIconosNR.Size = New System.Drawing.Size(167, 70)
        Me.BtCopiarIconosNR.TabIndex = 9
        Me.BtCopiarIconosNR.Text = "2. COPIADO iconos NR al nuevo Perfil"
        Me.BtCopiarIconosNR.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Lucida Bright", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(343, 55)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(389, 19)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Tras Limpiar Perfil e Iniciar con tu Usuario:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Lucida Bright", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(343, 179)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 20)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Explicación:"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Lucida Bright", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(466, 181)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(268, 100)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = resources.GetString("Label4.Text")
        '
        'FormUsuarioAdra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 396)
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
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
