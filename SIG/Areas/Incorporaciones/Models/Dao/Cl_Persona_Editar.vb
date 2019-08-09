Namespace SIG.Areas.Incorporaciones.Models

    Public Class Cl_Persona_Editar


#Region "Campos"

        Private _PK As String
        Private _per_identidad As String
        Private _per_nombre1 As String
        Private _per_nombre2 As String
        Private _per_apellido1 As String
        Private _per_apellido2 As String
        Private _per_fch_nacimiento As Nullable(Of Date)
        Private _per_sexo As String
        Private _per_estado As String
        Private _per_persona As String
        Private _per_edad As String
        Private _per_ciclo As String
        Private _per_sexoD As String
        Private _per_estadoD As String
        Private _per_titular As String
        Private _per_titularD As String
        Private _pre_persona_actualizacion As String
        Private _pre_per_personas As String




        Public Property Per_identidad As String
            Get
                Return _per_identidad
            End Get
            Set(value As String)
                _per_identidad = value
            End Set
        End Property

        Public Property Per_nombre1 As String
            Get
                Return _per_nombre1
            End Get
            Set(value As String)
                _per_nombre1 = value
            End Set
        End Property

        Public Property Per_nombre2 As String
            Get
                Return _per_nombre2
            End Get
            Set(value As String)
                _per_nombre2 = value
            End Set
        End Property

        Public Property Per_apellido1 As String
            Get
                Return _per_apellido1
            End Get
            Set(value As String)
                _per_apellido1 = value
            End Set
        End Property

        Public Property Per_apellido2 As String
            Get
                Return _per_apellido2
            End Get
            Set(value As String)
                _per_apellido2 = value
            End Set
        End Property

        Public Property per_fch_nacimiento As Date?
            Get
                Return _per_fch_nacimiento
            End Get
            Set(value As Date?)
                _per_fch_nacimiento = value
            End Set
        End Property

        Public Property Per_sexo As String
            Get
                Return _per_sexo
            End Get
            Set(value As String)
                _per_sexo = value
            End Set
        End Property

        Public Property Per_estado As String
            Get
                Return _per_estado
            End Get
            Set(value As String)
                _per_estado = value
            End Set
        End Property

        Public Property Per_persona As String
            Get
                Return _per_persona
            End Get
            Set(value As String)
                _per_persona = value
            End Set
        End Property

        Public Property Per_edad As String
            Get
                Return _per_edad
            End Get
            Set(value As String)
                _per_edad = value
            End Set
        End Property

        Public Property Per_ciclo As String
            Get
                Return _per_ciclo
            End Get
            Set(value As String)
                _per_ciclo = value
            End Set
        End Property

        Public Property Per_sexoD As String
            Get
                Return _per_sexoD
            End Get
            Set(value As String)
                _per_sexoD = value
            End Set
        End Property

        Public Property Per_estadoD As String
            Get
                Return _per_estadoD
            End Get
            Set(value As String)
                _per_estadoD = value
            End Set
        End Property

        Public Property Per_titular As String
            Get
                Return _per_titular
            End Get
            Set(value As String)
                _per_titular = value
            End Set
        End Property

        Public Property Per_titularD As String
            Get
                Return _per_titularD
            End Get
            Set(value As String)
                _per_titularD = value
            End Set
        End Property

        Public Property Pre_persona_actualizacion As String
            Get
                Return _pre_persona_actualizacion
            End Get
            Set(value As String)
                _pre_persona_actualizacion = value
            End Set
        End Property

        Public Property Pre_per_personas As String
            Get
                Return _pre_per_personas
            End Get
            Set(value As String)
                _pre_per_personas = value
            End Set
        End Property

        Public Property PK As String
            Get
                Return _PK
            End Get
            Set(value As String)
                _PK = value
            End Set
        End Property
    End Class

#End Region


End Namespace
