Imports System.IO

Imports System.Xml

'TODO leer XML nemo - Intro para confirmar - No se admita en blanco
Public Class FrmConfigurarISL

    Private Sub BtConfirmarISL_Click(sender As Object, e As EventArgs) Handles BtConfirmarISL.Click
        If TbISLGrupo.Text = "" Then
            MessageBox.Show("No se admiten campos vacíos", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        ElseIf TbISLNombre.Text = "" Then
            MessageBox.Show("No se admiten campos vacíos", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        Else
            InstaladorKubo.Show()
            Me.Close()
        End If
    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        TbISLGrupo.Text = ""
        TbISLNombre.Text = ""
        InstaladorKubo.Show()
        Me.Close()
    End Sub
End Class