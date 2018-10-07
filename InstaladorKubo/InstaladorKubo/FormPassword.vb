Imports Instalador.LeerFicherosINI
Imports System.IO
Imports Instalador.FrmInstaladorKubo

Public Class FormPassword

    Private Sub FormPassword_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Limpiar Icono versión anterior
        Try
            Dim Escritorio As String = "" & My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & ""
            File.Delete(Escritorio & "InstaladorKubo.appref-ms")
        Catch ex As Exception
            RegistroInstalacion("INFO Limpiar InstaladorKubo: " & ex.Message)
        End Try

        Dim entrada = cIniArray.IniGet(FrmInstaladorKubo.instaladorkuboini, "LOGIN", "ENTRADA", 2)
        If entrada = 1 Then
            FrmInstaladorKubo.Show()
            Me.Close()
        End If
    End Sub



    Private Sub TbPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbPassword.KeyPress
        If TbPassword.Text = "b30330104b" Then
            TbPassword.BackColor = Color.PaleGreen
            'cIniArray.IniWrite("C:\TEMP\InstaladorKubo\instaladorkubo.ini", "LOGIN", "ENTRADA", 1)
            FrmInstaladorKubo.Show()
            Me.Close()
        End If
    End Sub


End Class