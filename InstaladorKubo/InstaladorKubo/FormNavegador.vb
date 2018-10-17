Imports Instalador.LeerFicherosINI
Imports Instalador.FrmInstaladorKubo
Imports System.ComponentModel

Public Class FormNavegador
    Private Sub FrmNavegador_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.WindowState = FormWindowState.Minimized

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


    End Sub

    'TODO arreglar esta chapuza
    Private Sub FormNavegador_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Close()
    End Sub

    Private Sub FormNavegador_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.Close()
    End Sub

    Private Sub FormNavegador_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Me.Close()
    End Sub

    Private Sub FormNavegador_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Me.Close()
    End Sub

    Private Sub FormNavegador_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Me.Close()
    End Sub

    Private Sub FormNavegador_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        Me.Close()
    End Sub

    Private Sub FormNavegador_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Me.Close()
    End Sub
End Class