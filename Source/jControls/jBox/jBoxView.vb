Public Class jBoxView
    Inherits Panel

    'Application Level Items
    Private ReadOnly Theme As Theme = My.Application.Config.Theme
    Private ReadOnly Props As Properties = My.Application.Config.Properties
    Private ReadOnly Config As jConfig = My.Application.Config

    Private ready As Boolean = False


    Private Containers As Dictionary(Of jBoxPlacementType, jBoxViewItems)
    Private ContainerVisibility As Dictionary(Of jBoxPlacementType, Boolean)

    'Split Containers Naming Overview
    ' S# = Splitter Number
    ' P# = Panel# withing Splitter
    '  ----------- -------------------------
    ' | S1P1      | S1P2                    |
    ' |  -------  |  ---------- ----------  |
    ' | | S2P1  | | | S3P1     | S3P2     | |
    ' | |       | | |  ------- |  ------- | |
    ' |  -------  | | | S4P1 | | | S5P1 | | |
    ' | | S2P2  | | |  ------  |  ------  | |
    ' | |       | | | | S4P2 | | | S5P2 | | |
    ' |  -------  | |  ------  |  ------  | |
    ' |           |  ---------- ----------  |
    '  ----------- -------------------------  
    Private WithEvents SplitS1 As SplitContainer
    Private WithEvents SplitS2 As SplitContainer
    Private WithEvents SplitS3 As SplitContainer
    Private WithEvents SplitS4 As SplitContainer
    Private WithEvents SplitS5 As SplitContainer

    Private BoxKeys As Dictionary(Of String, jBoxViewItem) = New Dictionary(Of String, jBoxViewItem)

    'Useable Client Areas Overview
    ' --- ---- ---- ---- ---
    '|   | TL | TM | TR |   |
    '| L  ---- ---- ----  R |
    '|   | BL | BM | BR |   |
    ' --- ---- ---- ---- ---
    '|         Unplaced     |
    ' ----------------------

    Private _ViewKey As String
    Public ReadOnly Property ViewKey As String
        Get
            Return Me.Name
        End Get
    End Property

    Public Property HasFocus(BoxPlacement As jBoxPlacementType) As Boolean
        Get
            Return BoxContainer(BoxPlacement).HasFocus
        End Get
        Set(value As Boolean)
            BoxContainer(BoxPlacement).HasFocus = True
        End Set
    End Property

    Public Property IsVisible(BoxPlacement As jBoxPlacementType) As Boolean
        Get
            If ContainerVisibility.ContainsKey(BoxPlacement) Then
                Return ContainerVisibility(BoxPlacement)
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            SetBoxContainerVisibility(BoxPlacement, value)
        End Set
    End Property

    Default Public ReadOnly Property BoxContainer(BoxPlacement As jBoxPlacementType) As jBoxViewItems
        Get
            If Containers.ContainsKey(BoxPlacement) Then
                Return Containers(BoxPlacement)
            Else
                Return Nothing
            End If
        End Get
    End Property

    'Private ReadOnly Property BoxParent(BoxPlacement As jBoxPlacementType) As Panel
    '    Get
    '        If Containers.ContainsKey(BoxPlacement) Then
    '            Return Containers(BoxPlacement).Parent
    '        Else
    '            Return Nothing
    '        End If
    '    End Get
    'End Property

    'Define Custom Events
    Public Event BoxVisibilityChanged(BoxPlacement As jBoxPlacementType, ByVal visible As Boolean)

    Public Sub New(ViewKey As String)
        Name = ViewKey.ToUpper
        Containers = New Dictionary(Of jBoxPlacementType, jBoxViewItems)
        ContainerVisibility = New Dictionary(Of jBoxPlacementType, Boolean)
    End Sub

    'Public Sub ChangeContainerType(BoxPlacement As jBoxPlacementType, ContainerType As jBoxContainerType)
    '    Dim ctl As jBoxViewItems = Containers.Item(BoxPlacement)
    '    Dim parentPanel As Control = ctl.Parent
    '    Containers.Remove(BoxPlacement)
    '    ctl = New jBoxViewItems(ContainerType)
    '    'Configure the new Container
    '    With ctl
    '        .BoxLocation = BoxPlacement
    '        .Text = .BoxLocation.ToString
    '        AddHandler .FocusRecieved, AddressOf SetBoxContainerFocus
    '        AddHandler .NoItems, AddressOf SetBoxContainerEmpty
    '    End With
    '    'Place the new Container
    '    With parentPanel.Controls
    '        'Remove any existing Container at this placement
    '        .Clear()
    '        'Place the new Container
    '        .Add(ctl)
    '    End With
    '    Containers.Add(BoxPlacement, ctl)
    'End Sub

    Private Function PlacementTypeToKey(BoxPlacement As jBoxPlacementType) As String
        Select Case BoxPlacement
            Case jBoxPlacementType.BoxTopLeft
                Return "TL"
            Case jBoxPlacementType.BoxTopMiddle
                Return "TM"
            Case jBoxPlacementType.BoxTopRight
                Return "TR"
            Case jBoxPlacementType.BoxBottomLeft
                Return "BL"
            Case jBoxPlacementType.BoxBottomMiddle
                Return "BM"
            Case jBoxPlacementType.BoxBottomRight
                Return "BR"
            Case Else
                Return ""
        End Select
    End Function

    Public Sub AddContainer(BoxPlacement As jBoxPlacementType, parentPanel As Panel)
        SetBoxContainerVisibility(BoxPlacement, False)
        Dim BoxContainerType As jBoxContainerType

        Select Case Config.getViewBoxPlacement(ViewKey, BoxPlacement)
            Case "Tab"
                BoxContainerType = jBoxContainerType.TabDockBox
            Case Else
                BoxContainerType = jBoxContainerType.DockBox
        End Select
        Dim ctl As jBoxViewItems = New jBoxViewItems(BoxContainerType)
        'Configure the new Container
        With ctl
            .BoxLocation = BoxPlacement
            .Text = .BoxLocation.ToString
            AddHandler .FocusRecieved, AddressOf SetBoxContainerFocus
            AddHandler .NoItems, AddressOf SetBoxContainerEmpty
        End With
        'Place the new Container
        With parentPanel.Controls
            'Remove any existing Container at this placement
            .Clear()
            'Place the new Container
            .Add(ctl)
        End With
        Containers.Add(BoxPlacement, ctl)
    End Sub

    Private Sub SetBoxContainerVisibility(BoxPlacement As jBoxPlacementType, visible As Boolean)
        'TODO: Add Support for BoxTray and BoxUnplaced
        If BoxPlacement = jBoxPlacementType.BoxTrayLeft Or BoxPlacement = jBoxPlacementType.BoxTrayRight Or BoxPlacement = jBoxPlacementType.BoxUnplaced Then Exit Sub

        'TM is considered main panel and cannot be invisible
        If BoxPlacement = jBoxPlacementType.BoxTopMiddle And visible = False Then
            Exit Sub
        End If
        ContainerVisibility(BoxPlacement) = visible
        'LEFT COLUMN
        If IsVisible(jBoxPlacementType.BoxTopLeft) Or IsVisible(jBoxPlacementType.BoxBottomLeft) Then
            SplitS1.Panel1Collapsed = False
            With SplitS2
                .Panel1Collapsed = Not IsVisible(jBoxPlacementType.BoxTopLeft)
                .Panel2Collapsed = Not IsVisible(jBoxPlacementType.BoxBottomLeft)
            End With
        Else
            SplitS1.Panel1Collapsed = True
        End If
        'MIDDLE COLUMN
        SplitS4.Panel2Collapsed = Not IsVisible(jBoxPlacementType.BoxBottomMiddle)
        'RIGHT COLUMN
        If IsVisible(jBoxPlacementType.BoxTopRight) Or IsVisible(jBoxPlacementType.BoxBottomRight) Then
            SplitS3.Panel2Collapsed = False
            With SplitS5
                .Panel1Collapsed = Not IsVisible(jBoxPlacementType.BoxTopRight)
                .Panel2Collapsed = Not IsVisible(jBoxPlacementType.BoxBottomRight)
            End With
        Else
            SplitS3.Panel2Collapsed = True
        End If
    End Sub

    Private Sub SetBoxContainerEmpty(BoxPlacement As jBoxPlacementType)
        'Debug.Print("BoxContainerEmpty({0})", BoxPlacement)
        SetBoxContainerVisibility(BoxPlacement, False)
    End Sub

    Private Sub SetBoxContainerFocus(BoxPlacement As jBoxPlacementType)
        'Debug.Print("JBoxContainers_SetBoxContainerFocus({0})", BoxPlacement)
        For Each JBoxPlacement As jBoxPlacementType In Containers.Keys
            If BoxPlacement = JBoxPlacement Then
                If BoxContainer(JBoxPlacement).HasFocus = False Then
                    Containers(BoxPlacement).Parent.Focus()
                    BoxContainer(JBoxPlacement).HasFocus = True
                End If
            Else
                BoxContainer(JBoxPlacement).HasFocus = False
            End If
        Next
    End Sub


    Private Sub InitSplitter(splitID As Integer, split As SplitContainer, splitOrientation As Orientation)
        AddHandler split.GotFocus, AddressOf RemoveSplitterFocus
        With split
            With .Panel1
                .BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
                .BorderStyle = BorderStyle.None
                .Padding = New Padding(0)
                .Margin = New Padding(0)
            End With
            With .Panel2
                .BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
                .BorderStyle = BorderStyle.None
                .Padding = New Padding(0)
                .Margin = New Padding(0)
            End With
            .ClientSize = .Parent.ClientSize
            .Orientation = splitOrientation
            .BorderStyle = BorderStyle.None
            .BackColor = Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .Padding = New Padding(0)
            .Margin = New Padding(0)
            .TabIndex = 0
            .TabStop = False
            .SplitterDistance = Props.GetProperty("SPLIT." & splitID & ".SPLITTERDISTANCE", 50)
            .SplitterWidth = 6
            .Dock = DockStyle.Fill
        End With
    End Sub

    Private Sub RemoveSplitterFocus()
        Focus()
    End Sub

    Public Sub AddBoxItem(ItemObject As jBoxViewItem)
        BoxKeys.Add(ItemObject.Key, ItemObject)
        BoxContainer(ItemObject.BoxLocation).AddItem(ItemObject.Key, ItemObject)
        IsVisible(ItemObject.BoxLocation) = True
    End Sub

    Private Sub jBoxContainers_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged
        'Debug.Print("jBoxContainers_ParentChanged: BEGIN")
        Dock = DockStyle.Fill

        'Add the splitters to the client area
        SplitS1 = New SplitContainer()
        SplitS1.SuspendLayout()
        Controls.Add(SplitS1)
        InitSplitter(1, SplitS1, Orientation.Vertical)

        SplitS2 = New SplitContainer()
        SplitS2.SuspendLayout()
        SplitS1.Panel1.Controls.Add(SplitS2)
        InitSplitter(2, SplitS2, Orientation.Horizontal)

        SplitS3 = New SplitContainer()
        SplitS3.SuspendLayout()
        SplitS1.Panel2.Controls.Add(SplitS3)
        InitSplitter(3, SplitS3, Orientation.Vertical)

        SplitS4 = New SplitContainer()
        SplitS4.SuspendLayout()
        SplitS3.Panel1.Controls.Add(SplitS4)
        InitSplitter(4, SplitS4, Orientation.Horizontal)

        SplitS5 = New SplitContainer()
        SplitS5.SuspendLayout()
        SplitS3.Panel2.Controls.Add(SplitS5)
        InitSplitter(5, SplitS5, Orientation.Horizontal)

        ' For Simplicity assign better working names to Containers
        AddContainer(jBoxPlacementType.BoxTopLeft, SplitS2.Panel1)
        AddContainer(jBoxPlacementType.BoxTopMiddle, SplitS4.Panel1)
        AddContainer(jBoxPlacementType.BoxTopRight, SplitS5.Panel1)
        AddContainer(jBoxPlacementType.BoxBottomLeft, SplitS2.Panel2)
        AddContainer(jBoxPlacementType.BoxBottomMiddle, SplitS4.Panel2)
        AddContainer(jBoxPlacementType.BoxBottomRight, SplitS5.Panel2)

        IsVisible(jBoxPlacementType.BoxTopLeft) = False
        IsVisible(jBoxPlacementType.BoxTopMiddle) = True
        IsVisible(jBoxPlacementType.BoxTopRight) = False
        IsVisible(jBoxPlacementType.BoxBottomLeft) = False
        IsVisible(jBoxPlacementType.BoxBottomMiddle) = False
        IsVisible(jBoxPlacementType.BoxBottomRight) = False

        SplitS5.ResumeLayout(False)
        SplitS4.ResumeLayout(False)
        SplitS3.ResumeLayout(False)
        SplitS2.ResumeLayout(False)
        SplitS1.ResumeLayout(False)
        PerformLayout()
        'Load Splitter Size
        ready = True
    End Sub

    Public Sub SaveLayout()
        Props.PutProperty(ViewKey + ".SPLIT.1.SPLITTERDISTANCE", SplitS1.SplitterDistance)
        Props.PutProperty(ViewKey + ".SPLIT.2.SPLITTERDISTANCE", SplitS2.SplitterDistance)
        Props.PutProperty(ViewKey + ".SPLIT.3.SPLITTERDISTANCE", SplitS3.SplitterDistance)
        Props.PutProperty(ViewKey + ".SPLIT.4.SPLITTERDISTANCE", SplitS4.SplitterDistance)
        Props.PutProperty(ViewKey + ".SPLIT.5.SPLITTERDISTANCE", SplitS5.SplitterDistance)
    End Sub

    Public Sub LoadLayout()
        'Load User Props
        SplitS1.SplitterDistance = Props.GetProperty(ViewKey + ".SPLIT.1.SPLITTERDISTANCE")
        SplitS2.SplitterDistance = Props.GetProperty(ViewKey + ".SPLIT.2.SPLITTERDISTANCE")
        SplitS3.SplitterDistance = Props.GetProperty(ViewKey + ".SPLIT.3.SPLITTERDISTANCE")
        SplitS4.SplitterDistance = Props.GetProperty(ViewKey + ".SPLIT.4.SPLITTERDISTANCE")
        SplitS5.SplitterDistance = Props.GetProperty(ViewKey + ".SPLIT.5.SPLITTERDISTANCE")
    End Sub

End Class
