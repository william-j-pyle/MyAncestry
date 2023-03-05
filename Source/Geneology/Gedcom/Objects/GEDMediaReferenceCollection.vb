
'TODO: Not Finished - Can this inhiert from GEDReferenceCollection?
Public Class GEDMediaReferenceCollection
    Private MediaReferences As New Dictionary(Of GEDReference, GEDMediaReference)

    Default Public ReadOnly Property Individual(Ref As GEDReference) As GEDMediaReference
        Get
            Return MediaReferences(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Individual(idx As Integer) As GEDMediaReference
        Get
            Return MediaReferences.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return MediaReferences.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData, fileKey As String)
        'Debug.Print("GEDMediaReferenceCollection")
        Dim rec As New GEDMediaReference
        rec.addObject(data, fileKey)
        MediaReferences.Add(rec.Key, rec)
    End Sub

End Class