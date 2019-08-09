Namespace SIG.Areas.Incorporaciones.Class

    Public Class Cl_ParamConexion

        Private Shared _source As String ' esta variable establece el nombre de la instancia del servidor de base de datos

        Public Property source() As String
            Get
                Return _source
            End Get
            Set(ByVal value As String)
                _source = value
            End Set
        End Property

        Private Shared _database As String ' esta variable establece el nombre de la base de datos a la que se conecta el ssistema
        Public Property db() As String
            Get
                Return _database
            End Get
            Set(ByVal value As String)
                _database = value
            End Set
        End Property

        Private Shared _user As String ' esta variable establece el nombre de usuario con el que se conecta al servidor de base de datos
        Public Property name() As String
            Get
                Return _user
            End Get
            Set(ByVal value As String)
                _user = value
            End Set
        End Property

        Private Shared password As String ' esta variable establece la contraceña con la que se conecta a la base de datos
        Public Property pwd() As String
            Get
                Return password
            End Get
            Set(ByVal value As String)
                password = value
            End Set
        End Property

    End Class

End Namespace
