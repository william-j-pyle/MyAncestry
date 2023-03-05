Public Class jBoxViewItems
    Inherits jPanel

    Protected Theme As Theme = My.Application.Config.Theme
    Protected items As Dictionary(Of String, jBoxViewItem) = New Dictionary(Of String, jBoxViewItem)

    'Create Controls
    Private WithEvents BtnCloseHeader As jButton
    Private WithEvents PnlHeader As jPanel
    Private WithEvents LblHeader As Label
    Private WithEvents btnDropDownHeader As jButton
    Private WithEvents PnlClientArea As Panel
    Private WithEvents pnlToolbar As Panel
    Private WithEvents pnlSearch As jSearch
    Private WithEvents externalClientControl As Control
    Private WithEvents pnlTabBar As jPanel
    Private WithEvents pnlMainBox As jPanel
    Private selectedTab As jPanel

    'Custom Events
    Public Event FocusRecieved(BoxPlacement As jBoxPlacementType)
    Public Event NoItems(BoxPlacement As jBoxPlacementType)
    Public Event ItemSelected(BoxPlacement As jBoxPlacementType, Key As String)
    Public Event CloseClicked(BoxPlacement As jBoxPlacementType)
    Public Event ContextMenuClicked(BoxPlacement As jBoxPlacementType)
    Public Event SearchClicked(BoxPlacement As jBoxPlacementType)

    'Custom Properties
    Public Property ClientControl As Control
        Get
            Return externalClientControl
        End Get
        Set(value As Control)
            If externalClientControl IsNot Nothing Then
                PnlClientArea.Controls.Remove(externalClientControl)
            End If
            externalClientControl = value
            PnlClientArea.Controls.Add(externalClientControl)
            AddHandler externalClientControl.Click, AddressOf DockBox_SetFocus
            AddHandler externalClientControl.GotFocus, AddressOf DockBox_SetFocus
            externalClientControl.Dock = DockStyle.Fill
        End Set
    End Property

    Public Property BoxLocation As jBoxPlacementType = jBoxPlacementType.BoxTopLeft

    Private _SelectedKey As String
    Public ReadOnly Property SelectedItemKey As String
        Get
            Return _SelectedKey
        End Get
    End Property

    Private _ShowFocus As Boolean
    Public Property HasFocus As Boolean
        Get
            Return _ShowFocus
        End Get
        Set
            If Value <> _ShowFocus Then
                _ShowFocus = Value
                ApplyFocus()
                If Value Then
                    RaiseEvent FocusRecieved(BoxLocation)
                End If
            End If
        End Set
    End Property

    Private _ViewItemContainerType As jBoxContainerType = jBoxContainerType.DockBox
    Public Property ViewItemContainerType As jBoxContainerType
        Get
            Return _ViewItemContainerType
        End Get
        Set(value As jBoxContainerType)
            _ViewItemContainerType = value
        End Set
    End Property

    Public Sub New()
        Init()
    End Sub

    Public Sub New(ViewItemContainerType As jBoxContainerType)
        _ViewItemContainerType = ViewItemContainerType
        Init()
    End Sub

    Private Sub Init()
        Select Case ViewItemContainerType
            Case jBoxContainerType.DockBox
                InitDock()
            Case jBoxContainerType.TabDockBox
                InitTab()
            Case Else
                Throw New Exception("ContainerType Not yet supported: " & ViewItemContainerType.ToString)
        End Select
    End Sub

    Private Sub InitDock()
        'Control Settings
        BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
        Margin = New Padding(0, 0, 0, 0)
        Padding = New Padding(0, 0, 0, 0)

        'Tabs at bottom of controls - Visible with >1 dock
        pnlTabBar = New jPanel
        AddHandler pnlTabBar.Click, AddressOf DockBox_SetFocus
        Controls.Add(pnlTabBar)
        With pnlTabBar
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderWidthBottom = 0
            .BorderWidthLeft = 0
            .BorderWidthRight = 0
            .BorderWidthTop = 0
            .BorderStyle = BorderStyle.None
            .Height = 20
            .Dock = DockStyle.Bottom
            .Visible = False
        End With

        'Main Control Body - Always Visible
        pnlMainBox = New jPanel
        AddHandler pnlMainBox.Click, AddressOf DockBox_SetFocus
        Controls.Add(pnlMainBox)
        With pnlMainBox
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorBottom = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorLeft = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorRight = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderWidthBottom = 1
            .BorderWidthLeft = 1
            .BorderWidthRight = 1
            .BorderWidthTop = 1
            .BorderStyle = BorderStyle.None
            .Dock = DockStyle.Fill
            .Visible = True
        End With

        'Toolbar of MainBox
        pnlToolbar = New Panel
        AddHandler pnlToolbar.Click, AddressOf DockBox_SetFocus
        pnlMainBox.Controls.Add(pnlToolbar)
        With pnlToolbar
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .Dock = DockStyle.Top
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            .Height = 23
            .Visible = False
            .BringToFront()
        End With

        'Search of MainBox
        pnlSearch = New jSearch
        AddHandler pnlSearch.Click, AddressOf DockBox_SetFocus
        AddHandler pnlSearch.ChildGotFocus, AddressOf DockBox_SetFocus
        pnlMainBox.Controls.Add(pnlSearch)
        With pnlSearch
            '.BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            '.ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .Dock = DockStyle.Top
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            .Height = 20
            .Visible = True
            .BringToFront()
        End With

        'Header of main box
        PnlHeader = New jPanel
        AddHandler PnlHeader.Click, AddressOf DockBox_SetFocus
        pnlMainBox.Controls.Add(PnlHeader)
        With PnlHeader
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorBottom = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderWidthBottom = 1
            .BorderWidthTop = 1
            .BorderStyle = BorderStyle.None
            .Dock = DockStyle.Top
            .Height = 24
            .Visible = True
            .Focus()
        End With

        'Label in Header
        LblHeader = New Label
        AddHandler LblHeader.Click, AddressOf DockBox_SetFocus
        PnlHeader.Controls.Add(LblHeader)
        With LblHeader
            .FlatStyle = FlatStyle.Flat
            .Dock = DockStyle.Left
            .BackColor = Color.Transparent
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderStyle = BorderStyle.None
            .Text = Text
            .AutoSize = True
            .Margin = New Padding(0)
            .Padding = New Padding(3, 2, 4, 0)
            .Visible = True
            .BringToFront()
        End With

        'Close button on Header
        BtnCloseHeader = New jButton
        AddHandler BtnCloseHeader.Click, AddressOf CloseItem
        AddHandler BtnCloseHeader.GotFocus, AddressOf RedirectFocus
        PnlHeader.Controls.Add(BtnCloseHeader)
        With BtnCloseHeader
            .Dock = DockStyle.Right
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.MouseDownBackColor = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            .FlatAppearance.MouseOverBackColor = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            .BackColor = Color.Transparent
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            .Width = 25
            .Image = Theme.Icon("X", New Size(16, 16), Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE))
            .Text = ""
            .Visible = True
            .BringToFront()
        End With

        'Dropdown button on header
        btnDropDownHeader = New jButton
        AddHandler btnDropDownHeader.Click, AddressOf DockBox_SetFocus
        AddHandler btnDropDownHeader.GotFocus, AddressOf RedirectFocus
        PnlHeader.Controls.Add(btnDropDownHeader)
        With btnDropDownHeader
            .Dock = DockStyle.Right
            .BackColor = Color.Transparent
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.MouseDownBackColor = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            .FlatAppearance.MouseOverBackColor = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            .Margin = New Padding(0, 0, 10, 0)
            .Padding = New Padding(0)
            .Width = 25
            .Image = Theme.Icon("DROPDOWN", New Size(16, 16), Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE))
            .Text = ""
            .Visible = False
            .BringToFront()
        End With

        'Client Hosting Box
        PnlClientArea = New Panel
        AddHandler PnlClientArea.Click, AddressOf DockBox_SetFocus
        pnlMainBox.Controls.Add(PnlClientArea)
        With PnlClientArea
            .BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .Dock = DockStyle.Fill
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            .Visible = True
            .BringToFront()
        End With

    End Sub

    Private Sub InitTab()
        'Control Settings
        BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
        Margin = New Padding(0, 0, 0, 0)
        Padding = New Padding(0, 0, 0, 0)

        'Tabs at top of 
        pnlTabBar = New jPanel
        AddHandler pnlTabBar.Click, AddressOf DockBox_SetFocus
        Controls.Add(pnlTabBar)
        With pnlTabBar
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorBottom = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderWidthBottom = 0
            .BorderWidthLeft = 0
            .BorderWidthRight = 0
            .BorderWidthTop = 0
            .BorderStyle = BorderStyle.None
            .Height = 20
            .Dock = DockStyle.Top
            .Visible = True
        End With

        'Main Control Body - Always Visible
        pnlMainBox = New jPanel
        AddHandler pnlMainBox.Click, AddressOf DockBox_SetFocus
        Controls.Add(pnlMainBox)
        With pnlMainBox
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorBottom = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorLeft = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorRight = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .BorderWidthBottom = 1
            .BorderWidthLeft = 1
            .BorderWidthRight = 1
            .BorderWidthTop = 2
            .Dock = DockStyle.Fill
            .BringToFront()
            .Visible = True
        End With

        'Toolbar of MainBox
        pnlToolbar = New Panel
        AddHandler pnlToolbar.Click, AddressOf DockBox_SetFocus
        pnlMainBox.Controls.Add(pnlToolbar)
        With pnlToolbar
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .Dock = DockStyle.Top
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            .Height = 23
            .Visible = False
            .BringToFront()
        End With

        'Search of MainBox
        pnlSearch = New jSearch
        AddHandler pnlSearch.Click, AddressOf DockBox_SetFocus
        pnlMainBox.Controls.Add(pnlSearch)
        With pnlSearch
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .Dock = DockStyle.Top
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            .Height = 23
            .Visible = False
            .BringToFront()
        End With

        ''Dropdown button on header
        'btnDropDownHeader = New jButton
        'AddHandler btnDropDownHeader.Click, AddressOf DockBox_SetFocus
        'AddHandler btnDropDownHeader.GotFocus, AddressOf RedirectFocus
        'PnlHeader.Controls.Add(btnDropDownHeader)
        'With btnDropDownHeader
        '    .Dock = DockStyle.Right
        '    .BackColor = Color.Transparent
        '    .FlatStyle = FlatStyle.Flat
        '    .FlatAppearance.BorderSize = 0
        '    .FlatAppearance.MouseDownBackColor = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
        '    .FlatAppearance.MouseOverBackColor = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
        '    .Margin = New Padding(0, 0, 10, 0)
        '    .Padding = New Padding(0)
        '    .Width = 25
        '    .Image = Theme.Icon("DROPDOWN", New Size(16, 16), Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE))
        '    .Text = ""
        '    .Visible = False
        '    .BringToFront()
        'End With

        'Client Hosting Box
        PnlClientArea = New Panel
        AddHandler PnlClientArea.Click, AddressOf DockBox_SetFocus
        pnlMainBox.Controls.Add(PnlClientArea)
        With PnlClientArea
            .BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
            .Dock = DockStyle.Fill
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            .Visible = True
            .BringToFront()
        End With

    End Sub

    Public Sub AddItem(Key As String, item As jBoxViewItem)
        AddHandler item.FocusRecieved, AddressOf RaiseFocusEvent
        item.Key = Key
        items.Add(Key, item)
        SelectItem(Key)
    End Sub

    Public Sub RemoveItem(Key As String)
        If items.ContainsKey(Key) Then
            items.Remove(Key)
            If items.Count = 0 Then

                RaiseEvent NoItems(BoxLocation)
                Exit Sub
            End If
            SelectItem(items.Keys(0))
        End If
    End Sub
    Public Sub SelectItem(Key As String)
        _SelectedKey = Key
        reDraw()
        RaiseEvent ItemSelected(BoxLocation, Key)
        HasFocus = True
    End Sub

    Public Sub CloseItem()
        RemoveItem(SelectedItemKey)
    End Sub

    Public Sub RaiseFocusEvent()
        'Debug.Print("JBoxContainer_RaiseFocusEvent({0})", BoxLocation)
        RaiseEvent FocusRecieved(BoxLocation)
    End Sub

    Private Sub Container_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged
        'Debug.Print("DockBox_ParentChanged")
        With Me
            .BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .Dock = DockStyle.Fill
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
            '.Visible = True
        End With
    End Sub

    Private Sub Container_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        HasFocus = True
    End Sub

    Private Sub Container_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        HasFocus = False
    End Sub

    Private Sub ApplyFocus()
        Select Case ViewItemContainerType
            Case jBoxContainerType.DockBox
                If ClientControl IsNot Nothing Then
                    Try
                        CType(ClientControl, jBoxViewItem).HasFocus = HasFocus
                    Catch ex As Exception

                    End Try
                End If
                If HasFocus Then
                    If selectedTab IsNot Nothing Then
                        selectedTab.BorderColorBottom = Theme.HighlightColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
                    End If
                    pnlMainBox.BorderColorTop = Theme.HighlightColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                    PnlHeader.BorderColorTop = Theme.HighlightColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                    PnlHeader.BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                    LblHeader.ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                Else
                    If selectedTab IsNot Nothing Then
                        selectedTab.BorderColorBottom = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                    End If
                    pnlMainBox.BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
                    PnlHeader.BorderColorTop = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
                    PnlHeader.BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
                    LblHeader.ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
                End If
                If selectedTab IsNot Nothing Then
                    selectedTab.Invalidate()
                End If
                pnlMainBox.Refresh()
            Case jBoxContainerType.TabDockBox
                If ClientControl IsNot Nothing Then
                    Try
                        CType(ClientControl, jBoxViewItem).HasFocus = HasFocus
                    Catch ex As Exception

                    End Try
                End If
                If HasFocus Then
                    If selectedTab IsNot Nothing Then
                        selectedTab.BorderColorTop = Theme.HighlightColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
                    End If
                    pnlMainBox.BorderColorTop = Theme.HighlightColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
                Else
                    If selectedTab IsNot Nothing Then
                        selectedTab.BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                    End If
                    pnlMainBox.BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                End If
                pnlTabBar.Invalidate()
                pnlMainBox.Invalidate()
                pnlTabBar.Refresh()
                pnlMainBox.Refresh()
            Case Else

        End Select
    End Sub

    Private Sub Container_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        If LblHeader IsNot Nothing Then
            LblHeader.Text = Text
        End If
    End Sub

    Private Sub DockBox_SetFocus()
        If HasFocus Then Exit Sub
        HasFocus = True
    End Sub

    'DOCK CODE
    Private Sub BtnCloseHeader_Click(sender As Object, e As EventArgs) Handles BtnCloseHeader.Click
        RaiseEvent CloseClicked(BoxLocation)
    End Sub

    'TAB CODE
    Private Sub BtnCloseClick(sender As Object, e As EventArgs)
        RemoveItem(sender.tag)
    End Sub

    Private Sub btnDropDownHeader_Click(sender As Object, e As EventArgs) Handles btnDropDownHeader.Click
        RaiseEvent ContextMenuClicked(BoxLocation)
    End Sub

    Private Sub reDraw()
        Select Case ViewItemContainerType
            Case jBoxContainerType.DockBox
                ClientControl = items(SelectedItemKey)
                With CType(ClientControl, jBoxViewItem)
                        BtnCloseHeader.Enabled = .ShowClose
                        LblHeader.Text = IIf(.HeaderText.Length > 0, .HeaderText, .Caption)
                        pnlSearch.Visible = .ShowSearch
                        pnlToolbar.Visible = .ShowToolbar
                    End With
                    If items.Count > 1 Then
                        pnlMainBox.SuspendLayout()
                        pnlMainBox.BorderWidthBottom = 0
                        With pnlTabBar
                            .SuspendLayout()
                            .Controls.Clear()
                            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                            .Visible = True
                        End With
                        For i As Integer = 0 To items.Count - 1
                            Dim myCaption As String = CType(items.ElementAt(i).Value, jBoxViewItem).Caption
                            Dim myKey As String = CType(items.ElementAt(i).Value, jBoxViewItem).Key
                            Dim isSelected As Boolean = myKey.Equals(_SelectedKey)
                            Dim pnlLbl As New Label
                            With pnlLbl
                                .Text = myCaption
                                .AutoSize = True
                                .Tag = myKey
                                If isSelected Then
                                    .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                                Else
                                    .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
                                    .AutoEllipsis = True
                                End If
                                .Dock = DockStyle.Fill
                            End With
                            Dim pnlTab As New jPanel
                            If isSelected Then selectedTab = pnlTab
                            pnlTabBar.Controls.Add(pnlTab)
                            With pnlTab
                                .OnHoverEventsEnabled = True
                                .Controls.Add(pnlLbl)
                                .BorderColorLeft = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                                .BorderColorRight = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                                If isSelected Then
                                    .BorderColorTop = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                                    If HasFocus Then
                                        .BorderColorBottom = Theme.HighlightColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
                                    Else
                                        .BorderColorBottom = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                                    End If
                                    .BorderWidthTop = 1
                                    .BorderWidthBottom = 1
                                    .BorderWidthLeft = 1
                                    .BorderWidthRight = 1
                                Else
                                    .BorderColorBottom = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                                    .BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                                    .BorderWidthTop = 1
                                    .BorderWidthBottom = 1
                                    .BorderWidthLeft = 0
                                    .BorderWidthRight = 0
                                End If

                                .AutoSize = True
                                .MinimumSize = New Size(50, 20)
                                .Dock = DockStyle.Left
                            End With
                            If Not isSelected Then
                                AddHandler pnlTab.OnHover, AddressOf TabMouseHover
                                'AddHandler pnlLbl.MouseEnter, AddressOf TabMouseOver
                                AddHandler pnlLbl.Click, AddressOf TabMouseClick
                            End If
                        Next
                        Dim pnl As New jPanel
                        With pnl
                            .BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                            .BorderWidthBottom = 0
                            .BorderWidthLeft = 0
                            .BorderWidthRight = 0
                            .BorderWidthTop = 1
                            .Dock = DockStyle.Fill
                        End With
                        With pnlTabBar
                            .Controls.Add(pnl)
                            .ResumeLayout(False)
                            '.BringToFront()
                            .PerformLayout()
                        End With
                        pnlMainBox.ResumeLayout(False)
                        pnlMainBox.PerformLayout()
                    Else
                        pnlTabBar.Visible = False
                        pnlMainBox.BorderWidthBottom = 1
                    End If
            Case jBoxContainerType.TabDockBox
                ClientControl = items(SelectedItemKey)
                With CType(ClientControl, jBoxViewItem)
                        pnlSearch.Visible = .ShowSearch
                        pnlToolbar.Visible = .ShowToolbar
                    End With
                    With pnlTabBar
                        .SuspendLayout()
                        .Controls.Clear()
                        .BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
                        .Visible = True
                    End With
                    For i As Integer = 0 To items.Count - 1
                    Dim myCaption As String = CType(items.ElementAt(i).value, jBoxViewItem).Caption
                    Dim myKey As String = CType(items.ElementAt(i).Value, jBoxViewItem).Key
                        Dim isSelected As Boolean = myKey.Equals(_SelectedKey)
                        Dim pnlLbl As New Label
                        With pnlLbl
                            .Text = myCaption
                            .Margin = New Padding(0)
                            .AutoSize = True
                            .Tag = myKey
                            If isSelected Then
                                .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                            Else
                                .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.INACTIVE)
                                .AutoEllipsis = True
                            End If
                            .Dock = DockStyle.Fill
                        End With
                        Dim BtnClose As New jButton
                        AddHandler BtnClose.Click, AddressOf BtnCloseClick
                        AddHandler BtnClose.GotFocus, AddressOf RedirectFocus
                        With BtnClose
                            .Dock = DockStyle.Right
                            .FlatStyle = FlatStyle.Flat
                            .FlatAppearance.BorderSize = 0
                            .BackColor = Color.Transparent
                            .Margin = New Padding(0, 0, 5, 0)
                            .Padding = New Padding(0)
                            .Tag = myKey
                            .Width = 17
                            .HoverBackColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                            .Icon = Theme.Icon("X", New Size(16, 16), Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE))
                            .HoverIcon = Theme.Icon("X", New Size(16, 16), Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE))
                            .Text = ""
                            .Visible = False
                            .BringToFront()
                        End With

                        Dim pnlTab As New jPanel
                        If isSelected Then selectedTab = pnlTab
                        pnlTabBar.Controls.Add(pnlTab)
                        With pnlTab
                            .OnHoverEventsEnabled = True
                            .Controls.Add(pnlLbl)
                            .Controls.Add(BtnClose)
                            .Tag = myKey
                            .BorderWidthBottom = 0
                            .BorderWidthLeft = 1
                            .BorderWidthRight = 1
                            If isSelected Then
                                .BackColor = Theme.BackColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED)
                                .BorderColorLeft = Theme.BorderColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED)
                                .BorderColorRight = Theme.BackColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED)
                                If HasFocus Then
                                    .BorderColorTop = Theme.HighlightColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.ACTIVE)
                                Else
                                    .BorderColorTop = Theme.HighlightColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.INACTIVE)
                                End If
                                .BorderWidthTop = 2
                                pnlLbl.Padding = New Padding(4, 2, 0, 0)
                                BtnClose.Visible = True
                            Else
                                .BackColor = Theme.BackColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.UNSELECTED)
                                .BorderColorTop = Theme.BorderColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.UNSELECTED)
                                .BorderWidthTop = 1
                                pnlLbl.Padding = New Padding(4, 3, 0, 0)
                                .BorderColorLeft = Theme.BorderColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.UNSELECTED)
                                .BorderColorRight = Theme.BorderColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.UNSELECTED)
                            End If

                            .AutoSize = True
                            .MinimumSize = New Size(100, 15)
                            .Dock = DockStyle.Left
                        End With
                        If Not isSelected Then
                            AddHandler pnlTab.OnHover, AddressOf TabMouseHover
                            AddHandler pnlTab.Click, AddressOf TabMouseClick
                            AddHandler pnlLbl.Click, AddressOf TabMouseClick
                        End If
                    Next
                    With pnlTabBar
                        .ResumeLayout(False)
                        .PerformLayout()
                    End With
            Case Else

        End Select
    End Sub

    Private Sub Container_FocusRecieved(BoxPlacement As jBoxPlacementType) Handles Me.FocusRecieved
        DockBox_SetFocus()
    End Sub

    Private Sub RedirectFocus()
        Focus()
    End Sub

    Private Sub TabMouseHover(sender As Control, isHovering As Boolean)
        Select Case ViewItemContainerType
            Case jBoxContainerType.DockBox
                If isHovering Then
                    sender.BackColor = Theme.BorderColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                Else
                    sender.BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
                End If
            Case jBoxContainerType.TabDockBox
                For Each ctl In sender.Controls
                    If ctl.GetType.Name.Equals("jButton") Then
                        ctl.Visible = isHovering
                        Exit For
                    End If
                Next
                If isHovering Then
                    sender.BackColor = Theme.BackColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED)
                Else
                    sender.BackColor = Theme.BackColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.UNSELECTED)
                End If
            Case Else
                'Not sure yet
        End Select
    End Sub

    Private Sub TabMouseClick(sender As Object, e As EventArgs)
        SelectItem(sender.tag)
    End Sub

    Private Sub Container_NoItems(BoxPlacement As jBoxPlacementType) Handles Me.NoItems
        Dim pnl As New jPanel
        pnl.BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
        pnl.Dock = DockStyle.Fill
        ClientControl = pnl
    End Sub

    'DOCK CODE
    Private Sub pnlSearch_SearchRequest(searchCriteria As String) Handles pnlSearch.SearchRequest
        CType(ClientControl, jBoxViewItem).SearchCriteria = searchCriteria
    End Sub

    'DOCK CODE
    Private Sub pnlSearch_SearchCleared() Handles pnlSearch.SearchCleared
        CType(ClientControl, jBoxViewItem).SearchCriteria = ""
    End Sub
End Class
