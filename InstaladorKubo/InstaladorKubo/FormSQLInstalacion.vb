Imports System.IO
Imports Instalador.FrmInstaladorKubo
Imports Instalador.LeerFicherosINI



Public Class FrmSQLInstalacion

    Private Sub BtDescargarSQL_Click(sender As Object, e As EventArgs) Handles BtDescargarSQL.Click
        TlpInstalaSQL.ToolTipTitle = "Descarga e Instala SQL 2014 Business"
        TlpInstalaSQL.SetToolTip(BtDescargarSQL, "Tras la Descarga se procederá a realizar la instalación desatendida del Motor SQL 2014.")

        Dim instancia = TbInstancia.Text.ToString
        cIniArray.IniWrite(instaladorkuboini, "SQL2014", "INSTANCIA", instancia)

        Dim rutabdusuario = TbBDUsuario.Text.ToString
        cIniArray.IniWrite(instaladorkuboini, "SQL2014", "BDUSUARIO", rutabdusuario)
        cIniArray.IniWrite(instaladorkuboini, "SQL2014", "RUTABACKUP", rutabdusuario & "\Backup")
        cIniArray.IniWrite(instaladorkuboini, "SQL2014", "INSTANCIA", instancia)

        Dim rutadescargas = cIniArray.IniGet(instaladorkuboini, "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")
        ObtenerEjecutables.obtenerwget()
        ObtenerEjecutables.obtenerunrar()

        Directory.CreateDirectory(rutadescargas & "SQL")
        Dim wgetserialsql As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/SerialsSQL2014.txt -O " & rutadescargas & "SQL\SerialsSQL2014.txt"
        Dim wgetsql As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/SQLServer2014.exe -O " & rutadescargas & "SQL\SQLServer2014.exe"
        Shell("cmd /c " & rutadescargas & wgetserialsql, AppWinStyle.Hide, True)
        Shell("cmd /c " & rutadescargas & wgetsql, AppWinStyle.NormalFocus, True)
        RegistroInstalacion("SQL2014: Terminada descarga del Paquete en RAR. Se procede a descomprimir la imagen.")

        Shell("cmd.exe /c " & rutadescargas & "unrar.exe x -u -y " & rutadescargas & "SQL\SQLServer2014.exe " & rutadescargas & "SQL\", AppWinStyle.NormalFocus, True)
        Dim parametrossql As String = "/IAcceptSQLServerLicenseTerms=True /Action=Install /ENU=False /QUIETSIMPLE=True /UpdateEnabled=False /ERRORREPORTING=False /USEMICROSOFTUPDATE=False /FEATURES=SQLENGINE,SSMS,ADV_SSMS /UpdateSource=MU /HELP=False /INDICATEPROGRESS=True /X86=False" & " /INSTALLSHAREDDIR=" & """" & "C:\Program Files\Microsoft SQL Server" & """" & " /INSTALLSHAREDWOWDIR=" & """" & "C:\Program Files (x86)\Microsoft SQL" & """" & " /INSTANCENAME=" & """" & instancia & """" & " /SQMREPORTING=False /INSTANCEID=" & """" & instancia & """" & " /INSTANCEDIR=" & """" & "C:\Program Files\Microsoft SQL Server" & """" & " /AGTSVCSTARTUPTYPE=Manual /COMMFABRICPORT=0 /COMMFABRICNETWORKLEVEL=0 /COMMFABRICENCRYPTION=0 /MATRIXCMBRICKCOMMPORT=0 /SQLSVCSTARTUPTYPE=Automatic /FILESTREAMLEVEL=1 /ENABLERANU=False /SQLCOLLATION=Modern_Spanish_CI_AS /SECURITYMODE=SQL /SAPWD=03071997" & " /SQLBACKUPDIR=" & """" & rutabdusuario & "\Backup" & """" & " /SQLUSERDBDIR=" & """" & rutabdusuario & """" & " /TCPENABLED=1 /NPENABLED=1 /BROWSERSVCSTARTUPTYPE=Automatic /SQLSYSADMINACCOUNTS=Administrador"

        Try
            Directory.CreateDirectory(rutabdusuario)
            Directory.CreateDirectory(rutabdusuario & "\Backup")
        Catch ex As Exception
            RegistroInstalacion("SQL ADVERTENCIA: No se crearon las rutas BD Usuario. Motivo: " & ex.Message)
        End Try

        Try
            Process.Start(rutadescargas & "SQL\SQLServer2014\Setup.exe", parametrossql)
            RegistroInstalacion("SQL2014: Ejecutado proceso de instalación en hilo independiente.")
        Catch ex As Exception
            MessageBox.Show("Error de Setup: " & ex.Message)
            RegistroInstalacion("SQL2014: Error al ejecutar el Setup: " & ex.Message)
        End Try

        cIniArray.IniWrite(instaladorkuboini, "SQL2014", "INSTALADO", "1")
        MessageBox.Show("Lanzada instalación desatendida de SQL2014 x64. Revisa Log para ver los detalles.", "Fin instalación desatendida SQL", MessageBoxButtons.OK, MessageBoxIcon.Information)
        FrmInstaladorKubo.Show()
        Me.Close()
    End Sub

    Private Sub TbBDUsuario_TextChanged(sender As Object, e As EventArgs) Handles TbBDUsuario.TextChanged
        TbBackup.Text = TbBDUsuario.Text & "\Backup"
    End Sub

    Private Sub FrmSQLInstalacion_Load(sender As Object, e As EventArgs) Handles Me.Load
        FrmInstaladorKubo.Hide()

        TbBDUsuario.Text = cIniArray.IniGet(instaladorkuboini, "SQL2014", "BDUSUARIO", "G:\RESPALDO\F\NOTAWIN.NET")
        TbInstancia.Text = cIniArray.IniGet(instaladorkuboini, "SQL2014", "INSTANCIA", "MSSQLSERVER")

        Dim estadospsql = cIniArray.IniGet(instaladorkuboini, "SQL", "SP2", "2")
        If estadospsql = 1 Then
            BtSPSQL.BackColor = Color.PaleGreen
        ElseIf estadospsql = 0 Then
            BtSPSQL.BackColor = Color.LightSalmon
        End If

    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        FrmInstaladorKubo.Show()
        Me.Close()
    End Sub

    Private Sub FrmSQLInstalacion_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FrmInstaladorKubo.Show()
    End Sub

    Private Sub BtLogs_Click(sender As Object, e As EventArgs) Handles BtLogs.Click
        Process.Start("explorer.exe", "C:\Program Files\Microsoft SQL Server\120\Setup Bootstrap\Log")
    End Sub

    Private Sub SPSQL_Click(sender As Object, e As EventArgs) Handles BtSPSQL.Click
        Dim rutadescargas = cIniArray.IniGet(instaladorkuboini, "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")
        Try
            Directory.CreateDirectory(rutadescargas & "SQL")
            Shell("cmd.exe /c" & rutadescargas & "wget.exe -c -q --show-progress https://download.microsoft.com/download/1/7/6/17672C60-A610-414F-81E4-F50B71C1161A/SQLServer2014SP2-KB3171021-x64-ESN.exe -O " & rutadescargas & "SQL\SQLServer2014SP2-KB3171021-x64-ESN.exe", AppWinStyle.NormalFocus, True)
        Catch ex As Exception
            RegistroInstalacion("ERROR descargando SP2 para SQL2014: " & ex.Message)
        End Try

        Try
            Dim pspsql As New ProcessStartInfo With {
            .FileName = rutadescargas & "SQL\SQLServer2014SP2-KB3171021-x64-ESN.exe"
        }
            Dim spsql As Process = Process.Start(pspsql)
            spsql.WaitForExit()
            RegistroInstalacion("PARCHE SP2 para SQL 2014 lanzado correctamente.")
            cIniArray.IniWrite(instaladorkuboini, "SQL", "SP2", "1")
            BtSPSQL.BackColor = Color.PaleGreen
        Catch ex As Exception
            RegistroInstalacion("ERROR Parche SP2 SQL2014: " & ex.Message)
            cIniArray.IniWrite(instaladorkuboini, "SQL", "SP2", "0")
            BtSPSQL.BackColor = Color.LightSalmon
        End Try
    End Sub
End Class