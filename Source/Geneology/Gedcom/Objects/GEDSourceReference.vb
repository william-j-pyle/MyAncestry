Public Class GEDSourceReference
  Public Property Key As GEDReference
  Public Property AncestryAPID As String
  Public Property DataNote As String
  Public Property DataText As String
  Public Property DataWWW As String
  Public Property Page As String
  Public Property Media As New GEDMediaReferenceCollection



  Public Sub addObject(data As GEDComData, fileKey As String)
    Dim processedRoot As Boolean = False
    While data.HasNext
      If data.Key.Contains(fileKey) Then
        Select Case data.Key.Replace(fileKey, "SOUR")
          Case "SOUR"
            If processedRoot Then
              Exit Sub
            End If
            Key = data.RefKey
            processedRoot = True
            data.NextRow()
          Case "SOUR._APID"
            AncestryAPID = data.Data
            data.NextRow()
          Case "SOUR.PAGE"
            Page = data.MultiLineData()
            data.NextRow()
          Case "SOUR.DATA"
            data.NextRow()
          Case "SOUR.DATA.NOTE"
            DataNote = data.MultiLineData()
            data.NextRow()
          Case "SOUR.DATA.WWW"
            DataWWW = data.Data
            data.NextRow()
          Case "SOUR.DATA.TEXT"
            DataText = data.MultiLineData()
            data.NextRow()
          Case "SOUR.OBJE"
            Media.addObject(data, data.Key)
          Case Else
            Debug.Print("GEDSourceReference: Unhandled Key [{0}]", data.Key)
            data.NextRow()
        End Select
      Else
        Exit Sub
      End If
    End While
  End Sub
End Class
