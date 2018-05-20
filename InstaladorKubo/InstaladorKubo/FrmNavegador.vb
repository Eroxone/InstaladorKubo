Imports InstaladorKubo.LeerFicherosINI
Imports InstaladorKubo.InstaladorKubo

Public Class FrmNavegador
    Private Sub FrmNavegador_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim islnombre As String = FrmConfigurarISL.TbISLNombre.Text
        Dim islgrupo As String = FrmConfigurarISL.TbISLGrupo.Text

        If islgrupo = "" Then
            RegistroInstalacion("ISL: Un campo obligatorio no se rellenó o se canceló el proceso.")
            Exit Sub

        ElseIf islnombre = "" Then
            RegistroInstalacion("ISL: Un campo obligatorio no se rellenó o se canceló el proceso.")
            Exit Sub
        End If


        Dim urlnavegador As String = "http://isl.notin.net/users/start/ISLAlwaysOn?cmdline=grant_silent+%22zeJw9jjFuAzEMBEEYcZUU%2bYhBiaJEPiHNpUnpRiKp4ADjUtj%2bRt6cMwK4m91mZgIsP7d1e4FvmwdYPr8%2blp1XP0DBMHZsYrNMSUoVSckzenOUFu5CI7cRpQ8sZCqtJFPzntgGz%2f2o2sXJ5ojedWIiqqKcObr0NjRbREidQaHcImc2ra6UtWVC69E6Wy2GCesvXAHW6%2bW0PXJPW9ze4A5wPj%2f3P73D%2fRGPievuyqyFXuEIf6iePyA%3d%22+%2fSILENT+%2fVERYSILENT+password+%22b30330104%22+description+%22" & islgrupo & "+-+" & islnombre & "%22"

        Navegador.Navigate(New Uri(urlnavegador))


        RegistroInstalacion("ISLAlwaysON: Configurado Servicio ISL con las credenciales: " & islgrupo & " - " & islnombre & ".")
        cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "INSTALACIONES", "ISL", "1")
        InstaladorKubo.BtISL.BackColor = Color.PaleGreen        'TODO ver la forma de cerrar este formulario

    End Sub


End Class