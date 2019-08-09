Imports System.IO
Imports System.Xml.Serialization
Imports System.Web.Script.Serialization

Namespace SIG.Areas.Incorporaciones.Class

    Public Class Cl_ReadFile

        ' esta función lee el archivo de configuracion donde se encuntran las credenciales para poder conectarse a la base de datos del sistema
        Public Shared Sub Fnc_ReadFileConexion()

            If File.Exists("C:\inetpub\wwwroot\Core\Config\Config.config") Then
                Dim Objserializar As New XmlSerializer(GetType(Cl_ParamConexion)) ' esta vaiable permite realiza la serialización de los parametros de conexion  a la clase de cl_paramConexion

                Dim Objreader As New FileStream("C:\inetpub\wwwroot\Core\Config\Config.config", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
                ' este objeto permite realizar la lectura del archivo de configuracion para conectarse a la base de datos

                Dim ConexParam As Cl_ParamConexion = DirectCast(Objserializar.Deserialize(Objreader), Cl_ParamConexion)
                Objreader.Close()
            End If

        End Sub
    End Class

End Namespace
