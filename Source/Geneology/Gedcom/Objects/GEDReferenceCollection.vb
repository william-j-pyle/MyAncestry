'TODO: Need ability to add ALL References into 1 object, and find a reference and return that object of whatever type
Public Class GEDReferenceCollection
    Private Ref As New ArrayList

    Public Sub Clear()
        Ref.Clear()
    End Sub

    Public Function Add(value As GEDReference) As Integer
        Return Ref.Add(value)
    End Function

    Public ReadOnly Property Count() As Integer
        Get
            Return Ref.Count
        End Get
    End Property

    Default Public ReadOnly Property Item(index As Integer) As GEDReference
        Get
            Return Ref.Item(index)
        End Get
    End Property

    Public Function Contains(value As GEDReference) As Boolean
        Return Ref.Contains(value)
    End Function

End Class
