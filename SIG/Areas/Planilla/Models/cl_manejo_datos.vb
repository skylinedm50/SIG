Imports Newtonsoft.Json

Namespace SIG.Areas.Planilla.Models

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

        Public Function fnc_formatear_texto_query(ByVal strTexto As String) As String 'Función para formatear texto que contenga comillas simples(').
            Dim intConteo As Integer = 0 'Contador de veces que se recorre el ciclo.
            Dim intPosicionCaracter As Integer 'Número que indica la posición donde se encuentra el caracter a buscar.
            Dim strTextoValidado As String = "" 'Texto que ya ha sido validado.
            Dim strTextoPorValidar As String = "" 'Texto que aun no se ha validad
            Dim strSimboloBuscar As String = "'" 'Simbolo o caracter a buscar dentro de la cadena de texto.
            Dim strSimboloAdd As String = "''" 'Simbolo o caracter a agregar en la cadena de texto.

            Do
                intConteo += 1
                If intConteo = 1 Then 'Si el recorrido del ciclo es por primera vez.
                    intPosicionCaracter = strTexto.IndexOf(strSimboloBuscar) 'Identificamos la posición del caracter en la cadena de texto original.
                    If intPosicionCaracter = -1 Then 'Si el simbolo a buscar no se enceuntra en alguna posición de la cadena de texto.
                        Return strTexto
                    Else 'En caso que si se encuentra en alguna posición de la cadena de texto.
                        strTextoValidado = strTexto.Substring(0, intPosicionCaracter).Insert(intPosicionCaracter, strSimboloAdd) 'Identificamos el primer simbolo a buscar encontrado y
                        'se sustituye por otro simbolo esta cadena se guarda en una variable.
                        strTextoPorValidar = strTexto.Substring(intPosicionCaracter + 1) 'La parte de la cadena de texto que no esta validad o faltante se agrega a una variable.
                        intPosicionCaracter = strTextoPorValidar.IndexOf(strSimboloBuscar) 'Identificamos la posición en la cadena faltante si aun existe un simbolo a buscar.
                    End If
                Else 'Cuando el recorrido es por segunda vez o más.
                    intPosicionCaracter = strTextoPorValidar.IndexOf(strSimboloBuscar) 'Identificamos la posición en la cadena faltante si aun existe un simbolo a buscar.
                    strTextoValidado += strTextoPorValidar.Substring(0, intPosicionCaracter).Insert(intPosicionCaracter, strSimboloAdd)
                    strTextoPorValidar = strTextoPorValidar.Substring(intPosicionCaracter + 1)
                    intPosicionCaracter = strTextoPorValidar.IndexOf(strSimboloBuscar)
                End If
            Loop While intPosicionCaracter <> -1

            Return strTextoValidado + strTextoPorValidar
        End Function

    End Class

End Namespace
