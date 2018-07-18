Imports System.IO
Imports InstaladorKubo.LeerFicherosINI


Public Class FormUsuarioAdra
    Private rutadescargas = cIniArray.IniGet("C:\TEMP\InstaladorKubo", "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")

    Private Sub FormUsuarioAdra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim carpetasperfil = Directory.GetDirectories("C:\USERS\")
        Dim totalcarpetas As Integer = carpetasperfil.Count - 1
        Dim numcarpeta = 0
        While numcarpeta < totalcarpetas
            'File.AppendAllText("C:\TEMP\carpetas.txt", " " & """" & carpetasperfil(numcarpeta) & """")
            'nomusuario = numcarpeta + 1
            Dim nombrelista = carpetasperfil(numcarpeta)
            Dim lenghtnombre As Integer = carpetasperfil(numcarpeta).Length - 9
            Dim solonombre = nombrelista.Substring(9, lenghtnombre)
            LBoxUsuarios.Items.Add(solonombre)
            numcarpeta = numcarpeta + 1
        End While
        MessageBox.Show("Selecciona ahora tu Usuario y clic en LIMPIAR. Una vez termine puedes cerrar sesión e Iniciar de nuevo con el usuario original del Dominio." & vbCrLf & "(Empezamos por esto y en siguientes versiones lo automatizo)", "Eliminar usuario en NOTARIA", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        Directory.CreateDirectory(rutadescargas & "ADRA")

        Dim carpetasperfil = Directory.GetDirectories("\\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Workspaces")
        FrmInstaladorKubo.RegistroInstalacion("Se procede a limpiar la Ruta \\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Workspaces. Mas info en TXT CarpetasPerfil en " & rutadescargas & "ADRA\")
        'Dim carpetasperfil = Directory.GetDirectories("C:\TEMP\Prueba")
        Dim totalcarpetas As Integer = carpetasperfil.Count
        Dim numcarpeta As Integer = 0
        File.WriteAllText(rutadescargas & "ADRA\CarpetasPerfil.txt", "")
        While numcarpeta < totalcarpetas
            File.AppendAllText(rutadescargas & "ADRA\CarpetasPerfil.txt", carpetasperfil(numcarpeta) & vbCrLf)
            Dim carpetaactual As String = carpetasperfil(numcarpeta)
            Try
                'Directory.Delete(carpetaactual, True)
                Shell("cmd /c RD /S /Q " & """" & carpetaactual & """", AppWinStyle.Hide, True)
                FrmInstaladorKubo.RegistroInstalacion("Eliminada Ruta del Perfil con ID " & carpetaactual)
            Catch ex As Exception
                MessageBox.Show("No se pudieron eliminar carpetas del Perfil. Mas info en el Logger.", "Error Borrado Carpetas Perfil", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FrmInstaladorKubo.RegistroInstalacion("ERROR Limpieza Perfil Usuario: " & ex.Message)
                FrmInstaladorKubo.RegistroInstalacion("Se buscó la Ruta " & carpetaactual & ". No se pudo eliminar.")
            End Try
            numcarpeta = numcarpeta + 1
        End While

        'Limpieza de recursos NR del Adra
        Dim archivosnr = Directory.GetFiles("\\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC)")
        FrmInstaladorKubo.RegistroInstalacion("Se procede a limpiar la Ruta \\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC). Mas info en TXT ArchivosNR.txt en " & rutadescargas & "ADRA\")
        'Dim archivosnr = Directory.GetFiles("C:\TEMP\Prueba")
        Dim totalarchivos As Integer = archivosnr.Count
        Dim numarchivo As Integer = 0
        File.WriteAllText(rutadescargas & "ADRA\ArchivosNR.txt", "")
        While numarchivo < totalarchivos
            File.AppendAllText(rutadescargas & "ADRA\ArchivosNR.txt", archivosnr(numarchivo) & vbCrLf)
            Dim archivoactual As String = archivosnr(numarchivo)
            Try
                'File.Delete(archivoactual)
                Shell("del /F /Q " & """" & archivoactual & """", AppWinStyle.Hide, True)
            Catch ex As Exception
                MessageBox.Show("No se pudieron eliminar archivos de NR (RADC). Mas info en el Logger.", "Error borrado NR (RADC)", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FrmInstaladorKubo.RegistroInstalacion("ERROR Limpieza archivos NR (RADC): " & ex.Message)
                FrmInstaladorKubo.RegistroInstalacion("Se procedió a eliminar Perfil con ID " & archivoactual & ". No se pudo eliminar.")
            End Try
            numarchivo = numarchivo + 1
        End While

        'Limpiar carpeta RAPP_Control
        Try
            Dim hostname = Environment.MachineName
            Directory.Delete("\\NotinRapp\Z\rapp_control\" & hostname, True)
            FrmInstaladorKubo.RegistroInstalacion("Eliminada ruta en Rapp_Control para " & hostname & ".")
        Catch ex As Exception
            MessageBox.Show("No se limpiar Rapp_Control para este Equipo. Mas info en el Logger.", "Error borrado RAPP_CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim hostname = Environment.MachineName
            FrmInstaladorKubo.RegistroInstalacion("ERROR eliminando Rapp_Control para " & hostname & "." & ex.Message)
        End Try

        'TODO limpiar iconos Escritorio
        Dim vinculosnr = Directory.GetFiles("\\NotinRapp\Z\" & userseleccionado & "\Favorites\Vínculos")
        FrmInstaladorKubo.RegistroInstalacion("Se procede a limpiar los Vínculos para" & userseleccionado & ". Mas info en TXT VinculosNR.txt en " & rutadescargas & "ADRA\")
        Dim totalvinculos As Integer = vinculosnr.Count
        Dim numvinculos As Integer = 0
        File.WriteAllText(rutadescargas & "ADRA\VinculosNR.txt", "")
        While numvinculos < totalvinculos
            File.AppendAllText(rutadescargas & "ADRA\ArchivosNR.txt", archivosnr(numarchivo) & vbCrLf)
            Dim vinculoactual As String = vinculosnr(numvinculos)
            Try
                'File.Delete(archivoactual)
                Shell("del /F /Q " & """" & vinculoactual & """", AppWinStyle.Hide, True)
            Catch ex As Exception
                MessageBox.Show("No se pudieron eliminar archivos de NR (RADC). Mas info en el Logger.", "Error borrado Vínculos NR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FrmInstaladorKubo.RegistroInstalacion("ERROR Limpieza archivos en Vínculos NR): " & ex.Message)
            End Try
            numvinculos = numvinculos + 1
        End While

        Try
            'Process.Start("control userpasswords2")
            MessageBox.Show("A continuación selecciona " & userseleccionado & " y haz clic en Quitar. Con esto limpiaremos el Usuario Local con cuenta en el Dominio.", "Eliminar Usuario del Dominio en Local", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Shell("cmd /c control userpasswords2", AppWinStyle.Hide, True)
            BtCerrarsesion.Visible = True
        Catch ex As Exception
            FrmInstaladorKubo.RegistroInstalacion("ERROR. No se pudo llamar a Control UserPasswords2: " & ex.Message)
        End Try

        MessageBox.Show("Limpieza del Perfil Completada. Cierra sesión e Inicia con tu usuario NOTARIA\" & userseleccionado & " para terminar la operación.", "== PROCESO TERMINADO ==")
        FrmInstaladorKubo.RegistroInstalacion("Terminada Limpieza del Perfil " & userseleccionado & " en entorno ADRA.")
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