Public Class jBoxViews
    Inherits jPanel

    Private Config As jConfig = My.Application.Config
    Private Views As New Dictionary(Of String, jBoxView)

    Private _ActiveView As jBoxView
    Public ReadOnly Property ActiveView As jBoxView
        Get
            Return _ActiveView
        End Get
    End Property

    Private _ActiveViewKey As String = ""
    Public Property ActiveViewKey As String
        Get
            Return _ActiveViewKey
        End Get
        Set(value As String)
            Debug.Print("Activating View: {0}", value)
            If Not Views.ContainsKey(value.ToUpper) Then
                Debug.Print("Loading View: {0}", value)
                LoadView(value.ToUpper)
            End If
            _ActiveViewKey = value.ToUpper
            _ActiveView = Views(value.ToUpper)
            _ActiveView.BringToFront()
        End Set
    End Property

    Private Event BorderWidthChanged()
    Private _BorderWidth As Integer = 6
    Public Property BorderWidth As Integer
        Get
            Return _BorderWidth
        End Get
        Set(value As Integer)
            _BorderWidth = value
            RaiseEvent BorderWidthChanged()
        End Set
    End Property

    Public Sub New()
        Name = "JBoxView"
        BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
        BorderColorBottom = BackColor
        BorderColorLeft = BackColor
        BorderColorRight = BackColor
        BorderColorTop = BackColor
        Margin = New Padding(0)
        BorderWidthBottom = BorderWidth
        BorderWidthLeft = BorderWidth
        BorderWidthRight = BorderWidth
        BorderWidthTop = BorderWidth
    End Sub

    Private Sub jBoxView_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged
        Location = New Point(0, 0)
        ClientSize = Parent.ClientSize
        Height = Parent.Height
        Width = Parent.Width
        Dock = DockStyle.Fill
        Invalidate()
    End Sub

    Private Sub jBoxView_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged
        BorderColorBottom = BackColor
        BorderColorLeft = BackColor
        BorderColorRight = BackColor
        BorderColorTop = BackColor
        Invalidate()
    End Sub

    Private Sub jBoxView_BorderWidthChanged() Handles Me.BorderWidthChanged
        BorderWidthBottom = BorderWidth
        BorderWidthLeft = BorderWidth
        BorderWidthRight = BorderWidth
        BorderWidthTop = BorderWidth
        Invalidate()
    End Sub


    Private Sub LoadView(pViewKey As String)
        Dim ViewKey As String = pViewKey.ToUpper
        Dim JSControlFile As String = Config.Config.JSControlFile
        Dim HostName As String = Config.Config.VirtualHostName
        Dim FSBasePath As String = Config.Config.PathWebRoot
        Dim View As New jBoxView(ViewKey)
        Controls.Add(View)
        Views.Add(ViewKey, View)
        LoadLayout(ViewKey)
        For Each itm As cfgViewItem In Config.ViewItems
            If itm.ViewKey.ToUpper = ViewKey Then
                Select Case itm.Type
                    Case "jBoxViewItemWebViewer", "Web"
                        Debug.Print("Adding Item: {0}", itm.TabCaption)
                        Dim boxWeb As jBoxViewItemWeb
                        boxWeb = New jBoxViewItemWeb(itm.TabCaption, itm.BoxHeader, itm.Url, JSControlFile, HostName, FSBasePath)
                        boxWeb.Key = itm.Key
                        For Each flg As String In itm.Flags
                            Select Case flg.ToUpper
                                Case "SHOWCLOSE"
                                    boxWeb.ShowClose = True
                                Case "SHOWMENU"
                                    boxWeb.ShowContextMenu = True
                                Case "SHOWSEARCH"
                                    boxWeb.ShowSearch = True
                                Case "SHOWTOOLBAR"
                                    boxWeb.ShowToolbar = True
                                Case Else
                                    'Throw New Exception("Unknown ViewItem Flag: " & flg)
                                    Debug.Print("Unknown ViewItem Flag: " & flg)
                            End Select
                        Next
                        boxWeb.BoxLocation = Config.translateBoxPlacement(itm.BoxLocation)
                        Views(ViewKey).AddBoxItem(boxWeb)
                    Case Else
                        Throw New Exception("New jBoxViewItem Type required")
                End Select
            End If
        Next
    End Sub



    'Public Sub AddView(ViewKey As String)
    '    If Views.ContainsKey(ViewKey.ToUpper) Then Exit Sub
    '    Dim View As New jBoxView(ViewKey)
    '    Controls.Add(View)
    '    Views.Add(ViewKey.ToUpper, View)
    '    ActiveViewKey = ViewKey.ToUpper
    '    LoadLayout(ViewKey.ToUpper)
    'End Sub

    'Public Sub AddViewItem(ViewKey As String, Item As jBoxViewItem)
    '    AddView(ViewKey.ToUpper)
    '    Views(ViewKey.ToUpper).AddBoxItem(Item)
    'End Sub

    Public Sub SaveLayout(Optional ViewKey As String = "")
        If ViewKey = "" Then
            For Each C In Views
                C.Value.SaveLayout()
            Next
        Else
            Views(ViewKey.ToUpper).SaveLayout()
        End If

    End Sub

    Public Sub LoadLayout(Optional ViewKey As String = "")
        If ViewKey = "" Then
            For Each C In Views
                C.Value.LoadLayout()
            Next
        Else
            Views(ViewKey.ToUpper).LoadLayout()
        End If
    End Sub
End Class
