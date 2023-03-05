Public MustInherit Class jBoxViewItem
    Inherits Panel

    Protected Theme As Theme = My.Application.Config.Theme

    ' Events
    Event SearchCleared()
    Event SearchCriteriaChanged(criteria As String)
    Event FocusRecieved(BoxPlacement As jBoxPlacementType)
    Event SearchClicked(BoxPlacement As jBoxPlacementType)
    Event CloseClicked(BoxPlacement As jBoxPlacementType)
    Event ItemFocusChanged(itemHasFocus As Boolean)
    ' Changed Events

    ' Properties
    Public Property BoxLocation As jBoxPlacementType
    Public MustOverride ReadOnly Property PreferedBoxLocation As jBoxPlacementType
    Public Property HeaderText As String = ""
    Public Property Caption As String = ""
    Public Property Description As String = ""
    Public Property Icon As JIcon
    Public Property ShowHeader As Boolean = True
    Public Property ShowClose As Boolean = False
    Public Property ShowContextMenu As Boolean = False
    Public Property ShowToolbar As Boolean = False
    Public Property ShowSearch As Boolean = False

    Private _SearchCriteria As String
    Public Property SearchCriteria As String
        Get
            Return _SearchCriteria
        End Get
        Set(value As String)
            If _SearchCriteria <> value Then
                _SearchCriteria = value
                If value = "" Then
                    RaiseEvent SearchCleared()
                Else
                    RaiseEvent SearchCriteriaChanged(value)
                End If
            End If
        End Set
    End Property

    Private _HasFocus As Boolean = False
    Public Property HasFocus As Boolean
        Get
            Return _HasFocus
        End Get
        Set(value As Boolean)
            _HasFocus = value
            RaiseEvent ItemFocusChanged(value)
        End Set
    End Property

    Private _key As String = ""
    Public Property Key As String
        Get
            If _key = "" Then
                _key = [GetType].ToString
            End If
            Return _key
        End Get
        Set(value As String)
            _key = value
        End Set
    End Property

    Private Sub jBoxViewItem_ControlAdded(sender As Object, e As ControlEventArgs) Handles Me.ControlAdded
        'Debug.Print("jBoxViewItem_ControlAdded({0})", e.Control)
        AddHandler e.Control.GotFocus, AddressOf RaiseFocusEvent
        AddHandler e.Control.Click, AddressOf RaiseFocusEvent
    End Sub

    Public Sub RaiseFocusEvent()
        'Debug.Print("jBoxViewItem_RaiseFocusEvent({0})", BoxLocation)
        RaiseEvent FocusRecieved(BoxLocation)
    End Sub
End Class
