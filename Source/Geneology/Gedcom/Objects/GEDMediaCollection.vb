Public Class GEDMediaCollection
    Private MediaRecords As New Dictionary(Of GEDReference, GEDMediaRecord)

    Default Public ReadOnly Property Individual(Ref As GEDReference) As GEDMediaRecord
        Get
            Return MediaRecords(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Individual(idx As Integer) As GEDMediaRecord
        Get
            Return MediaRecords.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return MediaRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData)
        Dim rec As New GEDMediaRecord
        rec.addObject(data)
        MediaRecords.Add(rec.Key, rec)
    End Sub
End Class
