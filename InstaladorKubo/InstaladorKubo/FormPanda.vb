Imports System.IO
Imports System.Xml
Imports System.Environment
Imports Instalador.FormNavegador

Public Class FormPanda
    Private Sub FormPanda_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim profile As String = LeerXML()
            Dim longprofile As Integer = profile.Length
            Dim longusuario As Integer = profile.LastIndexOf(".")
            Dim grupo = profile.Remove(0, longusuario + 1)
            TbPandaNotaria.Text = grupo
            FormInstaladorKubo.RegistroInstalacion("PANDA: Leído " & grupo & " desde el XML jNemo.")
        Catch ex As Exception
            'TbISLGrupo.Text = "UNIDATA"
            TbPandaNotaria.Text = ""
            FormInstaladorKubo.RegistroInstalacion("PANDA: No se pudo obtener el Grupo. Dejamos el campo sin rellenar.")
        End Try

    End Sub

    Private Function LeerXML()
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        Dim jnemoxml As String = appData & "\jNemo\jnemo.xml"
        Dim jnemopanda As String = appData & "\jNemo\jnemopanda.xml"

        If File.Exists(jnemoxml) Then
            Try
                File.Copy(jnemoxml, appData & "\jNemo\jnemopanda.xml", True)
            Catch ex As Exception
                FormInstaladorKubo.RegistroInstalacion("ERROR Panda. No se pudo realizar la copia del XML de jNemo.")
            End Try

            Dim doc As New XmlDocument()
            doc.Load(jnemopanda)

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
            Return "sin determinar"
        End If
    End Function

    Private Sub BtPanda_Click(sender As Object, e As EventArgs) Handles BtPanda.Click
        If TbPandaNotaria.Text = "" Then
            MessageBox.Show("No se admiten campos vacíos o espacios en blanco.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            FormNavegador.IniciarNavegador("panda")
            Me.Close()
        End If
    End Sub


    Private Sub TbPandaNotaria_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbPandaNotaria.KeyPress
        If Char.IsWhiteSpace(e.KeyChar) Then
            MessageBox.Show("El Grupo Notaría no puede contener espacios.", "Grupo erróneo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TbPandaNotaria.Text = ""
        End If
    End Sub
End Class