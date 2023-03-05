Public Class DMDataSet

    Public Property DataSetName As String

    Private _MetaData As DOMetaData
    Public ReadOnly Property Metadata As DOMetaData
        Get
            Return _MetaData
        End Get
    End Property

    Private _Data As DOData
    Public ReadOnly Property Data As DOData
        Get
            Return _Data
        End Get
    End Property


    Public Sub New()
        _MetaData = New DOMetaData()
        _Data = New DOData
    End Sub

    Public Sub addColumn(name As String, columnType As DMColumnType, Optional Visible As Boolean = True)
        _MetaData.addColumn(name, columnType, Visible)
    End Sub

    Public Sub setColumnVisibility(name As String, visible As Boolean)
        setColumnVisibility(_MetaData.getIndex(name), visible)
    End Sub
    Public Sub setColumnVisibility(idx As Integer, visible As Boolean)
        _MetaData.setVisibility(idx, visible)
    End Sub
    Public Sub sortOnColumn(name As String, sortType As DMColumnSortType)
        sortOnColumn(_MetaData.getIndex(name), sortType)
    End Sub
    Public Sub sortOnColumn(idx As Integer, sortType As DMColumnSortType)
        _MetaData.setSortType(idx, sortType)
    End Sub
    Public Sub addDataRow(ParamArray data() As String)
        _Data.addRow(data)
        _MetaData.Rows = _Data.Rows
    End Sub
    Public Sub deleteDataRow(rowIdx As Integer)
        Throw New Exception("Functionality not yet implemented")
    End Sub
    Public Sub updateDataRow(rowIdx As Integer, ParamArray data() As String)
        Throw New Exception("Functionality not yet implemented")
    End Sub
    Public Sub updateData(rowIdx As Integer, columnIdx As Integer, data As String)
        Throw New Exception("Functionality not yet implemented")
    End Sub
End Class
