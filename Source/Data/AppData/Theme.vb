Public Class Theme
    Private Cfg As jConfig
    Private Icons As Dictionary(Of String, JIcon)
    Private Colors As Dictionary(Of String, Color)
    Private Mapping As Dictionary(Of String, Color)

    Public Sub New(cfg As jConfig)
        Me.Cfg = cfg
        Me.Icons = New Dictionary(Of String, JIcon)
        Me.Colors = New Dictionary(Of String, Color)
        loadColors()
        loadIcons()
        loadMapping()
    End Sub

    Private Sub loadColors()
        Dim themeid As Integer = Cfg.Config.Theme
        For Each c As cfgThemeColors In Cfg.ThemeColors
            If c.ThemeID = themeid Then
                For Each clr As cfgThemeColor In c.Colors
                    Colors.Add(clr.ColorID, Drawing.Color.FromArgb(clr.R, clr.G, clr.B))
                Next
            End If
        Next
    End Sub

    Private Sub loadIcons()
        Dim iconPath As String = Cfg.Config.PathIcon
        Dim ico As JIcon
        For Each i As cfgIcon In Cfg.Icons
            ico = New JIcon(iconPath & i.FileName)
            If i.ReSize > 0 Then
                ico = ico.Resize(New Size(i.ReSize, i.ReSize))
            End If
            Try
                If i.ReColorID IsNot Nothing Then
                    ico = ico.Recolor(Colors(i.ReColorID))
                End If
            Catch e As Exception

            End Try
            Icons.Add(i.IconKey, ico)
        Next
    End Sub

    Public Sub loadMapping()
        For Each mapping As cfgColorMap In Cfg.ColorMap
            addColorCode(StringThemeItemType(mapping.ItemType), StringThemeColorType(mapping.ColorType), StringThemeColorItemState(mapping.ColorState), Colors(mapping.ColorID))
        Next
    End Sub

    Private Function StringThemeItemType(value As String) As ThemeItemType
        Select Case value.ToUpper
            Case ThemeItemType.APP.ToString.ToUpper
                Return ThemeItemType.APP
            Case ThemeItemType.DOCKBOX.ToString.ToUpper
                Return ThemeItemType.DOCKBOX
            Case ThemeItemType.DOCKBOXTAB.ToString.ToUpper
                Return ThemeItemType.DOCKBOXTAB
            Case ThemeItemType.MENUBAR.ToString.ToUpper
                Return ThemeItemType.MENUBAR
            Case ThemeItemType.STATUSBAR.ToString.ToUpper
                Return ThemeItemType.STATUSBAR
            Case ThemeItemType.TABBOX.ToString.ToUpper
                Return ThemeItemType.TABBOX
            Case ThemeItemType.TABBOXTAB.ToString.ToUpper
                Return ThemeItemType.TABBOXTAB
            Case ThemeItemType.TOOLBAR.ToString.ToUpper
                Return ThemeItemType.TOOLBAR
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function StringThemeColorType(value As String) As ThemeColorType
        Select Case value.ToUpper
            Case ThemeColorType.ACCENT.ToString.ToUpper
                Return ThemeColorType.ACCENT
            Case ThemeColorType.BACKGROUND.ToString.ToUpper
                Return ThemeColorType.BACKGROUND
            Case ThemeColorType.BORDER.ToString.ToUpper
                Return ThemeColorType.BORDER
            Case ThemeColorType.FOREGROUND.ToString.ToUpper
                Return ThemeColorType.FOREGROUND
            Case ThemeColorType.HIGHLIGHT.ToString.ToUpper
                Return ThemeColorType.HIGHLIGHT
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function StringThemeColorItemState(value As String) As ThemeColorItemState
        Select Case value.ToUpper
            Case ThemeColorItemState.ACTIVE.ToString.ToUpper
                Return ThemeColorItemState.ACTIVE
            Case ThemeColorItemState.DISABLED.ToString.ToUpper
                Return ThemeColorItemState.DISABLED
            Case ThemeColorItemState.HOVER.ToString.ToUpper
                Return ThemeColorItemState.HOVER
            Case ThemeColorItemState.INACTIVE.ToString.ToUpper
                Return ThemeColorItemState.INACTIVE
            Case ThemeColorItemState.SELECTED.ToString.ToUpper
                Return ThemeColorItemState.SELECTED
            Case ThemeColorItemState.UNSELECTED.ToString.ToUpper
                Return ThemeColorItemState.UNSELECTED
            Case Else
                Return Nothing
        End Select
    End Function


    Private Function makeKey(ParamArray args() As String) As String
        Dim propName As String = ""
        For Each arg As String In args
            If propName.Length > 0 Then propName += "."
            propName += arg
        Next
        Return propName.ToUpper
    End Function

    Private Function getKey(itemType As ThemeItemType, colorType As ThemeColorType, state As ThemeColorItemState) As String
        Return itemType & "." & colorType & "." & state
    End Function

    Private Function newColor(R As Integer, Optional G As Integer = -1, Optional B As Integer = -1) As Color
        Dim clr As Color
        If G = -1 Or B = -1 Then
            clr = System.Drawing.Color.FromArgb(R, R, R)
        Else
            clr = System.Drawing.Color.FromArgb(R, G, B)
        End If
        Return clr
    End Function

    Private Sub addColorCode(itemType As ThemeItemType, colorType As ThemeColorType, state As ThemeColorItemState, clr As Color)
        Dim key As String
        key = getKey(itemType, colorType, state)
        Colors.Add(key, clr)
    End Sub

    Private Sub DumpColorCode(itemType As ThemeItemType, colorType As ThemeColorType, state As ThemeColorItemState)
        Debug.Print("addColorCode(ThemeItemType." & itemType.ToString() & ",ThemeColorType." & colorType.ToString & ", ThemeColorItemState." & state.ToString & ", &Hxx)")
    End Sub

    Public Function Color(itemType As ThemeItemType, colorType As ThemeColorType, state As ThemeColorItemState) As Color
        If Colors.ContainsKey(getKey(itemType, colorType, state)) Then
            'Perfect match good go to!
            Return Colors(getKey(itemType, colorType, state))
        End If
        'Crap not found, return anoying RED
        Debug.Print("Theme MATCH NOT FOUND!")
        DumpColorCode(itemType, colorType, state)
        Return System.Drawing.Color.Red
    End Function

    Public Function ForeColor(Optional itemType As ThemeItemType = ThemeItemType.APP, Optional state As ThemeColorItemState = ThemeColorItemState.ACTIVE) As Color
        Return Color(itemType, ThemeColorType.FOREGROUND, state)
    End Function

    Public Function BackColor(Optional itemType As ThemeItemType = ThemeItemType.APP, Optional state As ThemeColorItemState = ThemeColorItemState.ACTIVE) As Color
        Return Color(itemType, ThemeColorType.BACKGROUND, state)
    End Function

    Public Function AccentColor(Optional itemType As ThemeItemType = ThemeItemType.APP, Optional state As ThemeColorItemState = ThemeColorItemState.ACTIVE) As Color
        Return Color(itemType, ThemeColorType.ACCENT, state)
    End Function

    Public Function HighlightColor(Optional itemType As ThemeItemType = ThemeItemType.APP, Optional state As ThemeColorItemState = ThemeColorItemState.ACTIVE) As Color
        Return Color(itemType, ThemeColorType.HIGHLIGHT, state)
    End Function

    Public Function BorderColor(Optional itemType As ThemeItemType = ThemeItemType.APP, Optional state As ThemeColorItemState = ThemeColorItemState.ACTIVE) As Color
        Return Color(itemType, ThemeColorType.BORDER, state)
    End Function

    Public Function Icon(iconKey As String, Optional reSize As Size = Nothing, Optional reColor As Color = Nothing) As Bitmap
        'Debug.Print("Theme: Icon(iconKey:" & iconKey & ")")
        If Icons.ContainsKey(iconKey) Then
            Dim i As JIcon
            i = Icons(iconKey)
            If reSize <> Nothing Then i.Resize(reSize)
            If reColor <> Nothing Then i.Recolor(reColor)
            Return i.Icon()
        End If
        Return Nothing
    End Function

    Public Function Font(Optional itemType As ThemeItemType = ThemeItemType.APP, Optional bold As Boolean = True) As Font
        Dim fs As FontStyle = FontStyle.Regular
        'Debug.Print("Theme: Font(itemType:" & itemType & ", bold:" & bold & ")")
        If bold Then
            fs = FontStyle.Bold
        End If
        Return New System.Drawing.Font("Segoe UI", 9.0!, fs, System.Drawing.GraphicsUnit.Point)
    End Function

End Class
