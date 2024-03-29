﻿'TODO: Not Finished
Public Class GEDTagRecord

    Public Property Key As GEDReference
    Public Property Name As String
    Public Property RIN As String

    Public Sub addObject(data As GEDComData)
        Dim processedRoot As Boolean = False
        While data.HasNext
            If data.Key.Contains("_MTTAG") Then
                Select Case data.Key
                    Case "_MTTAG"
                        If processedRoot Then
                            Exit Sub
                        End If
                        Key = data.RefKey
                        processedRoot = True
                        data.NextRow()
                    Case "_MTTAG.NAME"
                        Name = data.Data
                        data.NextRow()
                    Case "_MTTAG.RIN"
                        RIN = data.Data
                        data.NextRow()
                    Case Else
                        Debug.Print("GEDTagRecord: Unhandled Key [{0}]", data.Key)
                        data.NextRow()
                End Select
            Else
                Exit Sub
            End If
        End While
    End Sub
End Class

