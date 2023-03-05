'TODO: Not Finished
Public Class GEDNameCollection
    Private NameRecords As New ArrayList


    Default Public ReadOnly Property Name(idx As Integer) As GEDNameRecord
        Get
            Return NameRecords(idx)
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return NameRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData)
        Dim rec As New GEDNameRecord
        rec.addObject(data)
        NameRecords.Add(rec)
    End Sub
End Class
