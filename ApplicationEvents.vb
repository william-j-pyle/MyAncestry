Imports Microsoft.VisualBasic.ApplicationServices
Imports Newtonsoft.Json
Imports System.IO

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Public Config As jConfig
        Public DataMgr As DBData

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            Config = JsonConvert.DeserializeObject(Of jConfig)(New StreamReader("D:\FamilyAncestry\apps\MyAncestry\Project\Config\MyAncestry.json").ReadToEnd)
            Config.LoadProperties("D:\FamilyAncestry\apps\MyAncestry\FileSystem\config\user.props")

            DataMgr = New DBData()
            DataMgr.LoadGedCom("D:\FamilyAncestry\apps\MyAncestry\FileSystem\data\tree.ged")
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            Config.Properties.Save()
        End Sub
    End Class
End Namespace
