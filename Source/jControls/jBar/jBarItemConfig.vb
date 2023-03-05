Public Class jBarItemConfig

    Private _attributes As New Dictionary(Of String, String)
    Public Property Attribute(Name As String) As String
        Get
            If _attributes.ContainsKey(Name.ToUpper) Then
                Return _attributes(Name.ToUpper)
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            If _attributes.ContainsKey(Name.ToUpper) Then
                _attributes.Remove(Name.ToUpper)
            End If
            _attributes.Add(Name.ToUpper, value)
        End Set
    End Property

    Public Property GroupKey As String = ""
    Public Property ItemKey As String = ""
    Public Property ParentItemKey As String = ""

    Public Property ItemType As BarItemType

    Public Property IconKey As String = ""
    Public Property Caption As String = ""

    Private _Enabled As Boolean = True
    Public Property Enabled As Boolean
        Get
            Return _Enabled
        End Get
        Set(value As Boolean)
            _Enabled = value
            If StripItem IsNot Nothing Then
                StripItem.Enabled = value
            End If
        End Set
    End Property

    Private _Visible As Boolean = True
    Public Property Visible As Boolean
        Get
            Return _Visible
        End Get
        Set(value As Boolean)
            _Visible = value
            If StripItem IsNot Nothing Then
                StripItem.Visible = value
            End If
        End Set
    End Property

    Private _Checked As Boolean = False
    Public Property Checked As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
            If StripItem IsNot Nothing Then
                Try
                    CType(StripItem, ToolStripButton).Checked = value
                Catch ex As Exception
                    Debug.Print("BLABLA")
                End Try
            End If
        End Set
    End Property

    Protected Friend StripItem As ToolStripItem

End Class
