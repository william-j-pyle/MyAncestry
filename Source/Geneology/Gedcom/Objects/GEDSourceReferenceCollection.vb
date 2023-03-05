'TODO: Not Finished
Public Class GEDSourceReferenceCollection
    Private SourceReferences As New Dictionary(Of GEDReference, GEDSourceReference)

    Default Public ReadOnly Property Source(Ref As GEDReference) As GEDSourceReference
        Get
            Return SourceReferences(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Source(idx As Integer) As GEDSourceReference
        Get
            Return SourceReferences.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return SourceReferences.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData, fileKey As String)
        Dim rec As New GEDSourceReference
        rec.addObject(data, fileKey)
        SourceReferences.Add(rec.Key, rec)
    End Sub
End Class
