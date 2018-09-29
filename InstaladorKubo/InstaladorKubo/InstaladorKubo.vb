Imports System.IO
Imports System.Text
Imports InstaladorKubo.LeerFicherosINI
Imports System.Threading
Imports System.Net.Mail
Imports InstaladorKubo.FrmConfigurarISL
Imports InstaladorKubo.ObtenerEjecutables
Imports System.Environment
Imports InstaladorKubo.Chocolatey
Imports System.Deployment.Application


'WEB DE INSTALACIÓN
'http://instalador.notin.net


Public Class FrmInstaladorKubo
    'CONTROLES DESCARGAS VARIABLES STRING
    Private Const FILE_DOWNLOAD As String = "descargas.txt"
    Private Const REQUISITOS_DOWNLOAD As String = "requisitos.txt"
    Private Const TERCEROS_DOWNLOAD As String = "terceros.txt"
    Private Const REGISTRO_DOWNLOAD As String = "registro.txt"
    Private Const PuestoNotin As String = "ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/"
    Private RutaDescargas As String = GetPathTemp() 'PATH_TEMP
    Public Const instaladorkuboini = "C:\TEMP\InstaladorKubo\InstaladorKubo.ini"


    Private Sub FrmInstaladorNotin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Directory.CreateDirectory("C:\TEMP\InstaladorKubo")
            File.AppendAllText("C:\TEMP\InstaladorKubo\RegistroInstalacion.txt", vbCrLf & vbCrLf)
            RegistroInstalacion("=== NUEVA EJECUCION DEL INSTALADOR === FECHA: " & DateTime.Now.Date)
            'RegistroInstalacion("FECHA:" & DateTime.Now.Date)
        Catch ex As Exception
            MessageBox.Show("Error creando ruta Temporal. Pueden seguir mas errores en el Instalador.", "Error Ruta TEMP", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        SistemaOperativo()
        lbRuta.Text = GetPathTemp()
        UpTimeServidor()
        YaDescargados()
        Tooltips()
        FicheroINI()
        'NotinenF()

        'Comprobación de ficheros para habilitar boton Traer Paquete a Servidor
        If Directory.Exists("F:\PRG.INS\NOTIN\InstaladorKubo") = True Then
            BtTraerdeF.Enabled = True
            BtTraerdeF.BackColor = Color.AliceBlue
        End If

        'Icono reconectar F
        If UnidadF() = False Then
            BtReconectar.Enabled = True
        End If

        Dim preparacioninicial = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "REGFT", "2")
        If preparacioninicial = 1 Then
            LbPreparacionInicial.Visible = True
        End If

        'Cargo correo anterior de notificaciones
        CBoxEmail.Text = cIniArray.IniGet(instaladorkuboini, "EMAIL", "DESTINATARIO", "")

        'Version NET en Sistema
        ObtenerVersionNet()


        'Ejecución MigradorSQL
        'LbMigrador.Text = cIniArray.IniGet(instaladorkuboini, "SQL", "FECHAMIGRADOR", "Sin determinar")

        'If File.Exists("C:\Program Files (x86)\Humano Software\MigradorSQL\Log\LoggerMigradorNotin.txt") Then
        LeerLogMigradorSQL()
        'End If

        'Version FrameWork Sistema
        ObtenerVersionFW()

        'Mostrar versión de Aplicación

        'TODO Muestre versión
        Try
            'LbVersionApp.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
            'LbVersionApp.Text = My.Application.Info.Version.Revision
            Dim myVersion As Version

            If ApplicationDeployment.IsNetworkDeployed Then
                myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion
                LbVersionApp.Text = String.Concat("ClickOnce: v", myVersion)
            End If
        Catch ex As Exception
            RegistroInstalacion("No se detectó la versión de ClciOnce Publicada. Esto no es un error en la App")
        End Try

        'Comprobar si existe Office 2016 x64 para mostrar .Net con x64

        If File.Exists("C:\Program Files\Microsoft Office\Office16\WINWORD.EXE") Then
            BtEstablex64F462.Enabled = True
            BtNetBetax64F462.Enabled = True
            'LbWordx64.Text = "Versión Office2016 x64"

            BtEstableNet.Enabled = False
            BtNetBeta.Enabled = False
            BtEstablew32F462.Enabled = False
            BtNetBetaW32F462.Enabled = False

            LbSitienesF462.Enabled = False
        Else
            LbWordx64.Enabled = False
        End If

    End Sub

    Private Sub InstaladorKubo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Keys.J Then
            BtLimpiar.Visible = True
            BtLogin.Visible = True
            BtSubeBinario.Visible = True
        End If
    End Sub

    Private Sub BtLimpiar_Click(sender As Object, e As EventArgs) Handles BtLimpiar.Click
        Try
            'File.Delete("C:\TEMP\InstaladorKubo\instaladorkubo.ini")
            File.Delete("C:\TEMP\InstaladorKubo\RegistroInstalacion.txt")
            RegistroInstalacion("=== Limpiado Log de Registro ===")

            PbInstalaciones.Visible = True
            PbInstalaciones.Step = 35
            Dim p As Integer
            While p < 3
                p = p + 1
                Threading.Thread.Sleep(500)
                PbInstalaciones.PerformStep()
            End While
            PbInstalaciones.Visible = False
        Catch ex As Exception
            RegistroInstalacion("Limpieza ficheros: " & ex.Message)
        End Try

    End Sub

    Private Sub BtLogin_Click(sender As Object, e As EventArgs) Handles BtLogin.Click
        cIniArray.IniWrite(instaladorkuboini, "LOGIN", "ENTRADA", 1)

        PbInstalaciones.Visible = True
        PbInstalaciones.Step = 35
        Dim p As Integer
        While p < 3
            p = p + 1
            Threading.Thread.Sleep(500)
            PbInstalaciones.PerformStep()
        End While
        PbInstalaciones.Visible = False
    End Sub

    Private Sub BtLimpiarPaquetes_Click(sender As Object, e As EventArgs) Handles BtLimpiarPaquetes.Click
        'TODO limpiar REG - BAT y lo que pueda ser delicado.
    End Sub


#Region "Cabecera Formulario"
    Private Sub SistemaOperativo()
        Dim SistemaO = (My.Computer.Info.OSFullName)
        lbSistemaO.Text = SistemaO
        If SistemaO.Contains("Home") Then
            lbSistemaO.ForeColor = Color.Red
        End If

        If Directory.Exists("C:\WINDOWS\SYSWOW64") Then
            lb64bits.Text = ("Sistema Operativo de 64bits")
        ElseIf Directory.Exists("C:\WINDOWS\SYSTEM32") Then
            lb64bits.Text = ("Sistema Operativo de 32bits")
        Else
            MessageBox.Show("No se puede determinar la arquitectura de Windows. Revisa la instalación del SO y que la ruta raíz sea C:\", "Arquitectura indeterminada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            lb64bits.Text = ("Arquitectura SO sin determinar")
        End If

        Dim equipousuario As String = (My.User.Name)
        Dim equipo As Integer = equipousuario.LastIndexOf("\")
        Dim usuario = equipousuario.Remove(0, equipo + 1)
        lbUsuario.Text = "Usuario: " & usuario

        Dim hostname = Environment.MachineName
        LbHostname.Text = hostname

        Dim memoriaram = My.Computer.Info.TotalPhysicalMemory
        Dim memoriarammb As Integer = memoriaram / 1024 / 1024
        LBmemoriaram.Text = "Memoria RAM " & memoriarammb & "MB"
        If memoriarammb < 3096 Then
            LBmemoriaram.ForeColor = Color.Red
        End If

        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
            ' lbUnidadF.BackColor = Color.Green
            'Logger("Iniciando con Unidad F CONECTADA")
            RegistroInstalacion("Iniciando con Unidad F CONECTADA.")
        Else
            lbUnidadF.Text = "DESCONECTADA"
            lbUnidadF.ForeColor = Color.Red
            '  lbUnidadF.BackColor = Color.Red
            'Logger("Iniciando con Unidad F DESCONECTADA")
            RegistroInstalacion("Iniciando con Unidad F DESCONECTADA.")
        End If

    End Sub
#End Region

    'RUTA ANTERIOR. SI EXISTÍA
    Private Function GetPathTemp() As String
        'cIniArray.IniWrite("D:\NOTIN\NNOTIN.INI", "NET", "NOMBRESERVIDOR", "holaquetal")
        Dim rutadescargasini = cIniArray.IniGet(instaladorkuboini, "RUTAS", "RUTADESCARGAS", Nothing)
        If rutadescargasini IsNot Nothing Then
            Return rutadescargasini
            cIniArray.IniWrite(instaladorkuboini, "RUTAS", "RUTADESCARGAS", rutadescargasini)
        Else
            'If System.IO.File.Exists("C:\TEMP\InstaladorKubo\RutaAnterior.txt") Then
            '    Return (System.IO.File.ReadAllText("C:\TEMP\InstaladorKubo\RutaAnterior.txt"))
            'End If
            Return "C:\NOTIN\"
        End If
    End Function

    'COMPROBAR Unidad F
    Private Function UnidadF() As Boolean
        If File.Exists("F:\Windows\Nnotin.ini") Then
            Return True
        End If
        Return False
        'Dim CuentaReintentos As Integer = CuentaReintentos + 1
    End Function

    Private Sub FicheroINI()
        Dim odbc = cIniArray.IniGet(instaladorkuboini, "ODBC", "NOTINSQL", "2")
        If odbc = 1 Then
            BtOdbc.BackColor = Color.PaleGreen
        ElseIf odbc = 0 Then
            BtOdbc.BackColor = Color.LightSalmon
        End If

        Dim framework = cIniArray.IniGet(instaladorkuboini, "REQUISITOS", "FRAMEWORK35", "2")
        If framework = 1 Then
            BtFramework.BackColor = Color.PaleGreen
        Else
            BtFramework.BackColor = SystemColors.Control
        End If

        Dim excepjava = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "EXCEPJAVA", "2")
        If excepjava = 1 Then
            BtExcepJava.BackColor = Color.PaleGreen
        Else
            BtExcepJava.BackColor = SystemColors.Control
        End If

        Dim java = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "JAVA8", "2")
        If java = 1 Then
            BtJava.BackColor = Color.PaleGreen
        End If

        Dim uac = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "UAC", "2")
        If uac = 1 Then
            BtUac.BackColor = Color.PaleGreen
        ElseIf uac = 0 Then
            BtUac.BackColor = Color.LightSalmon
        End If

        Dim comienzodescargas = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "COMIENZO", "2")
        If comienzodescargas = 1 Then
            BtCopiarhaciaF.Enabled = True
        End If

        Dim claveinift = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "REGFT", "2")
        If claveinift = 1 Then
            btNotinKubo.BackColor = Color.Honeydew
        End If

        Dim configuraword2016 = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "2")
        If configuraword2016 = 1 Then
            BtConfiguraWord2016.BackColor = Color.PaleGreen
        ElseIf configuraword2016 = 0 Then
            BtConfiguraWord2016.BackColor = Color.LightSalmon
        End If

        Dim directivas = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "DIRECTIVAS", "2")
        If directivas = 1 Then
            BtDirectivas.BackColor = Color.PaleGreen
        End If
        Dim kmspico = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "KMSPICO10", "2")
        If kmspico = 1 Then
            BtKmsPico.BackColor = Color.PaleGreen
        ElseIf kmspico = 0 Then
            BtKmsPico.BackColor = Color.LightSalmon
        End If

        Dim notinpdf = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "NOTINPDF", "2")
        If notinpdf = 1 Then
            BtNotinpdf.BackColor = Color.PaleGreen
        ElseIf notinpdf = 0 Then
            BtNotinpdf.BackColor = Color.LightSalmon
        End If

        Dim isl = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "ISL", "2")
        If isl = 1 Then
            BtISL.BackColor = Color.PaleGreen
        End If
        'Dim limpia2003 = cIniArray.IniGet(instaladorkuboini, "LIMPIADORES", "OFFICE2003", "2")
        'If limpia2003 = 1 Then
        '    BtLimpiar2003.BackColor = Color.PaleGreen
        'End If
        'Dim limpia2016 = cIniArray.IniGet(instaladorkuboini, "LIMPIADORES", "OFFICE2016", "2")
        'If limpia2016 = 1 Then
        '    BtLimpiar2016.BackColor = Color.PaleGreen
        'End If

        Dim sql2014 = cIniArray.IniGet(instaladorkuboini, "SQL2014", "INSTALADO", "2")
        If sql2014 = 1 Then
            BtSQL2014.BackColor = Color.PaleGreen
        End If

        Dim confword2016adra = cIniArray.IniGet(instaladorkuboini, "ADRA", "CONFIGURAWORD2016", "2")
        If confword2016adra = 1 Then
            BtConfWord2016ADRA.BackColor = Color.PaleGreen
        ElseIf confword2016adra = 0 Then
            BtConfWord2016ADRA.BackColor = Color.LightSalmon
        Else
            BtConfWord2016ADRA.BackColor = SystemColors.Control
        End If

        Dim notinnet = cIniArray.IniGet(instaladorkuboini, "NET", "NOTINNET", "FALSE")
        If notinnet = "BETA" Then
            BtNetBeta.BackColor = Color.PaleGreen
        ElseIf notinnet = "ESTABLEX64F462" Then
            BtEstablex64F462.BackColor = Color.PaleGreen
        ElseIf notinnet = "ESTABLE" Then
            BtEstableNet.BackColor = Color.PaleGreen
        ElseIf notinnet = "BETAx64F462" Then
            BtNetBetax64F462.BackColor = Color.PaleGreen
        ElseIf notinnet = "BETAW32F462" Then
            BtNetBetaW32F462.BackColor = Color.PaleGreen
        ElseIf notinnet = "ESTABLEW32F462" Then
            BtEstablew32F462.BackColor = Color.PaleGreen

        End If

        If File.Exists(RutaDescargas & "NotinNet\NotinNetInstaller.exe") Then
            BtNotinNetF.Visible = True
        End If

        Dim bdblancos = cIniArray.IniGet(instaladorkuboini, "SQL2014", "BLANCOS", "2")
        If bdblancos = 1 Then
            BtBlancosBD.BackColor = Color.PaleGreen
        ElseIf bdblancos = 0 Then
            BtBlancosBD.BackColor = Color.Salmon
        Else
            BtBlancosBD.BackColor = SystemColors.Control
        End If

        'Dim migradorsql = cIniArray.IniGet(instaladorkuboini, "SQL", "MIGRADOR", "SINDATOS")
        'If migradorsql = "EXITO" Then
        '    BtMigradorSQL.BackColor = Color.PaleGreen
        '    'BtMigradorLOG.Visible = True
        'ElseIf migradorsql = "ERROR" Then
        '    BtMigradorSQL.BackColor = Color.LightSalmon
        'End If

        Dim framework462 = cIniArray.IniGet(instaladorkuboini, "CHOCOLATEY", "FRAMEWORK462", "2")
        If framework462 = 1 Then
            BtFramework462.BackColor = Color.PaleGreen
        End If

        Dim instaladochocolatey = cIniArray.IniGet(instaladorkuboini, "CHOCOLATEY", "INSTALADO", "2")
        If instaladochocolatey = 1 Then
            BtChocolatey.BackColor = Color.PaleGreen
            BtLogChoco.Visible = True
        ElseIf instaladochocolatey = 0 Then
            BtChocolatey.BackColor = Color.LightSalmon
            BtLogChoco.Visible = True
        Else
            BtChocolatey.BackColor = SystemColors.Control
        End If

        Dim focos = cIniArray.IniGet(instaladorkuboini, "ADRA", "FOCOS", "2")
        If focos = 1 Then
            BtFocos.BackColor = Color.PaleGreen
        End If

        Dim nautilus = cIniArray.IniGet(instaladorkuboini, "NET", "NAUTILUS", "2")
        If nautilus = 1 Then
            BtNautilus.BackColor = Color.PaleGreen
        ElseIf nautilus = 0 Then
            BtNautilus.BackColor = Color.LightSalmon
        End If

        Dim visorimagenes = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "VISORIMAGENES", "2")
        If visorimagenes = 1 Then
            BtVisorImagenes.BackColor = Color.PaleGreen
        End If

        Dim sql2008r2 = cIniArray.IniGet(instaladorkuboini, "SQL", "DESCARGASQL2008R2", "2")
        If sql2008r2 = 1 Then
            BtSQL2008R2.BackColor = Color.PaleGreen
        End If

        Dim dynamicsolar = cIniArray.IniGet(instaladorkuboini, "ADRA", "DYNAMICSOLAR", "2")
        If dynamicsolar = 1 Then
            BtDynamic.BackColor = Color.PaleGreen
        End If

        Dim word2016adra = cIniArray.IniGet(instaladorkuboini, "ADRA", "CONFIGURAWORD2016", "2")
        If word2016adra = 1 Then
            BtConfWord2016ADRA.BackColor = Color.PaleGreen
        ElseIf word2016adra = 0 Then
            BtConfWord2016ADRA.BackColor = Color.LightSalmon
        End If

        Dim limpiarperfiladra = cIniArray.IniGet(FrmInstaladorKubo.instaladorkuboini, "ADRA", "LIMPIARPERFIL", "2")
        If limpiarperfiladra = 1 Then
            BtLimpiarPerfil.BackColor = Color.PaleGreen
        End If
    End Sub

    'Boton EXAMINAR
    Private Sub BtDirDescargas_Click(sender As Object, e As EventArgs) Handles btDirDescargas.Click
        fbdDescarga.ShowDialog()

        ' Si hago clic en Cancelar me quedo con la ruta anterior
        If String.IsNullOrEmpty(fbdDescarga.SelectedPath) OrElse fbdDescarga.SelectedPath = "\" Then
            fbdDescarga.SelectedPath = GetPathTemp()
            fbdDescarga.SelectedPath = GetPathTemp.Remove(GetPathTemp.Length - 1)
        End If

        'RutaDescargas = fbdDescarga.SelectedPath & "\"
        'lbRuta.Text = RutaDescargas
        'cIniArray.IniWrite(instaladorkuboini, "RUTAS", "RUTADESCARGAS", RutaDescargas)
        'File.WriteAllText("C:\TEMP\InstaladorKubo\RutaAnterior.txt", RutaDescargas)
        'Compruebo si el Directorio contiene espacios y pido que lo cambies

        'Busco si la ruta es raiz o contiene espacios
        If fbdDescarga.SelectedPath.Contains(" ") OrElse fbdDescarga.SelectedPath.Contains("F:") OrElse
            System.Text.RegularExpressions.Regex.IsMatch(fbdDescarga.SelectedPath, "[A-Z](:\\)$") Then
            MessageBox.Show(fbdDescarga.SelectedPath & " no es una ruta válida. No se admiten espacios ni unidades de red o raíz.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Dim RutaDescargas = "C:\NOTIN\"
            'cIniArray.IniWrite(instaladorkuboini, "RUTAS", "RUTADESCARGAS", RutaDescargas)
            'lbRuta.Text = RutaDescargas
        Else
            RutaDescargas = fbdDescarga.SelectedPath & "\"
        End If
        cIniArray.IniWrite(instaladorkuboini, "RUTAS", "RUTADESCARGAS", RutaDescargas)
        lbRuta.Text = RutaDescargas
        RegistroInstalacion("Ruta Descargas definida a " & RutaDescargas)
        YaDescargados()


        'lbRuta.Text = RutaDescargas
        'If btDirDescargas.DialogResult = Windows.Forms.DialogResult.Cancel Then
        '    RutaDescargas = GetPathTemp()
        'End If
    End Sub

    Private Sub UpTimeServidor()
        Try
            Dim uptime = Environment.TickCount
            Dim uptimedias As String = (uptime / 1000) / 3600 / 24
            Dim uptimesolodias As Integer = uptimedias.LastIndexOf(",")
            Dim uptimesimple = uptimedias.Substring(0, uptimesolodias)
            If uptimesimple < 1 OrElse uptimesimple = 1 Then
                LbUptime.Text = "UpTime < 1 día."
            ElseIf uptimesimple > 1 And uptimesimple < 2 Then
                LbUptime.Text = "UpTime < 2 días."
            ElseIf uptimesimple < 0 Then
                LbUptime.Text = "Uptime Sin determinar."
            Else
                LbUptime.Text = "UpTime " & uptimesimple & " días"
            End If
            RegistroInstalacion("Determinado Uptime Servidor en " & uptime & " milisegundos.")
        Catch ex As Exception
            LbUptime.Text = "Uptime No se pudo determinar."
        End Try
    End Sub


    'Recarga formulario tras salir de la eleccion de ruta
    'Private Sub lbRuta_TextChanged(sender As Object, e As EventArgs) Handles lbRuta.TextChanged
    '    YaDescargados()
    'End Sub

#Region "COMPROBAR TAMAÑO DESCARGAS"
    ' Archivo existe y mostrar tamaño del mismo
    Private Sub YaDescargados()
        'FICHEROS NOTIN Y WORD
        If System.IO.File.Exists(RutaDescargas & "Office2003.exe") Then
            'Obtener tamaño del archivo
            Dim Archivo2003 As New FileInfo(RutaDescargas & "Office2003.exe")
            Dim Length2003 As Long = Archivo2003.Length
            If Archivo2003.Length = "517577131" Then
                CbOffice2003.ForeColor = Color.DarkGreen
                '         cbOffice2003.Enabled = False
            ElseIf Archivo2003.Length < "517577131" Then
                CbOffice2003.ForeColor = Color.Red
            End If
        Else
            CbOffice2003.ForeColor = SystemColors.ControlText
            '     cbOffice2003.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Registro\ConfigAccess.reg") Then
            Dim ConfigNotin As New FileInfo(RutaDescargas & "Registro\ConfigAccess.reg")
            Dim LengthNotin As Long = ConfigNotin.Length
            If ConfigNotin.Length = "16688" Then
                CbConfiguraNotin.ForeColor = Color.DarkGreen
                '        cbConfiguraNotin.Enabled = False
            End If
        Else
            CbConfiguraNotin.ForeColor = SystemColors.ControlText
            '     cbConfiguraNotin.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Office2016.exe") Then
            Dim Archivo2016 As New FileInfo(RutaDescargas & "Office2016.exe")
            Dim Length2016 As Long = Archivo2016.Length
            If Archivo2016.Length = "739967123" Then
                CbOffice2016.ForeColor = Color.DarkGreen
                CbOffice2016odt.ForeColor = Color.DarkGreen
                '         cbOffice2016.Enabled = False
            ElseIf Archivo2016.Length < "739967123" Then
                CbOffice2016.ForeColor = Color.Red
                CbOffice2016odt.ForeColor = Color.Red
            End If
        Else
            CbOffice2016.ForeColor = SystemColors.ControlText
            CbOffice2016odt.ForeColor = SystemColors.ControlText
            '     cbOffice2016.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Office2016x64.rar") Then
            Dim Archivo2016x64 As New FileInfo(RutaDescargas & "Office2016x64.rar")
            Dim Length2016x64 As Long = Archivo2016x64.Length
            If Archivo2016x64.Length = "864537901" Then
                CbOffice2016x64.ForeColor = Color.DarkGreen
                '         cbOffice2016.Enabled = False
            ElseIf Archivo2016x64.Length < "864537901" Then
                CbOffice2016x64.ForeColor = Color.Red
            End If
        Else
            CbOffice2016x64.ForeColor = SystemColors.ControlText
        End If
        If System.IO.File.Exists(RutaDescargas & "ConfWord2016.rar") Then
            Dim Config2016 As New FileInfo(RutaDescargas & "ConfWord2016.rar")
            Dim LengthConfig2016 As Long = Config2016.Length
            If Config2016.Length = "8211" Then
                CbConfiguraWord2016.ForeColor = Color.DarkGreen
                '        cbConfiguraWord2016.Enabled = False
            End If
        Else
            CbConfiguraWord2016.ForeColor = SystemColors.ControlText
            '      cbConfiguraWord2016.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "ConfWord2016x64.rar") Then
            Dim Config2016x64 As New FileInfo(RutaDescargas & "ConfWord2016x64.rar")
            Dim LengthConfig2016x64 As Long = Config2016x64.Length
            If Config2016x64.Length = "8229" Then
                CbConfiguraWord2016x64.ForeColor = Color.DarkGreen
            End If
        Else
            CbConfiguraWord2016x64.ForeColor = SystemColors.ControlText
        End If

        If System.IO.File.Exists(RutaDescargas & "jnemo-latest.exe") Then
            Dim jNemo As New FileInfo(RutaDescargas & "jnemo-latest.exe")
            Dim LengthjNemo As Long = jNemo.Length
            If jNemo.Length = "12672337" Then
                CbNemo.ForeColor = Color.DarkGreen

                '         cbNemo.Enabled = False
            End If
        Else
            CbNemo.ForeColor = SystemColors.ControlText
            '     cbNemo.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "PuestoNotinC.exe") Then
            Dim PuestoNotinC As New FileInfo(RutaDescargas & "PuestoNotinC.exe")
            Dim LengthPuestoNotinC As Long = PuestoNotinC.Length
            If PuestoNotinC.Length = "17034578" Then
                CbPuestoNotin.ForeColor = Color.DarkGreen
                '        cbPuestoNotin.Enabled = False
            End If
        Else
            CbPuestoNotin.ForeColor = SystemColors.ControlText
            '    cbPuestoNotin.Enabled = True
        End If

        'REQUISITOS. Cuento los archivos en el directorio
        If System.IO.Directory.Exists(RutaDescargas & "\Requisitos") Then
            Dim ArchivosenRequisitos = My.Computer.FileSystem.GetFiles(RutaDescargas & "\Requisitos")

            If ArchivosenRequisitos.Count >= 4 Then
                CbRequisitos.ForeColor = Color.DarkGreen
                '                cbRequisitos.Enabled = False
            Else
                CbRequisitos.ForeColor = Color.Red
            End If
        Else
            CbRequisitos.ForeColor = SystemColors.ControlText
            '        cbRequisitos.Enabled = True
        End If

        'ANCERT Como puede variar el tamaño solo miro que exista el fichero
        If System.IO.File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
            CbSferen.ForeColor = Color.DarkGreen
            '            cbSferen.Enabled = False
        Else
            CbSferen.ForeColor = SystemColors.ControlText
            '          cbSferen.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
            CbPasarelaSigno.ForeColor = Color.DarkGreen
            '       cbPasarelaSigno.Enabled = False
        Else
            CbPasarelaSigno.ForeColor = SystemColors.ControlText
            '     cbPasarelaSigno.Enabled = True
        End If

        'SOFTWARE TERCEROS
        If System.IO.Directory.Exists(RutaDescargas & "\Software") Then
            CbTerceros.ForeColor = Color.DarkGreen
            BtExplorarRutas.Enabled = True
        Else
            CbTerceros.ForeColor = SystemColors.ControlText
        End If

        'ABBYY Fine Reader
        If System.IO.File.Exists(RutaDescargas & "Software/FineReaderV11.exe") Then
            Dim FineReader11 As New FileInfo(RutaDescargas & "Software/FineReaderV11.exe")
            Dim LengthFineReader11 As Long = FineReader11.Length
            If FineReader11.Length = "405915565" Then
                CbFineReader.ForeColor = Color.DarkGreen
            ElseIf FineReader11.Length < "405915565" Then
                CbFineReader.ForeColor = Color.Red
            End If
        Else
            CbFineReader.ForeColor = SystemColors.ControlText
        End If

        'Paquetes FT
        If System.IO.File.Exists(RutaDescargas & "PaquetesFT.rar") Then
            Dim paquetesft As New FileInfo(RutaDescargas & "PaquetesFT.rar")
            Dim lengthpaquetesft As Long = paquetesft.Length
            If paquetesft.Length = "2044887" Then
                CbPaquetesFT.ForeColor = Color.DarkGreen
            ElseIf paquetesft.Length < "2044887" Then
                CbPaquetesFT.ForeColor = Color.Red
            End If
        Else
            CbPaquetesFT.ForeColor = SystemColors.ControlText
        End If


    End Sub
#End Region



#Region "COMENZAR DESCARGAS"
    Private Sub BtDescargar_Click(sender As Object, e As EventArgs) Handles btDescargar.Click

        'Si no chequeas nada salimos
        If (CbConfiguraNotin.Checked OrElse CbConfiguraWord2016.Checked OrElse CbNemo.Checked OrElse CbOffice2003.Checked OrElse CbOffice2016.Checked OrElse CbOffice2016odt.Checked OrElse CbPasarelaSigno.Checked OrElse CbPuestoNotin.Checked OrElse CbRequisitos.Checked OrElse CbSferen.Checked OrElse CbTerceros.Checked OrElse CbFineReader.Checked OrElse CbPaquetesFT.Checked OrElse CbOffice2016x64.Checked OrElse CbConfiguraWord2016x64.Checked) = False Then
            MessageBox.Show("NINGUNA DESCARGA SELECCIONADA.", "Gestión Descargas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'Escribir en el INI para que conste que ya se ha efectuado alguna descarga
        cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "COMIENZO", "1")

        'TODO Mas adelante me meto con la barra de progreso en otro hilo

        'Defino la cadenas vacias del fichero para las descargas y que esten limpios de principio
        Dim texto As String = ""
        Dim requisitos As String = ""
        Dim terceros As String = ""
        Dim registro As String = ""
        'Dim abbyyfinereader As String = ""

        'Directorio descargas. No es necesario pero me apunto estas funciones
        Try
            System.IO.Directory.CreateDirectory(RutaDescargas)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error de Ruta", MessageBoxButtons.OK)
        End Try



        'Comprobamos si existe ya wget.exe
        If Not System.IO.File.Exists(RutaDescargas & "wget.exe") Then

            'Descargar ejecutable WGet
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "juanjo", "Palomeras24", False, 20000, False)
            Catch ex As Exception
                'MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "juanjo", "Palomeras24", False, 20000, False)
                If REINTENTAR = DialogResult.Retry Then
                    My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "juanjo", "Palomeras24", False, 20000, True)
                End If

            End Try

        End If



#Region "CREACIÓN FICHEROS RUTAS DESCARGAS"

        'Creación contenido del fichero
        If CbOffice2003.Checked Then
            texto = texto & PuestoNotin & "Office2003.exe" & vbCrLf
            texto = texto & PuestoNotin & "Setup.mst" & vbCrLf
            texto = texto & PuestoNotin & "Setup2003.mst" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2003", "1")
        End If

        If CbOffice2016.Checked Then
            texto = texto & PuestoNotin & "Office2016.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016", "1")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016ODT", "0")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016X64", "0")
        End If

        If CbOffice2016odt.Checked Then
            texto = texto & PuestoNotin & "Office2016.exe" & vbCrLf
            texto = texto & PuestoNotin & "setup2016.MSP" & vbCrLf
            texto = texto & PuestoNotin & "Setup2016SinWord.MSP" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016ODT", "1")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016", "0")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016X64", "0")
        End If
        If CbOffice2016x64.Checked Then
            texto = texto & PuestoNotin & "Office2016x64.rar" & vbCrLf
            texto = texto & PuestoNotin & "setup2016x64.MSP" & vbCrLf

            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016ODT", "0")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016", "0")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016X64", "1")
        End If

        If CbNemo.Checked Then
            texto = texto & "http://nemo.notin.net/jnemo-latest.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "NEMO", "1")
        End If

        If CbPuestoNotin.Checked Then
            texto = texto & PuestoNotin & "PuestoNotinC.exe" & vbCrLf
            texto = texto & PuestoNotin & "AccesosDirectos.exe" & vbCrLf
            texto = texto & PuestoNotin & "AccesosDirectos2003.exe" & vbCrLf
            texto = texto & PuestoNotin & "AccesosDirectosx64.exe" & vbCrLf
            texto = texto & PuestoNotin & "ScanImg_Beta_FT.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "PUESTONOTIN", "1")
        End If

        If CbPaquetesFT.Checked Then
            texto = texto & PuestoNotin & "PaquetesFT.rar" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "PAQUETESFT", "1")
        End If


        If CbRequisitos.Checked Then
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "KryptonSuite300.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "Office2003PrimaryInterop.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2005.exe" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2015.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "REQUISITOS", "1")
            '   requisitos = requisitos & PuestoNotin & "Requisitos/" & "Framework35.bat" & vbCrLf
        End If

        If CbSferen.Checked Then
            texto = texto & PuestoNotin & "SFeren-2.8.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "SFEREN", "1")
        End If

        If CbPasarelaSigno.Checked Then
            texto = texto & PuestoNotin & "PasarelaSigno.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "PASARELASIGNO", "1")
        End If

        If CbTerceros.Checked Then
            terceros = terceros & PuestoNotin & "Software/" & "AcrobatReaderDC.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "FileZilla_3_win64-setup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "ChromeSetup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "Notepad_x64.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "WinRAR5.exe" & vbCrLf
            '    terceros = terceros & PuestoNotin & "Software/" & "FineReaderV11demo .exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "SOFTWARETERCEROS", "1")
        End If

        'Descagar configuradores del autochequeo
        If CbConfiguraNotin.Checked Then
            registro = registro & PuestoNotin & "ConfigAccess.reg" & vbCrLf
            registro = registro & PuestoNotin & "FTComoAdministrador.reg" & vbCrLf
            registro = registro & PuestoNotin & "VentanasSigno.reg" & vbCrLf
            registro = registro & PuestoNotin & "MSOUTL.OLB" & vbCrLf
            'registro = registro & PuestoNotin & "ExclusionDefender.reg" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "CLAVESREGISTRO", "1")
        End If

        If CbConfiguraWord2016.Checked Then
            'texto = texto & PuestoNotin & "ConfiguraWord2016.exe" & vbCrLf
            texto = texto & PuestoNotin & "ConfWord2016.rar" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "CONFIGURAWORD", "1")
        End If
        If CbConfiguraWord2016x64.Checked Then
            texto = texto & PuestoNotin & "ConfWord2016x64.rar" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "CONFIGURAWORDX64", "1")
        End If

        'Guardar el texto en el fichero
        System.IO.File.WriteAllText(RutaDescargas & FILE_DOWNLOAD, texto)
        System.IO.File.WriteAllText(RutaDescargas & REQUISITOS_DOWNLOAD, requisitos)
        System.IO.File.WriteAllText(RutaDescargas & TERCEROS_DOWNLOAD, terceros)
        System.IO.File.WriteAllText(RutaDescargas & REGISTRO_DOWNLOAD, registro)

#End Region


#Region "EJECUTAMOS WGET"
        'EJECUCIONES DE WGET POR RUTAS DE DESCARGA

        'Ejecutar WGET Office y Notin
        Dim WGETPANDORA As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 -i " & """" & RutaDescargas & "descargas.txt"" -P " & RutaDescargas
        Dim RutaCMDWget As String = RutaDescargas & WGETPANDORA

        'SI EXISTEN ESPACIOS EN LOS NOMBRES DE ARCHIVOS NO FUNCIONA YA QUE SI PONGO COMILLAS SI NO LLEVA ESPACIOS DA ERROR

        Shell("cmd.exe /c " & RutaCMDWget, AppWinStyle.NormalFocus, True)
        '    YaDescargados()

        'Ejecutar WGET Requisitos
        If CbRequisitos.Checked Then
            Dim WGETPANDORAREQUISITOS As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 -i " & """" & RutaDescargas & "requisitos.txt"" -P " & RutaDescargas & "Requisitos\"
            Dim RutaCMDWgetRequisitos As String = RutaDescargas & WGETPANDORAREQUISITOS

            Shell("cmd.exe /c " & RutaCMDWgetRequisitos, AppWinStyle.NormalFocus, True)
            '       YaDescargados()
        End If

        'Ejecutar WGET Terceros
        If CbTerceros.Checked Then
            Dim WGETPANDORATERCEROS As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 -i " & """" & RutaDescargas & "terceros.txt"" -P " & RutaDescargas & "Software\"
            Dim RutaCMDWgetTerceros As String = RutaDescargas & WGETPANDORATERCEROS

            Shell("cmd.exe /c " & RutaCMDWgetTerceros, AppWinStyle.NormalFocus, True)
            '     YaDescargados()
        End If

        'Ejecutar WGET AbbyyFineReader
        If CbFineReader.Checked Then
            Dim WGETFINEREADER As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 " & PuestoNotin & "Software/FineReaderV11.exe " & "-O " & RutaDescargas & "Software\FineReaderV11.exe"
            Dim RutaCMDWgetFineReader As String = RutaDescargas & WGETFINEREADER
            Shell("cmd.exe /c " & RutaCMDWgetFineReader, AppWinStyle.NormalFocus, True)
        End If

        'Ejecutar WGET Registro
        If CbConfiguraNotin.Checked Then
            Dim WGETPANDORREGISTRO As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 -i " & """" & RutaDescargas & "registro.txt"" -P " & RutaDescargas & "Registro\"
            Dim RutaCMDWgetRegistro As String = RutaDescargas & WGETPANDORREGISTRO

            Shell("cmd.exe /c " & RutaCMDWgetRegistro, AppWinStyle.NormalFocus, True)
            ' YaDescargados()
        End If
        YaDescargados()

#End Region

        'Borrar ficheros txt escritos
        System.IO.File.Delete(RutaDescargas & FILE_DOWNLOAD)
        System.IO.File.Delete(RutaDescargas & REQUISITOS_DOWNLOAD)
        System.IO.File.Delete(RutaDescargas & TERCEROS_DOWNLOAD)
        System.IO.File.Delete(RutaDescargas & REGISTRO_DOWNLOAD)

        CbOffice2003.Checked = False
        CbOffice2016.Checked = False
        CbOffice2016odt.Checked = False
        CbOffice2016x64.Checked = False
        CbNemo.Checked = False
        CbRequisitos.Checked = False
        CbPuestoNotin.Checked = False
        CbSferen.Checked = False
        CbPasarelaSigno.Checked = False
        CbTerceros.Checked = False
        CbConfiguraNotin.Checked = False
        CbConfiguraWord2016.Checked = False
        CbFineReader.Checked = False
        CbPaquetesFT.Checked = False
        lbProcesandoDescargas.Visible = False

        EnvioMail()

        MessageBox.Show("DESCARGAS FINALIZADAS. Puedes encontrar los Paquetes en " & RutaDescargas, "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)

        BtCopiarhaciaF.Enabled = True
        btTodo.Text = "Marcar todos"
    End Sub
#End Region

    'Mensajes de acción
    Private Sub BtDescargar_MouseDown(sender As Object, e As MouseEventArgs) Handles btDescargar.MouseDown
        lbProcesandoDescargas.Visible = True
    End Sub

    Private Sub BtNotinKubo_MouseDown(sender As Object, e As MouseEventArgs) Handles btNotinKubo.MouseDown
        lbInstalando.Visible = True
    End Sub

    Private Sub LnkNemo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim urlnemo As String = "http://nemo.notin.net/jnemo-latest.exe"
        My.Computer.Clipboard.SetText(urlnemo)
    End Sub


#Region "MARCAR/DESMARCAR TODOS"
    ' Marcar TODOS. Opcion variable Marcar/Desmarcar todos
    Private MarcarTodos As Integer = 0
    Private Sub BtTodo_Click(sender As Object, e As EventArgs) Handles btTodo.Click
        If MarcarTodos = 0 Then
            CbOffice2003.Checked = True
            CbOffice2016.Checked = False
            CbOffice2016odt.Checked = True
            CbOffice2016x64.Checked = False
            CbNemo.Checked = True
            CbRequisitos.Checked = True
            CbPuestoNotin.Checked = True
            CbSferen.Checked = True
            CbPasarelaSigno.Checked = True
            CbTerceros.Checked = True
            CbPaquetesFT.Checked = True
            btTodo.Text = "Desmarcar"
            'sumar uno a la variable
            MarcarTodos = 1
        ElseIf MarcarTodos = 1 Then
            CbOffice2003.Checked = False
            CbOffice2016.Checked = False
            CbOffice2016odt.Checked = False
            CbOffice2016x64.Checked = False
            CbNemo.Checked = False
            CbRequisitos.Checked = False
            CbPuestoNotin.Checked = False
            CbSferen.Checked = False
            CbPasarelaSigno.Checked = False
            CbTerceros.Checked = False
            CbFineReader.Checked = False
            CbPaquetesFT.Checked = False
            btTodo.Text = "Marcar todos"
            MarcarTodos = 0
        End If
    End Sub

    'Autochequear Configuradores Notin y Word 2016 <> Office 2016odt
    Private Sub CbOffice2003_CheckedChanged(sender As Object, e As EventArgs) Handles CbOffice2003.CheckedChanged
        CbConfiguraNotin.CheckState = CbOffice2003.CheckState
        CalcularTamanoDescarga(493, CbOffice2003.Checked)
    End Sub

    Private Sub CbOffice2016_CheckedChanged(sender As Object, e As EventArgs) Handles CbOffice2016.CheckedChanged
        CalcularTamanoDescarga(705, CbOffice2016.Checked)
        CbConfiguraWord2016.CheckState = CbOffice2016.CheckState
        CbOffice2016odt.Checked = False
        CbOffice2016x64.Checked = False
    End Sub

    Private Sub CbOffice2016odt_CheckedChanged(sender As Object, e As EventArgs) Handles CbOffice2016odt.CheckedChanged
        CalcularTamanoDescarga(705, CbOffice2016odt.Checked)
        CbConfiguraWord2016.CheckState = CbOffice2016odt.CheckState
        CbOffice2016.Checked = False
        CbOffice2016x64.Checked = False
    End Sub
    Private Sub CbOffice2016x64_CheckedChanged(sender As Object, e As EventArgs) Handles CbOffice2016x64.CheckedChanged
        CalcularTamanoDescarga(824, CbOffice2016x64.Checked)
        CbConfiguraWord2016x64.CheckState = CbOffice2016x64.CheckState
        CbOffice2016.Checked = False
        CbOffice2016odt.Checked = False
    End Sub

#End Region

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles btSalir.Click

        'Limpieza de ficheros temporales para instalaciones por ejemplo.
        'TODO revisar si hay que limpiar mas archivos.
        'Try
        '    File.Delete(RutaDescargas & "Requisitos\Framework35.bat")
        '    File.Delete(RutaDescargas & "odbc32.bat")
        '    File.Delete(RutaDescargas & "Registro\msoutl.bat")
        '    File.Delete(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
        '    File.Delete(RutaDescargas & "Registro\unidadfword.bat")
        '    File.Delete(RutaDescargas & "primerreinicio.bat")
        '    File.Delete(RutaDescargas & "reiniciodirectivas.bat")
        'Catch ex As Exception
        'End Try
        'Me.Dispose()
        Me.Close()
    End Sub

    Private Sub BtSalir_MouseDown(sender As Object, e As MouseEventArgs) Handles btSalir.MouseDown
        btSalir.BackColor = SystemColors.ControlLightLight
    End Sub

    'Control de registro de instalación
    Public Shared Sub RegistroInstalacion(ByVal mensajelog As String)
        File.AppendAllText("C:\TEMP\InstaladorKubo\RegistroInstalacion.txt", DateTime.Now.Hour & ":" & DateTime.Now.Minute & " - " & mensajelog & vbCrLf)
    End Sub

    'TODO añadir instalación para Software de terceros usando Choco. Por ejemplo paquetes de JAVA o Acrobat Reader


#Region "COMIENZO DE INSTALACION DE PAQUETES NOTIN+KUBO. COMPROBACIONES INICIALES."

    Private Sub BtNotinKubo_Click(sender As Object, e As EventArgs) Handles btNotinKubo.Click
        RegistroInstalacion("=== COMIENZO INSTALACIONES NOTIN-KUBO ===")

        'Comprobar si se ha efectuado alguna descarga
        Dim comienzo = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "COMIENZO", "2")
        Dim rutaenf = cIniArray.IniGet(instaladorkuboini, "PAQUETES", "TRAERDEF", "2")
        If comienzo = 2 AndAlso rutaenf = 2 Then
            MessageBox.Show("Descarga o Trae los Paquetes antes de comenzar las Instalaciones.", "¿Descargaste los paquetes?", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            lbInstalando.Visible = False

            Exit Sub
        End If

        Dim claveinift = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "REGFT", "2")
        If claveinift = 2 Then
            If File.Exists(RutaDescargas & "Registro\FTComoAdministrador.reg") = False Then
                Directory.CreateDirectory(RutaDescargas & "Registro")
                My.Computer.Network.DownloadFile(PuestoNotin & "FTComoAdministrador.reg", RutaDescargas & "Registro\FTComoAdministrador.reg", "juanjo", "Palomeras24", False, 60000, True)
            End If
            Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "1")

            'Deshabilitar SMARTSCREEN. Problemas tras el reinicio en ClickOnce
            Try
                My.Computer.Network.DownloadFile(PuestoNotin & "SmartScreen.reg", RutaDescargas & "SmartScreen.reg", "juanjo", "Palomeras24", False, 10000, True)
            Catch ex As Exception
                RegistroInstalacion("No se pudo obtener SmartScreen REG del FTP: " & ex.Message & ".")
            End Try

            Try
                Process.Start("regedit.exe", "/s " & RutaDescargas & "SmartScreen.reg")
                RegistroInstalacion("Deshabilitar SmartScreen. Permitir ejecución en entornos con problemas ClickOnce.")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                RegistroInstalacion("SmartScreen no se pudo importar: " & ex.Message)
            End Try
            PbInstalaciones.Visible = True
            Dim p As Integer
            While p < 6
                p = p + 1
                Threading.Thread.Sleep(600)
                PbInstalaciones.PerformStep()
            End While

            Dim claveregft As DialogResult
            claveregft = MessageBox.Show("Se importaron Claves de Registro y será recomendable REINICIAR antes de proceder con la Instalación. ¿Realizamos la preparación inicial?", "Importar Claves y Reiniciar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If claveregft = DialogResult.Yes Then

                MessageBox.Show("REINICIO NECESARIO. Guarda tu trabajo.", "Reinicio inicial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dim reinicio As String = "shutdown /r /f /t 0"
                File.WriteAllText(RutaDescargas & "primerreinicio.bat", reinicio)
                RegistroInstalacion("Procedemos a Reiniciar el equipo para la preparación inicial.")
                RunAsAdmin(RutaDescargas & "primerreinicio.bat")
                Exit Sub
            ElseIf claveregft = DialogResult.No Then
                MessageBox.Show("Continuamos con el resto de instalaciones.", "Reinicio cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "0")
                RegistroInstalacion("ADVERTENCIA: No se ejecutó el Reinicio tras importar las Claves de Registro.")
                lbInstalando.Visible = False
            End If
        ElseIf claveinift = 1 OrElse claveinift = 0 Then
        End If

        'Mientras unidad F no valida y usuario pulse reintentar

        Dim QueHacerF As DialogResult
        While UnidadF() = False
            QueHacerF = MessageBox.Show("Unidad F no conectada. Habrá procesos que no se podrán completar y se omitirán.", "Advertencia Unidad F", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)
            If QueHacerF = DialogResult.Abort Then
                lbInstalando.Visible = False
                Exit Sub
            ElseIf QueHacerF = DialogResult.Ignore Then
                RegistroInstalacion("ADVERTENCIA: Comenzaste la Instalación sin Conectar la Unidad F.")
                Exit While
            End If
        End While
        ' CONTROLAR PULSAR ABORTAR


        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
        End If

        obtenerunrar()

        Dim NotinSiNo As Integer = Nothing

        Dim EjecutableAccess As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE11\MSACCESS.EXE")

        If EjecutableAccess = False Then
            RegistroInstalacion("No se encontro Office 2003 instalado. Se procede a la instalación automatizada.")
            InstalarNotinNet()

        Else
            NotinSiNo = MessageBox.Show("Posible instalación existente de NOTIN .NET (Access 2003). ¿Ejecutar instalación Office 2003?", "Instalación Office 2003", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If NotinSiNo = DialogResult.Yes Then
                RegistroInstalacion("Se encontró el paquete Office pero se procede a su reinstalación.")
                InstalarNotinNet()
            Else
                RegistroInstalacion("Omitida instalación Office 2003. Pasamos a la instalación de los Pre-Requisitos.")
                InstalarRequisitosNet()
            End If
        End If

    End Sub


    Private Sub InstalarNotinNet()

        'Claves Registro
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ConfigAccess.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\VentanasSigno.reg", AppWinStyle.Hide, True)
        'Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ExclusionDefender.reg", AppWinStyle.Hide, True)
        RegistroInstalacion("Importadas Claves Registro para Notin.")

        If File.Exists(RutaDescargas & "PuestoNotinC.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PuestoNotinC.exe " & "C:\", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "ScanImg_Beta_FT.exe", AppWinStyle.Hide, False)
            'Shell("cmd.exe /c " & "C:\Notawin.Net\FT.exe /actualizaciones", AppWinStyle.Hide, False)
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete PuestoNotinC. Se suprimió su instalación.")
        End If

        If File.Exists(RutaDescargas & "Office2003.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2003.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            'Setup MST que personaliza la instalación de Office 2003
            File.Copy(RutaDescargas & "Setup.mst", RutaDescargas & "Office2003\Setup.mst", True)
            '  Shell("C:\WINDOWS\system32\notepad.exe " & RutaDescargas & "Office2003\NSERIE.TXT", AppWinStyle.NormalFocus, False)
            'Esperamos 5 segundos a que se complete la copia.
            Threading.Thread.Sleep(3000)

            Shell("cmd.exe /C " & RutaDescargas & "Office2003\setup.exe TRANSFORMS=" & RutaDescargas & "Office2003\Setup.mst /qb-", AppWinStyle.Hide, True)
            RegistroInstalacion("Realizada instalación desatendida de Office 2003.")
            ' Shell("cmd.exe /C taskkill /f /im notepad.exe", AppWinStyle.Hide, False)

            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\Office2003SP3-KB923618-FullFile-ESN.exe" & """" & " /q", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\MSACCESS.msp" & """" & " /passive", AppWinStyle.Hide, True)
            RegistroInstalacion("Instalados SP3 y Parche Access para Office 2003.")
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete OFFICE2003.EXE ¿Seguro que lo descargaste?")
        End If


        'Copiar Referencia Outlook
        Try
            Dim msoutlxcopy As String = "xcopy /F /Y /C "
            Dim msoutlorigen As String = RutaDescargas & "Registro\MSOUTL.OLB "
            Dim msoutldestino As String = " ""C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\"" "
            File.WriteAllText(RutaDescargas & "Registro\msoutl.bat", msoutlxcopy & msoutlorigen & msoutldestino)

            RunAsAdmin(RutaDescargas & "Registro\msoutl.bat")
            RegistroInstalacion("Copiada libreria Outlook necesaria para Notin.")
        Catch ex As Exception
            RegistroInstalacion("ERROR MSOUTLB: " & ex.Message)
        End Try
        'Try
        '    File.Copy(RutaDescargas & "Registro\MSOUTL.OLB", "C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\MSOUTL.OLB", True)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

        InstalarRequisitosNet()
    End Sub

    Private Sub InstalarRequisitosNet()
        Try
            Dim requisitosini = cIniArray.IniGet(instaladorkuboini, "REQUISITOS", "NET", "2")

            If requisitosini = "2" Then
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\KryptonSuite300.msi /passive", AppWinStyle.Hide, True)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\Office2003PrimaryInterop.msi /passive", AppWinStyle.Hide, True)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2005.exe /q", AppWinStyle.Hide, True)
                Threading.Thread.Sleep(15000)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2015.exe /q", AppWinStyle.Hide, True)
                Threading.Thread.Sleep(15000)
                cIniArray.IniWrite(instaladorkuboini, "REQUISITOS", "NET", "1")
                RegistroInstalacion("Instalados Pre-Requisitos .Net")
            Else
                RegistroInstalacion("Pre-Requisitos no instalados. Se detectó instalación previa.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR Requisitos Net: " & ex.Message)
        End Try
        InstalarWord2016()
    End Sub

    Private Sub InstalarWord2016()
        Dim EjecutableWord As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE16\WINWORD.EXE") OrElse File.Exists("C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE")

        If File.Exists(RutaDescargas & "Office2016.exe") Then
            If EjecutableWord = False Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                RegistroInstalacion("No se encuentra instalación previa de Office 2016. Procedemos a realizarla.")
                'Que office instalamos??
                Dim Office2016odt = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "OFFICE2016ODT", "2")
                Dim office2016per = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "OFFICE2016", "2")
                If Office2016odt = 1 Then
                    Try
                        'My.Computer.Network.DownloadFile(PuestoNotin & "setup2016.MSP", RutaDescargas & "Office2016\setup2016.MSP", "juanjo", "Palomeras24", False, 60000, True)
                        File.Copy(RutaDescargas & "setup2016.MSP", RutaDescargas & "Office2016\setup2016.MSP", True)
                    Catch ex As Exception
                        MessageBox.Show("Error al copiar fichero MSP. Revisa que el fichero exista en " & RutaDescargas & "Office2016", "Error Setup MSP", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RegistroInstalacion("Setup2016.MSP: " & ex.Message)
                    End Try
                    Threading.Thread.Sleep(3000)
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE /adminfile setup2016.MSP", AppWinStyle.Hide, True)
                    RegistroInstalacion("Se procedió a realizar la instalación Desatendida de Office 2016.")

                ElseIf office2016per = 1 Then
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
                    RegistroInstalacion("Se procedió a realizar la instalación Personalizada de Office 2016.")
                Else
                    Try
                        'My.Computer.Network.DownloadFile(PuestoNotin & "setup2016.MSP", RutaDescargas & "Office2016\setup2016.MSP", "juanjo", "Palomeras24", False, 60000, True)
                        File.Copy(RutaDescargas & "setup2016.MSP", RutaDescargas & "Office2016\setup2016.MSP", True)
                    Catch ex As Exception
                        MessageBox.Show("Error al copiar fichero MSP. Revisa que el fichero exista en " & RutaDescargas & "Office2016", "Error Setup MSP", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RegistroInstalacion("Setup2016.MSP: " & ex.Message)
                    End Try
                    Threading.Thread.Sleep(3000)
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE /adminfile setup2016.MSP", AppWinStyle.Hide, True)
                    RegistroInstalacion("ADVERTENCIA: No se determinó la preferencia de instalación para Office 2016. Por defecto realizamos la ODT.")
                End If
            Else
                Dim WordSiNo = MessageBox.Show("Se ha detectado una posible instalación de WORD 2016. ¿Ejecutar instalación Office 2016 Personalizable?", "Instalación Office 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If WordSiNo = 6 Then
                    Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
                    RegistroInstalacion("Se detectó una instalación previa de Office2016. Se ofrece la instalación Personalizada.")
                End If
            End If
        Else
            RegistroInstalacion("ADVERTENCIA: No se encontró el Paquete OFFICE2016.EXE. Buscando Paquete de Office X64.")
        End If

        EjecutableNotinNet()
    End Sub

    Private Sub EjecutableNotinNet()

        If File.Exists("C:\Notawin.Net\notin.ini") Then
            cIniArray.IniWrite("C:\Notawin.Net\notin.ini", "Sistema", "PlataformaAddin", "32")
            RegistroInstalacion("PlataformaAddin=32 escrito en el INI local de C:\Notawin.Net por si existía versión =64")
        Else
            RegistroInstalacion("ERROR: PlataformaAddin=32 no se pudo escribir en el INI Local. Revisa su existencia y permisos.")
        End If

        If UnidadF() = True Then
            Try
                Directory.CreateDirectory(RutaDescargas & "NotinNet")
                File.Copy("F:\NOTAWIN.NET\NotinNetInstaller.exe", RutaDescargas & "NotinNet\NotinNetInstaller.exe", True)
                RegistroInstalacion("NotinNetInstaller copiado correctamente desde F:\Notawin.Net\ para su ejecución.")
            Catch ex As Exception
                RegistroInstalacion("NotinNetInstaller: No se pudo obtener de F:\Notawin.Net\ se procede a su decarga desde static.unidata")
            End Try




            '    Dim notinnetinstaller As New FileInfo(RutaDescargas & "NotinNet\NotinNetInstaller.exe")
            'Dim Lengthnotinnetinstallerexe As Long = notinnetinstaller.Length
            'If notinnetinstaller.Length < "100000000" Then

            If File.Exists(RutaDescargas & "NotinNet\NotinNetInstaller.exe") = False Then
                'RegistroInstalacion("NotinNetInstaller no encontrado. Se procede a su descarga.")
                Try
                    Directory.CreateDirectory(RutaDescargas & "NotinNet")
                    Dim urlnotinnetestable As String = "http://static.unidata.es/estable/NotinNetInstaller.exe"
                    Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress " & urlnotinnetestable & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
                Catch ex As Exception
                    RegistroInstalacion("NotinNetInstaller: No se pudo obtener desde su url de descarga. Seguirán errores de Addins.")
                End Try
            End If
        End If


        'Una vez obtenido procedemos a ejecutar NotinNetInstaller
        Try
            Dim pnotinnet As New ProcessStartInfo()
            pnotinnet.FileName = RutaDescargas & "\NotinNet\NotinNetInstaller.exe"
            Dim notinnet As Process = Process.Start(pnotinnet)
            'notintaskpane.WaitForInputIdle()
            notinnet.WaitForExit()
            RegistroInstalacion("Ejecutado instalador NotinNetInstaller.exe desde " & RutaDescargas & "NotinNet\")
        Catch ex As Exception
            RegistroInstalacion("NotinNetInstaller: " & ex.Message)
        End Try
        'Ademas me traigo las Plantillas y el MDE
        Try
            File.Copy("F:\NOTIN8.mde", "C:\Notawin.Net\notin8.mde", True)
        Catch ex As Exception
            RegistroInstalacion("ERROR: Copiando Notin8.mde " & ex.Message)
        End Try
        Try
            File.Copy("F:\NOTIN\PLANTILLAS\NORMAL.DOTM", "C:\PLANTILLAS\NORMAL.DOTM", True)
        Catch ex As Exception
            RegistroInstalacion("ERROR: Copiando Normal.dotm " & ex.Message)
            'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ObtenerVersionNet()
        ConfigurarWord2016()
    End Sub

    Private Sub ConfigurarWord2016()
        If File.Exists(RutaDescargas & "ConfWord2016.rar") Then

            If UnidadF() = True Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "ConfWord2016.rar " & RutaDescargas & "Office2016\", AppWinStyle.Hide, True)
                'Dim ConfigurarWord = MessageBox.Show("¿Configuramos Word 2016?", "Configurar Word 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                'If ConfigurarWord = DialogResult.Yes Then
                Dim configurawordini = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "2")

                If configurawordini = "2" Then
                    Try
                        Process.Start("C:\Program Files (x86)\Humano Software\Notin\Compatibilidad\ReferNet.exe")
                        Threading.Thread.Sleep(5000)
                    Catch ex As Exception
                        'MessageBox.Show(ex.Message, "Revisa Instalacion de NotinNET. Continuamos.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                        RegistroInstalacion("ReferNet: " & ex.Message)
                    End Try

                    'Instalacion de los Addins. Hay que forzarlo.
                    Try
                        'Crear una nueva estructura ProcessStartInfo.
                        Dim pInfoaddin As New ProcessStartInfo()
                        'Establecer el miembro de un nombre de archivo de pinfo como Eula.txt en la carpeta de sistema.
                        pInfoaddin.FileName = "C:\Program Files (x86)\Humano Software\Notin\Addins\NotinAddin\NotinAddinInstaller.exe"
                        'Ejecutar el proceso.
                        Dim notinaddin As Process = Process.Start(pInfoaddin)
                        'Esperar a que la ventana de proceso complete la carga.
                        'notinaddin.WaitForInputIdle()
                        'Esperar a que el proceso termine.
                        notinaddin.WaitForExit()
                        'Continuar con el código.
                    Catch ex As Exception
                        RegistroInstalacion("ERROR NotinAddin: " & ex.Message)
                        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                    End Try
                    Try
                        Dim pInfotaskpane As New ProcessStartInfo()
                        pInfotaskpane.FileName = "C:\Program Files (x86)\Humano Software\Notin\Addins\NotinTaskPane\NotinTaskPaneInstaller.exe"
                        Dim notintaskpane As Process = Process.Start(pInfotaskpane)
                        'notintaskpane.WaitForInputIdle()
                        notintaskpane.WaitForExit()
                    Catch ex As Exception
                        RegistroInstalacion("ERROR NotinTaskPane: " & ex.Message)
                        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                    End Try
                    'Shell("cmd.exe /C " & RutaDescargas & "ConfiguraWord2016.exe", AppWinStyle.NormalFocus, True)
                    'RunAsAdmin(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
                    Process.Start(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
                    'Threading.Thread.Sleep(10000)

                    Try
                        Dim expedientes As String = cIniArray.IniGet("F:\WINDOWS\NNotin.ini", "Expedientes", "Ruta", "\\SERVIDOR\I\")
                        expedientes = expedientes.Remove(0, 2)
                        Dim unidadi = expedientes.LastIndexOf("\I")
                        expedientes = expedientes.Substring(0, unidadi)

                        cIniArray.IniWrite(instaladorkuboini, "RUTAS", "EXPEDIENTES", expedientes)

                        Directory.CreateDirectory(RutaDescargas & "Registro")
                        Dim claveregistroservidor As String = """" & "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Office\16.0\Word\Security\Trusted Locations\Location3" & """" & " /v Path /t REG_SZ /d \\" & expedientes & "\F" & " /f"
                        File.WriteAllText(RutaDescargas & "Registro\unidadfword.bat", "reg add ")
                        File.AppendAllText(RutaDescargas & "Registro\unidadfword.bat", claveregistroservidor)

                        Process.Start("regedit", "/s " & RutaDescargas & "Office2016\ConfWord2016\w16recopregjj.reg")
                        RunAsAdmin(RutaDescargas & "Registro\unidadfword.bat")
                        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "1")
                    Catch ex As Exception
                        RegistroInstalacion("Ruta Expedientes UnidadF-WORD y W16REG: " & ex.Message)
                    End Try

                End If
            Else
                MessageBox.Show("Unidad F desconectada. No se puede configurar Word 2016.", "Configura WORD 2016", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
            End If
        Else
            RegistroInstalacion("ERROR: No se pudo Configurar WORD 2016. No se encontró la descarga de la Utilidad.")
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
        End If
        'TODO preparar esto también para la versión de 64bits

        SoftwareAncert()
        'KMSPico()
    End Sub


    Private Sub SoftwareAncert()
        If File.Exists(RutaDescargas & "SFeren-2.8.exe") OrElse File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
            Dim Ancert As Integer = Nothing
            Ancert = MessageBox.Show("¿Instalar Software Ancert?", "Sferen y Pasarela", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Ancert = 6 Then
                If File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
                    Shell("cmd.exe /c " & RutaDescargas & "SFeren-2.8.exe", AppWinStyle.Hide, True)
                    RegistroInstalacion("SFEREN: Lanzado Instalador.")
                Else
                    RegistroInstalacion("ADVERTENCIA: Paquete Sferen no encontrado. No se instalará.")
                End If
                If File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
                    Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PasarelaSigno.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                    Shell("cmd.exe /c " & RutaDescargas & "PasarelaSigno\setup.exe", AppWinStyle.Hide, True)
                    RegistroInstalacion("PASARELA: Lanzado Instalador.")
                Else
                    RegistroInstalacion("ADVERTENCIA: Instalable PasarelaSigno no encontrado. No se instalará.")
                End If
            End If
        Else
            RegistroInstalacion("Software ANCERT no descargado. Se omite su instalación.")
        End If
        JNemo()
    End Sub

    Private Sub JNemo()
        If Directory.Exists("c:\Program Files (x86)\Java") = False Then
            'Descarga de JAVA 1.8.171
            Directory.CreateDirectory(RutaDescargas & "Software")
            Dim urljava As String = "http://javadl.oracle.com/webapps/download/AutoDL?BundleId=233170_512cd62ec5174c3487ac17c61aaa89e8"
            Dim wgetjava As String = "wget.exe -q --show-progress -t 5 -c "
            Shell("cmd.exe /c " & RutaDescargas & wgetjava & urljava & " -O " & RutaDescargas & "Software\java8.exe", AppWinStyle.Hide, True)
            Try
                Dim pinstalajava As New ProcessStartInfo()
                pinstalajava.FileName = RutaDescargas & "Software/java8.exe"
                pinstalajava.Arguments = "/s WEB_JAVA_SECURITY_LEVEL=M"
                Dim instalajava As Process = Process.Start(pinstalajava)
                instalajava.WaitForExit()
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "JAVA8", "1")
                BtJava.BackColor = Color.PaleGreen
                RegistroInstalacion("Instalada última versión de JAVA para jNemo.")
            Catch ex As Exception
                MessageBox.Show("No se pudo instalar JAVA. Instálalo manualmente al terminar. Puedes usar este mismo Instalador.", "Error Java", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR Java jNemo: " & ex.Message)
            End Try
        End If

        If File.Exists(RutaDescargas & "jnemo-latest.exe") Then
            If File.Exists("c:\Program Files (x86)\jNemo\jNemo.exe") = False Then
                Dim instalajnemo As New Process
                instalajnemo.StartInfo.FileName = RutaDescargas & "jnemo-latest.exe"
                'MiProceso.StartInfo.Arguments = "1664"
                instalajnemo.Start() 'iniciar el proceso
                'MiProceso.Kill()
                'MiProceso.Dispose()
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "JNEMO", "1")
            End If
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ADVERTENCIA: Se encontró el ejecutable jNemo. Se omite su instalación.")
        End If
        FT()
    End Sub

    Private Sub FT()
        Try
            ' Shell("cmd.exe /c " & "C:\Notawin.Net\FT.exe /actualizaciones", AppWinStyle.Hide, False)
            Process.Start("C:\NOTAWIN.NET\FT.EXE", "/actualizaciones")
            RegistroInstalacion("Instalando Paquetes de FT.")
        Catch ex As Exception
            RegistroInstalacion("Paquetes FT. No se pudieron instalar: " & ex.Message)
        End Try

        If File.Exists(RutaDescargas & "PaquetesFT.rar") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PaquetesFT.rar " & RutaDescargas, AppWinStyle.NormalFocus, True)

            Try
                Process.Start(RutaDescargas & "PaquetesFT\BarCodex.exe")
                Process.Start(RutaDescargas & "PaquetesFT\catastrowsclient-setup.exe")
                Process.Start(RutaDescargas & "PaquetesFT\NotinScrap-setup.exe")
                RegistroInstalacion("Instalados Paquetes esenciales de FT.")
            Catch ex As Exception
                RegistroInstalacion("ERROR PaquetesFT: " & ex.Message)
            End Try

            'Mailer-Setup
            Try
                Dim mailerxcopy As String = "xcopy /F /Y /C "
                Dim mailerorigen As String = RutaDescargas & "PaquetesFT\MailerCOM.dll "
                If Directory.Exists("C:\Windows\SysWOW64") Then
                    Dim mailerdestino As String = " ""C:\Windows\SysWOW64\"" "
                    File.WriteAllText(RutaDescargas & "PaquetesFT\mailersetup.bat", mailerxcopy & mailerorigen & mailerdestino)
                    RegistroInstalacion("Mailer-Setup: copiada Referencia en Sistema de 64bits.")
                ElseIf Directory.Exists("C:\Windows\System32") Then
                    Dim mailerdestino As String = " ""C:\Windows\System32\"" "
                    RegistroInstalacion("Mailer-Setup: copiada Referencia en Sistema de 32bits.")
                    File.WriteAllText(RutaDescargas & "PaquetesFT\mailersetup.bat", mailerxcopy & mailerorigen & mailerdestino)
                Else
                    RegistroInstalacion("ERROR Paquete Mailer-Setup: No he podido determinar Sistema 32/64bits.")
                End If

                Try
                    RunAsAdmin(RutaDescargas & "PaquetesFT\mailersetup.bat")
                    RegistroInstalacion("DLL MailerCOM Registrada correctamente.")
                Catch ex As Exception
                    RegistroInstalacion("ERROR DLL Mailer-Setup: " & ex.Message)
                End Try
            Catch ex As Exception
            End Try
        Else
            RegistroInstalacion("PaquetesFT no descargado. Se omite su instalación.")
        End If

        AccesosDirectosEscritorio()
    End Sub

    Private Sub AccesosDirectosEscritorio()
        Dim Escritorio As String = """" & My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & """"
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe e -y " & RutaDescargas & "AccesosDirectos.exe " & Escritorio, AppWinStyle.Hide, True)
        RegistroInstalacion("Creados Accesos Directos en el Escritorio. Ruta Escritorio: " & Escritorio)
        Try
            File.Delete("C:\Users\Public\Desktop\Krypton Explorer.lnk")
        Catch ex As Exception
        End Try
        'AbreExcel()

        lbInstalando.Visible = False
        MessageBox.Show("INSTALACIONES TERMINADAS. Se recomienda REINICIAR el equipo. Consulta el Registro de Instalación para más detalles.", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        RegistroInstalacion("=== FINALIZADAS INSTALACIONES NOTIN+KUBO ===")

        PbInstalaciones.Visible = False
    End Sub



    'Private Sub AbreExcel()
    '    'TODO establecer asociacion de archivos.
    '    Try
    '        My.Computer.Network.DownloadFile(PuestoNotin & "AbreExcel.exe", RutaDescargas & "AbreExcel.exe", "juanjo", "Palomeras24", False, 20000, False)
    '        File.Copy(RutaDescargas & "AbreExcel.exe", "C:\Notawin.Net\AbreExcel.exe", True)
    '    Catch ex As Exception

    '    End Try
    'btNotinKubo.ForeColor = Color.YellowGreen
    'End Sub
#End Region



#Region "Tooltips"
    Private Sub Tooltips()
        'Las propiedades tambien se pueden definir desde el explorador de objetos
        tlpUnidadF.ToolTipIcon = ToolTipIcon.Info
        tlpUnidadF.ToolTipTitle = "Conexión Unidad de Red"
        tlpUnidadF.SetToolTip(lbUnidadF, "Chequea fichero F:\Windows\NNotin.ini")
        tlpUnidadF.IsBalloon = True

        tlpDescargas.ToolTipIcon = ToolTipIcon.Info
        tlpDescargas.ToolTipTitle = "Leyenda Descargas"
        tlpDescargas.SetToolTip(GroupBox1, "Gris: Descarga no realizada" + vbCrLf & "Verde: Descarga Completada" + vbCrLf & "Rojo: Descarga Incompleta")
        tlpDescargas.IsBalloon = True

        tlpOffice2016odt.ToolTipIcon = ToolTipIcon.Info
        tlpOffice2016odt.ToolTipTitle = "Office 2016 DESATENDIDO"
        tlpOffice2016odt.SetToolTip(CbOffice2016odt, "Descarga el paquete Office 2016 con instalación automatizada.")
        tlpOffice2016odt.IsBalloon = True

        tlpOffice2016.ToolTipTitle = "Paquete Office 2016 PERSONALIZABLE"
        tlpOffice2016.SetToolTip(CbOffice2016, "Descarga el paquete Office con instalación personalizable")

        TlpOffice2016x64.ToolTipTitle = "Paquete Office 2016 x64 DESATENTIDO"
        TlpOffice2016x64.SetToolTip(CbOffice2016x64, "Descarga el paquete Office 2016 x64. Versiones Beta Nexus.")

        tlpTerceros.ToolTipIcon = ToolTipIcon.Info
        tlpTerceros.ToolTipTitle = "Software recomendado"
        tlpTerceros.SetToolTip(CbTerceros, "Incluye Adobe Reader DC, FileZilla, Google Chrome y Notepad++")
        tlpTerceros.IsBalloon = True

        tlpNotinKubo.ToolTipTitle = "Comienza Instalaciones Notin y Word 2016 + Kubo"
        tlpNotinKubo.SetToolTip(btNotinKubo, "Preguntará por cada Paquete descargado. Sialgún software se encuentra instalado preguntará si se desea realizar la reinstalación.")

        TlpNotinWord2003.ToolTipTitle = "Comienza Instalación Notin y Word 2003"
        TlpNotinWord2003.SetToolTip(BtNotinWord2003, "Instala/Configura Notin y Word 2003. Además instalará Outlook, Excel..2016 si lo descargaste previamente.")

        'tlpAncert.ToolTipTitle = "URL Notariado"
        'tlpAncert.SetToolTip(lblAncert, "Acceder a url soporte.notariado.org")

        tlpOffice2003.ToolTipTitle = "Office 2003 DESATENDIDO"
        tlpOffice2003.SetToolTip(CbOffice2003, "Instalación automatizada ACCESS y librerías Outlook.")

        TlpRutaDescargas.ToolTipTitle = "Cambiar Carpeta Descargas"
        TlpRutaDescargas.SetToolTip(btDirDescargas, "Permite seleccionar una carpeta diferente para realizar las descargas.")

        TlpComenzarDescargas.ToolTipTitle = "Comenzar proceso Descargas"
        TlpComenzarDescargas.SetToolTip(btDescargar, "Se descargarán/resumirán los paquetes seleccionados.")

        TlpJava.ToolTipTitle = "Instalación Desatendida JAVA 8"
        TlpJava.SetToolTip(BtJava, "Instalación silenciosa de Java. No requiere intervención del usuario")

        TlpUac.SetToolTip(BtUac, "Exclusiones Windows Defender y Control Cuentas Usuario")

        TlpCopiarServidor.ToolTipTitle = "Copia Paquetes al Servidor"
        TlpCopiarServidor.SetToolTip(BtCopiarhaciaF, "Copia los Paquetes descargados al Servidor para poder recogerlos posteriormente en otro equipo usando la opción siguiente.")

        TlpTraerServidor.ToolTipTitle = "Recoge Paquetes desde el Servidor"
        TlpTraerServidor.SetToolTip(BtTraerdeF, "Trae hacia la ruta Local los Paquetes copiados previamente al Servidor para así no tener que obtenerlos de Internet.")

        TlpConfigWord2016.ToolTipTitle = "Configurador independiente WORD 2016"
        TlpConfigWord2016.SetToolTip(BtConfiguraWord2016, "Instala Notin Addin y TaskPane para Word 2016. Se ha añadido soporte para Word X64.")

        TlpKmspico.ToolTipTitle = "Instalación de KMSpico 10.2 FINAL"
        TlpKmspico.SetToolTip(BtKmsPico, "Descarga, descomprime e Instala KMSpico 10. Revisa antes las Excepciones del Antivirus.")

        TlpReconectarF.ToolTipTitle = "Reconectar Unidad F"
        TlpReconectarF.SetToolTip(BtReconectar, "Chequea la existencia Unidad F. Usa esto si la conectaste una vez arrancado el Instalador.")

        TlpDirectivas.ToolTipTitle = "Directivas de Windows"
        TlpDirectivas.SetToolTip(BtDirectivas, "Aplica las Directivas de Windows. Para más información lee la hoja de Requisitos.")

        TlpExplorerDescargas.ToolTipTitle = "Explorar carpeta Descargas"
        TlpExplorerDescargas.SetToolTip(BtExplorarRutas, "Muestra en el explorador de archivos la ruta " & RutaDescargas)

        TlpSistema.ToolTipTitle = "Información Sistema Operativo"
        TlpSistema.SetToolTip(GroupBox3, "Si alguna característica puede no cumplir los Requisitos se mostrará de color rojo.")

        TlpFramework.ToolTipTitle = "Instalación Framework 3.5"
        TlpFramework.SetToolTip(BtFramework, "Se procederá a la instalación del Paquete Framework 3.5 necesario para .Net. Se recomienda Reinciar tras su instalación.")

        TlpTuemail.ToolTipTitle = "Indica tu email para recibir un aviso"
        TlpTuemail.SetToolTip(CBoxEmail, "Se te remitirá un email de confirmación cuando finalice el proceso en ejecución.")

        TlpNotinpdf.ToolTipTitle = "Instalador NotinPDF"
        TlpNotinpdf.SetToolTip(BtNotinpdf, "Descargará el paquete NotinPDF y mostrará la ruta de descarga. Su ejecución será manual.")

        TlpRequisitosNotin.ToolTipTitle = "Hoja Requisitos Notin"
        TlpRequisitosNotin.SetToolTip(BtDocRequisitos, "Muestra el documento con los Requisitos para instalar Notin.")

        TlpPaquetesFT.ToolTipTitle = "Paquetes FT esenciales"
        TlpPaquetesFT.SetToolTip(CbPaquetesFT, "Descarga e Instala los Paquetes FT esenciales para el funcionamiento de Notin.")
        TlpPaquetesFT.ToolTipIcon = ToolTipIcon.Info

        TlpISL.ToolTipTitle = "Instalación automatizada ISL AlwaysON"
        TlpISL.SetToolTip(BtISL, "Añade este equipo para su futura conexión desde ISL Light de operador.")

        TlpSQL2014.ToolTipTitle = "Muestra Formulario para Descarga e Instalación de SQL 2014 Business"
        TlpSQL2014.SetToolTip(BtSQL2014, "Accederemos al formulario donde podrás personalizar la instalación de SQL Server 2014 o aplicar el último Update.")

        TlpNotinNetaF.ToolTipTitle = "Copia NotinNetInstaller a F"
        TlpNotinNetaF.SetToolTip(BtNotinNetF, "Copia NotinNetInstaller.exe previamente descargado a F:\Notawin.Net para su distribución al resto de equipos.")

        TlpNotin8.ToolTipTitle = "Descargar Notaría"
        TlpNotin8.SetToolTip(BtNotin8exe, "Descarga la última versión de Notin8.exe. Lo ejecuta y deja en F raíz.")

        TlpBDBlancos.ToolTipTitle = "Bases de Datos BLANCOS"
        TlpBDBlancos.SetToolTip(BtBlancosBD, "Descarga los BD BLANCOS para SQL2014 o superior y muestra la carpeta. No los importa a tu Motor SQL.")

        TlpMigrador.ToolTipTitle = "Ejecuta MigradorNotinSQL"
        TlpMigrador.SetToolTip(BtMigradorSQL, "Descarga la última versión y lo lanza con el parámetro /allowdataloss")

        TlpChoco.ToolTipTitle = "Paquete Chocolatey"
        TlpChoco.SetToolTip(BtChocolatey, "Descarga e Instala la última versión del Manejador de Paquetes Chocolatey para Windows.")

        TlpFocos.ToolTipTitle = "Downgrade versión Cliente RDP"
        TlpFocos.SetToolTip(BtFocos, "Arregla bug versión 10.4 RDP para problema de Focos en Adra.")

        TlpLogMigrador.ToolTipTitle = "Log MigradorNotinSQL"
        TlpLogMigrador.SetToolTip(TbMigradorLog, "Haz clic para visualizar Log completo.")

        TlpConsultaDatosSQL.ToolTipTitle = "Consulta sql Reducir DATOS LDF"
        TlpConsultaDatosSQL.SetToolTip(BtReducirDatos, "Copia la sentencia sql al portapapeles. Después dirígete al SQL Manager para ejecutarla.")

        TlpConsultaTriggers.ToolTipTitle = "Consulta sql Eliminar Triggers (Desencadenadores)"
        TlpConsultaTriggers.SetToolTip(BtTriggers, "Copia la sentencia sql al portapapeles. Después dirígete al SQL Manager para ejecutarla.")

        TlpUrlNemo.ToolTipTitle = "Check copia url descarga jNemo al Portapapeles"
        TlpUrlNemo.SetToolTip(CbNemo, "URL: http://nemo.notin.net/jnemo-latest.exe")

        TlpVisorImagenes.ToolTipTitle = "Visor de Imágenes de Windows"
        TlpVisorImagenes.SetToolTip(BtVisorImagenes, "Habilita selección del Visor de Imágenes en asociación de archivos para Windows 10.")

        TlpConfiguraWordAdra.SetToolTip(BtConfWord2016ADRA, "Distribuye y Ejecuta ConfiguraWord2016 en entorno ADRA.")

        TlpPerfilAdra.SetToolTip(BtLimpiarPerfil, "Permite Limpiar el Perfil de usuario en Adra y gestionar los vínculos hacia aplicaciones (NR). Se irán añadiendo más opciones...")

        TlpDeploy.SetToolTip(BtMigradorDeploy, "Descarga y Ejecuta Migrador forzando Deploy. Aplicando así las Actualizaciones en la BD. Esto no afecta a la bitácora de SQL.")

        TlpNotin8Forzar.ToolTipTitle = "Fuerza requisitos para Instalación del paquete Notin8.exe"
        TlpNotin8Forzar.SetToolTip(BtNotin8exeForzar, "Descarga y realiza y fuerza los requisitos necesarios para que se provoque la Instalación de Notin8.exe y .Net en cualquier entorno. Clic para leer las advertencias.")

        TlpPuestoC.SetToolTip(CbPuestoNotin, "Descarga y descomprime en C raíz carpetas como Notawin.Net, Plantillas, Accesos directos al Escritorio.." & vbCrLf & "Este proceso se realizará tras una instalación Notin-Kubo-Nexus.")
        TlpPuestoC.AutoPopDelay = 10000
        TlpPuestoC.IsBalloon = True

        TlpBetaNetAdra.SetToolTip(CbBetaAdra, "Marcando esta casilla se descargará la última Beta de Net. En caso contrario se forzará la instalación de la versión Estable.")

        TlpBackupNet.SetToolTip(BtBackupNet, "Además de explorar las carpeta de BackupNet se limpiarán las versiones de mas de 15 días.")
    End Sub
#End Region

#Region "ODBC"
    Private Sub BtOdbc_Click(sender As Object, e As EventArgs) Handles BtOdbc.Click
        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
            'Uso la funcion SHARED de la Clase LeerFicherosINI
            Dim nombre_servidor As String = cIniArray.IniGet("F:\WINDOWS\NNOTIN.INI", "NET", "NOMBRESERVIDOR", "SERVIDOR")
            My.Computer.Network.DownloadFile(PuestoNotin & "BDDatosNotinSQL.reg", RutaDescargas & "Registro\BDDatosNotinSQL.reg", "juanjo", "Palomeras24", False, 5000, True)

            If File.Exists("C:\Windows\SysWoW64\odbcconf.exe") Then
                Dim odbc64 As String = "C:\Windows\SysWoW64\odbcconf.exe " & "/A " & "{CONFIGSYSDSN " & """" & "SQL Server" & """" & " " & """" & "DSN=NOTINSQL|Server=" & nombre_servidor & """" & "}"
                File.WriteAllText(RutaDescargas & "odbc32.bat", odbc64 & vbCrLf)
                Dim bddatosnotinsql As String = "regedit.exe /s " & RutaDescargas & "Registro\BDDatosNotinSQL.reg"
                File.WriteAllText(RutaDescargas & "BDDatosNotinSQL.bat", bddatosnotinsql)

                RunAsAdmin(RutaDescargas & "odbc32.bat")
                RunAsAdmin(RutaDescargas & "BDDatosNotinSQL.bat")

                RegistroInstalacion("ODBC NotinSQL Configurado hacia " & nombre_servidor & ".")

                BtOdbc.BackColor = Color.PaleGreen

                MessageBox.Show("NotinSQL configurado hacia: " & nombre_servidor, "ODBC NotinSQL", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Process.Start("C:\Windows\SysWoW64\odbcad32.exe")
                cIniArray.IniWrite(instaladorkuboini, "ODBC", "NOTINSQL", "1")
                cIniArray.IniWrite(instaladorkuboini, "ODBC", "SQLSERVER", nombre_servidor)
                'C:\Windows\SysWoW64\odbcconf.exe /A {CONFIGSYSDSN "SQL Server" "DSN=NOTINSQL|Server=clustersql"}
            ElseIf File.Exists("C:\Windows\System32\odbcconf.exe") Then
                Dim odbc32 As String = "C:\Windows\System32\odbcconf.exe " & "/A " & "{CONFIGSYSDSN " & """" & "SQL Server" & """" & " " & """" & "DSN=NOTINSQL|Server=" & nombre_servidor & """" & "}"
                File.WriteAllText(RutaDescargas & "odbc32.bat", odbc32 & vbCrLf)
                'TODO Comprobar ruta registro Sistema 32bits
                ' Dim bddatosnotinsql As String = "regedit.exe /s " & RutaDescargas & "Registro\BDDatosNotinSQL.reg"
                'File.WriteAllText(RutaDescargas & "BDDatosNotinSQL.bat", bddatosnotinsql)

                RunAsAdmin(RutaDescargas & "odbc32.bat")
                'RunAsAdmin(RutaDescargas & "BDDatosNotinSQL.bat")

                BtOdbc.BackColor = Color.PaleGreen
                RegistroInstalacion("ODBC NotinSQL Configurado hacia " & nombre_servidor & ".")

                MessageBox.Show("NotinSQL configurado hacia: " & nombre_servidor & ". Revisa ODBC creado.", "ODBC NotinSQL", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Process.Start("C:\Windows\System32\odbcad32.exe")


                cIniArray.IniWrite(instaladorkuboini, "ODBC", "NOTINSQL", "1")
                cIniArray.IniWrite(instaladorkuboini, "ODBC", "SQLSERVER", nombre_servidor)
            Else
                MessageBox.Show("No se puede acceder a la utilidad ODBCConf.", "Ejecutable no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No se puede conectar con el Servidor (F:)", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cIniArray.IniWrite(instaladorkuboini, "ODBC", "NOTINSQL", "0")
            BtOdbc.BackColor = Color.LightSalmon
            RegistroInstalacion("ERROR: ODBC NotinSQL NO Configurado. No se pudo leer el fichero NNotin.ini. ¿Unidad F desconectada?")
        End If

    End Sub
#End Region

    'EJECUTAR PROCESOS COMO ADMINISTRADOR
    Public Sub RunAsAdmin(strFileName As String)

        Dim proc As New Process()

        proc.StartInfo.FileName = strFileName
        proc.StartInfo.UseShellExecute = True
        proc.StartInfo.Verb = "runas"
        proc.Start()

    End Sub

    Private Sub BtFramework_Click(sender As Object, e As EventArgs) Handles BtFramework.Click
        Dim framework As String = "DISM /Online /Enable-Feature /FeatureName:NetFx3 /All"
        If File.Exists(RutaDescargas & "Requisitos\Framework35.bat") Then
            File.Delete(RutaDescargas & "Requisitos\Framework35.bat")
        Else
        End If
        Directory.CreateDirectory(RutaDescargas & "Requisitos")
        File.AppendAllText(RutaDescargas & "Requisitos\Framework35.bat", "@echo off" & vbCrLf)
        File.AppendAllText(RutaDescargas & "Requisitos\Framework35.bat", framework)
        'File.AppendAllText(RutaDescargas & "Requisitos\Framework35.bat", "pause")
        'Borro el archivo BAT al SALIR del formulario
        RunAsAdmin(RutaDescargas & "Requisitos\Framework35.bat")

        cIniArray.IniWrite(instaladorkuboini, "REQUISITOS", "FRAMEWORK35", "1")
        BtFramework.BackColor = Color.PaleGreen
        RegistroInstalacion("Instalado FrameWord 3.5 en el Sistema.")

        ' Shell("cmd.exe /c " & """" & "DISM /Online /Enable-Feature /FeatureName:NetFx3 /All" & """", AppWinStyle.NormalFocus, True)
    End Sub

    Private Sub BtDirectivas_Click(sender As Object, e As EventArgs) Handles BtDirectivas.Click
        Directory.CreateDirectory(RutaDescargas & "Directivas")

        PbInstalaciones.Visible = True
        PbInstalaciones.Maximum = 100
        PbInstalaciones.Step = 20
        Threading.Thread.Sleep(2000)
        PbInstalaciones.PerformStep()

        'Desactivar resolución de nombres de multidifusión - Habilitada
        Dim multicast As String = "REG add " & """" & "HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient" & """" & " /v EnableMulticast /t REG_DWORD /d 0 /f"
        File.WriteAllText(RutaDescargas & "Directivas\multidifusion.bat", "cmd /c " & multicast)
        RunAsAdmin(RutaDescargas & "Directivas\multidifusion.bat")

        PbInstalaciones.Step = 25
        Threading.Thread.Sleep(2000)
        PbInstalaciones.PerformStep()


        Dim sinconexion As String = "REG add " & """" & "HKLM\SOFTWARE\Policies\Microsoft\Windows\NetCache" & """" & " /V Enabled /t REG_DWORD /d 0 /f"
        File.WriteAllText(RutaDescargas & "Directivas\sinconexion.bat", sinconexion)
        RunAsAdmin(RutaDescargas & "Directivas\sinconexion.bat")

        PbInstalaciones.Step = 15
        Threading.Thread.Sleep(2000)
        PbInstalaciones.PerformStep()
        'powershell.exe -executionpolicy unrestricted -command .\UAC.ps1

        'No reiniciar automáticamente con usuarios que hayan iniciado sesión en instalaciones de actualizaciones automáticas - Habilitada
        'HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\NoAutoRebootWithLoggedOnUsers: 0x00000001
        'Dim noreiniciarupdate As String = "REG add " & """" & "HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\NoAutoRebootWithLoggedOnUsers" & """" & " /V Enabled /t REG_DWORD /d 1 /f"

        'File.WriteAllText(RutaDescargas & "Directivas\noreiniciarupdate.bat", sinconexion)
        'RunAsAdmin(RutaDescargas & "Directivas\noreiniciarupdate.bat")

        PbInstalaciones.Step = 10
        Threading.Thread.Sleep(2000)
        PbInstalaciones.PerformStep()

        'Enviar LM y NTLM - Contraseñas en Blanco para Consola - No reiniciar Windows Update
        Dim directivasps1 As String = "Set-ItemProperty -Path HKLM:\System\CurrentControlSet\Control\Lsa -Name LmCompatibilityLevel -Value 1" & vbCrLf & "Set-ItemProperty -Path HKLM:\System\CurrentControlSet\Control\Lsa -Name LimitBlankPasswordUse -Value 1" & vbCrLf & "Set-ItemProperty -Path HKLM:\Software\Policies\Microsoft\Windows\WindowsUpdate\AU -Name NoAutoRebootWithLoggedOnUsers -Value 1"
        File.WriteAllText(RutaDescargas & "Directivas\Directivas.ps1", directivasps1)
        File.WriteAllText(RutaDescargas & "Directivas\Directivasps1.bat", "@echo off" & vbCrLf & "powershell.exe -executionpolicy unrestricted -command " & RutaDescargas & "Directivas\Directivas.ps1")
        RunAsAdmin(RutaDescargas & "Directivas\Directivasps1.bat")

        PbInstalaciones.Step = 30
        Threading.Thread.Sleep(2000)
        PbInstalaciones.PerformStep()

        Threading.Thread.Sleep(1000)
        PbInstalaciones.Visible = False

        Dim reiniciodirectivas As DialogResult
        reiniciodirectivas = MessageBox.Show("Se procede a aplicar las Directivas. El equipo solo se reiniciará si es necesario.", "¿Aplicamos Update a Directivas?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If reiniciodirectivas = DialogResult.Yes Then
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "DIRECTIVAS", "1")
            If File.Exists("C:\Windows\SysWOW64\gpupdate.exe") Then
                RegistroInstalacion("Aplicando Directivas usando gpupdate 64bits.")
                Process.Start("C:\Windows\SysWOW64\gpupdate.exe", "/force /boot")
                BtDirectivas.BackColor = Color.PaleGreen
            ElseIf File.Exists("C:\Windows\System32\gpupdate.exe") Then
                RegistroInstalacion("Aplicando Directivas usando gpupdate 32bits.")
                Process.Start("C:\Windows\System32\gpupdate.exe", "/force /boot")
                BtDirectivas.BackColor = Color.PaleGreen
            Else
                RegistroInstalacion("ADVERTENCIA: No pude encontrar gpupdate.exe. Fuerzo reinicio a través de shutdown.")
                Process.Start("shutdown", "/r /f /t 0")
                BtDirectivas.BackColor = Color.PaleGreen
            End If

        ElseIf reiniciodirectivas = DialogResult.No Then
            RegistroInstalacion("ADVERTENCIA: No se ejecutó el Reinicio tras importar las Directivas.")
            BtDirectivas.BackColor = Color.LightSalmon
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "DIRECTIVAS", "1")
        End If


    End Sub


    Private Sub BtExcepJava_Click(sender As Object, e As EventArgs) Handles BtExcepJava.Click
        My.Computer.Network.DownloadFile(PuestoNotin & "Utiles\ExcepcionesJava.bat", RutaDescargas & "Utiles\ExcepcionesJava.bat", "juanjo", "Palomeras24", False, 20000, True)

        RunAsAdmin(RutaDescargas & "Utiles\ExcepcionesJava.bat")
        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "EXCEPJAVA", "1")
        BtExcepJava.BackColor = Color.PaleGreen
        RegistroInstalacion("Añadidas excepciones JAVA.")
    End Sub

    Private Sub LinkInstalador_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("http://instalador.notin.net/publish.htm")
    End Sub

    Private Sub BtJava_Click(sender As Object, e As EventArgs) Handles BtJava.Click
        obtenerwget()

        'Descarga de JAVA 1.8.171
        Directory.CreateDirectory(RutaDescargas & "Software")
        Dim urljava As String = "http://javadl.oracle.com/webapps/download/AutoDL?BundleId=233170_512cd62ec5174c3487ac17c61aaa89e8"
        Dim wgetjava As String = "wget.exe -q --show-progress -t 5 -c "
        Shell("cmd.exe /c " & RutaDescargas & wgetjava & urljava & " -O " & RutaDescargas & "Software\java8.exe", AppWinStyle.Hide, True)
        Dim instalajava As New Process()
        instalajava.StartInfo.FileName = RutaDescargas & "Software/java8.exe"
        instalajava.StartInfo.Arguments = "/s WEB_JAVA_SECURITY_LEVEL=M"
        instalajava.Start() 'iniciar el proceso

        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "JAVA8", "1")
        BtJava.BackColor = Color.PaleGreen
        RegistroInstalacion("Instalada última versión de JAVA.")
        MessageBox.Show("JAVA se encuentra instalándose en segundo plano. Acabará en pocos minutos.", "Instalación desatendida JAVA", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BtUac_Click(sender As Object, e As EventArgs) Handles BtUac.Click
        obtenerwget()

        Directory.CreateDirectory(RutaDescargas & "Utiles")
        Dim wgetuac As String = "wget.exe -q --show-progress --no-check-certificate -t 5 -c https://static.unidata.es/devops/ClientInstaller.exe "
        Shell("cmd.exe /c " & RutaDescargas & wgetuac & "-O " & RutaDescargas & "Utiles\ClientInstaller.exe", AppWinStyle.Hide, True)
        Try
            Process.Start(RutaDescargas & "Utiles\ClientInstaller.exe")
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "UAC", "1")
            BtUac.BackColor = Color.PaleGreen
            RegistroInstalacion("Ejecutado ClientInstaller.exe para deshabilitar UAC y añadir excepciones WindowsDefender.")
        Catch ex As Exception
            RegistroInstalacion("ERROR ClientInstaller: " & ex.Message)
            BtUac.BackColor = Color.LightSalmon
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "UAC", "0")
            Exit Sub
        End Try

        'Shell("cmd.exe /c " & RutaDescargas & wgetuac & "-O " & RutaDescargas & "Utiles\UAC.ps1", AppWinStyle.NormalFocus, True)

        'Dim uacadmin = RutaDescargas & "Utiles\UAC.bat"
        'Dim archivops1 As String = "cmd.exe /k powershell.exe -executionpolicy unrestricted -command " & RutaDescargas & "Utiles\UAC.ps1"
        'File.WriteAllText(RutaDescargas & "Utiles\UAC.bat", archivops1)

        'RunAsAdmin(uacadmin)

    End Sub


    Private Sub BtCopiarhaciaF_Click(sender As Object, e As EventArgs) Handles BtCopiarhaciaF.Click
        If UnidadF() = False Then
            MessageBox.Show("Conecta antes la Unidad de Red F.", "Unidad no disponible", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        obtenerrobocopy()
        Dim notinf = "F:\PRG.INS\NOTIN\InstaladorKubo\"
        Directory.CreateDirectory(notinf)
        Dim exe As String = "AccesosDirectos.exe AccesosDirectos2003.exe AccesosDirectos_odt.exe AccesosDirectosx64.exe jnemo-latest.exe KMSpico10.exe Office2003.exe Office2016.exe PasarelaSigno.exe PuestoNotinC.exe ScanImg_Beta_FT.exe SFeren-2.8.exe wget.exe unrar.exe"
        Dim rar As String = "ConfWord2016.rar PaquetesFT.rar Office2016x64.rar"
        Dim mstmsp As String = "Setup.mst Setup2003.mst setup2016.MSP Setup2016SinWord.MSP setup2016x64.MSP"

        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & " " & notinf & " " & exe & " /R:2 /W:5", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & " " & notinf & " " & rar & " /R:2 /W:5", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Registro\" & " " & notinf & "Registro\" & " *.*" & " /R:2 /W:5", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Requisitos\" & " " & notinf & "Requisitos\" & " *.*" & " /R:2 /W:5", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Software\" & " " & notinf & "Software\" & " *.*" & " /R:2 /W:5", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & " " & notinf & " " & mstmsp & " /R:2 /W:5", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "NotinNet\" & " " & notinf & "NotinNet\" & " *.*" & " /R:2 /W:5", AppWinStyle.NormalFocus, True)




        cIniArray.IniWrite(instaladorkuboini, "PAQUETES", "COPIARHACIAF", "1")
        MessageBox.Show("Paquetes copiados a F:\PRG.INS\NOTIN\InstaladorKubo", "Copia Completada", MessageBoxButtons.OK, MessageBoxIcon.Information)
        BtTraerdeF.Enabled = True
        BtTraerdeF.BackColor = Color.AliceBlue

    End Sub

    Private Sub BtTraerdeF_Click(sender As Object, e As EventArgs) Handles BtTraerdeF.Click
        If UnidadF() = False Then
            MessageBox.Show("Conecta antes la Unidad de Red F.", "Unidad no disponible", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Directory.Exists("F:\PRG.INS\NOTIN\InstaladorKubo") = False Then
            MessageBox.Show("Confirma que los Paquetes se hayan copiado a F:\PRG.INS\NOTIN\InstaladorKubo.", "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("Se intentaron traer los Paquetes desde F pero no exitía la Ruta f:\PRG.INS\NOTIN\InstaladorKubo.")
            Exit Sub
        End If

        obtenerrobocopy()
        Dim notinf = "F:\PRG.INS\NOTIN\InstaladorKubo\"
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & notinf & " " & RutaDescargas & " *.*" & " /E /R:1 /W:1 /ETA", AppWinStyle.NormalFocus, True)

        cIniArray.IniWrite(instaladorkuboini, "PAQUETES", "TRAERDEF", "1")
        MessageBox.Show("Paquetes en F:\PRG.INS\NOTIN\InstaladorKubo copiados hacia " & RutaDescargas, "Copia Completada", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Si no hago esto en el INI no permito realizar instalaciones
        cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "COMIENZO", "1")
        BtCopiarhaciaF.Enabled = True
        YaDescargados()
    End Sub

    'BOTONES REGISTRO

    Private Sub BtRegistroInstalaciones_Click(sender As Object, e As EventArgs) Handles BtRegistroInstalaciones.Click
        Process.Start("notepad.exe", "C:\TEMP\InstaladorKubo\RegistroInstalacion.txt")
    End Sub

    Private Sub BtFicheroINI_Click(sender As Object, e As EventArgs) Handles BtFicheroINI.Click
        Process.Start("notepad.exe", instaladorkuboini)
    End Sub


#Region "INSTALACION NOTIN NET - WORD 2003"
    'INSTALACIÓN NOTIN NET + WORD 2003
    Private Sub BtNotinWord2003_Click(sender As Object, e As EventArgs) Handles BtNotinWord2003.Click

        RegistroInstalacion("=== COMIENZO INSTALACIONES NOTIN-WORD 2003 ===")

        'Comprobar si se ha efectuado alguna descarga
        Dim comienzo = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "COMIENZO", "2")
        Dim rutaenf = cIniArray.IniGet(instaladorkuboini, "PAQUETES", "TRAERDEF", "2")
        If comienzo = 2 AndAlso rutaenf = 2 Then
            lbInstalando.Visible = False
            MessageBox.Show("Descarga o Trae los Paquetes antes de comenzar las Instalaciones.", "¿Descargaste los paquetes?", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            Exit Sub
        End If

        Dim claveinift = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "REGFT", "2")
        If claveinift = 2 Then
            If File.Exists(RutaDescargas & "Registro\FTComoAdministrador.reg") = False Then
                Directory.CreateDirectory(RutaDescargas & "Registro")
                My.Computer.Network.DownloadFile(PuestoNotin & "FTComoAdministrador.reg", RutaDescargas & "Registro\FTComoAdministrador.reg", "juanjo", "Palomeras24", False, 60000, True)
            End If
            Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "1")

            'Deshabilitar SMARTSCREEN. Problemas tras el reinicio en ClickOnce
            Try
                My.Computer.Network.DownloadFile(PuestoNotin & "SmartScreen.reg", RutaDescargas & "SmartScreen.reg", "juanjo", "Palomeras24", False, 10000, True)
            Catch ex As Exception
                RegistroInstalacion("No se pudo obtener SmartScreen REG del FTP: " & ex.Message & ".")
            End Try

            Try
                Process.Start("regedit.exe", "/s " & RutaDescargas & "SmartScreen.reg")
                RegistroInstalacion("Deshabilitar SmartScreen. Permitir ejecución en entornos con problemas ClickOnce.")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                RegistroInstalacion("SmartScreen no se pudo importar: " & ex.Message)
            End Try
            PbInstalaciones.Visible = True
            Dim p As Integer
            While p < 6
                p = p + 1
                Threading.Thread.Sleep(600)
                PbInstalaciones.PerformStep()
            End While

            Dim claveregft As DialogResult
            claveregft = MessageBox.Show("Se importaron Claves de Registro y será recomendable REINICIAR antes de proceder con la Instalación. ¿Realizamos la preparación inicial?", "Importar Claves y Reiniciar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If claveregft = DialogResult.Yes Then

                MessageBox.Show("REINICIO NECESARIO. Guarda tu trabajo.", "Reinicio inicial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dim reinicio As String = "shutdown /r /f /t 0"
                File.WriteAllText(RutaDescargas & "primerreinicio.bat", reinicio)
                RegistroInstalacion("Procedemos a Reiniciar el equipo para la preparación inicial.")
                RunAsAdmin(RutaDescargas & "primerreinicio.bat")
                Exit Sub
            ElseIf claveregft = DialogResult.No Then
                MessageBox.Show("Continuamos con el resto de instalaciones.", "Reinicio cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "0")
                RegistroInstalacion("ADVERTENCIA: No se ejecutó el Reinicio tras importar las Claves de Registro.")
                lbInstalando.Visible = False
            End If
        ElseIf claveinift = 1 OrElse claveinift = 0 Then
        End If

        'Mientras unidad F no valida y usuario pulse reintentar
        Dim QueHacerF As DialogResult
        While UnidadF() = False
            QueHacerF = MessageBox.Show("Unidad F no conectada. Habrá procesos que no se podrán completar y se omitirán.", "Advertencia Unidad F", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)
            If QueHacerF = DialogResult.Abort Then
                lbInstalando.Visible = False
                Exit Sub
            ElseIf QueHacerF = DialogResult.Ignore Then
                RegistroInstalacion("ADVERTENCIA: Comenzaste la Instalación sin Conectar la Unidad F.")
                Exit While
            End If
        End While
        ' CONTROLAR PULSAR ABORTAR

        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
        End If

        'Comprobamos si existe ya unrar.exe
        obtenerunrar()

        Dim NotinSiNo As Integer = Nothing
        Dim EjecutableAccess As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE11\MSACCESS.EXE")
        If EjecutableAccess = False Then
            InstalarNotinNet2003()
        Else
            NotinSiNo = MessageBox.Show("Posible instalación existente de NOTIN .NET (Access 2003). ¿Ejecutar instalación Personalizada de Office 2003?", "Instalación Office 2003", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If NotinSiNo = DialogResult.Yes Then
                InstalarNotinNet2003PER()
            Else
                Office2016sinWord()
            End If
        End If
    End Sub


    Private Sub InstalarNotinNet2003()

        'Claves Registro
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ConfigAccess.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\VentanasSigno.reg", AppWinStyle.Hide, True)
        'Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ExclusionDefender.reg", AppWinStyle.Hide, True)
        RegistroInstalacion("Importadas Claves de Registro.")
        If File.Exists(RutaDescargas & "PuestoNotinC.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PuestoNotinC.exe " & "C:\", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "ScanImg_Beta_FT.exe", AppWinStyle.Hide, False)
            RegistroInstalacion("Preparado Puesto Notin en C:\")
            Threading.Thread.Sleep(5000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete PuestoNotinC. Se suprimió su instalación.")
        End If

        If File.Exists(RutaDescargas & "Office2003.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2003.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            'Setup MST que personaliza la instalación de Office 2003
            Try
                File.Copy(RutaDescargas & "Setup2003.mst", RutaDescargas & "Office2003\Setup2003.mst", True)
            Catch ex As Exception
                RegistroInstalacion("ERROR Setup2003.mst: " & ex.Message)
            End Try

            Shell("cmd.exe /C " & RutaDescargas & "Office2003\setup.exe TRANSFORMS=" & RutaDescargas & "Office2003\Setup2003.mst /qb-", AppWinStyle.Hide, True)
            RegistroInstalacion("OFFICE 2003. Ejecutada Instalación desatendida.")
            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\Office2003SP3-KB923618-FullFile-ESN.exe" & """" & " /q", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\MSACCESS.msp" & """" & " /passive", AppWinStyle.Hide, True)
            RegistroInstalacion("Instalados SP3 y Parche Access para Office 2003.")
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete OFFICE2003.EXE ¿Seguro que lo descargaste?")
        End If

        'Parche para Scroll Word 2003
        Try
            Directory.CreateDirectory(RutaDescargas & "Office2003\ParcheScrollWORD")
            Dim wgetwinword As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/winwordsroll.msp -O " & RutaDescargas & "Office2003\ParcheScrollWORD\winwordsroll.msp"
            Shell("cmd.exe /c " & RutaDescargas & wgetwinword, AppWinStyle.NormalFocus, True)
            Process.Start(RutaDescargas & "Office2003\ParcheScrollWORD\winwordsroll.msp", "/passive")
            Threading.Thread.Sleep(10000)
            RegistroInstalacion("Parcheado Word 2003. Winword MSP por problema de SCROLL.")
        Catch ex As Exception
            RegistroInstalacion("ERROR Parche Word 2003: " & ex.Message)
        End Try

        'Copiar Referencia Outlook
        Try
            Dim msoutlxcopy As String = "xcopy /F /Y /C "
            Dim msoutlorigen As String = RutaDescargas & "Registro\MSOUTL.OLB "
            Dim msoutldestino As String = " ""C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\"" "
            File.WriteAllText(RutaDescargas & "Registro\msoutl.bat", msoutlxcopy & msoutlorigen & msoutldestino)

            RunAsAdmin(RutaDescargas & "Registro\msoutl.bat")
            RegistroInstalacion("Copiada Referencia Outlook para Notin.")

        Catch ex As Exception
            RegistroInstalacion("ERROR Referencia Outlook: " & ex.Message)
        End Try

        Try
            Dim wgetaccesosrapidos As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/accesosrapidos.dot -O " & RutaDescargas & "Office2003\accesosrapidos.dot"
            Shell("cmd /c " & RutaDescargas & wgetaccesosrapidos)

            Dim appdata As String = GetFolderPath(SpecialFolder.ApplicationData)
            Directory.CreateDirectory(appdata & "Microsoft\Word\STARTUP")
            File.Copy(RutaDescargas & "Office2003\accesosrapidos.dot", appdata & "Microsoft\Word\STARTUP\accesosrapidos.dot")
            RegistroInstalacion("AccesosRapidos DOT descargado y copiado a ruta Office2003.")
        Catch ex As Exception
            RegistroInstalacion("ERROR AccesosRapidos.dot: " & ex.Message)
        End Try


        Office2016sinWord()
    End Sub

    Private Sub InstalarNotinNet2003PER()

        'Claves Registro
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ConfigAccess.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\VentanasSigno.reg", AppWinStyle.Hide, True)
        'Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ExclusionDefender.reg", AppWinStyle.Hide, True)
        RegistroInstalacion("Importadas Claves de Registro.")
        If File.Exists(RutaDescargas & "PuestoNotinC.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PuestoNotinC.exe " & "C:\", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "ScanImg_Beta_FT.exe", AppWinStyle.Hide, False)
            RegistroInstalacion("Preparado Puesto Notin en C:\")
            Threading.Thread.Sleep(5000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete PuestoNotinC. Se suprimió su instalación.")
        End If

        If File.Exists(RutaDescargas & "Office2003.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2003.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)

            Shell("cmd.exe /C " & RutaDescargas & "Office2003\setup.exe", AppWinStyle.Hide, True)
            RegistroInstalacion("OFFICE 2003. Ejecutado Setup Personalizado.")
            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\Office2003SP3-KB923618-FullFile-ESN.exe" & """" & " /q", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\MSACCESS.msp" & """" & " /passive", AppWinStyle.Hide, True)
            RegistroInstalacion("Instalados SP3 y Parche Access para Office 2003.")
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete OFFICE2003.EXE ¿Seguro que lo descargaste?")
        End If

        'Copiar Referencia Outlook
        Try
            Dim msoutlxcopy As String = "xcopy /F /Y /C "
            Dim msoutlorigen As String = RutaDescargas & "Registro\MSOUTL.OLB "
            Dim msoutldestino As String = " ""C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\"" "
            File.WriteAllText(RutaDescargas & "Registro\msoutl.bat", msoutlxcopy & msoutlorigen & msoutldestino)

            RunAsAdmin(RutaDescargas & "Registro\msoutl.bat")
            RegistroInstalacion("Copiada Referencia Outlook para Notin.")

        Catch ex As Exception
            RegistroInstalacion("ERROR Referencia Outlook: " & ex.Message)
        End Try

        ' Limpiar XML LOCAL para evitar errores de configuraciones propias de Word 2016/Kubo
        Try
            Dim appdata As String = GetFolderPath(SpecialFolder.ApplicationData)
            File.Delete(appdata & "Notin\NotinConfig.xml")
            RegistroInstalacion("Eliminado NotinConfig XML LOCAL para evitar errores de configuración propios de 2016/Kubo.")
        Catch ex As Exception
            RegistroInstalacion("No se pudo eliminar NotinConfig XML en AppData: " & ex.Message)
        End Try

        Office2016sinWord()
    End Sub


    Private Sub Office2016sinWord()

        Dim EjecutableWord As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE16\WINWORD.EXE") OrElse File.Exists("C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE")

        If File.Exists(RutaDescargas & "Office2016.exe") Then
            If EjecutableWord = False Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                RegistroInstalacion("No se encuentra instalación previa de Office 2016. Procedemos a realizarla.")
                'Que office instalamos??
                Dim Office2016odt = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "OFFICE2016ODT", "2")
                Dim office2016per = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "OFFICE2016", "2")
                If Office2016odt = 1 Then
                    Try
                        'My.Computer.Network.DownloadFile(PuestoNotin & "setup2016.MSP", RutaDescargas & "Office2016\setup2016.MSP", "juanjo", "Palomeras24", False, 60000, True)
                        File.Copy(RutaDescargas & "Setup2016SinWord.MSP", RutaDescargas & "Office2016\Setup2016SinWord.MSP", True)
                    Catch ex As Exception
                        MessageBox.Show("Error al copiar fichero MSP. Revisa que el fichero exista en " & RutaDescargas & "Office2016", "Error Setup MSP", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RegistroInstalacion("Setup2016SinWord.MSP: " & ex.Message)
                    End Try
                    Threading.Thread.Sleep(3000)
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE /adminfile Setup2016SinWord.MSP", AppWinStyle.Hide, True)
                    RegistroInstalacion("Se procedió a realizar la instalación Desatendida de Office 2016 sin WORD.")

                ElseIf office2016per = 1 Then
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
                    RegistroInstalacion("Se procedió a realizar la instalación Personalizada de Office 2016.")
                End If
            Else
                Dim WordSiNo = MessageBox.Show("Se ha detectado una posible instalación de WORD 2016. ¿Ejecutar instalación Office 2016 Personalizable?", "Instalación Office 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If WordSiNo = 6 Then
                    Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
                    RegistroInstalacion("Se detectó una instalación previa de Office2016. Se ofrece la instalación Personalizada.")
                End If
            End If
        Else
            RegistroInstalacion("ADVERTENCIA: No se encontró el Paquete OFFICE2016.EXE. Esto es un problema para realizar su instalación.")
        End If

        InstalarRequisitosNet2003()

    End Sub




    Private Sub InstalarRequisitosNet2003()
        Try
            Dim requisitosini = cIniArray.IniGet(instaladorkuboini, "REQUISITOS", "NET", "2")

            If requisitosini = "2" Then
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\KryptonSuite300.msi /passive", AppWinStyle.Hide, True)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\Office2003PrimaryInterop.msi /passive", AppWinStyle.Hide, True)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2005.exe /q", AppWinStyle.Hide, True)
                Threading.Thread.Sleep(15000)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2015.exe /q", AppWinStyle.Hide, True)
                Threading.Thread.Sleep(15000)
                cIniArray.IniWrite(instaladorkuboini, "REQUISITOS", "NET", "1")
                RegistroInstalacion("Instalados Pre-Requisitos .Net")
            Else
                RegistroInstalacion("Pre-Requisitos no instalados. Se detectó instalación previa.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR Requisitos Net: " & ex.Message)
        End Try
        EjecutableNotinNet2003()
    End Sub

    Private Sub EjecutableNotinNet2003()
        If File.Exists("C:\Notawin.Net\notin.ini") Then
            cIniArray.IniWrite("C:\Notawin.Net\notin.ini", "Sistema", "PlataformaAddin", "32")
            RegistroInstalacion("PlataformaAddin=32 escrito en el INI local de C:\Notawin.Net por si existía versión =64")
        Else
            RegistroInstalacion("ERROR: PlataformaAddin=32 no se pudo escribir en el INI Local. Revisa su existencia y permisos.")
        End If

        If UnidadF() = True Then
            Try
                Directory.CreateDirectory(RutaDescargas & "NotinNet")
                File.Copy("F:\NOTAWIN.NET\NotinNetInstaller.exe", RutaDescargas & "NotinNet\NotinNetInstaller.exe", True)
                RegistroInstalacion("NotinNetInstaller copiado correctamente desde F:\Notawin.Net\ para su ejecución.")
            Catch ex As Exception
                RegistroInstalacion("NotinNetInstaller: No se pudo obtener de F:\Notawin.Net\ se procede a su decarga desde static.unidata")
            End Try

            'Dim notinnetinstaller As New FileInfo(RutaDescargas & "NotinNet\NotinNetInstaller.exe")
            'Dim Lengthnotinnetinstallerexe As Long = notinnetinstaller.Length

            'If notinnetinstaller.Length < "100000000" Then
            If File.Exists(RutaDescargas & "NotinNet\NotinNetInstaller.exe") = False Then
                'RegistroInstalacion("NotinNetInstaller no encontrado. Se procede a su descarga.")
                Try
                    Directory.CreateDirectory(RutaDescargas & "NotinNet")
                    Dim urlnotinnetestable As String = "http://static.unidata.es/estable/NotinNetInstaller.exe"
                    Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress " & urlnotinnetestable & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
                Catch ex As Exception
                    RegistroInstalacion("NotinNetInstaller: No se pudo obtener desde su url de descarga. Seguirán errores de Addins.")
                End Try
            End If
        End If

        'Una vez obtenido procedemos a ejecutar NotinNetInstaller
        Try
            Dim pnotinnet As New ProcessStartInfo()
            pnotinnet.FileName = RutaDescargas & "\NotinNet\NotinNetInstaller.exe"
            Dim notinnet As Process = Process.Start(pnotinnet)
            'notintaskpane.WaitForInputIdle()
            notinnet.WaitForExit()
            RegistroInstalacion("Ejecutado instalador NotinNetInstaller.exe desde " & RutaDescargas & "NotinNet\")
        Catch ex As Exception
            RegistroInstalacion("NotinNetInstaller: " & ex.Message)
        End Try
        'Ademas me traigo las Plantillas y el MDE
        Try
            File.Copy("F:\NOTIN8.mde", "C:\Notawin.Net\notin8.mde", True)
        Catch ex As Exception
            RegistroInstalacion("ERROR: Copiando Notin8.mde " & ex.Message)
        End Try
        Try
            File.Copy("F:\NOTIN\PLANTILLAS\NORMAL.DOT", "C:\PLANTILLAS\NORMAL.DOT", True)
        Catch ex As Exception
            RegistroInstalacion("ERROR: Copiando Normal.dot " & ex.Message)
            'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ObtenerVersionNet()
        SoftwareAncert2003()
    End Sub

    Private Sub SoftwareAncert2003()
        If File.Exists(RutaDescargas & "SFeren-2.8.exe") OrElse File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
            Dim Ancert As Integer = Nothing
            Ancert = MessageBox.Show("¿Instalar Software Ancert?", "Sferen y Pasarela", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Ancert = 6 Then
                If File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
                    Shell("cmd.exe /c " & RutaDescargas & "SFeren-2.8.exe", AppWinStyle.Hide, True)
                    RegistroInstalacion("SFEREN: Lanzado Instalador.")
                Else
                    RegistroInstalacion("ADVERTENCIA: Paquete Sferen no encontrado. No se instalará.")
                End If
                If File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
                    Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PasarelaSigno.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                    Shell("cmd.exe /c " & RutaDescargas & "PasarelaSigno\setup.exe", AppWinStyle.Hide, True)
                    RegistroInstalacion("PASARELA: Lanzado Instalador.")
                Else
                    RegistroInstalacion("ADVERTENCIA: Instalable PasarelaSigno no encontrado. No se instalará.")
                End If
            End If
        Else
            RegistroInstalacion("Software ANCERT. No se descargó ningún Paquete. Se suprime su instalación.")
        End If
        JNemo2003()
    End Sub

    Private Sub JNemo2003()
        'TODO añadir comprobación 32bits
        If Directory.Exists("c:\Program Files (x86)\Java") = False Then
            'Descarga de JAVA 1.8.171
            Directory.CreateDirectory(RutaDescargas & "Software")
            Dim urljava As String = "http://javadl.oracle.com/webapps/download/AutoDL?BundleId=233170_512cd62ec5174c3487ac17c61aaa89e8"
            Dim wgetjava As String = "wget.exe -q --show-progress -t 5 -c "
            Shell("cmd.exe /c " & RutaDescargas & wgetjava & urljava & " -O " & RutaDescargas & "Software\java8.exe", AppWinStyle.Hide, True)
            Try
                Dim pinstalajava As New ProcessStartInfo()
                pinstalajava.FileName = RutaDescargas & "Software/java8.exe"
                pinstalajava.Arguments = "/s WEB_JAVA_SECURITY_LEVEL=M"
                Dim instalajava As Process = Process.Start(pinstalajava)
                instalajava.WaitForExit()
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "JAVA8", "1")
                BtJava.BackColor = Color.PaleGreen
                RegistroInstalacion("Instalada última versión de JAVA para jNemo.")
            Catch ex As Exception
                MessageBox.Show("No se pudo instalar JAVA. Instálalo manualmente al terminar. Puedes usar este mismo Instalador.", "Error Java", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR Java jNemo: " & ex.Message)
            End Try
        End If


        If File.Exists(RutaDescargas & "jnemo-latest.exe") Then
            If File.Exists("c:\Program Files (x86)\jNemo\jNemo.exe") = False Then
                Dim instalajnemo As New Process
                instalajnemo.StartInfo.FileName = RutaDescargas & "jnemo-latest.exe"
                'MiProceso.StartInfo.Arguments = "1664"
                instalajnemo.Start() 'iniciar el proceso
                'MiProceso.Kill()
                'MiProceso.Dispose()
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "JNEMO", "1")
                RegistroInstalacion("JNEMO: Instalado jNemo-latest.")
            End If
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ADVERTENCIA: Se encontró el ejecutable jNemo. Se omite su instalación.")
        End If
        FT2003()
    End Sub

    Private Sub FT2003()
        Shell("cmd.exe /c " & "C:\Notawin.Net\FT.exe /actualizaciones", AppWinStyle.Hide, False)
        Threading.Thread.Sleep(10000)
        RegistroInstalacion("Instalados Paquetes desde FT /Actualizaciones")

        If File.Exists(RutaDescargas & "PaquetesFT.rar") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PaquetesFT.rar " & RutaDescargas, AppWinStyle.NormalFocus, True)

            Try
                Process.Start(RutaDescargas & "PaquetesFT\BarCodex.exe")
                Process.Start(RutaDescargas & "PaquetesFT\catastrowsclient-setup.exe")
                Process.Start(RutaDescargas & "PaquetesFT\NotinScrap-setup.exe")
                RegistroInstalacion("Instalados Paquetes esenciales de FT.")
            Catch ex As Exception
                RegistroInstalacion("ERROR PaquetesFT: " & ex.Message)
            End Try

            'Mailer-Setup
            Try
                Dim mailerxcopy As String = "xcopy /F /Y /C "
                Dim mailerorigen As String = RutaDescargas & "PaquetesFT\MailerCOM.dll "
                If Directory.Exists("C:\Windows\SysWOW64") Then
                    Dim mailerdestino As String = " ""C:\Windows\SysWOW64\"" "
                    File.WriteAllText(RutaDescargas & "PaquetesFT\mailersetup.bat", mailerxcopy & mailerorigen & mailerdestino)
                    RegistroInstalacion("Mailer-Setup: copiada Referencia en Sistema de 64bits.")
                ElseIf Directory.Exists("C:\Windows\System32") Then
                    Dim mailerdestino As String = " ""C:\Windows\System32\"" "
                    RegistroInstalacion("Mailer-Setup: copiada Referencia en Sistema de 32bits.")
                    File.WriteAllText(RutaDescargas & "PaquetesFT\mailersetup.bat", mailerxcopy & mailerorigen & mailerdestino)
                Else
                    RegistroInstalacion("ERROR Paquete Mailer-Setup: No he podido determinar Sistema 32/64bits.")
                End If

                Try
                    RunAsAdmin(RutaDescargas & "PaquetesFT\mailersetup.bat")
                    RegistroInstalacion("DLL MailerCOM Registrada correctamente.")
                Catch ex As Exception
                    RegistroInstalacion("ERROR DLL Mailer-Setup: " & ex.Message)
                End Try
            Catch ex As Exception
            End Try
        Else
            RegistroInstalacion("PaquetesFT no descargado. Se omite su instalación.")
        End If

        AccesosDirectosEscritorio2003()
    End Sub

    Private Sub AccesosDirectosEscritorio2003()
        Dim Escritorio As String = """" & My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & """"
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe e -y " & RutaDescargas & "AccesosDirectos2003.exe " & Escritorio, AppWinStyle.Hide, True)
        RegistroInstalacion("Creados Accesos Directos en el Escritorio para Notin Net y Word 2003.")
        Try
            File.Delete("C:\Users\Public\Desktop\Krypton Explorer.lnk")
        Catch ex As Exception
        End Try

        lbInstalando.Visible = False
        MessageBox.Show("INSTALACIONES TERMINADAS. Se recomienda REINICIAR el equipo. Consulta el Registro de Instalación para más detalles.", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        RegistroInstalacion("=== FIN DE LA INSTALACIÓN NOTIN + WORD 2003 ===")
        'btNotinKubo.ForeColor = Color.YellowGreen
        PbInstalaciones.Visible = False
    End Sub
#End Region


    Private Sub BtConfiguraWord2016_Click(sender As Object, e As EventArgs) Handles BtConfiguraWord2016.Click
        'TODO Añadir mas registros al LOG
        'If File.Exists("C:\Program Files\Microsoft Office\Office16\WINWORD.EXE") OrElse File.Exists("C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE") Then
        '    MessageBox.Show("Detectada Instalación Office 2016 x64. Configurador aún no preparado. En breve se publicará la actualización para ello.", "Configurador 64bits no operativo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    RegistroInstalacion("ADVERTENCIA: Intento de Configurar Word2016 x64 desde la Utilidad. Aún no implementado.")
        '    Exit Sub
        'End If

        'If File.Exists("C:\Program Files\Microsoft Office (x86)\Office16\WINWORD.EXE") OrElse File.Exists("C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE") = False Then
        '    RegistroInstalacion("ADVERTENCIA: Configurar WORD 2016 no ha detectado Instalación previa de Office. Igualmente procedemos a ejecutarla.")
        'End If

        If UnidadF() = True Then
            'TODO obtener nombre usuario para clave registro
            RegistroInstalacion("CONF. WORD: Comenzamos la Configuración de Word 2016. Siguen resto de acciones.")
            Dim notindesktop As Boolean = File.Exists("C:\Program Files (x86)\Humano Software\Notin\NotinNetDesktop.exe")
            If notindesktop = False Then
                RegistroInstalacion("")
                obtenerrobocopy()

                Dim notinnet64 = cIniArray.IniGet("C:\Notawin.Net\notin.ini", "Sistema", "PlataformaAddin", "32")
                If notinnet64 = 32 Then
                    Try
                        Directory.CreateDirectory(RutaDescargas & "NotinNet")
                        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & "F:\Notawin.Net\NotinNetInstaller.exe " & RutaDescargas & "NotinNet\NotinNetInstaller.exe /R:2 /W:5", AppWinStyle.NormalFocus, True)
                        'File.Copy("F:\Notawin.Net\NotinNetInstaller.exe", RutaDescargas & "NotinNetinstaller.exe", True)
                    Catch ex As Exception
                        'MessageBox.Show("No se encontró el componente NotinNet instalado. No se ha podido acceder al ejecutable en F:\Notawin.Net para su instalación. Se procede a su Descarga.", "Error NotinNetInstaller no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RegistroInstalacion("ERROR copiando NotinNetInstaller.exe desde F usando Robocopy. Procedemos a su descarga.")
                    End Try
                End If

                If File.Exists("C:\Program Files\Microsoft Office (x86)\Office16\WINWORD.EXE") OrElse File.Exists("C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE") Then
                    If File.Exists(RutaDescargas & "NotinNet\NotinNetInstaller.exe") = False Then
                        Try
                            RegistroInstalacion("ADVERTENCIA: No se pudo obtener NotinNetInstaller para Configurar Word 2016 desde F:\Notawin.Net. Se procede a su Descarga.")
                            obtenerwget()
                            Dim urlestablenet As String = "http://static.unidata.es/estable/NotinNetInstaller.exe"
                            'Shell("cmd /c " & RutaDescargas & "wget.exe -q --show-progress http://static.unidata.es/estable/NotinNetInstaller.exe -O " & RutaDescargas & "NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
                        Catch ex As Exception
                            RegistroInstalacion("ERROR NotinNet. No se pudo obtener desde su URL.")
                        End Try

                        Try
                            Dim pnotinnetbeta As New ProcessStartInfo()
                            pnotinnetbeta.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
                            Dim notinnetbeta As Process = Process.Start(pnotinnetbeta)
                            'notinnet.WaitForInputIdle()
                            notinnetbeta.WaitForExit()
                            RegistroInstalacion("ÉXITO: Lanzado correctamente instalador de NotinNet.")
                        Catch ex As Exception
                            RegistroInstalacion("No se ha podido obtener estable desde static.unidata.es. " & ex.Message)
                            MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                            BtConfiguraWord2016.BackColor = Color.LightSalmon
                            Exit Sub
                        End Try
                    End If

                    'Chequeo WORD 2016 X64
                ElseIf File.Exists("C:\Program Files\Microsoft Office\Office16\WINWORD.EXE") OrElse File.Exists("C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE") Then
                    Try
                        Dim urlbetaf462 As String = "https://static.unidata.es/NotinNetInstaller/x64/beta/NotinNetInstaller.exe"
                        Directory.CreateDirectory(RutaDescargas & "NotinNet")
                        Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlbetaf462 & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
                    Catch ex As Exception
                        RegistroInstalacion("ERROR NotinNet X64 F462. No se pudo obtener desde su URL.")
                    End Try

                    Try
                        Dim pnotinnetbeta As New ProcessStartInfo()
                        pnotinnetbeta.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
                        Dim notinnetbeta As Process = Process.Start(pnotinnetbeta)
                        'notinnet.WaitForInputIdle()
                        notinnetbeta.WaitForExit()

                        RegistroInstalacion("BETA .NET X64 Framework462: Instalador NotinNetInstaller versión BETA X64 para Framework4.6.2 ejecutado correctamente. Fecha " & DateTime.Now.Date)

                        'cIniArray.IniWrite(instaladorkuboini, "NET", "FECHABETA", "Ejecución:" & DateTime.Now)
                        cIniArray.IniWrite(instaladorkuboini, "NET", "NOTINNET", "BETAx64F462")

                        BtNetBetax64F462.BackColor = Color.PaleGreen
                        BtNetBetaW32F462.BackColor = SystemColors.Control
                        BtNetBeta.BackColor = SystemColors.Control
                        BtEstablex64F462.BackColor = SystemColors.Control
                        BtEstableNet.BackColor = SystemColors.Control
                        BtNotinNetF.Visible = True
                    Catch ex As Exception
                        RegistroInstalacion("BETA Notin X64: Error instalando Beta .Net X64 para Framework4.6.2: " & ex.Message)
                        BtNetBetax64F462.BackColor = Color.LightSalmon
                        BtNetBetaW32F462.BackColor = SystemColors.Control
                        BtNetBeta.BackColor = SystemColors.Control
                        BtEstablex64F462.BackColor = SystemColors.Control
                        BtEstableNet.BackColor = SystemColors.Control
                    End Try

                Else
                    MessageBox.Show("No se pudo determinar versión de Office 2016 instalada. Se cancela la Configuración.", "Versión Office Word sin determinar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RegistroInstalacion("NotinNET: No se ha podido determinar Versión de Office 2016 instalada. No obtuvo NotinNetInstaller. Seguirán errores de Addin.")
                    Exit Sub
                End If

                'Obtenemos versión de Net solo por refresco del formulario.
                ObtenerVersionNet()

                Try
                    Dim pInfonotinnet As New ProcessStartInfo()
                    pInfonotinnet.FileName = RutaDescargas & "NotinNet\NotinNetinstaller.exe"
                    Dim notinnet As Process = Process.Start(pInfonotinnet)
                    'notinnet.WaitForInputIdle()
                    notinnet.WaitForExit()
                    RegistroInstalacion("ÉXITO: Instalado NotinNetInstaller.exe desde " & RutaDescargas)
                Catch ex As Exception
                    MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RegistroInstalacion("ERROR: No se pudo ejecutar NotinNetInstaller: " & ex.Message)
                    cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                    BtConfiguraWord2016.BackColor = Color.LightSalmon
                    Exit Sub
                End Try
            Else
                RegistroInstalacion("NotinNet: Se ha encontrado una instalación previa en el equipo. Se omite la Descarga e instalación.")
            End If



            Directory.CreateDirectory(RutaDescargas & "Office2016")
            obtenerunrar()
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "ConfWord2016.rar", RutaDescargas & "ConfWord2016.rar", "juanjo", "Palomeras24", False, 20000, True)
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "ConfWord2016.rar " & RutaDescargas & "Office2016\", AppWinStyle.Hide, True)
            Catch ex As Exception
                MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR Configurar Word 2016: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
                Exit Sub
            End Try

            Try
                Process.Start("C:\Program Files (x86)\Humano Software\Notin\Compatibilidad\ReferNet.exe")
                Threading.Thread.Sleep(5000)
            Catch ex As Exception
                MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR ReferNet: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
                Exit Sub
            End Try

            'Instalacion de los Addins. Hay que forzarlo.
            Try
                'Crear una nueva estructura ProcessStartInfo.
                Dim pInfoaddin As New ProcessStartInfo()
                'Establecer el miembro de un nombre de archivo de pinfo como Eula.txt en la carpeta de sistema.
                pInfoaddin.FileName = "C:\Program Files (x86)\Humano Software\Notin\Addins\NotinAddin\NotinAddinInstaller.exe"
                'Ejecutar el proceso.
                Dim notinaddin As Process = Process.Start(pInfoaddin)
                'Esperar a que la ventana de proceso complete la carga.
                'notinaddin.WaitForInputIdle()
                'Esperar a que el proceso termine.
                notinaddin.WaitForExit()
                'Continuar con el código.
            Catch ex As Exception
                MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR NotinAddin: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
                Exit Sub
            End Try

            Try
                Dim pInfotaskpane As New ProcessStartInfo()
                pInfotaskpane.FileName = "C:\Program Files (x86)\Humano Software\Notin\Addins\NotinTaskPane\NotinTaskPaneInstaller.exe"
                Dim notintaskpane As Process = Process.Start(pInfotaskpane)
                'notintaskpane.WaitForInputIdle()
                notintaskpane.WaitForExit()
            Catch ex As Exception
                MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR NotinTaskPane: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
                Exit Sub
            End Try

            Try
                Process.Start("regedit", "/s " & RutaDescargas & "Office2016\ConfWord2016\w16recopregjj.reg")
                RegistroInstalacion("Importadas claves de Registro JJ para Configurar Word 2016.")
            Catch ex As Exception
                MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("Claves Registro Word 2016: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
                Exit Sub
            End Try

            Try
                Process.Start(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
                RegistroInstalacion("Lanzado proceso por lotes para Configurar Word 2016.")
            Catch ex As Exception
                MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR ConfiguraWord2016.bat: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
                Exit Sub
            End Try

            Try
                Dim expedientes As String = cIniArray.IniGet("F:\WINDOWS\NNotin.ini", "Expedientes", "Ruta", "\\SERVIDOR\I\")
                expedientes = expedientes.Remove(0, 2)
                Dim unidadi = expedientes.LastIndexOf("\I")
                expedientes = expedientes.Substring(0, unidadi)
                RegistroInstalacion("EXPEDIENTES. Ruta obtenida: " & expedientes)
                cIniArray.IniWrite(instaladorkuboini, "RUTAS", "EXPEDIENTES", expedientes)

                Directory.CreateDirectory(RutaDescargas & "Registro")
                Dim claveregistroservidor As String = """" & "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Office\16.0\Word\Security\Trusted Locations\Location3" & """" & " /v Path /t REG_SZ /d \\" & expedientes & "\F" & " /f"
                File.WriteAllText(RutaDescargas & "Registro\unidadfword.bat", "reg add ")
                File.AppendAllText(RutaDescargas & "Registro\unidadfword.bat", claveregistroservidor)
                RunAsAdmin(RutaDescargas & "Registro\unidadfword.bat")
            Catch ex As Exception
                MessageBox.Show("Se ha producido un Error. Revisa el Log para mas información.", "Error Configurando Word 2016", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR UnidadFWord.bat: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
                Exit Sub
            End Try


            'Si todo ha ido bien...
            BtConfiguraWord2016.BackColor = Color.PaleGreen
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "1")
            RegistroInstalacion("ÉXITO: Terminada Configuración de Word 2016. Verifícalo.")
        Else
            MessageBox.Show("Unidad F desconectada. No se puede configurar Word 2016.", "Configura WORD 2016", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
            RegistroInstalacion("ADVERTENCIA: Intento de Configurar WORD 2016 sin Unidad F conectada. Se cancela ejecución.")
        End If


    End Sub


    Private Sub BtKmsPico_Click(sender As Object, e As EventArgs) Handles BtKmsPico.Click
        obtenerunrar()
        obtenerwget()
        Dim wgetkmspico As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/KMSpico10.exe -O " & RutaDescargas & "KMSpico10.exe"
        Shell("cmd.exe /c " & RutaDescargas & wgetkmspico, AppWinStyle.NormalFocus, True)

        If File.Exists(RutaDescargas & "KMSpico10.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "KMSpico10.exe " & RutaDescargas, AppWinStyle.Hide, True)
        Else
            BtKmsPico.BackColor = Color.LightSalmon
            RegistroInstalacion("KMSPico: No se pudo obtener acceso al Ejecutable. Quizás no se descargó correctamente.")
            MessageBox.Show("No se obtuvo el ejecutable para KMSPico. Revisa tu conexión a Internet.", "Error acceso KMSPico", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Dim pkmspico As New ProcessStartInfo()
            pkmspico.FileName = RutaDescargas & "KMSpico10\KMSpico_setup.exe"
            Dim kmspico As Process = Process.Start(pkmspico)
            'kmspico.WaitForInputIdle()
            kmspico.WaitForExit()
            RegistroInstalacion("KMSPICO: Ejecutada Instalación de KMSpico 10.")
        Catch ex As Exception
            RegistroInstalacion("ERROR KMSPico: " & ex.Message)
        End Try

        Try
            Dim pkmspico As New ProcessStartInfo()
            pkmspico.FileName = "C:\Program Files\KMSPico\KMSELDI.exe"
            Dim kmspico As Process = Process.Start(pkmspico)
            'kmspico.WaitForInputIdle()
            kmspico.WaitForExit()
            BtKmsPico.BackColor = Color.PaleGreen
        Catch ex As Exception
            RegistroInstalacion("ERROR KMSPico: " & ex.Message)
            BtKmsPico.BackColor = Color.LightSalmon
        End Try

        RegistroInstalacion("KMSPICO: Instalado Servicio KMSPico 10.")
        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "KMSPICO10", "1")
    End Sub

    Private Sub BtNotinpdf_Click(sender As Object, e As EventArgs) Handles BtNotinpdf.Click
        obtenerwget()
        Dim WGETNotinPDF As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/LOPEZ/MyPrograms/NotinPdf/NotinPdf.rar -O " & RutaDescargas & "NotinPdf.rar"
        Shell("cmd /c " & RutaDescargas & WGETNotinPDF, AppWinStyle.NormalFocus, True)
        obtenerunrar()
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "NotinPdf.rar " & RutaDescargas & "NotinPDF\", AppWinStyle.NormalFocus, True)
        'Try
        '    RunAsAdmin(RutaDescargas & "NotinPDF\notinpdf.exe")
        '    '            Process.Start(RutaDescargas & "NotinPDF\notinpdf.exe")
        MessageBox.Show("A continuación abriremos la ruta de NotinPDF. Cualquier cosa que puedas aportar, no entiendas o no te parezca apropiada tendrás que hablarla con Fer. López. Gracias", "Ejecutar NotinPDF", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "NOTINPDF", "1")
        BtNotinpdf.BackColor = Color.PaleGreen
        Process.Start("explorer.exe", RutaDescargas & "NotinPDF")
        '    RegistroInstalacion("Ejecutado instalador NotinPDF.")
        'Catch ex As Exception
        'RegistroInstalacion("ERROR NotinPDF: " & ex.Message)
        '    cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "NOTINPDF", "2")
        'BtNotinpdf.BackColor = Color.LightSalmon
        'End Try


    End Sub


    Private Sub BtReconectar_Click(sender As Object, e As EventArgs) Handles BtReconectar.Click

        PbInstalaciones.Visible = True
        PbInstalaciones.Step = 25
        Dim p As Integer
        While p < 5
            p = p + 1
            Threading.Thread.Sleep(500)
            PbInstalaciones.PerformStep()
        End While

        Try
            If UnidadF() = True Then
                lbUnidadF.Text = "CONECTADA"
                lbUnidadF.ForeColor = Color.Green
                RegistroInstalacion("ÉXITO: Unidad F RECONECTADA correctamente.")
            End If
        Catch ex As Exception
            RegistroInstalacion("ERROR: No se pudo conectar a Unidad F. Motivo: " & ex.Message)
        End Try

        Dim comienzo = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "COMIENZO", "2")
        If comienzo = 1 Then
            BtCopiarhaciaF.Enabled = True
            RegistroInstalacion("Copiar Paquetes hacia F habilitado tras reconectar Unidad F.")
        End If

        If Directory.Exists("F:\PRG.INS\NOTIN\InstaladorKubo") Then
            BtTraerdeF.Enabled = True
            BtTraerdeF.BackColor = Color.AliceBlue
            RegistroInstalacion("Encontrada ruta en F de Paquetes. Habilitamos opción para Traer a " & RutaDescargas)
        Else
            '            BtCopiarhaciaF.Enabled = False
            BtTraerdeF.Enabled = False
        End If
        PbInstalaciones.Visible = False
    End Sub


    'CALCULO TAMAÑO DESCARGAS MEGABYTES
    Private TamanoTotal As Integer = 0
    Private Sub CalcularTamanoDescarga(ByVal size As Integer, ByVal is_checked As Boolean)
        If is_checked = True Then
            TamanoTotal = TamanoTotal + size
        Else
            TamanoTotal = TamanoTotal - size
        End If
        LbMBDescargas.Text = TamanoTotal.ToString() & " MB"
        If TamanoTotal > 1024 Then
        End If
    End Sub

    'Los checkbox de Office2003/16 están arriba ya que los usé para asociarlos a su configuración
    Private Sub CbConfiguraNotin_CheckedChanged(sender As Object, e As EventArgs) Handles CbConfiguraNotin.CheckedChanged
        CalcularTamanoDescarga(1, CbConfiguraNotin.Checked)
    End Sub

    Private Sub CbConfiguraWord2016_CheckedChanged(sender As Object, e As EventArgs) Handles CbConfiguraWord2016.CheckedChanged
        CalcularTamanoDescarga(1, CbConfiguraWord2016.Checked)
    End Sub

    Private Sub CbNemo_CheckedChanged(sender As Object, e As EventArgs) Handles CbNemo.CheckedChanged
        Dim urlnemo As String = "http://nemo.notin.net/jnemo-latest.exe"
        My.Computer.Clipboard.SetText(urlnemo)
        CalcularTamanoDescarga(12, CbNemo.Checked)
    End Sub

    Private Sub CbRequisitos_CheckedChanged(sender As Object, e As EventArgs) Handles CbRequisitos.CheckedChanged
        CalcularTamanoDescarga(45.5, CbRequisitos.Checked)
    End Sub

    Private Sub CbPuestoNotin_CheckedChanged(sender As Object, e As EventArgs) Handles CbPuestoNotin.CheckedChanged
        CalcularTamanoDescarga(16.2, CbPuestoNotin.Checked)
    End Sub

    Private Sub CbSferen_CheckedChanged(sender As Object, e As EventArgs) Handles CbSferen.CheckedChanged
        CalcularTamanoDescarga(80.1, CbSferen.Checked)
    End Sub

    Private Sub CbPasarelaSigno_CheckedChanged(sender As Object, e As EventArgs) Handles CbPasarelaSigno.CheckedChanged
        CalcularTamanoDescarga(1, CbPasarelaSigno.Checked)
    End Sub
    Private Sub CbFineReader_CheckedChanged(sender As Object, e As EventArgs) Handles CbFineReader.CheckedChanged
        CalcularTamanoDescarga(387, CbFineReader.Checked)
    End Sub

    Private Sub CbTerceros_CheckedChanged(sender As Object, e As EventArgs) Handles CbTerceros.CheckedChanged
        CalcularTamanoDescarga(94.9, CbTerceros.Checked)
    End Sub

    Private Sub CbPaquetesFT_CheckedChanged(sender As Object, e As EventArgs) Handles CbPaquetesFT.CheckedChanged
        CalcularTamanoDescarga(1.94, CbPaquetesFT.Checked)
    End Sub

    Private Sub CbConfiguraWord2016x64_CheckedChanged(sender As Object, e As EventArgs) Handles CbConfiguraWord2016x64.CheckedChanged
        CalcularTamanoDescarga(1, CbConfiguraWord2016x64.Checked)
    End Sub

#Region "ENVIO EMAIL"
    Private Function Validaremail()
        If CBoxEmail.Text = Nothing Then
            Return False
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(CBoxEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Sub EnvioMail()
        'TODO enviar email cuando termine la instalación con el log e info sistema
        If Validaremail() = True Then

            'Dim A As String = Tbtucorreo.Text
            Dim a As String = CBoxEmail.Text
            Dim Destinatario As MailAddress = New MailAddress(a)

            Dim correo As New MailMessage
            Dim smtp As New SmtpClient()

            correo.From = New MailAddress("instaladorkubo@gmail.com", "Instalador Kubo", System.Text.Encoding.UTF8)
            correo.To.Add(Destinatario)
            correo.SubjectEncoding = System.Text.Encoding.UTF8
            correo.Subject = "Descargas InstaladorKubo Finalizadas"
            correo.Body = "" & vbCrLf & "Las descargas finalizaron a las " & DateTime.Now.Hour & " horas " & "y " & DateTime.Now.Minute & " minutos." & vbCrLf & "Puedes proceder a realizar las instalaciones cuando quieras." & vbCrLf & "Cualquier duda tienes disponible el Comunicado 1573: http://tecnicos.notin.net/detalles.asp?id=1573" & vbCrLf & "Muchas gracias"
            correo.BodyEncoding = System.Text.Encoding.UTF8
            'correo.IsBodyHtml = False(formato tipo web o normal:  true = web)
            correo.IsBodyHtml = False
            correo.Priority = MailPriority.Normal

            smtp.Credentials = New System.Net.NetworkCredential("instaladorkubo@gmail.com", "juanmola2017")
            smtp.Port = 587
            smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True
            Try
                smtp.Send(correo)
                RegistroInstalacion("Correo de notificación enviado a " & CBoxEmail.Text)
            Catch ex As Exception
                RegistroInstalacion("ERROR Email: " & ex.Message)

            End Try
        Else
            RegistroInstalacion("ADVERTENCIA: No se pudo notificar por correo. La dirección " & CBoxEmail.Text & " no se consideró válida o no se indicó ningunta dirección.")
        End If

    End Sub

#End Region


    Private Sub BtExplorarRutas_Click(sender As Object, e As EventArgs) Handles BtExplorarRutas.Click
        If Directory.Exists(RutaDescargas) Then
            Process.Start("explorer.exe", RutaDescargas)
        End If
    End Sub

    Private Sub CBoxEmail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBoxEmail.LostFocus
        If Validaremail() = True Then
            Dim destinatario As String = CBoxEmail.Text
            cIniArray.IniWrite(instaladorkuboini, "EMAIL", "DESTINATARIO", destinatario)
            RegistroInstalacion("EMAIL: Dirección de correo establecida a " & destinatario)
        Else
            cIniArray.IniWrite(instaladorkuboini, "EMAIL", "DESTINATARIO", "")
        End If
    End Sub

    Private Sub BtDocRequisitos_Click(sender As Object, e As EventArgs) Handles BtDocRequisitos.Click
        Try
            Process.Start("iexplore.exe", "https://docs.google.com/document/d/1NPprOtwrgrz6evWbYzYDfsjGrG9jcPAwX0aNjSkx5wQ/edit?usp=sharing")
            RegistroInstalacion("Hoja de Requisitos Notin visualizada.")
        Catch ex As Exception
            RegistroInstalacion("ERROR Hoja Requisitos: " & ex.Message)
        End Try

    End Sub

    Private Sub BtISL_Click(sender As Object, e As EventArgs) Handles BtISL.Click
        'MessageBox.Show("Funcionalidad en pruebas. Pendiente de revisión por Sánchez", "Instalar ISL", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        FrmConfigurarISL.ShowDialog()
    End Sub

    Private Sub BtSQL2014_Click(sender As Object, e As EventArgs) Handles BtSQL2014.Click
        FrmSQLInstalacion.ShowDialog()
    End Sub

    Private Sub BtConfWord2016ADRA_Click(sender As Object, e As EventArgs) Handles BtConfWord2016ADRA.Click

        Directory.CreateDirectory(RutaDescargas & "Office2016")
        Try
            'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
            My.Computer.Network.DownloadFile(PuestoNotin & "ConfiguraWord2016_RAPP.exe", RutaDescargas & "Office2016\ConfiguraWord2016_RAPP.exe", "juanjo", "Palomeras24", False, 20000, True)
            obtenerrobocopy()
            Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Office2016\ \\NOTINRAPP\F\PRG.INS\notaria.local\ ConfiguraWord2016_RAPP.exe /R:1 /W:1", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Office2016\ \\NOTINRAPP\C$\apps_remoteapp\ ConfiguraWord2016_RAPP.exe /R:1 /W:1", AppWinStyle.Hide, True)
            RegistroInstalacion("CONFIGURA WORD2016 ADRA. Realizada copia hacia notaria.local / apps_remoteapp de NotinRapp.")
            MessageBox.Show("Se ha copiado en distribución ConfiguraWord 2016_ADRA en NotinRapp." & vbCrLf & "Te mostramos la Ruta NR (RDAC). Procede a realizar la ejecución manual del ConfiguraWord2016_ADRA.", "Ejecución de ConfiguraWord2016 ADRA", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Se produjo un error al obtener ConfiguraWord 2016 ADRA. Revisa Log para más detalles.", "Error ConfiguraWord ADRA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR ConfiguraWord 2016 ADRA: " & ex.Message)
            cIniArray.IniWrite(instaladorkuboini, "ADRA", "CONFIGURAWORD2016", "0")
            BtConfWord2016ADRA.BackColor = Color.LightSalmon
            Exit Sub
        End Try

        'Obtener nombre Usuario
        Dim equipousuario As String = (My.User.Name)
        Dim equipo As Integer = equipousuario.LastIndexOf("\")
        Dim usuario = equipousuario.Remove(0, equipo + 1)
        RegistroInstalacion("WORD 2016 ADRA: Obtenido nombre Usuario Adra: " & usuario)

        Try
            'Esta ruta es la antigua. No añado comprobación tiro de la Local.
            'Dim rutaconfword As String = """" & "\\NOTINRAPP\Z\" & usuario & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC)\" & """"
            Dim rutaconfword As String = """" & "C:\Users\" & usuario & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC)\" & """"

            '%systemroot%\system32\mstsc.exe "\\NOTINRAPP\Z\rosa\AppData\Roaming\Microsoft\Workspaces\{CE4B537B-B510-4C8C-80DE-CEACE84BD4B2}\Resource\Configura Word 2016 (NR).rdp"
            If Directory.Exists(rutaconfword) Then
                Process.Start("explorer.exe", rutaconfword)
            Else
                MessageBox.Show("No se pudo determinar/acceder a la ruta NR (RADC) del Usuario " & usuario & ". Intenta acceder manualmente.", "Error acceso Ruta NR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            RegistroInstalacion("ERROR de acceso a Ruta NR (RADC):" & ex.Message)
        End Try

        'Si todo ha ido bien..
        cIniArray.IniWrite(instaladorkuboini, "ADRA", "CONFIGURAWORD2016", "1")
        BtConfWord2016ADRA.BackColor = Color.PaleGreen
        RegistroInstalacion("WORD 2016 ADRA: Proceso ejecutado correctamente. Pendiente ejecución manual del usuario.")
    End Sub




    Private Sub BtSubeBinario_Click(sender As Object, e As EventArgs) Handles BtSubeBinario.Click
        Try
            Dim original As String = "C:\Users\inxid\source\repos\InstaladorKubo\InstaladorKubo\InstaladorKubo\bin\Debug\app.publish\InstaladorKubo.exe"
            My.Computer.Network.UploadFile(original, "ftp://instalador.notin.net/InstaladorKubo.exe", "instalador", "4a9P1dK", True, 20000)
            RegistroInstalacion("Subido binario al FTP para su ejecución en entornos con problemas ClickOnce.")
        Catch ex As Exception
            RegistroInstalacion("Error subida Binario: " & ex.Message & ".")
        End Try
    End Sub


    Private Sub BtLimpiar2003_Click(sender As Object, e As EventArgs) Handles BtLimpiar2003.Click

        Dim limpiar = MessageBox.Show("ADVERTENCIA: Se va a proceder a realizar una LIMPIEZA del paquete Office 2003. Se eliminarán las configuraciones existentes. Esto no es una desinstalación ¿Seguro quieres proceder?.", "Limpiador Office 2003", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If limpiar = DialogResult.Yes Then
            obtenerwget()
            Directory.CreateDirectory(RutaDescargas & "Utiles")
            Dim WGETLimpiar As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 " & PuestoNotin & "Utiles/LimpiarOffice2003.diagcab -O " & RutaDescargas & "Utiles\LimpiarOffice2003.diagcab"
            Shell("cmd /c " & RutaDescargas & WGETLimpiar, AppWinStyle.NormalFocus, True)
            Try
                Process.Start(RutaDescargas & "Utiles\LimpiarOffice2003.diagcab")
                RegistroInstalacion("Limpieza Office2003 ejecutada. Continua el usuario con el proceso.")
                cIniArray.IniWrite(instaladorkuboini, "LIMPIADORES", "OFFICE2003", "1")

            Catch ex As Exception
                RegistroInstalacion("ERROR Limpieza Office 2003: " & ex.Message)
            End Try
        Else
            RegistroInstalacion("Limpieza Office 2003 cancelada por el usuario.")
        End If

    End Sub

    Private Sub BtLimpiar2016_Click(sender As Object, e As EventArgs) Handles BtLimpiar2016.Click
        Dim limpiar = MessageBox.Show("ADVERTENCIA: Se va a proceder a realizar una LIMPIEZA del paquete Office 2016. Se eliminarán las configuraciones existentes. Esto no es una desinstalación ¿Seguro quieres proceder?.", "Limpiador Office 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If limpiar = DialogResult.Yes Then
            obtenerwget()
            Directory.CreateDirectory(RutaDescargas & "Utiles")
            Dim WGETLimpiar As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 " & PuestoNotin & "Utiles/LimpiarOffice2016.diagcab -O " & RutaDescargas & "Utiles\LimpiarOffice2016.diagcab"
            Shell("cmd /c " & RutaDescargas & WGETLimpiar, AppWinStyle.NormalFocus, True)
            Try
                Process.Start(RutaDescargas & "Utiles\LimpiarOffice2016.diagcab")
                RegistroInstalacion("Limpieza Office2016 ejecutada. Continua el usuario con el proceso.")
                cIniArray.IniWrite(instaladorkuboini, "LIMPIADORES", "OFFICE2016", "1")

            Catch ex As Exception
                RegistroInstalacion("ERROR Limpieza Office 2016: " & ex.Message)
            End Try
        Else
            RegistroInstalacion("Limpieza Office 2016 cancelada por el usuario.")
        End If
    End Sub

    Private Sub BtNexus64_Click(sender As Object, e As EventArgs) Handles BtNexus64.Click
        RegistroInstalacion("=== COMIENZO INSTALACIONES NOTIN-NEXUS x64 ===")

        'Comprobar si se ha efectuado alguna descarga
        Dim comienzo = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "COMIENZO", "2")
        Dim rutaenf = cIniArray.IniGet(instaladorkuboini, "PAQUETES", "TRAERDEF", "2")
        If comienzo = 2 AndAlso rutaenf = 2 Then
            lbInstalando.Visible = False
            MessageBox.Show("Descarga o Trae los Paquetes antes de comenzar las Instalaciones.", "¿Descargaste los paquetes?", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            Exit Sub
        End If

        Dim claveinift = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "REGFT", "2")
        If claveinift = 2 Then
            If File.Exists(RutaDescargas & "Registro\FTComoAdministrador.reg") = False Then
                Directory.CreateDirectory(RutaDescargas & "Registro")
                My.Computer.Network.DownloadFile(PuestoNotin & "FTComoAdministrador.reg", RutaDescargas & "Registro\FTComoAdministrador.reg", "juanjo", "Palomeras24", False, 60000, True)
            End If
            Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "1")

            'Deshabilitar SMARTSCREEN. Problemas tras el reinicio en ClickOnce
            Try
                My.Computer.Network.DownloadFile(PuestoNotin & "SmartScreen.reg", RutaDescargas & "SmartScreen.reg", "juanjo", "Palomeras24", False, 10000, True)
            Catch ex As Exception
                RegistroInstalacion("No se pudo obtener SmartScreen REG del FTP: " & ex.Message & ".")
            End Try

            Try
                Process.Start("regedit.exe", "/s " & RutaDescargas & "SmartScreen.reg")
                RegistroInstalacion("Deshabilitar SmartScreen. Permitir ejecución en entornos con problemas ClickOnce.")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                RegistroInstalacion("SmartScreen no se pudo importar: " & ex.Message)
            End Try
            PbInstalaciones.Visible = True
            Dim p As Integer
            While p < 6
                p = p + 1
                Threading.Thread.Sleep(600)
                PbInstalaciones.PerformStep()
            End While

            Dim claveregft As DialogResult
            claveregft = MessageBox.Show("Se importaron Claves de Registro y será recomendable REINICIAR antes de proceder con la Instalación. ¿Realizamos la preparación inicial?", "Importar Claves y Reiniciar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If claveregft = DialogResult.Yes Then

                MessageBox.Show("REINICIO NECESARIO. Guarda tu trabajo.", "Reinicio inicial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dim reinicio As String = "shutdown /r /f /t 0"
                File.WriteAllText(RutaDescargas & "primerreinicio.bat", reinicio)
                RegistroInstalacion("Procedemos a Reiniciar el equipo para la preparación inicial.")
                RunAsAdmin(RutaDescargas & "primerreinicio.bat")
                Exit Sub
            ElseIf claveregft = DialogResult.No Then
                MessageBox.Show("Continuamos con el resto de instalaciones.", "Reinicio cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "0")
                RegistroInstalacion("ADVERTENCIA: No se ejecutó el Reinicio tras importar las Claves de Registro.")
                lbInstalando.Visible = False
            End If
        ElseIf claveinift = 1 OrElse claveinift = 0 Then
        End If

        'Mientras unidad F no valida y usuario pulse reintentar

        Dim QueHacerF As DialogResult
        While UnidadF() = False
            QueHacerF = MessageBox.Show("Unidad F no conectada. Habrá procesos que no se podrán completar y se omitirán.", "Advertencia Unidad F", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)
            If QueHacerF = DialogResult.Abort Then
                lbInstalando.Visible = False
                Exit Sub
            ElseIf QueHacerF = DialogResult.Ignore Then
                RegistroInstalacion("ADVERTENCIA: Comenzaste la Instalación sin Conectar la Unidad F.")
                Exit While
            End If
        End While
        ' CONTROLAR PULSAR ABORTAR


        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
        End If

        obtenerunrar()

        Dim NotinSiNo As Integer = Nothing

        Dim EjecutableAccess As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE11\MSACCESS.EXE")

        If EjecutableAccess = False Then
            RegistroInstalacion("No se encontro Office 2003 instalado. Se procede a la instalación automatizada.")
            InstalarNotinNetx64()

        Else
            NotinSiNo = MessageBox.Show("Posible instalación existente de NOTIN .NET (Access 2003). ¿Ejecutar instalación Office 2003?", "Instalación Office 2003", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If NotinSiNo = DialogResult.Yes Then
                RegistroInstalacion("Se encontró el paquete Office pero se procede a su reinstalación.")
                InstalarNotinNetx64()
            Else
                RegistroInstalacion("Omitida instalación Office 2003. Pasamos a la instalación de los Pre-Requisitos.")
                InstalarRequisitosNetx64()
            End If
        End If

    End Sub


    Private Sub InstalarNotinNetx64()

        'Claves Registro
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ConfigAccess.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\VentanasSigno.reg", AppWinStyle.Hide, True)
        'Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ExclusionDefender.reg", AppWinStyle.Hide, True)
        RegistroInstalacion("Importadas Claves Registro para Notin.")

        If File.Exists(RutaDescargas & "PuestoNotinC.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PuestoNotinC.exe " & "C:\", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "ScanImg_Beta_FT.exe", AppWinStyle.Hide, False)
            'Shell("cmd.exe /c " & "C:\Notawin.Net\FT.exe /actualizaciones", AppWinStyle.Hide, False)
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete PuestoNotinC. Se suprimió su instalación.")
        End If

        If File.Exists(RutaDescargas & "Office2003.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2003.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            'Setup MST que personaliza la instalación de Office 2003
            File.Copy(RutaDescargas & "Setup.mst", RutaDescargas & "Office2003\Setup.mst", True)
            '  Shell("C:\WINDOWS\system32\notepad.exe " & RutaDescargas & "Office2003\NSERIE.TXT", AppWinStyle.NormalFocus, False)
            'Esperamos 5 segundos a que se complete la copia.
            Threading.Thread.Sleep(3000)

            Shell("cmd.exe /C " & RutaDescargas & "Office2003\setup.exe TRANSFORMS=" & RutaDescargas & "Office2003\Setup.mst /qb-", AppWinStyle.Hide, True)
            RegistroInstalacion("Realizada instalación desatendida de Office 2003.")
            ' Shell("cmd.exe /C taskkill /f /im notepad.exe", AppWinStyle.Hide, False)

            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\Office2003SP3-KB923618-FullFile-ESN.exe" & """" & " /q", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\MSACCESS.msp" & """" & " /passive", AppWinStyle.Hide, True)
            RegistroInstalacion("Instalados SP3 y Parche Access para Office 2003.")
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ERROR: No se encontró el Paquete OFFICE2003.EXE ¿Seguro que lo descargaste?")
        End If


        'Copiar Referencia Outlook
        Try
            Dim msoutlxcopy As String = "xcopy /F /Y /C "
            Dim msoutlorigen As String = RutaDescargas & "Registro\MSOUTL.OLB "
            Dim msoutldestino As String = " ""C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\"" "
            File.WriteAllText(RutaDescargas & "Registro\msoutl.bat", msoutlxcopy & msoutlorigen & msoutldestino)

            RunAsAdmin(RutaDescargas & "Registro\msoutl.bat")
            RegistroInstalacion("Copiada libreria Outlook necesaria para Notin.")
        Catch ex As Exception
            RegistroInstalacion("ERROR MSOUTLB: " & ex.Message)
        End Try
        'Try
        '    File.Copy(RutaDescargas & "Registro\MSOUTL.OLB", "C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\MSOUTL.OLB", True)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

        InstalarRequisitosNetx64()
    End Sub

    Private Sub InstalarRequisitosNetx64()
        Try
            Dim requisitosini = cIniArray.IniGet(instaladorkuboini, "REQUISITOS", "NET", "2")

            If requisitosini = "2" Then
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\KryptonSuite300.msi /passive", AppWinStyle.Hide, True)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\Office2003PrimaryInterop.msi /passive", AppWinStyle.Hide, True)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2005.exe /q", AppWinStyle.Hide, True)
                Threading.Thread.Sleep(15000)
                Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2015.exe /q", AppWinStyle.Hide, True)
                Threading.Thread.Sleep(15000)
                cIniArray.IniWrite(instaladorkuboini, "REQUISITOS", "NET", "1")
                RegistroInstalacion("Instalados Pre-Requisitos .Net")
            Else
                RegistroInstalacion("Pre-Requisitos no instalados. Se detectó instalación previa.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR Requisitos Net: " & ex.Message)
        End Try
        InstalarWord2016x64()
    End Sub

    Private Sub InstalarWord2016x64()
        Dim EjecutableWordx64 As Boolean = File.Exists("C:\Program Files\Microsoft Office\OFFICE16\WINWORD.EXE") OrElse File.Exists("C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE")

        If EjecutableWordx64 = False Then
            If File.Exists(RutaDescargas & "Office2016x64.rar") Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016x64.rar " & RutaDescargas, AppWinStyle.NormalFocus, True)
                RegistroInstalacion("No se encuentra instalación previa de Office 2016 x64. Procedemos a realizarla.")
                Try
                    File.Copy(RutaDescargas & "setup2016x64.MSP", RutaDescargas & "Office2016x64\setup2016x64.MSP", True)
                Catch ex As Exception
                    MessageBox.Show("Error al copiar fichero MSP x64. Revisa que el fichero exista en " & RutaDescargas & "Office2016 x64", "Error Setup x64 MSP", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RegistroInstalacion("Setup2016x64.MSP: " & ex.Message)
                End Try
                Threading.Thread.Sleep(3000)
                Shell("cmd.exe /C " & RutaDescargas & "Office2016x64\SETUP.EXE /adminfile setup2016x64.MSP", AppWinStyle.Hide, True)
                RegistroInstalacion("Se procedió a realizar la instalación Desatendida de Office 2016 x64.")

            Else
                RegistroInstalacion("ADVERTENCIA: No se determinó la preferencia de instalación para Office 2016. Por defecto realizamos la ODT.")
            End If
        Else
            MessageBox.Show("Se ha encontrado una instalación previa de Office 2016 x64. Se omite su instalación.", "Instalación Office 2016 x64", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        EjecutableNotinNetx64()
    End Sub


    Private Sub EjecutableNotinNetx64()
        'Escribir en el INI que el Sistema es 64bits
        If File.Exists("C:\Notawin.Net\notin.ini") Then
            cIniArray.IniWrite("C:\Notawin.Net\notin.ini", "Sistema", "PlataformaAddin", "64")
            RegistroInstalacion("PlataformaAddin=64 escrito en el INI local de C:\Notawin.Net.")
        Else
            RegistroInstalacion("ERROR: PlataformaAddin=64 no pudo escribirse en el INI Local. Revisa su existencia y permisos.")
        End If

        If UnidadF() = True Then
            Try
                Directory.CreateDirectory(RutaDescargas & "NotinNet")
                File.Copy("F:\NOTAWIN.NET\x64\NotinNetInstaller.exe", RutaDescargas & "NotinNet\NotinNetInstaller.exe", True)
                RegistroInstalacion("NotinNetInstaller x64 copiado correctamente desde F:\Notawin.Net\x64\ para su ejecución.")
            Catch ex As Exception
                RegistroInstalacion("NotinNetInstaller x64: No se pudo obtener de F:\Notawin.Net\x64\ se procede a su decarga desde static.unidata")
            End Try

            If File.Exists(RutaDescargas & "NotinNet\NotinNetInstaller.exe") = False Then
                'RegistroInstalacion("NotinNetInstaller no encontrado. Se procede a su descarga.")
                Try
                    Directory.CreateDirectory(RutaDescargas & "NotinNet")
                    Dim urlnotinnetx64 As String = "http://static.unidata.es/NotinNetInstaller/x64/beta/NotinNetInstaller.exe"
                    Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress " & urlnotinnetx64 & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
                    RegistroInstalacion("NotinNet x64: Realizada descargar desde su url. Prosigue su instalación.")
                Catch ex As Exception
                    RegistroInstalacion("NotinNetInstaller x64: No se pudo obtener desde su url de descarga. Seguirán errores de Addins.")
                End Try
            End If

            Try
                Dim pnotinnet As New ProcessStartInfo()
                pnotinnet.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
                Dim notinnet As Process = Process.Start(pnotinnet)
                'notinnet.WaitForInputIdle()
                notinnet.WaitForExit()
                RegistroInstalacion("NotinNetInstaller x64: Ejecutado proceso de instalación.")
            Catch ex As Exception
                RegistroInstalacion("ERROR NotinNetInstaller x64: " & ex.Message)
            End Try

            Try
                File.Copy("F:\NOTIN8.mde", "C:\Notawin.Net\notin8.mde", True)
            Catch ex As Exception
                RegistroInstalacion("ERROR: Notin8.mde " & ex.Message)
            End Try

            Try
                File.Copy("F:\NOTIN\PLANTILLAS\NORMAL.DOTM", "C:\PLANTILLAS\NORMAL.DOTM", True)
            Catch ex As Exception
                RegistroInstalacion("ERROR: Normal.dotm " & ex.Message)
                'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
        ObtenerVersionNet()
        ConfigurarWord2016x64()
    End Sub

    Private Sub ConfigurarWord2016x64()
        If File.Exists(RutaDescargas & "ConfWord2016x64.rar") Then

            If UnidadF() = True Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "ConfWord2016x64.rar " & RutaDescargas & "Office2016x64\", AppWinStyle.Hide, True)
                'Dim ConfigurarWord = MessageBox.Show("¿Configuramos Word 2016?", "Configurar Word 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                'If ConfigurarWord = DialogResult.Yes Then
                Try
                    Process.Start("C:\Program Files (x86)\Humano Software\Notin\Compatibilidad\ReferNet.exe")
                    Threading.Thread.Sleep(5000)
                Catch ex As Exception
                    'MessageBox.Show(ex.Message, "Revisa Instalacion de NotinNET. Continuamos.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016X64", "0")
                    RegistroInstalacion("ReferNet: " & ex.Message)
                End Try

                'Instalacion de los Addins. Hay que forzarlo.
                Try
                    'Crear una nueva estructura ProcessStartInfo.
                    Dim pInfoaddin As New ProcessStartInfo()
                    'Establecer el miembro de un nombre de archivo de pinfo como Eula.txt en la carpeta de sistema.
                    pInfoaddin.FileName = "C:\Program Files (x86)\Humano Software\Notin\Addins\NotinAddin\NotinAddinInstaller.exe"
                    'Ejecutar el proceso.
                    Dim notinaddin As Process = Process.Start(pInfoaddin)
                    'Esperar a que la ventana de proceso complete la carga.
                    'notinaddin.WaitForInputIdle()
                    'Esperar a que el proceso termine.
                    notinaddin.WaitForExit()
                    'Continuar con el código.
                Catch ex As Exception
                    RegistroInstalacion("ERROR NotinAddin: " & ex.Message)
                    cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016X64", "0")
                End Try
                Try
                    Dim pInfotaskpane As New ProcessStartInfo()
                    pInfotaskpane.FileName = "C:\Program Files (x86)\Humano Software\Notin\Addins\NotinTaskPane\NotinTaskPaneInstaller.exe"
                    Dim notintaskpane As Process = Process.Start(pInfotaskpane)
                    'notintaskpane.WaitForInputIdle()
                    notintaskpane.WaitForExit()
                Catch ex As Exception
                    RegistroInstalacion("ERROR NotinTaskPane: " & ex.Message)
                    cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016X64", "0")
                End Try
                'Shell("cmd.exe /C " & RutaDescargas & "ConfiguraWord2016.exe", AppWinStyle.NormalFocus, True)
                'RunAsAdmin(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
                Process.Start(RutaDescargas & "Office2016x64\ConfWord2016x64\ConfiguraWord2016.bat")
                'Threading.Thread.Sleep(10000)

                Try
                    Dim expedientes As String = cIniArray.IniGet("F:\WINDOWS\NNotin.ini", "Expedientes", "Ruta", "\\SERVIDOR\I\")
                    expedientes = expedientes.Remove(0, 2)
                    Dim unidadi = expedientes.LastIndexOf("\I")
                    expedientes = expedientes.Substring(0, unidadi)

                    cIniArray.IniWrite(instaladorkuboini, "RUTAS", "EXPEDIENTES", expedientes)
                    RegistroInstalacion("RUTA Expedientes añadida al INI del Instalador.")

                    Directory.CreateDirectory(RutaDescargas & "Registro")
                    Dim claveregistroservidor As String = """" & "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Office\16.0\Word\Security\Trusted Locations\Location3" & """" & " /v Path /t REG_SZ /d \\" & expedientes & "\F" & " /f"
                    File.WriteAllText(RutaDescargas & "Registro\unidadfword.bat", "reg add ")
                    File.AppendAllText(RutaDescargas & "Registro\unidadfword.bat", claveregistroservidor)

                    Process.Start("regedit", "/s " & RutaDescargas & "Office2016\ConfWord2016\w16recopregjj.reg")
                    RunAsAdmin(RutaDescargas & "Registro\unidadfword.bat")
                    cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016X64", "1")
                    RegistroInstalacion("Configura WORD: Unidad F y W16REG ejecutados.")

                Catch ex As Exception
                    RegistroInstalacion("Ruta Expedientes UnidadF-WORD y W16REG: " & ex.Message)
                End Try


            Else
                MessageBox.Show("Unidad F desconectada. No se puede configurar Word 2016 x64.", "Configura WORD 2016 x64", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016X64", "0")
            End If
        Else
            RegistroInstalacion("ERROR: No se pudo Configurar WORD 2016 x64. No se encontró la descarga de la Utilidad.")
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016X64", "0")
        End If
        'TODO preparar esto también para la versión de 64bits

        SoftwareAncertx64()
        'KMSPico()
    End Sub


    Private Sub SoftwareAncertx64()
        If File.Exists(RutaDescargas & "SFeren-2.8.exe") OrElse File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
            Dim Ancert As Integer = Nothing
            Ancert = MessageBox.Show("¿Instalar Software Ancert?", "Sferen y Pasarela", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Ancert = 6 Then
                If File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
                    Shell("cmd.exe /c " & RutaDescargas & "SFeren-2.8.exe", AppWinStyle.Hide, True)
                    RegistroInstalacion("SFEREN: Lanzado Instalador.")
                Else
                    RegistroInstalacion("ADVERTENCIA: Paquete Sferen no encontrado. No se instalará.")
                End If
                If File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
                    Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PasarelaSigno.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                    Shell("cmd.exe /c " & RutaDescargas & "PasarelaSigno\setup.exe", AppWinStyle.Hide, True)
                    RegistroInstalacion("PASARELA: Lanzado Instalador.")
                Else
                    RegistroInstalacion("ADVERTENCIA: Instalable PasarelaSigno no encontrado. No se instalará.")
                End If
            End If
        Else
            RegistroInstalacion("Software ANCERT no descargado. Se omite su instalación.")
        End If
        JNemox64()
    End Sub

    Private Sub JNemox64()
        'TODO añadir comprobación 32bits aqui y en Office 2003
        If Directory.Exists("c:\Program Files (x86)\Java") = False Then
            'Descarga de JAVA 1.8.171
            Directory.CreateDirectory(RutaDescargas & "Software")
            Dim urljava As String = "http://javadl.oracle.com/webapps/download/AutoDL?BundleId=233170_512cd62ec5174c3487ac17c61aaa89e8"
            Dim wgetjava As String = "wget.exe -q --show-progress -t 5 -c "
            Shell("cmd.exe /c " & RutaDescargas & wgetjava & urljava & " -O " & RutaDescargas & "Software\java8.exe", AppWinStyle.Hide, True)
            Try
                Dim pinstalajava As New ProcessStartInfo()
                pinstalajava.FileName = RutaDescargas & "Software/java8.exe"
                pinstalajava.Arguments = "/s WEB_JAVA_SECURITY_LEVEL=M"
                Dim instalajava As Process = Process.Start(pinstalajava)
                instalajava.WaitForExit()
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "JAVA8", "1")
                BtJava.BackColor = Color.PaleGreen
                RegistroInstalacion("Instalada última versión de JAVA para jNemo.")
            Catch ex As Exception
                MessageBox.Show("No se pudo instalar JAVA. Instálalo manualmente al terminar. Puedes usar este mismo Instalador.", "Error Java", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR Java jNemo: " & ex.Message)
            End Try
        End If

        If File.Exists(RutaDescargas & "jnemo-latest.exe") Then
            If File.Exists("c:\Program Files (x86)\jNemo\jNemo.exe") = False Then
                Dim instalajnemo As New Process
                instalajnemo.StartInfo.FileName = RutaDescargas & "jnemo-latest.exe"
                'MiProceso.StartInfo.Arguments = "1664"
                instalajnemo.Start() 'iniciar el proceso
                'MiProceso.Kill()
                'MiProceso.Dispose()
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "JNEMO", "1")
            End If
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ADVERTENCIA: Se encontró el ejecutable jNemo. Se omite su instalación.")
        End If
        FTx64()
    End Sub

    Private Sub FTx64()
        Try
            ' Shell("cmd.exe /c " & "C:\Notawin.Net\FT.exe /actualizaciones", AppWinStyle.Hide, False)
            Process.Start("C:\NOTAWIN.NET\FT.EXE", "/actualizaciones")
            RegistroInstalacion("Instalando Paquetes de FT.")
        Catch ex As Exception
            RegistroInstalacion("Paquetes FT. No se pudieron instalar: " & ex.Message)
        End Try

        If File.Exists(RutaDescargas & "PaquetesFT.rar") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PaquetesFT.rar " & RutaDescargas, AppWinStyle.NormalFocus, True)

            Try
                Process.Start(RutaDescargas & "PaquetesFT\BarCodex.exe")
                Process.Start(RutaDescargas & "PaquetesFT\catastrowsclient-setup.exe")
                Process.Start(RutaDescargas & "PaquetesFT\NotinScrap-setup.exe")
                RegistroInstalacion("Instalados Paquetes esenciales de FT.")
            Catch ex As Exception
                RegistroInstalacion("ERROR PaquetesFT: " & ex.Message)
            End Try

            'Mailer-Setup
            Try
                Dim mailerxcopy As String = "xcopy /F /Y /C "
                Dim mailerorigen As String = RutaDescargas & "PaquetesFT\MailerCOM.dll "
                If Directory.Exists("C:\Windows\SysWOW64") Then
                    Dim mailerdestino As String = " ""C:\Windows\SysWOW64\"" "
                    File.WriteAllText(RutaDescargas & "PaquetesFT\mailersetup.bat", mailerxcopy & mailerorigen & mailerdestino)
                    RegistroInstalacion("Mailer-Setup: copiada Referencia en Sistema de 64bits.")
                ElseIf Directory.Exists("C:\Windows\System32") Then
                    Dim mailerdestino As String = " ""C:\Windows\System32\"" "
                    RegistroInstalacion("Mailer-Setup: copiada Referencia en Sistema de 32bits.")
                    File.WriteAllText(RutaDescargas & "PaquetesFT\mailersetup.bat", mailerxcopy & mailerorigen & mailerdestino)
                Else
                    RegistroInstalacion("ERROR Paquete Mailer-Setup: No he podido determinar Sistema 32/64bits.")
                End If

                Try
                    RunAsAdmin(RutaDescargas & "PaquetesFT\mailersetup.bat")
                    RegistroInstalacion("DLL MailerCOM Registrada correctamente.")
                Catch ex As Exception
                    RegistroInstalacion("ERROR DLL Mailer-Setup: " & ex.Message)
                End Try
            Catch ex As Exception
            End Try
        Else
            RegistroInstalacion("PaquetesFT no descargado. Se omite su instalación.")
        End If

        AccesosDirectosEscritoriox64()
    End Sub

    Private Sub AccesosDirectosEscritoriox64()
        Dim Escritorio As String = """" & My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & """"
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe e -y " & RutaDescargas & "AccesosDirectosx64.exe " & Escritorio, AppWinStyle.Hide, True)
        RegistroInstalacion("Creados Accesos Directos en el Escritorio. Ruta Escritorio: " & Escritorio)
        Try
            File.Delete("C:\Users\Public\Desktop\Krypton Explorer.lnk")
        Catch ex As Exception
        End Try
        'AbreExcel()

        lbInstalando.Visible = False
        PbInstalaciones.Visible = False
        MessageBox.Show("INSTALACIONES TERMINADAS. Se recomienda REINICIAR el equipo. Consulta el Registro de Instalación para más detalles.", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        RegistroInstalacion("=== FINALIZADAS INSTALACIONES NOTIN+NEXUS x64 ===")


    End Sub

    Private Sub BtMigradorSQL_Click(sender As Object, e As EventArgs) Handles BtMigradorSQL.Click
        Dim SistemaO = (My.Computer.Info.OSFullName)
        RegistroInstalacion("MigradorSQL ejecutado en entorno " & SistemaO)
        If SistemaO.Contains("2003") Then
            Dim win2003 = MessageBox.Show("MigradorSQL no compatible con Windows 2003 Server. ¿Quieres continuar?", "Posible Windows 2003 Server", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If win2003 = DialogResult.No Then
                RegistroInstalacion("MigradorSQL: Ejecución en Windows 2003 Server. Usuario cancela la operación.")
                Exit Sub
            End If
        End If

        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 https://static.unidata.es/MigradorNotinSQL.exe -O " & RutaDescargas & "NotinNet\MigradorNotinSQL.exe", AppWinStyle.Hide, True)
        'Process.Start(RutaDescargas & "NotinNet\MigradorNotinSQL.exe", "/allowdataloss")
        Try
            File.WriteAllText(RutaDescargas & "NotinNet\MigradorNotinSQLAllowDataLoss.bat", "@echo off" & vbCrLf & RutaDescargas & "NotinNet\MigradorNotinSQL.exe /allowdataloss")
            Dim pmigrador As New ProcessStartInfo()
            pmigrador.FileName = RutaDescargas & "NotinNet\MigradorNotinSQLAllowDataLoss.bat"
            Dim migrador As Process = Process.Start(pmigrador)
            migrador.WaitForExit()
            RegistroInstalacion("ÉXITO: Ejecución de MigradorNotinSQL completada. Revisa el Log del mismo para más detalle.")
            'BtMigradorSQL.BackColor = Color.PaleGreen
            'BtMigradorLOG.Visible = True
            'cIniArray.IniWrite(instaladorkuboini, "SQL", "MIGRADOR", "1")
            cIniArray.IniWrite(instaladorkuboini, "SQL", "EJECUCIONMIGRADOR", DateTime.Now)
            'LbMigrador.Text = cIniArray.IniGet(instaladorkuboini, "SQL", "FECHAMIGRADOR", "Sin determinar")
            LbVersionMigrador.Visible = True
            TbMigradorLog.Visible = True
            'BtMigradorLOG.Visible = True
            LeerLogMigradorSQL()


            If TbMigradorLog.Text.Contains("EXITO") OrElse TbMigradorLog.Text.Contains("ExitCode = 0") Then
                'Dim notin8 = cIniArray.IniGet(instaladorkuboini, "NET", "NOTIN8", "2")
                'If notin8 = 2 Then
                Dim descarganotin8 = MessageBox.Show("¿Quieres Descargar Notaría (Notin8.exe)?", "Descarga Notaría", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If descarganotin8 = DialogResult.Yes Then
                    DescargarNotaria()
                    'BtNotin8exe.BackColor = Color.PaleGreen
                    'cIniArray.IniWrite(instaladorkuboini, "NET", "NOTIN8", "1")
                    If descarganotin8 = DialogResult.No Then
                        '       cIniArray.IniWrite(instaladorkuboini, "NET", "NOTIN8", "0")
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            RegistroInstalacion("ERROR ejecución MigradorSQL: " & ex.Message)
            BtMigradorSQL.BackColor = Color.LightSalmon
        End Try
    End Sub

    Private Sub TbMigradorLog_MouseClick(sender As Object, e As MouseEventArgs) Handles TbMigradorLog.MouseClick
        Process.Start("notepad.exe", "C:\Program Files (x86)\Humano Software\MigradorSQL\Log\LoggerMigradorNotin.txt")
    End Sub

    Private Sub BtMigradorDeploy_Click(sender As Object, e As EventArgs) Handles BtMigradorDeploy.Click
        Dim SistemaO = (My.Computer.Info.OSFullName)
        RegistroInstalacion("MigradorSQL ejecutado en entorno " & SistemaO)
        If SistemaO.Contains("2003") Then
            Dim win2003 = MessageBox.Show("MigradorSQL no compatible con Windows 2003 Server. ¿Quieres continuar?", "Posible Windows 2003 Server", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If win2003 = DialogResult.No Then
                RegistroInstalacion("MigradorSQL: Ejecución en Windows 2003 Server. Usuario cancela la operación.")
                Exit Sub
            End If
        End If

        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 https://static.unidata.es/MigradorNotinSQL.exe -O " & RutaDescargas & "NotinNet\MigradorNotinSQL.exe", AppWinStyle.Hide, True)
        'Process.Start(RutaDescargas & "NotinNet\MigradorNotinSQL.exe", "/allowdataloss")
        Try
            File.WriteAllText(RutaDescargas & "NotinNet\MigradorNotinSQLForceAutomaticDeploy.bat", "@echo off" & vbCrLf & RutaDescargas & "NotinNet\MigradorNotinSQL.exe /forceautomaticdeploy")
            Dim pmigrador As New ProcessStartInfo()
            pmigrador.FileName = RutaDescargas & "NotinNet\MigradorNotinSQLForceAutomaticDeploy.bat"
            Dim migrador As Process = Process.Start(pmigrador)
            migrador.WaitForExit()
            RegistroInstalacion("ÉXITO: Ejecución de MigradorNotinSQL ForceAutomaticDeploy completada. Revisa el Log del mismo para más detalle.")
            'BtMigradorSQL.BackColor = Color.PaleGreen
            'BtMigradorLOG.Visible = True
            'cIniArray.IniWrite(instaladorkuboini, "SQL", "MIGRADOR", "1")
            'cIniArray.IniWrite(instaladorkuboini, "SQL", "EJECUCIONMIGRADOR", DateTime.Now)
            'LbMigrador.Text = cIniArray.IniGet(instaladorkuboini, "SQL", "FECHAMIGRADOR", "Sin determinar")
            LbVersionMigrador.Visible = True
            TbMigradorLog.Visible = True
            'BtMigradorLOG.Visible = True
            BtMigradorDeploy.BackColor = Color.PaleGreen
            LeerLogMigradorSQL()


            If TbMigradorLog.Text.Contains("EXITO") OrElse TbMigradorLog.Text.Contains("ExitCode = 0") Then
                'Dim notin8 = cIniArray.IniGet(instaladorkuboini, "NET", "NOTIN8", "2")
                'If notin8 = 2 Then
                Dim descarganotin8 = MessageBox.Show("¿Quieres Descargar Notaría (Notin8.exe)?", "Descarga Notaría", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If descarganotin8 = DialogResult.Yes Then
                    DescargarNotaria()
                    'BtNotin8exe.BackColor = Color.PaleGreen
                    'cIniArray.IniWrite(instaladorkuboini, "NET", "NOTIN8", "1")
                    If descarganotin8 = DialogResult.No Then
                        '       cIniArray.IniWrite(instaladorkuboini, "NET", "NOTIN8", "0")
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            RegistroInstalacion("ERROR ejecución MigradorSQL: " & ex.Message)
            BtMigradorDeploy.BackColor = Color.LightSalmon
        End Try
    End Sub


    Private Sub LeerLogMigradorSQL()

        Dim loggermigrador As String = "C:\Program Files (x86)\Humano Software\MigradorSQL\Log\LoggerMigradorNotin.txt"
        If File.Exists(loggermigrador) Then
            LbVersionMigrador.Visible = True
            TbMigradorLog.Visible = True
            'BtMigradorLOG.Visible = True

            ':::Creamos nuestro objeto de tipo StreamReader que nos permite leer archivos
            Dim leer As New StreamReader("C:\Program Files (x86)\Humano Software\MigradorSQL\Log\LoggerMigradorNotin.txt")
            Try
                ':::Indicamos mediante un While que mientras no sea el ultimo caracter repita el proceso
                While leer.Peek <> -1
                    ':::Leemos cada linea del archivo TXT
                    Dim linea As String = leer.ReadLine
                    'Dim linea As String = leer.ReadToEnd()
                    ':::Validamos que la linea no este vacia
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If
                    ':::Agregramos los registros encontrados (nos quedamos con el último)
                    TbMigradorLog.Text = linea.ToString
                    If linea.Contains("EXITO") OrElse linea.Contains("ExitCode = 0") Then
                        BtMigradorSQL.BackColor = Color.PaleGreen
                    ElseIf linea.Contains("finalizado") OrElse linea.Contains("ExitCode = 1") Then
                        BtMigradorSQL.BackColor = Color.LightSalmon
                    ElseIf linea.Contains("horaria 4 - 6") OrElse linea.Contains("horaria 0 - 7") OrElse linea.Contains("ExitCode = -1") Then
                        BtMigradorSQL.BackColor = Color.Khaki
                    Else
                        BtMigradorSQL.BackColor = SystemColors.Control
                    End If
                End While

                leer.Close()
                RegistroInstalacion("MigradorSQL: Leído correctamente Log. Pasada última línea a TextBox.")
            Catch ex As Exception
                RegistroInstalacion("MigradorSQL: No se pudo leer el archivo LOG. No se puede mostrar resultado última ejecución.")
            End Try
            LbVersionMigrador.Visible = True
            TbMigradorLog.Visible = True
            'BtMigradorLOG.Visible = True
        Else
            RegistroInstalacion("MigradorSQL: No se pudo acceder a fichero Log. No se puede mostrar resultado última ejecución.")
        End If


    End Sub

    Private Sub BtNotin8exe_Click(sender As Object, e As EventArgs) Handles BtNotin8exe.Click
        DescargarNotaria()
    End Sub

    Private Sub DescargarNotaria()
        If NotinRapp() = True Then
            Dim notinrapp = MessageBox.Show("Se va a proceder a Descargar y Ejecutar NOTIN8.exe en host NOTINRAPP. ¿Estás seguro?", "NotinNet en AdRa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            RegistroInstalacion("ADRA: Ejecutando Notin8.exe en entorno NotinRapp. Se advierte al usuario.")
            If notinrapp = DialogResult.No Then
                RegistroInstalacion("ADRA: Operación Cancelada. Hice bien en preguntar... (Gracias DPerez).")
                Exit Sub
            End If
        End If

        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        'TODO Versión para 32 bits. Leer INI local o global para Descargar uno u otro. Comparar ambos ini.
        Dim urlnotin8 = "http://static.unidata.es/NotariaEvo/v40/notin8.exe"
        'Shell("cmd.exe /c " & RutaDescargas & "wget.exe " & urlnotin8 & " -O " & RutaDescargas & "NotinNet\Notin8.exe")
        Dim WGETNOTIN8 As String = "wget.exe -q --show-progress -t 5 " & urlnotin8 & " -O " & RutaDescargas & "NotinNet\Notin8.exe"
        Dim RutaCMDWgetNotin8 As String = RutaDescargas & WGETNOTIN8
        Shell("cmd.exe /c " & RutaCMDWgetNotin8, AppWinStyle.NormalFocus, True)

        Try
            Dim pnotin8 As New ProcessStartInfo()
            pnotin8.FileName = RutaDescargas & "NotinNet\Notin8.exe"
            Dim notin8 As Process = Process.Start(pnotin8)
            notin8.WaitForExit()
            RegistroInstalacion("ÉXITO: NOTIN8 ejecutado correctamente.")
            LeerLogMigradorSQL()
        Catch ex As Exception
            BtNotin8exe.BackColor = Color.LightSalmon
            RegistroInstalacion("ERROR NOTIN8: " & ex.Message)
        End Try

        If UnidadF() = True Then
            Try
                obtenerrobocopy()
                Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "NotinNet\ F:\ Notin8.exe", AppWinStyle.NormalFocus, True)
                RegistroInstalacion("Notin8.exe copiado correctamente a F:\ para futuras ejecuciones.")
            Catch ex As Exception
                RegistroInstalacion("ERROR Notin8.exe no se pudo copiar a F. Causa: " & ex.Message)
                BtNotin8exe.BackColor = Color.LightSalmon
            End Try
        Else
            RegistroInstalacion("Notin8 no copiado a F: al no encontrarse la Unidad disponible.")
        End If

        If ProcesosActivos() = True Then
            Dim procesosactivos As DialogResult = MessageBox.Show("Hay procesos en ejecución que puden interrumpir la instalación de NotinNet. Si continúas se forzará su cierre. ¿Proseguimos con la instalación?", "Procesos .Net detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If procesosactivos = DialogResult.Yes Then
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
                Try
                    Dim pnotinnet As New ProcessStartInfo()
                    pnotinnet.FileName = "F:\NOTAWIN.NET\NotinNetInstaller.exe"
                    Dim notinnet As Process = Process.Start(pnotinnet)
                    notinnet.WaitForExit()
                    RegistroInstalacion("ÉXITO: NOTIN NET ejecutado correctamente desde F:\Notawin.Net tras la descarga de Notin8.exe.")
                    ObtenerVersionNet()
                Catch ex As Exception
                    'BtEstableNet.BackColor = Color.LightSalmon
                    RegistroInstalacion("ERROR NOTIN NET: No se pudo ejecutar NotinNetInstaller de F tras la descarga de Notin8.exe.")
                    BtNotin8exe.BackColor = Color.LightSalmon
                End Try
            Else
                BtNotin8exe.BackColor = Color.LightSalmon
                MessageBox.Show("Actualización Versión Notin8 finalizada con errores." & vbCrLf & "Consulta Logger para mas detalles.", "Actualizar Notaría", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If

        MessageBox.Show("Actualización Versión Notin8 finalizada correctamente.", "Actualizar Notaría", MessageBoxButtons.OK, MessageBoxIcon.Information)
        BtNotin8exe.BackColor = Color.PaleGreen

        'Como no puedo comprobar que versión de Net tiene dejo todas en gris
        BtNetBeta.BackColor = SystemColors.Control
        BtEstablex64F462.BackColor = SystemColors.Control
        BtEstableNet.BackColor = SystemColors.Control
        BtNetBetax64F462.BackColor = SystemColors.Control
        BtNetBetaW32F462.BackColor = SystemColors.Control

    End Sub

    Private Sub BtNotin8exeForzar_Click(sender As Object, e As EventArgs) Handles BtNotin8exeForzar.Click
        Dim forzarnotin8 As DialogResult = MessageBox.Show("Se procede a Forzar la Descarga y Ejecución de NOTIN8. Ten en cuenta las siguientes advertencias:" & vbCrLf & "-Se forzará ejecución del MigradorSQL en Deploy (no es necesario que se hubiera ejecutado antes)." & vbCrLf & "-Si se requiere un cambio de diseño en la Base de Datos se debe advertir a los usuarios y forzar la actualización o readjunte del Notaría." & vbCrLf & "-Se lanzará Notin8.exe y se procederá a la instalación de .Net" & vbCrLf & "-En entorno ADRA se terminarán procesos en ejecución tales como Notin o Word." & vbCrLf & "¿DESEAS CONTINUAR?", "Forzar ejecución NOTIN8", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If forzarnotin8 = DialogResult.Yes Then

            'Primero el Migrador...
            obtenerwget()
            Directory.CreateDirectory(RutaDescargas & "NotinNet")
            Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 https://static.unidata.es/MigradorNotinSQL.exe -O " & RutaDescargas & "NotinNet\MigradorNotinSQL.exe", AppWinStyle.Hide, True)
            'Process.Start(RutaDescargas & "NotinNet\MigradorNotinSQL.exe", "/allowdataloss")
            RegistroInstalacion("= FORZAR Instalación de NOTIN. Se lanza MigradorSQL y ejecución de .Net =")

            Try
                RegistroInstalacion("Lanzamos el Migrador con ForceAutomaticDeploy lo cual nos aplicará el diseño necesario para esta ejecución de Notin8.")
                File.WriteAllText(RutaDescargas & "NotinNet\MigradorNotinSQLForceAutomaticDeploy.bat", "@echo off" & vbCrLf & RutaDescargas & "NotinNet\MigradorNotinSQL.exe /forceautomaticdeploy")
                Dim pmigrador As New ProcessStartInfo()
                pmigrador.FileName = RutaDescargas & "NotinNet\MigradorNotinSQLForceAutomaticDeploy.bat"
                Dim migrador As Process = Process.Start(pmigrador)
                migrador.WaitForExit()
                'RegistroInstalacion("ÉXITO: Ejecución de MigradorNotinSQL ForceAutomaticDeploy completada. Revisa el Log del mismo para más detalle.")
                LbVersionMigrador.Visible = True
                TbMigradorLog.Visible = True
                BtMigradorDeploy.BackColor = Color.PaleGreen
                LeerLogMigradorSQL()
            Catch ex As Exception
                RegistroInstalacion("ERROR ejecución MigradorSQL: " & ex.Message)
                BtMigradorDeploy.BackColor = Color.LightSalmon
            End Try

            RegistroInstalacion("Comenzamos la Descarga y Ejecución de NOTIN8.EXE")
            'TODO Versión para 32 bits. Leer INI local o global para Descargar uno u otro. Comparar ambos ini.
            Dim urlnotin8 = "http://static.unidata.es/NotariaEvo/v40/notin8.exe"
            'Shell("cmd.exe /c " & RutaDescargas & "wget.exe " & urlnotin8 & " -O " & RutaDescargas & "NotinNet\Notin8.exe")
            Dim WGETNOTIN8 As String = "wget.exe -q --show-progress -t 5 " & urlnotin8 & " -O " & RutaDescargas & "NotinNet\Notin8.exe"
            Dim RutaCMDWgetNotin8 As String = RutaDescargas & WGETNOTIN8
            Shell("cmd.exe /c " & RutaCMDWgetNotin8, AppWinStyle.NormalFocus, True)

            Try
                Dim pnotin8 As New ProcessStartInfo()
                pnotin8.FileName = RutaDescargas & "NotinNet\Notin8.exe"
                Dim notin8 As Process = Process.Start(pnotin8)
                notin8.WaitForExit()
                'RegistroInstalacion("ÉXITO: NOTIN8 ejecutado correctamente.")
                LeerLogMigradorSQL()
            Catch ex As Exception
                BtNotin8exeForzar.BackColor = Color.LightSalmon
                RegistroInstalacion("ERROR NOTIN8: " & ex.Message)
            End Try

            If UnidadF() = True Then
                Try
                    obtenerrobocopy()
                    Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "NotinNet\ F:\ Notin8.exe /R:1 /W:1", AppWinStyle.NormalFocus, True)
                    RegistroInstalacion("Notin8.exe copiado correctamente a F:\ para futuras ejecuciones.")
                    Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & "F:\ C:\NOTAWIN.NET\ Notin8.mde /R:1 /W:1", AppWinStyle.NormalFocus, True)
                    RegistroInstalacion("Notin8 MDE copiado a Notawin.Net local del usuario.")
                Catch ex As Exception
                    RegistroInstalacion("ERROR Notin8 EXE/MDE no se pudo copiar. Causa: " & ex.Message)
                    BtNotin8exeForzar.BackColor = Color.LightSalmon
                End Try
            Else
                RegistroInstalacion("Notin8 no copiado a F: al no encontrarse la Unidad disponible.")
            End If

            If ProcesosActivos() = True Then
                RegistroInstalacion("Terminamos los procesos que puedan afectar a la Instalación de .Net. Se enviará un KILL.")
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Else
                RegistroInstalacion("No hay procesos que afecten a la instalación de NotinNet. Se procede a su ejecución.")
            End If

            Try
                Dim pnotinnet As New ProcessStartInfo()
                pnotinnet.FileName = "F:\NOTAWIN.NET\NotinNetInstaller.exe"
                Dim notinnet As Process = Process.Start(pnotinnet)
                notinnet.WaitForExit()
                RegistroInstalacion("ÉXITO: NOTIN NET ejecutado correctamente desde F:\Notawin.Net tras la descarga de Notin8.exe.")
                ObtenerVersionNet()
            Catch ex As Exception
                'BtEstableNet.BackColor = Color.LightSalmon
                RegistroInstalacion("ERROR NOTIN NET: No se pudo ejecutar NotinNetInstaller de F tras la descarga de Notin8.exe.")
                MessageBox.Show("Actualización Versión Notin8 finalizada con errores." & vbCrLf & "Consulta Logger para mas detalles.", "Actualizar Notaría Deploy", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtNotin8exeForzar.BackColor = Color.LightSalmon
            End Try

            'Como no puedo comprobar que versión de Net tiene dejo todas en gris
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtEstableNet.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            RegistroInstalacion("TERMINADO. Proceso Notin8 aplicando Deploy finalizó. Revisa resultados.")

            MessageBox.Show("Actualización Versión Notin8 finalizada correctamente.", "Actualizar Notaría Deploy", MessageBoxButtons.OK, MessageBoxIcon.Information)
            BtNotin8exeForzar.BackColor = Color.PaleGreen

        Else
            RegistroInstalacion("El Usuario cancela el Forzado para Instalar Notin8.exe")
        End If

        BtNotin8exe.BackColor = SystemColors.Control
    End Sub



    Private Sub DescargarNotariaX64()
        'TODO esto para mas adelante. Añadir comprobación del ini local/global para descargar 32 o 64bits y en esa funcion llamar a 32 o 64 según corresponda
        obtenerwget()
        Dim urlnotin8 = "http://static.unidata.es/NotariaEvo/x64/notin8.exe"
        'Shell("cmd.exe /c " & RutaDescargas & "wget.exe " & urlnotin8 & " -O " & RutaDescargas & "NotinNet\Notin8.exe")
        Dim WGETNOTIN8 As String = "wget.exe -q --show-progress -t 5 " & urlnotin8 & " -O " & RutaDescargas & "NotinNet\Notin8x64.exe"
        Dim RutaCMDWgetNotin8 As String = RutaDescargas & WGETNOTIN8
        Shell("cmd.exe /c " & RutaCMDWgetNotin8, AppWinStyle.NormalFocus, True)

        Try
            Dim pnotin8 As New ProcessStartInfo()
            pnotin8.FileName = RutaDescargas & "NotinNet\Notin8x64.exe"
            Dim notin8 As Process = Process.Start(pnotin8)
            notin8.WaitForExit()
            RegistroInstalacion("ÉXITO: NOTIN8x64 ejecutado correctamente.")
            ' BtNotin8exe.BackColor = Color.PaleGreen
        Catch ex As Exception
            'BtNotin8exe.BackColor = Color.LightSalmon
            RegistroInstalacion("ERROR NOTIN8x64: " & ex.Message)
        End Try

    End Sub


    'Private Function LeerFicheroDesdeLinea(ByVal numeroLinea As Integer, ByVal nombreFichero As String) As String
    '    Dim fichero As New System.IO.FileInfo(nombreFichero)
    '    LeerFicheroDesdeLinea = ""
    '    If fichero.Exists Then
    '        Dim sr As System.IO.StreamReader
    '        Dim lineaActual As Integer = 1
    '        Try
    '            sr = New System.IO.StreamReader(fichero.FullName)
    '            While lineaActual < numeroLinea And Not sr.EndOfStream
    '                sr.ReadLine()
    '                lineaActual += 1
    '            End While
    '            LeerFicheroDesdeLinea = sr.ReadToEnd
    '        Catch ex As Exception
    '            MsgBox("No se pudo ejecutar la operación")
    '        Finally
    '            If sr IsNot Nothing Then
    '                sr.Close()
    '                sr.Dispose()
    '            End If
    '        End Try
    '    End If
    'End Function

    Private Sub ObtenerVersionNet()
        Try
            Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
            Dim infoinstaller As String = appData & "\Notin\InfoInstaller.txt"
            Dim infoversion As String

            Dim sr As New System.IO.StreamReader(infoinstaller)
            infoversion = sr.ReadLine()
            'infoversion = sr.ReadToEnd
            sr.Close()
            LbBetaNet.Text = infoversion
            cIniArray.IniWrite(instaladorkuboini, "NET", "NETSISTEMA", infoversion)
            'cIniArray.IniWrite(instaladorkuboini, "NET", "FECHAEJECUCION", DateTime.Now)
            RegistroInstalacion("NOTIN .NET: InfoVersión en el Sistema: " & infoversion)
        Catch ex As Exception
            RegistroInstalacion("NOTIN .NET: No se pudo determinar Versión .Net en Sistema.")
        End Try
    End Sub

    Private Sub ObtenerVersionFW()
        Directory.CreateDirectory(RutaDescargas)

        Try
            Shell("cmd /c reg Query " & """" & "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Client" & """" & " /v Version > " & RutaDescargas & "fwversion.txt", AppWinStyle.Hide, True)
        Catch ex As Exception
            RegistroInstalacion("ERROR Obtener versión FW: " & ex.Message)
        End Try

        Try
            Dim versionfw As String = RutaDescargas & "fwversion.txt"
            Dim infoversion As String = "Sin información."

            Dim sr As New System.IO.StreamReader(versionfw)
            Dim numerolinea As Integer = 0
            'TODO Sandraaaaaaaa ayudame con esto. While seguro es cutre jaja
            While numerolinea < 3
                infoversion = sr.ReadLine()
                numerolinea = numerolinea + 1
            End While
            sr.Close()

            TlpVersionFW.ToolTipTitle = "REG Query para Versión Framework:"
            TlpVersionFW.SetToolTip(LbVersionFW, infoversion)

            Dim numeroversion As String = infoversion.Substring(25)

            LbVersionFW.Text = "Versión " & numeroversion

            cIniArray.IniWrite(instaladorkuboini, "NET", "FWSISTEMA", numeroversion)
            RegistroInstalacion("INFO FRAMEWORK: InfoVersión en el Sistema: " & numeroversion)
        Catch ex As Exception
            RegistroInstalacion("INFO FRAMEWORK: No se pudo determinar Versión FW en Sistema.")
        End Try
    End Sub



    Private Sub BtNetBeta_Click(sender As Object, e As EventArgs) Handles BtNetBeta.Click
        If NotinRapp() = True Then
            Dim notinrapp = MessageBox.Show("Se va a proceder a ejecutar NotinNetInstaller en host NOTINRAPP. ¿Estás seguro?", "NotinNet en AdRa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            RegistroInstalacion("ADRA: Ejecutando NotinNetInstaller en entorno NotinRapp. Se advierte al usuario.")
            If notinrapp = DialogResult.No Then
                RegistroInstalacion("ADRA: Operación Cancelada. Hice bien en preguntar... (Gracias DPerez).")
                Exit Sub
            End If
        End If

        If ProcesosActivos() = True Then
            Dim procesosactivos As DialogResult = MessageBox.Show("Hay procesos en ejecución que puden interrumpir la instalación de NotinNet. Si continúas se forzará su cierre. ¿Proseguimos con la instalación?", "Procesos activos Net detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If procesosactivos = DialogResult.Yes Then
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Else
                BtNetBeta.BackColor = Color.LightSalmon
                RegistroInstalacion("ADVERTENCIA NOTINNET: No se ejecutó la instalación ya que existían procesos en ejecución. El usuario cancela.")
                Exit Sub
            End If
        End If

        BackupNotinNet()

        obtenerwget()
        Dim urlbeta As String = "https://static.unidata.es/NotinNetInstaller/v40/beta/NotinNetInstaller.exe"
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlbeta & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)

        Try
            Dim pnotinnetbeta As New ProcessStartInfo()
            pnotinnetbeta.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
            Dim notinnetbeta As Process = Process.Start(pnotinnetbeta)
            'notinnet.WaitForInputIdle()
            notinnetbeta.WaitForExit()

            RegistroInstalacion("BETA Notin: Instalador NotinNetInstaller versión BETA v40 ejecutado correctamente. Fecha " & DateTime.Now.Date)

            'cIniArray.IniWrite(instaladorkuboini, "NET", "FECHABETA", "Ejecución:" & DateTime.Now)
            cIniArray.IniWrite(instaladorkuboini, "NET", "NOTINNET", "BETA")

            ObtenerVersionNet()
            ObtenerVersionFW()

            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = Color.PaleGreen
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
            BtNotinNetF.Visible = True
        Catch ex As Exception
            RegistroInstalacion("BETA Notin: Error instalando Beta: " & ex.Message)
            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = Color.LightSalmon
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
        End Try
    End Sub

    Private Sub BtEstablex64F462_Click(sender As Object, e As EventArgs) Handles BtEstablex64F462.Click
        If NotinRapp() = True Then
            Dim notinrapp = MessageBox.Show("Se va a proceder a ejecutar NotinNetInstaller en host NOTINRAPP. ¿Estás seguro?", "NotinNet en AdRa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            RegistroInstalacion("ADRA: Ejecutando NotinNetInstaller en entorno NotinRapp. Se advierte al usuario.")
            If notinrapp = DialogResult.No Then
                RegistroInstalacion("ADRA: Operación Cancelada. Hice bien en preguntar... (Gracias DPerez).")
                Exit Sub
            End If
        End If

        If ProcesosActivos() = True Then
            Dim procesosactivos As DialogResult = MessageBox.Show("Hay procesos en ejecución que puden interrumpir la instalación de NotinNet. Si continúas se forzará su cierre. ¿Proseguimos con la instalación?", "Procesos activos Net detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If procesosactivos = DialogResult.Yes Then
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Else
                BtEstablex64F462.BackColor = Color.LightSalmon
                RegistroInstalacion("ADVERTENCIA NOTINNET: No se ejecutó la instalación ya que existían procesos en ejecución. El usuario cancela.")
                Exit Sub
            End If
        End If

        BackupNotinNet()

        obtenerwget()
        Dim urlbeta64 As String = "https://static.unidata.es/NotinNetInstaller/x64/estable/NotinNetInstaller.exe"
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlbeta64 & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
        Try
            Dim pnotinnetbetax64 As New ProcessStartInfo()
            pnotinnetbetax64.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
            Dim notinnetbetax64 As Process = Process.Start(pnotinnetbetax64)
            'notinnet.WaitForInputIdle()
            notinnetbetax64.WaitForExit()
            RegistroInstalacion("ESTABLEx64 Notin FW462: Instalador NotinNetInstaller versión ESTABLEx64 para FW462 ejecutado correctamente. Fecha " & DateTime.Now.Date)
            cIniArray.IniWrite(instaladorkuboini, "NET", "NOTINNET", "ESTABLEX64F462")

            ObtenerVersionNet()
            ObtenerVersionFW()

            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = Color.PaleGreen
            BtNetBetax64F462.BackColor = SystemColors.Control
            BtNotinNetF.Visible = True
        Catch ex As Exception
            RegistroInstalacion("BETAx64 Notin: Error instalando Beta: " & ex.Message)
            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = Color.LightSalmon
            BtNetBetax64F462.BackColor = SystemColors.Control
        End Try

    End Sub

    Private Function ProcesosActivos() As Boolean
        Dim procesoword() As Process
        procesoword = Process.GetProcessesByName("winword")
        Dim procesonotin() As Process
        procesonotin = Process.GetProcessesByName("msaccess")
        Dim procesonet() As Process
        procesonet = Process.GetProcessesByName("notinnetdesktop")
        Dim procesonexus() As Process
        procesonexus = Process.GetProcessesByName("nexus")

        If procesoword.Count > 0 OrElse procesonotin.Count > 0 OrElse procesonet.Count > 0 OrElse procesonexus.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub BackupNotinNet()
        'Copiar el de NOTIN NET de la Carpeta Descargas
        If File.Exists(RutaDescargas & "NotinNet\NotinNetInstaller.exe") Then
            Try
                Directory.CreateDirectory(RutaDescargas & "NotinNet\BackupNet")
            Catch ex As Exception
            End Try

            Dim Fechahoy As String = DateTime.Now.Date
            Dim notinnetfecha As String = Replace(Fechahoy, "/", ".")
            Try
                File.Copy(RutaDescargas & "NotinNet\NotinNetInstaller.exe", RutaDescargas & "NotinNet\BackupNet\NotinNetInstaller_" & notinnetfecha & ".exe", True)
            Catch ex As Exception
                RegistroInstalacion("ERROR: No se puedo crear el Backup de NotinNetInstaller en RutaDescargas. " & ex.Message)
            End Try
        End If

        'Copiar el de F NOTAWIN.NET
        Try
            Directory.CreateDirectory("F:\Notawin.Net\BackupNet")
        Catch ex As Exception
        End Try

        If File.Exists("F:\Notawin.Net\NotinNetInstaller.exe") Then
            Dim Fechahoy As String = DateTime.Now.Date
            Dim notinnetfecha As String = Replace(Fechahoy, "/", ".")
            Try
                File.Copy("F:\Notawin.Net\NotinNetInstaller.exe", "F:\Notawin.Net\BackupNet\NotinNetInstaller_" & notinnetfecha & ".exe", True)
            Catch ex As Exception
                RegistroInstalacion("ERROR: No se puedo crear el Backup de NotinNetInstaller de F NotawinNet. " & ex.Message)
            End Try
        End If
    End Sub


    Private Sub BtEstableNet_Click(sender As Object, e As EventArgs) Handles BtEstableNet.Click

        If NotinRapp() = True Then
            Dim notinrapp = MessageBox.Show("Se va a proceder a ejecutar NotinNetInstaller en host NOTINRAPP. ¿Estás seguro?", "NotinNet en AdRa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            RegistroInstalacion("ADRA: Ejecutando NotinNetInstaller en entorno NotinRapp. Se advierte al usuario.")
            If notinrapp = DialogResult.No Then
                RegistroInstalacion("ADRA: Operación Cancelada. Hice bien en preguntar... (Gracias DPerez).")
                Exit Sub
            End If
        End If

        If ProcesosActivos() = True Then
            Dim procesosactivos As DialogResult = MessageBox.Show("Hay procesos en ejecución que puden interrumpir la instalación de NotinNet. Si continúas se forzará su cierre. ¿Proseguimos con la instalación?", "Procesos activos Net detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If procesosactivos = DialogResult.Yes Then
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Else
                BtEstableNet.BackColor = Color.LightSalmon
                RegistroInstalacion("ADVERTENCIA NOTINNET: No se ejecutó la instalación ya que existían procesos en ejecución. El usuario cancela.")
                Exit Sub
            End If
        End If

        BackupNotinNet()

        obtenerwget()
        Dim urlestable As String = "https://static.unidata.es/NotinNetInstaller/v40/estable/NotinNetInstaller.exe"
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlestable & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)

        Try
            Dim pnotinnetestable As New ProcessStartInfo()
            pnotinnetestable.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
            Dim notinnetestable As Process = Process.Start(pnotinnetestable)
            'notinnet.WaitForInputIdle()
            notinnetestable.WaitForExit()

            RegistroInstalacion("ESTABLE Notin: Instalador NotinNetInstaller versión ESTABLE v40 ejecutado correctamente. Fecha " & DateTime.Now.Date)
            cIniArray.IniWrite(instaladorkuboini, "NET", "NOTINNET", "ESTABLE")

            ObtenerVersionNet()
            ObtenerVersionFW()

            BtEstableNet.BackColor = Color.PaleGreen
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
            BtNotinNetF.Visible = True
        Catch ex As Exception
            RegistroInstalacion("ESTABLE Notin: Error instalando NotiNet: " & ex.Message)
            BtEstableNet.BackColor = Color.LightSalmon
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
        End Try
    End Sub


    Private Sub BtNotinNetF_Click(sender As Object, e As EventArgs) Handles BtNotinNetF.Click
        If UnidadF() = True AndAlso File.Exists(RutaDescargas & "NotinNet\NotinNetInstaller.exe") = True Then
            obtenerrobocopy()
            Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "NotinNet\ F:\Notawin.Net\ NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
            RegistroInstalacion("NotinNetInstaller copiado a F:\Notawin.Net para su distribución en el despacho.")
            BtNotinNetF.BackColor = Color.PaleGreen
        Else
            MessageBox.Show("Hubo un problema al Copiar NotinNetInstaller a F. Revisa conexión con Unidad F y que hayas descargado el ejecutable.", "Error de Ruta o Ejecutable", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR Copiando NotinNetInstaller a F. Posible Unidad desconectada o archivo no descargado.")
        End If
    End Sub

    Private Sub BtBlancosBD_Click(sender As Object, e As EventArgs) Handles BtBlancosBD.Click
        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "BLANCOS_SQL2014")
        'BAK
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/BAK/DATOS.BAK -O " & RutaDescargas & "BLANCOS_SQL2014\DATOS.BAK", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/BAK/HISTORIC.BAK -O " & RutaDescargas & "BLANCOS_SQL2014\HISTORIC.BAK", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/BAK/MODELOS.BAK -O " & RutaDescargas & "BLANCOS_SQL2014\MODELOS.BAK", AppWinStyle.NormalFocus, True)
        'MDF-LDF
        Directory.CreateDirectory(RutaDescargas & "BLANCOS_SQL2014\MDF-LDF")
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/MDF-LDF/HISTORIC.mdf -O " & RutaDescargas & "BLANCOS_SQL2014\MDF-LDF\HISTORIC.MDF", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/MDF-LDF/HISTORIC.ldf -O " & RutaDescargas & "BLANCOS_SQL2014\MDF-LDF\HISTORIC.LDF", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/MDF-LDF/MODELOS.mdf -O " & RutaDescargas & "BLANCOS_SQL2014\MDF-LDF\MODELOS.MDF", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/MDF-LDF/MODELOS.ldf -O " & RutaDescargas & "BLANCOS_SQL2014\MDF-LDF\MODELOS.LDF", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/MDF-LDF/DATOS.mdf -O " & RutaDescargas & "BLANCOS_SQL2014\MDF-LDF\DATOS.MDF", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/BLANCOS_SQL2014/MDF-LDF/DATOS.ldf -O " & RutaDescargas & "BLANCOS_SQL2014\MDF-LDF\DATOS.LDF", AppWinStyle.NormalFocus, True)


        If File.Exists(RutaDescargas & "BLANCOS_SQL2014\DATOS.BAK") Then
            RegistroInstalacion("ËXITO: Obtenidos DATOS BLANCOS desde Pandora.")
            BtBlancosBD.BackColor = Color.PaleGreen
            cIniArray.IniWrite(instaladorkuboini, "SQL2014", "BLANCOS", "1")
            Process.Start("explorer.exe", RutaDescargas & "BLANCOS_SQL2014\")
        Else
            RegistroInstalacion("ERROR BD_BLANCOS: No se pudo comprobar la existencia de las descargas.")
            BtBlancosBD.BackColor = Color.LightSalmon
            cIniArray.IniWrite(instaladorkuboini, "SQL2014", "BLANCOS", "0")
        End If

    End Sub


    'MODULO CHOCOLATEY
    Private Sub BtFramework462_Click(sender As Object, e As EventArgs) Handles BtFramework462.Click
        Dim instaladochocolatey = cIniArray.IniGet(instaladorkuboini, "CHOCOLATEY", "INSTALADO", "0")
        If instaladochocolatey = 0 Then
            Dim instalachocolatey = MessageBox.Show("Necesario Paquete CHOCOLATEY. Disponible también en la pestaña Útiles. ¿Lo instalamos?", "Paquete Chocolatey necesario", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If instalachocolatey = DialogResult.Yes Then
                ObtenerChocolatey()
                Threading.Thread.Sleep(10000)
                MessageBox.Show("Al finalizar la instalación de Chocolatey podrás instalar Framework 4.6.2. Aguarda unos segundos.", "Choco para Framework 4.6.2", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                Exit Sub
            End If
        End If

        Dim framework462 As String = "@echo off" & vbCrLf & "choco install -y dotnet4.6.2"
        File.WriteAllText(RutaDescargas & "Chocolatey\Framework462.bat", framework462)
        Try
            RunAsAdmin(RutaDescargas & "Chocolatey\Framework462.bat")
            RegistroInstalacion("Lanzado Paquete Choco para Framework 4.6.2")
            cIniArray.IniWrite(instaladorkuboini, "CHOCOLATEY", "FRAMEWORK462", "1")
            ObtenerVersionFW()
            BtFramework462.BackColor = Color.PaleGreen
        Catch ex As Exception
            RegistroInstalacion("ERROR lanzando Framework 4.6.2 desde Choco: " & ex.Message)
            ObtenerVersionFW()
            BtFramework462.BackColor = Color.LightSalmon
        End Try
    End Sub

    Private Sub BtChocolatey_Click(sender As Object, e As EventArgs) Handles BtChocolatey.Click
        ObtenerChocolatey()
    End Sub

    Private Sub BtLogChoco_Click(sender As Object, e As EventArgs) Handles BtLogChoco.Click
        Process.Start("notepad.exe", "C:\ProgramData\chocolatey\logs\chocolatey.log")
    End Sub

    'Chequeo si trabajamos en NotinRapp
    Private Function NotinRapp() As Boolean
        'Dim equipousuario As String = (My.User.Name)
        'Dim equipo As Integer = equipousuario.LastIndexOf("\")
        'Dim hostname = equipousuario.Substring(0, equipo).ToUpper
        Dim hostname = Environment.MachineName.ToUpper
        If hostname = "NOTINRAPP" Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub BtFocos_Click(sender As Object, e As EventArgs) Handles BtFocos.Click
        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "ADRA")
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress http://static.unidata.es/focos.rar -O " & RutaDescargas & "\ADRA\focos.rar", AppWinStyle.NormalFocus, True)

        obtenerunrar()
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -y " & RutaDescargas & "ADRA\focos.rar " & RutaDescargas & "ADRA\", AppWinStyle.Hide, True)
        Try
            Process.Start(RutaDescargas & "Adra\rdp_v1607.exe")
            cIniArray.IniWrite(instaladorkuboini, "ADRA", "FOCOS", "1")
            BtFocos.BackColor = Color.PaleGreen
            RegistroInstalacion("ADRA: Ejecutado Downgrade RDP en versión 1607 para bug Focos.")
        Catch ex As Exception
            RegistroInstalacion("ADRA: Error ejecutando Downgrade RDP: " & ex.Message)
            BtFocos.BackColor = Color.LightSalmon
        End Try
    End Sub

    Private Sub BtDynamic_Click(sender As Object, e As EventArgs) Handles BtDynamic.Click
        obtenerwget()
        obtenerunrar()

        Directory.CreateDirectory(RutaDescargas & "ADRA")
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 --ftp-user=tecnicos --ftp-password=20070401 ftp://ftp.pandora.notin.net/Juanjo/Adra/DynamicSolar.exe -O " & RutaDescargas & "ADRA\DynamicSolar.exe", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -y -pb30330104b " & RutaDescargas & "ADRA\DynamicSolar.exe " & RutaDescargas & "ADRA\", AppWinStyle.Hide, True)

        Dim batdynamic As String = "%SystemRoot%\system32\WindowsPowerShell\v1.0\powershell.exe -File " & RutaDescargas & "ADRA\DynamicSolar.ps1"
        File.WriteAllText(RutaDescargas & "ADRA\DynamicSolar.bat", batdynamic)

        Try
            Process.Start(RutaDescargas & "ADRA\DynamicSolar.bat")
            BtDynamic.BackColor = Color.PaleGreen
            RegistroInstalacion("DynamicSolar ejecutado correctamente.")
            cIniArray.IniWrite(instaladorkuboini, "ADRA", "DYNAMICSOLAR", "1")
        Catch ex As Exception
            BtDynamic.BackColor = Color.LightSalmon
            RegistroInstalacion("ERROR DynamicSolar: " & ex.Message)
        End Try


    End Sub

    Private Sub BtReducirDatos_MouseDown(sender As Object, e As MouseEventArgs) Handles BtReducirDatos.MouseDown
        LbSentenciaSQL.Visible = True
    End Sub

    Private Sub BtReducirDatos_Click(sender As Object, e As EventArgs) Handles BtReducirDatos.Click
        Try
            Dim consulta As String = "USE DATOS" & vbCrLf & "ALTER DATABASE DATOS" & vbCrLf & "SET RECOVERY SIMPLE;" & vbCrLf & "GO" & vbCrLf & "DBCC SHRINKFILE (DATOS_log, 1);" & vbCrLf & "GO" & vbCrLf & "ALTER DATABASE DATOS" & vbCrLf & "SET RECOVERY FULL;" & vbCrLf & "GO"
            My.Computer.Clipboard.SetText(consulta)
            Threading.Thread.Sleep(2000)
            LbSentenciaSQL.Visible = False
        Catch ex As Exception
            RegistroInstalacion("ERROR Consulta Reducir Datos: " & ex.Message)
            BtReducirDatos.BackColor = Color.LightSalmon
        End Try

    End Sub


    Private Sub BtTriggers_MouseDown(sender As Object, e As MouseEventArgs) Handles BtTriggers.MouseDown
        LbSentenciaSQL.Visible = True
    End Sub

    Private Sub BtTriggers_Click(sender As Object, e As EventArgs) Handles BtTriggers.Click
        Try
            Dim consulta As String = "USE DATOS" & vbCrLf & "DROP TRIGGER Actualizar_Tramite" & vbCrLf & "DROP TRIGGER insertar_Tramite" & vbCrLf & "DROP TRIGGER Borrar_Tramite" & vbCrLf & "DROP TRIGGER Actualizar_Cif_Representante" & vbCrLf & "DROP TRIGGER Actualizar_Tramite_Clientes" & vbCrLf & "DROP TRIGGER Actualizar" & vbCrLf & "DROP TRIGGER Borrar" & vbCrLf & "DROP TRIGGER Actualizar_Escrituras_Operaciones" & vbCrLf & "DROP TRIGGER insertar_Numero_Operaciones"
            My.Computer.Clipboard.SetText(consulta)
            Threading.Thread.Sleep(2000)
            LbSentenciaSQL.Visible = False
        Catch ex As Exception
            RegistroInstalacion("ERROR Consulta Triggers: " & ex.Message)
            BtTriggers.BackColor = Color.LightSalmon
        End Try
    End Sub

    Private Sub BtEstablew32F462_Click(sender As Object, e As EventArgs) Handles BtEstablew32F462.Click
        If NotinRapp() = True Then
            Dim notinrapp = MessageBox.Show("Se va a proceder a ejecutar NotinNetInstaller en host NOTINRAPP. ¿Estás seguro?", "NotinNet en AdRa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            RegistroInstalacion("ADRA: Ejecutando NotinNetInstaller en entorno NotinRapp. Se advierte al usuario.")
            If notinrapp = DialogResult.No Then
                RegistroInstalacion("ADRA: Operación Cancelada. Hice bien en preguntar... (Gracias DPerez).")
                Exit Sub
            End If
        End If

        If ProcesosActivos() = True Then
            Dim procesosactivos As DialogResult = MessageBox.Show("Hay procesos en ejecución que puden interrumpir la instalación de NotinNet. Si continúas se forzará su cierre. ¿Proseguimos con la instalación?", "Procesos activos Net detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If procesosactivos = DialogResult.Yes Then
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Else
                BtNetBetaW32F462.BackColor = Color.LightSalmon
                RegistroInstalacion("ADVERTENCIA NOTINNET: No se ejecutó la instalación ya que existían procesos en ejecución. El usuario cancela.")
                Exit Sub
            End If
        End If

        BackupNotinNet()

        obtenerwget()
        Dim urlestablef462 As String = "https://static.unidata.es/NotinNetInstaller/W32/estable/NotinNetInstaller.exe"
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlestablef462 & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)

        Try
            Dim pnotinnetbeta As New ProcessStartInfo()
            pnotinnetbeta.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
            Dim notinnetbeta As Process = Process.Start(pnotinnetbeta)
            'notinnet.WaitForInputIdle()
            notinnetbeta.WaitForExit()

            RegistroInstalacion("ESTABLE .NET Framework462: Instalador NotinNetInstaller versión ESTABLE para FW462 ejecutado correctamente. Fecha " & DateTime.Now.Date)

            'cIniArray.IniWrite(instaladorkuboini, "NET", "FECHABETA", "Ejecución:" & DateTime.Now)
            cIniArray.IniWrite(instaladorkuboini, "NET", "NOTINNET", "ESTABLEW32F462")

            ObtenerVersionNet()
            ObtenerVersionFW()

            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = Color.PaleGreen
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
            BtNotinNetF.Visible = True
        Catch ex As Exception
            RegistroInstalacion("ESTABLE Notin: Error instalando Beta .Net para Framework4.6.2: " & ex.Message)
            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = Color.LightSalmon
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
        End Try
    End Sub


    Private Sub BtNetBetaW32F462_Click(sender As Object, e As EventArgs) Handles BtNetBetaW32F462.Click
        If NotinRapp() = True Then
            Dim notinrapp = MessageBox.Show("Se va a proceder a ejecutar NotinNetInstaller en host NOTINRAPP. ¿Estás seguro?", "NotinNet en AdRa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            RegistroInstalacion("ADRA: Ejecutando NotinNetInstaller en entorno NotinRapp. Se advierte al usuario.")
            If notinrapp = DialogResult.No Then
                RegistroInstalacion("ADRA: Operación Cancelada. Hice bien en preguntar... (Gracias DPerez).")
                Exit Sub
            End If
        End If

        If ProcesosActivos() = True Then
            Dim procesosactivos As DialogResult = MessageBox.Show("Hay procesos en ejecución que puden interrumpir la instalación de NotinNet. Si continúas se forzará su cierre. ¿Proseguimos con la instalación?", "Procesos activos Net detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If procesosactivos = DialogResult.Yes Then
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Else
                BtNetBetaW32F462.BackColor = Color.LightSalmon
                RegistroInstalacion("ADVERTENCIA NOTINNET: No se ejecutó la instalación ya que existían procesos en ejecución. El usuario cancela.")
                Exit Sub
            End If
        End If

        BackupNotinNet()

        obtenerwget()
        Dim urlbetaf462 As String = "https://static.unidata.es/NotinNetInstaller/v47/beta/NotinNetInstaller.exe"
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlbetaf462 & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)

        Try
            Dim pnotinnetbeta As New ProcessStartInfo()
            pnotinnetbeta.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
            Dim notinnetbeta As Process = Process.Start(pnotinnetbeta)
            'notinnet.WaitForInputIdle()
            notinnetbeta.WaitForExit()

            RegistroInstalacion("BETA .NET Framework462: Instalador NotinNetInstaller versión BETA para FW462 ejecutado correctamente. Fecha " & DateTime.Now.Date)

            'cIniArray.IniWrite(instaladorkuboini, "NET", "FECHABETA", "Ejecución:" & DateTime.Now)
            cIniArray.IniWrite(instaladorkuboini, "NET", "NOTINNET", "BETAW32F462")

            ObtenerVersionNet()
            ObtenerVersionFW()

            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = Color.PaleGreen
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
            BtNotinNetF.Visible = True
        Catch ex As Exception
            RegistroInstalacion("BETA Notin: Error instalando Beta .Net para Framework4.6.2: " & ex.Message)
            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = Color.LightSalmon
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = SystemColors.Control
        End Try
    End Sub

    Private Sub BtNetBetax64F462_Click(sender As Object, e As EventArgs) Handles BtNetBetax64F462.Click
        If NotinRapp() = True Then
            Dim notinrapp = MessageBox.Show("Se va a proceder a ejecutar NotinNetInstaller en host NOTINRAPP. ¿Estás seguro?", "NotinNet en AdRa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            RegistroInstalacion("ADRA: Ejecutando NotinNetInstaller en entorno NotinRapp. Se advierte al usuario.")
            If notinrapp = DialogResult.No Then
                RegistroInstalacion("ADRA: Operación Cancelada. Hice bien en preguntar... (Gracias DPerez).")
                Exit Sub
            End If
        End If

        If ProcesosActivos() = True Then
            Dim procesosactivos As DialogResult = MessageBox.Show("Hay procesos en ejecución que puden interrumpir la instalación de NotinNet. Si continúas se forzará su cierre. ¿Proseguimos con la instalación?", "Procesos activos Net detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If procesosactivos = DialogResult.Yes Then
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Else
                BtNetBetax64F462.BackColor = Color.LightSalmon
                RegistroInstalacion("ADVERTENCIA NOTINNET: No se ejecutó la instalación ya que existían procesos en ejecución. El usuario cancela.")
                Exit Sub
            End If
        End If

        BackupNotinNet()

        obtenerwget()
        Dim urlbetaf462 As String = "https://static.unidata.es/NotinNetInstaller/x64/beta/NotinNetInstaller.exe"
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlbetaf462 & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)

        Try
            Dim pnotinnetbeta As New ProcessStartInfo()
            pnotinnetbeta.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
            Dim notinnetbeta As Process = Process.Start(pnotinnetbeta)
            'notinnet.WaitForInputIdle()
            notinnetbeta.WaitForExit()

            RegistroInstalacion("BETA .NET X64 Framework462: Instalador NotinNetInstaller versión BETAx64 para FW462 ejecutado correctamente. Fecha " & DateTime.Now.Date)

            'cIniArray.IniWrite(instaladorkuboini, "NET", "FECHABETA", "Ejecución:" & DateTime.Now)
            cIniArray.IniWrite(instaladorkuboini, "NET", "NOTINNET", "BETAx64F462")

            ObtenerVersionNet()
            ObtenerVersionFW()

            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = Color.PaleGreen
            BtNotinNetF.Visible = True
        Catch ex As Exception
            RegistroInstalacion("BETA Notin X64: Error instalando Beta .Net X64 para Framework4.6.2: " & ex.Message)
            BtEstableNet.BackColor = SystemColors.Control
            BtNetBeta.BackColor = SystemColors.Control
            BtEstablew32F462.BackColor = SystemColors.Control
            BtNetBetaW32F462.BackColor = SystemColors.Control
            BtEstablex64F462.BackColor = SystemColors.Control
            BtNetBetax64F462.BackColor = Color.LightSalmon
        End Try
    End Sub

    Private Sub BtLogNet_Click(sender As Object, e As EventArgs) Handles BtLogNet.Click
        Dim appdata As String = GetFolderPath(SpecialFolder.ApplicationData)
        Process.Start("explorer.exe", appdata & "\Notin\Addins\NotinTaskPane\Log")
    End Sub

    Private Sub BtNautilus_Click(sender As Object, e As EventArgs) Handles BtNautilus.Click
        RegistroInstalacion("Instalación Notin Control Center (NAUTILUS) ")
        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd /c " & RutaDescargas & "wget.exe -q --show-progress https://static.unidata.es/Nautilus/NautilusUpdater.exe -O " & RutaDescargas & "NotinNet\NautilusUpdater.exe", AppWinStyle.NormalFocus, True)
        RegistroInstalacion("Nautilus: Obtenida versión desde Static-Unidata hacía " & RutaDescargas & "NotinNet\")

        Try
            Process.Start(RutaDescargas & "NotinNet\NautilusUpdater.exe")
            cIniArray.IniWrite(instaladorkuboini, "NET", "NAUTILUS", "1")
            BtNautilus.BackColor = Color.PaleGreen
            RegistroInstalacion("Nautilus. Lanzado proceso de instalación. Revisa su Log de Salida para más información.")
        Catch ex As Exception
            RegistroInstalacion("ERROR Nautilus: " & ex.Message)
            cIniArray.IniWrite(instaladorkuboini, "NET", "NAUTILUS", "0")
            BtNautilus.BackColor = Color.LightSalmon
        End Try
    End Sub

    Private Sub BtNautilusLog_Click(sender As Object, e As EventArgs) Handles BtNautilusLog.Click
        Try
            Process.Start("explorer.exe", "C:\Program Files (x86)\Humano Software\Services\NotinControlCenter\Log")
        Catch ex As Exception
            RegistroInstalacion("Nautilus. No se pudo acceder a ruta Log: " & ex.Message)
        End Try
    End Sub

    Private Sub BtPaginaActiva_Click(sender As Object, e As EventArgs) Handles BtPaginaActiva.Click
        Try
            Process.Start("iexplore.exe", "http://www.notin.net/portal/425r312x/pagina.asp")
            'Shell("cmd /k start /d " & """" & "" & """" & " IEXPLORE.EXE http://www.notin.net/portal/425r312x/pagina.asp", AppWinStyle.Hide, False)
            RegistroInstalacion("Página activa lanzada a Internet Explorer.")
        Catch ex As Exception
            RegistroInstalacion("ERROR Página Activa: " & ex.Message)
        End Try
    End Sub

    Private Sub BtVisorImagenes_Click(sender As Object, e As EventArgs) Handles BtVisorImagenes.Click
        obtenerunrar()
        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "Registro")
        Dim wgetvisor As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 "
        Shell("cmd.exe /c " & RutaDescargas & wgetvisor & PuestoNotin & "VisorDeImagenes.reg" & " -O" & RutaDescargas & "Registro\VisorDeImagenes.reg", AppWinStyle.NormalFocus, True)
        Try
            Process.Start("regedit.exe", "/s " & RutaDescargas & "Registro\VisorDeImagenes.reg")
            RegistroInstalacion("Visor Imágenes: Clave Registro Importada correctamente.")
            BtVisorImagenes.BackColor = Color.PaleGreen
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "VISORIMAGENES", "1")
            Dim visorpredeterminado As String = "Configuración de aplicaciones predeterminadas"
            My.Computer.Clipboard.SetText(visorpredeterminado)
            MessageBox.Show("Establece ahora Visor de Imágenes como Aplicación Predeterminada en Visualizador de fotos." & vbCrLf & "Acepta e intentaremos abrir la Configuración.", "Predeterminar Visor Imágenes Windows", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            RegistroInstalacion("ERROR Visor Imágenes: " & ex.Message)
            BtVisorImagenes.BackColor = Color.LightSalmon
        End Try

        'Dim ProcID As Integer
        '' Start the Calculator application, and store the process id.
        'ProcID = Shell("CALC.EXE", AppWinStyle.NormalFocus)
        '' Activate the Calculator application.
        'AppActivate(ProcID)
        '' Send the keystrokes to the Calculator application.
        'My.Computer.Keyboard.SendKeys("22", True)
        'My.Computer.Keyboard.SendKeys("*", True)
        'My.Computer.Keyboard.SendKeys("44", True)
        'My.Computer.Keyboard.SendKeys("=", True)
        '' The result is 22 * 44 = 968.

        My.Computer.Keyboard.SendKeys("^{ESC}", True)
        Threading.Thread.Sleep(500)
        SendKeys.Send("^V")
        Threading.Thread.Sleep(2000)
        SendKeys.Send("{ENTER}")
    End Sub



    Private Sub BtSQL2008R2_Click(sender As Object, e As EventArgs) Handles BtSQL2008R2.Click
        FormSQL2008R2.Show()
    End Sub


    Private Sub CboxWUpdate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboxWUpdate.SelectedIndexChanged
        'TODO Cambiar esto por un desplegable de verdad
        If CboxWUpdate.SelectedIndex = 0 Then
            Try
                Directory.CreateDirectory(RutaDescargas & "Registro")
                My.Computer.Network.DownloadFile(PuestoNotin & "wu_enable.reg", RutaDescargas & "Registro\wu_enable.reg", "juanjo", "Palomeras24", False, 20000, True)
                Dim enable As String = "@echo off" & vbCrLf & "regedit.exe /s " & RutaDescargas & "Registro\wu_enable.reg"
                File.WriteAllText(RutaDescargas & "Registro\wu_enable.bat", enable)
                RunAsAdmin(RutaDescargas & "Registro\wu_enable.bat")
                Shell("gpupdate.exe /force", AppWinStyle.Hide, False)
                RegistroInstalacion("ÉXITO: Habilitado Windows Update.")
                MessageBox.Show("ACTIVADO Windows Update en este equipo. Reinicia para aplicar los cambios.", "Activar Windows Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("No se pudieron Activar los Updates. Revisa el Log para más información.", "Error WUpdate", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR: No se pudo Habilitar WU: " & ex.Message)
            End Try

        ElseIf CboxWUpdate.SelectedIndex = 1 Then
            Try
                Directory.CreateDirectory(RutaDescargas & "Registro")
                My.Computer.Network.DownloadFile(PuestoNotin & "wu_disable.reg", RutaDescargas & "Registro\wu_disable.reg", "juanjo", "Palomeras24", False, 20000, True)
                Dim disable As String = "@echo off" & vbCrLf & "regedit.exe /s " & RutaDescargas & "Registro\wu_disable.reg"
                File.WriteAllText(RutaDescargas & "Registro\wu_disable.bat", disable)
                RunAsAdmin(RutaDescargas & "Registro\wu_disable.bat")
                Shell("gpupdate.exe /force", AppWinStyle.Hide, False)
                RegistroInstalacion("ÉXITO: Deshabilitado Windows Update.")
                MessageBox.Show("DESACTIVADO Windows Update en este equipo. Reinicia para aplicar los cambios.", "Desactivar Windows Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("No se pudieron Desactivar los Updates. Revisa el Log para más información.", "Error WUpdate", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR: No se pudo Deshabilitar WU: " & ex.Message)
            End Try
        End If
    End Sub


    Private Sub BtLimpiarPerfil_Click(sender As Object, e As EventArgs) Handles BtLimpiarPerfil.Click
        RegistroInstalacion("\\ Ejecutado proceso LIMPIEZA USUARIO ADRA \\")
        Dim equipousuario As String = (My.User.Name)
        Dim equipo As Integer = equipousuario.LastIndexOf("\")
        Dim usuario = equipousuario.Remove(0, equipo + 1).ToUpper

        If usuario <> "USUARIO" Then
            Dim sesionusuario As DialogResult = MessageBox.Show("== Debes Iniciar Sesión con el perfil USUARIO ==" & vbCrLf & "Para ello debemos Cerrar Sesión y en Otros Usuarios escribe .\USUARIO sin contraseña." & vbCrLf & "Tras el Cierre de Sesión deberás volver a ejecutar este Instalador." & vbCrLf & "Si deséas realizar otras tareas haz clic en NO. Te mostraremos el formulario igualmente pero habrá funciones que no podrás usar.", "Sesión de " & usuario & " no válida.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If sesionusuario = DialogResult.Yes Then
                RegistroInstalacion("Se inició sesión con " & usuario & ". Se cierra su sesión para iniciar como .\USUARIO local.")
                Shell("shutdown /l /f", AppWinStyle.Hide, True)
                Exit Sub
                Me.Close()
            ElseIf sesionusuario = DialogResult.No Then
                FormUsuarioAdra.ShowDialog()
                RegistroInstalacion("Usuario cancela el Cierre de Sesión del perfil actual. Se muestra el formulario con botones deshabilitados.")
                Exit Sub
            Else
                RegistroInstalacion("Iniciada sesión como Usuario. Se omite esta comprobación.")
                Exit Sub
            End If
        End If
        NotinrappZ()
    End Sub

    Private Sub NotinrappZ()
        'Advertir de las carpetas que se van a borrar.
        If unidadZ() = False Then
            MessageBox.Show("No se pudo conectar a \\NotinRAPP\Z. A continuación te dirigimos para que realices la conexión manualmente. Una vez hecha aguarda unos segundos y el proceso continuará.", "Conexión Manual a Z del Rapp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            FrmInstaladorKubo.RegistroInstalacion("No se pudo conectar automáticamente al Rapp. Se procede a ofrecer la conexión manual.")
            Try
                My.Computer.Clipboard.SetText("\\NOTINRAPP\Z")
                My.Computer.Keyboard.SendKeys("^{ESC}", True)
                Threading.Thread.Sleep(500)
                SendKeys.Send("^V")
                Threading.Thread.Sleep(2000)
                SendKeys.Send("{ENTER}")
            Catch ex As Exception
                MessageBox.Show("No se pudo llamar a la utilidad de Unidades de Red. Debes conectar la Unidad Z manualmente hacia \\NotinRapp\Z y entonces volver a ejecutar esta utilidad.", "Sin conxión con NotinRapp\Z", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("No se pudo llamar a la utilidad de Unidades de Red. Debes conectar la Unidad Z manualmente hacia \\NotinRapp\Z y entonces volver a ejecutar esta utilidad.")
                RegistroInstalacion("Motivo Error anterior: " & ex.Message)
                Exit Sub
            End Try
        End If

        Threading.Thread.Sleep(20000)
        While unidadZ() = False
            Dim QueHacerZ As DialogResult = MessageBox.Show("Unidad NotinRapp\Z no conectada. No se puede limpiar el Perfil del Usuario.", "Sin acceso a Perfiles en NotinRapp", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning)
            If QueHacerZ = DialogResult.Cancel Then
                MessageBox.Show("Operación cancelada por el usuario. No se sigue con el resto de automatizaciones.", "Proceso cancelado.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("Operación cancelada por el usuario. No se sigue con el resto de automatizaciones.")
                Exit While
                Exit Sub
            End If
        End While
        FormUsuarioAdra.ShowDialog()
    End Sub

    Private Function unidadZ() As Boolean
        Shell("NET USE Z: \\NOTINRAPP\Z b30330104b /user:NOTARIA\ADMINISTRADOR /p:no", AppWinStyle.NormalFocus, True)
        If Directory.Exists("Z:\rapp_control") = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub BtCopiaNotario_Click(sender As Object, e As EventArgs) Handles BtCopiaNotario.Click
        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "Scripts")
        Dim entregaprotocolo As String = "http://tecnicos.notin.net/descargas/comunicados/979//Entrega_Protocolo.rar"
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe --show-progress -t 5 " & """" & entregaprotocolo & """" & " -O " & RutaDescargas & "Scripts\Entrega_Protocolo.rar", AppWinStyle.Hide, True)

        obtenerunrar()
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Scripts\Entrega_Protocolo.rar " & RutaDescargas & "Scripts\", AppWinStyle.NormalFocus, True)
        Try
            Process.Start(RutaDescargas & "Scripts\entregafinal.bat")
        Catch ex As Exception
            MessageBox.Show("Se ha producido un error. Consulta el Log para mas información.", "Error abriendo proceso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR Entrega Protocolo: " & ex.Message)
        End Try

        'Threading.Thread.Sleep(10000)
        'TODO Preguntar si mostrar ruta para visualizar el acta
        Try
            Process.Start("explorer.exe", RutaDescargas & "Scripts\")
        Catch ex As Exception
            RegistroInstalacion("ERROR Ruta Scripts: " & ex.Message)
        End Try

    End Sub


    ' -----------------------------------------------------
    '-----------------------TEST ADRA ---------------------
    Private Sub BTTESTADRA_Click(sender As Object, e As EventArgs)
        RegistroInstalacion("Ejecutado TEST ADRA. Botón solo habilitado para pruebas.")
        'Process.Start("C:\WINDOWS\system32\systempropertiesadvanced.exe")
        Shell("cmd.exe /c %WINDIR%\system32\systempropertiesadvanced.exe", AppWinStyle.Hide, True)


        'Shell("control system", AppWinStyle.NormalFocus, False)
        Threading.Thread.Sleep(2000)
        'AppActivate("Sistema")

        'Dim vecestab As Integer = 0
        'While 11 > vecestab
        AppActivate("Propiedades del sistema")
        Threading.Thread.Sleep(1000)
        SendKeys.Send("{TAB}")
        '    vecestab = vecestab + 1
        'End While
        ''Entramos a las Opciones Avanzadas del Sistema
        'SendKeys.Send("{ENTER}")
        'Threading.Thread.Sleep(1000)


        'Threading.Thread.Sleep(1000)
        'SendKeys.Send("{TAB}")
        SendKeys.Send("{ENTER}")





        'Dim carpetasperfil = Directory.GetDirectories("C:\Notawin.Net\")
        'Dim totalcarpetas As Integer = carpetasperfil.Count - 1
        'Dim numcarpeta = 0
        'File.WriteAllText("C:\TEMP\carpetas.txt", "")
        'While numcarpeta < totalcarpetas
        '    File.AppendAllText("C:\TEMP\carpetas.txt", " " & """" & carpetasperfil(numcarpeta) & """")
        '    numcarpeta = numcarpeta + 1
        '    Directory.Delete(numcarpeta, True)
        'End While

        'FormUsuarioAdra.ShowDialog()
    End Sub


    Private Sub BtNotinAdraDiferido_Click(sender As Object, e As EventArgs) Handles BtNotinAdraDiferido.Click
        'COMPROBAR QUE TRABAJAMOS EN UN SERVIDOR NOTINRAPP.
        If NotinRapp() = False Then
            MessageBox.Show("Por seguridad solo se permite la ejecución en Entornos ADRA bajo el host NotinRapp. Se cancela la ejecución.", "Ejecución fuera de AdRa", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            RegistroInstalacion("ADRA: Se cancela la actualización diferida de Adra. El Host no coincide.")
            BtNotinAdraDiferido.BackColor = SystemColors.Control
            LBAdraDiferido.Visible = False
            Exit Sub
        Else
            RegistroInstalacion("Detectado entorno ADRA. Se permite la ejecución de Actualización Adra Diferida.")
        End If

        'MENSAJE ADVERTENCIA PROCESOS
        Dim horaseleccionada = LboxHoraAdraDiferido.SelectedItem
        Dim minutoseleccionado = LboxMinutoAdraDiferido.SelectedItem

        If horaseleccionada = Nothing Then
            horaseleccionada = "22"
            RegistroInstalacion("HORA EJECUCIÓN: No se ha indicado una HORA válida. Se aplica valor por defecto a las 22 horas.")
        End If
        If minutoseleccionado = Nothing Then
            minutoseleccionado = "0"
            RegistroInstalacion("HORA EJECUCIÓN: No se ha indicado un MINUTO válido. Se aplica valor por defecto a las en punto.")
        End If


        Dim adradiferido As DialogResult = MessageBox.Show("PROGRAMAR EJECUCIÓN A LAS " & horaseleccionada & ":" & minutoseleccionado & " HORAS." & vbCrLf & "En ese momento se procederá a terminar los procesos que afecten a la actualización tales como Notin, Word, Nexus..." & vbCrLf & "Ejecutaremos MigradorSQL con AllowDataLoss y se Descargará Versión de Notin y Net Estable o Beta según se marque." & vbCrLf & "Bajo un entorno estándar completar este proceso llevará quince minutos. Previamente no se realizará ninguna acción. Si deseas Cancelar termina el proceso del Instalador." & vbCrLf & "¿Deseas programar la ejecución a la hora seleccionada?", "Advertencia actualización diferida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If adradiferido = DialogResult.Yes Then
            RegistroInstalacion("= Programada ACTUALIZACIÓN DIFERIDA NOTIN .NET entorno ADRA = HORA: " & horaseleccionada & ":" & minutoseleccionado & vbCrLf & "Se irán logeando el resto de eventos producidos tras la hora de la ejecución.")

            'PARADA HASTA LAS 22.00 HORAS
            'Dim horaejecucion As String = "22:0"
            'Dim horaejecucion As String = "10:51"


            'If horaseleccionada = Nothing Then
            '    'horaseleccionada = "22"
            '    MessageBox.Show("Selecciona una HORA válida en el listado.", "Sin selección en lista", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    BtNotinAdraDiferido.BackColor = SystemColors.Control
            '    LBAdraDiferido.Visible = False
            '    Exit Sub
            'End If

            'If minutoseleccionado = Nothing Then
            '    'MessageBox.Show("Selecciona un MINUTO válido en el listado.", "Sin selección en lista", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    RegistroInstalacion("HORA EJECUCIÓN: No se ha indicado un minuto válido. Se aplica valor por defecto a las en punto.")
            '    minutoseleccionado = "0"
            'End If


            Dim horaprogramada = horaseleccionada & ":" & minutoseleccionado
            Dim horaactual As String = DateTime.Now.Hour & ":" & DateTime.Now.Minute
            While horaactual <> horaprogramada
                horaactual = DateTime.Now.Hour & ":" & DateTime.Now.Minute
                Threading.Thread.Sleep(20000)
            End While
            LBAdraDiferido.Text = "PROCESO DE ACTUALIZACION EN EJECUCIÓN. ESPERA..."
            LBAdraDiferido.ForeColor = Color.ForestGreen
            ActualizacionDiferidaAdra()
        Else
            BtNotinAdraDiferido.BackColor = SystemColors.Control
            LBAdraDiferido.Visible = False
            'EnvioMailADRA()
            Exit Sub
        End If

    End Sub

    Private Sub ActualizacionDiferidaAdra()
        'TERMINAR PROCESOS NOTIN NET
        If ProcesosActivos() = True Then
            Try
                RegistroInstalacion("PROCESOS: Se encontraron procesos que afectan a la actualización. Se procede a su finalización.")
                Shell("cmd /c taskkill.exe /f /im winword.exe & taskkill.exe /f /im msaccess.exe & taskkill.exe /f /im notinnetdesktop.exe & taskkill.exe /f /im nexus.exe", AppWinStyle.Hide, True)
            Catch ex As Exception
                RegistroInstalacion("ERROR PROCESOS: " & ex.Message)
                BtNotinAdraDiferido.BackColor = Color.LightSalmon
            End Try
        Else
            RegistroInstalacion("PROCESOS: No hay procesos que afecten a la instalación. Se prosigue sin ejecutar KILL.")
        End If


        'MIGRADOR
        obtenerwget()
        Directory.CreateDirectory(RutaDescargas & "NotinNet")
        Shell("cmd.exe /c " & RutaDescargas & "wget.exe -q --show-progress -t 5 https://static.unidata.es/MigradorNotinSQL.exe -O " & RutaDescargas & "NotinNet\MigradorNotinSQL.exe", AppWinStyle.Hide, True)

        Try
            File.WriteAllText(RutaDescargas & "NotinNet\MigradorNotinSQLAllowDataLoss.bat", "@echo off" & vbCrLf & RutaDescargas & "NotinNet\MigradorNotinSQL.exe /allowdataloss")
            Dim pmigrador As New ProcessStartInfo()
            pmigrador.FileName = RutaDescargas & "NotinNet\MigradorNotinSQLAllowDataLoss.bat"
            Dim migrador As Process = Process.Start(pmigrador)
            migrador.WaitForExit()
            cIniArray.IniWrite(instaladorkuboini, "SQL", "EJECUCIONMIGRADOR", DateTime.Now)
            LbVersionMigrador.Visible = True
            TbMigradorLog.Visible = True

            RegistroInstalacion("MIGRADORSQL: Ejecutado Migrador correctamente pasando AllowDataLoss.")

            LeerLogMigradorSQL()

        Catch ex As Exception
            RegistroInstalacion("ERROR MIGRADOR: " & ex.Message)
            BtNotinAdraDiferido.BackColor = Color.LightSalmon
        End Try

        'NOTIN 8
        Dim urlnotin8 = "http://static.unidata.es/NotariaEvo/v40/notin8.exe"
        Dim WGETNOTIN8 As String = "wget.exe -q --show-progress -t 5 " & urlnotin8 & " -O " & RutaDescargas & "NotinNet\Notin8.exe"
        Dim RutaCMDWgetNotin8 As String = RutaDescargas & WGETNOTIN8
        Shell("cmd.exe /c " & RutaCMDWgetNotin8, AppWinStyle.Hide, True)
        RegistroInstalacion("NOTIN8 descargado correctamente desde static.unidata.es")

        Try
            File.Delete("F:\Notawin.Net\NotinNetInstaller.exe")
            RegistroInstalacion("LIMPIAR VERSION NET en F:\Notawin.Net para evitar conflictos de versión Estable/Beta")
        Catch ex As Exception
            RegistroInstalacion("ADVERTENCIA. No se pudo limpiar Versión de Net en F:\Notawin.Net. " & ex.Message)
        End Try

        Try
            Dim pnotin8 As New ProcessStartInfo()
            pnotin8.FileName = RutaDescargas & "NotinNet\Notin8.exe"
            Dim notin8 As Process = Process.Start(pnotin8)
            notin8.WaitForExit()
            RegistroInstalacion("NOTIN8: Ejecutado correctamente su Instalador.")
            BtNotin8exe.BackColor = Color.PaleGreen
        Catch ex As Exception
            BtNotin8exe.BackColor = Color.LightSalmon
            RegistroInstalacion("ERROR NOTIN8: " & ex.Message)
            BtNotinAdraDiferido.BackColor = Color.LightSalmon
        End Try

        If UnidadF() = True Then
            Try
                obtenerrobocopy()
                Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "NotinNet\ F:\ Notin8.exe", AppWinStyle.Hide, True)
                RegistroInstalacion("NOTIN8: Notin8.exe copiado correctamente a F:\ para futuras ejecuciones.")
            Catch ex As Exception
                RegistroInstalacion("ERROR: Notin8.exe no se pudo copiar a F. Causa: " & ex.Message)
                BtNotin8exe.BackColor = Color.LightSalmon
            End Try
        Else
            RegistroInstalacion("ADVERTENCIA: NOTIN8 no copiado a F: al no encontrarse la Unidad disponible.")
        End If

        'EJECUCIÓN .NET DESCARGADO
        If CbBetaAdra.Checked = True Then
            Dim urlbeta As String = "https://static.unidata.es/NotinNetInstaller/v40/beta/NotinNetInstaller.exe"
            Directory.CreateDirectory(RutaDescargas & "NotinNet")
            Shell("cmd /c " & RutaDescargas & "wget.exe -q - --show-progress " & urlbeta & " -O " & RutaDescargas & "NotinNet\NotinNetInstaller.exe", AppWinStyle.Hide, True)

            'INSTALACION BETA NET
            Try
                Dim pnotinnetbeta As New ProcessStartInfo()
                pnotinnetbeta.FileName = RutaDescargas & "NotinNet\NotinNetInstaller.exe"
                Dim notinnetbeta As Process = Process.Start(pnotinnetbeta)
                notinnetbeta.WaitForExit()

                RegistroInstalacion("NOTIN .NET BETA: Ejecutado correctamente desde F:\Notawin.Net. Se procede a obtener versión instalada.")

                ObtenerVersionNet()
                'BtNotinNetF.Visible = True
            Catch ex As Exception
                RegistroInstalacion("ERROR NOTIN NET BETA: No se pudo ejecutar NotinNetInstaller Beta. " & ex.Message)
                BtNotinAdraDiferido.BackColor = Color.LightSalmon
            End Try

            'COPIAR BETA A F:\NOTAWIN.NET
            Try
                File.Copy(RutaDescargas & "NotinNet\NotinNetInstaller.exe", "F:\Notawin.Net\NotinNetInstaller.exe", True)
                RegistroInstalacion("BETA NET copiada correctamente a F:\Notawin.Net")
            Catch ex As Exception
                RegistroInstalacion("BETA NET. ERROR: No se pudo copiar a F:\Notawin.Net. " & ex.Message)
                BtNotinAdraDiferido.BackColor = Color.LightSalmon
            End Try


            'INSTALACION ESTABLE NET
        Else
            Try
                Dim pnotinnet As New ProcessStartInfo()
                pnotinnet.FileName = "F:\NOTAWIN.NET\NotinNetInstaller.exe"
                Dim notinnet As Process = Process.Start(pnotinnet)
                notinnet.WaitForExit()

                RegistroInstalacion("NOTIN .NET: Ejecutado correctamente desde F:\Notawin.Net. Se procede a obtener versión instalada.")

                ObtenerVersionNet()
                'BtNotinNetF.Visible = True
            Catch ex As Exception
                RegistroInstalacion("ERROR NOTIN NET: No se pudo ejecutar NotinNetInstaller de F tras la descarga de Notin8.exe. " & ex.Message)
                BtNotinAdraDiferido.BackColor = Color.LightSalmon
            End Try
        End If

        'TERMINAMOS PROCESO Y AVISAMOS
        RegistroInstalacion("= ACTUALIZACIÓN DIFERIDA ADRA FINALIZADA.=")
        BtNotinAdraDiferido.BackColor = Color.PaleGreen
        LBAdraDiferido.Visible = False
        'EnvioMailADRA()
        'MessageBox.Show("Proceso Actualización ADRA Finalizado." & vbCrLf & "Revisa Log o Correo enviado para más información.", "Actualización Completada", MessageBoxButtons.OK, MessageBoxIcon.Information)
        MessageBox.Show("Proceso Actualización ADRA Finalizado." & vbCrLf & "En verde todo correcto. Rojo indicará algún error. Revisa Log para más información.", "Actualización Completada", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'TODO AÑADIR ALGO EN EL INI PARA INDICAR QUE SE HIZO O NO BIEN
    End Sub

    Private Sub BtNotinAdraDiferido_MouseDown(sender As Object, e As MouseEventArgs) Handles BtNotinAdraDiferido.MouseDown
        BtNotinAdraDiferido.BackColor = Color.NavajoWhite

        LBAdraDiferido.Visible = True

        Dim horaseleccionada = LboxHoraAdraDiferido.SelectedItem
        Dim minutoseleccionado = LboxMinutoAdraDiferido.SelectedItem

        If horaseleccionada = Nothing Then
            horaseleccionada = "22"
        End If
        If minutoseleccionado = Nothing Then
            minutoseleccionado = "0"
        End If

        LBAdraDiferido.Text = "PROGRAMADA ACTUALIZACIÓN ADRA A LAS " & horaseleccionada & ":" & minutoseleccionado & " HORAS."
    End Sub

    Public Sub EnvioMailADRA()
        'TODO enviar email cuando termine la instalación con el log e info sistema
        If Validaremail() = True Then

            'Dim A As String = Tbtucorreo.Text
            Dim a As String = CBoxEmail.Text
            Dim Destinatario As MailAddress = New MailAddress(a)

            Dim correo As New MailMessage
            Dim smtp As New SmtpClient()

            correo.From = New MailAddress("instaladorkubo@gmail.com", "Instalador Kubo", System.Text.Encoding.UTF8)
            correo.To.Add(Destinatario)
            correo.SubjectEncoding = System.Text.Encoding.UTF8
            correo.Subject = "Actualización Diferida ADRA " & TbIdentificaNotaria.Text
            'correo.Body = "" & vbCrLf & "Las descargas finalizaron a las " & DateTime.Now.Hour & " horas " & "y " & DateTime.Now.Minute & " minutos." & vbCrLf & "Puedes proceder a realizar las instalaciones cuando quieras." & vbCrLf & "Cualquier duda tienes disponible el Comunicado 1573: http://tecnicos.notin.net/detalles.asp?id=1573" & vbCrLf & "Muchas gracias"
            Dim loggerinstalador As String = File.ReadAllText("C:\TEMP\InstaladorKubo\RegistroInstalacion.txt")
            correo.Body = loggerinstalador
            correo.BodyEncoding = System.Text.Encoding.UTF8
            'correo.IsBodyHtml = False(formato tipo web o normal:  true = web)
            correo.IsBodyHtml = False
            correo.Priority = MailPriority.Normal

            smtp.Credentials = New System.Net.NetworkCredential("instaladorkubo@gmail.com", "juanmola2017")
            smtp.Port = 587
            smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True


            Try
                smtp.Send(correo)
                RegistroInstalacion("Correo de notificación enviado a " & CBoxEmail.Text)
            Catch ex As Exception
                RegistroInstalacion("ERROR Email: " & ex.Message)

            End Try
        Else
            RegistroInstalacion("ADVERTENCIA: No se pudo notificar por correo. La dirección " & CBoxEmail.Text & " no se consideró válida o no se indicó ningunta dirección.")
        End If

    End Sub

    Private Sub BtBackupNet_Click(sender As Object, e As EventArgs) Handles BtBackupNet.Click
        If Directory.Exists(RutaDescargas & "NotinNet\BackupNet") OrElse Directory.Exists("F:\Notawin.Net\BackupNet") Then
            Try
                Dim limpiezanet As String = "@echo off" & vbCrLf & "FORFILES /P " & RutaDescargas & "NotinNet\BackupNet /S /D -15 /M *.exe /c " & """" & "CMD /c DEL /Q @PATH" & """"
                Dim limpiezanetf As String = vbCrLf & "FORFILES /P F:\Notawin.Net\BackupNet /S /D -15 /M *.exe /c " & """" & "CMD /c DEL /Q @PATH" & """"
                File.WriteAllText(RutaDescargas & "NotinNet\BackupNet\LimpiarNet.bat", limpiezanet)
                File.AppendAllText(RutaDescargas & "NotinNet\BackupNet\LimpiarNet.bat", limpiezanetf)
                'Process.Start(RutaDescargas & "NotinNet\BackupNet\LimpiarNet.bat")
                Shell("cmd /c " & RutaDescargas & "NotinNet\BackupNet\LimpiarNet.bat", AppWinStyle.Hide, True)
                RegistroInstalacion("INFO. Limpiados ficheros en BackupNet de mas de 15 días.")
                File.Delete(RutaDescargas & "NotinNet\BackupNet\LimpiarNet.bat")
            Catch ex As Exception
                RegistroInstalacion("INFO. No se limpiaron los ficheros de BackupNet." & ex.Message)
            End Try
        End If

        If Directory.Exists(RutaDescargas & "NotinNet\BackupNet") Then
            Process.Start("explorer.exe", RutaDescargas & "NotinNet\BackupNet")
        ElseIf Directory.Exists("F:\NOTAWIN.NET\BackupNet") Then
            Process.Start("explorer.exe", "F:\NOTAWIN.NET\BackupNet")
        Else
            MessageBox.Show("No se encontraron las Rutas de Backup para NotinNet. ¿Seguro realizaste una instalación previa con este Instalador?", "Ruta BackupNet no accesible", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    '    Private Sub LboxHoraAdraDiferido_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LboxHoraAdraDiferido.SelectedIndexChanged
    '       BtNotinAdraDiferido.Text = "Programar ejecución a las " & LboxHoraAdraDiferido.SelectedItem & ":" & LboxMinutoAdraDiferido.SelectedItem & " horas."
    '  End Sub

    'Private Sub LboxMinutoAdraDiferido_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LboxMinutoAdraDiferido.SelectedIndexChanged
    '   BtNotinAdraDiferido.Text = "Programar ejecución a las " & LboxHoraAdraDiferido.SelectedItem & ":" & LboxMinutoAdraDiferido.SelectedItem & " horas."
    'End Sub






    'Private Sub AbreExcel()
    '    'TODO establecer asociacion de archivos.
    '    Try
    '        My.Computer.Network.DownloadFile(PuestoNotin & "AbreExcel.exe", RutaDescargas & "AbreExcel.exe", "juanjo", "Palomeras24", False, 20000, False)
    '        File.Copy(RutaDescargas & "AbreExcel.exe", "C:\Notawin.Net\AbreExcel.exe", True)
    '    Catch ex As Exception

    '    End Try
    'btNotinKubo.ForeColor = Color.YellowGreen
    'End Sub




    '        RegistroInstalacion("ERROR: Detectada posible instalación de Office 2016. Debe limpiarse antes de proceder a relizar la instalación desatendida.")
    '    End If
    'End If


End Class