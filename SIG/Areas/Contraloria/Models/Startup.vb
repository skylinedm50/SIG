Imports System.Threading.Tasks
Imports Microsoft.Owin
Imports Owin
Imports Microsoft.AspNet.SignalR

<Assembly: OwinStartup(GetType(SignalR.Startup))> 

Namespace SignalR
    Public Class Startup
        Public Sub Configuration(app As IAppBuilder)
            app.MapSignalR()
        End Sub
    End Class


    Public Class ClienteSignalR
        Implements IUserIdProvider

        Public Function GetUserId(request As IRequest) As String Implements IUserIdProvider.GetUserId
            Return request.User.Identity.Name
        End Function
    End Class


End Namespace
