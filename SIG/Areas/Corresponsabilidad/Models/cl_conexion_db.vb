Imports System.Data.SqlClient

Namespace SIG.Areas.Corresponsabilidad.Models
    Public Class cl_conexion_db
        Private strCadenaConexion As String
        Private objConexionDB As New SqlConnection
        Private objComando As New SqlCommand
        Private objProcedimineto As New SqlCommand
        Private strcomandoSQL As String
        Private intResultQuery As Integer
        Private intResultado As Integer
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

        Public Function fnc_ejecutar_simple_comando(ByVal query As String) As Integer
            Try
                objComando.CommandText = query
                objComando.Connection = objConexionDB
                objConexionDB.Open()
                intResultQuery = objComando.ExecuteNonQuery()
                objConexionDB.Close()
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
                objDataAdapter.SelectCommand.CommandTimeout = 172800
                objConexionDB.Close()
                objDataAdapter.Fill(objTabla)

            Catch ex As Exception
                objConexionDB.Close()
                Return objTabla
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


        Public Function fnc_p_corr_operaciones_corresponsabilidades(ByRef intTipOperacion As Integer,
                                                                    ByRef intCodUser As Integer,
                                                                    ByRef intCodDetCorr As Integer,
                                                                    ByRef intCodTipCorr As Integer,
                                                                    ByRef intNumDetCorr As Integer,
                                                                    ByRef intAño As Integer,
                                                                    ByRef intParIni As Integer,
                                                                    ByRef intParFin As Integer,
                                                                    ByRef fechIni As Date,
                                                                    ByRef fechFin As Date,
                                                                    ByRef strNombre As String) As Integer
            objProcedimineto.Connection = objConexionDB
            objProcedimineto.CommandTimeout = intSegundos
            objProcedimineto.CommandText = "p_corr_operaciones_corresponsabilidades"
            objProcedimineto.CommandType = CommandType.StoredProcedure
            objProcedimineto.Parameters.Clear()
            objProcedimineto.Parameters.AddWithValue("@intTipOperacion", intTipOperacion)
            objProcedimineto.Parameters.AddWithValue("@intCodUser", intCodUser)
            objProcedimineto.Parameters.AddWithValue("@intCodDetCorr", intCodDetCorr)
            objProcedimineto.Parameters.AddWithValue("@intCodTipCorr", intCodTipCorr)
            objProcedimineto.Parameters.AddWithValue("@intNumDetCorr", intNumDetCorr)
            objProcedimineto.Parameters.AddWithValue("@intAño", intAño)
            objProcedimineto.Parameters.AddWithValue("@intParIni", intParIni)
            objProcedimineto.Parameters.AddWithValue("@intParFin", intParFin)
            objProcedimineto.Parameters.AddWithValue("@fechIni", fechIni)
            objProcedimineto.Parameters.AddWithValue("@fechFin", fechFin)
            objProcedimineto.Parameters.AddWithValue("@strNombre", strNombre)

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
