Imports System.Timers

#Const SHOW_DEBUG = False

Public Class jSearch
    Inherits Panel

    Private ReadOnly Theme As Theme = My.Application.Config.Theme

    Public Event SearchRequest(ByVal searchCriteria As String)
    Public Event SearchCleared()
    Public Event ChildGotFocus()

    Public Property SearchPrompt As String = "Search..."

    Private WithEvents ctl As Panel
    Private WithEvents txt As TextBox
    Private WithEvents ico As Panel

    Private icoSearch As Bitmap
    Private icoClose As Bitmap
    Private prevTxt As String = ""
    Private WithEvents trigger As Timer


    Public Sub New()
        trigger = New Timer(600)
        icoSearch = Theme.Icon("SEARCH", New Size(16, 16), Theme.ForeColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED))
        icoClose = Theme.Icon("X", New Size(16, 16), Theme.ForeColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED))
        ctl = New Panel
        With ctl
            .Margin = New Padding(0)
            .Padding = New Padding(8, 1, 1, 1)
            .ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED)
            .BackColor = Theme.BackColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED)
            .Dock = DockStyle.Fill
        End With
        Controls.Add(ctl)

        ico = New Panel
        With ico
            .Size = New Size(20, 20)
            .Dock = DockStyle.Right
            .BackgroundImage = icoSearch
            .BackgroundImageLayout = ImageLayout.Center
            .BackColor = Color.Transparent
            .BorderStyle = BorderStyle.None
            .Margin = New Padding(0)
            .Padding = New Padding(0)
        End With
        ctl.Controls.Add(ico)

        txt = New TextBox
        With txt
            .Dock = DockStyle.Fill
            .Location = New Point(0, 0)
            .BackColor = ctl.BackColor
            .ForeColor = ctl.ForeColor
            .Margin = New Padding(0, 0, 8, 0)
            .Padding = New Padding(0)
            .BorderStyle = BorderStyle.None
            .HideSelection = True
            .TabIndex = 0
            .TabStop = False
            .AcceptsTab = False
            .MaxLength = 80
            .Multiline = False
            .Text = SearchPrompt
        End With
        ctl.Controls.Add(txt)
        Focus()
        prevTxt = ""
    End Sub

    Private Sub txt_GotFocus(sender As Object, e As EventArgs) Handles txt.GotFocus
        If SearchPrompt = txt.Text Then
            txt.Text = ""
        End If
        txt.ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.SELECTED)
        RaiseEvent ChildGotFocus()
    End Sub

    Private Sub txt_LostFocus(sender As Object, e As EventArgs) Handles txt.LostFocus
        If txt.Text = "" Then
            txt.Text = SearchPrompt
        End If
        txt.ForeColor = Theme.ForeColor(ThemeItemType.DOCKBOXTAB, ThemeColorItemState.UNSELECTED)
    End Sub

    Private Sub txt_TextChanged(sender As Object, e As EventArgs) Handles txt.TextChanged
#If SHOW_DEBUG Then
        Debug.Print("txt_TextChanged: C-{0}   P-{0}", txt.Text, prevTxt)
#End If
        If txt.Text = "" Or txt.Text = SearchPrompt Then
            If prevTxt.Length > 0 Then
                RaiseEvent SearchCleared()
            End If
            prevTxt = ""
            ico.BackgroundImage = icoSearch
            ico.Tag = 0
            Exit Sub
        End If
        If prevTxt = "" Then
            ico.Tag = 1
            ico.BackgroundImage = icoClose
        End If
        prevTxt = txt.Text
    End Sub

    Private Sub txt_KeyDown(sender As Object, e As KeyEventArgs) Handles txt.KeyDown
        trigger.Stop()
        If txt.Text = "" Or txt.Text = SearchPrompt Then Exit Sub
        If e.KeyValue = Keys.Enter Then
#If SHOW_DEBUG Then
            Debug.Print("txt_KeyDown: C-{0}   P-{0}", txt.Text, prevTxt)
#End If
            e.Handled = True
            RaiseEvent SearchRequest(txt.Text)
            Focus()
        Else
            trigger.Start()
        End If
    End Sub

    Private Sub trigger_Elapsed(sender As Object, e As ElapsedEventArgs) Handles trigger.Elapsed
#If SHOW_DEBUG Then
        Debug.Print("trigger_Elapsed: C-{0}   P-{0}", txt.Text, prevTxt)
#End If
        trigger.Stop()
        If txt.Text = "" Or txt.Text = SearchPrompt Then Exit Sub
        RaiseEvent SearchRequest(txt.Text)
    End Sub

    Private Sub ico_Click(sender As Object, e As EventArgs) Handles ico.Click
        If ico.Tag = 1 Then
            ico.Tag = 0
            txt.Text = SearchPrompt
            RaiseEvent SearchCleared()
            Focus()
        End If
    End Sub

#If SHOW_DEBUG Then
    Private Sub jSearch_SearchCleared() Handles Me.SearchCleared
        Debug.Print("SEARCH: **Cleared**")
    End Sub

    Private Sub jSearch_SearchRequest(searchCriteria As String) Handles Me.SearchRequest
        Debug.Print("SEARCH: {0}", searchCriteria)
    End Sub
#End If

End Class
