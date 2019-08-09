Imports Microsoft.Owin
Imports Owin
<Assembly: OwinStartup(GetType(Startup))> 

Public Class Startup

    Sub Configuration(app As IAppBuilder)
        app.MapSignalR()
    End Sub

End Class
