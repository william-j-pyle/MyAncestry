Public Class jApp
    Inherits jPanel

    'Application Level Items
    Private Config As jConfig = My.Application.Config

    'Initalization Constants
    'HACK: This should be placed some different way
    Private Const FORM_WIDTH = 800
    Private Const FORM_HEIGHT = 600
    Private Const FORM_BORDER = 2
    Private Const FORM_BAR_HEIGHT = 24

    'Internal State Tracking
    Private MouseX As Integer
    Private MouseY As Integer

    Private FormInitialWindowState As FormWindowState
    Private FormInitialX As Integer
    Private FormInitialY As Integer
    Private FormInitialWidth As Integer
    Private FormInitialHeight As Integer
    Private FormResizeWidthFlag As Integer
    Private FormResizeHeightFlag As Integer

    Private IsFormDragging As Boolean
    Private IsFormResizing As Boolean

    'Create Controls
    Private WithEvents JFormHeaderBar As jPanel
    Private WithEvents JFormControlBox As jPanel
    Private WithEvents BtnMinimize As jButton
    Private WithEvents BtnMaximize As jButton
    Private WithEvents BtnClose As jButton
    Private WithEvents JFormIcon As jPanel
    Public WithEvents ToolBar As jToolBar
    Public WithEvents MenuBar As jMenuBar
    Public WithEvents StatusBar As jStatusBar
    Private WithEvents Views As jBoxViews
    Private WithEvents FormIcon As PictureBox
    Private WithEvents Frm As Form                                  'Alias Holder of Parent Form
    Private WithEvents Timer As Timer

    'Define Custom Events
    Public Event AppClose()
    Public Event AppWinMinimized()
    Public Event AppWinRestored()
    Public Event AppWinMaximized()
    Public Event AppWinResized()

    Public Event ToolbarItemClicked(ItemKey As String)


    'Define Custom Properties
    Public Property ActiveViewKey As String
        Get
            Return Views.ActiveViewKey
        End Get
        Set(value As String)
            Views.ActiveViewKey = value
        End Set
    End Property


    'As this Class depends on the base for to successfully initialize, it will not Init until the parent is assigned
    Private Sub jApp_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged

        Timer = New Timer()
        Timer.Interval = 100

        'Clear Up the Forms Configuration
        'No Borders, No Padding, No Margin, and correct backcolor
        Frm = Parent
        With Frm
            .Padding = New Padding(0)
            .Margin = New Padding(0)
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .FormBorderStyle = FormBorderStyle.None
            FormInitialWindowState = .WindowState
        End With

        'The form knows its size, lock this overlay onto the forms size
        Location = New Point(0, 0)
        Margin = New Padding(0)
        Size = Frm.Size
        Dock = DockStyle.Fill

        'Setup and Assign Controls
        BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
        BorderColorBottom = Config.Theme.BackColor(ThemeItemType.STATUSBAR, ThemeColorItemState.ACTIVE)
        BorderColorLeft = BackColor
        BorderColorRight = BackColor
        BorderColorTop = BackColor
        BorderWidthBottom = FORM_BORDER
        BorderWidthLeft = FORM_BORDER
        BorderWidthRight = FORM_BORDER
        BorderWidthTop = FORM_BORDER
        Padding = New System.Windows.Forms.Padding(FORM_BORDER)
        ResumeLayout(False)
        PerformLayout()

        'Suspend all container layouts so we can draw all of this mess
        SuspendLayout()

        Views = New jBoxViews()
        Controls.Add(Views)

        ToolBar = New jToolBar()
        Controls.Add(ToolBar)

        JFormHeaderBar = New jPanel()
        Controls.Add(JFormHeaderBar)
        With JFormHeaderBar
            .AutoSize = True
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .BorderWidthBottom = 0
            .BorderWidthLeft = 0
            .BorderWidthRight = 0
            .BorderWidthTop = 0
            .Dock = System.Windows.Forms.DockStyle.Top
            .Location = New System.Drawing.Point(0, 0)
            .Name = "JFormHeaderBar"
            .Size = New System.Drawing.Size(FORM_WIDTH, FORM_BAR_HEIGHT)
            '.TabIndex = 4
        End With

        MenuBar = New jMenuBar()
        JFormHeaderBar.Controls.Add(MenuBar)

        JFormControlBox = New jPanel()
        JFormHeaderBar.Controls.Add(JFormControlBox)
        With JFormControlBox
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .BorderWidthBottom = 0
            .BorderWidthLeft = 0
            .BorderWidthRight = 0
            .BorderWidthTop = 0
            .Dock = System.Windows.Forms.DockStyle.Right
            .Location = New System.Drawing.Point(0, 0)
            .Name = "JFormControlBox"
            .Size = New System.Drawing.Size(FORM_BAR_HEIGHT * 3, FORM_BAR_HEIGHT)
            '.TabIndex = 1
        End With

        BtnMinimize = New jButton()
        JFormControlBox.Controls.Add(BtnMinimize)
        With BtnMinimize
            .Dock = DockStyle.Right
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .ForeColor = Config.Theme.ForeColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .Width = 25
            .Icon = Config.Theme.Icon("MINIMIZE",, .ForeColor)
            .HoverBackColor = .ForeColor
            .HoverIcon = Config.Theme.Icon("MINIMIZE",, .BackColor)
            .Width = 25
            .Location = New System.Drawing.Point(46, 6)
            .Name = "BtnMinimize"
            .TabIndex = 0
            .UseVisualStyleBackColor = False
        End With

        BtnMaximize = New jButton()
        JFormControlBox.Controls.Add(BtnMaximize)
        With BtnMaximize
            .Dock = DockStyle.Right
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .ForeColor = Config.Theme.ForeColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .Width = 25
            .Icon = Config.Theme.Icon("MAXIMIZE",, .ForeColor)
            .HoverBackColor = .ForeColor
            .HoverIcon = Config.Theme.Icon("MAXIMIZE",, .BackColor)
            .Location = New System.Drawing.Point(46, 6)
            .Name = "BtnMaximize"
            .TabIndex = 0
            .UseVisualStyleBackColor = False
        End With

        BtnClose = New jButton()
        JFormControlBox.Controls.Add(BtnClose)
        With BtnClose
            .Dock = DockStyle.Right
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .ForeColor = Config.Theme.ForeColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .Width = 25
            .Icon = Config.Theme.Icon("X",, .ForeColor)
            .HoverBackColor = .ForeColor
            .HoverIcon = Config.Theme.Icon("X",, .BackColor)
            .Width = 25
            .Location = New System.Drawing.Point(46, 6)
            .Name = "BtnClose"
            .TabIndex = 0
            .UseVisualStyleBackColor = False
        End With

        FormIcon = New System.Windows.Forms.PictureBox()
        JFormHeaderBar.Controls.Add(FormIcon)
        With FormIcon
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .Dock = System.Windows.Forms.DockStyle.Left
            .Image = Config.Theme.Icon("APP")
            .Location = New System.Drawing.Point(0, 0)
            .Margin = New System.Windows.Forms.Padding(0)
            .Name = "FormIcon"
            .Size = New System.Drawing.Size(32, 32)
            .SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            '.TabIndex = 0
            '.TabStop = False
        End With

        StatusBar = New jStatusBar
        With StatusBar
            .BackColor = Config.Theme.BackColor(ThemeItemType.STATUSBAR, ThemeColorItemState.ACTIVE)
            .Dock = System.Windows.Forms.DockStyle.Bottom
        End With
        Controls.Add(StatusBar)

        ResumeLayout(False)
        PerformLayout()

        Application.DoEvents()

        LoadSettings()

        CreateMainMenuBar()
        CreateMainToolBar()
        CreateMainStatusbar()

    End Sub


    Private Sub CreateMainMenuBar()
        CreateBar(Config.MenuBars, MenuBar)
    End Sub

    Private Sub CreateMainToolBar()
        CreateBar(Config.ToolBars, ToolBar)
    End Sub

    Private Sub CreateMainStatusbar()
        CreateBar(Config.StatusBar, StatusBar)
    End Sub

    Private Sub CreateBar(bar As List(Of cfgBarItem), obj As jBar)
        Dim barItem As jBarItemConfig
        For Each itm As cfgBarItem In bar
            barItem = New jBarItemConfig
            barItem.ItemKey = itm.Key
            barItem.ParentItemKey = itm.ParentKey
            barItem.Caption = itm.Caption
            barItem.GroupKey = itm.GroupKey
            barItem.IconKey = itm.IconKey
            barItem.ItemType = BarItemTypeEnumFromString(itm.Type)
            For Each flg As String In itm.Flags
                Select Case flg
                    Case "Checked"
                        barItem.Checked = True
                    Case "Visible"
                        barItem.Visible = True
                    Case "Enabled"
                        barItem.Enabled = True
                    Case "Disabled"
                        barItem.Enabled = False
                    Case Else
                        'Shouldnt be here...
                        Throw New Exception("Unknown Flag in Create Bar: " & flg)
                End Select
            Next
            obj.AddItem(barItem)
        Next
    End Sub

    Private Sub jApp_WinMinimize() Handles BtnMinimize.Click
        Frm.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub jApp_WinMaximize() Handles MenuBar.DoubleClick, BtnMaximize.Click
        If Frm.WindowState = FormWindowState.Maximized Then
            Frm.WindowState = FormWindowState.Normal
        Else
            Frm.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub jApp_WinClose() Handles BtnClose.Click
        RaiseEvent AppClose()
        Frm.Close()
    End Sub

    Public Sub SaveSettings() Handles Frm.FormClosing
        Views.SaveLayout()
        'Save Form Size
        Config.Properties.PutProperty("FORM.WIDTH", Frm.Width)
        Config.Properties.PutProperty("FORM.HEIGHT", Frm.Height)
        Config.Properties.PutProperty("FORM.TOP", Frm.Top)
        Config.Properties.PutProperty("FORM.LEFT", Frm.Left)
    End Sub

    Public Sub LoadSettings()
        'Load Form Size
        Frm.Width = Config.Properties.GetProperty("FORM.WIDTH", Frm.Width)
        Frm.Height = Config.Properties.GetProperty("FORM.HEIGHT", Frm.Height)
        Frm.Top = Config.Properties.GetProperty("FORM.TOP", Frm.Top)
        Frm.Left = Config.Properties.GetProperty("FORM.LEFT", Frm.Left)

        Views.LoadLayout()

        If Views.ActiveView IsNot Nothing Then
            'Default Placement to Tab Box onload
            Try
                Views.ActiveView.HasFocus(jBoxPlacementType.BoxTopMiddle) = True
            Catch ex As Exception
                Debug.Print("Loaded settings before ActiveContainer was populated????")
            End Try
        End If
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        'Debug.Print("Timer: Pos({0},{1})  BTN({2})", MousePosition.X, MousePosition.Y, MouseButtons)
        If IsFormDragging Then
            If MouseButtons = MouseButtons.None Then
                jApp_FormDragging("MouseUp")
            Else
                jApp_FormDragging("MouseMove")
            End If
        End If
    End Sub

    Private Sub jApp_FormDragging(triggerName As String)
        'Debug.Print("jApp_FormDragging: {0}", triggerName)

        Select Case triggerName
            Case "MouseDown"
                'Debug.Print("jApp_FormDragging_MouseDown: ({0},{1})", MousePosition.X, MousePosition.Y)
                MouseY = MousePosition.Y
                MouseX = MousePosition.X
                FormInitialX = Frm.Left
                FormInitialY = Frm.Top
                IsFormDragging = True
                Timer.Start()
            Case "MouseUp"
                Timer.Stop()
                IsFormDragging = False
            Case "MouseMove"
                'Debug.Print("jApp_FormDragging_MouseMove: ({0},{1})", MousePosition.X, MousePosition.Y)
                Frm.Left = MousePosition.X - MouseX + FormInitialX
                Frm.Top = MousePosition.Y - MouseY + FormInitialY
            Case Else

        End Select
    End Sub

    Private Sub jApp_FormDragging_MouseDown(sender As Object, e As MouseEventArgs) Handles MenuBar.MouseDown
        If IsFormResizing Then Exit Sub
        jApp_FormDragging("MouseDown")
    End Sub

    Private Sub jApp_FormDragging_MouseUp(sender As Object, e As MouseEventArgs) Handles MenuBar.MouseUp
        jApp_FormDragging("MouseUp")
    End Sub

    Private Sub jAppFormResizing(triggerName As String)
        'Debug.Print("jAppFormResizing: {0}", triggerName)
        'Debug.Print("Mouse({0},{1})  Form({2},{3},{4},{5})", MousePosition.X, MousePosition.Y, Frm.Left, Frm.Top, Frm.Width, Frm.Height)
        Select Case triggerName
            Case "MouseDown"
                jApp_FormResizing_Initialize()
                IsFormResizing = True
                MouseY = MousePosition.Y
                MouseX = MousePosition.X
                FormInitialX = Frm.Left
                FormInitialY = Frm.Top
                FormInitialWidth = Frm.Width
                FormInitialHeight = Frm.Height
            Case "MouseUp"
                IsFormResizing = False
                RaiseEvent AppWinResized()
            Case "MouseMove"
                Dim adjWidth = MousePosition.X - MouseX
                Dim adjHeight = MousePosition.Y - MouseY
                'Only do the adjustment if moved more than 10px
                If Math.Abs(adjWidth) > 10 Then
                    Select Case FormResizeWidthFlag
                        Case 1
                            'Adjust Form
                            Frm.Width = adjWidth + FormInitialWidth
                            'Update Tracking Data
                            'FormInitialWidth = Frm.Width
                            'MouseX = MousePosition.X
                            'Make sure all events have time to fire!
                            Application.DoEvents()
                        Case -1
                            'Adjust Form
                            Frm.Width = FormInitialWidth + (adjWidth * -1)
                            Frm.Left = FormInitialX + adjWidth
                            'Update Tracking Data
                            'FormInitialX = Frm.Left
                            'FormInitialWidth = Frm.Width
                            'MouseX = MousePosition.X
                            'Make sure All events have time to fire!
                            Application.DoEvents()
                        Case Else
                            'NO UPDATE
                    End Select
                End If
                'Only do the adjustment if moved more than 10px
                If Math.Abs(adjHeight) > 10 Then
                    Select Case FormResizeHeightFlag
                        Case 1
                            Frm.Height = adjHeight + FormInitialHeight
                            'Update Tracking Data
                            'FormInitialHeight = Frm.Height
                            'MouseY = MousePosition.Y
                            'Make sure All events have time to fire!
                            Application.DoEvents()
                        Case -1
                            Frm.Height = FormInitialHeight + (adjHeight * -1)
                            Frm.Top = FormInitialY + adjHeight
                            'Update Tracking Data
                            'FormInitialY = Frm.Top
                            'FormInitialHeight = Frm.Height
                            'MouseY = MousePosition.Y
                            'Make sure All events have time to fire!
                            Application.DoEvents()
                        Case Else
                            'NO UPDATE
                    End Select
                End If
            Case Else

        End Select
    End Sub

    Private Sub jApp_FormResizing_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        jAppFormResizing("MouseDown")
    End Sub

    Private Sub jApp_FormResizing_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If IsFormResizing Then
            jAppFormResizing("MouseMove")
        End If
    End Sub

    Private Sub jApp_FormResizing_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If IsFormResizing Then
            jAppFormResizing("MouseUp")
        End If
    End Sub

    Private Sub jApp_FormResizing_Initialize()
        Dim pt As Point = PointToClient(New Point(MousePosition.X, MousePosition.Y))
        'Upper Left
        If pt.X < 10 And pt.Y < 10 Then
            FormResizeWidthFlag = -1
            FormResizeHeightFlag = -1
            Cursor = Cursors.SizeNWSE
            Exit Sub
        End If
        'Lower Right
        If pt.X > Width - 10 And pt.Y > Height - 10 Then
            FormResizeWidthFlag = 1
            FormResizeHeightFlag = 1
            Cursor = Cursors.SizeNWSE
            Exit Sub
        End If
        'Upper Right
        If pt.X > Width - 10 And pt.Y < 10 Then
            FormResizeWidthFlag = 1
            FormResizeHeightFlag = -1
            Cursor = Cursors.SizeNESW
            Exit Sub
        End If
        'Lower Left
        If pt.X < 10 And pt.Y > Height - 10 Then
            FormResizeWidthFlag = -1
            FormResizeHeightFlag = 1
            Cursor = Cursors.SizeNESW
            Exit Sub
        End If

        'Top Bar
        If pt.Y < 5 Then
            FormResizeWidthFlag = 0
            FormResizeHeightFlag = -1
            Cursor = Cursors.SizeNS
            Exit Sub
        End If
        'Lower Bar
        If pt.Y > Height - 5 Then
            FormResizeWidthFlag = 0
            FormResizeHeightFlag = 1
            Cursor = Cursors.SizeNS
            Exit Sub
        End If

        'Left Bar
        If pt.X < 10 Then
            FormResizeWidthFlag = -1
            FormResizeHeightFlag = 0
            Cursor = Cursors.SizeWE
            Exit Sub
        End If
        'Right Bar
        If pt.X > Width - 10 Then
            FormResizeWidthFlag = 1
            FormResizeHeightFlag = 0
            Cursor = Cursors.SizeWE
            Exit Sub
        End If
    End Sub

    Private Sub jApp_FormResizing_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        If IsFormResizing Then Exit Sub
        jApp_FormResizing_Initialize()
    End Sub

    Private Sub jApp_FormResizing_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If IsFormResizing Then Exit Sub
        Cursor = Cursors.Default
    End Sub

    Private Sub jApp_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Frm IsNot Nothing Then
            If Frm.WindowState = FormInitialWindowState Then Exit Sub
            If Frm.WindowState = FormWindowState.Normal And FormInitialWindowState = FormWindowState.Minimized Then
                RaiseEvent AppWinRestored()
            ElseIf Frm.WindowState = FormWindowState.Normal And FormInitialWindowState = FormWindowState.Maximized Then
                RaiseEvent AppWinRestored()
            ElseIf Frm.WindowState = FormWindowState.Minimized And FormInitialWindowState = FormWindowState.Normal Then
                RaiseEvent AppWinMinimized()
            ElseIf Frm.WindowState = FormWindowState.Maximized And FormInitialWindowState = FormWindowState.Normal Then
                RaiseEvent AppWinMaximized()
            End If
            FormInitialWindowState = Frm.WindowState
        End If
    End Sub

    Private Sub ToolBar_ItemClicked(ItemKey As String) Handles ToolBar.ItemClicked
        Debug.Print("ToolBar_ItemClicked({0})", ItemKey)
        DoToolbarItemClicked(ItemKey)
    End Sub

    Private Sub DoToolbarItemClicked(ItemKey As String)
        RaiseEvent ToolbarItemClicked(ItemKey)
    End Sub

    Public Event ToolBarItemStateChanged(ItemKey As String, ItemStateType As BarStateType, StateValue As Boolean)

    Private Sub ToolBar_ItemStateChanged(ItemKey As String, ItemStateType As BarStateType, StateValue As Boolean) Handles ToolBar.ItemStateChanged
        DoToolBarItemStateChanged(ItemKey, ItemStateType, StateValue)
    End Sub

    Private Sub DoToolBarItemStateChanged(ItemKey As String, ItemStateType As BarStateType, StateValue As Boolean)
        RaiseEvent ToolBarItemStateChanged(ItemKey, ItemStateType, StateValue)
    End Sub


    Public Event MenubarItemClicked(ItemKey As String)

    Private Sub MenuBar_ItemClicked(ItemKey As String) Handles MenuBar.ItemClicked
        DoMenubarItemClicked(ItemKey)
    End Sub

    Private Sub DoMenubarItemClicked(ItemKey As String)
        RaiseEvent MenubarItemClicked(ItemKey)
    End Sub

    Public Event MenuBarItemStateChanged(ItemKey As String, ItemStateType As BarStateType, StateValue As Boolean)

    Private Sub MenuBar_ItemStateChanged(ItemKey As String, ItemStateType As BarStateType, StateValue As Boolean) Handles MenuBar.ItemStateChanged
        DoMenuBarItemStateChanged(ItemKey, ItemStateType, StateValue)
    End Sub

    Private Sub DoMenuBarItemStateChanged(ItemKey As String, ItemStateType As BarStateType, StateValue As Boolean)
        RaiseEvent MenuBarItemStateChanged(ItemKey, ItemStateType, StateValue)
    End Sub

End Class
