' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.vb en el Explorador de soluciones e inicie la depuración.

Imports System.Data.Odbc
Imports AppGeneral
Imports AppUtils
Imports Compresion
Imports SQLIfxOdbcConnect


Public Class wsCloto
    Implements IwsCloto

    Private sCnn As String
    Private cConn As String
    Private Connection As OdbcConnection
    Private Command As OdbcCommand
    Private SqlDat As SQLIfxOdbcConnect.SQLOdbcOdbcConnect
    Private AppGen As AppGeneral.AppGeneral
    Private AppUtil As AppUtils.AppUtils
    Private nFormatoFecha As AppGeneral.FormatoFecha
    Private compresion
    Public Sub New()

        SqlDat = New SQLIfxOdbcConnect.SQLOdbcOdbcConnect
        '-------------------------------------------------
        sCnn = ConfigurationManager.ConnectionStrings("ApplicationServices_wsCloto").ConnectionString
        nFormatoFecha = FormatoFecha.iso_YYYYMMDD
        Connection = New OdbcConnection(sCnn)
        Command = New OdbcCommand

    End Sub

    Public Sub Close()
        Command.Dispose()
        Connection.Dispose()
    End Sub

    Public Function Get_citas_usuario() As String Implements IwsCloto.Get_citas_usuario

        Dim Retorno As String
        Dim cConn As String
        Dim rComprimir As Compresion.Compresion
        Dim ds As DataSet
        Dim dsf As DataSet

        Retorno = String.Empty
        SqlDat = New SQLIfxOdbcConnect.SQLOdbcOdbcConnect
        AppGen = New AppGeneral.AppGeneral
        rComprimir = New Compresion.Compresion
        ds = New DataSet
        dsf = New DataSet
        cConn = String.Empty

        cConn = "SELECT "
        cConn += " ti_agecompensar.citdoc, ti_agecompensar.citfue, ti_agecompensar.citead,"
        cConn += " citcod, citesp, espnom, citmed, mednom, fechaasignacion, cithor, cithis, pacnom, numero,"
        cConn += " pacap1, tidpaciente, idpaciente, ti_agecompensar.citest, fechadeseadaporelusuario, pyp, glneps, iddiagnostico,"
        cConn += " telefono, numidentificarcita, servicio, idprestador, idremitente, tiposerv, sesiones, glnips, causaexterna, glnpuntoatencion"
        cConn += " FROM ti_agecompensar, cncit, abpac, inesp, inmed"
        cConn += " WHERE"
        cConn += " ti_agecompensar.citdoc = cncit.citdoc"
        cConn += " And ti_agecompensar.citfue = cncit.citfue"
        cConn += " And ti_agecompensar.citead = cncit.citead"
        cConn += " And pachis = cithis"
        cConn += " And citesp = espcod"
        cConn += " And citmed = medcod"
        cConn += " And ti_agecompensar.citest Is NULL"

        Try
            ds = SqlDat.GetDataDb(sCnn, cConn, SQLIfxOdbcConnect.wsCargaTipo.wsNormal)
            dsf = ds
        Catch ex As Exception
            AppGen.RegistreEvento("Error Servicio wsCloto: Get_citas_usuario - " & ex.Message, TipoEvento.Ev_General, "Application")
        Finally
            ds.Dispose()
        End Try

        Try
            Retorno = rComprimir.ComprimirDataset(dsf)
        Catch ex As Exception
            AppGen.RegistreEvento("Error Servicio wsCloto: Get_citas_usuario (final)- " & ex.Message, TipoEvento.Ev_General, "Application")
        Finally
            dsf.Dispose()
        End Try

        Return Retorno

    End Function

    Public Function Set_citas_procesado(sCitdoc As String, sCitfue As String, sCitead As String, sObservacion As String, sNumero As String, sPrograma As String) As Boolean Implements IwsCloto.Set_citas_procesado

        Dim Retorno As Boolean
        Dim sCad As String
        Dim nEstado As Integer
        Dim nVal As Int32
        Dim sFormato As FormatoFecha

        Retorno = False
        nEstado = 0
        nVal = 0
        sCad = String.Empty
        cConn = String.Empty
        SqlDat = New SQLIfxOdbcConnect.SQLOdbcOdbcConnect
        AppGen = New AppGeneral.AppGeneral
        sFormato = ConfigurationManager.AppSettings("FormatoFecha")

        Try
            nVal = Val(sNumero)
        Catch ex As Exception
            nVal = 0
        Finally

        End Try


        If (nVal > 0) Then

            nEstado = 1
            sCad = sNumero

        Else

            nEstado = 2
            sCad = "NO"

        End If

        'grabar nuevo registro de paciente

        cConn = "UPDATE ti_agecompensar "
        cConn += "SET"
        cConn += " numero = '" & sCad & "', "
        cConn += " observaciones = '" & sObservacion & "', "
        cConn += " programa = '" & sPrograma & "', "
        cConn += " citest = '" & nEstado & "'"
        cConn += " WHERE"
        cConn += " citead = '" & sCitead & "'"
        cConn += " AND citfue = '" & sCitfue & "'"
        cConn += " AND citdoc = '" & sCitdoc & "'"

        Try
            Retorno = SqlDat.EjecutarSQL(sCnn, cConn, SQLIfxOdbcConnect.wsCargaTipo.wsNormal)
        Catch ex As Exception
            AppGen.RegistreEvento("Error Servicio wsCloto: Set_citas_procesado - " & ex.Message, TipoEvento.Ev_General, "Application")
            Retorno = False
        Finally

        End Try

        Return Retorno

    End Function

End Class
