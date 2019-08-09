Imports System.Web.HttpContext
Imports System.Web.SessionState.HttpSessionState
Imports System.Globalization

Namespace SIG.Areas.Incorporaciones.Models

    Public Class Cl_Hogar_Beneficiario
        Inherits SIG.Areas.Incorporaciones.Models.Cl_Hogar

        Private _personasactualizadas As New Cl_Persona()
        Private _personasparametro As New Cl_Persona()
        Dim Obj_PersonasPreActualizacion As New SIG.Areas.Incorporaciones.Models.Cl_Persona()
        Dim obj_tabla As New DataTable()
        Shared Expediente_Hogar As New DataTable()


        ' esta función ejecula un procedimiento almacenado que realiza la busqueda de la información del hogar que se va a suspender del programa.

        Public Function Fnc_InformacionHogar(ByVal str_identidad As String, ByVal int_hogar As Integer)

            Me.Str_Query = " exec P_InformacionHogarSuspender @identidad='{0}' , @Codhogar = {1} "
            Me.Str_Query = String.Format(Str_Query, str_identidad, int_hogar)
            Return FncGetTableQuery()

        End Function


        Public Function Fnc_SuspensionActivacionHogar(ByVal int_hogar As Integer, ByVal int_estado As Integer, ByVal str_descripcion As String)

            Me.Str_Query = " exec P_Suspension_del_Hogar @cod_hogar={0},@nuevo_estado={1} , @usuario={2} , @descripcion_='{3}'"
            Me.Str_Query = String.Format(Me.Str_Query, int_hogar, int_estado, HttpContext.Current.Session("usuario").ToString(), str_descripcion)
            Return Me.Fnc_GetSingledataConexion()

        End Function



        Function Fnc_Validar_PreActualizaciones(hog_hogar As Integer, int_solicitud As Integer, int_consula As Integer)

            Me.Str_Query = "select stuff(( ( select distinct Upper(' , '+ISNULL(Observacion_Desagregacion,'') + case when Observacion_Desagregacion <> '' "
            Me.Str_Query += " and Observacion_Actualizacion is not null then ' con código '+CAST( b.per_persona as nvarchar(500) ) else '' end+' '+ISNULL(Observacion_Actualizacion,'') + "
            Me.Str_Query += " case when Observacion_Actualizacion <> '' and Observacion_Actualizacion is not null then 'con código: '+CAST( b.per_persona as nvarchar(100)) else ''  end +' , '+ISNULL(Observacion_Cambio_Titular,'') +', '+ISNULL(Observacion_Agregar_Miembro,'')+', '+ISNULL(Detalle_Actualizaciones,'') )"
            Me.Str_Query += " from DB_AP.dbo.V_Hogares_Validados_Det as b "
            Me.Str_Query += " where b.per_hogar = " + hog_hogar.ToString() + " And ( b.Desagregar = 1  or b.Actualizar = 1 or Cambio_Titular = 1 or Agregar_menor = 1 )"
            Me.Str_Query += " for XML path(''), type)	).value('.','varchar(max)'),1,0,'')"
            HttpContext.Current.Session("Observacion") = Fnc_GetSingledataConexion()


            Me.Str_Query = " select distinct a.hog_hogar , a.desc_departamento , a.desc_municipio , a.desc_aldea, a.desc_caserio , a.hogar_direccion , "
            Me.Str_Query += "       a.hog_telefono, a.hog_umbral, a.PK, a.per_persona, a.per_identidad, a.per_nombre1, a.per_nombre2, a.per_apellido1, a.per_apellido2,"
            Me.Str_Query += "       a.per_sexoD, a.per_fch_nacimiento, a.per_estadoD, a.per_estado, a.per_sexo, a.pre_personas_actualizacion ,a.per_titular , a.per_titularD ,"
            Me.Str_Query += "       a.per_ciclo, a.per_edad ,b.Desagregar , "
            Me.Str_Query += "  CASE 
								    WHEN PER_CICLO = 1 THEN 'Edad de: 0 - 4 años' 
								    WHEN PER_CICLO = 2 THEN 'Edad de: 5 - 6 años' 
								    WHEN PER_CICLO = 3 THEN 'Edad de: 7 - 11 años' 
								    WHEN PER_CICLO = 4 THEN 'Edad de: 12 años' 
								    WHEN PER_CICLO = 5 THEN 'Edad de: 13 - 15 años'
								    WHEN PER_CICLO = 6 THEN 'Edad de: 16 - 17 años'  
								    ELSE 'Mayor de 17 años'
							     END as Ciclo_Edad "
            Me.Str_Query += "  from V_Hogares_ValidarPreActualizacion as a"
            Me.Str_Query += "     left join DB_AP.dbo.V_Hogares_Validados_Det as b"
            Me.Str_Query += "  on a.per_persona = b.per_persona"
            Me.Str_Query += "  and Desagregar = 1 where hog_hogar = {0}"

            Me.Str_Query = String.Format(Str_Query, hog_hogar)
            Return FncGetTableQuery()
        End Function

        Function fnc_actualiarInformacionHogarBeneficiario(hog_hogar As Integer, accion As Integer, descripcion As String)


            fnc_Iniciarlizar_Tabla_Informacion_Registrar_PreActualizacion()
            fnc_crear_registros_informacion_pre_actualizar()
            Dim resultado
            Dim Rows = obj_tabla_personas_actualizar.Select(" per_titular = 1 or per_titular = 2 ")

            If Rows.Length > 0 And HttpContext.Current.Session("usuario") IsNot Nothing Then

                Me.conexion.Open()
                Me.Comando.CommandText = "p_crear_informacion_personas_beneficiarias_2"
                Me.Comando.Connection = Me.conexion
                Me.Comando.CommandType = CommandType.StoredProcedure
                Me.Comando.Parameters.AddWithValue("@hog_hogar", hog_hogar)
                _parametro_tabla = Me.Comando.Parameters.AddWithValue("@personasActualizar", obj_tabla_personas_actualizar)
                _parametro_tabla.SqlDbType = SqlDbType.Structured
                Me.Comando.Parameters.AddWithValue("@confirmar_registro_titular", accion)
                Me.Comando.Parameters.AddWithValue("@usuario", HttpContext.Current.Session("usuario"))
                Me.Comando.Parameters.AddWithValue("@descripcion", descripcion)
                Me.Comando.CommandTimeout = 864000
                resultado = Me.Comando.ExecuteScalar()

                Me.conexion.Close()

                If Not resultado.Contains("""salida"" : ""25""") Then
                    HttpContext.Current.Session("PersonasHogarActualizar") = ""
                    HttpContext.Current.Session("Expediente") = Nothing
                    HttpContext.Current.Session("dots") = Nothing
                End If


            Else
                resultado = "{'salida': '100' }"
            End If

            Return resultado
        End Function

        Function fnc_BuscarHistoricosActualizacion(ByRef perPersona As Integer)

            Me.Str_Query = "  SELECT PER_PERSONA ,  "
            Me.Str_Query += "        IDENTIDAD_ANTES ,  "
            Me.Str_Query += "        IDENTIDAD_DESPUES , "
            Me.Str_Query += "        NOMBRE1_ANTES  , "
            Me.Str_Query += "        NOMBRE1_DESPUES , "
            Me.Str_Query += "        NOMBRE2_ANTES , "
            Me.Str_Query += "        NOMBRE2_DESPUES , "
            Me.Str_Query += "        APELLIDO1_ANTES , "
            Me.Str_Query += "	     APELLIDO1_DESPUES , "
            Me.Str_Query += "		 APELLIDO2_ANTES , "
            Me.Str_Query += "        APELLIDO2_DESPUES , "
            Me.Str_Query += "        CASE "
            Me.Str_Query += "	          WHEN SEXO_ANTES = '1' THEN 'Masculino' "
            Me.Str_Query += " 	          WHEN SEXO_ANTES = '2' THEN 'Femenino'  "
            Me.Str_Query += "	          ELSE CAST(SEXO_ANTES AS nvarchar(20) ) "
            Me.Str_Query += "        END AS SEXO_ANTES , "
            Me.Str_Query += "        CASE "
            Me.Str_Query += "              WHEN SEXO_DESPUES = '1' THEN 'Masculino' "
            Me.Str_Query += "	           WHEN SEXO_DESPUES = '2' THEN 'Femenino'  "
            Me.Str_Query += "              ELSE CAST(SEXO_DESPUES AS nvarchar(20) ) "
            Me.Str_Query += "        END AS SEXO_DESPUES , "
            Me.Str_Query += "        FCH_NACIMIENTO_ANTES , "
            Me.Str_Query += "        FCH_NACIMIENTO_DESPUES , "
            Me.Str_Query += "        B.per_estado_descripcion AS ESTADO_ANTES ,"
            Me.Str_Query += "        CASE "
            Me.Str_Query += "            WHEN C.per_estado_descripcion IS NULL THEN A.ESTADO_DESPUES  "
            Me.Str_Query += "	         ELSE C.per_estado_descripcion "
            Me.Str_Query += "        END  AS ESTADO_DESPUES, "
            Me.Str_Query += "        CASE "
            Me.Str_Query += "	         WHEN TITULAR_ANTES = '1' THEN 'Es Titular' "
            Me.Str_Query += "	         WHEN TITULAR_ANTES = '0' THEN 'No Es Titular' "
            Me.Str_Query += "            ELSE CAST(TITULAR_ANTES  AS nvarchar(20) ) "
            Me.Str_Query += "        END AS TITULAR_ANTES , "
            Me.Str_Query += "        CASE "
            Me.Str_Query += "	         WHEN TITULAR_DESPUES = '1' THEN 'Es Titular' "
            Me.Str_Query += "	         WHEN TITULAR_DESPUES = '0' THEN 'No Es Titular' "
            Me.Str_Query += "	         ELSE CAST( TITULAR_DESPUES AS nvarchar (20 ) )"
            Me.Str_Query += "        END  AS TITULAR_DESPUES, "
            Me.Str_Query += "        PER_FCH_REGISTRO_PRE_ACTUALIZACION ,"
            Me.Str_Query += "         NOMBRE_USUARIO ,"
            Me.Str_Query += "         ORDEN "
            Me.Str_Query += " FROM SIG_T.[dbo].[V_HOGARES_DETALLE_HISTORICO]			AS A "
            Me.Str_Query += "           Left Join "
            Me.Str_Query += "      SIG_T.DBO.t_per_estados								AS B "
            Me.Str_Query += "	        ON A.ESTADO_ANTES = B.per_estado "
            Me.Str_Query += "           Left Join "
            Me.Str_Query += "      SIG_T.DBO.t_per_estados								AS C "
            Me.Str_Query += "           ON C.per_estado = CASE WHEN A.ESTADO_DESPUES <> 'SIN CAMBIO' THEN A.ESTADO_DESPUES END "
            Me.Str_Query += " WHERE PER_PERSONA = " + perPersona.ToString() + " ORDER BY ORDEN ASC "

            Return Me.FncGetTableQuery()

        End Function

        Function fnc_solicitudes_Actualizacion()

            Dim Str_Hogares_Actualizados = ""

            Me.Str_Query = "select distinct " & _
                            "	a.per_hogar, " & _
                            "	PATH, " & _
                            "	b.desc_departamento, " & _
                            "	b.desc_municipio, " & _
                            "	b.desc_aldea, " & _
                            "	b.desc_caserio, " & _
                            "	ISNULL(b.per_nombre1,'')+' '+ISNULL(b.per_nombre2,'')+' '+ISNULL(b.per_apellido1,' ')+' '+ISNULL(b.per_apellido2,' ') as Participante,	 " & _
                            "	RTRIM(LTRIM(replace(Comentarios_Expediente,'" & _
                            "',''))) as Comentarios_Expediente " & _
                            "from " & _
                            "	DB_AP.dbo.V_Hogares_Validados_Det as a " & _
                            "left join  " & _
                            "	SIG_T.dbo.V_Ben_Hogares_Personas as b " & _
                            "	on a.per_hogar = b.hog_hogar " & _
                            "where " & _
                            "	b.per_titular = 1 " & _
                            "order by " & _
                            "	a.per_hogar "


            Return Me.FncGetTableQuery()

        End Function

        Function FNC_BuscarExpedienteDigitalHogarActualizar(ByRef hog_hogar As Integer)

            Me.Str_Query = "select distinct " & _
                            "	PATH, " & _
                            "	RTRIM(LTRIM(replace(Comentarios_Expediente,'" & _
                            "',''))) as Comentarios_Expediente " & _
                            "from " & _
                            "	DB_AP.dbo.V_Hogares_Validados_Det as a " & _
                            "left join  " & _
                            "	SIG_T.dbo.T_BEN_PERSONAS as b " & _
                            "	on a.per_hogar = b.per_hogar " & _
                            "where " & _
                            "	b.per_titular = 1  and a.per_hogar = " + hog_hogar.ToString()

            Return Me.FncGetTableQuery()
        End Function


        Function fnc_buscar_para_pre_actualizar(hog_hogar As Integer)
            Me.Str_Query = "select * from v_pre_actualizaciones where hog_hogar = {0}"
            Me.Str_Query = String.Format(Me.Str_Query, hog_hogar)
            Return Me.FncGetTableQuery()
        End Function

        Function fnc_buscar_informacion_hogar(hogar As Integer)

            Me.Str_Query = "select hog_hogar , hogar_direccion,cod_caserio,desc_caserio"
            Me.Str_Query += "   , cod_aldea , desc_aldea, cod_municipio,desc_municipio,cod_departamento , desc_departamento"
            Me.Str_Query += "    , hog_umbral,hog_telefono,per_persona,per_nombre1,per_nombre2,per_apellido1,per_apellido2 , per_identidad,per_edad,per_ciclo,"
            Me.Str_Query += "    per_titular,per_fch_nacimiento,case when per_sexo=1 then 'Masculino' when per_sexo=2 then 'Femenino' else '' end as per_sexo ,"
            Me.Str_Query += "   case when per_estado in(1,2,3) then 'Activo' when per_estado =0 then 'Desagregado' when per_estado = 4 then 'Falleció' end as per_estado"
            Me.Str_Query += " from V_Ben_Hogares_Personas"
            Me.Str_Query += " where hog_hogar ={0}"

            Me.Str_Query = String.Format(Me.Str_Query, hogar)
            Return Me.FncGetTableQuery()
        End Function


        Public Function fnc_actualizar_persona(persona As Cl_Persona_Editar, hog_hogar As Integer, tipo As Boolean)

            Obj_PersonasPreActualizacion.Per_Personas = If(persona.Per_persona IsNot Nothing, persona.Per_persona.Replace("""", ""), persona.Per_persona)
            Obj_PersonasPreActualizacion.Identidad = If(persona.Per_identidad IsNot Nothing, persona.Per_identidad.Replace("""", ""), persona.Per_identidad)
            Obj_PersonasPreActualizacion.Nombre1 = If(persona.Per_nombre1 IsNot Nothing, persona.Per_nombre1.Replace("""", ""), persona.Per_nombre1)
            Obj_PersonasPreActualizacion.Nombre2 = If(persona.Per_nombre2 IsNot Nothing, persona.Per_nombre2.Replace("""", ""), persona.Per_nombre2)
            Obj_PersonasPreActualizacion.Apellido1 = If(persona.Per_apellido1 IsNot Nothing, persona.Per_apellido1.Replace("""", ""), persona.Per_apellido1)
            Obj_PersonasPreActualizacion.Apellido2 = If(persona.Per_apellido2 IsNot Nothing, persona.Per_apellido2.Replace("""", ""), persona.Per_apellido2)
            Obj_PersonasPreActualizacion.SexoD(0, 0, False) = If(persona.Per_sexoD IsNot Nothing, persona.Per_sexoD.Replace("""", ""), persona.Per_sexoD)
            Obj_PersonasPreActualizacion.Sexo() = If(persona.Per_sexo IsNot Nothing, persona.Per_sexo.Replace("""", ""), persona.Per_sexo)
            Obj_PersonasPreActualizacion.Fch_nacimiento = persona.per_fch_nacimiento
            Obj_PersonasPreActualizacion.EstadoD(0, 0, False) = If(persona.Per_estadoD IsNot Nothing, persona.Per_estadoD.Replace("""", ""), persona.Per_estadoD)
            Obj_PersonasPreActualizacion.Pre_Per_Persona = If(persona.Pre_per_personas IsNot Nothing, persona.Pre_per_personas.Replace("""", ""), persona.Pre_per_personas)
            'Obj_PersonasPreActualizacion.Estado() = If(persona.Per_estado IsNot Nothing, persona.Per_estado.Replace("""", ""), persona.Per_estado)
            Obj_PersonasPreActualizacion.Edad() = If(persona.Per_edad IsNot Nothing, persona.Per_edad.Replace("""", ""), persona.Per_edad)
            Obj_PersonasPreActualizacion.Ciclo() = If(persona.Per_ciclo IsNot Nothing, persona.Per_ciclo.Replace("""", ""), persona.Per_ciclo)
            Obj_PersonasPreActualizacion.Pre_persona_actualizacion() = If(persona.Pre_persona_actualizacion IsNot Nothing, persona.Pre_persona_actualizacion.Replace("""", ""), persona.Pre_persona_actualizacion)
            Obj_PersonasPreActualizacion.Per_titularD() = If(persona.Per_titularD IsNot Nothing, persona.Per_titularD.Replace("""", ""), persona.Per_titularD)
            Obj_PersonasPreActualizacion.Per_titular() = If(persona.Per_titular IsNot Nothing, persona.Per_titular.Replace("""", ""), persona.Per_titular)

            If (HttpContext.Current.Session("PersonasHogarPreActualizar") IsNot Nothing) Then
                obj_tabla = HttpContext.Current.Session("PersonasHogarPreActualizar")
            Else
                If tipo Then
                    obj_tabla = fnc_buscar_informacion_hogar(hog_hogar)
                Else
                    obj_tabla = Fnc_Validar_PreActualizaciones(hog_hogar, 0, 2)
                End If
                obj_tabla.Columns.Add("per_Actualizado", GetType(String))
                obj_tabla.Columns.Add("pre_per_persona", GetType(String))
            End If

            Dim Rows = obj_tabla.Select(" PK = '" + persona.PK + "'")
            If (Rows.Length > 0) Then
                Rows(0)("per_identidad") = Obj_PersonasPreActualizacion.Identidad()
                Rows(0)("per_nombre1") = Trim(UCase(Obj_PersonasPreActualizacion.Nombre1()))
                Rows(0)("per_nombre2") = Trim(UCase(Obj_PersonasPreActualizacion.Nombre2()))
                Rows(0)("per_apellido1") = Trim(UCase(Obj_PersonasPreActualizacion.Apellido1()))
                Rows(0)("per_apellido2") = Trim(UCase(Obj_PersonasPreActualizacion.Apellido2()))
                Rows(0)("per_estado") = Obj_PersonasPreActualizacion.Estado()
                Rows(0)("per_estadoD") = Obj_PersonasPreActualizacion.EstadoD(0, 0, False)
                Rows(0)("per_sexo") = If(Obj_PersonasPreActualizacion.Sexo() Is Nothing, DBNull.Value, Obj_PersonasPreActualizacion.Sexo())
                Rows(0)("per_sexoD") = Obj_PersonasPreActualizacion.SexoD(0, 0, False)
                Rows(0)("per_edad") = If(Obj_PersonasPreActualizacion.Edad() Is Nothing, DBNull.Value, Obj_PersonasPreActualizacion.Edad())
                Rows(0)("per_ciclo") = If(Obj_PersonasPreActualizacion.Ciclo() Is Nothing, DBNull.Value, Obj_PersonasPreActualizacion.Ciclo())
                Rows(0)("per_fch_nacimiento") = If(Obj_PersonasPreActualizacion.Fch_nacimiento() Is Nothing, DBNull.Value, Obj_PersonasPreActualizacion.Fch_nacimiento())
                Rows(0)("per_Actualizado") = If(IsDBNull(Rows(0)("per_Actualizado")), 1, If(Rows(0)("per_Actualizado") = 2, 2, 1))
                Rows(0)("pre_personas_actualizacion") = Obj_PersonasPreActualizacion.Pre_persona_actualizacion()
                Rows(0)("per_titularD") = Obj_PersonasPreActualizacion.Per_titularD()
                Rows(0)("per_titular") = Obj_PersonasPreActualizacion.Per_titular()
            End If

            Return obj_tabla
        End Function


        Public Function fnc_agregar_nueva_persona(persona As Cl_Persona_Editar, hog_hogar As Integer, tipo As Boolean)

            Obj_PersonasPreActualizacion.Per_Personas = If(persona.Per_persona IsNot Nothing, persona.Per_persona.Replace("""", ""), persona.Per_persona)
            Obj_PersonasPreActualizacion.Identidad = If(persona.Per_identidad IsNot Nothing, persona.Per_identidad.Replace("""", ""), persona.Per_identidad)
            Obj_PersonasPreActualizacion.Nombre1 = If(persona.Per_nombre1 IsNot Nothing, persona.Per_nombre1.Replace("""", ""), persona.Per_nombre1)
            Obj_PersonasPreActualizacion.Nombre2 = If(persona.Per_nombre2 IsNot Nothing, persona.Per_nombre2.Replace("""", ""), persona.Per_nombre2)
            Obj_PersonasPreActualizacion.Apellido1 = If(persona.Per_apellido1 IsNot Nothing, persona.Per_apellido1.Replace("""", ""), persona.Per_apellido1)
            Obj_PersonasPreActualizacion.Apellido2 = If(persona.Per_apellido2 IsNot Nothing, persona.Per_apellido2.Replace("""", ""), persona.Per_apellido2)
            Obj_PersonasPreActualizacion.SexoD(0, 0, False) = If(persona.Per_sexoD IsNot Nothing, persona.Per_sexoD.Replace("""", ""), persona.Per_sexoD)
            Obj_PersonasPreActualizacion.Sexo() = If(persona.Per_sexo IsNot Nothing, persona.Per_sexo.Replace("""", ""), persona.Per_sexo)
            Obj_PersonasPreActualizacion.Fch_nacimiento = persona.per_fch_nacimiento
            Obj_PersonasPreActualizacion.EstadoD(0, 0, False) = "Activo"
            Obj_PersonasPreActualizacion.Pre_Per_Persona = CInt(Math.Ceiling(Rnd() * 100000000)) + 1 '
            Obj_PersonasPreActualizacion.Estado() = 1
            Obj_PersonasPreActualizacion.Edad() = If(persona.Per_edad IsNot Nothing, persona.Per_edad.Replace("""", ""), persona.Per_edad)
            Obj_PersonasPreActualizacion.Ciclo() = If(persona.Per_ciclo IsNot Nothing, persona.Per_ciclo.Replace("""", ""), persona.Per_ciclo)
            Obj_PersonasPreActualizacion.Pre_persona_actualizacion() = If(persona.Pre_persona_actualizacion IsNot Nothing, persona.Pre_persona_actualizacion.Replace("""", ""), persona.Pre_persona_actualizacion)
            Obj_PersonasPreActualizacion.Per_titularD() = If(persona.Per_titularD IsNot Nothing, persona.Per_titularD.Replace("""", ""), persona.Per_titularD)
            Obj_PersonasPreActualizacion.Per_titular() = If(persona.Per_titular IsNot Nothing, persona.Per_titular.Replace("""", ""), persona.Per_titular)


            If (HttpContext.Current.Session("PersonasHogarPreActualizar") IsNot Nothing) Then
                obj_tabla = HttpContext.Current.Session("PersonasHogarPreActualizar")
            Else
                If tipo Then
                    obj_tabla = fnc_buscar_informacion_hogar(hog_hogar)
                Else
                    obj_tabla = Fnc_Validar_PreActualizaciones(hog_hogar, 0, 2)
                End If
                obj_tabla.Columns.Add("per_Actualizado", GetType(String))
                obj_tabla.Columns.Add("pre_per_persona", GetType(String))
            End If

            _row_fila = obj_tabla.NewRow()
            Dim Rows = obj_tabla.Select()
            _row_fila("per_identidad") = Obj_PersonasPreActualizacion.Identidad()
            _row_fila("per_nombre1") = Trim(UCase(Obj_PersonasPreActualizacion.Nombre1()))
            _row_fila("per_nombre2") = Trim(UCase(Obj_PersonasPreActualizacion.Nombre2()))
            _row_fila("per_apellido1") = Trim(UCase(Obj_PersonasPreActualizacion.Apellido1()))
            _row_fila("per_apellido2") = Trim(UCase(Obj_PersonasPreActualizacion.Apellido2()))
            _row_fila("per_estado") = Obj_PersonasPreActualizacion.Estado()
            _row_fila("per_sexo") = If(Obj_PersonasPreActualizacion.Sexo() Is Nothing, DBNull.Value, Obj_PersonasPreActualizacion.Sexo())
            _row_fila("per_sexoD") = Obj_PersonasPreActualizacion.SexoD(0, 0, False)
            _row_fila("per_fch_nacimiento") = If(Obj_PersonasPreActualizacion.Fch_nacimiento() IsNot Nothing, Obj_PersonasPreActualizacion.Fch_nacimiento(), DBNull.Value)
            _row_fila("per_edad") = If(Obj_PersonasPreActualizacion.Edad() Is Nothing, DBNull.Value, Obj_PersonasPreActualizacion.Edad())
            _row_fila("per_estadoD") = Obj_PersonasPreActualizacion.EstadoD(0, 0, False)
            _row_fila("per_ciclo") = If(Obj_PersonasPreActualizacion.Ciclo() Is Nothing, DBNull.Value, Obj_PersonasPreActualizacion.Ciclo())
            _row_fila("per_Actualizado") = 2
            _row_fila("pre_personas_actualizacion") = If(Obj_PersonasPreActualizacion.Pre_persona_actualizacion() <> "", Obj_PersonasPreActualizacion.Pre_persona_actualizacion(), (Rows.Length + 1).ToString() + "NaN" + Obj_PersonasPreActualizacion.Pre_Per_Persona().ToString())
            _row_fila("per_titularD") = Obj_PersonasPreActualizacion.Per_titularD()
            _row_fila("per_titular") = Obj_PersonasPreActualizacion.Per_titular()
            _row_fila("PK") = If(Obj_PersonasPreActualizacion.Pre_persona_actualizacion() <> "", Obj_PersonasPreActualizacion.Pre_persona_actualizacion(), (Rows.Length + 1).ToString() + "NaN" + Obj_PersonasPreActualizacion.Pre_Per_Persona().ToString())
            _row_fila("pre_persona") = Obj_PersonasPreActualizacion.Pre_Per_Persona()

            If _row_fila IsNot Nothing Then
                obj_tabla.Rows.Add(_row_fila)
            End If

            Return obj_tabla
        End Function


        Private Sub fnc_Iniciarlizar_Tabla_Informacion_Registrar_PreActualizacion()
            Me.obj_tabla_personas_actualizar.Clear()
            Me.obj_tabla_personas_actualizar.Columns.Add("per_identidad", GetType(String))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_nombre1", GetType(String))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_nombre2", GetType(String))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_apellido1", GetType(String))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_apellido2", GetType(String))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_nombre", GetType(String))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_edad", GetType(Integer))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_ciclo", GetType(Integer))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_fch_nacimiento", GetType(Date))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_sexo", GetType(Integer))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_persona", GetType(Integer))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_estado", GetType(Integer))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_titular", GetType(Integer))
            Me.obj_tabla_personas_actualizar.Columns.Add("per_Actualizado", GetType(Integer))
            Me.obj_tabla_personas_actualizar.Columns.Add("pre_personas_actualizacion", GetType(String))
        End Sub


        Sub fnc_crear_registros_informacion_pre_actualizar()

            For Each dr As DataRow In HttpContext.Current.Session("PersonasHogarPreActualizar").Rows
                _row_fila = obj_tabla_personas_actualizar.NewRow()
                _row_fila("per_persona") = If(dr("per_persona") Is DBNull.Value, DBNull.Value, If(dr("per_persona").Contains("NaN"), DBNull.Value, dr("per_persona")))                
                _row_fila("per_identidad") = dr("per_identidad")
                _row_fila("per_nombre1") = dr("per_nombre1")
                _row_fila("per_nombre2") = dr("per_nombre2")
                _row_fila("per_apellido1") = dr("per_apellido1")
                _row_fila("per_apellido2") = dr("per_apellido2")
                _row_fila("per_nombre") = dr("per_nombre1") + " " + dr("per_nombre2")
                _row_fila("per_edad") = dr("per_edad")
                _row_fila("per_ciclo") = dr("per_ciclo")
                _row_fila("per_fch_nacimiento") = dr("per_fch_nacimiento")
                _row_fila("per_sexo") = dr("per_sexo")
                _row_fila("per_estado") = dr("per_estado")
                _row_fila("per_titular") = dr("per_titular")
                _row_fila("per_Actualizado") = dr("per_actualizado")
                _row_fila("pre_personas_actualizacion") = If(dr("pre_personas_actualizacion") Is DBNull.Value, DBNull.Value, If(dr("pre_personas_actualizacion").Contains("NaN"), DBNull.Value, dr("pre_personas_actualizacion")))                
                obj_tabla_personas_actualizar.Rows.Add(_row_fila)
            Next

        End Sub

        Function fnc_RegistrarPerActualizaciones(hog_hogar As Integer, Nuevo_Titular As Integer, Titular_a_Registrar As List(Of String), registrar_titular As Integer, confirmacion_registro_titular As Integer)

            fnc_Iniciarlizar_Tabla_Informacion_Registrar_PreActualizacion()
            fnc_crear_registros_informacion_pre_actualizar()

            Me.conexion.Open()
            Me.Comando.CommandText = "p_registrar_pre_actualizaciones"
            Me.Comando.Connection = Me.conexion
            Me.Comando.CommandType = CommandType.StoredProcedure
            Me.Comando.Parameters.AddWithValue("@hog_hogar", hog_hogar)            
            _parametro_tabla = Me.Comando.Parameters.AddWithValue("@personasActualizar", obj_tabla_personas_actualizar)
            _parametro_tabla.SqlDbType = SqlDbType.Structured
            Dim resultado = Me.Comando.ExecuteScalar()
            Me.conexion.Close()

            If resultado = 1 Then
                HttpContext.Current.Session("PersonasHogarPreActualizar") = ""
            End If

            Return resultado
        End Function

        Function fnc_registrarConfirmacionNuevoTitularEnPreActualizaciones(hog_hogar As Integer)

            Dim _parametro_tabla As SqlClient.SqlParameter
            Me.conexion.Open()
            Me.Comando.CommandText = "p_registrar_pre_actualizaciones"
            Me.Comando.Connection = Me.conexion
            Me.Comando.CommandType = CommandType.StoredProcedure
            Me.Comando.Parameters.AddWithValue("@hog_hogar", hog_hogar)
            Me.Comando.Parameters.AddWithValue("@confirmar_registro_titular", 1)
            _parametro_tabla = Me.Comando.Parameters.AddWithValue("@personasActualizar", HttpContext.Current.Session("ConfirmarTitular"))
            _parametro_tabla.SqlDbType = SqlDbType.Structured
            Dim resultado = Me.Comando.ExecuteScalar()
            Me.conexion.Close()
            If resultado = 1 Then
                HttpContext.Current.Session("PersonasHogarPreActualizar") = ""
                HttpContext.Current.Session("ConfirmarTitular") = ""
            End If
            Return resultado
        End Function

        Function Fnc_Habilitar_solicitudes_noValidadas(solicitud As Integer)

            Me.Str_Query = "if ( (select estado_solicitud_validad from SIG_T.dbo.t_solicitudes_validadas where cod_solicitud_validad = " + solicitud.ToString() + " ) = 1)"
            Me.Str_Query += "     begin"
            Me.Str_Query += "          delete from SIG_T.dbo.t_solicitudes_validadas "
            Me.Str_Query += "           where cod_solicitud_validad = " + solicitud.ToString()
            Me.Str_Query += " end"
            Return Fnc_ExecQuery()

        End Function

        Function fnc_buscar_nucleo(hogar As Integer, titular As String, ValorBuscar As Integer)

            Me.Str_Query = " Exec SIG_T.dbo.P_BUSCAR_NUCLEO_IMPRIMIR {0},'{1}' , {2} " '+ If(hogar = "", DBNull.Value, hogar) + " , " + If(titular = "", DBNull.Value, titular) + " , " + ValorBuscar.ToString()
            Me.Str_Query = String.Format(Me.Str_Query, If(hogar = 0, "NULL", hogar), If(titular = "", "NULL", titular), ValorBuscar.ToString())
            Return Me.FncGetTableQuery()

        End Function

        Function Buscar_Periodo_Actualizacion()

            Me.Str_Query += "SELECT  cod_actualizacion_periodo	  , "
            Me.Str_Query += "        periodo_actualizacion_nombre , "
            Me.Str_Query += "        periodo_fch_apertura		  , "
            Me.Str_Query += "	     case    "
            Me.Str_Query += "	            when periodo_estado = 0 then 'Cerrado' "
            Me.Str_Query += "	            when periodo_estado = 1 then 'Aperturado' "
            Me.Str_Query += "	     end as EstadoD "
            Me.Str_Query += " FROM SIG_T.DBO.t_ben_actualizaciones_periodo"
            Me.Str_Query += " ORDER BY periodo_estado DESC "

            Return Me.FncGetTableQuery()

        End Function

        Function RealizarCierre(periodo As Integer)

            Me.Str_Query += " update  SIG_T.dbo.t_ben_actualizaciones_periodo"
            Me.Str_Query += "	set periodo_estado = 0 , periodo_fch_cierre = getdate()"
            Me.Str_Query += "        where cod_actualizacion_periodo = " + periodo.ToString()

            Return Me.Fnc_ExecQuery()

        End Function

        Function RealizarApertura(PeriodoNombre As String)

            Me.Str_Query += " SELECT * FROM SIG_T.dbo.t_ben_actualizaciones_periodo  WHERE periodo_estado = 1 "

            Dim PeriodosAbiertos = Me.FncGetTableQuery()

            If (PeriodosAbiertos.Rows.Count = 0) Then

                Me.Str_Query = " EXEC SIG_T.DBO.P_TRASLADO_ACTUALIZACIONES_TEMPORALES_EN_APERTURA  '" + PeriodoNombre + "'"

                Return Me.Fnc_GetSingledataConexion()
            End If

            Return 2

        End Function

        Function Fnc_ActualizacionesRealizadas()

            Me.Str_Query = " SELECT FECHA ,NOMBRE_USUARIO , COUNT(DISTINCT hog_hogar) CANTIDAD_HOGARES"
            Me.Str_Query += " FROM ("
            Me.Str_Query += "    SELECT ROW_NUMBER() OVER(PARTITION BY HOG_HOGAR ORDER BY CAST(per_fch_registro_pre_actualizacion AS DATE) DESC ) AS ORDEN, hog_hogar , NOMBRE_USUARIO , CAST(per_fch_registro_pre_actualizacion AS DATE)  AS FECHA"
            Me.Str_Query += " FROM  SIG_T.DBO.V_HOGARES_DETALLE_HISTORICO AS A"
            Me.Str_Query += " ) AS A "
            Me.Str_Query += " GROUP BY NOMBRE_USUARIO , FECHA"
            Me.Str_Query += " ORDER BY FECHA DESC"

            Return Me.FncGetTableQuery()

        End Function

        Function Fnc_VerificacionesRealizadas()

            Me.Str_Query = " SELECT D.nom1_usr_persona+' '+D.nom2_usr_persona+' '+D.ape1_usr_persona+' '+D.ape2_usr_persona Nombre_Usuario,     "
            Me.Str_Query += "       CAST( B.hog_fch_registro_pre_actualizacion AS DATE  ) Fch_Verificacion ,                                    "
            Me.Str_Query += "        COUNT(DISTINCT B.HOG_HOGAR ) Cantidad_Hogares                                                              "
            Me.Str_Query += " From SIG_T.DBO.T_BEN_PERSONAS_TEMP		A                                                                       "
            Me.Str_Query += "               INNER JOIN                                                                                          "
            Me.Str_Query += "       SIG_T.DBO.T_BEN_HOGARES_TEMP       B                                                                        "
            Me.Str_Query += "       On A.PER_HOG_ACTUALIZADO = B.HOG_ACTUALIZADO                                                                "
            Me.Str_Query += "               INNER JOIN                                                                                          "
            Me.Str_Query += "       SIG_T.DBO.T_USUARIOS               C                                                                        "
            Me.Str_Query += "       On C.COD_USUARIO = B.HOG_USUARIO_ACTUALIZADO                                                                "
            Me.Str_Query += "               INNER JOIN                                                                                          "
            Me.Str_Query += "       SIG_T.DBO.T_USR_PERSONAS           D                                                                        "
            Me.Str_Query += "       On D.COD_USR_PERSONA = C.COD_USUARIO                                                                        "
            Me.Str_Query += "       Group BY D.nom1_usr_persona+' '+D.nom2_usr_persona+' '+D.ape1_usr_persona+' '+D.ape2_usr_persona , CAST( B.hog_fch_registro_pre_actualizacion AS DATE  )"
            Me.Str_Query += " ORDER BY CAST( B.hog_fch_registro_pre_actualizacion AS DATE  ) DESC                                               "

            Return Me.FncGetTableQuery()

        End Function


        Function Fnc_Actualizaciones_Hogares()

            Me.Str_Query = " Select C.cod_departamento, C.desc_departamento, c.cod_municipio, c.desc_municipio, Count(DISTINCT A.hog_hogar) As 'Cantidad_hogares' "
            Me.Str_Query += " FROM SIG_T.DBO.V_HOGARES_DETALLE_HISTORICO		A "
            Me.Str_Query += "	INNER JOIN "
            Me.Str_Query += " SIG_T.DBO.t_ben_hogares					B "
            Me.Str_Query += "	ON A.hog_hogar = B.hog_hogar "
            Me.Str_Query += "	INNER JOIN "
            Me.Str_Query += " SIG_T.DBO.V_Glo_caserios					C "
            Me.Str_Query += "	ON B.hog_caserio = C.cod_caserio "
            Me.Str_Query += " GROUP BY C.cod_departamento, C.desc_departamento , c.cod_municipio, c.desc_municipio "

            Return Me.FncGetTableQuery

        End Function

        Function Fnc_ReporteHogaresNuevasIncorporaciones()

            Me.Str_Query = "    SELECT ISNULL(E.nom1_usr_persona,'')+' '+ISNULL(E.nom2_usr_persona,'')+' '+ISNULL(E.ape1_usr_persona,'')+' '+ISNULL(E.ape2_usr_persona,'') Usuario, "
            Me.Str_Query += "           CAST(B.hog_fch_registro_pre_actualizacion AS DATE) Fecha_Actualizacion,                                                                     "
            Me.Str_Query += "           COUNT( DISTINCT C.HOG_HOGAR ) Cantidad_Hogares                                                                                              "
            Me.Str_Query += "   FROM SIG_T.DBO.T_BEN_PERSONAS_TEMP					 A                                                                                              "
            Me.Str_Query += "	        INNER JOIN                                                                                                                                  "
            Me.Str_Query += "   	 SIG_T.DBO.T_BEN_HOGARES_TEMP				 B                                                                                                  "
            Me.Str_Query += "   	 ON A.per_hog_actualizado = B.hog_actualizado                                                                                                   "
            Me.Str_Query += "   	 	INNER JOIN                                                                                                                                  "
            Me.Str_Query += "   	 SIG_T.DBO.V_Ben_Hogares_Personas			 C                                                                                                  "
            Me.Str_Query += "   	 ON C.per_rup_persona = A.per_rup_persona                                                                                                       "
            Me.Str_Query += "   	 	AND C.HOG_CARGA <> 9999                                                                                                                     "
            Me.Str_Query += "   	 	INNER JOIN                                                                                                                                  "
            Me.Str_Query += "   	 SIG_T.DBO.T_usuarios						 D                                                                                                  "
            Me.Str_Query += "   	 ON D.cod_usuario = B.hog_usuario_actualizado                                                                                                   "
            Me.Str_Query += "   	 	INNER JOIN                                                                                                                                  "
            Me.Str_Query += "   	 SIG_T.DBO.t_usr_personas					 E                                                                                                  "
            Me.Str_Query += "   	 ON E.cod_usr_persona = D.cod_usuario                                                                                                           "
            Me.Str_Query += "   WHERE pre_persona_validada = 2 AND  per_hogar_procesado = 1 AND A.per_ciclo IN (1,2,3,4,5,6)                                                        "
            Me.Str_Query += "   GROUP BY ISNULL(E.nom1_usr_persona,'')+' '+ISNULL(E.nom2_usr_persona,'')+' '+ISNULL(E.ape1_usr_persona,'')+' '+ISNULL(E.ape2_usr_persona,'') ,      "
            Me.Str_Query += "   		 CAST(B.hog_fch_registro_pre_actualizacion AS DATE)                                                                                         "
            Me.Str_Query += "   ORDER BY CAST(B.hog_fch_registro_pre_actualizacion AS DATE) DESC                                                                                    "

            Return Me.FncGetTableQuery

        End Function


        Function Fnc_ReportePersonasNuevasIncorporaciones()

            Me.Str_Query = "  SELECT per_hogar , per_identidad , per_nombre1 , per_nombre2, per_apellido1 , per_apellido2 ,                                                         "
            Me.Str_Query += "        CASE                                                                                                                                           "
            Me.Str_Query += "           WHEN per_titular = 1 THEN 'SI'                                                                                                              "
            Me.Str_Query += "           WHEN per_titular = 2 THEN 'SI'                                                                                                              "
            Me.Str_Query += "	        WHEN per_titular = 0 THEN 'NO'                                                                                                              "
            Me.Str_Query += "        END Titular, CAST(B.hog_fch_registro_pre_actualizacion AS DATE) Fch_Actualizacion ,                                                            "
            Me.Str_Query += "        CASE                                                                                                                                           "
            Me.Str_Query += "           WHEN per_hogar_procesado = 0 THEN 'La Persona no Incorporada por no pertenecer al hogar en la ficha actualizada del CENISS'                 "
            Me.Str_Query += "           WHEN per_hogar_procesado = 1 THEN 'La nueva persona se logro incorporar al hogar'                                                           "
            Me.Str_Query += "        END Estado , A.pre_personas_actualizacion   ,                                                                                                  "
            Me.Str_Query += "        ISNULL(D.nom1_usr_persona,'')+' '+ISNULL(D.nom2_usr_persona,'')+' '+ISNULL(D.ape1_usr_persona,'')+' '+ISNULL(D.ape2_usr_persona,'') Usuario    "
            Me.Str_Query += "        , A.per_hog_actualizado                                                                                                                        "
            Me.Str_Query += " FROM SIG_T.DBO.t_ben_personas_temp					A                                                                                               "
            Me.Str_Query += "           INNER JOIN                                                                                                                                  "
            Me.Str_Query += "      SIG_T.DBO.t_ben_hogares_temp					    B                                                                                               "
            Me.Str_Query += "      ON A.per_hog_actualizado = B.hog_actualizado                                                                                                     "
            Me.Str_Query += "           INNER JOIN                                                                                                                                  "
            Me.Str_Query += "      SIG_T.DBO.T_usuarios							    C                                                                                               "
            Me.Str_Query += "      ON C.cod_usuario = B.hog_usuario_actualizado                                                                                                     "
            Me.Str_Query += "           INNER JOIN                                                                                                                                  "
            Me.Str_Query += "      SIG_T.DBO.t_usr_personas						    D                                                                                               "
            Me.Str_Query += "      ON D.cod_usr_persona = C.cod_usr_persona                                                                                                         "
            Me.Str_Query += " WHERE pre_persona_validada = 2                                                                                                                        "
            Me.Str_Query += " ORDER BY CAST(B.hog_fch_registro_pre_actualizacion AS DATE) DESC   ,        per_hogar                                                                 "


            Return Me.FncGetTableQuery

        End Function


        Function Fnc_ListarDescripciones(hog_hogar As Integer)
            Me.Str_Query = "SELECT  HOGAR.DESCRIPCION_ACTUALIZACION							, 
		                            CAST( HOGAR.HOG_FCH_REGISTRO_PRE_ACTUALIZACION AS DATE)	FCH_ACTUALIZACION  ,
		                            PER_USUARIO.NOM1_USR_PERSONA + ' '+PER_USUARIO.APE1_USR_PERSONA USUARIO
                            FROM SIG_T.DBO.T_BEN_HOGARES_TEMP				HOGAR
		                            INNER JOIN
	                             SIG_T.DBO.T_USUARIOS						USUARIO
		                            ON HOGAR.HOG_USUARIO_ACTUALIZADO = USUARIO.COD_USUARIO
		                            INNER JOIN
	                             SIG_T.DBO.T_USR_PERSONAS					PER_USUARIO 
		                            ON PER_USUARIO.COD_USR_PERSONA = USUARIO.COD_USR_PERSONA
                            WHERE  DESCRIPCION_ACTUALIZACION != '' AND HOG_HOGAR = " + hog_hogar.ToString()

            Return Me.FncGetTableQuery()
        End Function

    End Class

End Namespace
