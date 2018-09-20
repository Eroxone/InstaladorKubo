<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPassword))
        Me.TbPassword = New System.Windows.Forms.TextBox()
        Me.LbPassword = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TbPassword
        '
        Me.TbPassword.Font = New System.Drawing.Font("Lucida Bright", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbPassword.ForeColor = System.Drawing.Color.Blue
        Me.TbPassword.Location = New System.Drawing.Point(19, 54)
        Me.TbPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.TbPassword.Name = "TbPassword"
        Me.TbPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TbPassword.Size = New System.Drawing.Size(175, 36)
        Me.TbPassword.TabIndex = 0
        '
        'LbPassword
        '
        Me.LbPassword.AutoSize = True
        Me.LbPassword.Font = New System.Drawing.Font("Lucida Bright", 18.0!, System.Drawing.FontStyle.Bold)
        Me.LbPassword.Location = New System.Drawing.Point(26, 15)
        Me.LbPassword.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LbPassword.Name = "LbPassword"
        Me.LbPassword.Size = New System.Drawing.Size(154, 27)
        Me.LbPassword.TabIndex = 1
        Me.LbPassword.Text = "Contraseña:"
        '
        'FormPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(211, 102)
        Me.Controls.Add(Me.LbPassword)
        Me.Controls.Add(Me.TbPassword)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormPassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control Acceso"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TbPassword As TextBox
    Friend WithEvents LbPassword As Label
End Class
