Imports System.IO
Imports InstaladorKubo.InstaladorKubo
Imports InstaladorKubo.LeerFicherosINI




Public Class SQLInstalacion

    'TODO obtener Wget y Obtener Unrar como clase fuera para poder usarlo en mas funciones.


    Private Sub BtDescargarSQL_Click(sender As Object, e As EventArgs) Handles BtDescargarSQL.Click
        Dim rutadescargas = cIniArray.IniGet(instaladorkuboini, "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")

        Directory.CreateDirectory(rutadescargas & "SQL")
        Dim wgetserialsql As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/SerialsSQL2014.txt -O " & rutadescargas & "SQL\SerialsSQL2014.txt"
        Dim wgetsql As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/SQLServer2014.iso -O " & rutadescargas & "SQL\SQLServer2014.iso"
        Shell("cmd /c " & rutadescargas & wgetserialsql, AppWinStyle.Hide, True)
        Shell("cmd /c " & rutadescargas & wgetsql, AppWinStyle.NormalFocus, True)

        Process.Start("explorer.exe", rutadescargas & "SQL")
        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "SQL2014", "1")

        'TODO Arreglar las comillas. Habrá comillas para las rutas.
        'Dim parametrossql As String = / IAcceptSQLServerLicenseTerms = True / Action = Install / ENU = False / QUIETSIMPLE = True / UpdateEnabled = True / ERRORREPORTING = False / USEMICROSOFTUPDATE = False / FEATURES = SQLENGINE, SSMS, ADV_SSMS /UpdateSource=MU /HELP=False /INDICATEPROGRESS=True /X86=False /INSTALLSHAREDDIR="C:\Program Files\Microsoft SQL Server" /INSTALLSHAREDWOWDIR="C:\Program Files (x86)\Microsoft SQL" /INSTANCENAME=MSSQLSERVER /SQMREPORTING=False /INSTANCEID=MSSQLSERVER /INSTANCEDIR="C:\Program Files\Microsoft SQL Server" /AGTSVCACCOUNT="NT Service\SQLSERVERAGENT" /AGTSVCSTARTUPTYPE=Manual /COMMFABRICPORT=0 /COMMFABRICNETWORKLEVEL=0 /COMMFABRICENCRYPTION=0 /MATRIXCMBRICKCOMMPORT=0 /SQLSVCSTARTUPTYPE=Automatic /FILESTREAMLEVEL=1 /ENABLERANU=False /SQLCOLLATION=Modern_Spanish_CI_AS /SQLSVCACCOUNT="NT Service\MSSQLSERVER" /SECURITYMODE=SQL /SAPWD=03071997 /SQLBACKUPDIR=F:\Notawin.Net\Backup /SQLUSERDBDIR=F:\Notawin.Net /TCPENABLED=1 /NPENABLED=1 /BROWSERSVCSTARTUPTYPE=Automatic /SQLSYSADMINACCOUNTS=Administrador

    End Sub
End Class