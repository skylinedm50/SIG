Imports System.Data.SqlClient

Namespace SIG.Areas.Planilla.Models

    Public Class cl_conexion_db
        Private strCadenaConexion As String
        Private objConexionDB As New SqlConnection
        Private objComando As New SqlCommand
        Private objProcedimineto As New SqlCommand
        Private strcomandoSQL As String
        Private intFilasAfect As Integer
        Private intResultQuery As Integer
        Private intResultado As Integer
        Private objResultado As Object
        Private objValorSalida As SqlParameter
        Private objDataSet As DataSet
        Private objDataAdapter As SqlDataAdapter
        Private objTabla As DataTable
        Private objDataReader As SqlDataReader
        Private objParametros As Cl_ParamConexion
        Private intSegundos As Integer = 172800 'La cantidad de segundos corresponde a 2 días en ejecución.

        Public Sub New()
            objParametros = New Cl_ParamConexion()
            Cl_ReadFile.Fnc_ReadFileConexion()
            strCadenaConexion = String.Format("Data Source ={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};", objParametros.source, objParametros.db, objParametros.name, objParametros.pwd)
            objConexionDB.ConnectionString = strCadenaConexion
        End Sub

        Public Function fnc_ejecutar_simple_comando(ByVal query As String)
            Try
                objComando.CommandText = query
                objComando.Connection = objConexionDB
                objConexionDB.Open()
                intFilasAfect = objComando.ExecuteNonQuery()
                objConexionDB.Close()

                If intFilasAfect > 0 Then
                    intResultQuery = 1
                Else
                    intResultQuery = 0
                End If
            Catch ex As Exception
                intResultQuery = 0
            End Try

            Return intResultQuery
        End Function

        Public Function fnc_crear_datatable(ByVal strQuery As String) As DataTable
            Try
                objTabla = New DataTable
                objConexionDB.Open()
                objDataAdapter = New SqlDataAdapter(strQuery, objConexionDB)
                objDataAdapter.SelectCommand.CommandTimeout = 5000
                objConexionDB.Close()
                objDataAdapter.Fill(objTabla)

            Catch ex As Exception
            End Try
            Return objTabla
        End Function

        Public Function fnc_crear_arreglo_regis_unico(ByVal strQuery As String, ByVal intCantRegis As Integer) As Array 'Funcion para crear un arreglo aprtir de un consulta, que solo traera un registro o fila.
            Dim arrValores(intCantRegis - 1) As String  'Arreglo tipo string, se indica el indice del arreglo se le resta si se usa un intCantRegis = 2 el tamaño del arreglo seria 3 por que el arreglo empiza en 0.
            ' y lo que requiero es de tamaño 2 entonces le resto 1.
            Try
                objConexionDB.Open()
                objComando = New SqlCommand(strQuery, objConexionDB)
                objDataReader = objComando.ExecuteReader()
                While objDataReader.Read()
                    For i = 0 To objDataReader.FieldCount - 1
                        arrValores(i) = objDataReader(i)
                    Next
                End While

                objConexionDB.Close()
            Catch ex As Exception
                objConexionDB.Close()
            End Try

            Return arrValores
        End Function

        Public Function fnc_verificar_existe_registros(ByVal strQuery As String) As Boolean 'Funcion para verificar si existen registros determinadolos por una consulta, el valor devuelto en caso de existencia es 1.
            Try
                objConexionDB.Open()
                objComando = New SqlCommand(strQuery, objConexionDB)
                objDataReader = objComando.ExecuteReader()

                If objDataReader.Read Then
                    objConexionDB.Close()
                    Return True
                Else
                    objConexionDB.Close()
                    Return False
                End If


            Catch ex As Exception
                objConexionDB.Close()
                Return False
            End Try
        End Function

        Public Function fnc_obtener_scalar(ByVal strQuery As String) As Object
            Try
                objComando.CommandText = strQuery

                objComando.Connection = objConexionDB
                objComando.CommandTimeout = 864000
                objConexionDB.Open()
                objResultado = objComando.ExecuteScalar
                objConexionDB.Close()
            Catch ex As Exception
                Return -1
            End Try

            Return objResultado
        End Function

        Public Function fnc_ejecutar_procedimiento(ByVal comand As SqlCommand) As Object
            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter()
                Dim ds As New DataSet

                comand.Connection = objConexionDB
                da.SelectCommand = comand
                da.Fill(ds)

                objConexionDB.Close()
                Return ds

            Catch ex As Exception
                objConexionDB.Close()
                Return Nothing
            End Try
        End Function

        Public Function fnc_p_pla_eliminar_esquema(ByVal intCodEsquema As Integer, ByVal intCodUser As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_eliminar_esquema"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intCodUser", intCodUser.ToString)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_actualizar_esquema(ByVal intCodEsquema As Integer, ByVal intCodPago As Integer, ByVal strNombreEsq As String, _
                                                     ByVal strCenso As String, ByVal strFechaIni As String, ByVal strFechaFin As String, _
                                                     ByVal intMeses As Integer, ByVal intTipInterva As Integer, ByVal strTipoPago As String, _
                                                     ByVal strFechaElegibi As String, ByVal intAño As Integer, ByVal intCantBono As Integer, _
                                                     ByVal strDetMeses As String, ByVal intTipEsquema As Integer, ByVal strObser As String, _
                                                     ByVal intNumEsque As Integer, ByVal intCodUser As Integer, ByVal intCodCofMonto As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_actualizar_esquema"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intCodPago", intCodPago)
            objProcedimineto.Parameters.AddWithValue("@strNombreEsq", strNombreEsq)
            objProcedimineto.Parameters.AddWithValue("@strCenso", strCenso)
            objProcedimineto.Parameters.AddWithValue("@strFechaFin", strFechaFin)
            objProcedimineto.Parameters.AddWithValue("@strFechaIni", strFechaIni)
            objProcedimineto.Parameters.AddWithValue("@intMeses", intMeses)
            objProcedimineto.Parameters.AddWithValue("@intTipInterva", intTipInterva)
            objProcedimineto.Parameters.AddWithValue("@strTipoPago", strTipoPago)
            objProcedimineto.Parameters.AddWithValue("@strFechaElegibi", strFechaElegibi)
            objProcedimineto.Parameters.AddWithValue("@intAño", intAño)
            objProcedimineto.Parameters.AddWithValue("@intCantBono", intCantBono)
            objProcedimineto.Parameters.AddWithValue("@strDetMeses", strDetMeses)
            objProcedimineto.Parameters.AddWithValue("@intTipEsquema", intTipEsquema)
            objProcedimineto.Parameters.AddWithValue("@strObser", strObser)
            objProcedimineto.Parameters.AddWithValue("@intNumEsque", intNumEsque)
            objProcedimineto.Parameters.AddWithValue("@intCodUser", intCodUser)
            objProcedimineto.Parameters.AddWithValue("@intCodCofMon", intCodCofMonto)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_nuevo_esquema(ByVal intCodPago As Integer, ByVal strNombreEsq As String, _
                                                ByVal strCenso As String, ByVal strFechaIni As String, ByVal strFechaFin As String, _
                                                ByVal intMeses As Integer, ByVal intTipInterva As Integer, ByVal strTipoPago As String, _
                                                ByVal strFechaElegibi As String, ByVal intAño As Integer, ByVal intCantBono As Integer, _
                                                ByVal strDetMeses As String, ByVal intTipEsquema As Integer, ByVal strObser As String, _
                                                ByVal intCodUser As Integer, ByVal intCodCofMonto As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_nuevo_esquema"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodPago", intCodPago)
            objProcedimineto.Parameters.AddWithValue("@strNombreEsq", strNombreEsq)
            objProcedimineto.Parameters.AddWithValue("@strCenso", strCenso)
            objProcedimineto.Parameters.AddWithValue("@strFechaFin", strFechaFin)
            objProcedimineto.Parameters.AddWithValue("@strFechaIni", strFechaIni)
            objProcedimineto.Parameters.AddWithValue("@intMeses", intMeses)
            objProcedimineto.Parameters.AddWithValue("@intTipInterva", intTipInterva)
            objProcedimineto.Parameters.AddWithValue("@strTipoPago", strTipoPago)
            objProcedimineto.Parameters.AddWithValue("@strFechaElegibi", strFechaElegibi)
            objProcedimineto.Parameters.AddWithValue("@intAño", intAño)
            objProcedimineto.Parameters.AddWithValue("@intCantBono", intCantBono)
            objProcedimineto.Parameters.AddWithValue("@strDetMeses", strDetMeses)
            objProcedimineto.Parameters.AddWithValue("@intTipEsquema", intTipEsquema)
            objProcedimineto.Parameters.AddWithValue("@strObser", strObser)
            objProcedimineto.Parameters.AddWithValue("@intCodUser", intCodUser)
            objProcedimineto.Parameters.AddWithValue("@intCodCofMon", intCodCofMonto)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function


        Public Function fnc_p_pla_nueva_cobertura_esquema(ByVal intCodEsq As Integer, _
                                                          ByVal strCarga As String, _
                                                          ByVal strHaSalido As String, _
                                                          ByVal strNoHaSalido As String, _
                                                          ByVal intTipoUbica As Integer, _
                                                          ByVal strCodUbica As String, _
                                                          ByVal strSigno As String, _
                                                          ByVal strDescripcion As String, _
                                                          ByVal intCodUsuario As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_nueva_cobertura_esquema"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodEsq", intCodEsq)
            objProcedimineto.Parameters.AddWithValue("@strCarga", strCarga)
            objProcedimineto.Parameters.AddWithValue("@strHaSalido", strHaSalido)
            objProcedimineto.Parameters.AddWithValue("@strNoHaSalido", strNoHaSalido)
            objProcedimineto.Parameters.AddWithValue("@intTipoUbica", intTipoUbica)
            objProcedimineto.Parameters.AddWithValue("@strCodUbica", strCodUbica)
            objProcedimineto.Parameters.AddWithValue("@strSigno", strSigno)
            objProcedimineto.Parameters.AddWithValue("@strDescripcion", strDescripcion)
            objProcedimineto.Parameters.AddWithValue("intCodUsuario", intCodUsuario)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_enlazar_esquemas(ByVal strCodEsquemas As String, ByVal intCodUsuari As Integer, ByVal intCodTipoEnlace As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_enlazar_esquemas"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@strCodEsquemas", strCodEsquemas)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuari)
            objProcedimineto.Parameters.AddWithValue("@intCodTipoEnlace", intCodTipoEnlace)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_desenlazar_esquemas(ByVal strCodEsquemas As String, _
                                                      ByVal intCodPrincipal As Integer, _
                                                      ByVal intCodUsuario As Integer, _
                                                      ByVal intTipoOperacion As Integer, _
                                                      ByVal intCodDesenlace As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_desenlazar_esquemas"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@strCodEsquemas", strCodEsquemas)
            objProcedimineto.Parameters.AddWithValue("@intCodPrincipal", intCodPrincipal)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@intTipoOperacion", intTipoOperacion)
            objProcedimineto.Parameters.AddWithValue("@intCodDesenlace", intCodDesenlace)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_borrar_cobertura(ByVal intCodCober As Integer, ByVal intCodUsuario As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_borrar_cobertura_esquema"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodCober", intCodCober)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_nuevo_establecimiento_fondo(ByVal intCodEsquema As Integer, _
                                                              ByVal intCodFondo As Integer, _
                                                              ByVal intTipoLocalizacion As String, _
                                                              ByVal strCodLocalizacion As String, _
                                                              ByVal strSigno As String, _
                                                              ByVal strDescripcion As String, _
                                                              ByVal intCodUsuario As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_nuevo_establecimiento_fondo"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intCodFondo", intCodFondo)
            objProcedimineto.Parameters.AddWithValue("@intTipoLocalizacion", intTipoLocalizacion)
            objProcedimineto.Parameters.AddWithValue("@strCodLocalizacion", strCodLocalizacion)
            objProcedimineto.Parameters.AddWithValue("@strSigno", strSigno)
            objProcedimineto.Parameters.AddWithValue("@strDescripcion", strDescripcion)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_borrar_establecimiento_fondo(ByVal intCodEstableFondo As Integer, ByVal intCodUsuario As Integer, ByRef strDescripcion As String) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_borrar_establecimiento_fondo"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodEstableFondo", intCodEstableFondo)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@strDescripcion", strDescripcion)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_borrar_mecanis_pago(ByVal intCodMecaniPago As Integer, ByVal intCodUsuario As Integer, ByRef strDescripcion As String) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_borrar_mecanis_pago"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodMecaniPago", intCodMecaniPago)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@strDescripcion", strDescripcion)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_nuevo_mecanismo_pagador(ByVal intCodEsquema As Integer, _
                                                              ByVal intCodPagador As Integer, _
                                                              ByVal intTipoLocalizacion As String, _
                                                              ByVal strCodLocalizacion As String, _
                                                              ByVal strSigno As String, _
                                                              ByVal strDescripcion As String, _
                                                              ByVal intCodUsuario As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_nuevo_mecanismo_pagador"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intCodPagador", intCodPagador)
            objProcedimineto.Parameters.AddWithValue("@intTipoLocalizacion", intTipoLocalizacion)
            objProcedimineto.Parameters.AddWithValue("@strCodLocalizacion", strCodLocalizacion)
            objProcedimineto.Parameters.AddWithValue("@strSigno", strSigno)
            objProcedimineto.Parameters.AddWithValue("@strDescripcion", strDescripcion)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function


        Public Function fnc_p_pla_nuevo_bono(ByVal intCodEsquema As Integer, _
                                            ByVal intNum As Integer, _
                                            ByVal intNumeInterval As Integer, _
                                            ByVal intTipoInter As Integer, _
                                            ByVal intYear As Integer, _
                                            ByVal dateFecIni As String, _
                                            ByVal dateFecFin As String, _
                                            ByVal dateFecElegibi As String, _
                                            ByVal strDetMeses As String, _
                                            ByVal intCantMeses As Integer, _
                                            ByVal intCodUsuario As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_nuevo_bono"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intNum", intNum)
            objProcedimineto.Parameters.AddWithValue("@intNumeInterval", intNumeInterval)
            objProcedimineto.Parameters.AddWithValue("@intTipoInter", intTipoInter)
            objProcedimineto.Parameters.AddWithValue("@intYear", intYear)
            objProcedimineto.Parameters.AddWithValue("@dateFecIni", dateFecIni)
            objProcedimineto.Parameters.AddWithValue("@dateFecFin", dateFecFin)
            objProcedimineto.Parameters.AddWithValue("@dateFecElegibi", dateFecElegibi)
            objProcedimineto.Parameters.AddWithValue("@strDetMeses", strDetMeses)
            objProcedimineto.Parameters.AddWithValue("@intCantMeses", intCantMeses)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_borrar_bono(ByVal intCodBono As Integer, ByVal intCodUsuario As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_borrar_bono"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodBono", intCodBono)
            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function



        Public Function fnc_p_pla_nuevo_bono_corresp(ByVal intCodUsuario As Integer, ByVal intCodTransfer As Integer, ByVal intCodCorresp As Integer, ByVal intCodComponente As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_nuevo_bono_corresp"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@intCodTransfer", intCodTransfer)
            objProcedimineto.Parameters.AddWithValue("@intCodCorresp", intCodCorresp)
            objProcedimineto.Parameters.AddWithValue("@intCodComponente", intCodComponente)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_nuevo_bono_corresp_aperc(ByVal intCodUsuario As Integer, ByVal intCodTransfer As Integer, ByVal intCodCorresp As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_nuevo_bono_corresp_aperc"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@intCodTransfer", intCodTransfer)
            objProcedimineto.Parameters.AddWithValue("@intCodCorresp", intCodCorresp)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_modificar_bono_corresp(ByVal intCodUsuario As Integer, ByVal intCodBono As Integer, ByVal intNumOrden As Integer, ByVal intTipoOpera As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_modificar_bono_corresp"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@intCodBono", intCodBono)
            objProcedimineto.Parameters.AddWithValue("@intNumOrden", intNumOrden)
            objProcedimineto.Parameters.AddWithValue("@intTipoOpera", intTipoOpera)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_modificar_bono_corresp_aperc(ByVal intCodUsuario As Integer, ByVal intCodBono As Integer, ByVal intNumOrden As Integer, ByVal intTipoOpera As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_modificar_bono_corresp_aperc"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@intCodBono", intCodBono)
            objProcedimineto.Parameters.AddWithValue("@intNumOrden", intNumOrden)
            objProcedimineto.Parameters.AddWithValue("@intTipoOpera", intTipoOpera)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function


        Public Function fnc_p_pla_operaciones_transferencia_acumulada(ByVal intCodUsuario As Integer, ByVal intCodEsqPadre As Integer, ByVal intCodEsqHaAcum As Integer, ByVal intTipoOperacion As Integer, ByVal intCodBonNoCobro As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_operaciones_transferencia_acumulada"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@intCodEsqPadre", intCodEsqPadre)
            objProcedimineto.Parameters.AddWithValue("@intCodEsqHaAcum", intCodEsqHaAcum)
            objProcedimineto.Parameters.AddWithValue("@intTipoOperacion", intTipoOperacion)
            objProcedimineto.Parameters.AddWithValue("@intCodBonNoCobro", intCodBonNoCobro)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_operaciones_otra_verificacion(ByVal intCodUsuario As Integer, ByVal intCodEsquema As Integer, ByVal intCodVerificacion As Integer, ByVal intTipoOperacion As Integer, ByVal intCodVeriEsque As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_operaciones_otra_verificacion"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodUsuario", intCodUsuario)
            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intCodVerificacion", intCodVerificacion)
            objProcedimineto.Parameters.AddWithValue("@intTipoOperacion", intTipoOperacion)
            objProcedimineto.Parameters.AddWithValue("@intCodVeriEsque", intCodVeriEsque)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_desaprobar_esquema_cambio(ByRef intCodEsquema As Integer, ByRef intCodUser As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_desaprobar_esquema_cambio"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intCodUser", intCodUser)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function

        Public Function fnc_p_pla_borrar_hijos_esquema(ByRef intCodEsquema As Integer, ByRef intCodUser As Integer) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_pla_borrar_hijos_esquema"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()

            objProcedimineto.Parameters.AddWithValue("@intCodEsquema", intCodEsquema)
            objProcedimineto.Parameters.AddWithValue("@intCodUser", intCodUser)

            objValorSalida = New SqlParameter("@intRespu", Data.SqlDbType.Int)

            objValorSalida.Direction = Data.ParameterDirection.Output
            objProcedimineto.Parameters.Add(objValorSalida)

            objConexionDB.Open()
            objProcedimineto.ExecuteScalar()
            intResultado = CInt(objValorSalida.Value)
            objConexionDB.Close()

            Return intResultado
        End Function


    End Class

End Namespace
