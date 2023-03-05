'TODO: Not Finished
Public Class GEDEventCollection
    Private EventRecords As New Dictionary(Of GEDReference, GEDEventRecord)

    Default Public ReadOnly Property Individual(Ref As GEDReference) As GEDEventRecord
        Get
            Return EventRecords(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Individual(idx As Integer) As GEDEventRecord
        Get
            Return EventRecords.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return EventRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData, fileKey As String)
        Dim rec As New GEDEventRecord
        rec.addObject(data, fileKey)
        EventRecords.Add(rec.Key, rec)
    End Sub
End Class