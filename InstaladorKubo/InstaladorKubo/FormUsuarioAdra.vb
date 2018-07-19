Imports System.IO
Imports InstaladorKubo.LeerFicherosINI


Public Class FormUsuarioAdra
    Private rutadescargas = cIniArray.IniGet("C:\TEMP\InstaladorKubo", "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")

    Private Sub FormUsuarioAdra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim carpetasperfil = Directory.GetDirectories("C:\USERS\")
        Dim totalcarpetas As Integer = carpetasperfil.Count - 1
        Dim numcarpeta = 0
        While numcarpeta < totalcarpetas
            Dim nombrelista = carpetasperfil(numcarpeta)
            Dim lenghtnombre As Integer = carpetasperfil(numcarpeta).Length - 9
            Dim solonombre = nombrelista.Substring(9, lenghtnombre)
            LBoxUsuarios.Items.Add(solonombre)
            numcarpeta = numcarpeta + 1
        End While

        'Ocultar funciones para usuarios con sesión iniciada bajo usuario del Dominio
        Dim equipousuario As String = (My.User.Name)
        Dim equipo As Integer = equipousuario.LastIndexOf("\")
        Dim usuario = equipousuario.Remove(0, equipo + 1).ToUpper

        If usuario <> "USUARIO" OrElse "ADMINISTRADOR" Then
            BtLimpiar.Enabled = False
            BtCerrarsesion.Visible = True
        Else
            MessageBox.Show("Selecciona ahora tu Usuario y clic en LIMPIAR. Una vez termine puedes cerrar sesión e Iniciar de nuevo con el usuario original del Dominio." & vbCrLf & "(Empezamos por esto y en siguientes versiones lo automatizo)", "Eliminar usuario en NOTARIA", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub LBoxUsuarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LBoxUsuarios.SelectedIndexChanged
        LbNotaria.Visible = True
        LbUsuario.Visible = True
        Dim userseleccionado = LBoxUsuarios.SelectedItem
        LbUsuario.Text = userseleccionado
    End Sub


    Private Sub BtLimpiar_Click(sender As Object, e As EventArgs) Handles BtLimpiar.Click
        Dim userseleccionado = LbUsuario.Text
        FrmInstaladorKubo.RegistroInstalacion("Seleccionado Usuario: " & LBoxUsuarios.SelectedItem)
        If userseleccionado = "UsuarioAdra" Then
            MessageBox.Show("Selecciona un Usuario válido y seguimos.", "Usuario no válido o desconocido", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FrmInstaladorKubo.RegistroInstalacion("No se seleccionó un usuario válido para Limpiar. Se cancela la operación. (eso o coincide que se llama UsuarioAdra y me pego un tiro...")
            Exit Sub
        End If
        'TODO confirmar que los TXT guarden las rutas.
        Directory.CreateDirectory(rutadescargas & "ADRA")

        Dim carpetasperfil = Directory.GetDirectories("\\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Workspaces")
        FrmInstaladorKubo.RegistroInstalacion("Se procede a limpiar la Ruta \\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Workspaces. Mas info en TXT CarpetasPerfil en " & rutadescargas & "ADRA\")
        'Dim carpetasperfil = Directory.GetDirectories("C:\TEMP\Prueba")
        Dim totalcarpetas As Integer = carpetasperfil.Count
        Dim numcarpeta As Integer = 0
        'File.WriteAllText(rutadescargas & "ADRA\CarpetasPerfil.txt", "")
        While numcarpeta < totalcarpetas
            File.AppendAllText(rutadescargas & "ADRA\CarpetasPerfil.txt", carpetasperfil(numcarpeta) & vbCrLf)
            Dim carpetaactual As String = carpetasperfil(numcarpeta)
            Try
                'Directory.Delete(carpetaactual, True)
                Shell("cmd /c RD /S /Q " & """" & carpetaactual & """", AppWinStyle.NormalFocus, True)
                FrmInstaladorKubo.RegistroInstalacion("Eliminada Ruta del Perfil con ID " & carpetaactual)
            Catch ex As Exception
                MessageBox.Show("No se pudieron eliminar carpetas del Perfil. Mas info en el Logger.", "Error Borrado Carpetas Perfil", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FrmInstaladorKubo.RegistroInstalacion("ERROR Limpieza Perfil Usuario: " & ex.Message)
                FrmInstaladorKubo.RegistroInstalacion("Se buscó la Ruta " & carpetaactual & ". No se pudo eliminar.")
                BtLimpiar.BackColor = Color.LightSalmon
            End Try
            numcarpeta = numcarpeta + 1
        End While

        'Limpieza de recursos NR del Adra
        Dim archivosnr = Directory.GetFiles("\\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC)")
        FrmInstaladorKubo.RegistroInstalacion("Se procede a limpiar la Ruta \\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC). Mas info en TXT ArchivosNR.txt en " & rutadescargas & "ADRA\")
        'Dim archivosnr = Directory.GetFiles("C:\TEMP\Prueba")
        Dim totalarchivos As Integer = archivosnr.Count
        Dim numarchivo As Integer = 0
        'File.WriteAllText(rutadescargas & "ADRA\ArchivosNR.txt", "")
        While numarchivo < totalarchivos
            File.AppendAllText(rutadescargas & "ADRA\ArchivosNR.txt", archivosnr(numarchivo) & vbCrLf)
            Dim archivoactual As String = archivosnr(numarchivo)
            Try
                'File.Delete(archivoactual)
                Shell("cmd /c del /F /Q " & """" & archivoactual & """", AppWinStyle.NormalFocus, True)
            Catch ex As Exception
                'MessageBox.Show("No se pudieron eliminar archivos de NR (RADC). Mas info en el Logger.", "Error borrado NR (RADC)", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FrmInstaladorKubo.RegistroInstalacion("ERROR Limpieza archivos NR (RADC): " & ex.Message)
                FrmInstaladorKubo.RegistroInstalacion("Se procedió a eliminar Perfil con ID " & archivoactual & ". No se pudo eliminar.")
                BtLimpiar.BackColor = Color.LightSalmon
            End Try
            numarchivo = numarchivo + 1
        End While

        'Limpiar carpeta RAPP_Control
        Try
            Dim hostname = Environment.MachineName
            Try
                Directory.Delete("\\NotinRapp\Z\rapp_control\" & hostname, True)
                FrmInstaladorKubo.RegistroInstalacion("Eliminada ruta en Rapp_Control para " & hostname & ".")
            Catch ex As Exception
                FrmInstaladorKubo.RegistroInstalacion("No se pudo eliminar Rapp Control para " & hostname & ". " & ex.Message)
                BtLimpiar.BackColor = Color.LightSalmon
            End Try

        Catch ex As Exception
            MessageBox.Show("No se limpiar Rapp_Control para este Equipo. Mas info en el Logger.", "Error borrado RAPP_CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim hostname = Environment.MachineName
            FrmInstaladorKubo.RegistroInstalacion("ERROR eliminando Rapp_Control para " & hostname & "." & ex.Message)
            BtLimpiar.BackColor = Color.LightSalmon
        End Try

        Try
            'Process.Start("control userpasswords2")
            MessageBox.Show("Clic en Configuración bajo Perfiles de Usuario. Selecciona NOTARIA\" & userseleccionado & " y haz clic en ELMINAR. Con esto limpiaremos el Usuario Local con cuenta en el Dominio." & vbCrLf & "La eliminación puede demorarse unos minutos...", "Eliminar Usuario del Dominio en Local", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Shell("cmd /c control userpasswords2", AppWinStyle.Hide, True)


            Shell("cmd.exe /c %WINDIR%\system32\systempropertiesadvanced.exe", AppWinStyle.Hide, True)
            'Shell("control system", AppWinStyle.NormalFocus, False)
            'Threading.Thread.Sleep(2000)
            'AppActivate("Sistema")

            'Dim vecestab As Integer = 0
            'While 11 > vecestab
            '    SendKeys.Send("{TAB}")
            '    vecestab = vecestab + 1
            'End While
            ''Entramos a las Opciones Avanzadas del Sistema
            'SendKeys.Send("{ENTER}")
            'Threading.Thread.Sleep(1000)

            'AppActivate("Propiedades del sistema")
            'Threading.Thread.Sleep(1000)
            'SendKeys.Send("{TAB}")
            'SendKeys.Send("{ENTER}")


            BtCerrarsesion.Visible = True
        Catch ex As Exception
            FrmInstaladorKubo.RegistroInstalacion("ERROR. No se pudo llamar a System Properties Adv a traves del proceso: " & ex.Message)
            BtLimpiar.BackColor = Color.LightSalmon
        End Try

        'Threading.Thread.Sleep(10000)

        'Limpiar iconos
        MessageBox.Show("A continuación se procede a limpiar Iconos en el Escritorio y Vínculos del anterior Perfil.", "Limpieza Escritorio y Vínculos", MessageBoxButtons.OK, MessageBoxIcon.Information)
        LimpiarIconos()

        MessageBox.Show("== LIMPIEZA DE PERFIL COMPLETADA ==" & vbCrLf & "Nota: Si el botón Limpiar está en rojo revisa Logs. En caso contrario Cierra Sesión e inicia con tu usuario NOTARIA\" & userseleccionado & " para terminar la operación. Limpia el Escritorio y Vínculos manualmente. Lo automatizaré en las siguientes versiones.", "== LIMPIEZA USUARIO FINALIZADA ==", MessageBoxButtons.OK, MessageBoxIcon.Information)
        FrmInstaladorKubo.RegistroInstalacion("Terminada Limpieza del Perfil " & userseleccionado & " en entorno ADRA.")
        BtLimpiar.BackColor = Color.PaleGreen
        cIniArray.IniWrite(FrmInstaladorKubo.instaladorkuboini, "ADRA", "LIMPIARPERFIL", "1")
    End Sub

    Private Sub BtLimpiarIconos_Click(sender As Object, e As EventArgs) Handles BtLimpiarIconos.Click
        LimpiarIconos()
    End Sub

    Private Sub LimpiarIconos()
        Dim userseleccionado = LBoxUsuarios.SelectedItem
        If userseleccionado = Nothing Then
            MessageBox.Show("Selecciona previamente el Usuario sobre el que realizar la Limpieza.", "Usuario no seleccionado o no válido.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            'LIMPIAR ESCRITORIO
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\Notin 8.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\Word 2016.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\NotinControlClient (NR).lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\ABBYY FineReader 11.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\Excel 2016.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\Outlook 2016.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\Administrador de Tareas (NR).lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\Agenda.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\reload (NR).lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\ScanImg.lnk")

            'LIMPIAR VINCULOS
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\Notin 8.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\Word 2016.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\NotinControlClient (NR).lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\ABBYY FineReader 11.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\Excel 2016.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\Outlook 2016.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\Administrador de Tareas (NR).lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\Agenda.lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\reload (NR).lnk")
            File.Delete("\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\ScanImg.lnk")

            FrmInstaladorKubo.RegistroInstalacion("LIMPIAR USUARIO ADRA: Ejecutada Limpieza de Escritorio y Vínculos.")
        End If
    End Sub

    Private Sub BtCopiarIconosNR_Click(sender As Object, e As EventArgs) Handles BtCopiarIconosNR.Click
        Dim userseleccionado = LBoxUsuarios.SelectedItem
        If userseleccionado = Nothing Then
            MessageBox.Show("Selecciona previamente el Usuario sobre el que realizar la Copia de iconos NR.", "Usuario no seleccionado o no válido.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim origennr As String = "\\Notinrapp\z\" & userseleccionado & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC)"
            Dim escritorio As String = "\\NOTINRAPP\Z\" & userseleccionado & "\Desktop\"
            Dim vinculos As String = "\\NOTINRAPP\Z\" & userseleccionado & "\Favorites\Vínculos\"
            Try
                'ESCRITORIO
                File.Copy(origennr & "Notin 8.lnk", escritorio & "Notin 8.lnk", True)
                File.Copy(origennr & "Word 2016.lnk", escritorio & "Word 2016.lnk", True)
                File.Copy(origennr & "Outlook 2016.lnk", escritorio & "Outlook 2016.lnk", True)
                File.Copy(origennr & "NotinControlClient (NR).lnk", escritorio & "NotinControlClient (NR).lnk", True)
                File.Copy(origennr & "ABBYY FineReader 11.lnk", escritorio & "ABBYY FineReader 11.lnk", True)
                File.Copy(origennr & "Excel 2016.lnk", escritorio & "Excel 2016.lnk", True)
                File.Copy(origennr & "Administrador de Tareas (NR).lnk", escritorio & "Administrador de Tareas (NR).lnk", True)
                File.Copy(origennr & "Agenda.lnk", escritorio & "Agenda.lnk", True)
                File.Copy(origennr & "ScanImg.lnk", escritorio & "ScanImg.lnk", True)

                'VINCULOS
                File.Copy(origennr & "Notin 8.lnk", vinculos & "Notin 8.lnk", True)
                File.Copy(origennr & "Word 2016.lnk", vinculos & "Word 2016.lnk", True)
                File.Copy(origennr & "Outlook 2016.lnk", vinculos & "Outlook 2016.lnk", True)
                File.Copy(origennr & "NotinControlClient (NR).lnk", vinculos & "NotinControlClient (NR).lnk", True)
                BtCopiarIconosNR.BackColor = Color.PaleGreen
            Catch ex As Exception
                BtCopiarIconosNR.BackColor = Color.LightSalmon
            End Try
        End If

    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.Close()
    End Sub

    Private Sub BtRegistroInstalaciones_Click(sender As Object, e As EventArgs) Handles BtRegistroInstalaciones.Click
        Process.Start("notepad.exe", "C:\TEMP\InstaladorKubo\RegistroInstalacion.txt")
    End Sub

    Private Sub BtCerrarsesion_Click(sender As Object, e As EventArgs) Handles BtCerrarsesion.Click
        Shell("shutdown /l /f", AppWinStyle.Hide, True)
        Me.Close()
    End Sub


End Class