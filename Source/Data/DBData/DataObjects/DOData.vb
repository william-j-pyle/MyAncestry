Public Class DOData

    Private _data As New ArrayList
    Public ReadOnly Property Data As ArrayList
        Get
            Return _data
        End Get
    End Property

    Default Public ReadOnly Property Row(rowIndex) As String()
        Get
            Try
                Return _data(rowIndex)

            Catch ex As Exception
                Return {}
            End Try
        End Get
    End Property

    Public ReadOnly Property Rows As Integer
        Get
            Return _data.Count
        End Get
    End Property

    Public Sub addRow(data() As String)
        _data.Add(data)
    End Sub
End Class
