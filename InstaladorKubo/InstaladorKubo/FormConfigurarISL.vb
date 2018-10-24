Imports System.IO
Imports System.Xml
Imports System.Environment
Imports Instalador.FormNavegador

Public Class FrmConfigurarISL

    Private Sub BtConfirmarISL_Click(sender As Object, e As EventArgs) Handles BtConfirmarISL.Click
        If TbISLGrupo.Text = "" Then
            MessageBox.Show("No se admiten campos vacíos", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        ElseIf TbISLNombre.Text = "" Then
            MessageBox.Show("No se admiten campos vacíos", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        Else
            LimpiarUID()
            FormNavegador.IniciarNavegador("isl")
            Me.Close()
        End If
    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        TbISLGrupo.Text = ""
        TbISLNombre.Text = ""
        FormInstaladorKubo.Show()
        FormNavegador.Close()
        Me.Close()
    End Sub

    Private Sub FrmConfigurarISL_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim profile As String = LeerXML()
            Dim longprofile As Integer = profile.Length
            Dim longusuario As Integer = profile.LastIndexOf(".")
            Dim usuario = profile.Remove(longusuario, longprofile - longusuario)
            Dim grupo = profile.Remove(0, longusuario + 1)
            TbISLGrupo.Text = grupo
            TbISLNombre.Text = usuario
            FormInstaladorKubo.RegistroInstalacion("ISL: Leído " & grupo & " - " & usuario & " desde el XML jNemo.")
        Catch ex As Exception
            TbISLNombre.Text = LeerXML().ToString
            TbISLGrupo.Text = "UNIDATA"
            FormInstaladorKubo.RegistroInstalacion("ISL: Se obtiene usuario: " & TbISLNombre.Text.ToString & ". No se pudo determinar el Grupo. Se ofrece UNIDATA.")
        End Try

    End Sub

    'TODO QUE SANDRA ME EXPLIQUE COMO HACER ESTO SIN FUNCION CON UN SUB Y UN BYVAL
    Private Function LeerXML()
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        Dim jnemoxml As String = appData & "\jNemo\jnemo.xml"
        Dim jnemoisl As String = appData & "\jNemo\jnemoisl.xml"

        If File.Exists(jnemoxml) Then
            Try
                File.Copy(jnemoxml, appData & "\jNemo\jnemoisl.xml", True)
            Catch ex As Exception
                FormInstaladorKubo.RegistroInstalacion("ERROR ISL. No se pudo realizar la copia del XML de jNemo")
            End Try

            Dim doc As New XmlDocument()
            doc.Load(jnemoisl)

            Dim ultimoprofile As String = ""

            '"/jnemo/profiles/profile" ' nos quedamos con el ultimo
            Dim profiles As XmlNode = doc.SelectSingleNode("/jnemo/profiles")
            If profiles IsNot Nothing Then
                Dim nr As New XmlNodeReader(profiles)
                While nr.Read()
                    ' IsStartElement para que lea username no /username ya que en ese caso se quedaría solo con el final.
                    If nr.IsStartElement() AndAlso nr.Name = "username" Then
                        If nr.Read() Then
                            ultimoprofile = nr.Value.Trim()
                        End If
                    End If
                End While
            End If
            Return ultimoprofile
        Else
            'Obtener nombre del equipo
            Dim equipousuario As String = (My.User.Name)
            Dim equipo As Integer = equipousuario.LastIndexOf("\")
            Dim usuario = equipousuario.Remove(0, equipo + 1)
            FormInstaladorKubo.RegistroInstalacion("ISL: No se pudo obtener <profile> desde el XML. Leemos Usuario del equipo: " & usuario)
            Return usuario
        End If

    End Function

    Private Sub LimpiarUID()
        Dim claveregisl As String = "reg delete " & """" & "HKLM\SOFTWARE\WOW6432Node\ISL Online\ISL AlwaysOn" & """" & " /f"
        Directory.CreateDirectory(FormInstaladorKubo.RutaDescargas & "Registro")
        File.WriteAllText(FormInstaladorKubo.RutaDescargas & "Registro\ClaveRegISL.bat", claveregisl)

        Try
            FormInstaladorKubo.RunAsAdmin(FormInstaladorKubo.RutaDescargas & "Registro\ClaveRegISL.bat")
            FormInstaladorKubo.RegistroInstalacion("ISL. Limpiada la Clave Registro para ISL Alaways ON.")
        Catch ex As Exception
            FormInstaladorKubo.RegistroInstalacion("ISL. No se pudo eliminar la Clave Registro para ISL Alaways ON.")
        End Try
    End Sub

End Class