Public Class GEDHeader
    Public Property Name As String
    Public Property GEDVersion As String
    Public Property GEDFormat As String
    Public Property FileVersion As String
    Public Property FileDate As GEDDate
    Public Property FileTime As GEDTime
    Public Property FileCharacterSet As String
    Public Property AncestryTreeName As String
    Public Property AncestryNote As String
    Public Property AncestryRIN As String
    Public Property Submitter As GEDReference

    Public Sub addObject(data As GEDComData)
        Dim Ignore As String
        While data.HasNext
            If data.Key.StartsWith("HEAD") Then
                Select Case data.Key
                    Case "HEAD"
                        data.NextRow()
                    Case "HEAD.SUBM"
                        Submitter = data.RefKey
                        data.NextRow()
                    Case "HEAD.SOUR"
                        Ignore = data.Data
                        data.NextRow()
                    Case "HEAD.SOUR.NAME"
                        Name = data.Data
                        data.NextRow()
                    Case "HEAD.SOUR.VERS"
                        FileVersion = data.Data
                        data.NextRow()
                    Case "HEAD.SOUR._TREE"
                        AncestryTreeName = data.Data
                        data.NextRow()
                    Case "HEAD.SOUR._TREE.RIN"
                        AncestryRIN = data.Data
                        data.NextRow()
                    Case "HEAD.SOUR._TREE.NOTE"
                        AncestryNote = data.MultiLineData()
                        data.NextRow()
                    Case "HEAD.SOUR._TREE._ENV"
                        data.NextRow()
                    Case "HEAD.SOUR.CORP"
                        data.NextRow()
                    Case "HEAD.SOUR.CORP.PHON"
                        data.NextRow()
                    Case "HEAD.SOUR.CORP.WWW"
                        data.NextRow()
                    Case "HEAD.SOUR.CORP.ADDR"
                        Ignore = data.MultiLineData()
                        data.NextRow()
                    Case "HEAD.DATE"
                        FileDate = New GEDDate(data.Data)
                        data.NextRow()
                    Case "HEAD.DATE.TIME"
                        FileTime = New GEDTime(data.Data)
                        data.NextRow()
                    Case "HEAD.GEDC"
                        data.NextRow()
                    Case "HEAD.GEDC.VERS"
                        GEDVersion = data.Data
                        data.NextRow()
                    Case "HEAD.GEDC.FORM"
                        GEDFormat = data.Data
                        data.NextRow()
                    Case "HEAD.CHAR"
                        FileCharacterSet = data.Data
                        data.NextRow()
                    Case Else
                        Debug.Print("GEDHeader: Unhandled Key [{0}]", data.Key)
                        data.NextRow()
                End Select
            Else
                Exit Sub
            End If
        End While
    End Sub

End Class

