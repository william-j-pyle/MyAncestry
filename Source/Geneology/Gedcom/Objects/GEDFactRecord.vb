'TODO: Not Finished
Public Class GEDFactRecord
  Public Property FactType As String = ""
  Public Property Note As String = ""


  Public Sub addObject(data As GEDComData, fileKey As String)
    Dim processedRoot As Boolean = False
    While data.HasNext
      If data.Key.Contains(fileKey) Then
        Select Case data.Key.Replace(fileKey, "FACT")
          Case "FACT"
            If processedRoot Then Exit Sub
            processedRoot = True
            data.NextRow()
          Case "FACT.TYPE"
            FactType = data.Data
            data.NextRow()
          Case "FACT.NOTE"
            Note = data.MultiLineData()
            data.NextRow()
          Case Else
            Debug.Print("GEDFactRecord: Unhandled Key [{0}]", data.Key)
            data.NextRow()
        End Select
      Else
        Exit Sub
      End If
    End While
  End Sub

End Class
