Public Class DMSelectionMessage
    Public ControlKey As String = ""
    Public EventName As String = "SelectionChanged"
    Public DataSetName As String = ""
    Public EventArg As String = ""

    Public Sub New()

    End Sub

    Public Sub New(ControlKey As String, EventName As String, DataSetName As String, EventArg As String)
        Me.ControlKey = ControlKey
        Me.EventName = EventName
        Me.DataSetName = DataSetName
        Me.EventArg = EventArg
    End Sub
End Class
