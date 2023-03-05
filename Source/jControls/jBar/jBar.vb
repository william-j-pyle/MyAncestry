Public Class jBar
    Inherits jPanel

    'Description
    '	Group/Item container with Attribute And State management.
    '	No presentation layer, only inherites jPanel For implementer use.

    Protected Friend Config As jConfig = My.Application.Config

    Protected Items As New Dictionary(Of String, jBarItemConfig)
    Protected Groups As New Dictionary(Of String, Collection)

    'Exposes Properties
    Public ReadOnly Property ItemKeys As String()
        Get
            Return Items.Keys.ToArray
        End Get
    End Property

    Public ReadOnly Property GroupKeys As String()
        Get
            Return Groups.Keys.ToArray
        End Get
    End Property

    Default Public Property ItemState(ItemKey As String, ItemStateType As BarStateType) As Boolean
        Get
            If Items.ContainsKey(ItemKey) Then
                Select Case ItemStateType
                    Case BarStateType.Visible
                        Return Items(ItemKey).Visible
                    Case BarStateType.Enabled
                        Return Items(ItemKey).Enabled
                    Case BarStateType.Checked
                        Return Items(ItemKey).Checked
                End Select
            End If
            Return False
        End Get
        Set(value As Boolean)
            If Items.ContainsKey(ItemKey) Then
                Select Case ItemStateType
                    Case BarStateType.Visible
                        Items(ItemKey).Visible = value
                    Case BarStateType.Enabled
                        Items(ItemKey).Enabled = value
                    Case BarStateType.Checked
                        Items(ItemKey).Checked = value
                End Select
            End If
        End Set
    End Property

    'Exposes Events
    Public Event ItemAdded(ByVal ItemKey As String)
    Public Event ItemRemoved(ByVal ItemKey As String, ByRef cancel As Boolean)
    Public Event ItemClicked(ByVal ItemKey As String)
    Public Event ItemStateChanged(ByVal ItemKey As String, ByVal ItemStateType As BarStateType, ByVal StateValue As Boolean)
    Public Event GroupStateChanged(ByVal GroupKey As String, ByVal GroupStateType As BarStateType, ByVal StateValue As Boolean)

    Protected Friend Sub OnItemClicked(ByVal ItemKey As String)
        RaiseEvent ItemClicked(ItemKey)
    End Sub

    Protected Friend Sub OnItemStateChanged(ByVal ItemKey As String, ByVal ItemStateType As BarStateType, ByVal StateValue As Boolean)
        RaiseEvent ItemStateChanged(ItemKey, ItemStateType, StateValue)
    End Sub


    Public Function AddItem(ItemObjectConfig As jBarItemConfig) As Boolean
        With ItemObjectConfig
            If .GroupKey Is Nothing Then .GroupKey = "APP"
            If Not Items.ContainsKey(.ItemKey) Then
                Items.Add(.ItemKey, ItemObjectConfig)
                If Not Groups.ContainsKey(.GroupKey) Then
                    Groups.Add(.GroupKey, New Collection)
                End If
                If Not Groups(.GroupKey).Contains(.ItemKey) Then
                    Groups(.GroupKey).Add(.ItemKey, .ItemKey)
                End If
                RaiseEvent ItemAdded(.ItemKey)
            End If
            Return Items.ContainsKey(.ItemKey)
        End With
    End Function

    Public Sub AddItemToGroup(ItemKey As String, GroupKey As String)
        If Items.ContainsKey(ItemKey) Then
            If Not Groups.ContainsKey(GroupKey) Then
                Groups.Add(GroupKey, New Collection)
            End If
            If Not Groups(GroupKey).Contains(ItemKey) Then
                Groups(GroupKey).Add(ItemKey, ItemKey)
            End If
        End If
    End Sub

    Public Function RemoveItem(ItemKey As String) As Boolean
        If Items.ContainsKey(ItemKey) Then
            Dim cancel As Boolean = False
            RaiseEvent ItemRemoved(ItemKey, cancel)
            If Not cancel Then
                Items.Remove(ItemKey)
                'Items can be added to multiple groups, clear it from all of them
                For Each ItemCollection As Collection In Groups.Values
                    If ItemCollection.Contains(ItemKey) Then
                        ItemCollection.Remove(ItemKey)
                    End If
                Next
            End If
        End If
        Return Items.ContainsKey(ItemKey)
    End Function

    Public Sub Load(BarKey As String, FileName As String)
        'TODO: Add Load Functionality
    End Sub

    Public Sub Save(BarKey As String, FileName As String)
        'TODO: Add Save Functionality
    End Sub

    Public Function SetGroupState(GroupKey As String, GroupStateType As BarStateType, value As Boolean, Optional FireEvents As Boolean = True) As Boolean
        Dim rtn As Boolean = False
        If Groups.ContainsKey(GroupKey) Then
            rtn = True
            For Each ItemKey As String In Groups(GroupKey)
                rtn = rtn And SetItemState(ItemKey, GroupStateType, FireEvents)
            Next
            If FireEvents Then
                RaiseEvent GroupStateChanged(GroupKey, GroupStateType, value)
            End If
        End If
        Return rtn
    End Function

    Public Function SetItemState(ItemKey As String, ItemStateType As BarStateType, value As Boolean) As Boolean
        Dim rtn As Boolean = False
        If Items.ContainsKey(ItemKey) Then
            rtn = True
            With Items(ItemKey)
                Select Case ItemStateType
                    Case BarStateType.Visible
                        .Visible = value
                    Case BarStateType.Enabled
                        .Enabled = value
                    Case BarStateType.Checked
                        .Checked = value
                    Case Else
                        rtn = False
                End Select
            End With
        End If
        Return rtn
    End Function

    Public Function GetItemState(ItemKey As String, ItemStateType As BarStateType) As Boolean
        If Items.ContainsKey(ItemKey) Then
            Select Case ItemStateType
                Case BarStateType.Visible
                    Return Items(ItemKey).Visible
                Case BarStateType.Enabled
                    Return Items(ItemKey).Enabled
                Case BarStateType.Checked
                    Return Items(ItemKey).Checked
            End Select
        End If
        Return False
    End Function

    Public Sub SetItemAttribute(ItemKey As String, ItemAttribute As String, AttributeValue As Object)
        If Items.ContainsKey(ItemKey) Then
            Items(ItemKey).Attribute(ItemAttribute) = AttributeValue
        End If
    End Sub

    Public Function GetItemAttribute(ItemKey As String, ItemAttribute As String) As Object
        If Items.ContainsKey(ItemKey) Then
            Return Items(ItemKey).Attribute(ItemAttribute)
        End If
        Return Nothing
    End Function

End Class
