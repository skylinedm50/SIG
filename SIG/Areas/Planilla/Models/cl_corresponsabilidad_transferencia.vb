Namespace SIG.Areas.Planilla.Models
    Public Class cl_corresponsabilidad_transferencia
        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private objManejoDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array

        Function fnc_obtener_corresp_transferencia(ByVal intCodTransferencia As Integer) As DataTable
            strQuery = String.Format("SELECT  " & _
                                    "	BON.bonc_codigo AS bon_codigo_key, " & _
                                    "	BON.bonc_orden, " & _
                                    "	COMP.comp_nombre, " & _
                                    "	CORR.corr_nombre " & _
                                    "FROM  " & _
                                    "	SIG_T.dbo.f_pla_bonos_corresp AS BON " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_corresponsabilidad AS CORR " & _
                                    "	ON BON.corresp_codigo = CORR.corr_codigo " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_tipo_componente AS COMP " & _
                                    "	ON CORR.comp_codigo = COMP.comp_codigo " & _
                                    "WHERE " & _
                                    "	BON.bon_codigo = {0} " & _
                                    "ORDER BY BON.bonc_orden ", intCodTransferencia)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_se_genero_esquema(ByRef intCodTransferencia As Integer) As Boolean
            strQuery = String.Format("SELECT " & _
                                    "	TOP 1 1 " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_bonos_act AS BON " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_esquema AS ESQ " & _
                                    "	ON BON.esq_codigo = ESQ.esq_codigo " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_titulares AS TIT " & _
                                    "	ON ESQ.esq_codigo = TIT.tit_esquema " & _
                                    "WHERE " & _
                                    "	BON.bon_codigo = {0} ", intCodTransferencia)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function

        Function fnc_agregar_corresp_transferencia(ByVal intCodUsuario As Integer, _
                                                   ByVal strCodTransfer() As String, _
                                                   ByVal intCodCorresp As Integer, _
                                                   ByVal intCodComponente As Integer) As Integer
            Dim intRespuesta As Integer = 1
            For i = 0 To strCodTransfer.Length - 1
                If intRespuesta = 1 Then
                    strQuery = String.Format("SELECT  " & _
                                            "	COUNT(bon_codigo) " & _
                                            "FROM " & _
                                            "	SIG_T.[dbo].[f_pla_bonos_corresp] " & _
                                            "WHERE " & _
                                            "	bon_codigo = {0} " & _
                                            "	AND corresp_codigo = {1} ", strCodTransfer(i), intCodCorresp)

                    arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

                    If arrObjeto(0) = 0 Then
                        If fnc_se_genero_esquema(strCodTransfer(i)) = False Then
                            intRespuesta = objConexionDB.fnc_p_pla_nuevo_bono_corresp(intCodUsuario, strCodTransfer(i), intCodCorresp, intCodComponente)
                            intRespuesta = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(fnc_obtener_cod_esquema(strCodTransfer(i)), intCodUsuario)
                        Else
                            Return 2 'Imposible realizar la acción el esquema ya fue generado.
                            Exit For
                        End If
                    Else
                        Return 3 'Imposible realizar la acción ya existe la corresponsabilidad.
                        Exit For
                    End If
                Else
                    Exit For
                End If
            Next
            Return intRespuesta
        End Function

        Function fnc_borrar_corresp_transferencia(ByRef intCodUsuario As Integer, ByRef intCodBono As Integer, ByRef intCodTransferencia As Integer) As Integer
            Dim intRespuesta As Integer = 1
            If fnc_se_genero_esquema(intCodTransferencia) = False Then
                Dim objDataTable As DataTable

                strQuery = String.Format("SELECT " & _
                                        "	bonc_codigo " & _
                                        "FROM " & _
                                        "	SIG_T.[dbo].[f_pla_bonos_corresp] " & _
                                        "WHERE " & _
                                        "	bon_codigo = {0} " & _
                                        "	AND bonc_codigo <> {1} " & _
                                        "ORDER BY bonc_orden ASC ", intCodTransferencia, intCodBono)
                objDataTable = objConexionDB.fnc_crear_datatable(strQuery)

                If objDataTable.Rows.Count > 0 Then
                    Dim intContador As Integer = 0
                    For Each objDataRow As DataRow In objDataTable.Rows
                        If intRespuesta = 1 Then
                            intContador += 1
                            intRespuesta = objConexionDB.fnc_p_pla_modificar_bono_corresp(intCodUsuario, objDataRow("bonc_codigo"), intContador, 1)
                            intRespuesta = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(fnc_obtener_cod_esquema(intCodTransferencia), intCodUsuario)
                        Else
                            Exit For
                        End If
                    Next

                    If intRespuesta = 1 Then
                        Return objConexionDB.fnc_p_pla_modificar_bono_corresp(intCodUsuario, intCodBono, intContador, 2)
                    Else
                        Return intRespuesta
                    End If
                Else
                    Return objConexionDB.fnc_p_pla_modificar_bono_corresp(intCodUsuario, intCodBono, 0, 2)
                End If
            Else
                Return 2 'Imposible realizar la acción el esquema ya fue ejecutado.
            End If
        End Function

        Function fnc_reordenar_corresp_transferencia(ByRef strCodBonos() As String, ByRef intCodUsuario As Integer, ByVal intCodTransferencia As Integer) As Integer
            If fnc_se_genero_esquema(intCodTransferencia) = False Then
                Dim intRespuesta As Integer = 1
                Dim intContador As Integer = 0
                For i = 0 To strCodBonos.Length - 1
                    intContador += 1
                    If intRespuesta = 1 Then
                        intRespuesta = objConexionDB.fnc_p_pla_modificar_bono_corresp(intCodUsuario, strCodBonos(i), intContador, 1)
                        intRespuesta = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(fnc_obtener_cod_esquema(intCodTransferencia), intCodUsuario)
                    Else
                        Exit For
                    End If
                Next
                Return intRespuesta
            Else
                Return 2 'Imposible realizar la acción el esquema ya fue ejecutado.
            End If
        End Function

        Function fnc_obtener_cod_esquema(ByRef intCodBono As Integer) As Integer
            strQuery = String.Format("SELECT  " & _
                                    "	DISTINCT ESQ.esq_codigo " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_bonos_act AS BON " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_esquema AS ESQ " & _
                                    "	ON BON.bon_codigo = ESQ.esq_codigo " & _
                                    "WHERE " & _
                                    "	BON.bon_codigo = {0} ", intCodBono)

            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            Return CInt(arrObjeto(0))
        End Function


    End Class
End Namespace