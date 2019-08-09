
Namespace SIG.Areas.Incorporaciones.Models

    Public Class Cl_Persona

#Region "Campos"

        Private _identidad As String
        Private _nombre1 As String
        Private _nombre2 As String
        Private _apellido1 As String
        Private _apellido2 As String
        Private _fch_nacimiento As Nullable(Of Date)
        Private _sexo As Nullable(Of Integer)
        Private _estado As Nullable(Of Integer)
        Private _per_personas As Nullable(Of Integer)
        Private _per_edad As Nullable(Of Integer)
        Private _per_ciclo As Nullable(Of Integer)
        Private _per_sexoD As String
        Private _pre_per_personas As String
        Private _per_estadoD As String
        Private _per_titular As Nullable(Of Integer)
        Private _per_titularD As String
        Private _pre_persona_actualizacion As String

#End Region

#Region "Propiedades"

        Public Property Pre_Per_Persona As String
            Get
                Return _pre_per_personas
            End Get
            Set(ByVal value As String)
                _pre_per_personas = value
            End Set
        End Property

        Public Property Identidad As String
            Get
                Return _identidad
            End Get
            Set(ByVal value As String)
                _identidad = value
            End Set
        End Property

        Public Property Nombre1 As String
            Get
                Return _nombre1
            End Get
            Set(ByVal value As String)
                _nombre1 = value
            End Set
        End Property

        Public Property Nombre2 As String
            Get
                Return _nombre2
            End Get
            Set(ByVal value As String)
                _nombre2 = value
            End Set
        End Property

        Public Property Apellido1 As String
            Get
                Return _apellido1
            End Get
            Set(ByVal value As String)
                _apellido1 = value
            End Set
        End Property

        Public Property Apellido2 As String
            Get
                Return _apellido2
            End Get
            Set(ByVal value As String)
                _apellido2 = value
            End Set
        End Property

        Public Property Fch_nacimiento As Date?
            Get
                Return _fch_nacimiento
            End Get
            Set(ByVal value As Date?)
                _fch_nacimiento = value
            End Set
        End Property

        Public Property Sexo As Integer?
            Get
                If (Me._per_sexoD = "Masculino") Then
                    Me._sexo = 1
                ElseIf (Me._per_sexoD = "Femenino") Then
                    Me._sexo = 2
                Else
                    Me._sexo = Nothing
                End If
                Return _sexo
            End Get
            Set(ByVal value As Integer?)
                _sexo = value
            End Set
        End Property

        Public Property EstadoD(indice As Integer, valor As Integer, tipo As Boolean) As String
            Get
                If Me._estado = 1 Then
                    Me._per_estadoD = "Activo"
                ElseIf Me._estado = 0 Then
                    Me._per_estadoD = "Desagregado"
                ElseIf Me._estado = 4 Then
                    Me._per_estadoD = "Falleció"
                ElseIf (_per_estadoD = "No remitido por RUP") Then
                    _estado = 9
                ElseIf (_per_estadoD = "Desagregación por Ficha RUP") Then
                    _estado = 10
                ElseIf (_per_estadoD = "Suspensión temporal por revisión") Then
                    _estado = 11
                End If
                Return _per_estadoD
            End Get
            Set(ByVal value As String)
                If tipo Then
                    Me._per_estadoD = valor
                Else
                    Me._per_estadoD = value
                End If
            End Set
        End Property

        Public Property Estado As Integer?
            Get
                If (_per_estadoD = "Activo") Then
                    _estado = 1
                ElseIf (_per_estadoD = "Desagregado") Then
                    _estado = 0
                ElseIf (_per_estadoD = "Falleció") Then
                    _estado = 4
                ElseIf (_per_estadoD = "No remitido por RUP") Then
                    _estado = 9
                ElseIf (_per_estadoD = "Desagregación por Ficha RUP") Then
                    _estado = 10
                ElseIf (_per_estadoD = "Suspensión temporal por revisión") Then
                    _estado = 11
                End If
                Return _estado
            End Get
            Set(ByVal value As Integer?)
                _estado = value
            End Set
        End Property

        Public Property Edad As Integer?

            Get
                If (Me._fch_nacimiento.ToString() <> "") Then
                    Me._per_edad = Int((DateDiff(DateInterval.DayOfYear, Convert.ToDateTime(Me._fch_nacimiento), Convert.ToDateTime(Date.Today)) / 365))
                Else
                    If IsDBNull(Me._per_edad) Or Me._per_edad.ToString() <> "" Then
                        Me._per_edad = Nothing
                    End If
                End If
                Return _per_edad
            End Get

            Set(ByVal value As Integer?)
                _per_edad = value
            End Set

        End Property

        Public Property Ciclo As Integer?
            Get
                If (Me._per_edad IsNot Nothing) Then
                    Select Case Convert.ToInt16(Me._per_edad)
                        Case 0 To 4
                            Me._per_ciclo = 1
                        Case 5 To 6
                            Me._per_ciclo = 2
                        Case 7 To 11
                            Me._per_ciclo = 3
                        Case 12
                            Me._per_ciclo = 4
                        Case 13 To 15
                            Me._per_ciclo = 5
                        Case 16 To 17
                            Me._per_ciclo = 6
                        Case Nothing
                            Me._per_ciclo = Nothing
                        Case Else
                            Me._per_ciclo = 0
                    End Select
                End If
                Return _per_ciclo
            End Get
            Set(ByVal value As Integer?)
                _per_ciclo = value
            End Set
        End Property

        Public Property SexoD(indice As Integer, valor As Integer, tipo As Boolean) As String

            Get
                If Me._sexo = 1 Then
                    Me._per_sexoD = "Masculino"
                ElseIf Me._sexo = 2 Then
                    Me._per_sexoD = "Femenino"
                Else
                    Me._per_sexoD = ""
                End If
                Return _per_sexoD
            End Get

            Set(ByVal value As String)

                If tipo Then
                    If valor = 1 Then
                        Me._sexo = 1
                    ElseIf valor = 2 Then
                        Me._sexo = 2
                    End If
                Else
                    _per_sexoD = value
                End If

            End Set
        End Property

        Public Property Per_Personas() As Integer?
            Get
                Return _per_personas
            End Get
            Set(ByVal value As Integer?)
                _per_personas = value
            End Set
        End Property

        Public Property Pre_persona_actualizacion() As String
            Get
                Return _pre_persona_actualizacion
            End Get
            Set(ByVal value As String)
                _pre_persona_actualizacion = value
            End Set
        End Property

        Public Property Per_titular As Integer?
            Get
                If _per_titularD = "Es Titular" Then
                    If _per_titular = 0 Then
                        _per_titular = 2
                    End If
                Else
                    _per_titular = 0
                End If
                Return _per_titular
            End Get
            Set(ByVal value As Integer?)
                _per_titular = value
            End Set
        End Property

        Public Property Per_titularD() As String
            Get
                Return _per_titularD
            End Get
            Set(ByVal value As String)
                _per_titularD = value
            End Set
        End Property

#End Region





    End Class

End Namespace