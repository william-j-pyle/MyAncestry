Public Class jStatusBar
    Inherits jBar

    Private WithEvents strip As New StatusStrip

    Public Sub New()
        Padding = New Padding(0)
        Margin = New Padding(0)
        BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.INACTIVE)
        AutoSize = True
        Height = 23
        Dock = DockStyle.Top
        Controls.Add(strip)
        With strip
            .Renderer = New StatusStripRenderer()
            .GripStyle = ToolStripGripStyle.Hidden
            .SizingGrip = False
            .BackColor = BackColor
            .LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            .Location = New System.Drawing.Point(0, 0)
            .Name = "jStatusBar"
            .Text = ""
            .Dock = DockStyle.Fill
        End With
    End Sub

    Private Function addSeperatorItem(barItem As jBarItemConfig) As ToolStripSeparator
        Dim Item As New ToolStripSeparator()
        barItem.StripItem = Item
        With Item
            .Tag = barItem
            .ForeColor = Config.Theme.ForeColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            .BackColor = Config.Theme.BackColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            .Name = barItem.ItemKey
        End With
        Return Item
    End Function

    Private Function addLabelItem(barItem As jBarItemConfig) As ToolStripLabel
        Dim Item As New ToolStripLabel()
        barItem.StripItem = Item
        With Item
            .Tag = barItem
            .ForeColor = Config.Theme.ForeColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            .BackColor = Config.Theme.BackColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            '.DropDown.BackColor = Theme.BackColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            '.DropDown.ForeColor = Theme.ForeColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            If barItem.IconKey <> "" Then
                .Image = Config.Theme.Icon(barItem.IconKey)
            End If
            .Name = barItem.ItemKey
            .Text = barItem.Caption
            .ImageAlign = ContentAlignment.MiddleLeft
            .TextImageRelation = TextImageRelation.ImageBeforeText
        End With
        Return Item
    End Function

    Private Function addButtonItem(barItem As jBarItemConfig) As ToolStripButton
        Dim Item As New ToolStripButton()
        barItem.StripItem = Item
        With Item
            .Tag = barItem
            .ForeColor = Config.Theme.ForeColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            .BackColor = Config.Theme.BackColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            '.DropDown.BackColor = Theme.BackColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            '.DropDown.ForeColor = Theme.ForeColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            If barItem.ItemType = BarItemType.CheckedButton Then
                .CheckOnClick = True
            End If
            If barItem.IconKey <> "" Then
                .Image = Config.Theme.Icon(barItem.IconKey)
            End If
            .Name = barItem.ItemKey
            '.Text = barItem.Caption
        End With
        AddHandler Item.Click, AddressOf Item_Clicked
        AddHandler Item.EnabledChanged, AddressOf Item_EnabledChanged
        AddHandler Item.CheckedChanged, AddressOf Item_CheckedChanged
        AddHandler Item.VisibleChanged, AddressOf Item_VisibleChanged
        Return Item
    End Function


    Private Sub jStatusBar_ItemAdded(ItemKey As String) Handles Me.ItemAdded
        Dim barItem As jBarItemConfig = Items(ItemKey)
        Dim item As ToolStripItem

        Select Case barItem.ItemType
            Case BarItemType.Button
                item = addButtonItem(barItem)
            Case BarItemType.CheckedButton
                item = addButtonItem(barItem)
            Case BarItemType.Seperator
                item = addSeperatorItem(barItem)
            Case BarItemType.Label
                item = addLabelItem(barItem)
            Case Else
                item = addButtonItem(barItem)
        End Select

        If barItem.ParentItemKey = "" Then
            strip.Items.Add(item)
        Else
            Dim items() As ToolStripItem = strip.Items.Find(barItem.ParentItemKey, True)
            If items.Length > 0 Then
                Dim parentItem As ToolStripMenuItem = items(0)
                parentItem.DropDownItems.Add(item)
            End If
        End If
    End Sub


    Private Sub Item_Clicked(sender As Object, e As EventArgs)
        OnItemClicked(CType(sender.tag, jBarItemConfig).ItemKey)
    End Sub

    Private Sub Item_EnabledChanged(sender As Object, e As EventArgs)
        OnItemStateChanged(CType(sender.tag, jBarItemConfig).ItemKey, BarStateType.Enabled, sender.enabled)
    End Sub

    Private Sub Item_CheckedChanged(sender As Object, e As EventArgs)
        OnItemStateChanged(CType(sender.tag, jBarItemConfig).ItemKey, BarStateType.Checked, sender.checked)
    End Sub

    Private Sub Item_VisibleChanged(sender As Object, e As EventArgs)
        OnItemStateChanged(CType(sender.tag, jBarItemConfig).ItemKey, BarStateType.Visible, sender.visible)
    End Sub

End Class
