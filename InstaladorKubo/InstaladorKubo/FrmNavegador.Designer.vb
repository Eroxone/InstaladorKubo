<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNavegador
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNavegador))
        Me.Navegador = New System.Windows.Forms.WebBrowser()
        Me.LBNavegador = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Navegador
        '
        Me.Navegador.AllowNavigation = False
        Me.Navegador.AllowWebBrowserDrop = False
        Me.Navegador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Navegador.Location = New System.Drawing.Point(0, 0)
        Me.Navegador.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Navegador.MinimumSize = New System.Drawing.Size(15, 16)
        Me.Navegador.Name = "Navegador"
        Me.Navegador.ScrollBarsEnabled = False
        Me.Navegador.Size = New System.Drawing.Size(476, 94)
        Me.Navegador.TabIndex = 0
        Me.Navegador.WebBrowserShortcutsEnabled = False
        '
        'LBNavegador
        '
        Me.LBNavegador.AutoSize = True
        Me.LBNavegador.Font = New System.Drawing.Font("Lucida Bright", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.LBNavegador.Location = New System.Drawing.Point(20, 33)
        Me.LBNavegador.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LBNavegador.Name = "LBNavegador"
        Me.LBNavegador.Size = New System.Drawing.Size(437, 22)
        Me.LBNavegador.TabIndex = 1
        Me.LBNavegador.Text = "Cierra esta ventana al completar la descarga"
        '
        'FrmNavegador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 94)
        Me.Controls.Add(Me.LBNavegador)
        Me.Controls.Add(Me.Navegador)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmNavegador"
        Me.Opacity = 0.9R
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Navegador"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Navegador As WebBrowser
    Friend WithEvents LBNavegador As Label
End Class
