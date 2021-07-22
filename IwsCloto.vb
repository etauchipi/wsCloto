' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IwsCloto

	'-------------------------------------------
	' GET's

	<OperationContract()>
	Function Get_citas_usuario() As String

	<OperationContract()>
	Function Set_citas_procesado(sCitdoc As String, sCitfue As String, sCitead As String, sObservacion As String, sNumero As String, sPrograma As String) As Boolean


	' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.

<DataContract()>
Public Class ws_CAutServiciosSalud
	<DataMember()>
	Public Property Encabezado() As ws_CAutServiciosSaludDetalle
End Class

<DataContract()>
Public Class ws_DsAutorizacion
	<DataMember()>
	Public Property Encabezado() As ws_DsAutorizacionDetalle
End Class

<DataContract()>
Public Class ws_CAutServiciosSaludDetalle
	<DataMember()>
	Public Property GlnIPS() As String
	<DataMember()>
	Public Property GlnEPS() As String
	<DataMember()>
	Public Property GlnPuntoAtencion() As String
	<DataMember()>
	Public Property GlnUsuario() As String
	<DataMember()>
	Public Property Fecha() As String
	<DataMember()>
	Public Property Numero() As String
	<DataMember()>
	Public Property TIDPaciente() As String
	<DataMember()>
	Public Property IDPaciente() As String
	<DataMember()>
	Public Property IDDiagnostico() As String
	<DataMember()>
	Public Property TipoServ() As String
	<DataMember()>
	Public Property Servicio() As String
	<DataMember()>
	Public Property CausaExterna() As String
	<DataMember()>
	Public Property IDRemitente() As String
	<DataMember()>
	Public Property Programa() As String
	<DataMember()>
	Public Property Sesiones() As String
	<DataMember()>
	Public Property NumIdentificarCita() As String
	<DataMember()>
	Public Property FechaDeseadaPorElUsuario() As String
	<DataMember()>
	Public Property IdPrestador() As String
	<DataMember()>
	Public Property PYP() As String
End Class

<DataContract()>
Public Class ws_DsAutorizacionDetalle
	<DataMember()>
	Public Property GlnEPS() As String
	<DataMember()>
	Public Property GlnIPS() As String
	<DataMember()>
	Public Property Numero() As String
	<DataMember()>
	Public Property NumeroAutorizacion() As String
	<DataMember()>
	Public Property TIDPaciente() As String
	<DataMember()>
	Public Property IDPaciente() As String
	<DataMember()>
	Public Property Nombre() As String
	<DataMember()>
	Public Property EstadoAF() As String
	<DataMember()>
	Public Property Plan() As String
	<DataMember()>
	Public Property Cobertura() As String
	<DataMember()>
	Public Property Estrato() As String
	<DataMember()>
	Public Property SemCotizadas() As String
	<DataMember()>
	Public Property FechaNacimiento() As String
	<DataMember()>
	Public Property Edad() As String
	<DataMember()>
	Public Property Sesiones() As String
	<DataMember()>
	Public Property ServiciosNoAut() As String
	<DataMember()>
	Public Property ValorCM() As String
	<DataMember()>
	Public Property ValorCop() As String
	<DataMember()>
	Public Property Parentesco() As String
	<DataMember()>
	Public Property Servicio() As String
	<DataMember()>
	Public Property CentroCosto() As String
	<DataMember()>
	Public Property Valor() As String
	<DataMember()>
	Public Property SedeCapita() As String
	<DataMember()>
	Public Property NumActaRecobro() As String
	<DataMember()>
	Public Property NumCasoRecobro() As String
	<DataMember()>
	Public Property TipoValor() As String
End Class
