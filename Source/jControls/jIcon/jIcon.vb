Public Class JIcon
    Private ico As Bitmap

    Private workingIco As Bitmap

    Public Sub New()
        ico = Nothing
        workingIco = Nothing
    End Sub

    Public Sub New(filename As String)
        ico = Nothing
        workingIco = Nothing
        Load(filename)
    End Sub

    Public Function Load(filename As String) As JIcon
        ico = Image.FromFile(filename)
        Return Me
    End Function

    Public Function Reset() As JIcon
        workingIco = Nothing
        Return Me
    End Function

    Public Function Resize(size As Size) As JIcon
        If workingIco Is Nothing Then
            workingIco = IResize(ico, size)
        Else
            workingIco = IResize(workingIco, size)
        End If
        Return Me
    End Function

    Private Function IResize(ico As Image, size As Size) As Image
        If size.Height <> ico.Height Or size.Width <> ico.Width Then
            Return New Bitmap(ico, size)
        End If
        Return ico
    End Function

    Public Function Recolor(newColor As Color) As JIcon
        If workingIco Is Nothing Then
            workingIco = IReColor(ico, newColor)
        Else
            workingIco = IReColor(workingIco, newColor)
        End If
        Return Me
    End Function


    Private Function IReColor(bitmap As Bitmap, newColor As Color) As Bitmap
        Dim originalColor As Color
        Dim newBitmap As Bitmap
        Try
            newBitmap = New Bitmap(bitmap.Width, bitmap.Height)
            For i As Integer = 0 To bitmap.Width - 1
                For j As Integer = 0 To bitmap.Height - 1
                    originalColor = bitmap.GetPixel(i, j)
                    newBitmap.SetPixel(i, j, Color.FromArgb(originalColor.A, newColor))
                Next
            Next
        Catch e As Exception
            newBitmap = bitmap
        End Try
        Return newBitmap
    End Function

    Public Function Icon(Optional Original As Boolean = False) As Bitmap
        If workingIco Is Nothing Or Original Then
            Return ico
        Else
            Return workingIco
        End If
    End Function
End Class
