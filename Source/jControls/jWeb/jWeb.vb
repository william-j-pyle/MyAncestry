Imports System.IO
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms
Imports Newtonsoft.Json

#Const SHOW_DEBUG = True

Public Class JWeb
    Inherits WebView2
    Dim WithEvents DataMgr As DBData = My.Application.DataMgr

    Private Const JSAPI = "window.MyAncestryAPI"

    Public Property IsReady As Boolean = False

    Private _JSControlerFile As String = ""
    Public Property JSControlerFile As String
        Get
            Return _JSControlerFile
        End Get
        Set(value As String)
            _JSControlerFile = value
        End Set
    End Property
    Private _HostName As String = ""
    Public Property HostName As String
        Get
            Return _HostName
        End Get
        Set(value As String)
            _HostName = value
            If IsReady Then
                SetVirtualConfig()
                SetURL()
            End If
        End Set
    End Property
    Private _FSBasePath As String = ""
    Public Property FSBasePath As String
        Get
            Return _FSBasePath
        End Get
        Set(value As String)
            _FSBasePath = value
            If IsReady Then
                SetVirtualConfig()
                SetURL()
            End If
        End Set
    End Property
    Private _URL As Uri
    Public Property URL As Uri
        Get
            Return _URL
        End Get
        Set(value As Uri)
            _URL = value
            If IsReady Then
                SetVirtualConfig()
                SetURL()
            End If
        End Set
    End Property

    Dim _ShowFocus As Boolean = False
    Public Property ShowFocus As Boolean
        Get
            Return _ShowFocus
        End Get
        Set(value As Boolean)
            _ShowFocus = value
            'todo post message to web control
        End Set
    End Property

    Public Event FocusReceived()
    Public Event WebMessage(ObjectName As String, Category As String, params() As String)

    Private WithEvents CoreWeb As CoreWebView2

    Public Sub New()
        SetStyle(ControlStyles.ContainerControl Or ControlStyles.Selectable Or ControlStyles.StandardClick, True)
        BackColor = Color.Black
        Visible = False
        IsReady = False
        'Dim env As New CoreWebView2Environmen
        EnsureCoreWebView2Async()
    End Sub

    Private Sub JWebView2_WebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs) Handles Me.WebMessageReceived
        Dim msg As DMSelectionMessage = JsonConvert.DeserializeObject(Of DMSelectionMessage)(e.WebMessageAsJson)
        Select Case msg.EventName
            Case "WindowClick"
                RaiseEvent FocusReceived()
            Case "ItemSelected"
                Debug.Print("ItemSelected: [{0}]", e.WebMessageAsJson())
                DataMgr.RowSelected(msg.DataSetName, msg.EventArg, msg.ControlKey)
            Case Else
                Debug.Print("UnknownMessage: [{0}]", e.WebMessageAsJson())

        End Select
    End Sub

    Private Sub SetVirtualConfig()
#If SHOW_DEBUG Then
        Debug.Print("SetVirtualConfig")
#End If
        If IsReady Then
            If HostName.Length > 0 And FSBasePath.Length > 0 And CoreWeb IsNot Nothing Then
                CoreWebView2.SetVirtualHostNameToFolderMapping(HostName, FSBasePath, CoreWebView2HostResourceAccessKind.Allow)
            End If
        End If
    End Sub

    Private Sub SetURL()
#If SHOW_DEBUG Then
        Debug.Print("SetURL")
#End If
        If IsReady Then
            If HostName.Length > 0 And FSBasePath.Length > 0 Then
                If URL IsNot Nothing Then
                    If URL.OriginalString.Length > 0 Then
                        Source = URL
                    End If
                End If
            End If
        End If
    End Sub

    'Public Async Sub SetDataObject(objName As String, obj As Object)
    '    CoreWebView2.AddHostObjectToScript(objName, obj)
    'End Sub


    Private Sub JWebView2_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles Me.CoreWebView2InitializationCompleted
#If SHOW_DEBUG Then
        Debug.Print("JWebView2_CoreWebView2InitializationCompleted")
#End If
        CoreWeb = CoreWebView2
        With CoreWeb
            With .Settings
                .AreHostObjectsAllowed = True
                .IsPasswordAutosaveEnabled = True
                .IsWebMessageEnabled = True
                .IsGeneralAutofillEnabled = True
                .IsScriptEnabled = True
                .IsZoomControlEnabled = False
                .AreBrowserAcceleratorKeysEnabled = False
#If SHOW_DEBUG Then
                .AreDefaultScriptDialogsEnabled = true
                .AreDevToolsEnabled = true
                .AreDefaultContextMenusEnabled = True
                .IsStatusBarEnabled = true
                .IsBuiltInErrorPageEnabled = true
#Else
                .AreDefaultScriptDialogsEnabled = False
                .AreDevToolsEnabled = False
                .AreDefaultContextMenusEnabled = False
                .IsStatusBarEnabled = False
                .IsBuiltInErrorPageEnabled = False
#End If
            End With
            .AddHostObjectToScript("DataMgr", DataMgr)
            If JSControlerFile.Length > 0 Then
                Dim js As String = File.ReadAllText(JSControlerFile)
                .AddScriptToExecuteOnDocumentCreatedAsync(js)
            End If
        End With
        IsReady = True
        SetVirtualConfig()
        SetURL()
    End Sub

    Public Async Function API(action As WebAPIActions, ParamArray args() As String) As Task(Of String)
        If Not IsReady Then Return False
        Select Case action
            Case WebAPIActions.AddItem

            Case WebAPIActions.RemoveItem

            Case WebAPIActions.SetFilterCriteria
                If args.Length > 0 Then
                    If args(0).Length > 0 Then
                        Return Await executeJS(JSAPI & ".applyFilter('" + args(0) + "');")
                    End If
                End If
                Return Await executeJS(JSAPI & ".clearFilter();")
            Case WebAPIActions.SetHasFocus
                Dim hasFocus As Boolean = True
                If args.Length > 0 Then
                    If args(0) = False.ToString() Then
                        hasFocus = False
                    End If
                End If
                If hasFocus Then
                    Return Await executeJS(JSAPI & ".setControlFocus();")
                Else
                    Return Await executeJS(JSAPI & ".removeControlFocus();")
                End If
                Return False
            Case WebAPIActions.SelectItem
                Return Await executeJS(JSAPI & ".selectedItem(" & args(0) & ");")
            Case WebAPIActions.GetSelectedItem
                Return Await executeJS(JSAPI & ".getSelectedItem();")
            Case WebAPIActions.SelectionChanged
                If args.Length > 1 Then
                    Dim msg As New DMSelectionMessage
                    msg.DataSetName = args(0)
                    msg.EventName = "SelectionChanged"
                    msg.EventArg = args(1)
                    Return Await executeJS(JSAPI & ".selectionChanged('" & JsonConvert.SerializeObject(msg, Formatting.None) & "');")
                End If
                Return False
            Case WebAPIActions.DataSetChanged
                Return Await executeJS(JSAPI & ".getSelectedItem();")
            Case Else
#If SHOW_DEBUG Then
                Debug.Print("INVALID WEB ACTION TYPE!")
#End If
        End Select
        Return False
    End Function

    Delegate Function ExecuteScript(ByVal js As String)
    Public mainThreadExecuteScript As ExecuteScript = New ExecuteScript(AddressOf ExecuteScriptAsync)

    Public Async Function executeJS(js As String) As Task(Of String)
#If SHOW_DEBUG Then
        Debug.Print("executeJS:  {0}", js)
#End If
        Try
            If InvokeRequired Then
                Return Await Invoke(mainThreadExecuteScript, js)
            Else
                Return Await ExecuteScriptAsync(js)
            End If
        Catch ex As Exception
#If SHOW_DEBUG Then
            Debug.Print("ERROR: {0}", ex.Message)
#End If
        End Try
        Return ""
    End Function

    Private Sub DataMgr_DataChanged(dataSetName As String) Handles DataMgr.DataChanged

    End Sub

    Private Sub DataMgr_SelectionChanged(dataSetName As String, selectedRowIdx As Integer) Handles DataMgr.SelectionChanged
#Disable Warning BC42358 ' Because this call is not awaited, execution of the current method continues before the call is completed
        API(WebAPIActions.SelectionChanged, dataSetName, selectedRowIdx)
#Enable Warning BC42358 ' Because this call is not awaited, execution of the current method continues before the call is completed
    End Sub

    Private Sub CoreWeb_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles CoreWeb.NavigationCompleted
        If Visible = False Then Visible = True
    End Sub

#If SHOW_DEBUG Then

    Private Sub CoreWeb_PermissionRequested(sender As Object, e As CoreWebView2PermissionRequestedEventArgs) Handles CoreWeb.PermissionRequested
        Debug.Print("CoreWeb_PermissionRequested")

    End Sub

    Private Sub CoreWeb_ProcessFailed(sender As Object, e As CoreWebView2ProcessFailedEventArgs) Handles CoreWeb.ProcessFailed
        Debug.Print("CoreWeb_ProcessFailed")

    End Sub

    Private Sub CoreWeb_StatusBarTextChanged(sender As Object, e As Object) Handles CoreWeb.StatusBarTextChanged
        Debug.Print("CoreWeb_StatusBarTextChanged")

    End Sub

    Private Sub CoreWeb_WebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs) Handles CoreWeb.WebMessageReceived
        Debug.Print("CoreWeb_WebMessageReceived: [{0}]", e.WebMessageAsJson())

    End Sub

    Private Sub CoreWeb_BasicAuthenticationRequested(sender As Object, e As CoreWebView2BasicAuthenticationRequestedEventArgs) Handles CoreWeb.BasicAuthenticationRequested
        Debug.Print("CoreWeb_BasicAuthenticationRequested")

    End Sub

    Private Sub CoreWeb_DocumentTitleChanged(sender As Object, e As Object) Handles CoreWeb.DocumentTitleChanged
        Debug.Print("CoreWeb_DocumentTitleChanged")

    End Sub

    Private Sub CoreWeb_HistoryChanged(sender As Object, e As Object) Handles CoreWeb.HistoryChanged
        Debug.Print("CoreWeb_HistoryChanged")

    End Sub

    Private Sub CoreWeb_NavigationStarting(sender As Object, e As CoreWebView2NavigationStartingEventArgs) Handles CoreWeb.NavigationStarting
        Debug.Print("CoreWeb_NavigationStarting")

    End Sub

    Private Sub CoreWeb_NewWindowRequested(sender As Object, e As CoreWebView2NewWindowRequestedEventArgs) Handles CoreWeb.NewWindowRequested
        Debug.Print("CoreWeb_NewWindowRequested")

    End Sub

    Private Sub CoreWeb_ContentLoading(sender As Object, e As CoreWebView2ContentLoadingEventArgs) Handles CoreWeb.ContentLoading
        Debug.Print("CoreWeb_ContentLoading")

    End Sub

    Private Sub CoreWeb_ContextMenuRequested(sender As Object, e As CoreWebView2ContextMenuRequestedEventArgs) Handles CoreWeb.ContextMenuRequested
        Debug.Print("CoreWeb_ContextMenuRequested")

    End Sub

    Private Sub CoreWeb_DOMContentLoaded(sender As Object, e As CoreWebView2DOMContentLoadedEventArgs) Handles CoreWeb.DOMContentLoaded
        Debug.Print("CoreWeb_DOMContentLoaded")

    End Sub

    Private Sub CoreWeb_DownloadStarting(sender As Object, e As CoreWebView2DownloadStartingEventArgs) Handles CoreWeb.DownloadStarting
        Debug.Print("CoreWeb_DownloadStarting")

    End Sub

    Private Sub CoreWeb_FrameNavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles CoreWeb.FrameNavigationCompleted
        Debug.Print("CoreWeb_FrameNavigationCompleted")

    End Sub

    Private Sub CoreWeb_FrameNavigationStarting(sender As Object, e As CoreWebView2NavigationStartingEventArgs) Handles CoreWeb.FrameNavigationStarting
        Debug.Print("CoreWeb_FrameNavigationStarting")
    End Sub
#End If

End Class
