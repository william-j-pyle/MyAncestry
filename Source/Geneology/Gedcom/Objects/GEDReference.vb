'TODO: Need to be able to identify ReferenceTYPE!
Public Class GEDReference
    Public Property Key As String = ""
    Public Property Type As String = ""

    Public Sub New(StringKey As String)
        Key = StringKey
    End Sub

    Public Sub New(StringKey As String, Type As String)
        Key = StringKey
        Type = Type
    End Sub
End Class
