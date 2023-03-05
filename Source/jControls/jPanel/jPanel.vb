Imports System.ComponentModel
Imports System.Timers

Public Class jPanel
    Inherits Panel


    Private _BorderWidthTop As Integer = 0
    Private _BorderWidthBottom As Integer = 0
    Private _BorderWidthLeft As Integer = 0
    Private _BorderWidthRight As Integer = 0
    Private _OnHoverEnabled As Boolean = False
    Private _IsHovering As Boolean = False
    Private WithEvents overTimer As Timer



    Public Event OnHover(sender As jPanel, isOver As Boolean)

    Public Property OnHoverEventsEnabled As Boolean
        Get
            Return _OnHoverEnabled
        End Get
        Set(value As Boolean)
            _OnHoverEnabled = value
            If value Then
                For Each ctl In Controls
                    AddHandler CType(ctl, Control).MouseEnter, AddressOf MouseOver
                Next
            End If
        End Set
    End Property

    Public Property IsHovering As Boolean
        Get
            Return _IsHovering
        End Get
        Set(value As Boolean)
            _IsHovering = value
            RaiseEvent OnHover(Me, _IsHovering)
        End Set
    End Property

    <Browsable(False),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Shadows Property BorderStyle As BorderStyle = BorderStyle.None

    <Category("Borders"), Description("Specifies the width of the top border.")>
    Public Property BorderWidthTop As Integer
        Get
            Return _BorderWidthTop
        End Get
        Set
            _BorderWidthTop = Value
            Padding = New Padding(Padding.Left, Value, Padding.Right, Padding.Bottom)
        End Set
    End Property

    <Category("Borders"), Description("Specifies the color of the top border.")>
    Public Property BorderColorTop As Color = BackColor

    <Category("Borders"), Description("Specifies the width of the bottom border.")>
    Public Property BorderWidthBottom As Integer
        Get
            Return _BorderWidthBottom
        End Get
        Set
            _BorderWidthBottom = Value
            Padding = New Padding(Padding.Left, Padding.Top, Padding.Right, Value)
        End Set
    End Property
    <Category("Borders"), Description("Specifies the color of the bottom border.")>
    Public Property BorderColorBottom As Color = BackColor
    Public Property BorderWidthLeft As Integer
        Get
            Return _BorderWidthLeft
        End Get
        Set
            _BorderWidthLeft = Value
            Padding = New Padding(Value, Padding.Top, Padding.Right, Padding.Bottom)
        End Set
    End Property

    Public Property BorderColorLeft As Color = BackColor
    Public Property BorderWidthRight As Integer
        Get
            Return _BorderWidthRight
        End Get
        Set
            _BorderWidthRight = Value
            Padding = New Padding(Padding.Left, Padding.Top, Value, Padding.Bottom)
        End Set
    End Property

    Public Property BorderColorRight As Color = BackColor


    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.DoubleBuffer Or ControlStyles.AllPaintingInWmPaint, True)
        overTimer = New Timer(300)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Using brush As SolidBrush = New SolidBrush(BackColor)
            e.Graphics.FillRectangle(brush, ClientRectangle)
        End Using
        Dim jPen As Pen
        'LEFT
        If BorderWidthLeft > 0 Then
            jPen = New Pen(BorderColorLeft, BorderWidthLeft * 2)
            e.Graphics.DrawLine(jPen, 0, 0, 0, ClientSize.Height - 1)
        End If
        'RIGHT
        If BorderWidthRight > 0 Then
            jPen = New Pen(BorderColorRight, BorderWidthRight * 2)
            e.Graphics.DrawLine(jPen, ClientSize.Width - 1, 0, ClientSize.Width - 1, ClientSize.Height - 1)
        End If
        'TOP
        If BorderWidthTop > 0 Then
            jPen = New Pen(BorderColorTop, BorderWidthTop * 2)
            e.Graphics.DrawLine(jPen, 0, 0, ClientSize.Width - 1, 0)
        End If
        'BOTTOM
        If BorderWidthBottom > 0 Then
            jPen = New Pen(BorderColorBottom, BorderWidthBottom * 2)
            e.Graphics.DrawLine(jPen, 0, ClientSize.Height - 1, ClientSize.Width - 1, ClientSize.Height - 1)
        End If
        'e.Graphics.DrawRectangle(mypen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1)
    End Sub

    Private Sub Process_MouseOver()
        Try
            If ClientRectangle.Contains(PointToClient(Cursor.Position)) Then
                overTimer.Stop()
                If Not IsHovering Then
                    IsHovering = True
                End If
                overTimer.Start()
            Else
                If IsHovering Then
                    overTimer.Stop()
                    IsHovering = False
                End If
            End If
        Catch ex As Exception
            'Just ignore anything here...
        End Try
    End Sub

    Private Sub MouseOver() Handles Me.MouseEnter, Me.MouseLeave, overTimer.Elapsed
        If Not OnHoverEventsEnabled Then Exit Sub
        Try
            If InvokeRequired Then
                Invoke(New Action(Sub() Process_MouseOver()))
            Else
                Process_MouseOver()
            End If
        Catch ex As Exception
            'IGNORE ALL
        End Try
    End Sub

    Private Sub jPanel_ControlAdded(sender As Object, e As ControlEventArgs) Handles Me.ControlAdded
        If OnHoverEventsEnabled Then
            AddHandler e.Control.MouseEnter, AddressOf MouseOver
        End If
    End Sub
End Class