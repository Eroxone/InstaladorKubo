Imports Instalador.LeerFicherosINI

Public Class ObtenerEjecutables
    Private Shared RutaDescargas = cIniArray.IniGet("C:\TEMP\InstaladorKubo\InstaladorKubo.ini", "RUTAS", "RUTADESCARGAS", "C:\NOTIN\")
    Private Const PuestoNotin As String = "ftp://ftp.lbackup.notin.net/tecnicos/JUANJO/PuestoNotin/"

    Public Shared Sub obtenerwget()
        If Not System.IO.File.Exists(RutaDescargas & "wget.exe") Then
            FormInstaladorKubo.RegistroInstalacion("No se encontró WGET. Se procede a su descarga.")
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

    Public Shared Sub obtenerrobocopy()
        If Not System.IO.File.Exists(RutaDescargas & "robocopy.exe") Then
            FormInstaladorKubo.RegistroInstalacion("No se encontró ROBOCOPY. Se procede a su descarga.")
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


    Public Shared Sub obtenerunrar()
        'Comprobamos si existe ya unrar.exe
        If Not System.IO.File.Exists(RutaDescargas & "unrar.exe") Then
            FormInstaladorKubo.RegistroInstalacion("No se encontró UNRAR. Se procede a su descarga.")
            'Descargar ejecutable UnRAR
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "juanjo", "Palomeras24", False, 20000, True)
            Catch ex As Exception
                'MessageBox.Show("Error al obtener el archivo. Revisa tu conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "unrar.exe", RutaDescargas & "unrar.exe", "juanjo", "Palomeras24", False, 20000, True)
                If REINTENTAR = DialogResult.Retry Then
                End If
            End Try
        End If
    End Sub


    Public Shared Sub curl()
        If Not System.IO.File.Exists(RutaDescargas & "curl.exe") Then
            FormInstaladorKubo.RegistroInstalacion("No se encontró WGET. Se procede a su descarga.")
            'Descargar ejecutable WGet
            Try
                'Dim RutaSinBarra As String = RutaDescargas.Substring(0, RutaDescargas.Length - 1)
                My.Computer.Network.DownloadFile(PuestoNotin & "curl.exe", RutaDescargas & "curl.exe", "juanjo", "Palomeras24", False, 20000, False)
            Catch ex As Exception

                'Reintentar descarga
                Dim REINTENTAR As DialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                My.Computer.Network.DownloadFile(PuestoNotin & "curl.exe", RutaDescargas & "curl.exe", "juanjo", "Palomeras24", False, 20000, False)
                If REINTENTAR = DialogResult.Retry Then
                    My.Computer.Network.DownloadFile(PuestoNotin & "curl.exe", RutaDescargas & "curl.exe", "juanjo", "Palomeras24", False, 20000, True)
                End If
            End Try
        End If
    End Sub


End Class
