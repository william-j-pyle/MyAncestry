Public Class jBoxViewItemList
    Inherits jBoxViewItem

    Const DEF_BOXLOCATION As jBoxPlacementType = jBoxPlacementType.BoxTopLeft

    Public Sub New()
        Caption = "Test Item"
        HeaderText = "Header Text"
        Description = "This is a large description"
        ShowContextMenu = False
        ShowSearch = False
        ShowToolbar = False
        Dim ctl As ListBox = New ListBox
        AddHandler ctl.Click, AddressOf RaiseFocusEvent
        Controls.Add(ctl)
        With ctl
            .Dock = DockStyle.Fill
            .BorderStyle = FlatStyle.Flat
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOX, ThemeColorItemState.ACTIVE)
            For i As Integer = 1 To 100
                .Items.Add("Row " & i)
            Next
        End With

    End Sub

    Public Overrides ReadOnly Property PreferedBoxLocation As jBoxPlacementType
        Get
            Return DEF_BOXLOCATION
        End Get
    End Property

    Private Sub jBoxViewItemTest_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged
        Dock = DockStyle.Fill
    End Sub
End Class
