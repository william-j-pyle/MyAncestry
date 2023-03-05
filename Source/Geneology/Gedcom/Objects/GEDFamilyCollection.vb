Public Class GEDFamilyCollection
    Private FamilyRecords As New Dictionary(Of GEDReference, GEDFamilyRecord)

    Default Public ReadOnly Property Individual(Ref As GEDReference) As GEDFamilyRecord
        Get
            Return FamilyRecords(Ref)
        End Get
    End Property

    Default Public ReadOnly Property Individual(idx As Integer) As GEDFamilyRecord
        Get
            Return FamilyRecords.ElementAt(idx).Value
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return FamilyRecords.Count
        End Get
    End Property

    Public Sub addObject(data As GEDComData)
        Dim rec As New GEDFamilyRecord
        rec.addObject(data)
        FamilyRecords.Add(rec.Key, rec)
    End Sub
End Class
