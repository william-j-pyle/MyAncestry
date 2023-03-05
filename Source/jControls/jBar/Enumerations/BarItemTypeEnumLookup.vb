Public Module BarItemTypeEnumLookup
    Public Function BarItemTypeEnumFromString(value As String) As BarItemType
        Select Case value.ToUpper
            Case BarItemType.Button.ToString.ToUpper
                Return BarItemType.Button
            Case BarItemType.CheckedButton.ToString.ToUpper
                Return BarItemType.CheckedButton
            Case BarItemType.Seperator.ToString.ToUpper
                Return BarItemType.Seperator
            Case BarItemType.Label.ToString.ToUpper
                Return BarItemType.Label
            Case Else
                Return Nothing
        End Select
    End Function
End Module
