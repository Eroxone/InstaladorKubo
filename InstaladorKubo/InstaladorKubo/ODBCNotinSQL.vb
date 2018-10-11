Module ODBCNotinSQL

    Public Sub NotinSQL(ByVal consultasql As String)
        '-------------------Primero Dleclaramos la coneccion recuerda cambiar los datos NombreDeBase, Usuario y Password 
        Dim cnn As New Odbc.OdbcConnection("Dsn=NotinSQL;uid=sa;pwd=03071997")

        '----------------Se abre la coneccion 
        cnn.Open()

        '----------------Escribimos la sentencia que se ejecutara 
        'consultasql = " SELECT * FROM DBO.DATOS GENERALES WHERE SEXO <> J"

        '----------------Declaramos el ejecutor y el contenedor 
        Dim rs As New Odbc.OdbcCommand(consultasql, cnn)
        Dim Tabla As Odbc.OdbcDataReader

        '---------------Se ejecuta y se asigna el resultado 
        Tabla = rs.ExecuteReader

        '---------------En este caso sabemos q solo nos regresara un registro asi q lo leemeos, en caso de que sean varios habria que hacerlos 
        '---------------con un ciclo 
        Tabla.Read()
        '---------------Asignamos el campo a un objeto o variable 
        Dim ContadorLineas = Tabla("Menu")
        '---------------Cerramos todo 
        Tabla.Close()
        cnn.Close()


    End Sub



End Module
