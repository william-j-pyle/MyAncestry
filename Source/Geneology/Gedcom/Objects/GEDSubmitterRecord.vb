
Public Class GEDSubmitterRecord
    Public Property Key As GEDReference
    Public Property Name As String

    Public Sub addObject(data As GEDComData)
        Dim processedRoot As Boolean = False
        While data.HasNext
            If data.Key.Contains("SUBM") Then
                Select Case data.Key
                    Case "SUBM"
                        If processedRoot Then
                            Exit Sub
                        End If
                        Key = data.RefKey
                        processedRoot = True
                        data.NextRow()
                    Case "SUBM.NAME"
                        Name = data.Data
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

