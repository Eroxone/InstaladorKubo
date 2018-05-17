<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConfigurarISL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConfigurarISL))
        Me.TbISLNombre = New System.Windows.Forms.TextBox()
        Me.TbISLGrupo = New System.Windows.Forms.TextBox()
        Me.LbNombreISL = New System.Windows.Forms.Label()
        Me.LbGrupoISL = New System.Windows.Forms.Label()
        Me.BtConfirmarISL = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TbISLNombre
        '
        Me.TbISLNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TbISLNombre.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbISLNombre.Location = New System.Drawing.Point(176, 96)
        Me.TbISLNombre.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TbISLNombre.Name = "TbISLNombre"
        Me.TbISLNombre.Size = New System.Drawing.Size(248, 35)
        Me.TbISLNombre.TabIndex = 1
        '
        'TbISLGrupo
        '
        Me.TbISLGrupo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TbISLGrupo.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbISLGrupo.Location = New System.Drawing.Point(176, 48)
        Me.TbISLGrupo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TbISLGrupo.Name = "TbISLGrupo"
        Me.TbISLGrupo.Size = New System.Drawing.Size(248, 35)
        Me.TbISLGrupo.TabIndex = 0
        '
        'LbNombreISL
        '
        Me.LbNombreISL.AutoSize = True
        Me.LbNombreISL.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbNombreISL.Location = New System.Drawing.Point(41, 100)
        Me.LbNombreISL.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LbNombreISL.Name = "LbNombreISL"
        Me.LbNombreISL.Size = New System.Drawing.Size(111, 27)
        Me.LbNombreISL.TabIndex = 2
        Me.LbNombreISL.Text = "Nombre"
        '
        'LbGrupoISL
        '
        Me.LbGrupoISL.AutoSize = True
        Me.LbGrupoISL.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbGrupoISL.Location = New System.Drawing.Point(41, 52)
        Me.LbGrupoISL.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LbGrupoISL.Name = "LbGrupoISL"
        Me.LbGrupoISL.Size = New System.Drawing.Size(89, 27)
        Me.LbGrupoISL.TabIndex = 3
        Me.LbGrupoISL.Text = "Grupo"
        '
        'BtConfirmarISL
        '
        Me.BtConfirmarISL.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtConfirmarISL.Font = New System.Drawing.Font("Lucida Bright", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.BtConfirmarISL.Location = New System.Drawing.Point(176, 159)
        Me.BtConfirmarISL.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtConfirmarISL.Name = "BtConfirmarISL"
        Me.BtConfirmarISL.Size = New System.Drawing.Size(188, 65)
        Me.BtConfirmarISL.TabIndex = 3
        Me.BtConfirmarISL.Text = "Confirmar"
        Me.BtConfirmarISL.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSalir.Location = New System.Drawing.Point(453, 218)
        Me.BtSalir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(100, 42)
        Me.BtSalir.TabIndex = 4
        Me.BtSalir.Text = "Salir"
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'FrmConfigurarISL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(569, 274)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtConfirmarISL)
        Me.Controls.Add(Me.LbGrupoISL)
        Me.Controls.Add(Me.LbNombreISL)
        Me.Controls.Add(Me.TbISLGrupo)
        Me.Controls.Add(Me.TbISLNombre)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmConfigurarISL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configurar ISL AlwaysON"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LbNombreISL As Label
    Friend WithEvents LbGrupoISL As Label
    Public WithEvents TbISLNombre As TextBox
    Public WithEvents TbISLGrupo As TextBox
    Friend WithEvents BtConfirmarISL As Button
    Friend WithEvents BtSalir As Button
End Class
