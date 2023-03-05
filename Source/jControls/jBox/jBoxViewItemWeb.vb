Public Class jBoxViewItemWeb
    Inherits jBoxViewItem

    Const DEF_BOXLOCATION As jBoxPlacementType = jBoxPlacementType.BoxTopLeft

    Private _URL As Uri
    Public Property URL As Uri
        Get
            If _URL IsNot Nothing Then
                Return _URL
            Else
                Return New Uri("http://MyAncestry/Blank.html")
            End If
        End Get
        Set(value As Uri)
            _URL = value
            web.URL = URL
        End Set
    End Property

    Private WithEvents web As JWeb

    Public Sub New(tabCaption As String, boxHeader As String, url As String, Optional JSControlFile As String = "", Optional VirtHostname As String = "", Optional VirtLocalPath As String = "")
        Caption = tabCaption
        HeaderText = boxHeader
        Description = ""
        web = New JWeb
        With web
            .JSControlerFile = JSControlFile
            .HostName = VirtHostname
            .FSBasePath = VirtLocalPath
            .URL = New Uri(url)
        End With
        InitControl()
    End Sub

    Private Sub InitControl()
        Controls.Add(web)
        With web
            .Dock = DockStyle.Fill
        End With
    End Sub

    Public Overrides ReadOnly Property PreferedBoxLocation As jBoxPlacementType
        Get
            Return DEF_BOXLOCATION
        End Get
    End Property

    Private Sub jBoxViewItemTest_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged
        Dock = DockStyle.Fill
    End Sub

    Private Sub web_FocusReceived() Handles web.FocusReceived
        RaiseFocusEvent()
    End Sub

    Private Async Sub jBoxViewItemWebView3_ItemFocusChanged(itemHasFocus As Boolean) Handles Me.ItemFocusChanged
        Await web.API(WebAPIActions.SetHasFocus, itemHasFocus)
    End Sub

    Private Async Sub jBoxViewItemWebViewer_SearchCriteriaChanged(criteria As String) Handles Me.SearchCriteriaChanged
        Await web.API(WebAPIActions.SetFilterCriteria, criteria)
    End Sub

    Private Async Sub jBoxViewItemWebViewer_SearchCleared() Handles Me.SearchCleared
        Await web.API(WebAPIActions.SetFilterCriteria)
    End Sub
End Class
