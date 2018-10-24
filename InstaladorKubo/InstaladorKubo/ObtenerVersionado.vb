Imports Instalador.FormInstaladorKubo
Imports System.IO
Imports Instalador.LeerFicherosINI
Imports System.Environment

Public Class ObtenerVersionado

    Public Shared Sub ObtenerVersionNotin()
        Dim criterio As String
        Dim filename As String

        Dim contador As Integer = 1
        Dim Linea_Actual As Integer = 0

        ' Obtener el resultado
        Dim vlcResultado As String = ""
        ' Especifica el criterio
        criterio = "ACCESS=ON"
        Dim vlcCriterio As String = criterio
        ' Nombre del archivo a utilizar
        filename = "C:\Notawin.Net\Notin.log"
        Dim vlcFileName As String = filename
        ' Si el archivo existe
        If IO.File.Exists(vlcFileName) Then
            ' Crear el stream
            Dim vloFile As New StreamReader(vlcFileName)
            ' Ciclo de iteración entre líneas
            While True
                ' Cargar la línea
                Dim vlcLinea = vloFile.ReadLine()
                ' Si la línea es nula, sale del ciclo
                If vlcLinea Is Nothing Then Exit While
                ' Si la línea contiene el criterio
                If vlcLinea.Contains(vlcCriterio) Then
                    ' Obtener la línea como resultado,
                    ' Concatenando un retorno de carro si ya hay ocurrencias
                    vlcResultado += IIf(vlcResultado <> "", vbCrLf, "") & vlcLinea
                    Linea_Actual = contador
                    ' Salir del ciclo
                    'Exit While ' Lo omites si quieres todas las ocurrencias
                End If
                contador += 1

            End While
        End If
        ' Si no se encontraron resultados
        If vlcResultado = "" Then
            vlcResultado = "Sin información"
            FormInstaladorKubo.LbVersionNotin.Text = vlcResultado
            Exit Sub
        End If

        'MsgBox("El resultado de La busqueda es: " + vlcResultado.ToString + " La linea en la que lo encontro es: " + Linea_Actual.ToString)

        Try
            Dim numeroversion = vlcResultado.LastIndexOf(":")
            vlcResultado = vlcResultado.Substring(numeroversion + 2)
            FormInstaladorKubo.LbVersionNotin.Text = vlcResultado
            cIniArray.IniWrite(instaladorkuboini, "NOTIN", "VERSION", vlcResultado)
            RegistroInstalacion("NOTIN: InfoVersión en el Sistema: " & vlcResultado)
        Catch ex As Exception
            'RegistroInstalacion("NOTIN: No se pudo determinar Versión NOTIN en Sistema.")
        End Try
    End Sub


    Public Shared Sub ObtenerVersionNet()
        Try
            Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
            Dim infoinstaller As String = appData & "\Notin\InfoInstaller.txt"
            Dim infoversion As String

            Dim sr As New System.IO.StreamReader(infoinstaller)
            infoversion = sr.ReadLine()
            'infoversion = sr.ReadToEnd
            sr.Close()
            Dim numeroversion = infoversion.LastIndexOf(",")
            infoversion = infoversion.Substring(0, numeroversion)
            FormInstaladorKubo.LbBetaNet.Text = infoversion
            cIniArray.IniWrite(instaladorkuboini, "NET", "NETSISTEMA", infoversion)
            'cIniArray.IniWrite(instaladorkuboini, "NET", "FECHAEJECUCION", DateTime.Now)
            RegistroInstalacion("NOTIN .NET: InfoVersión en el Sistema: " & infoversion)
        Catch ex As Exception
            RegistroInstalacion("NOTIN .NET: No se pudo determinar Versión .Net en Sistema.")
        End Try
    End Sub

    Public Shared Sub ObtenerVersionMigrador()
        Try
            Dim migradorok = cIniArray.IniGet("F:\WINDOWS\NNOTIN.INI", "VARIABLES", "VersionMigradorNotinSQL", "0")
            If migradorok <> 0 Then
                FormInstaladorKubo.LbMigradorINI.Text = "MigradorSQL " & migradorok
            End If
        Catch ex As Exception
            RegistroInstalacion("No se pudo obtener versión de MigradorSQL. " & ex.Message)
        End Try
    End Sub

    Public Shared Sub ObtenerVersionFW()
        Directory.CreateDirectory(FormInstaladorKubo.RutaDescargas)

        Try
            Shell("cmd /c reg Query " & """" & "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Client" & """" & " /v Version > " & FormInstaladorKubo.RutaDescargas & "fwversion.txt", AppWinStyle.Hide, True)
        Catch ex As Exception
            RegistroInstalacion("ERROR Obtener versión FW: " & ex.Message)
        End Try

        Try
            Dim versionfw As String = FormInstaladorKubo.RutaDescargas & "fwversion.txt"
            Dim infoversion As String = "Sin información."

            Dim sr As New System.IO.StreamReader(versionfw)
            Dim numerolinea As Integer = 0
            'TODO Sandraaaaaaaa ayudame con esto. While seguro es cutre jaja
            While numerolinea < 3
                infoversion = sr.ReadLine()
                numerolinea = numerolinea + 1
            End While
            sr.Close()

            FormInstaladorKubo.TlpVersionFW.ToolTipTitle = "REG Query para Versión Framework:"
            FormInstaladorKubo.TlpVersionFW.SetToolTip(FormInstaladorKubo.BtVersionFW, infoversion)

            Dim numeroversion As String = infoversion.Substring(25)

            FormInstaladorKubo.LbVersionFW.Text = "Versión " & numeroversion

            cIniArray.IniWrite(instaladorkuboini, "NET", "FWSISTEMA", numeroversion)
            RegistroInstalacion("INFO FRAMEWORK: InfoVersión en el Sistema: " & numeroversion)
            File.Delete(FormInstaladorKubo.RutaDescargas & "fwversion.txt")
        Catch ex As Exception
            RegistroInstalacion("INFO FRAMEWORK: No se pudo determinar Versión FW en Sistema.")
        End Try
    End Sub

End Class
