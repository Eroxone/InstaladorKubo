Imports System.IO
Imports InstaladorKubo.FrmInstaladorKubo
Imports InstaladorKubo.LeerFicherosINI




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
        Dim wgetsql As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/SQLServer2014.iso -O " & rutadescargas & "SQL\SQLServer2014.exe"
        Shell("cmd /c " & rutadescargas & wgetserialsql, AppWinStyle.Hide, True)
        Shell("cmd /c " & rutadescargas & wgetsql, AppWinStyle.NormalFocus, True)
        RegistroInstalacion("SQL2014: Terminada descarga de la ISO. Se procede a descomprimir la imagen.")

        Shell("cmd.exe /c " & rutadescargas & "unrar.exe x -u -y " & rutadescargas & "SQL\SQLServer2014.exe " & rutadescargas & "SQL\", AppWinStyle.NormalFocus, True)
        'TODO convertir la ISO en un RAR para poder trabajar con el unrar
        Dim parametrossql As String = "/IAcceptSQLServerLicenseTerms=True /Action=Install /ENU=False /QUIETSIMPLE=True /UpdateEnabled=True /ERRORREPORTING=False /USEMICROSOFTUPDATE=False /FEATURES=SQLENGINE, SSMS, ADV_SSMS /UpdateSource=MU /HELP=False /INDICATEPROGRESS=True /X86=False " & "/INSTALLSHAREDDIR=" & """" & "C:\Program Files\Microsoft SQL Server" & """" & " /INSTALLSHAREDWOWDIR=" & """" & "C:\Program Files (x86)\Microsoft SQL" & """" & " /INSTANCENAME=" & """" & instancia & """" & " /SQMREPORTING=False /INSTANCEID=" & """" & instancia & """" & "  /INSTANCEDIR=" & """" & "C:\Program Files\Microsoft SQL Server" & """" & " /AGTSVCACCOUNT=" & """" & "NT Service\SQLSERVERAGENT" & """" & " /AGTSVCSTARTUPTYPE=Manual /COMMFABRICPORT=0 /COMMFABRICNETWORKLEVEL=0 /COMMFABRICENCRYPTION=0 /MATRIXCMBRICKCOMMPORT=0 /SQLSVCSTARTUPTYPE=Automatic /FILESTREAMLEVEL=1 /ENABLERANU=False /SQLCOLLATION=Modern_Spanish_CI_AS /SQLSVCACCOUNT=" & """" & "NT Service\MSSQLSERVER" & """" & " /SECURITYMODE=SQL /SAPWD=03071997" & " /SQLBACKUPDIR=" & """" & rutabdusuario & "\Backup" & """" & " /SQLUSERDBDIR=" & """" & rutabdusuario & """" & " /TCPENABLED=1 /NPENABLED=1 /BROWSERSVCSTARTUPTYPE=Automatic /SQLSYSADMINACCOUNTS=Administrador"

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
    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        FrmInstaladorKubo.Show()
        Me.Close()
    End Sub

    Private Sub FrmSQLInstalacion_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FrmInstaladorKubo.Show()
    End Sub
End Class