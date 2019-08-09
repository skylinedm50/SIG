Namespace SIG.Areas.Planilla.Models
    Public Class cl_cobertura_planilla
        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private objManejoDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array

        Function fnc_obtener_esquemas() As DataTable
            strQuery = "SELECT   " & _
                        "   pag_codigo, " & _
                        "	esq_codigo,  " & _
                        "	nombre_esquema " & _
                        "FROM  " & _
                        "	SIG_T.dbo.f_pla_esquema  " & _
                        "ORDER BY  " & _
                        "   esq_codigo ASC"
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_cargas() As DataTable
            strQuery = "SELECT  " & _
                        "	car_codigo, " & _
                        "	car_nombre " & _
                        "FROM " & _
                        "	SIG_T.dbo.f_pla_cargas "
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_cobertura_planilla(ByVal strCodEsquemas As String) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	esqc_codigo, " & _
                                    "	CASE WHEN departamento = '' THEN 'TODO' ELSE departamento END AS departamento, " & _
                                    "	CASE WHEN municipio = '' THEN 'TODO' ELSE municipio END AS municipio, " & _
                                    "	CASE WHEN aldea = '' THEN 'TODO' ELSE aldea END AS aldea, " & _
                                    "	CASE WHEN caserio = '' THEN 'TODO' ELSE caserio END AS caserio, " & _
                                    "	esqc_planillas_no, " & _
                                    "	esqc_planillas_si, " & _
                                    "	esqc_cargas_si, " & _
                                    "   esqc_tipo_localizacion, " & _
                                    "   desc_cambio, " & _
                                    "	esqc_signo " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_plan_esquema_cobertura_localizacion " & _
                                    "WHERE " & _
                                    "	esq_codigo = {0} ", strCodEsquemas)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_esquemas_enlazados(ByVal intCodEsquema As Integer) As Array
            strQuery = String.Format("SELECT  " & _
                                    "    esq_codigos  " & _
                                    "FROM  " & _
                                    "    SIG_T.dbo.f_pla_esquemas_cobertura_enlazados " & _
                                    "WHERE  " & _
                                    "    (esq_codigos LIKE '% {0} %'  " & _
                                    "    OR esq_codigos LIKE '% {0}'  " & _
                                    "    OR esq_codigos LIKE '{0} %') " & _
                                    "    AND cod_estado = 1 ", intCodEsquema)
            Return objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)
        End Function

        Function fnc_existe_localizacion(ByVal intCodEsq As Integer, ByVal strCodLocalizacion As String) As Boolean
            strQuery = String.Format("SELECT  " & _
                                    "    TOP 1 esqc_codigo " & _
                                    "FROM  " & _
                                    "    SIG_T.dbo.f_pla_esquema_cobertura " & _
                                    "WHERE  " & _
                                    "    esq_codigo = {0} " & _
                                    "    AND esqc_localizacion = '{1}' ", intCodEsq, strCodLocalizacion)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)
            If arrObjeto(0) Is Nothing Then
                Return False 'No existe la localización para el esquema.
            Else
                Return True 'Si existe la localización para el esquema.
            End If
        End Function

        Function fnc_guardar_localizacion(ByVal arrEsquemas() As String, _
                                          ByVal strCarga As String, _
                                          ByVal strHaSalido As String, _
                                          ByVal strNoHaSalido As String, _
                                          ByVal strSigno As String, _
                                          ByVal arrDepar As Array, _
                                          ByVal arrMun As Array, _
                                          ByVal arrAld As Array, _
                                          ByVal arrCase As Array, _
                                          ByVal strDescripcion As String, _
                                          ByVal intCodUsuario As Integer, _
                                          ByVal bolYaSeEncuEnBD As Boolean) As Integer
            Try
                Dim bolExisteLocaliAlma As Boolean = False
                Dim intRespuestaDB As Integer = 1
                Dim boolEsquemaEjecuta As Boolean = False

                For i = 0 To arrEsquemas.Length - 1
                    strQuery = String.Format("SELECT  " & _
                                        "        TOP 1 1 " & _
                                        "FROM  " & _
                                        "    SIG_T.dbo.f_pla_titulares  " & _
                                        "WHERE  " & _
                                        "    tit_esquema =  {0} ", arrEsquemas(i))
                    arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)
                Next

                If arrObjeto(0) IsNot Nothing Then
                    boolEsquemaEjecuta = True
                End If


                If boolEsquemaEjecuta = False Then
                    'Identificamos si existen código repetidos en ubicaciones geograficas.
                    For i = 0 To arrEsquemas.Length - 1
                        If arrDepar(0) <> "00" And intRespuestaDB = 1 Then
                            For j = 0 To arrDepar.Length - 1
                                If fnc_existe_localizacion(arrEsquemas(i), arrDepar(j)) = True Then
                                    bolExisteLocaliAlma = True
                                Else
                                    intRespuestaDB = objConexionDB.fnc_p_pla_nueva_cobertura_esquema(arrEsquemas(i), strCarga, strHaSalido, strNoHaSalido, 1, arrDepar(j), strSigno, strDescripcion, intCodUsuario)
                                    intRespuestaDB = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(arrEsquemas(i), intCodUsuario) 'Si la el esquema fue aprobado desaprobarlo por algún cambio.
                                End If
                            Next
                        End If

                        If arrMun(0) <> "0000" And intRespuestaDB = 1 Then
                            For j = 0 To arrMun.Length - 1
                                If fnc_existe_localizacion(arrEsquemas(i), arrMun(j)) = True Then
                                    bolExisteLocaliAlma = True
                                Else
                                    intRespuestaDB = objConexionDB.fnc_p_pla_nueva_cobertura_esquema(arrEsquemas(i), strCarga, strHaSalido, strNoHaSalido, 2, arrMun(j), strSigno, strDescripcion, intCodUsuario)
                                    intRespuestaDB = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(arrEsquemas(i), intCodUsuario)
                                End If
                            Next
                        End If

                        If arrAld(0) <> "000000" And intRespuestaDB = 1 Then
                            For j = 0 To arrAld.Length - 1
                                If fnc_existe_localizacion(arrEsquemas(i), arrAld(j)) = True Then
                                    bolExisteLocaliAlma = True
                                Else
                                    intRespuestaDB = objConexionDB.fnc_p_pla_nueva_cobertura_esquema(arrEsquemas(i), strCarga, strHaSalido, strNoHaSalido, 3, arrAld(j), strSigno, strDescripcion, intCodUsuario)
                                    intRespuestaDB = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(arrEsquemas(i), intCodUsuario)
                                End If
                            Next
                        End If

                        If arrCase(0) <> "000000000" And intRespuestaDB = 1 Then
                            For j = 0 To arrCase.Length - 1
                                If fnc_existe_localizacion(arrEsquemas(i), arrCase(j)) = True Then
                                    bolExisteLocaliAlma = True
                                Else
                                    intRespuestaDB = objConexionDB.fnc_p_pla_nueva_cobertura_esquema(arrEsquemas(i), strCarga, strHaSalido, strNoHaSalido, 4, arrCase(j), strSigno, strDescripcion, intCodUsuario)
                                    intRespuestaDB = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(arrEsquemas(i), intCodUsuario)
                                End If
                            Next
                        End If
                    Next

                    If arrEsquemas.Length > 1 And bolYaSeEncuEnBD = False And intRespuestaDB = 1 Then 'Significa que son esquemas que estan enlazados y son nuevos.
                        Dim strCodigos As String = String.Join(" ", arrEsquemas)
                        intRespuestaDB = objConexionDB.fnc_p_pla_enlazar_esquemas(strCodigos, intCodUsuario, 1)
                    End If
                    If bolExisteLocaliAlma = True And intRespuestaDB = 1 Then
                        Return 7 'Se almaceno la cobertura pero habian configuraciones repetidas.
                    Else
                        Return intRespuestaDB
                    End If

                Else
                    Return 6 'El esquema ya fue ejecutado y no se ingreso una descripción de cambio.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_existe_cobertura_esquema(ByVal intCodEsquema As Integer) As Integer
            strQuery = String.Format("SELECT  " & _
                                    "    TOP 1 esqc_codigo " & _
                                    "FROM  " & _
                                    "    SIG_T.dbo.f_pla_esquema_cobertura " & _
                                    "WHERE  " & _
                                    "    esq_codigo = {0}", intCodEsquema)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)
            If arrObjeto(0) Is Nothing Then
                Return 0 'No existe cobertura del esquema.
            Else
                Return 1 'Si existe cobertura de esquema.
            End If
        End Function

        Function fnc_desenlazar_cobertura_esquema(ByVal intCodEsquema As Integer, ByVal intCodUsuario As Integer) As Integer
            Dim arrEsquemasDB() As String
            Dim objListCodEsqFinal As New ArrayList

            strQuery = String.Format("SELECT  " & _
                                        "        TOP 1 1 " & _
                                        "FROM  " & _
                                        "    SIG_T.dbo.f_pla_titulares  " & _
                                        "WHERE  " & _
                                        "    tit_esquema =  {0} ", intCodEsquema)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) Is Nothing Then
                strQuery = String.Format("SELECT  " & _
                                        "    esq_codigos,  " & _
                                        "    cod_cobertura_enlazada " & _
                                        "FROM  " & _
                                        "    SIG_T.dbo.f_pla_esquemas_cobertura_enlazados " & _
                                        "WHERE  " & _
                                        "    (esq_codigos LIKE '% {0} %'  " & _
                                        "    OR esq_codigos LIKE '% {0}'  " & _
                                        "    OR esq_codigos LIKE '{0} %') " & _
                                        "    AND cod_estado = 1 ", intCodEsquema)
                arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 2)


                If arrObjeto(0) IsNot Nothing Then
                    arrEsquemasDB = Split(arrObjeto(0), " ")

                    If arrEsquemasDB.Length > 2 Then
                        For i = 0 To arrEsquemasDB.Length - 1
                            If arrEsquemasDB(i) <> intCodEsquema Then
                                objListCodEsqFinal.Add(arrEsquemasDB(i))
                            End If
                        Next

                        Dim arrCodEsqFinal() As Object = objListCodEsqFinal.ToArray()

                        Return objConexionDB.fnc_p_pla_desenlazar_esquemas(String.Join(" ", arrCodEsqFinal), arrObjeto(1), intCodUsuario, 1, 1)
                    Else
                        Return objConexionDB.fnc_p_pla_desenlazar_esquemas(arrObjeto(0), arrObjeto(1), intCodUsuario, 0, 1)
                    End If
                Else
                    Return 1
                End If
            Else
                Return 5
            End If
        End Function

        Function fnc_borrar_cobertura(ByVal intCodCobertura As Integer, ByVal intCodUsuario As Integer) As Integer
            Dim arrEsquemasDB() As String
            Dim arrCodCobertura() As String
            Dim bolRespuesta As Boolean = True
            Dim arrYaSeEjecuto() As String
            Dim arrEsquemas() As String

            strQuery = String.Format("SELECT  " & _
                                    "    esq_codigo, " & _
                                    "    esqc_localizacion, " & _
                                    "    esqc_tipo_localizacion " & _
                                    "FROM " & _
                                    "    SIG_T.dbo.f_pla_esquema_cobertura " & _
                                    "WHERE " & _
                                    "    esqc_codigo = {0} ", intCodCobertura)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 3)


            strQuery = String.Format("SELECT  " & _
                                        "        TOP 1 1 " & _
                                        "FROM  " & _
                                        "    SIG_T.dbo.f_pla_titulares  " & _
                                        "WHERE  " & _
                                        "    tit_esquema =  {0} ", arrObjeto(0))
            arrYaSeEjecuto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrYaSeEjecuto(0) Is Nothing Then
                strQuery = String.Format("SELECT  " & _
                                        "    esq_codigos  " & _
                                        "FROM  " & _
                                        "    SIG_T.dbo.f_pla_esquemas_cobertura_enlazados " & _
                                        "WHERE  " & _
                                        "    (esq_codigos LIKE '% {0} %'  " & _
                                        "    OR esq_codigos LIKE '% {0}'  " & _
                                        "    OR esq_codigos LIKE '{0} %') " & _
                                        "    AND cod_estado = 1 ", arrObjeto(0))

                arrEsquemasDB = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

                If arrEsquemasDB(0) IsNot Nothing Then 'Cuando la cobertura es compartida por otro esquema.
                    arrEsquemas = Split(arrEsquemasDB(0), " ")

                    For i = 0 To arrEsquemas.Length - 1
                        If bolRespuesta = True Then 'Verifica que no existe ningun error.
                            strQuery = String.Format("SELECT  " & _
                                                    "    esqc_codigo " & _
                                                    "FROM " & _
                                                    "    SIG_T.dbo.f_pla_esquema_cobertura " & _
                                                    "WHERE " & _
                                                    "    esq_codigo = {0} " & _
                                                    "    AND esqc_localizacion = '{1}' " & _
                                                    "    AND esqc_tipo_localizacion = {2} ", arrEsquemas(i), arrObjeto(1), arrObjeto(2))
                            arrCodCobertura = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)
                            If objConexionDB.fnc_p_pla_borrar_cobertura(arrCodCobertura(0), intCodUsuario) = 0 _
                                And objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(arrEsquemas(i), intCodUsuario) = 0 Then 'Identificar el resultado del procedimiento.
                                bolRespuesta = False
                            End If
                        Else
                            Exit For 'Salir del FOR en caso de un error.
                        End If
                    Next
                    If bolRespuesta = True Then 'Validar para darle una respuesta al usuario.
                        Return 1 'En caso de exito.
                    Else
                        Return 0 'En caso de algun fallo en la consulta.
                    End If
                Else 'Cuando la cobertura no es compartida por otro esquema.
                    Return objConexionDB.fnc_p_pla_borrar_cobertura(intCodCobertura, intCodUsuario)
                End If
            Else
                Return 5 'Imposible realizar la acción el esquema se ejecuto.
            End If
        End Function
    End Class
End Namespace