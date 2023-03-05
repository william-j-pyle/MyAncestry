'TODO: Not Finished
Public Class GEDSex
    Public Sex As String = ""
    Public Source As New GEDSourceReferenceCollection

    Public Sub addObject(data As GEDComData, fileKey As String)
        Dim processedRoot As Boolean = False
        While data.HasNext
            If data.Key.Contains(fileKey) Then
                Select Case data.Key.Replace(fileKey, "SEX")
                    Case "SEX"
                        If processedRoot Then
                            Exit Sub
                        End If
                        Sex = data.Data
                        processedRoot = True
                        data.NextRow()
                    Case "SEX.SOUR"
                        Source.addObject(data, data.Key)
                    Case Else
                        Debug.Print("GEDSex: Unhandled Key [{0}]", data.Key)
                        data.NextRow()
                End Select
            Else
                Exit Sub
            End If
        End While
    End Sub
End Class