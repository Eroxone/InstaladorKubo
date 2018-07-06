Imports InstaladorKubo.FrmInstaladorKubo
Imports InstaladorKubo.LeerFicherosINI
Imports System.IO


Public Class FormSQL2008R2
    Private rutadescargas = cIniArray.IniGet(instaladorkuboini, "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")
    Private Const PuestoNotin As String = "ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/"

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

        'UpTimeServidor()
    End Sub

    Private Sub FormSQL2008R2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FrmInstaladorKubo.Show()
    End Sub

    Private Sub DescargarSQL()
        ObtenerEjecutables.obtenerwget()
        ObtenerEjecutables.obtenerunrar()

        Directory.CreateDirectory(rutadescargas & "SQL")
        Dim wgetsql As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/SQL2008R2.rar -O " & rutadescargas & "SQL\SQL2008R2.rar"

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

        Dim wgetini As String = "wget.exe -q -t 5 --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/SQL/ConfigurationFileR2.ini -O " & rutadescargas & "SQL\SQL2008R2\ConfigurationFileR2.ini"
        Shell("cmd /c " & rutadescargas & wgetini, AppWinStyle.NormalFocus, True)
        RegistroInstalacion("SQL2008R. Obtenido fichero INI para instalación desatendida.")
    End Sub

    'TODO arreglar ajuste de tiempo y mostrar entonces label
    Private Sub UpTimeServidor()
        Dim uptime = Environment.TickCount
        Dim uptimedias As String = (uptime / "3600") / "24" / "365"
        Dim uptimesimple = uptimedias.Substring(0, 3)
        LbUptime.Text = "UpTime: " & uptimesimple & " día/s."
    End Sub

    Private Sub BtDescargarSQL_Click(sender As Object, e As EventArgs) Handles BtDescargarSQL.Click
        DescargarSQL()
        MessageBox.Show("PROCESO COMPLETADO. Revisa el Log si has tenido algún error en la descarga/descompresión del paquete.", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        'Siempre descargo por lo que pueda pasar.
        DescargarSQL()

        Dim actualizarr2 = MessageBox.Show("A continuación se procederá a realizar la Instalación desatendida de SQL 2008R2." & vbCrLf & "- Verifica los Servicios SQL del Sistema, no debe haber ninguno Deshabilitado." & vbCrLf & "- Se recomienda Reiniciar el Servidor antes de la Actualización.", "¿Empezamos la actualización a SQL 2008R2?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If actualizarr2 = DialogResult.Yes Then
            RegistroInstalacion("SQL2008R2. El usuario confirma la ejecución del Upgrade. Empieza el proceso.")
            UpgradeSQL()
        Else
            RegistroInstalacion("SQL2008R2. El usuario canceló el Upgrade. Revisar Servicios y recomendado Reiniciar el Equipo.")
            Exit Sub
        End If
    End Sub

    Private Sub BtUpgradeLuego_Click(sender As Object, e As EventArgs) Handles BtUpgradeLuego.Click
        Dim horaejecucion As String = "22:0"
        Dim horaactual As String = DateTime.Now.Hour & ":" & DateTime.Now.Minute
        While horaactual <> horaejecucion
            horaactual = DateTime.Now.Hour & ":" & DateTime.Now.Minute
            Threading.Thread.Sleep(20000)
        End While

        DescargarSQL()

        UpgradeSQL()

        LbUpgradeLuego.ForeColor = Color.Green
        LbUpgradeLuego2.ForeColor = Color.Green

        LbUpgradeLuego.Text = "== PROCESO COMPLETADO =="
        LbUpgradeLuego2.Text = "REVISA LOGS. RECOMENDADO REINCIAR."

    End Sub

    Private Sub BtManualSQL_Click(sender As Object, e As EventArgs) Handles BtManualSQL.Click
        If Directory.Exists(rutadescargas & "SQL\SQL2008R2") = False Then
            DescargarSQL()
        End If
        Try
            Process.Start(rutadescargas & "SQL\SQL2008R2\Setup.exe")
            RegistroInstalacion("SQL2008R2. Ejecutada Instalación/Actualización Manual SQL.")
            'MessageBox.Show("Lanzada instalación desatendida de SQL. Puedes cerrar el InstaladorKubo si quieres")
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

    Private Sub BtLogSQLR2_Click(sender As Object, e As EventArgs) Handles BtLogSQLR2.Click
        Process.Start("explorer.exe", "C:\Archivos de programa\Microsoft SQL Server\100\Setup Bootstrap\Log")
    End Sub

    Private Sub BtUpgradeLuego_MouseDown(sender As Object, e As MouseEventArgs) Handles BtUpgradeLuego.MouseDown
        LbUpgradeLuego.Visible = True
        LbUpgradeLuego2.Visible = True
        BtUpgradeLuego.BackColor = Color.PaleGreen
    End Sub
End Class