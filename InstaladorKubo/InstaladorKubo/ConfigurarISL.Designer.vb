<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigurarISL
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
        Me.TbISLNombre = New System.Windows.Forms.TextBox()
        Me.TbISLGrupo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TbISLNombre
        '
        Me.TbISLNombre.Location = New System.Drawing.Point(167, 35)
        Me.TbISLNombre.Name = "TbISLNombre"
        Me.TbISLNombre.Size = New System.Drawing.Size(187, 20)
        Me.TbISLNombre.TabIndex = 0
        '
        'TbISLGrupo
        '
        Me.TbISLGrupo.Location = New System.Drawing.Point(167, 96)
        Me.TbISLGrupo.Name = "TbISLGrupo"
        Me.TbISLGrupo.Size = New System.Drawing.Size(187, 20)
        Me.TbISLGrupo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Nombre"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Grupo"
        '
        'ConfigurarISL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 277)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TbISLGrupo)
        Me.Controls.Add(Me.TbISLNombre)
        Me.Name = "ConfigurarISL"
        Me.Text = "ConfigurarISL"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TbISLNombre As TextBox
    Friend WithEvents TbISLGrupo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
