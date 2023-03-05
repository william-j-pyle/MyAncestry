Public Class GEDIndividualCollection
    Private IndividualRecords As New Dictionary(Of GEDReference, GEDIndividualRecord)

    Default Public ReadOnly Property Individual(Ref As GEDReference) As GEDIndividualRecord
        Get
            Return IndividualRecords(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Individual(idx As Integer) As GEDIndividualRecord
        Get
            Return IndividualRecords.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return IndividualRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData)
        'Debug.Print("GEDIndividualCollection")
        Dim rec As New GEDIndividualRecord
        rec.addObject(data)
        IndividualRecords.Add(rec.Key, rec)
    End Sub
End Class
