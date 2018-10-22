Imports Instalador.FrmInstaladorKubo
Imports System.IO

Public Class LimpiezaInicio
    Public Shared Sub LimpiezaInicio()
        'LIMPIEZA DE FICHEROS AL INICIO

        'Quitar Icono versión anterior
        Dim Escritorio As String = "" & My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & ""
        Try
            File.Delete(Escritorio & "InstaladorKubo.appref-ms")
        Catch ex As Exception
            RegistroInstalacion("INFO Limpieza Inicio anterior InstaladorKubo: " & ex.Message)
        End Try

        Try
            'Fuerzo que se descarguen la nueva version de WGET (quitar en unas semanas)
            Dim wgetexe As New FileInfo(FrmInstaladorKubo.RutaDescargas & "wget.exe")
            Dim Lengthwget As Long = wgetexe.Length
            If wgetexe.Length > "3895184" Then
                File.Delete(FrmInstaladorKubo.RutaDescargas & "wget.exe")
            End If
        Catch ex As Exception
            RegistroInstalacion("INFO Limpieza Inicio Wget: " & ex.Message)
        End Try

        'Cuando hay cambio de Perfil en ADRA se duplica el icono del Instalador
        Try
            File.Delete(Escritorio & "Instalador - 1 ")
            File.Delete(Escritorio & "Instalador - 1")
        Catch ex As Exception
            RegistroInstalacion("INFO Limpieza Inicio Instalador -1: " & ex.Message)
        End Try

        'Archivos TXT de las Descargas
        Try
            File.Delete(FrmInstaladorKubo.RutaDescargas & "descargas.txt")
            File.Delete(FrmInstaladorKubo.RutaDescargas & "registro.txt")
            File.Delete(FrmInstaladorKubo.RutaDescargas & "requisitos.txt")
            File.Delete(FrmInstaladorKubo.RutaDescargas & "terceros.txt")
        Catch ex As Exception
            RegistroInstalacion("INFO Limpieza Inicio TXT: " & ex.Message)
        End Try

        'Se descargará con cada instalación en la primera fase de FT
        Try
            File.Delete(FrmInstaladorKubo.RutaDescargas & "SmartScreen.reg")
        Catch ex As Exception
            RegistroInstalacion("INFO Limpieza Inicio SmartScreen REG: " & ex.Message)
        End Try
    End Sub
End Class
