Imports System.IO
Imports InstaladorKubo.LeerFicherosINI
Imports InstaladorKubo.FrmInstaladorKubo

Public Class Chocolatey
    Private Const PuestoNotin As String = "ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/"


    Public Shared Sub ObtenerChocolatey()
        Dim instaladochocolatey = cIniArray.IniGet(instaladorkuboini, "CHOCOLATEY", "INSTALADO", "0")

        If instaladochocolatey = 0 Then
            Dim rutadescargas = cIniArray.IniGet(instaladorkuboini, "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")
            Directory.CreateDirectory(rutadescargas & "Chocolatey")
            'Dim instalarchocolatey As String = "@"%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command "iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))" && SET "PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin"
            'File.WriteAllText(rutadescargas & "Chocolatey\instalacion.bat", instalarchocolatey)

            Try
                My.Computer.Network.DownloadFile(PuestoNotin & "InstalacionChocolatey.bat", rutadescargas & "Chocolatey\InstalacionChocolatey.bat", "juanjo", "Palomeras24", False, 20000, True)
                RegistroInstalacion("ÉXITO: Obtenido instalador para Chocolatey")
                FrmInstaladorKubo.RunAsAdmin(rutadescargas & "Chocolatey\InstalacionChocolatey.bat")
                cIniArray.IniWrite(instaladorkuboini, "CHOCOLATEY", "INSTALADO", "1")
                InstaladorKubo.FrmInstaladorKubo.BtChocolatey.BackColor = Color.PaleGreen
                InstaladorKubo.FrmInstaladorKubo.BtLogChoco.Visible = True
            Catch ex As Exception
                MessageBox.Show("Error instalando Chocolatey. Revisa Log para mas detalles.", "Paquete Chocolatey", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR obteniendo instalador para Chocolatey: " & ex.Message)
                InstaladorKubo.FrmInstaladorKubo.BtChocolatey.BackColor = Color.LightSalmon
            End Try

        End If
    End Sub

End Class
