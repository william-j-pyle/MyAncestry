'TODO: Not Finished
Public Class GEDEventRecord
  Public Property Key As GEDReference
  Public Property FileKey As String
  Public Property RecordType As String
  Public Property EventType As String
  Public Property EventDate As GEDDate
  Public Property Place As GEDPlace
  Public Property Source As New GEDSourceReferenceCollection
  Public Property Media As New GEDMediaReferenceCollection
  Public Property Note As String


  Public Sub addObject(data As GEDComData, fileKey As String)
    Dim processedRoot As Boolean = False
    While data.HasNext
      If data.Key.Contains(fileKey) Then
        Select Case data.Key.Replace(fileKey, "EVNT")
          Case "EVNT"
            If processedRoot Then
              Exit Sub
            End If
            Key = data.RefKey
            Me.FileKey = fileKey
            RecordType = fileKey.Split(".").Last()
            processedRoot = True
            data.NextRow()
          Case "EVNT.TYPE"
            EventType = data.Data
            data.NextRow()
          Case "EVNT.DATE"
            EventDate = New GEDDate(data.Data)
            data.NextRow()
          Case "EVNT.OBJE"
            Media.addObject(data, data.Key)
          Case "EVNT.PLAC"
            Place = New GEDPlace(data.Data)
            data.NextRow()
          Case "EVNT.SOUR"
            Source.addObject(data, data.Key)
          Case "EVNT.NOTE"
            Note = data.MultiLineData()
            data.NextRow()
          Case Else
            Debug.Print("GEDEventRecord: Unhandled Key [{0}]", data.Key)
            data.NextRow()
        End Select
      Else
        Exit Sub
      End If
    End While
  End Sub

End Class
