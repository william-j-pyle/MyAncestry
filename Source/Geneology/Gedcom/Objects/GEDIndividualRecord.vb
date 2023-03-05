'TODO: Not Finished
Public Class GEDIndividualRecord
    Public Property Key As GEDReference
    Public Property Name As New GEDNameCollection
    Public Property Sex As New GEDSex
    Public Property Source As New GEDSourceReferenceCollection
    Public Property Media As New GEDMediaReferenceCollection
    Public Property ChildFamily As New GEDFamilyReferenceCollection("FAMC")
    Public Property SpouseFamily As New GEDFamilyReferenceCollection("FAMS")
    Public Property Events As New GEDEventCollection
    Public Property Tags As New GEDReferenceCollection
    Public Property Note As String

    Public Sub addObject(data As GEDComData)
        Dim processedRoot As Boolean = False
        While data.HasNext
            If data.Key.StartsWith("INDI") Then
                Select Case data.Key
                    Case "INDI"
                        If processedRoot Then
                            Exit Sub
                        End If
                        Key = data.RefKey
                        Key.Type = "INDI"
                        processedRoot = True
                        data.NextRow()
                    Case "INDI.FACT"
                        data.NextRow()
                    Case "INDI.FACT.TYPE"
                        data.NextRow()
                    Case "INDI.EVEN"
                        Events.addObject(data, data.Key)
                    Case "INDI.BIRT"
                        Events.addObject(data, data.Key)
                    Case "INDI.RESI"
                        Events.addObject(data, data.Key)
                    Case "INDI._DEG"
                        Events.addObject(data, data.Key)
                    Case "INDI.PROP"
                        Events.addObject(data, data.Key)
                    Case "INDI._MILT"
                        Events.addObject(data, data.Key)
                    Case "INDI.BURI"
                        Events.addObject(data, data.Key)
                    Case "INDI.BAPM"
                        Events.addObject(data, data.Key)
                    Case "INDI.CHR"
                        Events.addObject(data, data.Key)
                    Case "INDI.DEAT"
                        Events.addObject(data, data.Key)
                    Case "INDI.MARR"
                        Events.addObject(data, data.Key)
                    Case "INDI.NATU"
                        Events.addObject(data, data.Key)
                    Case "INDI.PROB"
                        Events.addObject(data, data.Key)
                    Case "INDI.FAMC"
                        ChildFamily.Add(data.RefKey)
                        data.NextRow()
                    Case "INDI.FAMS"
                        SpouseFamily.Add(data.RefKey)
                        data.NextRow()
                    Case "INDI.NAME"
                        Name.addObject(data)
                    Case "INDI.NOTE"
                        Note = data.MultiLineData()
                        data.NextRow()
                    Case "INDI.OBJE"
                        Media.addObject(data, data.Key)
                    Case "INDI.SEX"
                        Sex.addObject(data, data.Key)
                    Case "INDI.SOUR"
                        Source.addObject(data, data.Key)
                    Case "INDI._MTTAG"
                        Tags.Add(data.RefKey)
                        data.NextRow()
                    Case Else
                        Debug.Print("GEDIndividualRecord: Unhandled Key [{0}]", data.Key)
                        data.NextRow()
                End Select
            Else
                Exit Sub
            End If
        End While
    End Sub

End Class