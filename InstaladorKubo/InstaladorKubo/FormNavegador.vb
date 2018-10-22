Imports Instalador.LeerFicherosINI
Imports Instalador.FrmInstaladorKubo


Public Class FormNavegador

    Public Sub IniciarNavegador(ByVal servicio As String)

        If servicio = "isl" Then
            LBNavegador.Text = "=ISL Light Notin= Mueve el Cursor para cerrar."
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


            'FrmInstaladorKubo.PbInstalaciones.Value = 0
            'FrmInstaladorKubo.PbInstalaciones.Visible = True

            'Dim tiempoespera As Integer
            'Dim pasosbarra As Integer = 16
            'While tiempoespera < 3
            '    FrmInstaladorKubo.PbInstalaciones.Step = pasosbarra
            '    FrmInstaladorKubo.PbInstalaciones.PerformStep()
            '    Threading.Thread.Sleep(1000)
            '    tiempoespera = tiempoespera + 1
            '    pasosbarra = pasosbarra + 1
            'End While

            'FrmInstaladorKubo.PbInstalaciones.Visible = False
            'FrmInstaladorKubo.PbInstalaciones.Value = 0


            'SendKeys.Send()


            RegistroInstalacion("ISLAlwaysON: Configurado Servicio ISL con las credenciales: " & islgrupo & " - " & islnombre & ".")
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "INSTALACIONES", "ISL", "1")
            FrmInstaladorKubo.BtISL.BackColor = Color.PaleGreen


        ElseIf servicio = "panda" Then
            LBNavegador.Text = "=Panda End Point= Mueve el Cursor para cerrar."

            Dim gruponotaria As String = FormPanda.TbPandaNotaria.Text

            If gruponotaria = "" Then
                RegistroInstalacion("PANDA: Un campo obligatorio no se rellenó o se canceló el proceso.")
                Exit Sub
            End If
            Dim urlnavegador As String = "https://managedprotection.pandasecurity.com/PartnerConsole/cv18/Customers/Administration/Install/Installer/GetAgent.aspx?CUST=cGVnQlFKSWkyVmM3VE85M1Bsc3Badz09&OS=Windows&GROUP=" & gruponotaria

            Navegador.Navigate(New Uri(urlnavegador))

            RegistroInstalacion("PandEndPiont: Configurado Servicio Panda con las credenciales: " & gruponotaria)
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "INSTALACIONES", "PANDA", "1")
            FrmInstaladorKubo.BtPanda.BackColor = Color.PaleGreen

        End If
    End Sub

End Class