Public Class jButton
    Inherits Button

    Private _Icon As Bitmap
    Private _HoverIcon As Bitmap

    Public Property Icon As Bitmap
        Get
            Return _Icon
        End Get
        Set(value As Bitmap)
            _Icon = value
            Image = value
        End Set
    End Property

    Public Property HoverIcon As Bitmap
        Get
            Return _HoverIcon
        End Get
        Set(value As Bitmap)
            _HoverIcon = value
        End Set
    End Property

    Public Property HoverBackColor As Color
        Get
            Return FlatAppearance.MouseOverBackColor
        End Get
        Set(value As Color)
            FlatAppearance.MouseOverBackColor = value
        End Set
    End Property

    Public Property PressedBackColor As Color
        Get
            Return FlatAppearance.MouseDownBackColor
        End Get
        Set(value As Color)
            FlatAppearance.MouseDownBackColor = value
        End Set
    End Property


    Public Sub New()
        SetStyle(ControlStyles.Selectable, False)
        SetStyle(ControlStyles.ResizeRedraw, True)
        FlatStyle = FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        BackColor = Color.Transparent
        Text = ""
    End Sub

    Private Sub jButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        If HoverIcon IsNot Nothing Then
            Image = HoverIcon
            Refresh()
        End If

    End Sub

    Private Sub jButton_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If HoverIcon IsNot Nothing Then
            Image = Icon
            Refresh()
        End If

    End Sub
End Class