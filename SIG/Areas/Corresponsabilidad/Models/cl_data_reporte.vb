Namespace SIG.Areas.Corresponsabilidad.Models

    Public Class cl_data_reporte
        Private objConexionDB As New cl_conexion_db()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array


        Function fnc_obtener_centros() As DataTable
            strQuery = String.Format("select cod_sac_cen_edu, nom_cen_edu from dbo.t_corr_centro_educativo")
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Return objTabla
        End Function

        Function fnc_obtener_centros_departa(ByVal codDepart As String) As DataTable
            Dim objFila As DataRow
            strQuery = String.Format("select Cent.cod_sac_cen_edu, Cent.nom_cen_edu " + _
                                      "from dbo.t_corr_centro_educativo as Cent " + _
                                      "INNER JOIN dbo.t_corr_municipio_sace as Muni " + _
                                      "ON Muni.cod_mun_sac = Cent.cod_sac_mun_sac " + _
                                      "inner join dbo.t_corr_departamento_sace as Depar " + _
                                      "ON Depar.cod_dep_sac = Muni.cod_sac_dep_sac " + _
                                      "where Depar.cod_dep_sac IN ({0})", codDepart)


            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_sac_cen_edu") = "0"
            objFila.Item("nom_cen_edu") = "(Todo)"
            objTabla.Rows.InsertAt(objFila, 0)


            Return objTabla
        End Function

        Function fnc_obtener_centros_muni(ByVal codMuni As String) As DataTable
            Dim objFila As DataRow
            strQuery = String.Format("select distinct Cent.cod_sac_cen_edu, Cent.nom_cen_edu " + _
                                      "from dbo.t_corr_centro_educativo as Cent " + _
                                      "INNER JOIN dbo.t_corr_municipio_sace as Muni " + _
                                      "ON Muni.cod_mun_sac = Cent.cod_sac_mun_sac " + _
                                      "inner join dbo.t_corr_departamento_sace as Depar " + _
                                      "ON Depar.cod_dep_sac = Muni.cod_sac_dep_sac " + _
                                      "where muni.cod_mun_sac IN ({0})", codMuni)


            objTabla = objConexionDB.fnc_crear_datatable(strQuery)

            objFila = objTabla.NewRow()
            objFila.Item("cod_sac_cen_edu") = "0"
            objFila.Item("nom_cen_edu") = "(Todo)"
            objTabla.Rows.InsertAt(objFila, 0)
            Return objTabla
        End Function

        Function fnc_obtener_departamento() As DataTable
            Dim objFila As DataRow
            strQuery = String.Format("SELECT  cod_departamento, desc_departamento FROM t_glo_departamentos")
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_departamento") = "00"
            objFila.Item("desc_departamento") = "(Todo)"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

        Function fnc_obtener_municipio()
            strQuery = String.Format("SELECT cod_municipio, cod_departamento, desc_municipio FROM t_glo_municipios")
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)

            Return objTabla
        End Function

        Function fnc_obtener_departamentos_sace()
            Dim objFila As DataRow
            strQuery = String.Format("SELECT  cod_sac_dep_sac, nom_dep_sac FROM t_corr_departamento_sace")
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_sac_dep_sac") = 0
            objFila.Item("nom_dep_sac") = "(Todos)"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

    End Class

End Namespace
