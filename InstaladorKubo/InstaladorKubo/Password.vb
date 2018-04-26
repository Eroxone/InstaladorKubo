Public Class FormPassword

    'Private Const intentos As Integer = 1




    'Private Sub BtPassword_Click(sender As Object, e As EventArgs) Handles BtPassword.Click
    '    If TbPassword.Text = "b30330104b" Then
    '        Me.Hide()
    '        InstaladorKubo.Show()
    '    Else
    '        'Me.Close()
    '    End If
    'End Sub

    Private Sub TbPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbPassword.KeyPress
        If TbPassword.Text = "b30330104b" Then
            InstaladorKubo.Show()
            Me.Close()
        End If
    End Sub
End Class