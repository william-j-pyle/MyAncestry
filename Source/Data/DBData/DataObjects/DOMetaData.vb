Public Class DOMetaData
    Public Columns As New ArrayList()
    Public Property Rows As Integer
    Public Property RowSelected As Integer = -1

    Public Sub addColumn(column As DMColumn)
        column.Index = Columns.Count + 1
        Columns.Add(column)
    End Sub

    Public Sub addColumn(Name As String, Optional Type As DMColumnType = DMColumnType.ColumnString, Optional Visible As Boolean = True)
        addColumn(New DMColumn(Name, Type, Visible))
    End Sub

    Public Function getIndex(Name As String) As Integer
        For i As Integer = 0 To Columns.Count - 1
            If CType(Columns(i), DMColumn).Name = Name Then Return i
        Next
        Return -1
    End Function

    Public Sub setVisibility(columnIndex As Integer, visible As Boolean)
        CType(Columns(columnIndex), DMColumn).Visible = visible
    End Sub

    Public Sub setSortType(columnIndex As Integer, sortType As DMColumnSortType)
        CType(Columns(columnIndex), DMColumn).SortType = sortType
    End Sub

End Class
