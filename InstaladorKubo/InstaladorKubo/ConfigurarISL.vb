Imports System.IO
Imports System.Xml
Imports System.Environment

Public Class FrmConfigurarISL

    Private Sub BtConfirmarISL_Click(sender As Object, e As EventArgs) Handles BtConfirmarISL.Click
        If TbISLGrupo.Text = "" Then
            MessageBox.Show("No se admiten campos vacíos", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        ElseIf TbISLNombre.Text = "" Then
            MessageBox.Show("No se admiten campos vacíos", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        Else
            'InstaladorKubo.Show()
            FrmNavegador.Show()
            Me.Close()
        End If
    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        TbISLGrupo.Text = ""
        TbISLNombre.Text = ""
        InstaladorKubo.Show()
        Me.Close()
    End Sub

    Private Sub FrmConfigurarISL_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' LeerXML()
        'TbISLNombre.Text = LeerXML()

        Try
            Dim profile As String = LeerXML()
            Dim longprofile As Integer = profile.Length
            Dim longusuario As Integer = profile.LastIndexOf(".")
            Dim usuario = profile.Remove(longusuario, longprofile - longusuario)
            Dim grupo = profile.Remove(0, longusuario + 1)
            TbISLGrupo.Text = grupo
            TbISLNombre.Text = usuario
            InstaladorKubo.RegistroInstalacion("ISL: Obtenido Grupo y Nombre del XML " & grupo & " - " & usuario & ".")
        Catch ex As Exception
            TbISLNombre.Text = LeerXML().ToString
            TbISLGrupo.Text = "UNIDATA"
            InstaladorKubo.RegistroInstalacion("ISL: Leído nombre de Usuario del Equipo. No se pudo obtener del XML.")
        End Try

    End Sub

    'TODO QUE SANDRA ME EXPLIQUE COMO HACER ESTO SIN FUNCION CON UN SUB Y UN BYVAL
    Private Function LeerXML()
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        Dim jnemoxml As String = appData & "\jNemo\jnemo.xml"

        If File.Exists(jnemoxml) Then
            Dim doc As New XmlDocument()
            doc.Load(jnemoxml)

            Dim ultimoprofile As String = ""

            '"/jnemo/profiles/profile" ' nos quedamos con el ultimo
            'TODO Hacer una copia del xml en un dir temporal
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
            InstaladorKubo.RegistroInstalacion("ISL: leído nodo username con valor " & ultimoprofile)
        Else
            'Obtener nombre del equipo
            Dim equipousuario As String = (My.User.Name)
            Dim equipo As Integer = equipousuario.LastIndexOf("\")
            Dim usuario = equipousuario.Remove(0, equipo + 1)
            Return usuario
            'TODO NO RECOGE NOMBRE DEL USUARIO
            InstaladorKubo.RegistroInstalacion("Se procede a leer el nombre de Usuario del equipo. Se obtiene " & usuario)
        End If
    End Function


End Class