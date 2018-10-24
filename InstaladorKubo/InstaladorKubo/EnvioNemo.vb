Public Class EnvioNemo

    Public Shared Sub envionemo(ByVal cuentanemo As String, ByVal mensaje As String)
        ObtenerEjecutables.curl()

        If cuentanemo = "@notin.net" OrElse cuentanemo = Nothing Then
            Exit Sub
        End If

        Dim autenticacioncurl As String = """" & "auth=cf4a61cadd12a624539ebc1566ff82d2&"
        Dim pluginopenfire As String = "https://openfire.notin.net/plugins/httpbroadcast/send"

        Shell("cmd /k " & FormInstaladorKubo.RutaDescargas & "curl.exe -XPOST -k -d " & autenticacioncurl & "txt=" & mensaje & "&from=instalador@notin.net&recps[]=" & cuentanemo & """" & " " & pluginopenfire, AppWinStyle.Hide, False)
        FormInstaladorKubo.RegistroInstalacion("Envio Nemo a: " & cuentanemo)
    End Sub



    '   curl -XPOST -k -d "auth=cf4a61cadd12a624539ebc1566ff82d2&txt=test+prueba+prueba&from=instalador@notin.net&recps[]=juanjo@notin.net" https//openfire.notin.net/plugins/httpbroadcast/send

End Class
