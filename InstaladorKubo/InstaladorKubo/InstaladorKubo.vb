﻿Imports System.IO

Public Class InstaladorKubo


    ' CONTROLES DESCARGAS

    'Variables
    'Private Const PATH_TEMP As String = RutaAnterior() 'cambiar a C:\NOTIN\ == IMPORTANTE ==
    Private Const FILE_DOWNLOAD As String = "descargas.txt"
    Private Const REQUISITOS_DOWNLOAD As String = "requisitos.txt"
    Private Const TERCEROS_DOWNLOAD As String = "terceros.txt"
    Private Const REGISTRO_DOWNLOAD As String = "registro.txt"
    Private Const PuestoNotin As String = "ftp://ftp.pandora.notin.net/Juanjo/PuestoNotin/"
    'Private UnidadF As Boolean = Directory.Exists("F:")

    Private RutaDescargas As String = GetPathTemp() 'PATH_TEMP

    Private Sub frmInstaladorNotin_Load(sender As Object, e As EventArgs) Handles Me.Load
        SistemaOperativo()
        lbRuta.Text = GetPathTemp()
        YaDescargados()
    End Sub

    Private Sub SistemaOperativo()
        Dim SistemaO = (My.Computer.Info.OSFullName)
        lbSistemaO.Text = SistemaO
        Dim UsuarioAtual = (My.User.Name)
        lbUsuario.Text = UsuarioAtual

        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
            ' lbUnidadF.BackColor = Color.Green
        Else
            lbUnidadF.Text = "DESCONECTADA"
            lbUnidadF.ForeColor = Color.Red
            '  lbUnidadF.BackColor = Color.Red
        End If

    End Sub
    'TODO mostrar texto si unidad F o no

    'TODO Buscar nueva version comparando un txt como string por ejemplo

    'RUTA ANTERIOR. SI EXISTÍA
    Private Function GetPathTemp() As String

        If System.IO.File.Exists("C:\TEMP\RutaAnterior.txt") Then
            Return (System.IO.File.ReadAllText("C:\TEMP\RutaAnterior.txt"))
        End If
        Return "D:\NOTIN\"
    End Function

    'COMPROBAR Unidad F
    Private Function UnidadF() As Boolean
        If Directory.Exists("F:") Then
            Return True
        End If
        Return False
        'Dim CuentaReintentos As Integer = CuentaReintentos + 1
    End Function

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
        End If

        RutaDescargas = fbdDescarga.SelectedPath & "\"
        lbRuta.Text = RutaDescargas
        Directory.CreateDirectory("C:\TEMP")
        File.WriteAllText("C:\TEMP\RutaAnterior.txt", RutaDescargas)
        'Compruebo si el Directorio contiene espacios y pido que lo cambies

        'Busco si la ruta es raiz
        If fbdDescarga.SelectedPath.Contains(" ") OrElse fbdDescarga.SelectedPath.Contains("F:") OrElse
            System.Text.RegularExpressions.Regex.IsMatch(fbdDescarga.SelectedPath, "[A-Z](:\\)$") Then
            MessageBox.Show(fbdDescarga.SelectedPath & " no es una ruta válida. No se admiten espacios ni unidades raíz", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Dejo la ruta defecto
            Dim RutaDescargas = "D:\NOTIN\"  'TODO Cambiar a C:
            lbRuta.Text = RutaDescargas
            YaDescargados() 'TODO Si elijo una ruta mala al volver a la buena no recarga los existentes
            File.WriteAllText("C:\TEMP\RutaAnterior.txt", RutaDescargas)
        End If
        'If btDirDescargas.DialogResult = Windows.Forms.DialogResult.Cancel Then
        '    RutaDescargas = GetPathTemp()
        'End If
    End Sub


    'Recarga formulario tras salir de la eleccion de ruta
    Private Sub lbRuta_TextChanged(sender As Object, e As EventArgs) Handles lbRuta.TextChanged
        YaDescargados()
    End Sub

    ' Archivo existe y mostrar tamaño del mismo
    Private Sub YaDescargados()
        'FICHEROS NOTIN Y WORD
        If System.IO.File.Exists(RutaDescargas & "Office2003.exe") Then
            'Obtener tamaño del archivo
            Dim Archivo2003 As New FileInfo(RutaDescargas & "Office2003.exe")
            Dim Length2003 As Long = Archivo2003.Length
            If Archivo2003.Length = "517573930" Then
                cbOffice2003.BackColor = Color.LawnGreen
                cbOffice2003.Enabled = False
            ElseIf Archivo2003.Length < "517573930" Then
                cbOffice2003.BackColor = Color.LightSalmon
            End If
        Else
            cbOffice2003.BackColor = SystemColors.Control
            cbOffice2003.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Registro\ConfigAccess.reg") Then
            Dim ConfigNotin As New FileInfo(RutaDescargas & "Registro\ConfigAccess.reg")
            Dim LengthNotin As Long = ConfigNotin.Length
            If ConfigNotin.Length = "16688" Then
                cbConfiguraNotin.BackColor = Color.LawnGreen
                cbConfiguraNotin.Enabled = False
            End If
        Else
            cbConfiguraNotin.BackColor = SystemColors.Control
            cbConfiguraNotin.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "Office2016.exe") Then
            Dim Archivo2016 As New FileInfo(RutaDescargas & "Office2016.exe")
            Dim Length2016 As Long = Archivo2016.Length
            If Archivo2016.Length = "739967123" Then
                cbOffice2016.BackColor = Color.LawnGreen
                cbOffice2016.Enabled = False
            ElseIf Archivo2016.Length < "739967123" Then
                cbOffice2016.BackColor = Color.LightSalmon
            End If
        Else
            cbOffice2016.BackColor = SystemColors.Control
            cbOffice2016.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "ConfiguraWord2016.exe") Then
            Dim Config2016 As New FileInfo(RutaDescargas & "ConfiguraWord2016.exe")
            Dim LengthConfig2016 As Long = Config2016.Length
            If Config2016.Length = "349696" Then
                cbConfiguraWord2016.BackColor = Color.LawnGreen
                cbConfiguraWord2016.Enabled = False
            End If
        Else
            cbConfiguraWord2016.BackColor = SystemColors.Control
            cbConfiguraWord2016.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "jnemo-latest.exe") Then
            Dim jNemo As New FileInfo(RutaDescargas & "jnemo-latest.exe")
            Dim LengthjNemo As Long = jNemo.Length
            If jNemo.Length = "12672337" Then
                cbNemo.BackColor = Color.LawnGreen
                cbNemo.Enabled = False
            End If
        Else
            cbNemo.BackColor = SystemColors.Control
            cbNemo.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "PuestoNotinC.exe") Then
            Dim PuestoNotinC As New FileInfo(RutaDescargas & "PuestoNotinC.exe")
            Dim LengthPuestoNotinC As Long = PuestoNotinC.Length
            If PuestoNotinC.Length = "15904392" Then
                cbPuestoNotin.BackColor = Color.LawnGreen
                cbPuestoNotin.Enabled = False
            End If
        Else
            cbPuestoNotin.BackColor = SystemColors.Control
            cbPuestoNotin.Enabled = True
        End If

        'REQUISITOS. Cuento los archivos en el directorio
        If System.IO.Directory.Exists(RutaDescargas & "\Requisitos") Then
            Dim ArchivosenRequisitos = My.Computer.FileSystem.GetFiles(RutaDescargas & "\Requisitos")

            If ArchivosenRequisitos.Count = 4 Then
                cbRequisitos.BackColor = Color.LawnGreen
                cbRequisitos.Enabled = False
            Else
                cbRequisitos.BackColor = SystemColors.Control
            End If
        Else
            cbRequisitos.BackColor = SystemColors.Control
            cbRequisitos.Enabled = True
        End If

        'ANCERT Como puede variar el tamaño solo miro que exista el fichero
        If System.IO.File.Exists(RutaDescargas & "SFeren-2.8.exe") Then
            cbSferen.BackColor = Color.LawnGreen
            cbSferen.Enabled = False
        Else
            cbSferen.BackColor = SystemColors.Control
            cbSferen.Enabled = True
        End If

        If System.IO.File.Exists(RutaDescargas & "PasarelaSigno.exe") Then
            cbPasarelaSigno.BackColor = Color.LawnGreen
            cbPasarelaSigno.Enabled = False
        Else
            cbPasarelaSigno.BackColor = SystemColors.Control
            cbPasarelaSigno.Enabled = True
        End If

        'SOFTWARE TERCEROS
        If System.IO.Directory.Exists(RutaDescargas & "\Software") Then
            cbTerceros.BackColor = Color.LawnGreen
        Else
            cbTerceros.BackColor = SystemColors.Control
        End If


    End Sub



    'COMENZAR DESCARGAS
    Private Sub btDescargar_Click(sender As Object, e As EventArgs) Handles btDescargar.Click
        'TODO Mas adelante me meto con la barra de progreso en otro hilo
        'pbDescargas.Visible = True

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
                My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "tecnicos", "20070401", False, 20000, False)
            Catch ex As Exception
                'MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "wget.exe", RutaDescargas & "wget.exe", "tecnicos", "20070401", False, 20000, False)
                If REINTENTAR = DialogResult.Retry Then

                End If

            End Try

        End If


        'Creación contenido del fichero
        ' MAS adelante cambiar Rutas para ordenar las descargas
        If cbOffice2003.Checked Then
            texto = texto & PuestoNotin & "Office2003.exe" & vbCrLf
        End If

        If cbOffice2016.Checked Then
            texto = texto & PuestoNotin & "KMSpico10.exe" & vbCrLf
            texto = texto & PuestoNotin & "Office2016.exe" & vbCrLf
        End If

        If cbNemo.Checked Then
            texto = texto & "http://nemo.notin.net/jnemo-latest.exe" & vbCrLf
        End If

        If cbPuestoNotin.Checked Then
            texto = texto & PuestoNotin & "PuestoNotinC.exe" & vbCrLf
            'Añado aqui los Accesos directos
            texto = texto & PuestoNotin & "AccesosDirectos.exe" & vbCrLf
        End If

        If cbRequisitos.Checked Then
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "KryptonSuite300.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "Office2003PrimaryInterop.msi" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2005.exe" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "VisualTools2015.exe" & vbCrLf
            requisitos = requisitos & PuestoNotin & "Requisitos/" & "Framework35.bat" & vbCrLf
        End If

        If cbSferen.Checked Then
            texto = texto & PuestoNotin & "SFeren-2.8.exe" & vbCrLf
        End If

        If cbPasarelaSigno.Checked Then
            texto = texto & PuestoNotin & "PasarelaSigno.exe" & vbCrLf
        End If

        If cbTerceros.Checked Then
            terceros = terceros & PuestoNotin & "Software/" & "AcrobatReaderDC.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "FileZilla_3_win64-setup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "ChromeSetup.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "Notepad_x64.exe" & vbCrLf
            terceros = terceros & PuestoNotin & "Software/" & "WinRAR5.exe" & vbCrLf
        End If

        'Descagar configuradores del autochequeo
        If cbConfiguraNotin.Checked Then
            registro = registro & PuestoNotin & "ConfigAccess.reg" & vbCrLf
            registro = registro & PuestoNotin & "FTComoAdministrador.reg" & vbCrLf
            registro = registro & PuestoNotin & "VentanasSigno.reg" & vbCrLf
            registro = registro & PuestoNotin & "MSOUTL.OLB" & vbCrLf
            registro = registro & PuestoNotin & "ExclusionDefender.reg" & vbCrLf
        End If

        If cbConfiguraWord2016.Checked Then
            texto = texto & PuestoNotin & "ConfiguraWord2016.exe" & vbCrLf
        End If



        'Guardar el texto en el fichero
        System.IO.File.WriteAllText(RutaDescargas & FILE_DOWNLOAD, texto)
        System.IO.File.WriteAllText(RutaDescargas & REQUISITOS_DOWNLOAD, requisitos)
        System.IO.File.WriteAllText(RutaDescargas & TERCEROS_DOWNLOAD, terceros)
        System.IO.File.WriteAllText(RutaDescargas & REGISTRO_DOWNLOAD, registro)

        'EJECUCIONES DE WGET POR RUTAS DE DESCARGA

        'Ejecutar WGET Office y Notin
        Dim WGETPANDORA As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=tecnicos --ftp-password=20070401 -i " & """" & RutaDescargas & "descargas.txt"" -P " & RutaDescargas
        Dim RutaCMDWget As String = RutaDescargas & WGETPANDORA
        'SI EXISTEN ESPACIOS EN LOS NOMBRES DE ARCHIVOS NO FUNCIONA YA QUE SI PONGO COMILLAS SI NO LLEVA ESPACIOS DA ERROR

        Shell("cmd.exe /c " & RutaCMDWget, AppWinStyle.NormalFocus, True)
        YaDescargados()

        'Ejecutar WGET Requisitos
        If cbRequisitos.Checked Then
            Dim WGETPANDORAREQUISITOS As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=tecnicos --ftp-password=20070401 -i " & """" & RutaDescargas & "requisitos.txt"" -P " & RutaDescargas & "Requisitos\"
            Dim RutaCMDWgetRequisitos As String = RutaDescargas & WGETPANDORAREQUISITOS

            Shell("cmd.exe /c " & RutaCMDWgetRequisitos, AppWinStyle.NormalFocus, True)
            YaDescargados()
        End If

        'Ejecutar WGET Terceros
        If cbTerceros.Checked Then
            Dim WGETPANDORATERCEROS As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=tecnicos --ftp-password=20070401 -i " & """" & RutaDescargas & "terceros.txt"" -P " & RutaDescargas & "Software\"
            Dim RutaCMDWgetTerceros As String = RutaDescargas & WGETPANDORATERCEROS

            Shell("cmd.exe /c " & RutaCMDWgetTerceros, AppWinStyle.NormalFocus, True)
            YaDescargados()
        End If
        'Ejecutar WGET Registro
        If cbConfiguraNotin.Checked Then
            Dim WGETPANDORREGISTRO As String = "wget.exe -q --show-progress -t 5 -c --ftp-user=tecnicos --ftp-password=20070401 -i " & """" & RutaDescargas & "registro.txt"" -P " & RutaDescargas & "Registro\"
            Dim RutaCMDWgetRegistro As String = RutaDescargas & WGETPANDORREGISTRO

            Shell("cmd.exe /c " & RutaCMDWgetRegistro, AppWinStyle.NormalFocus, True)
            YaDescargados()
        End If

        'TODO Añadir o bien la barra o un mensaje de Trabajando...
        'pbDescargas.Visible = False

        'Borrar ficheros txt escritos
        System.IO.File.Delete(RutaDescargas & FILE_DOWNLOAD)
        System.IO.File.Delete(RutaDescargas & REQUISITOS_DOWNLOAD)
        System.IO.File.Delete(RutaDescargas & TERCEROS_DOWNLOAD)
        System.IO.File.Delete(RutaDescargas & REGISTRO_DOWNLOAD)

        cbOffice2003.Checked = False
        cbOffice2016.Checked = False
        cbNemo.Checked = False
        cbRequisitos.Checked = False
        cbPuestoNotin.Checked = False
        cbSferen.Checked = False
        cbPasarelaSigno.Checked = False
        cbTerceros.Checked = False

        MessageBox.Show("Descargas finalizadas", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Marcar TODOS. Opcion variable Marcar/Desmarcar todos
    Private MarcarTodos As Integer = 0
    Private Sub btTodo_Click(sender As Object, e As EventArgs) Handles btTodo.Click
        If MarcarTodos = 0 Then
            cbOffice2003.Checked = True
            cbOffice2016.Checked = True
            cbNemo.Checked = True
            cbRequisitos.Checked = True
            cbPuestoNotin.Checked = True
            cbSferen.Checked = True
            cbPasarelaSigno.Checked = True
            cbTerceros.Checked = True
            btTodo.Text = "Desmarcar todos"
            'sumar uno a la variable
            MarcarTodos = 1
        ElseIf MarcarTodos = 1 Then
            cbOffice2003.Checked = False
            cbOffice2016.Checked = False
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



    'Autochequear Configuradores Notin y Word 2016
    Private Sub cbOffice2003_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2003.CheckedChanged
        cbConfiguraNotin.CheckState = cbOffice2003.CheckState
    End Sub

    Private Sub cbOffice2016_CheckedChanged(sender As Object, e As EventArgs) Handles cbOffice2016.CheckedChanged
        cbConfiguraWord2016.CheckState = cbOffice2016.CheckState
    End Sub




    Private Sub btSalir_Click(sender As Object, e As EventArgs) Handles btSalir.Click
        Me.Close()
    End Sub


    'CONTROLES DE INSTALACION

    Private Sub btNotinKubo_Click(sender As Object, e As EventArgs) Handles btNotinKubo.Click

        '--------------------
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
        ' CONTROLAR PULSR ABORTAR


        If UnidadF() = True Then
            lbUnidadF.Text = "CONECTADA"
            lbUnidadF.ForeColor = Color.Green
        End If

        'Comprobamos si existe ya unrar.exe
        If Not System.IO.File.Exists(RutaDescargas & "unrar.exe") Then

            'Descargar ejecutable UnRAR
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "tecnicos", "20070401", False, 20000, True)
            Catch ex As Exception
                MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "tecnicos", "20070401", False, 20000, True)
                If REINTENTAR = DialogResult.Retry Then
                End If
            End Try
        End If

        'TODO que sandra me ayude con esto. Seguro que se puede hacer mejor


        Dim NotinSiNo As Integer = Nothing

        Dim EjecutableAccess As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE11\MSACCESS.EXE")

        If EjecutableAccess = False Then
            InstalarNotinNet()
        End If

        If EjecutableAccess = True Then
            NotinSiNo = MessageBox.Show("Posible instalación existente de NOTIN .NET (Access 2003). ¿Ejecutar instalación Office 2003?", "Instalación Office 2003", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
        If NotinSiNo = 6 Then
            InstalarNotinNet()
        Else
            InstalarRequisitosNet()
        End If

    End Sub

    'TODO mas adelante comprobar para cada funcion si existe officce2003.exe 2016... etc
    Private Sub InstalarNotinNet()
        'Claves Registro
        Try
            Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\FTComoAdministrador.reg", AppWinStyle.Hide, True)
            Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ConfigAccess.reg", AppWinStyle.Hide, True)
            Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\VentanasSigno.reg", AppWinStyle.Hide, True)
            Shell("cmd.exe /c REGEDIT /s " & RutaDescargas & "Registro\ExclusionDefender.reg", AppWinStyle.Hide, True)
            File.Copy(RutaDescargas & "Registro\MSOUTL.OLB", "C:\Program Files (x86)\Common Files\microsoft shared\OFFICE11\MSOUTL.OLB", True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PuestoNotinC.exe " & "C:\", AppWinStyle.NormalFocus, True)

        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2003.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)

        'TODO que muestre el numero de serie en nuevo formulario. Mejor que el bloc de notas
        Shell("C:\WINDOWS\system32\notepad.exe " & RutaDescargas & "Office2003\NSERIE.TXT", AppWinStyle.NormalNoFocus, False)

        Shell("cmd.exe /C " & RutaDescargas & "Office2003\SETUP.EXE", AppWinStyle.Hide, True)
        Shell("cmd.exe /C taskkill /f /im notepad.exe", AppWinStyle.Hide, False)

        Shell("cmd.exe /c " & RutaDescargas & """" & "Office2003\SP3 y Parche Access\Office2003SP3-KB923618-FullFile-ESN.exe" & """", AppWinStyle.Hide, True)
        Shell("cmd.exe /c " & RutaDescargas & """" & "Office2003\SP3 y Parche Access\MSACCESS.msp" & """", AppWinStyle.Hide, True)
        'Libreria compartida Outlook

        InstalarRequisitosNet()
    End Sub

    Private Sub InstalarRequisitosNet()

        Dim RequisitosSiNo As Integer = Nothing

        RequisitosSiNo = MessageBox.Show("¿Instalar Requisitos .NET?", "Pre-Requisitos", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If RequisitosSiNo = 6 Then
            'TODO Arreglar esto de Administrador
            Shell("cmd.exe /c " & "runas /user:administrador@localhost " & """" & "DISM /Online /Enable-Feature /FeatureName:NetFx3 /All" & """", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\KryptonSuite300.msi", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\Office2003PrimaryInterop.msi", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2005.exe", AppWinStyle.Hide, True)
            Shell("cmd.exe /c " & RutaDescargas & "Requisitos\VisualTools2015.exe", AppWinStyle.Hide, True)
            InstalarWord2016()
        Else
            InstalarWord2016()
        End If
    End Sub

    Private Sub InstalarWord2016()

        Dim WordSiNo As Integer = Nothing

        Dim EjecutableWord As Boolean = File.Exists("C:\Program Files (x86)\Microsoft Office\OFFICE16\WINWORD.EXE")

        If EjecutableWord = False Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
            ConfigurarWord2016()
        End If

        If EjecutableWord = True Then
            WordSiNo = MessageBox.Show("Se ha detectado una posible instalación de WORD 2016. ¿Ejecutar instalación Office 2016?", "Instalación Office 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
        If WordSiNo = 6 Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "Office2016.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /C " & RutaDescargas & "Office2016\SETUP.EXE", AppWinStyle.Hide, True)
            ConfigurarWord2016()
        Else
            ConfigurarWord2016()
        End If
    End Sub

    Private Sub ConfigurarWord2016()
        Dim ConfigurarWord = MessageBox.Show("¿Configuramos Word 2016?", "Configurar Word 2016", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If ConfigurarWord = 6 Then
            Shell("cmd.exe /C " & RutaDescargas & "ConfiguraWord2016.exe", AppWinStyle.Hide, True)
        Else
            KMSPico()
        End If
    End Sub


    Private Sub KMSPico()
        'TODO Crear clave Registro para Excluir rutas de descarga e instalacion

        Dim KMSPicoSInO As Integer = Nothing
        KMSPicoSInO = MessageBox.Show("¿Ejecutar Activador Office 2016?", "KMSPico 10.2.0", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If KMSPicoSInO = 6 Then
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "KMSpico10.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /C " & RutaDescargas & "KMSpico10\" & "KMSpico_setup.exe", AppWinStyle.Hide, True)
            MessageBox.Show("Añade Exclusión de AntiVirus hacia KMSPico antes de ejecutarlo", "Ejecucion de KMSPico", MessageBoxButtons.OK)
            SoftwareAncert()
        Else
            SoftwareAncert()
        End If
    End Sub


    Private Sub SoftwareAncert()
        Dim Ancert As Integer = Nothing
        Ancert = MessageBox.Show("¿Instalar Software Ancert?", "Sferen y Pasarela", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Ancert = 6 Then
            Shell("cmd.exe /c " & RutaDescargas & "SFeren-2.8.exe", AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & "unrar.exe x -u -y " & RutaDescargas & "PasarelaSigno.exe " & RutaDescargas, AppWinStyle.NormalFocus, True)
            Shell("cmd.exe /c " & RutaDescargas & """" & "\Pasarela 2.1\setup.exe" & """", AppWinStyle.NormalFocus, True)
            AccesosDirectosEscritorio()
        Else
            AccesosDirectosEscritorio()
        End If
    End Sub

    Private Sub AccesosDirectosEscritorio()
        Dim Escritorio As String = """" & My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & """"
        Shell("cmd.exe /c " & RutaDescargas & "unrar.exe e -u -y " & RutaDescargas & "AccesosDirectos.exe " & Escritorio, AppWinStyle.NormalFocus, True)

        MessageBox.Show("Instalación Notin .Net + Kubo finalizada", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub






End Class
