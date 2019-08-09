Imports Newtonsoft.Json

Namespace SIG.Areas.Corresponsabilidad.Models

    Public Class cl_manejo_datos
        Private arrListFilas As New List(Of Dictionary(Of String, Object))
        Private arrListFila As Dictionary(Of String, Object)
        Private strReturnJson As String

        Public Function fnc_crear_datatable_diccionario(ByVal objDatoTabla As DataTable) As List(Of Dictionary(Of String, Object))
            For Each objDataFila As DataRow In objDatoTabla.Rows
                arrListFila = New Dictionary(Of String, Object)()
                For Each objColumnas As DataColumn In objDatoTabla.Columns
                    arrListFila.Add(objColumnas.ColumnName, objDataFila(objColumnas))
                Next
                arrListFilas.Add(arrListFila)
            Next

            Return arrListFilas
        End Function

        Public Function fnc_crear_datatable_json(ByVal objDatoTabla As DataTable) As String 'Funcion que convierte un objeto DataTable en una estructura JSON.
            For Each objDataFila As DataRow In objDatoTabla.Rows 'Por cada fila del objeto DataTable recorro el objeto.
                arrListFila = New Dictionary(Of String, Object)() 'Creo un objeto de tipo diccionario.
                For Each objColumnas As DataColumn In objDatoTabla.Columns 'Por la cantidad de columnas que contenga esa fila es las veces que voy a recorrerla.
                    arrListFila.Add(objColumnas.ColumnName, objDataFila(objColumnas)) 'Agrego un valor al diccionario, indicando el indice y el valor.
                Next
                arrListFilas.Add(arrListFila) 'Agrego el valor del diccionario a la lista.
            Next

            strReturnJson = JsonConvert.SerializeObject(arrListFilas) 'Convierto de una lista a un formato JSON.

            Return strReturnJson
        End Function
    End Class

End Namespace
