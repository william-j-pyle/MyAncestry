'TODO: Not Finished
Public Class GEDFactCollection
  Private FactRecords As New ArrayList


  Default Public ReadOnly Property Fact(idx As Integer) As GEDFactRecord
    Get
      Return FactRecords(idx)
    End Get
  End Property

  Public ReadOnly Property Count As Integer
    Get
      Return FactRecords.Count
    End Get
  End Property

  Public Sub addObject(data As GEDComData)
    Dim rec As New GEDFactRecord
    rec.addObject(data, data.Key)
    FactRecords.Add(rec)
  End Sub
End Class
