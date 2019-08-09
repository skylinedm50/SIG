Namespace SIG.Areas.Planilla.Models

    Public Class cl_shared

        Dim objConexionDB As SIG.Areas.Planilla.Models.cl_conexion_db = New SIG.Areas.Planilla.Models.cl_conexion_db
        Dim strQuery As String

        Public Function fnc_obtener_pagos() As DataTable

            Dim MyResult As New DataTable
            Dim sql As String = "" + vbCr + _
                "SELECT pag_codigo, pag_nombre " + vbCr + _
                "   FROM SIG_T.dbo.f_pla_pago " + vbCr + _
                "   ORDER BY pag_codigo"
            Try
                MyResult = objConexionDB.fnc_crear_datatable(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        'función para el combobox de los esquemas
        Public Function fnc_obtener_esquemas(ByVal pago As String) As DataTable

            Dim MyResult As New DataTable
            Dim sql As String = "" + vbCr + _
                "SELECT esq_codigo, nombre_esquema " + vbCr + _
                "   FROM SIG_T.dbo.f_pla_esquema " + vbCr + _
                "   WHERE pag_codigo = " + pago + vbCr + _
                "   ORDER BY esq_codigo"
            Try
                MyResult = objConexionDB.fnc_crear_datatable(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Public Function fnc_obtener_esquemas_con_detalle(ByVal pago As String) As DataTable

            Dim MyResult As New DataTable
            Dim sql As String = "" + vbCr + _
                "SELECT esq_codigo, nombre_esquema, esq_tipo_pago, esq_detalle_meses " + vbCr + _
                "   FROM SIG_T.dbo.f_pla_esquema " + vbCr + _
                "   WHERE pag_codigo = " + pago + vbCr + _
                "   ORDER BY esq_codigo"
            Try
                MyResult = objConexionDB.fnc_crear_datatable(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Function fnc_obtener_departamento_sig() As DataTable
            strQuery = "SELECT " & _
                        "	cod_departamento, " & _
                        "	desc_departamento " & _
                        "FROM " & _
                        "	SIG_T.dbo.t_glo_departamentos "
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_municipio_sig(ByVal strCodDepartamento) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	cod_municipio, " & _
                                    "	desc_municipio, " & _
                                    "   cod_departamento " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.t_glo_municipios " & _
                                    "WHERE " & _
                                    "	cod_departamento = '{0}' ", strCodDepartamento)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_aldea_sig(ByVal strCodMun As String) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	cod_aldea, " & _
                                    "	desc_aldea, " & _
                                    "   cod_municipio " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.t_glo_aldeas " & _
                                    "WHERE " & _
                                    "	cod_municipio = '{0}' ", strCodMun)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_caserio_sig(ByVal strCodAldea As String) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	cod_caserio, " & _
                                    "	desc_caserio, " & _
                                    "   cod_aldea " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.t_glo_caserios " & _
                                    "WHERE " & _
                                    "	cod_aldea = '{0}' ", strCodAldea)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_tipo_localizacion() As DataTable
            strQuery = "SELECT  " & _
                        "    int_codigo, " & _
                        "    int_descripcion " & _
                        "FROM " & _
                        "    SIG_T.dbo.f_pla_tipo_localizacion " & _
                        "WHERE " & _
                        "    int_codigo BETWEEN 0 AND 4 "
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_trasferencias(ByVal intCodEsquema As Integer) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	bon_codigo, " & _
                                    "	bon_numero, " & _
                                    "	bon_detalle_meses, " & _
                                    "	bon_meses_cubrir " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_bonos_act " & _
                                    "WHERE  " & _
                                    "	esq_codigo = {0} ", intCodEsquema)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_componente() As DataTable
            strQuery = "SELECT " & _
                        "	comp_codigo, " & _
                        "	comp_nombre " & _
                        "FROM " & _
                        "	SIG_T.dbo.f_pla_tipo_componente "
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obetener_corresponsabilidad(ByVal intCodComponente As Integer) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	corr_codigo, " & _
                                    "	corr_nombre " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_corresponsabilidad " & _
                                    "WHERE " & _
                                    "	comp_codigo = {0} ", intCodComponente)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function




    End Class

End Namespace


