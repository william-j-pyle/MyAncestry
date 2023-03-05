'TODO: Not Finished
Public Class GEDFamilyRecord
    Public Property Key As GEDReference
    'Public Property Type As String
    Public Property Child As New GEDReferenceCollection
    Public Property Divorce As New GEDEventRecord
    Public Property Marriage As New GEDEventRecord
    Public Property Husband As GEDReference
    Public Property Wife As GEDReference
    Public Property Source As New GEDSourceReferenceCollection
    Public Property Media As New GEDMediaReferenceCollection
    Public Property Notes As String

    Public Sub addObject(data As GEDComData)
        Dim processedRoot As Boolean = False
        While data.HasNext
            If data.Key.Contains("FAM") Then
                Select Case data.Key
                    Case "FAM"
                        If processedRoot Then Exit Sub
                        Key = data.RefKey
                        processedRoot = True
                        data.NextRow()
                    Case "FAM._SREL"
                        data.NextRow()
                    Case "FAM.CHIL"
                        Child.Add(data.RefKey)
                        data.NextRow()
                    Case "FAM.DIV"
                        Divorce.addObject(data, data.Key)
                    Case "FAM.MARR"
                        Marriage.addObject(data, data.Key)
                    Case "FAM.HUSB"
                        Husband = data.RefKey
                        data.NextRow()
                    Case "FAM.WIFE"
                        Wife = data.RefKey
                        data.NextRow()
                    Case "FAM.NOTE"
                        Notes = data.MultiLineData()
                        data.NextRow()
                    Case "FAM.SOUR"
                        Source.addObject(data, data.Key)
                    Case Else
                        Debug.Print("GEDFamilyRecord: Unhandled Key [{0}]", data.Key)
                        data.NextRow()
                End Select
            Else
                Exit Sub
            End If
        End While
    End Sub

End Class