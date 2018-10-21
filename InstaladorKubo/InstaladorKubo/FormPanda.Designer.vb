<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPanda
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPanda))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TbPandaNotaria = New System.Windows.Forms.TextBox()
        Me.BtPanda = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Grupo Notaría"
        '
        'TbPandaNotaria
        '
        Me.TbPandaNotaria.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold)
        Me.TbPandaNotaria.Location = New System.Drawing.Point(181, 16)
        Me.TbPandaNotaria.Name = "TbPandaNotaria"
        Me.TbPandaNotaria.Size = New System.Drawing.Size(185, 30)
        Me.TbPandaNotaria.TabIndex = 1
        '
        'BtPanda
        '
        Me.BtPanda.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtPanda.Font = New System.Drawing.Font("Lucida Bright", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtPanda.Image = CType(resources.GetObject("BtPanda.Image"), System.Drawing.Image)
        Me.BtPanda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtPanda.Location = New System.Drawing.Point(181, 63)
        Me.BtPanda.Name = "BtPanda"
        Me.BtPanda.Size = New System.Drawing.Size(185, 48)
        Me.BtPanda.TabIndex = 2
        Me.BtPanda.Text = "Confirmar"
        Me.BtPanda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtPanda.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSalir.Location = New System.Drawing.Point(378, 121)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(70, 35)
        Me.BtSalir.TabIndex = 5
        Me.BtSalir.Text = "Salir"
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'FormPanda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 167)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtPanda)
        Me.Controls.Add(Me.TbPandaNotaria)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormPanda"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Panda End Point"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TbPandaNotaria As TextBox
    Friend WithEvents BtPanda As Button
    Friend WithEvents BtSalir As Button
End Class
