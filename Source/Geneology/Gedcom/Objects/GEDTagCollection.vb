Public Class GEDTagCollection
    Private TagRecords As New Dictionary(Of GEDReference, GEDTagRecord)

    Default Public ReadOnly Property Tag(Ref As GEDReference) As GEDTagRecord
        Get
            Return TagRecords(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Tag(idx As Integer) As GEDTagRecord
        Get
            Return TagRecords.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return TagRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData)
        Dim rec As New GEDTagRecord
        rec.addObject(data)
        TagRecords.Add(rec.Key, rec)
    End Sub
End Class
