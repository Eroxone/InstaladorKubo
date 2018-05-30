<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SQLInstalacion
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
        Me.BtDescargarSQL = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtDescargarSQL
        '
        Me.BtDescargarSQL.Font = New System.Drawing.Font("Lucida Bright", 14.25!, System.Drawing.FontStyle.Bold)
        Me.BtDescargarSQL.Location = New System.Drawing.Point(12, 12)
        Me.BtDescargarSQL.Name = "BtDescargarSQL"
        Me.BtDescargarSQL.Size = New System.Drawing.Size(126, 65)
        Me.BtDescargarSQL.TabIndex = 0
        Me.BtDescargarSQL.Text = "Descargar SQL 2014"
        Me.BtDescargarSQL.UseVisualStyleBackColor = True
        '
        'SQLInstalacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 292)
        Me.Controls.Add(Me.BtDescargarSQL)
        Me.Name = "SQLInstalacion"
        Me.Text = "SQLInstalacion"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtDescargarSQL As Button
End Class
