Public Class jConfig
    Public Properties As Properties
    Public Config As cfgConfig
    Public Views As List(Of cfgView)
    Public ViewItems As List(Of cfgViewItem)
    Public MenuBars As List(Of cfgBarItem)
    Public ToolBars As List(Of cfgBarItem)
    Public StatusBar As List(Of cfgBarItem)
    Public ThemeColors As List(Of cfgThemeColors)
    Public ColorMap As List(Of cfgColorMap)
    Public Icons As List(Of cfgIcon)

    Private _Theme As Theme
    Public ReadOnly Property Theme As Theme
        Get
            If _Theme Is Nothing Then
                _Theme = New Theme(Me)
            End If
            Return _Theme
        End Get
    End Property

    Public Sub loadProperties(fileName As String)
        Properties = New Properties(fileName)
    End Sub

    Public Function getView(ViewKey As String) As cfgView
        For Each cfg As cfgView In Views
            If cfg.Key.ToUpper = ViewKey.ToUpper Then
                Return cfg
            End If
        Next
        Return Nothing
    End Function

    Public Function translateBoxPlacement(value As String) As jBoxPlacementType
        Select Case value.ToUpper
            Case jBoxPlacementType.BoxTopLeft.ToString.ToUpper, "TL"
                Return jBoxPlacementType.BoxTopLeft
            Case jBoxPlacementType.BoxTopMiddle.ToString.ToUpper, "TM"
                Return jBoxPlacementType.BoxTopMiddle
            Case jBoxPlacementType.BoxTopRight.ToString.ToUpper, "TR"
                Return jBoxPlacementType.BoxTopRight
            Case jBoxPlacementType.BoxBottomLeft.ToString.ToUpper, "BL"
                Return jBoxPlacementType.BoxBottomLeft
            Case jBoxPlacementType.BoxBottomMiddle.ToString.ToUpper, "BM"
                Return jBoxPlacementType.BoxBottomMiddle
            Case jBoxPlacementType.BoxBottomRight.ToString.ToUpper, "BR"
                Return jBoxPlacementType.BoxBottomRight
            Case Else
                Throw New Exception("Unknown BlockLocation in translateBoxPlacement: " & value)
        End Select
    End Function

    Public Function getViewBoxPlacement(ViewKey As String, BoxPlacement As jBoxPlacementType) As String
        For Each cfg As cfgView In Views
            If cfg.Key.ToUpper = ViewKey.ToUpper Then
                Select Case BoxPlacement
                    Case jBoxPlacementType.BoxTopLeft
                        Return cfg.TL.Type
                    Case jBoxPlacementType.BoxTopMiddle
                        Return cfg.TM.Type
                    Case jBoxPlacementType.BoxTopRight
                        Return cfg.TR.Type
                    Case jBoxPlacementType.BoxBottomLeft
                        Return cfg.BL.Type
                    Case jBoxPlacementType.BoxBottomMiddle
                        Return cfg.BM.Type
                    Case jBoxPlacementType.BoxBottomRight
                        Return cfg.BR.Type
                    Case Else
                        Return "Dock"
                End Select
            End If
        Next
        Return "Dock"
    End Function


End Class

Public Structure cfgIcon
    Public IconKey As String
    Public FileName As String
    Public ReSize As Integer
    Public ReColorID As String
End Structure

Public Structure cfgColorMap
    Public ItemType As String
    Public ColorType As String
    Public ColorState As String
    Public ColorID As String
End Structure


Public Structure cfgThemeColors
    Public ThemeID As String
    Public Colors As List(Of cfgThemeColor)
End Structure

Public Structure cfgThemeColor
    Public ColorID As String
    Public R As Integer
    Public G As Integer
    Public B As Integer
End Structure

Public Structure cfgConfig
    Public JSControlFile As String
    Public VirtualHostName As String
    Public PathWebRoot As String
    Public PathIcon As String
    Public Theme As Integer
    Public PathGedCom As String
    Public PathMediaMovies As String
    Public PathMediaStories As String
    Public PathMediaImages As String
    Public PathMediaAudio As String
    Public PathMedia As String
    Public PathData As String
    Public PathConfig As String
End Structure

Public Structure cfgView
    Public Key As String
    Public TL As cfgViewDock
    Public TM As cfgViewDock
    Public TR As cfgViewDock
    Public BL As cfgViewDock
    Public BM As cfgViewDock
    Public BR As cfgViewDock
End Structure

Public Structure cfgViewDock
    Public Type As String
    Public Visible As Boolean
End Structure

Public Structure cfgViewItem
    Public Key As String
    Public ViewKey As String
    Public GroupKey As String
    Public Type As String
    Public TabCaption As String
    Public BoxHeader As String
    Public BoxLocation As String
    Public Url As String
    Public ShowClose As Boolean
    Public ShowToolbar As Boolean
    Public ShowSearch As Boolean
    Public ShowStatus As Boolean
    Public Flags As List(Of String)
End Structure

Public Structure cfgBarItem
    Public Caption As String
    Public GroupKey As String
    Public Key As String
    Public ParentKey As String
    Public IconKey As String
    Public Type As String
    Public Flags As List(Of String)
End Structure

Public Structure cfgKeyValuePair
    Public Key As String
    Public Value As String
End Structure