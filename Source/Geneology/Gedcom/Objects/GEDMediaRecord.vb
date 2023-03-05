'TODO: Not Finished
Public Class GEDMediaRecord

    Public Property Key As GEDReference
    Public Property CreateDate As GEDDate
    Public Property FileName As String
    Public Property FormatType As String
    Public Property Title As String
    Public Property Place As GEDPlace
    Public Property RIN As String
    Public Property Ex_CLonOID As String
    Public Property Ex_CLonPID As String
    Public Property Ex_CLonTID As String
    Public Property Ex_CLonDate As GEDDate
    Public Property Ex_CreateDate As GEDDate
    Public Property Ex_Description As String
    Public Property Ex_Meta As String
    Public Property Ex_MserLKID As String
    Public Property Ex_URL As String
    Public Property Ex_FormatHeight As String
    Public Property Ex_FormatWidth As String
    Public Property Ex_FormatSize As String
    Public Property Ex_FormatMType As String
    Public Property Ex_FormatSType As String

    Public Sub addObject(data As GEDComData)
        Dim processedRoot As Boolean = False
        While data.HasNext
            If data.Key.Contains("OBJE") Then
                Select Case data.Key
                    Case "OBJE"
                        If processedRoot Then
                            Exit Sub
                        End If
                        Key = data.RefKey
                        processedRoot = True
                        data.NextRow()
                    Case "OBJE._CLON"
                        data.NextRow()
                    Case "OBJE._CLON._OID"
                        Ex_CLonOID = data.Data
                        data.NextRow()
                    Case "OBJE._CLON._PID"
                        Ex_CLonPID = data.Data
                        data.NextRow()
                    Case "OBJE._CLON._TID"
                        Ex_CLonTID = data.Data
                        data.NextRow()
                    Case "OBJE._CLON._DATE"
                        Ex_CLonDate = New GEDDate(data.Data)
                        data.NextRow()
                    Case "OBJE._CREA"
                        Ex_CreateDate = New GEDDate(data.Data)
                        data.NextRow()
                    Case "OBJE._DSCR"
                        Ex_Description = data.MultiLineData()
                        data.NextRow()
                    Case "OBJE._META"
                        Ex_Meta = data.MultiLineData()
                        data.NextRow()
                    Case "OBJE._MSER"
                        data.NextRow()
                    Case "OBJE._MSER._LKID"
                        Ex_MserLKID = data.Data
                        data.NextRow()
                    Case "OBJE._ORIG"
                        data.NextRow()
                    Case "OBJE._ORIG._URL"
                        data.NextRow()
                    Case "OBJE.DATE"
                        CreateDate = New GEDDate(data.Data)
                        data.NextRow()
                    Case "OBJE.FILE"
                        FileName = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.FORM"
                        FormatType = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.FORM._HGHT"
                        Ex_FormatHeight = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.FORM._MTYPE"
                        Ex_FormatMType = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.FORM._SIZE"
                        Ex_FormatSize = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.FORM._STYPE"
                        Ex_FormatSType = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.FORM._WDTH"
                        Ex_FormatWidth = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.FORM.TYPE"
                        FormatType = data.Data
                        data.NextRow()
                    Case "OBJE.FILE.TITL"
                        Title = data.MultiLineData()
                        data.NextRow()
                    Case "OBJE.PLAC"
                        Place = New GEDPlace(data.Data)
                        data.NextRow()
                    Case "OBJE.RIN"
                        RIN = data.Data
                        data.NextRow()
                    Case Else
                        Debug.Print("GEDMediaRecord: Unhandled Key [{0}]", data.Key)
                        data.NextRow()
                End Select
            Else
                Exit Sub
            End If
        End While
    End Sub
End Class

