Imports System.IO
Imports System.Text
Imports InstaladorKubo.LeerFicherosINI
Imports System.Threading
Imports System.Net.Mail

'WEB DE INSTALACIÓN
'http://instalador.notin.net


Public Class InstaladorKubo
    'CONTROLES DESCARGAS VARIABLES STRING
    Private Const FILE_DOWNLOAD As String = "descargas.txt"
    Private Const REQUISITOS_DOWNLOAD As String = "requisitos.txt"
    Private Const TERCEROS_DOWNLOAD As String = "terceros.txt"
    Private Const REGISTRO_DOWNLOAD As String = "registro.txt"
    Private Const PuestoNotin As String = "ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/"
    Private RutaDescargas As String = GetPathTemp() 'PATH_TEMP
    Private Const instaladorkuboini = "C:\TEMP\InstaladorKubo\InstaladorKubo.ini"


    Private Sub frmInstaladorNotin_Load(sender As Object, e As EventArgs) Handles Me.Load

        Directory.CreateDirectory("C:\TEMP\InstaladorKubo")
        File.AppendAllText("C:\TEMP\InstaladorKubo\RegistroInstalacion.txt", vbCrLf & vbCrLf)
        RegistroInstalacion("=== NUEVA EJECUCION DEL INSTALADOR ===")

        SistemaOperativo()
        lbRuta.Text = GetPathTemp()
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
    End Sub

    Private Sub InstaladorKubo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Keys.J Then
            BtLimpiar.Visible = True
            BtLogin.Visible = True
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
            btOdbc.BackColor = Color.PaleGreen
        ElseIf odbc = 0 Then
            btOdbc.BackColor = Color.LightSalmon
        End If

        Dim framework = cIniArray.IniGet(instaladorkuboini, "REQUISITOS", "FRAMEWORK35", "2")
        If framework = 1 Then
            btFramework.BackColor = Color.PaleGreen
        Else
            btFramework.BackColor = SystemColors.Control
        End If

        Dim excepjava = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "EXCEPJAVA", "2")
        If excepjava = 1 Then
            btExcepJava.BackColor = Color.PaleGreen
        Else
            btExcepJava.BackColor = SystemColors.Control
        End If
        Dim java = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "JAVA8", "2")
        If java = 1 Then
            btJava.BackColor = Color.PaleGreen
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
            btDirectivas.BackColor = Color.PaleGreen
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
    End Sub

    'ACCEDER A URL NOTARIADO
    'Private Sub lblAncert_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    '    System.Diagnostics.Process.Start("http://soporte.notariado.org")
    'End Sub

    Private Sub LnkRequisitos_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        'TODO añadir funcionalidad para url requisitos: https://docs.google.com/document/d/1NPprOtwrgrz6evWbYzYDfsjGrG9jcPAwX0aNjSkx5wQ/edit?usp=sharing
    End Sub

    'Boton EXAMINAR
    Private Sub btDirDescargas_Click(sender As Object, e As EventArgs) Handles btDirDescargas.Click
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
                cbOffice2003.ForeColor = Color.DarkGreen
                '         cbOffice2003.Enabled = False
            ElseIf Archivo2003.Length < "517577131" Then
                cbOffice2003.ForeColor = Color.Red
            End If
        Else
            cbOffice2003.ForeColor = SystemColors.ControlText
            '     cbOffice2003.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Registro\ConfigAccess.reg") Then
            Dim ConfigNotin As New FileInfo(RutaDescargas & "Registro\ConfigAccess.reg")
            Dim LengthNotin As Long = ConfigNotin.Length
            If ConfigNotin.Length = "16688" Then
                cbConfiguraNotin.ForeColor = Color.DarkGreen
                '        cbConfiguraNotin.Enabled = False
            End If
        Else
            cbConfiguraNotin.ForeColor = SystemColors.ControlText
            '     cbConfiguraNotin.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Office2016.exe") Then
            Dim Archivo2016 As New FileInfo(RutaDescargas & "Office2016.exe")
            Dim Length2016 As Long = Archivo2016.Length
            If Archivo2016.Length = "739967123" Then
                cbOffice2016.ForeColor = Color.DarkGreen
                cbOffice2016odt.ForeColor = Color.DarkGreen
                '         cbOffice2016.Enabled = False
            ElseIf Archivo2016.Length < "739967123" Then
                cbOffice2016.ForeColor = Color.Red
                cbOffice2016odt.ForeColor = Color.Red
            End If
        Else
            cbOffice2016.ForeColor = SystemColors.ControlText
            cbOffice2016odt.ForeColor = SystemColors.ControlText
            '     cbOffice2016.Enabled = True
        End If

        'If System.IO.File.Exists(RutaDescargas & "Office2016odt.exe") Then
        '    Dim Archivo2016odt As New FileInfo(RutaDescargas & "Office2016odt.exe")
        '    Dim Length2016odt As Long = Archivo2016odt.Length
        '    If Archivo2016odt.Length = "2452664263" Then
        '        cbOffice2016odt.ForeColor = Color.DarkGreen
        '        '        cbOffice2016odt.Enabled = False
        '    ElseIf Archivo2016odt.Length < "2452664263" Then
        '        cbOffice2016odt.ForeColor = Color.Red
        '    End If
        'Else
        '    cbOffice2016odt.ForeColor = SystemColors.ControlText
        '    '      cbOffice2016odt.Enabled = True
        'End If

        If System.IO.File.Exists(RutaDescargas & "ConfWord2016.rar") Then
            Dim Config2016 As New FileInfo(RutaDescargas & "ConfWord2016.rar")
            Dim LengthConfig2016 As Long = Config2016.Length
            If Config2016.Length = "8211" Then
                cbConfiguraWord2016.ForeColor = Color.DarkGreen
                '        cbConfiguraWord2016.Enabled = False
            End If
        Else
            cbConfiguraWord2016.ForeColor = SystemColors.ControlText
            '      cbConfiguraWord2016.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "jnemo-latest.exe") Then
            Dim jNemo As New FileInfo(RutaDescargas & "jnemo-latest.exe")
            Dim LengthjNemo As Long = jNemo.Length
            If jNemo.Length = "12672337" Then
                cbNemo.ForeColor = Color.DarkGreen

                '         cbNemo.Enabled = False
            End If
        Else
            cbNemo.ForeColor = SystemColors.ControlText
            '     cbNemo.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "PuestoNotinC.exe") Then
            Dim PuestoNotinC As New FileInfo(RutaDescargas & "PuestoNotinC.exe")
            Dim LengthPuestoNotinC As Long = PuestoNotinC.Length
            If PuestoNotinC.Length = "17034966" Then
                cbPuestoNotin.ForeColor = Color.DarkGreen
                '        cbPuestoNotin.Enabled = False
            End If
        Else
            cbPuestoNotin.ForeColor = SystemColors.ControlText
            '    cbPuestoNotin.Enabled = True
        End If

        'REQUISITOS. Cuento los archivos en el directorio
        If System.IO.Directory.Exists(RutaDescargas & "\Requisitos") Then
            Dim ArchivosenRequisitos = My.Computer.FileSystem.GetFiles(RutaDescargas & "\Requisitos")

            If ArchivosenRequisitos.Count >= 4 Then
                cbRequisitos.ForeColor = Color.DarkGreen
                '                cbRequisitos.Enabled = False
            Else
                cbRequisitos.ForeColor = Color.Red
            End If
        Else
            cbRequisitos.ForeColor = SystemColors.ControlText
            '        cbRequisitos.Enabled = True
        End If

        'ANCERT Como puede variar el tamaño solo miro que exista el fichero
        If System.IO.File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
            cbSferen.ForeColor = Color.DarkGreen
            '            cbSferen.Enabled = False
        Else
            cbSferen.ForeColor = SystemColors.ControlText
            '          cbSferen.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
            cbPasarelaSigno.ForeColor = Color.DarkGreen
            '       cbPasarelaSigno.Enabled = False
        Else
            cbPasarelaSigno.ForeColor = SystemColors.ControlText
            '     cbPasarelaSigno.Enabled = True
        End If

        'SOFTWARE TERCEROS
        If System.IO.Directory.Exists(RutaDescargas & "\Software") Then
            cbTerceros.ForeColor = Color.DarkGreen
            BtExplorarRutas.Enabled = True
        Else
            cbTerceros.ForeColor = SystemColors.ControlText
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
        End If


    End Sub
#End Region


    Private Sub obtenerwget()
        If Not System.IO.File.Exists(RutaDescargas & "wget.exe") Then
            'Descargar ejecutable WGet
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "juanjo", "Palomeras24", False, 20000, False)
            Catch ex As Exception

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "juanjo", "Palomeras24", False, 20000, False)
                If REINTENTAR = DialogResult.Retry Then
                    My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "juanjo", "Palomeras24", False, 20000, True)
                End If
            End Try
        End If
    End Sub

    Private Sub obtenerrobocopy()
        If Not System.IO.File.Exists(RutaDescargas & "robocopy.exe") Then
            'Descargar ejecutable WGet
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "robocopy.exe", RutaDescargas & "robocopy.exe", "juanjo", "Palomeras24", False, 20000, False)
            Catch ex As Exception

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "robocopy.exe", RutaDescargas & "robocopy.exe", "juanjo", "Palomeras24", False, 20000, False)
                If REINTENTAR = DialogResult.Retry Then
                    My.Computer.Network.DownloadFile(PuestoNotin & "robocopy.exe", RutaDescargas & "robocopy.exe", "juanjo", "Palomeras24", False, 20000, True)
                End If
            End Try
        End If
    End Sub


    Private Sub obtenerunrar()
        'Comprobamos si existe ya unrar.exe
        If Not System.IO.File.Exists(RutaDescargas & "unrar.exe") Then

            'Descargar ejecutable UnRAR
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "juanjo", "Palomeras24", False, 20000, True)
            Catch ex As Exception
                MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "juanjo", "Palomeras24", False, 20000, True)
                If REINTENTAR = DialogResult.Retry Then
                End If
            End Try
        End If
    End Sub

#Region "COMENZAR DESCARGAS"
    Private Sub btDescargar_Click(sender As Object, e As EventArgs) Handles btDescargar.Click

        'Si no chequeas nada salimos
        If (cbConfiguraNotin.Checked OrElse cbConfiguraWord2016.Checked OrElse cbNemo.Checked OrElse cbOffice2003.Checked OrElse cbOffice2016.Checked OrElse cbOffice2016odt.Checked OrElse cbPasarelaSigno.Checked OrElse cbPuestoNotin.Checked OrElse cbRequisitos.Checked OrElse cbSferen.Checked OrElse cbTerceros.Checked OrElse CbFineReader.Checked) = False Then
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
        If cbOffice2003.Checked Then
            texto = texto & PuestoNotin & "Office2003.exe" & vbCrLf
            texto = texto & PuestoNotin & "Setup.mst" & vbCrLf
            texto = texto & PuestoNotin & "Setup2003.mst" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2003", "1")
        End If

        If cbOffice2016.Checked Then
            texto = texto & PuestoNotin & "KMSpico10.exe" & vbCrLf
            texto = texto & PuestoNotin & "Office2016.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016", "1")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016ODT", "0")
        End If

        If cbOffice2016odt.Checked Then
            texto = texto & PuestoNotin & "KMSpico10.exe" & vbCrLf
            texto = texto & PuestoNotin & "Office2016.exe" & vbCrLf
            texto = texto & PuestoNotin & "setup2016.MSP" & vbCrLf
            texto = texto & PuestoNotin & "Setup2016SinWord.MSP" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016ODT", "1")
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "OFFICE2016", "0")
        End If

        If cbNemo.Checked Then
            texto = texto & "http://nemo.notin.net/jnemo-latest.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "NEMO", "1")
        End If

        If cbPuestoNotin.Checked Then
            texto = texto & PuestoNotin & "PuestoNotinC.exe" & vbCrLf
            texto = texto & PuestoNotin & "AccesosDirectos.exe" & vbCrLf
            texto = texto & PuestoNotin & "AccesosDirectos2003.exe" & vbCrLf
            texto = texto & PuestoNotin & "ScanImg_Beta_FT.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "PUESTONOTIN", "1")
        End If

        If cbRequisitos.Checked Then
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "KryptonSuite300.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "Office2003PrimaryInterop.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2005.exe" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2015.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "REQUISITOS", "1")
            '   requisitos = requisitos & PuestoNotin & "Requisitos/" & "Framework35.bat" & vbCrLf
        End If

        If cbSferen.Checked Then
            texto = texto & PuestoNotin & "SFeren-2.8.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "SFEREN", "1")
        End If

        If cbPasarelaSigno.Checked Then
            texto = texto & PuestoNotin & "PasarelaSigno.exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "PASARELASIGNO", "1")
        End If

        If cbTerceros.Checked Then
            terceros = terceros & PuestoNotin & "Software/" & "AcrobatReaderDC.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "FileZilla_3_win64-setup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "ChromeSetup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "Notepad_x64.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "WinRAR5.exe" & vbCrLf
            '    terceros = terceros & PuestoNotin & "Software/" & "FineReaderV11demo .exe" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "SOFTWARETERCEROS", "1")
        End If

        'Descagar configuradores del autochequeo
        If cbConfiguraNotin.Checked Then
            registro = registro & PuestoNotin & "ConfigAccess.reg" & vbCrLf
            registro = registro & PuestoNotin & "FTComoAdministrador.reg" & vbCrLf
            registro = registro & PuestoNotin & "VentanasSigno.reg" & vbCrLf
            registro = registro & PuestoNotin & "MSOUTL.OLB" & vbCrLf
            'registro = registro & PuestoNotin & "ExclusionDefender.reg" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "CLAVESREGISTRO", "1")
        End If

        If cbConfiguraWord2016.Checked Then
            'texto = texto & PuestoNotin & "ConfiguraWord2016.exe" & vbCrLf
            texto = texto & PuestoNotin & "ConfWord2016.rar" & vbCrLf
            cIniArray.IniWrite(instaladorkuboini, "DESCARGAS", "CONFIGURAWORD", "1")
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
        If cbRequisitos.Checked Then
            Dim WGETPANDORAREQUISITOS As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=juanjo --ftp-password=Palomeras24 -i " & """" & RutaDescargas & "requisitos.txt"" -P " & RutaDescargas & "Requisitos\"
            Dim RutaCMDWgetRequisitos As String = RutaDescargas & WGETPANDORAREQUISITOS

            Shell("cmd.exe /c " & RutaCMDWgetRequisitos, AppWinStyle.NormalFocus, True)
            '       YaDescargados()
        End If

        'Ejecutar WGET Terceros
        If cbTerceros.Checked Then
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
        If cbConfiguraNotin.Checked Then
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

        cbOffice2003.Checked = False
        cbOffice2016.Checked = False
        cbOffice2016odt.Checked = False
        cbNemo.Checked = False
        cbRequisitos.Checked = False
        cbPuestoNotin.Checked = False
        cbSferen.Checked = False
        cbPasarelaSigno.Checked = False
        cbTerceros.Checked = False
        cbConfiguraNotin.Checked = False
        cbConfiguraWord2016.Checked = False
        CbFineReader.Checked = False
        lbProcesandoDescargas.Visible = False

        EnvioMail()

        MessageBox.Show("DESCARGAS FINALIZADAS. Puedes encontrar los Paquetes en " & RutaDescargas, "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)

        BtCopiarhaciaF.Enabled = True
        btTodo.Text = "Marcar todos"
    End Sub
#End Region

    'Mensajes de acción
    Private Sub btDescargar_MouseDown(sender As Object, e As MouseEventArgs) Handles btDescargar.MouseDown
        lbProcesandoDescargas.Visible = True
    End Sub

    Private Sub btNotinKubo_MouseDown(sender As Object, e As MouseEventArgs) Handles btNotinKubo.MouseDown
        lbInstalando.Visible = True
    End Sub


#Region "MARCAR/DESMARCAR TODOS"
    ' Marcar TODOS. Opcion variable Marcar/Desmarcar todos
    Private MarcarTodos As Integer = 0
    Private Sub btTodo_Click(sender As Object, e As EventArgs) Handles btTodo.Click
        If MarcarTodos = 0 Then
            cbOffice2003.Checked = True
            cbOffice2016.Checked = False
            cbOffice2016odt.Checked = True
            cbNemo.Checked = True
            cbRequisitos.Checked = True
            cbPuestoNotin.Checked = True
            cbSferen.Checked = True
            cbPasarelaSigno.Checked = True
            cbTerceros.Checked = True
            btTodo.Text = "Desmarcar"
            'sumar uno a la variable
            MarcarTodos = 1
        ElseIf MarcarTodos = 1 Then
            cbOffice2003.Checked = False
            cbOffice2016.Checked = False
            cbOffice2016odt.Checked = False
            cbNemo.Checked = False
            cbRequisitos.Checked = False
            cbPuestoNotin.Checked = False
            cbSferen.Checked = False
            cbPasarelaSigno.Checked = False
            cbTerceros.Checked = False
            CbFineReader.Checked = False
            btTodo.Text = "Marcar todos"
            MarcarTodos = 0
        End If
    End Sub

    'Autochequear Configuradores Notin y Word 2016 <> Office 2016odt
    Private Sub cbOffice2003_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2003.CheckedChanged
        cbConfiguraNotin.CheckState = cbOffice2003.CheckState
        CalcularTamanoDescarga(493, cbOffice2003.Checked)
    End Sub

    Private Sub cbOffice2016_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2016.CheckedChanged
        CalcularTamanoDescarga(705, cbOffice2016.Checked)
        cbConfiguraWord2016.CheckState = cbOffice2016.CheckState
        cbOffice2016odt.Checked = False
    End Sub

    Private Sub cbOffice2016odt_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2016odt.CheckedChanged
        CalcularTamanoDescarga(705, cbOffice2016odt.Checked)
        cbConfiguraWord2016.CheckState = cbOffice2016odt.CheckState
        cbOffice2016.Checked = False
    End Sub
#End Region

    Private Sub btSalir_Click(sender As Object, e As EventArgs) Handles btSalir.Click

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
        Me.Close()
    End Sub

    Private Sub btSalir_MouseDown(sender As Object, e As MouseEventArgs) Handles btSalir.MouseDown
        btSalir.BackColor = SystemColors.ControlLightLight
    End Sub

    'Control de registro de instalación
    Private Sub RegistroInstalacion(ByVal mensajelog As String)
        File.AppendAllText("C:\TEMP\InstaladorKubo\RegistroInstalacion.txt", DateTime.Now.Hour & ":" & DateTime.Now.Minute & " - " & mensajelog & vbCrLf)
    End Sub


#Region "COMIENZO DE INSTALACION DE PAQUETES NOTIN+KUBO. COMPROBACIONES INICIALES."

    Private Sub btNotinKubo_Click(sender As Object, e As EventArgs) Handles btNotinKubo.Click
        RegistroInstalacion("=== COMIENZO INSTALACIONES NOTIN-KUBO ===")

        'Comprobar si se ha efectuado alguna descarga
        Dim comienzo = cIniArray.IniGet(instaladorkuboini, "DESCARGAS", "COMIENZO", "2")
        Dim rutaenf = cIniArray.IniGet(instaladorkuboini, "PAQUETES", "TRAERDEF", "2")
        If comienzo = 2 AndAlso rutaenf = 2 Then
            MessageBox.Show("Descarga o Trae los Paquetes antes de comenzar las Instalaciones.", "¿Descargaste los paquetes?", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            'TODO la etiqueta no se oculta... Arreglar tb para Word 2003
            'lbProcesandoDescargas.Visible = False
            Exit Sub
        End If

        Dim claveinift = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "REGFT", "2")
        If claveinift = 2 Then
            Dim claveregft As DialogResult
            claveregft = MessageBox.Show("Se importarán Claves de Registro y será recomendable REINICIAR antes de proceder con la Instalación. ¿Realizamos la preparación inicial?", "Importar Claves y Reiniciar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If claveregft = DialogResult.Yes Then
                If File.Exists(RutaDescargas & "Registro\FTComoAdministrador.reg") = False Then
                    Directory.CreateDirectory(RutaDescargas & "Registro")
                    My.Computer.Network.DownloadFile(PuestoNotin & "FTComoAdministrador.reg", RutaDescargas & "Registro\FTComoAdministrador.reg", "juanjo", "Palomeras24", False, 60000, True)
                End If
                Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "1")

                PbInstalaciones.Visible = True
                PbInstalaciones.Maximum = 50
                PbInstalaciones.Step = 10
                Dim p As Integer
                While p < 6
                    p = p + 1
                    Threading.Thread.Sleep(600)
                    PbInstalaciones.PerformStep()
                End While


                MessageBox.Show("Vamos a REINICIAR. Guarda tu trabajo. Después continúa con las Instalaciones.", "Reinicio inicial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                RegistroInstalacion("ÉXITO: Se reinició el equipo para aplicar las Claves de Registro.")

                Dim reinicio As String = "shutdown /r /f /t 0"
                File.WriteAllText(RutaDescargas & "primerreinicio.bat", reinicio)
                RunAsAdmin(RutaDescargas & "primerreinicio.bat")
                Exit Sub
            ElseIf claveregft = DialogResult.No Then
                MessageBox.Show("Continuamos con el resto de instalaciones.", "Operación cancelada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "0")

                RegistroInstalacion("ADVERTENCIA: No se ejecutó el Reinicio tras importar las Claves de Registro.")

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
        If Not System.IO.File.Exists(RutaDescargas & "unrar.exe") Then

            'Descargar ejecutable UnRAR
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "juanjo", "Palomeras24", False, 20000, True)
                RegistroInstalacion("ÉXITO: Obtenido paquete UNRAR.EXE")
            Catch ex As Exception
                MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR: No se pudo obtener el ejecutable UNRAR.EXE.")
                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "juanjo", "Palomeras24", False, 20000, True)
                If REINTENTAR = DialogResult.Retry Then
                End If
            End Try
        End If


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
                InstalarRequisitosNet()
                RegistroInstalacion("Omitida instalación Office 2003. Pasamos a la instalación de los Pre-Requisitos.")
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
            RegistroInstalacion("ERROR: No se encontró el Paquete OFFICE2016.EXE. Esto es un problema para realizar su instalación.")
        End If

        EjecutableNotinNet()
    End Sub

    Private Sub EjecutableNotinNet()
        If UnidadF() = True Then
            Try
                'Dim ExisteNotinNet As Boolean = File.Exists("C:\Program Files (x86)\Humano Software\Notin\NotinNetDesktop.exe")
                'If ExisteNotinNet = False Then
                Try
                    File.Copy("F:\NOTAWIN.NET\NotinNetInstaller.exe", RutaDescargas & "NotinNetInstaller.exe", True)
                Catch ex As Exception
                    RegistroInstalacion("NotinNetInstaller no encontrado en la Unidad F. No ejecutamos esta función.")
                End Try

                Dim pnotinnet As New ProcessStartInfo()
                pnotinnet.FileName = RutaDescargas & "NotinNetInstaller.exe"
                Dim notinnet As Process = Process.Start(pnotinnet)
                'notintaskpane.WaitForInputIdle()
                notinnet.WaitForExit()
                RegistroInstalacion("Ejecutado instalador NotinNetInstaller.exe")
                'Shell("cmd.exe /c " & RutaDescargas & "NotinNetInstaller.exe", AppWinStyle.Hide, True)
                'End If
            Catch ex As Exception
                RegistroInstalacion("NotinNetInstaller: " & ex.Message)
            End Try
            'Ademas me traigo las Plantillas y el MDE
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
                        MessageBox.Show(ex.Message, "Revisa Instalacion de NotinNET. Continuamos.", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                    'Obtener texto entre caracteres
                    Dim expedientes As String = cIniArray.IniGet("F:\WINDOWS\NNotin.ini", "Expedientes", "Ruta", "Servidor")
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
                End If
            Else
                MessageBox.Show("Unidad F desconectada. No se puede configurar Word 2016.", "Configura WORD 2016", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
            End If
        Else
            RegistroInstalacion("ERROR: No se pudo Configurar WORD 2016. No se encontró la descarga de la Utilidad.")
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
        End If

        SoftwareAncert()
        'KMSPico()
    End Sub


    Private Sub SoftwareAncert()
        Dim Ancert As Integer = Nothing
        Ancert = MessageBox.Show("¿Instalar Software Ancert?", "Sferen y Pasarela", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Ancert = 6 Then
            If File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
                Shell("cmd.exe /c " & RutaDescargas & "SFeren-2.8.exe", AppWinStyle.Hide, True)
            Else
                RegistroInstalacion("ADVERTENCIA: Paquete Sferen no encontrado. No se instalará.")
            End If
            If File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PasarelaSigno.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                Shell("cmd.exe /c " & RutaDescargas & "\PasarelaSigno\setup.exe", AppWinStyle.Hide, True)
            Else
                RegistroInstalacion("ADVERTENCIA: Instalable PasarelaSigno no encontrado. No se instalará.")
            End If
        End If

        jNemo()
    End Sub

    Private Sub jNemo()
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
                btJava.BackColor = Color.PaleGreen
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
        tlpOffice2016odt.SetToolTip(cbOffice2016odt, "Descarga el paquete Office 2016 con instalación automatizada.")
        tlpOffice2016odt.IsBalloon = True

        tlpOffice2016.ToolTipTitle = "Paquete Office 2016 PERSONALIZABLE"
        tlpOffice2016.SetToolTip(cbOffice2016, "Descarga el paquete Office con instalación personalizable")

        tlpTerceros.ToolTipIcon = ToolTipIcon.Info
        tlpTerceros.ToolTipTitle = "Software recomendado"
        tlpTerceros.SetToolTip(cbTerceros, "Incluye Adobe Reader DC, FileZilla, Google Chrome y Notepad++")
        tlpTerceros.IsBalloon = True

        tlpNotinKubo.ToolTipTitle = "Comienza Instalaciones Notin y Word 2016 + Kubo"
        tlpNotinKubo.SetToolTip(btNotinKubo, "Preguntará por cada Paquete descargado. Sialgún software se encuentra instalado preguntará si se desea realizar la reinstalación.")

        TlpNotinWord2003.ToolTipTitle = "Comienza Instalación Notin y Word 2003"
        TlpNotinWord2003.SetToolTip(BtNotinWord2003, "Instala/Configura Notin y Word 2003. Además instalará Outlook, Excel..2016 si lo descargaste previamente.")

        'tlpAncert.ToolTipTitle = "URL Notariado"
        'tlpAncert.SetToolTip(lblAncert, "Acceder a url soporte.notariado.org")

        tlpOffice2003.ToolTipTitle = "Office 2003 DESATENDIDO"
        tlpOffice2003.SetToolTip(cbOffice2003, "Instalación automatizada ACCESS y librerías Outlook.")

        TlpRutaDescargas.ToolTipTitle = "Cambiar Carpeta Descargas"
        TlpRutaDescargas.SetToolTip(btDirDescargas, "Permite seleccionar una carpeta diferente para realizar las descargas.")

        TlpComenzarDescargas.ToolTipTitle = "Comenzar proceso Descargas"
        TlpComenzarDescargas.SetToolTip(btDescargar, "Se descargarán/resumirán los paquetes seleccionados.")

        TlpJava.ToolTipTitle = "Instalación Desatendida JAVA 8"
        TlpJava.SetToolTip(btJava, "Instalación silenciosa de Java. No requiere intervención del usuario")

        TlpUac.SetToolTip(BtUac, "Exclusiones Windows Defender y Control Cuentas Usuario")

        TlpCopiarServidor.ToolTipTitle = "Copia Paquetes al Servidor"
        TlpCopiarServidor.SetToolTip(BtCopiarhaciaF, "Copia los Paquetes descargados al Servidor para poder recogerlos posteriormente en otro equipo usando la opción siguiente.")

        TlpTraerServidor.ToolTipTitle = "Recoge Paquetes desde el Servidor"
        TlpTraerServidor.SetToolTip(BtTraerdeF, "Trae hacia la ruta Local los Paquetes copiados previamente al Servidor para así no tener que obtenerlos de Internet.")

        TlpConfigWord2016.ToolTipTitle = "Configurador independiente Word 2016"
        TlpConfigWord2016.SetToolTip(BtConfiguraWord2016, "Instala Notin Addin y TaskPane para Word 2016. La instalación Notin+Kubo ya realiza esta acción.")

        TlpKmspico.ToolTipTitle = "Instalación de KMSpico 10.2 FINAL"
        TlpKmspico.SetToolTip(BtKmsPico, "Descarga, descomprime e Instala KMSpico 10. Revisa antes las Excepciones del Antivirus.")

        TlpReconectarF.ToolTipTitle = "Reconectar Unidad F"
        TlpReconectarF.SetToolTip(BtReconectar, "Chequea la existencia Unidad F. Usa esto si la conectaste una vez arrancado el Instalador.")

        TlpDirectivas.ToolTipTitle = "Directivas de Windows"
        TlpDirectivas.SetToolTip(btDirectivas, "Aplica las Directivas de Windows. Para más información lee la hoja de Requisitos.")

        TlpExplorerDescargas.ToolTipTitle = "Explorar carpeta Descargas"
        TlpExplorerDescargas.SetToolTip(BtExplorarRutas, "Muestra en el explorador de archivos la ruta " & RutaDescargas)

        TlpSistema.ToolTipTitle = "Información Sistema Operativo"
        TlpSistema.SetToolTip(GroupBox3, "Si alguna característica puede no cumplir los Requisitos se mostrará de color rojo.")

        TlpFramework.ToolTipTitle = "Instalación Framework 3.5"
        TlpFramework.SetToolTip(btFramework, "Se procederá a la instalación del Paquete Framework 3.5 necesario para .Net. Se recomienda Reinciar tras su instalación.")

        TlpTuemail.ToolTipTitle = "Indica tu email para recibir un aviso"
        TlpTuemail.SetToolTip(CBoxEmail, "Se te remitirá un email de confirmación cuando finalicen las descargas seleccionadas.")

        TlpNotinpdf.ToolTipTitle = "Instalador NotinPDF"
        TlpNotinpdf.SetToolTip(BtNotinpdf, "Se descargará y lanzará el ejecutable NotinPDF.")

        TlpRequisitosNotin.ToolTipTitle = "Hoja Requisitos Notin"
        TlpRequisitosNotin.SetToolTip(BtDocRequisitos, "Muestra el documento con los Requisitos para instalar Notin.")


    End Sub
#End Region

#Region "ODBC"
    Private Sub btOdbc_Click(sender As Object, e As EventArgs) Handles btOdbc.Click
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

                btOdbc.BackColor = Color.PaleGreen

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

                btOdbc.BackColor = Color.PaleGreen
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
            btOdbc.BackColor = Color.LightSalmon
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

    Private Sub btFramework_Click(sender As Object, e As EventArgs) Handles btFramework.Click
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
        btFramework.BackColor = Color.PaleGreen
        RegistroInstalacion("Instalado FrameWord 3.5 en el Sistema.")

        ' Shell("cmd.exe /c " & """" & "DISM /Online /Enable-Feature /FeatureName:NetFx3 /All" & """", AppWinStyle.NormalFocus, True)
    End Sub

    Private Sub btDirectivas_Click(sender As Object, e As EventArgs) Handles btDirectivas.Click
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
                btDirectivas.BackColor = Color.PaleGreen
            ElseIf File.Exists("C:\Windows\System32\gpupdate.exe") Then
                RegistroInstalacion("Aplicando Directivas usando gpupdate 32bits.")
                Process.Start("C:\Windows\System32\gpupdate.exe", "/force /boot")
                btDirectivas.BackColor = Color.PaleGreen
            Else
                RegistroInstalacion("ADVERTENCIA: No pude encontrar gpupdate.exe. Fuerzo reinicio a través de shutdown.")
                Process.Start("shutdown", "/r /f /t 0")
                btDirectivas.BackColor = Color.PaleGreen
            End If

        ElseIf reiniciodirectivas = DialogResult.No Then
            RegistroInstalacion("ADVERTENCIA: No se ejecutó el Reinicio tras importar las Directivas.")
            btDirectivas.BackColor = Color.LightSalmon
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "DIRECTIVAS", "1")
        End If


    End Sub


    Private Sub btExcepJava_Click(sender As Object, e As EventArgs) Handles btExcepJava.Click
        My.Computer.Network.DownloadFile(PuestoNotin & "Utiles\ExcepcionesJava.bat", RutaDescargas & "Utiles\ExcepcionesJava.bat", "juanjo", "Palomeras24", False, 20000, True)

        RunAsAdmin(RutaDescargas & "Utiles\ExcepcionesJava.bat")
        cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "EXCEPJAVA", "1")
        btExcepJava.BackColor = Color.PaleGreen
        RegistroInstalacion("Añadidas excepciones JAVA.")
    End Sub

    Private Sub LinkInstalador_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("http://instalador.notin.net/publish.htm")
    End Sub

    Private Sub btJava_Click(sender As Object, e As EventArgs) Handles btJava.Click
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
        btJava.BackColor = Color.PaleGreen
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

        Dim exe As String = "AccesosDirectos.exe AccesosDirectos2003.exe jnemo-latest.exe KMSpico10.exe Office2003.exe Office2016.exe PasarelaSigno.exe PuestoNotinC.exe ScanImg_Beta_FT.exe SFeren-2.8.exe wget.exe unrar.exe"
        Dim rar As String = "ConfWord2016.rar"
        Dim mstmsp As String = "Setup.mst Setup2003.mst setup2016.MSP Setup2016SinWord.MSP"

        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & " " & notinf & " " & exe & " /R:2 /W:5", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & " " & notinf & " " & rar & " /R:2 /W:5", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Registro\" & " " & notinf & "Registro\" & " *.*" & " /R:2 /W:5", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Requisitos\" & " " & notinf & "Requisitos\" & " *.*" & " /R:2 /W:5", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & "Software\" & " " & notinf & "Software\" & " *.*" & " /R:2 /W:5", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & RutaDescargas & "robocopy.exe " & RutaDescargas & " " & notinf & " " & mstmsp & " /R:2 /W:5", AppWinStyle.Hide, True)




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
            MessageBox.Show("Descarga o Trae los Paquetes antes de comenzar las Instalaciones.", "¿Descargaste los paquetes?", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            'lbProcesandoDescargas.Visible = False
            Exit Sub
        End If

        Dim claveinift = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "REGFT", "2")
        If claveinift = 2 Then
            Dim claveregft As DialogResult
            claveregft = MessageBox.Show("Se importarán Claves de Registro y será recomendable REINICIAR antes de proceder con la Instalación. ¿Realizamos la preparación inicial?", "Importar Claves y Reiniciar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If claveregft = DialogResult.Yes Then
                If File.Exists(RutaDescargas & "Registro\FTComoAdministrador.reg") = False Then
                    Directory.CreateDirectory(RutaDescargas & "Registro")
                    My.Computer.Network.DownloadFile(PuestoNotin & "FTComoAdministrador.reg", RutaDescargas & "Registro\FTComoAdministrador.reg", "juanjo", "Palomeras24", False, 60000, True)
                End If
                Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "1")

                PbInstalaciones.Visible = True
                Dim p As Integer
                While p < 6
                    p = p + 1
                    Threading.Thread.Sleep(600)
                    PbInstalaciones.PerformStep()
                End While


                MessageBox.Show("Vamos a REINICIAR. Guarda tu trabajo.", "Reinicio inicial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dim reinicio As String = "shutdown /r /f /t 0"
                File.WriteAllText(RutaDescargas & "primerreinicio.bat", reinicio)
                RegistroInstalacion("Procedemos a Reiniciar el equipo para la preparación inicial.")
                RunAsAdmin(RutaDescargas & "primerreinicio.bat")
                Exit Sub
            ElseIf claveregft = DialogResult.No Then
                MessageBox.Show("Continuamos con el resto de instalaciones.", "Operación cancelada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "REGFT", "0")
                RegistroInstalacion("ADVERTENCIA: No se ejecutó el Reinicio tras importar las Claves de Registro.")
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
            NotinSiNo = MessageBox.Show("Posible instalación existente de NOTIN .NET (Access 2003). ¿Ejecutar instalación Office 2003?", "Instalación Office 2003", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If NotinSiNo = DialogResult.Yes Then
                InstalarNotinNet2003()
            Else
                Office2016sinWord()
            End If
        End If

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
            RegistroInstalacion("ERROR: No se encontró el Paquete OFFICE2016.EXE. Esto es un problema para realizar su instalación.")
        End If

        InstalarNotinNet2003()
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
            RegistroInstalacion("Copiada Referencia Outlook.")

        Catch ex As Exception
            RegistroInstalacion("ERROR Referencia Outlook: " & ex.Message)
        End Try

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
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistroInstalacion("ERROR Requisitos Net: " & ex.Message)
        End Try
        EjecutableNotinNet2003()

    End Sub

    Private Sub EjecutableNotinNet2003()
        If UnidadF() = True Then
            Try
                'Dim ExisteNotinNet As Boolean = File.Exists("C:\Program Files (x86)\Humano Software\Notin\NotinNetDesktop.exe")
                'If ExisteNotinNet = False Then
                File.Copy("F:\NOTAWIN.NET\NotinNetInstaller.exe", RutaDescargas & "NotinNetInstaller.exe", True)
                Dim pnotinnet As New ProcessStartInfo()
                pnotinnet.FileName = RutaDescargas & "NotinNetInstaller.exe"
                Dim notinnet As Process = Process.Start(pnotinnet)
                'notintaskpane.WaitForInputIdle()
                notinnet.WaitForExit()
                RegistroInstalacion("Ejecutado NotinNetInstaller.exe")
                'Shell("cmd.exe /c " & RutaDescargas & "NotinNetInstaller.exe", AppWinStyle.Hide, True)
                'End If
            Catch ex As Exception
                RegistroInstalacion("NotinNetInstaller: " & ex.Message)
            End Try
            'Ademas me traigo las Plantillas y el MDE
            Try
                File.Copy("F:\NOTIN8.mde", "C:\Notawin.Net\notin8.mde", True)
            Catch ex As Exception
                RegistroInstalacion("ERROR: Notin8.mde" & ex.Message)
            End Try
            Try
                File.Copy("F:\NOTIN\PLANTILLAS\NORMAL.DOTM", "C:\PLANTILLAS\NORMAL.DOTM", True)
            Catch ex As Exception
                RegistroInstalacion("ERROR: Normal.dotm " & ex.Message)
                'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        SoftwareAncert2003()
    End Sub

    Private Sub SoftwareAncert2003()
        Dim Ancert As Integer = Nothing
        Ancert = MessageBox.Show("¿Instalar Software Ancert?", "Sferen y Pasarela", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Ancert = 6 Then
            If File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
                Shell("cmd.exe /c " & RutaDescargas & "SFeren-2.8.exe", AppWinStyle.Hide, True)
            Else
                RegistroInstalacion("ERROR: Paquete Sferen no encontrado.")
            End If
            If File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PasarelaSigno.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                Shell("cmd.exe /c " & RutaDescargas & """" & "\Pasarela 2.1\setup.exe" & """", AppWinStyle.Hide, True)
            Else
                RegistroInstalacion("ERROR: Instalable PasarelaSigno no encontrado.")
            End If
        End If

        jNemo2003()
    End Sub

    Private Sub jNemo2003()
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
                btJava.BackColor = Color.PaleGreen
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
                RegistroInstalacion("Instalando jNemo-latest. Verifica que tengas JAVA.")
            End If
            Threading.Thread.Sleep(10000)
        Else
            RegistroInstalacion("ADVERTENCIA: Se encontró el ejecutable jNemo. Se omite su instalación.")
        End If
        ft2003()
    End Sub

    Private Sub ft2003()
        Shell("cmd.exe /c " & "C:\Notawin.Net\FT.exe /actualizaciones", AppWinStyle.Hide, False)
        Threading.Thread.Sleep(10000)
        RegistroInstalacion("Instalados Paquetes desde FT /Actualizaciones")
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
    End Sub
#End Region



    Private Sub BtConfiguraWord2016_Click(sender As Object, e As EventArgs) Handles BtConfiguraWord2016.Click
        If UnidadF() = True Then

            Dim notindesktop As Boolean = File.Exists("C:\Program Files (x86)\Humano Software\Notin\NotinNetDesktop.exe")
            If notindesktop = False Then
                Try
                    File.Copy("F:\Notawin.Net\NotinNetInstaller.exe", RutaDescargas & "NotinNetinstaller.exe")
                    Dim pInfonotinnet As New ProcessStartInfo()
                    pInfonotinnet.FileName = RutaDescargas & "NotinNetinstaller.exe"
                    Dim notinnet As Process = Process.Start(pInfonotinnet)
                    notinnet.WaitForInputIdle()
                    notinnet.WaitForExit()
                Catch ex As Exception
                    MessageBox.Show("No se encontró el componente NotinNet instalado. No se ha podido acceder al ejecutable en F:\Notawin.Net para su instalación.", "Error NotinNetInstaller", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RegistroInstalacion("No se pudo obtener NotinNetInstaller para Configurar Word 2016")
                    cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                End Try
            End If

            Directory.CreateDirectory(RutaDescargas & "Office2016")
            obtenerunrar()
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "ConfWord2016.rar", RutaDescargas & "ConfWord2016.rar", "juanjo", "Palomeras24", False, 20000, True)
            Catch ex As Exception
                MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion("ERROR Configurar Word 2016: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
            End Try

            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "ConfWord2016.rar " & RutaDescargas & "Office2016\", AppWinStyle.Hide, True)
            'Dim ConfigurarWord = MessageBox.Show("¿Configuramos Word 2016?", "Configurar Word 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'If ConfigurarWord = DialogResult.Yes Then
            'Dim configurawordini = cIniArray.IniGet(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "2")

            Try
                Process.Start("C:\Program Files (x86)\Humano Software\Notin\Compatibilidad\ReferNet.exe")
                Threading.Thread.Sleep(5000)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Revisa Instalacion de NotinNET.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                RegistroInstalacion(ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
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
                notinaddin.WaitForInputIdle()
                'Esperar a que el proceso termine.
                notinaddin.WaitForExit()
                'Continuar con el código.
            Catch ex As Exception
                RegistroInstalacion("ERROR NotinAddin: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
            End Try
            Try
                Dim pInfotaskpane As New ProcessStartInfo()
                pInfotaskpane.FileName = "C:\Program Files (x86)\Humano Software\Notin\Addins\NotinTaskPane\NotinTaskPaneInstaller.exe"
                Dim notintaskpane As Process = Process.Start(pInfotaskpane)
                notintaskpane.WaitForInputIdle()
                notintaskpane.WaitForExit()
            Catch ex As Exception
                RegistroInstalacion("ERROR NotinTaskPane: " & ex.Message)
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
                BtConfiguraWord2016.BackColor = Color.LightSalmon
            End Try
            'Shell("cmd.exe /C " & RutaDescargas & "ConfiguraWord2016.exe", AppWinStyle.NormalFocus, True)
            'RunAsAdmin(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
            Process.Start(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
            'Threading.Thread.Sleep(10000)

            'Obtener texto entre caracteres
            Dim expedientes As String = cIniArray.IniGet("F:\WINDOWS\NNotin.ini", "Expedientes", "Ruta", "Servidor")
            expedientes = expedientes.Remove(0, 2)
            Dim unidadi = expedientes.LastIndexOf("\I")
            expedientes = expedientes.Substring(0, unidadi)

            cIniArray.IniWrite(instaladorkuboini, "RUTAS", "EXPEDIENTES", expedientes)

            Directory.CreateDirectory(RutaDescargas & "Registro")
            Dim claveregistroservidor As String = """" & "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Office\16.0\Word\Security\Trusted Locations\Location3" & """" & " /v Path /t REG_SZ /d \\" & expedientes & "\F" & " /f"
            File.WriteAllText(RutaDescargas & "Registro\unidadfword.bat", "reg add ")
            File.AppendAllText(RutaDescargas & "Registro\unidadfword.bat", claveregistroservidor)

            Try
                Process.Start("regedit", "/s " & RutaDescargas & "Office2016\ConfWord2016\w16recopregjj.reg")
                RunAsAdmin(RutaDescargas & "Registro\unidadfword.bat")
                BtConfiguraWord2016.BackColor = Color.PaleGreen
                cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "1")
            Catch ex As Exception
                RegistroInstalacion("Claves Registro Word 2013: " & ex.Message)
                BtConfiguraWord2016.BackColor = Color.LightSalmon
            End Try

        Else
            MessageBox.Show("Unidad F desconectada. No se puede configurar Word 2016.", "Configura WORD 2016", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "CONFIGURAWORD2016", "0")
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
        MessageBox.Show("A continuación se ejecutará el Instalador de Fer. López. Cualquier cosa que veas, no entiendas o no te parezca apropiada tendrás que hablarla con él. Gracias", "Ejecutar NotinPDF", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        obtenerunrar()
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "NotinPdf.rar " & RutaDescargas & "NotinPDF\", AppWinStyle.NormalFocus, True)
        Try
            Process.Start(RutaDescargas & "NotinPDF\notinpdf.exe")
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "NOTINPDF", "1")
            BtNotinpdf.BackColor = Color.PaleGreen
            RegistroInstalacion("Ejecutado instalador NotinPDF.")
        Catch ex As Exception
            RegistroInstalacion("ERROR NotinPDF: " & ex.Message)
            cIniArray.IniWrite(instaladorkuboini, "INSTALACIONES", "NOTINPDF", "2")
            BtNotinpdf.BackColor = Color.LightSalmon
        End Try


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
    Private Sub cbConfiguraNotin_CheckedChanged(sender As Object, e As EventArgs) Handles cbConfiguraNotin.CheckedChanged
        CalcularTamanoDescarga(1, cbConfiguraNotin.Checked)
    End Sub

    Private Sub cbConfiguraWord2016_CheckedChanged(sender As Object, e As EventArgs) Handles cbConfiguraWord2016.CheckedChanged
        CalcularTamanoDescarga(1, cbConfiguraWord2016.Checked)
    End Sub

    Private Sub cbNemo_CheckedChanged(sender As Object, e As EventArgs) Handles cbNemo.CheckedChanged
        CalcularTamanoDescarga(12, cbNemo.Checked)
    End Sub

    Private Sub cbRequisitos_CheckedChanged(sender As Object, e As EventArgs) Handles cbRequisitos.CheckedChanged
        CalcularTamanoDescarga(45.5, cbRequisitos.Checked)
    End Sub

    Private Sub cbPuestoNotin_CheckedChanged(sender As Object, e As EventArgs) Handles cbPuestoNotin.CheckedChanged
        CalcularTamanoDescarga(16.2, cbPuestoNotin.Checked)
    End Sub

    Private Sub cbSferen_CheckedChanged(sender As Object, e As EventArgs) Handles cbSferen.CheckedChanged
        CalcularTamanoDescarga(80.1, cbSferen.Checked)
    End Sub

    Private Sub cbPasarelaSigno_CheckedChanged(sender As Object, e As EventArgs) Handles cbPasarelaSigno.CheckedChanged
        CalcularTamanoDescarga(1, cbPasarelaSigno.Checked)
    End Sub
    Private Sub CbFineReader_CheckedChanged(sender As Object, e As EventArgs) Handles CbFineReader.CheckedChanged
        CalcularTamanoDescarga(387, CbFineReader.Checked)
    End Sub

    Private Sub cbTerceros_CheckedChanged(sender As Object, e As EventArgs) Handles cbTerceros.CheckedChanged
        CalcularTamanoDescarga(94.9, cbTerceros.Checked)
    End Sub



#Region "ENVIO EMAIL"
    Private Function validaremail()
        If CBoxEmail.Text = Nothing Then
            Return False
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(CBoxEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Sub EnvioMail()

        If validaremail() = True Then

            'Dim A As String = Tbtucorreo.Text
            Dim a As String = CBoxEmail.Text
            Dim Destinatario As MailAddress = New MailAddress(a)

            Dim correo As New MailMessage
            Dim smtp As New SmtpClient()

            correo.From = New MailAddress("instalador@notin.net", "Instalador Kubo", System.Text.Encoding.UTF8)
            correo.To.Add(Destinatario)
            correo.SubjectEncoding = System.Text.Encoding.UTF8
            correo.Subject = "Descargas InstaladorKubo Finalizadas"
            correo.Body = "Las descargas finalizaron a las " & DateTime.Now.Hour & " horas " & "y " & DateTime.Now.Minute & " minutos." & vbCrLf & "Puedes proceder a realizar las instalaciones cuando quieras." & vbCrLf & "Cualquier duda tienes disponible el Comunicado 1573: http://tecnicos.notin.net/detalles.asp?id=1573" & vbCrLf & "Muchas gracias"
            correo.BodyEncoding = System.Text.Encoding.UTF8
            'correo.IsBodyHtml = False(formato tipo web o normal:  true = web)
            correo.IsBodyHtml = False
            correo.Priority = MailPriority.High

            smtp.Credentials = New System.Net.NetworkCredential("instalador@notin.net", "insta24")
            smtp.Port = 587
            smtp.Host = "smtp.notin.net"
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
        If validaremail() = True Then
            Dim destinatario As String = CBoxEmail.Text
            cIniArray.IniWrite(instaladorkuboini, "EMAIL", "DESTINATARIO", destinatario)
            RegistroInstalacion("EMAIL: Dirección de correo establecida a " & destinatario)
        Else
            cIniArray.IniWrite(instaladorkuboini, "EMAIL", "DESTINATARIO", "")
        End If


    End Sub

    Private Sub BtDocRequisitos_Click(sender As Object, e As EventArgs) Handles BtDocRequisitos.Click
        Process.Start("iexplore.exe", "https://docs.google.com/document/d/1NPprOtwrgrz6evWbYzYDfsjGrG9jcPAwX0aNjSkx5wQ/edit?usp=sharing")
        RegistroInstalacion("Hoja de Requisitos Notin abierta.")
    End Sub

    Private Sub BtISL_Click(sender As Object, e As EventArgs) Handles BtISL.Click
        'TODO ISL
        ' http://isl.notin.net/start/ISLAlwaysOn?cmdline=grant_silent+%22zeJw9jzFOQzEMQGVVdIKFg1ROHDvxEWAoC2MXx3GqX1W%2fQ%2fuvwZlBHRjfm96bAJ%2bbrZfbHs4%2bd3D8%2bv44vsB5GTsoGM4Da%2fNZZktKgqQ0Mo46sNUYo1HPtUexjoVcWy3J1Ycl9s7zT4haG%2bSzh5lOTETSlDOHNatds0dEkxkUyjVyZlcZSllrJnSLauxSHBPKD9wBlvv1sN4ey3pY4%2fEGG8Dp9M%2bny%2fPkHbZnPXFOIlxJ%2bBX28AtDiT%2fb%22+%2FSILENT+%2FVERYSILENT+password+%22b30330104%22+description+%22carmonas+-+palomo%22



    End Sub
End Class