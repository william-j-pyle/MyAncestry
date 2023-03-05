Imports System.Runtime.InteropServices
Imports Newtonsoft.Json
Imports System.Data.SQLite


<ComVisible(True), ClassInterface(ClassInterfaceType.AutoDual)>
Public Class DBData
    Public GedCom As GEDComFile
    Private dataSets As New Dictionary(Of String, DMDataSet)

    Public Event SelectionChanged(dataSetName As String, selectedRowIdx As Integer)
    Public Event DataChanged(dataSetName As String)


    Public Sub LoadGedCom(fileName As String)
        GedCom = New GEDComFile(fileName)
    End Sub

    Public Function getMetaData(dataSetName As String) As DOMetaData
        Return dataSets(dataSetName).Metadata
    End Function

    Public Function getData(dataSetName As String) As DOData
        Return dataSets(dataSetName).Data
    End Function

    Public Function getDataRow(dataSetName As String, rowIdx As Integer) As String()
        Return dataSets(dataSetName).Data(rowIdx - 1)
    End Function

    <ComVisible(True)>
    Public Function getMetaDataAsJson(dataSetName As String) As String
        Return JsonConvert.SerializeObject(getMetaData(dataSetName), Formatting.Indented)
    End Function

    <ComVisible(True)>
    Public Function getDataAsJson(dataSetName As String) As String
        Return JsonConvert.SerializeObject(getData(dataSetName), Formatting.Indented)
    End Function

    <ComVisible(True)>
    Public Function getDataRowAsJson(dataSetName As String, rowIdx As Integer) As String
        Return JsonConvert.SerializeObject(getDataRow(dataSetName, rowIdx), Formatting.Indented)
    End Function

    <ComVisible(True)>
    Public Sub NotifyOnDataChange(dataSetName As String, ControlKey As String)

    End Sub

    <ComVisible(True)>
    Public Sub NotifyOnSelectionChange(dataSetName As String, ControlKey As String)

    End Sub

    <ComVisible(True)>
    Public Sub RowSelected(dataSetName As String, rowIdx As Integer, Optional ControlKey As String = "")
        If getMetaData(dataSetName).RowSelected = rowIdx Then Exit Sub
        Debug.Print("Datamanager: RowSelected {0} {1}", dataSetName, rowIdx)
        getMetaData(dataSetName).RowSelected = rowIdx
        RaiseEvent SelectionChanged(dataSetName, rowIdx)
    End Sub

    Public Sub RegisterDataSet(dataSetName As String, dataSet As DMDataSet)
        dataSets.Add(dataSetName, dataSet)
    End Sub

    Public Sub InsertRow(dataSetName As String, row As DODataRow)

    End Sub

    <ComVisible(True)>
    Public Sub InsertRowAsJson(dataSetName As String, jsonRow As String)

    End Sub

    Public Sub DeleteRow(dataSetName As String, rowIdx As Integer)

    End Sub

    Public Sub UpdateRow(dataSetName As String, rowBefore As DODataRow, rowAfter As DODataRow)

    End Sub

    <ComVisible(True)>
    Public Sub UpdateRowAsJson(dataSetName As String, jsonRowBefore As String, jsonRowAfter As String)

    End Sub

    Public Sub LoadDataSet(dataSetName As String, fileName As String)

    End Sub

    Public Sub SaveDataSet(dataSetName As String, fileName As String)

    End Sub

End Class