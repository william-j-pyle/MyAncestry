Public Class jMenuBar
    Inherits jBar

    Private WithEvents strip As New MenuStrip

    Public Sub New()
        Padding = New Padding(0)
        Margin = New Padding(0)
        BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
        AutoSize = True
        Height = 23
        Dock = DockStyle.Top
        Controls.Add(strip)
        With strip
            .Renderer = New MenuStripRenderer()
            .BackColor = BackColor
            .LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            .Location = New System.Drawing.Point(0, 0)
            .Name = "jMenuBar"
            .Text = ""
            .Dock = DockStyle.Top
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

    Private Function addMenuItem(barItem As jBarItemConfig) As ToolStripMenuItem
        Dim Item As ToolStripMenuItem = New ToolStripMenuItem()
        barItem.StripItem = Item
        With Item
            .Tag = barItem
            .ForeColor = Config.Theme.ForeColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            .BackColor = Config.Theme.BackColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            .DropDown.BackColor = Config.Theme.BackColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            .DropDown.ForeColor = Config.Theme.ForeColor(ThemeItemType.MENUBAR, ThemeColorItemState.ACTIVE)
            If barItem.ItemType = BarItemType.CheckedButton Then
                .CheckOnClick = True
            End If
            If barItem.IconKey <> "" Then
                .Image = Config.Theme.Icon(barItem.IconKey)
            End If
            .DropDown.AutoSize = True
            .Name = barItem.ItemKey
            .Text = barItem.Caption
        End With
        AddHandler Item.Click, AddressOf Item_Clicked
        AddHandler Item.EnabledChanged, AddressOf Item_EnabledChanged
        AddHandler Item.CheckedChanged, AddressOf Item_CheckedChanged
        AddHandler Item.VisibleChanged, AddressOf Item_VisibleChanged
        Return Item
    End Function


    Private Sub jMenuBar_ItemAdded(ItemKey As String) Handles Me.ItemAdded
        Dim barItem As jBarItemConfig = Items(ItemKey)
        Dim item As ToolStripItem

        Select Case barItem.ItemType
            Case BarItemType.Button
                item = addMenuItem(barItem)
            Case BarItemType.CheckedButton
                item = addMenuItem(barItem)
            Case BarItemType.Seperator
                item = addSeperatorItem(barItem)
            Case Else
                item = addMenuItem(barItem)
        End Select

        If barItem.ParentItemKey = "" Then
            strip.Items.Add(Item)
        Else
            Dim items() As ToolStripItem = strip.Items.Find(barItem.ParentItemKey, True)
            If items.Length > 0 Then
                Dim parentItem As ToolStripMenuItem = items(0)
                parentItem.DropDownItems.Add(Item)
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

    Private Sub strip_MouseDown(sender As Object, e As MouseEventArgs) Handles strip.MouseDown
        OnMouseDown(e)
    End Sub

    Private Sub strip_MouseEnter(sender As Object, e As EventArgs) Handles strip.MouseEnter
        OnMouseEnter(e)
    End Sub

    Private Sub strip_MouseUp(sender As Object, e As MouseEventArgs) Handles strip.MouseUp
        OnMouseUp(e)
    End Sub

    Private Sub strip_MouseMove(sender As Object, e As MouseEventArgs) Handles strip.MouseMove
        OnMouseMove(e)
    End Sub
End Class
