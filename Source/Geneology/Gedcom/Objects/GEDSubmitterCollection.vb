Public Class GEDSubmitterCollection
    Private SubmitterRecords As New Dictionary(Of GEDReference, GEDSubmitterRecord)

    Default Public ReadOnly Property Submitter(Ref As GEDReference) As GEDSubmitterRecord
        Get
            Return SubmitterRecords(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Submitter(idx As Integer) As GEDSubmitterRecord
        Get
            Return SubmitterRecords.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return SubmitterRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData)
        Dim rec As New GEDSubmitterRecord
        rec.addObject(data)
        SubmitterRecords.Add(rec.Key, rec)
    End Sub
End Class
