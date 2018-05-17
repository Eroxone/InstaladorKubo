Imports System.IO

Imports System.Xml


Public Class FrmConfigurarISL

    Private Sub FrmConfigurarISL_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    'TODO leer xml de Nemo
    'TODO que con intro puedas saltar de una a otra
    'Private Sub LbGrupoISL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LbGrupoISL.KeyPress
    '    If e.KeyChar = ChrW(Keys.Enter) Then
    '        e.Handled = True
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    'Private Sub LbNombreISL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LbNombreISL.KeyPress
    '    If e.KeyChar = ChrW(Keys.Enter) Then
    '        e.Handled = True
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    Private Sub BtConfirmarISL_Click(sender As Object, e As EventArgs) Handles BtConfirmarISL.Click
        InstaladorKubo.Show()
        Me.Close()
    End Sub
End Class