Public Class DMColumn
    Public Property Index As Integer
    Public Property Name As String
    Public Property Type As DMColumnType = DMColumnType.ColumnString
    Public Property SortType As DMColumnSortType = DMColumnSortType.SortNone
    Public Property Visible As Boolean = True

    Public Sub New()

    End Sub

    Public Sub New(Name As String, Optional Type As DMColumnType = DMColumnType.ColumnString, Optional Visible As Boolean = True)
        Me.Name = Name
        Me.Type = Type
        Me.Visible = Visible
    End Sub
End Class
