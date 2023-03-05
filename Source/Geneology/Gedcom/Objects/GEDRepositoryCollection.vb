
Public Class GEDRepositoryCollection
    Private RepositoryRecords As New Dictionary(Of GEDReference, GEDRepositoryRecord)

    Default Public ReadOnly Property Individual(Ref As GEDReference) As GEDRepositoryRecord
        Get
            Return RepositoryRecords(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Individual(idx As Integer) As GEDRepositoryRecord
        Get
            Return RepositoryRecords.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return RepositoryRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData)
        Dim rec As New GEDRepositoryRecord
        rec.addObject(data)
        RepositoryRecords.Add(rec.Key, rec)
    End Sub
End Class
