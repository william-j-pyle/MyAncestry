Public Class GEDSourceRecord
    Public Property Key As GEDReference
    Public Property Title As String
    Public Property AncestryAPID As String
    Public Property Author As String
    Public Property Note As String
    Public Property Publisher As String
    Public Property PublishDate As GEDDate
    Public Property PublishPlace As GEDPlace
    Public Property RepoRefKey As GEDReference

    Public Sub addObject(data As GEDComData)
        Dim processedRoot As Boolean = False
        While data.HasNext
            If data.Key.Contains("SOUR") Then
                Select Case data.Key
                    Case "SOUR"
                        If processedRoot Then
                            Exit Sub
                        End If
                        Key = data.RefKey
                        processedRoot = True
                        data.NextRow()
                    Case "SOUR.AUTH"
                        Author = data.Data
                        data.NextRow()
                    Case "SOUR.PUBL"
                        Publisher = data.MultiLineData()
                        data.NextRow()
                    Case "SOUR.PUBL.DATE"
                        PublishDate = New GEDDate(data.Data)
                        data.NextRow()
                    Case "SOUR.PUBL.PLAC"
                        PublishPlace = New GEDPlace(data.Data)
                        data.NextRow()
                    Case "SOUR._APID"
                        AncestryAPID = data.Data
                        data.NextRow()
                    Case "SOUR.REPO"
                        RepoRefKey = data.RefKey
                        data.NextRow()
                    Case "SOUR.TITL"
                        Title = data.MultiLineData()
                        data.NextRow()
                    Case "SOUR.NOTE"
                        Note = data.MultiLineData()
                        data.NextRow()
                    Case Else
                        Debug.Print("GEDSourceRecord: Unhandled Key [{0}]", data.Key)
                        data.NextRow()
                End Select
            Else
                Exit Sub
            End If
        End While
    End Sub

End Class
