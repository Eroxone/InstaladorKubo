Imports InstaladorKubo.FrmInstaladorKubo
Imports InstaladorKubo.LeerFicherosINI
Imports System.IO


Public Class FormSQL2008R2
    Dim rutadescargas = cIniArray.IniGet(instaladorkuboini, "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")


    Private Sub FormSQL2008R2_Load(sender As Object, e As EventArgs) Handles Me.Load
        FrmInstaladorKubo.Hide()
        LbRutaSQL.Text = "Carpeta: " & rutadescargas & "SQL\SQL2008R2"
        Dim descargadosql = cIniArray.IniGet(instaladorkuboini, "SQL", "DESCARGASQL2008R2", "2")
        If descargadosql = 1 Then
            BtDescargarSQL.BackColor = Color.PaleGreen
        ElseIf descargadosql = 0 Then
            BtDescargarSQL.BackColor = Color.LightSalmon
        End If


        TlpRutaSQL.ToolTipTitle = "Ruta de trabajo SQL 2008R2"
        TlpRutaSQL.SetToolTip(LbRutaSQL, "Clic para mostrar la carpeta en el explorador de archivos.")

        TlpDescargarSQL.ToolTipTitle = "Descarga y Descomprime Paquete SQL"
        TlpDescargarSQL.SetToolTip(BtDescargarSQL, "Descarga y Descomprime paquete SQL. No lo ejecuta.")

        TlpUpgradeSQL.ToolTipTitle = "Ejecuta la Actualización a SQL2008R2"
        TlpUpgradeSQL.SetToolTip(BtUpgrade, "Ejecuta la Actualización desatendida. Si el paquete SQL no se encuentra lo descargará.")

        TlpUpgradeLuegoSQL.ToolTipTitle = "Ejecuta la Actualiación a SQL2008R2"
        TlpUpgradeLuegoSQL.SetToolTip(BtUpgradeLuego, "Ejecuta la Actualización desatendida a las 22h del mismo día. Si el paquete SQL no se encuentra lo descargará.")

        TlpManualSQL.ToolTipTitle = "Descarga y ofrece instalación Manual"
        TlpManualSQL.SetToolTip(BtManualSQL, "Ejecuta Setup de SQL 2008R2 para que el usuario realice el proceso manualente.")

    End Sub

    Private Sub FormSQL2008R2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FrmInstaladorKubo.Show()
    End Sub

    Private Sub DescargarSQL()
        ObtenerEjecutables.obtenerwget()
        ObtenerEjecutables.obtenerunrar()

        Directory.CreateDirectory(rutadescargas & "SQL")
        Dim wgetsql As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/SQL2008R2.rar -O " & rutadescargas & "SQL\SQL2008R2.rar"

        'ConfigurationFileR2.ini
        Shell("cmd /c " & rutadescargas & wgetsql, AppWinStyle.NormalFocus, True)
        RegistroInstalacion("SQL2008R2: Terminada descarga del Paquete en RAR. Se procede a descomprimir la imagen.")
        Try
            Shell("cmd.exe /c " & rutadescargas & "unrar.exe x -u -y " & rutadescargas & "SQL\SQL2008R2.rar " & rutadescargas & "SQL\", AppWinStyle.NormalFocus, True)
            RegistroInstalacion("SQL2008R2 descomprimida correctamente en " & rutadescargas & "SQL\SQL2008R2")
            cIniArray.IniWrite(instaladorkuboini, "SQL", "DESCARGASQL2008R2", "1")
            BtDescargarSQL.BackColor = Color.PaleGreen
        Catch ex As Exception
            RegistroInstalacion("ERROR SQL2008R2. No se pudo descomprimir el paquete: " & ex.Message)
            cIniArray.IniWrite(instaladorkuboini, "SQL", "DESCARGASQL2008R2", "0")
            BtDescargarSQL.BackColor = Color.LightSalmon
        End Try

        Dim wgetini As String = "wget.exe -q -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/ConfigurationFileR2.ini -O " & rutadescargas & "SQL\SQL2008R2\ConfigurationFileR2.ini"
        Shell("cmd /c " & rutadescargas & wgetini, AppWinStyle.NormalFocus, True)
    End Sub

    Private Sub BtDescargarSQL_Click(sender As Object, e As EventArgs) Handles BtDescargarSQL.Click
        DescargarSQL()
    End Sub

    Private Sub UpgradeSQL()
        If File.Exists(rutadescargas & "SQL\SQL2008R2\Setup.exe") Then
            Try
                Process.Start(rutadescargas & "SQL\SQL2008R2\Setup.exe", "/ConfigurationFile=" & rutadescargas & "SQL\SQL2008R2\ConfigurationFileR2.ini")
                RegistroInstalacion("SQL2008R2: Ejecutado proceso de instalación en hilo independiente.")
            Catch ex As Exception
                MessageBox.Show("Error de Setup: " & ex.Message)
                RegistroInstalacion("SQL2008R2: Error al ejecutar el Setup: " & ex.Message)
            End Try
        Else
            MessageBox.Show("No se encuentra Setup para la instalación SQL 2008. Revisa la Descarga.", "ERROR Setup SQL2008 R2", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR SQL2008R2: No se encontró el ejecutable Setup. Revisa la descarga y descompresión del paquete.")
        End If

    End Sub

    Private Sub BtUpgrade_Click(sender As Object, e As EventArgs) Handles BtUpgrade.Click
        If Directory.Exists(rutadescargas & "SQL\SQL2008R2") = False Then
            DescargarSQL()
        End If

        UpgradeSQL()
    End Sub

    Private Sub BtUpgradeLuego_Click(sender As Object, e As EventArgs) Handles BtUpgradeLuego.Click
        If Directory.Exists(rutadescargas & "SQL\SQL2008R2") = False Then
            DescargarSQL()
        End If

        UpgradeSQL()
    End Sub

    Private Sub BtManualSQL_Click(sender As Object, e As EventArgs) Handles BtManualSQL.Click
        If Directory.Exists(rutadescargas & "SQL\SQL2008R2") = False Then
            DescargarSQL()
        End If
        Try
            Process.Start(rutadescargas & "SQL\SQL2008R2\Setup.exe")
            RegistroInstalacion("SQL2008R2. Ejecutada Instalación/Actualización Manual SQL.")
        Catch ex As Exception
            RegistroInstalacion("ERROR SQL2008R2. No se pudo ejecutar la Instalación/Actualización Manual SQL." & ex.Message)
        End Try

    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        FrmInstaladorKubo.Show()
        Me.Close()
    End Sub

    Private Sub LbRutaSQL_Click(sender As Object, e As EventArgs) Handles LbRutaSQL.Click
        If Directory.Exists(rutadescargas & "SQL\SQL2008R2") Then
            Process.Start("explorer.exe", rutadescargas & "SQL\SQL2008R2")
        Else
            MessageBox.Show("No existe la Carpeta. Comprueba que se haya descargado y descomprimido el paquete.", "Error de acceso a Ruta", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class