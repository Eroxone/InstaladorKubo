Imports System.IO
Imports System.Text
Imports InstaladorKubo.LeerFicherosINI


'WEB DE INSTALACIÓN
'http://instalador.notin.net


Public Class InstaladorKubo
    'TODO Establecer contraseña de ejecución.

    ' CONTROLES DESCARGAS

    'Variables
    'Private Const PATH_TEMP As String = RutaAnterior() 'cambiar a C:\NOTIN\ == IMPORTANTE ==
    Private Const FILE_DOWNLOAD As String = "descargas.txt"
    Private Const REQUISITOS_DOWNLOAD As String = "requisitos.txt"
    Private Const TERCEROS_DOWNLOAD As String = "terceros.txt"
    Private Const REGISTRO_DOWNLOAD As String = "registro.txt"
    Private Const PuestoNotin As String = "ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/"
    'Private UnidadF As Boolean = Directory.Exists("F:")
    'Private textoLog As StringBuilder = New StringBuilder()

    'Private nombre_fichero_log As String = "LOG_" & DateTime.Now.Day & "_" & DateTime.Now.Month & "_" & DateTime.Now.Year & ".txt"
    'De nombre de fichero dejo un único LOG de momento. Ya aprenderé el resto
    Private nombre_fichero_log As String = "Logger_InstaladorKubo.txt"
    Private ruta_log As String = "C:\TEMP\InstaladorKubo\" & nombre_fichero_log
    Private RutaDescargas As String = GetPathTemp() 'PATH_TEMP


    Private Sub frmInstaladorNotin_Load(sender As Object, e As EventArgs) Handles Me.Load

        Directory.CreateDirectory("C:\TEMP\InstaladorKubo")

        SistemaOperativo()
        lbRuta.Text = GetPathTemp()
        YaDescargados()
        Tooltips()
        FicheroINI()

        'TODO Terminar proceso de Logger. Ahora mismo lo hace al revés
        Dim cabecera_log As String = "=====  INICIO APLICACIÓN  =====" & vbCrLf & My.Computer.Info.OSFullName & vbCrLf & My.User.Name
        Logger(cabecera_log)

    End Sub



    'Private Sub ComprobarVersion()
    '    'Antes crear ruta si no existe y descargar el version ini de internet. controlar posible error de conexion.
    '    Dim versioninternet = cIniArray.IniGet("C:\TEMP\InstaladorKubo\versioninternet", "CONTROL", "Version")

    'End Sub

    ' Funcion para logear el sistema
#Region "LOG"

    'Using writer As New StreamWriter(FullName, True)
    'writer.writeline("linea")
    'End Using
    Private Sub Logger(ByVal textolog As String)
        'Directory.CreateDirectory("C:\TEMP\InstaladorKubo")
        File.AppendAllText(ruta_log, DateTime.Now.Hour & ":" & DateTime.Now.Minute & "  " & textolog & vbCrLf)
    End Sub
#End Region

    Private Sub SistemaOperativo()
        Dim SistemaO = (My.Computer.Info.OSFullName)
        lbSistemaO.Text = SistemaO
        Dim UsuarioAtual = (My.User.Name)
        lbUsuario.Text = UsuarioAtual
        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
            ' lbUnidadF.BackColor = Color.Green
            Logger("Iniciando con Unidad F CONECTADA")
        Else
            lbUnidadF.Text = "DESCONECTADA"
            lbUnidadF.ForeColor = Color.Red
            '  lbUnidadF.BackColor = Color.Red
            Logger("Iniciando con Unidad F DESCONECTADA")
        End If

        If Directory.Exists("C:\WINDOWS\SYSWOW64") Then
            lb64bits.Text = ("Sistema Operativo de 64bits")
        Else
            lb64bits.Text = ("Sistema Operativo de 32bits")
        End If

    End Sub

    'RUTA ANTERIOR. SI EXISTÍA
    Private Function GetPathTemp() As String
        'cIniArray.IniWrite("D:\NOTIN\NNOTIN.INI", "NET", "NOMBRESERVIDOR", "holaquetal")
        Dim rutadescargasini = cIniArray.IniGet("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "RUTAS", "RUTADESCARGAS", Nothing)
        If rutadescargasini IsNot Nothing Then
            Return rutadescargasini
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "RUTAS", "RUTADESCARGAS", rutadescargasini)
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
        Dim odbc = cIniArray.IniGet("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "ODBC", "NOTINSQL", "2")
        If odbc = 1 Then
            btOdbc.BackColor = Color.PaleGreen
        ElseIf odbc = 0 Then
            btOdbc.BackColor = Color.LightSalmon
        End If

        Dim framework = cIniArray.IniGet("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "REQUISITOS", "FRAMEWORK35", "2")
        If framework = 1 Then
            btFramework.BackColor = Color.PaleGreen
        Else
            btFramework.BackColor = SystemColors.Control
        End If

        Dim excepjava = cIniArray.IniGet("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "INSTALACIONES", "EXCEPJAVA", "2")
        If excepjava = 1 Then
            btExcepJava.BackColor = Color.PaleGreen
        Else
            btExcepJava.BackColor = SystemColors.Control
        End If

    End Sub


    'ACCEDER A URL NOTARIADO
    Private Sub lblAncert_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAncert.LinkClicked
        System.Diagnostics.Process.Start("http://soporte.notariado.org")

    End Sub



    'Boton Examinar
    Private Sub btDirDescargas_Click(sender As Object, e As EventArgs) Handles btDirDescargas.Click
        fbdDescarga.ShowDialog()

        ' Si hago clic en Cancelar me quedo con la ruta anterior
        If String.IsNullOrEmpty(fbdDescarga.SelectedPath) OrElse fbdDescarga.SelectedPath = "\" Then
            fbdDescarga.SelectedPath = GetPathTemp()
            fbdDescarga.SelectedPath = GetPathTemp.Remove(GetPathTemp.Length - 1)
        End If

        'RutaDescargas = fbdDescarga.SelectedPath & "\"
        'lbRuta.Text = RutaDescargas
        'cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "RUTAS", "RUTADESCARGAS", RutaDescargas)
        'File.WriteAllText("C:\TEMP\InstaladorKubo\RutaAnterior.txt", RutaDescargas)
        'Compruebo si el Directorio contiene espacios y pido que lo cambies

        'Busco si la ruta es raiz o contiene espacios
        If fbdDescarga.SelectedPath.Contains(" ") OrElse fbdDescarga.SelectedPath.Contains("F:") OrElse
            System.Text.RegularExpressions.Regex.IsMatch(fbdDescarga.SelectedPath, "[A-Z](:\\)$") Then
            MessageBox.Show(fbdDescarga.SelectedPath & " no es una ruta válida. No se admiten espacios ni unidades de red o raíz.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Dim RutaDescargas = "C:\NOTIN\"
            'cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "RUTAS", "RUTADESCARGAS", RutaDescargas)
            'lbRuta.Text = RutaDescargas
        Else
            RutaDescargas = fbdDescarga.SelectedPath & "\"
        End If
        cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "RUTAS", "RUTADESCARGAS", RutaDescargas)
        lbRuta.Text = RutaDescargas
        YaDescargados()


        'lbRuta.Text = RutaDescargas
        'If btDirDescargas.DialogResult = Windows.Forms.DialogResult.Cancel Then
        '    RutaDescargas = GetPathTemp()
        'End If
    End Sub



    'Recarga formulario tras salir de la eleccion de ruta
    Private Sub lbRuta_TextChanged(sender As Object, e As EventArgs) Handles lbRuta.TextChanged
        YaDescargados()
    End Sub

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
            cbOffice2003.BackColor = SystemColors.Control
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
            cbConfiguraNotin.BackColor = SystemColors.Control
            '     cbConfiguraNotin.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Office2016.exe") Then
            Dim Archivo2016 As New FileInfo(RutaDescargas & "Office2016.exe")
            Dim Length2016 As Long = Archivo2016.Length
            If Archivo2016.Length = "739967123" Then
                cbOffice2016.ForeColor = Color.DarkGreen
                '         cbOffice2016.Enabled = False
            ElseIf Archivo2016.Length < "739967123" Then
                cbOffice2016.ForeColor = Color.Red
            End If
        Else
            cbOffice2016.BackColor = SystemColors.Control
            '     cbOffice2016.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Office2016odt.exe") Then
            Dim Archivo2016odt As New FileInfo(RutaDescargas & "Office2016odt.exe")
            Dim Length2016odt As Long = Archivo2016odt.Length
            If Archivo2016odt.Length = "2452664263" Then
                cbOffice2016odt.ForeColor = Color.DarkGreen
                '        cbOffice2016odt.Enabled = False
            ElseIf Archivo2016odt.Length < "2452664263" Then
                cbOffice2016odt.ForeColor = Color.Red
            End If
        Else
            cbOffice2016odt.BackColor = SystemColors.Control
            '      cbOffice2016odt.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "ConfWord2016.rar") Then
            Dim Config2016 As New FileInfo(RutaDescargas & "ConfWord2016.rar")
            Dim LengthConfig2016 As Long = Config2016.Length
            If Config2016.Length = "8320" Then
                cbConfiguraWord2016.ForeColor = Color.DarkGreen
                '        cbConfiguraWord2016.Enabled = False
            End If
        Else
            cbConfiguraWord2016.BackColor = SystemColors.Control
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
            cbNemo.BackColor = SystemColors.Control
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
            cbPuestoNotin.BackColor = SystemColors.Control
            '    cbPuestoNotin.Enabled = True
        End If

        'REQUISITOS. Cuento los archivos en el directorio
        If System.IO.Directory.Exists(RutaDescargas & "\Requisitos") Then
            Dim ArchivosenRequisitos = My.Computer.FileSystem.GetFiles(RutaDescargas & "\Requisitos")

            If ArchivosenRequisitos.Count >= 4 Then
                cbRequisitos.ForeColor = Color.DarkGreen
                '                cbRequisitos.Enabled = False
            Else
                cbRequisitos.BackColor = SystemColors.Control
            End If
        Else
            cbRequisitos.BackColor = SystemColors.Control
            '        cbRequisitos.Enabled = True
        End If

        'ANCERT Como puede variar el tamaño solo miro que exista el fichero
        If System.IO.File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
            cbSferen.ForeColor = Color.DarkGreen
            '            cbSferen.Enabled = False
        Else
            cbSferen.BackColor = SystemColors.Control
            '          cbSferen.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
            cbPasarelaSigno.ForeColor = Color.DarkGreen
            '       cbPasarelaSigno.Enabled = False
        Else
            cbPasarelaSigno.BackColor = SystemColors.Control
            '     cbPasarelaSigno.Enabled = True
        End If

        'SOFTWARE TERCEROS
        If System.IO.Directory.Exists(RutaDescargas & "\Software") Then
            cbTerceros.ForeColor = Color.DarkGreen
        Else
            cbTerceros.BackColor = SystemColors.Control
        End If


    End Sub
#End Region

    'TODO Funcion calcular tamaño descarga chequeada


    'COMENZAR DESCARGAS
    Private Sub btDescargar_Click(sender As Object, e As EventArgs) Handles btDescargar.Click

        'Si no chequeas nada salimos
        If (cbConfiguraNotin.Checked OrElse cbConfiguraWord2016.Checked OrElse cbNemo.Checked OrElse cbOffice2003.Checked OrElse cbOffice2016.Checked OrElse cbOffice2016odt.Checked OrElse cbPasarelaSigno.Checked OrElse cbPuestoNotin.Checked OrElse cbRequisitos.Checked OrElse cbSferen.Checked OrElse cbTerceros.Checked) = False Then
            MessageBox.Show("NINGUNA DESCARGA SELECCIONADA.", "Gestión Descargas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'Escribir en el INI para que conste que ya se ha efectuado alguna descarga
        cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "COMIENZO", "1")

        'TODO Mas adelante me meto con la barra de progreso en otro hilo

        'Defino la cadenas vacias del fichero para las descargas y que esten limpios de principio
        Dim texto As String = ""
        Dim requisitos As String = ""
        Dim terceros As String = ""
        Dim registro As String = ""

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
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "OFFICE2003", "1")
        End If

        If cbOffice2016.Checked Then
            texto = texto & PuestoNotin & "KMSpico10.exe" & vbCrLf
            texto = texto & PuestoNotin & "Office2016.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "OFFICE2016", "1")
        End If

        If cbOffice2016odt.Checked Then
            texto = texto & PuestoNotin & "KMSpico10.exe" & vbCrLf
            texto = texto & PuestoNotin & "Office2016odt.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "OFFICE2016ODT", "1")
        End If

        If cbNemo.Checked Then
            texto = texto & "http://nemo.notin.net/jnemo-latest.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "NEMO", "1")
        End If

        If cbPuestoNotin.Checked Then
            texto = texto & PuestoNotin & "PuestoNotinC.exe" & vbCrLf
            texto = texto & PuestoNotin & "AccesosDirectos.exe" & vbCrLf
            texto = texto & PuestoNotin & "AccesosDirectos_odt.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "PUESTONOTIN", "1")
        End If

        If cbRequisitos.Checked Then
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "KryptonSuite300.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "Office2003PrimaryInterop.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2005.exe" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2015.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "REQUISITOS", "1")
            '   requisitos = requisitos & PuestoNotin & "Requisitos/" & "Framework35.bat" & vbCrLf
        End If

        If cbSferen.Checked Then
            texto = texto & PuestoNotin & "SFeren-2.8.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "SFEREN", "1")
        End If

        If cbPasarelaSigno.Checked Then
            texto = texto & PuestoNotin & "PasarelaSigno.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "PASARELASIGNO", "1")
        End If

        If cbTerceros.Checked Then
            terceros = terceros & PuestoNotin & "Software/" & "AcrobatReaderDC.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "FileZilla_3_win64-setup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "ChromeSetup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "Notepad_x64.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "WinRAR5.exe" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "SOFTWARETERCEROS", "1")
        End If

        'Descagar configuradores del autochequeo
        If cbConfiguraNotin.Checked Then
            registro = registro & PuestoNotin & "ConfigAccess.reg" & vbCrLf
            registro = registro & PuestoNotin & "FTComoAdministrador.reg" & vbCrLf
            registro = registro & PuestoNotin & "VentanasSigno.reg" & vbCrLf
            registro = registro & PuestoNotin & "MSOUTL.OLB" & vbCrLf
            registro = registro & PuestoNotin & "ExclusionDefender.reg" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "CLAVESREGISTRO", "1")
        End If

        If cbConfiguraWord2016.Checked Then
            'texto = texto & PuestoNotin & "ConfiguraWord2016.exe" & vbCrLf
            texto = texto & PuestoNotin & "ConfWord2016.rar" & vbCrLf
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "CONFIGURAWORD", "1")
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

        lbProcesandoDescargas.Visible = False
        MessageBox.Show("DESCARGAS FINALIZADAS.", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)


    End Sub

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
            btTodo.Text = "Marcar todos"
            MarcarTodos = 0
        End If
    End Sub
#End Region


    'Autochequear Configuradores Notin y Word 2016 <> Office 2016odt
    Private Sub cbOffice2003_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2003.CheckedChanged
        cbConfiguraNotin.CheckState = cbOffice2003.CheckState
    End Sub

    Private Sub cbOffice2016_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2016.CheckedChanged
        cbConfiguraWord2016.CheckState = cbOffice2016.CheckState
        cbOffice2016odt.Checked = False
    End Sub

    Private Sub cbOffice2016odt_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2016odt.CheckedChanged
        cbConfiguraWord2016.CheckState = cbOffice2016odt.CheckState
        cbOffice2016.Checked = False
    End Sub


    Private Sub btSalir_Click(sender As Object, e As EventArgs) Handles btSalir.Click

        'Limpieza de ficheros temporales para instalaciones por ejemplo
        Try
            File.Delete(RutaDescargas & "Requisitos\Framework35.bat")
            File.Delete(RutaDescargas & "odbc32.bat")
            File.Delete(RutaDescargas & "Registro\msoutl.bat")
        Catch ex As Exception
        End Try
        Me.Close()
    End Sub


    'CONTROLES DE INSTALACION

    Private Sub btNotinKubo_Click(sender As Object, e As EventArgs) Handles btNotinKubo.Click

        'Comprobar si se ha efectuado alguna descarga
        Dim comienzo = cIniArray.IniGet("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "DESCARGAS", "COMIENZO", "2")
        If comienzo = 2 Then
            MessageBox.Show("Descarga los Paquetes antes de comenzar las Instalaciones.", "¿Descargaste los paquetes?", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        'Mientras unidad F no valida y usuario pulse reintentar

        Dim QueHacerF As DialogResult
        While UnidadF() = False
            QueHacerF = MessageBox.Show("Unidad F no conectada. Habrá procesos que no se podrán completar y se omitirán.", "Advertencia Unidad F", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)
            If QueHacerF = DialogResult.Abort Then
                Exit Sub
            ElseIf QueHacerF = DialogResult.Ignore Then
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
            Catch ex As Exception
                MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "juanjo", "Palomeras24", False, 20000, True)
                If REINTENTAR = DialogResult.Retry Then
                End If
            End Try
        End If

        'Verifico si has descargado Office 2003 usando el INI. Haré lo mismo con el resto de descargas

        Dim NotinSiNo As Integer = Nothing

        Dim EjecutableAccess As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE11\MSACCESS.EXE")

        If EjecutableAccess = False Then
            InstalarNotinNet()

        Else
            NotinSiNo = MessageBox.Show("Posible instalación existente de NOTIN .NET (Access 2003). ¿Ejecutar instalación Office 2003?", "Instalación Office 2003", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If NotinSiNo = DialogResult.Yes Then
                InstalarNotinNet()
            Else
                InstalarRequisitosNet()
            End If
        End If

    End Sub

#Region "INSTALACIONES SOFTWARE"
    Private Sub InstalarNotinNet()
        'Claves Registro
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ConfigAccess.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\VentanasSigno.reg", AppWinStyle.Hide, True)
        Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ExclusionDefender.reg", AppWinStyle.Hide, True)


        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PuestoNotinC.exe " & "C:\", AppWinStyle.NormalFocus, True)

        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2003.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
        'Setup MST que personaliza la instalación de Office 2003
        File.Copy(RutaDescargas & "Setup.mst", RutaDescargas & "Office2003\Setup.mst", True)
        '  Shell("C:\WINDOWS\system32\notepad.exe " & RutaDescargas & "Office2003\NSERIE.TXT", AppWinStyle.NormalFocus, False)


        Shell("cmd.exe /C " & RutaDescargas & "Office2003\setup.exe TRANSFORMS=" & RutaDescargas & "Office2003\Setup.mst /qb-", AppWinStyle.NormalFocus, True)
        ' Shell("cmd.exe /C taskkill /f /im notepad.exe", AppWinStyle.Hide, False)

        Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\Office2003SP3-KB923618-FullFile-ESN.exe" & """", AppWinStyle.NormalFocus, True)
        Shell("cmd.exe /c " & """" & RutaDescargas & "Office2003\SP3 y Parche Access\MSACCESS.msp /passive" & """", AppWinStyle.Hide, True)

        Shell("cmd.exe /c " & "C:\Notawin.Net\FT.exe /actualizaciones", AppWinStyle.Hide, False)

        'Copiar Referencia Outlook
        Dim msoutlxcopy As String = "xcopy /F /Y /C "
        Dim msoutlorigen As String = RutaDescargas & "Registro\MSOUTL.OLB "
        Dim msoutldestino As String = " ""C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\"" "
        File.WriteAllText(RutaDescargas & "Registro\msoutl.bat", msoutlxcopy & msoutlorigen & msoutldestino)

        RunAsAdmin(RutaDescargas & "Registro\msoutl.bat")
        'Try
        '    File.Copy(RutaDescargas & "Registro\MSOUTL.OLB", "C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\MSOUTL.OLB", True)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

        InstalarRequisitosNet()
    End Sub

    Private Sub InstalarRequisitosNet()

        Dim RequisitosSiNo As Integer = Nothing

        RequisitosSiNo = MessageBox.Show("¿Instalar Requisitos .NET?", "Pre-Requisitos", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If RequisitosSiNo = 6 Then
            'Shell("cmd.exe /c " & """" & "DISM /Online /Enable-Feature /FeatureName:NetFx3 /All" & """", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\KryptonSuite300.msi /passive", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\Office2003PrimaryInterop.msi /passive", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2005.exe /q", AppWinStyle.Hide, True)
            Threading.Thread.Sleep(500)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2015.exe /q", AppWinStyle.Hide, True)
            Threading.Thread.Sleep(500)
        End If

        InstalarWord2016()
    End Sub

    Private Sub InstalarWord2016()
        'TODO Esto se podra simplificar con variables que recojan la existencia del ejecutable. Lo hare
        Dim EjecutableWord As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE16\WINWORD.EXE") OrElse File.Exists("C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE")

        If EjecutableWord = False Then
            If File.Exists(RutaDescargas & "Office2016.exe") Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
            ElseIf File.Exists(RutaDescargas & "Office2016odt.exe") Then
                Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016odt.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                Shell("cmd.exe /C " & RutaDescargas & "Office2016ODT\SETUP.EXE " & "/configure " & RutaDescargas & "Office2016ODT\Configuracion.xml", AppWinStyle.Hide, True)
            End If
        ElseIf EjecutableWord = True Then
            Dim WordSiNo = MessageBox.Show("Se ha detectado una posible instalación de WORD 2016. ¿Ejecutar instalación Office 2016?", "Instalación Office 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If WordSiNo = 6 Then
                If File.Exists(RutaDescargas & "Office2016.exe") Then
                    Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                    Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
                Else
                    If File.Exists(RutaDescargas & "Office2016odt.exe") Then
                        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016odt.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
                        Shell("cmd.exe /C " & RutaDescargas & "Office2016ODT\SETUP.EXE " & "/configure " & RutaDescargas & "Office2016ODT\Configuracion.xml", AppWinStyle.Hide, True)
                    Else
                        MessageBox.Show("¿Seguro que has descargado una versión válida de Office 2016?")
                    End If
                End If
            End If
        End If
        EjecutableNotinNet()
    End Sub

    Private Sub EjecutableNotinNet()
        If UnidadF() = True Then
            Try
                Dim ExisteNotinNet As Boolean = File.Exists("C:\Program Files (x86)\Humano Software\Notin\NotinNetDesktop.exe")
                If ExisteNotinNet = False Then
                    File.Copy("F:\NOTAWIN.NET\NotinNetInstaller.exe", RutaDescargas & "NotinNetInstaller.exe", True)
                    Shell("cmd.exe /c " & RutaDescargas & "NotinNetInstaller.exe", AppWinStyle.NormalFocus, True)
                End If
            Catch ex As Exception
            End Try
            'Ademas me traigo las Plantillas y el MDE
            Try
                File.Copy("F:\NOTIN8.mde", "C:\Notawin.Net\notin8.mde", True)
            Catch ex As Exception
            End Try
            Try
                File.Copy("F:\NOTIN\PLANTILLAS\NORMAL.DOTM", "C:\PLANTILLAS\NORMAL.DOTM", True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
        ConfigurarWord2016()
    End Sub

    Private Sub ConfigurarWord2016()
        If UnidadF() = True Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "ConfWord2016.rar " & RutaDescargas & "Office2016\", AppWinStyle.Hide, True)
            Dim ConfigurarWord = MessageBox.Show("¿Configuramos Word 2016?", "Configurar Word 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ConfigurarWord = DialogResult.Yes Then
                'Shell("cmd.exe /C " & RutaDescargas & "ConfiguraWord2016.exe", AppWinStyle.NormalFocus, True)
                RunAsAdmin(RutaDescargas & "Office2016\ConfWord2016\ConfiguraWord2016.bat")
                'TODO mejorar esto y obtener el proceso
                Threading.Thread.Sleep(10000)

                'Obtener texto entre caracteres
                Dim expedientes As String = cIniArray.IniGet("F:\WINDOWS\NNotin.ini", "Expedientes", "Ruta", "Servidor")
                expedientes = expedientes.Remove(0, 2)
                Dim unidadi = expedientes.LastIndexOf("\I")
                expedientes = expedientes.Substring(0, unidadi)

                Directory.CreateDirectory(RutaDescargas & "Registro")
                Dim claveregistroservidor As String = """" & "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Office\16.0\Word\Security\Trusted Locations\Location3" & """" & " /v Path /t REG_SZ /d \\" & expedientes & "\F" & " /f"
                File.WriteAllText(RutaDescargas & "Registro\unidadfword.bat", "reg add ")
                File.AppendAllText(RutaDescargas & "Registro\unidadfword.bat", claveregistroservidor)

                RunAsAdmin(RutaDescargas & "Registro\unidadfword.bat")
            End If
        Else
            MessageBox.Show("Unidad F desconectada. No se puede configurar Word 2016.", "Configura WORD 2016", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        KMSPico()
    End Sub


    Private Sub KMSPico()
        'TODO Crear clave Registro para Excluir rutas de descarga e instalacion

        Dim KMSPicoSInO As Integer = Nothing
        KMSPicoSInO = MessageBox.Show("¿Ejecutar Activador Office 2016?", "KMSPico 10.2.0", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If KMSPicoSInO = 6 Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "KMSpico10.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /C " & RutaDescargas & "KMSpico10\" & "KMSpico_setup.exe", AppWinStyle.NormalFocus, True)
            MessageBox.Show("Añade Exclusión de AntiVirus hacia KMSPico antes de ejecutarlo", "Ejecucion de KMSPico", MessageBoxButtons.OK)
        End If
        SoftwareAncert()
    End Sub


    Private Sub SoftwareAncert()
        Dim Ancert As Integer = Nothing
        Ancert = MessageBox.Show("¿Instalar Software Ancert?", "Sferen y Pasarela", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Ancert = 6 Then
            Shell("cmd.exe /c " & RutaDescargas & "SFeren-2.8.exe", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PasarelaSigno.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & """" & "\Pasarela 2.1\setup.exe" & """", AppWinStyle.NormalFocus, True)
        End If
        jNemo()
    End Sub

    Private Sub jNemo()
        If File.Exists(RutaDescargas & "jnemo-latest.exe") Then
            If File.Exists("c:\Program Files (x86)\jNemo\jNemo.exe") = False Then
                Dim instalajnemo As New Process
                instalajnemo.StartInfo.FileName = RutaDescargas & "jnemo-latest.exe"
                'MiProceso.StartInfo.Arguments = "1664"
                instalajnemo.Start() 'iniciar el proceso
                'MiProceso.Kill()
                'MiProceso.Dispose()
                cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "INSTALACIONES", "JNEMO", "1")
            End If
        Else
        End If
        AccesosDirectosEscritorio()
    End Sub

    Private Sub AccesosDirectosEscritorio()
        Dim Escritorio As String = """" & My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & """"
        If File.Exists(RutaDescargas & "Office2016.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe e -y " & RutaDescargas & "AccesosDirectos.exe " & Escritorio, AppWinStyle.Hide, True)
        ElseIf File.Exists(RutaDescargas & "Office2016odt.exe") Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe e -y " & RutaDescargas & "AccesosDirectos_odt.exe " & Escritorio, AppWinStyle.Hide, True)
        Else
        End If

        lbInstalando.Visible = False
        MessageBox.Show("INSTALACIONES TERMINADAS", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'btNotinKubo.ForeColor = Color.YellowGreen
        'TODO guardar en el ini para que conste que ya se realizo, cambiar color y poner fecha


    End Sub
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

        tlpOffice2016.ToolTipTitle = "Paquete Office 2016"
        tlpOffice2016.SetToolTip(cbOffice2016, "Descarga el paquete Office con instalación personalizable")

        tlpTerceros.ToolTipIcon = ToolTipIcon.Info
        tlpTerceros.ToolTipTitle = "Softare de terceros"
        tlpTerceros.SetToolTip(cbTerceros, "Adobe Reader DC, FileZilla, Google Chrome, Notepad++, etc")
        tlpTerceros.IsBalloon = True

        tlpNotinKubo.ToolTipTitle = "Comienza Instalaciones"
        tlpNotinKubo.SetToolTip(btNotinKubo, "Preguntará por cada Software descargado. No obliga a instalar el paquete completo.")

        tlpAncert.ToolTipTitle = "URL Notariado"
        tlpAncert.SetToolTip(lblAncert, "Acceder a url soporte.notariado.org")

        tlpOffice2003.ToolTipTitle = "Office 2003 DESATENDIDO"
        tlpOffice2003.SetToolTip(cbOffice2003, "Instalación automatizada ACCESS y librerías Outlook.")

        TlpRutaDescargas.ToolTipTitle = "Cambiar Carpeta Descargas"
        TlpRutaDescargas.SetToolTip(btDirDescargas, "Permite seleccionar una carpeta diferente para realizar las descargas.")

        TlpComenzarDescargas.ToolTipTitle = "Comenzar proceso Descargas"
        TlpComenzarDescargas.SetToolTip(btDescargar, "Se descargarán/resumirán los paquetes seleccionados.")

    End Sub
#End Region

#Region "ODBC"
    Private Sub btOdbc_Click(sender As Object, e As EventArgs) Handles btOdbc.Click
        If UnidadF() = True Then
            'Uso la funcion SHARED de la Clase LeerFicherosINI
            Dim nombre_servidor As String = cIniArray.IniGet("F:\WINDOWS\NNOTIN.INI", "NET", "NOMBRESERVIDOR", "SERVIDOR")

            If File.Exists("C:\Windows\SysWoW64\odbcconf.exe") Then
                Dim odbc64 As String = "C:\Windows\SysWoW64\odbcconf.exe " & "/A " & "{CONFIGSYSDSN " & """" & "SQL Server" & """" & " " & """" & "DSN=NOTINSQL|Server=" & nombre_servidor & """" & "}"
                File.WriteAllText(RutaDescargas & "odbc32.bat", odbc64 & vbCrLf)

                RunAsAdmin(RutaDescargas & "odbc32.bat")

                btOdbc.BackColor = Color.PaleGreen
                MessageBox.Show("NotinSQL configurado hacia: " & nombre_servidor & ". Revisa ODBC creado.", "ODBC NotinSQL", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Process.Start("C:\Windows\SysWoW64\odbcad32.exe")
                cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "ODBC", "NOTINSQL", "1")
                cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "ODBC", "SQLSERVER", nombre_servidor)
                'C:\Windows\SysWoW64\odbcconf.exe /A {CONFIGSYSDSN "SQL Server" "DSN=NOTINSQL|Server=clustersql"}
            ElseIf File.Exists("C:\Windows\System32\odbcconf.exe") Then
                Dim odbc32 As String = "C:\Windows\System32\odbcconf.exe " & "/A " & "{CONFIGSYSDSN " & """" & "SQL Server" & """" & " " & """" & "DSN=NOTINSQL|Server=" & nombre_servidor & """" & "}"
                File.WriteAllText(RutaDescargas & "odbc32.bat", odbc32 & vbCrLf)

                RunAsAdmin(RutaDescargas & "odbc32.bat")

                btOdbc.BackColor = Color.PaleGreen
                MessageBox.Show("NotinSQL configurado hacia: " & nombre_servidor & ". Revisa ODBC creado.", "ODBC NotinSQL", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Process.Start("C:\Windows\System32\odbcad32.exe")


                cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "ODBC", "NOTINSQL", "1")
                cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "ODBC", "SQLSERVER", nombre_servidor)
            Else
                MessageBox.Show("No se puede acceder a la utilidad ODBCConf.", "Ejecutable no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No se puede conectar con el Servidor (F:)", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "ODBC", "NOTINSQL", "0")
            btOdbc.BackColor = Color.LightSalmon

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

        cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "REQUISITOS", "FRAMEWORK35", "1")
        btFramework.BackColor = Color.PaleGreen


        ' Shell("cmd.exe /c " & """" & "DISM /Online /Enable-Feature /FeatureName:NetFx3 /All" & """", AppWinStyle.NormalFocus, True)
    End Sub
    'TODO Arreglar esta funcion. Debe llamarse desde cmd
    Private Sub btExcepJava_Click(sender As Object, e As EventArgs) Handles btExcepJava.Click
        My.Computer.Network.DownloadFile(PuestoNotin & "Utiles\ExcepcionesJava.bat", RutaDescargas & "Utiles\ExcepcionesJava.bat", "juanjo", "Palomeras24", False, 20000, True)

        RunAsAdmin(RutaDescargas & "Utiles\ExcepcionesJava.bat")
        cIniArray.IniWrite("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "INSTALACIONES", "EXCEPJAVA", "1")
        btExcepJava.BackColor = Color.PaleGreen
    End Sub

    Private Sub LinkInstalador_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkInstalador.LinkClicked
        System.Diagnostics.Process.Start("http://instalador.notin.net/publish.htm")
    End Sub

End Class
