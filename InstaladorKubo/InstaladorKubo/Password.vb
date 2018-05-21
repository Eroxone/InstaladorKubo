Imports InstaladorKubo.LeerFicherosINI



Public Class FormPassword

    Private Sub FormPassword_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim entrada = cIniArray.IniGet(InstaladorKubo.instaladorkuboini, "LOGIN", "ENTRADA", 2)
        If entrada = 1 Then
            InstaladorKubo.Show()
            Me.Close()
        End If
    End Sub


    Private Sub TbPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbPassword.KeyPress
        If TbPassword.Text = "b30330104b" Then
            'cIniArray.IniWrite("C:\TEMP\InstaladorKubo\instaladorkubo.ini", "LOGIN", "ENTRADA", 1)
            InstaladorKubo.Show()
            Me.Close()
        End If
    End Sub

End Class