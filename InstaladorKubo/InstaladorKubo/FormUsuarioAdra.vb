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
        'Dim carpetasperfil = Directory.GetDirectories("C:\TEMP\Prueba")
        Dim totalcarpetas As Integer = carpetasperfil.Count
        Dim numcarpeta As Integer = 0
        File.WriteAllText(rutadescargas & "ADRA\CarpetasPerfil.txt", "")
        While numcarpeta < totalcarpetas
            File.AppendAllText(rutadescargas & "ADRA\CarpetasPerfil.txt", carpetasperfil(numcarpeta) & vbCrLf)
            Dim carpetaactual As String = carpetasperfil(numcarpeta)
            Try
                Directory.Delete(carpetaactual, True)
            Catch ex As Exception
                MessageBox.Show("No se pudieron eliminar carpetas del Perfil. Mas info en el Logger.","Error Borrado Carpetas Perfil",MessageBoxButtons.OK,MessageBoxIcon.Error)
                FrmInstaladorKubo.RegistroInstalacion("ERROR Limpieza Perfil Usuario: " & ex.Message)
            End Try
            numcarpeta = numcarpeta + 1
        End While


        Dim archivosnr = Directory.GetFiles("\\NotinRapp\Z\" & userseleccionado & "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\NR (RADC)")
        'Dim archivosnr = Directory.GetFiles("C:\TEMP\Prueba")
        Dim totalarchivos As Integer = archivosnr.Count
        Dim numarchivo As Integer = 0
        File.WriteAllText(rutadescargas & "ADRA\ArchivosNR.txt", "")
        While numarchivo < totalarchivos
            File.AppendAllText(rutadescargas & "ADRA\ArchivosNR.txt", archivosnr(numarchivo) & vbCrLf)
            Dim archivoactual As String = archivosnr(numarchivo)
            Try
                File.Delete(archivoactual)
            Catch ex As Exception
                MessageBox.Show("No se pudieron eliminar archivos de NR (RADC). Mas info en el Logger.", "Error borrado NR (RADC)", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FrmInstaladorKubo.RegistroInstalacion("ERROR Limpieza archivos NR (RADC): " & ex.Message)
            End Try
            numarchivo = numarchivo + 1
        End While

        'Borrar Usuario
        MessageBox.Show("Selecciona ahora tu Usuario y clic en Eliminar. Una vez termine puedes cerrar sesión e Iniciar de nuevo con el usuario original." & vbCrLf & "(Empezamos por esto y en siguientes versiones lo automatizo)", "Eliminar usuario en NOTARIA", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Process.Start("control userpasswords2")
    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.Close()
    End Sub

    Private Sub BtRegistroInstalaciones_Click(sender As Object, e As EventArgs) Handles BtRegistroInstalaciones.Click
        Process.Start("notepad.exe", "C:\TEMP\InstaladorKubo\RegistroInstalacion.txt")
    End Sub
End Class