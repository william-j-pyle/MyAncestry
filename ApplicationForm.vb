Public Class ApplicationForm
    Private Config As jConfig = My.Application.Config
    Private ReloadSettings As Boolean = True

    'CONTROLS
    Friend WithEvents jAppForm As jApp

    Public Sub New()
        SuspendLayout()
        PrepDataSets()
        With Me
            .AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            .AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            .BackColor = Config.Theme.BackColor(ThemeItemType.APP, ThemeColorItemState.ACTIVE)
            .Font = Config.Theme.Font(ThemeItemType.APP)
            .CausesValidation = False
            .ClientSize = New System.Drawing.Size(Config.Properties.GetProperty("FORM.WIDTH", 800), Config.Properties.GetProperty("FORM.HEIGHT", 450))
            .DoubleBuffered = True
            .FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            .Name = "ApplicationForm"
            .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            .Text = "AncestryApp"
        End With

        jAppForm = New jApp()
        Controls.Add(jAppForm)

        ResumeLayout(False)
        PerformLayout()

        jAppForm.ActiveViewKey = "PEOPLE"

    End Sub

    Private Sub PrepDataSets()
        Dim ds As DMDataSet
        Dim dsNames() As String = {"Dashboard", "Summary", "Person", "Hintlist", "Hint", "Personlist", "Medialist", "Mediaview", "Mediainfo", "Contacts", "Contactmessages", "Persontree", "Placelist", "Place", "Placemap", "Sourcelist", "Sourcecitations", "Source", "Tasklist", "Task"}

        For Each nm As String In dsNames
            ds = New DMDataSet
            ds.DataSetName = nm
            ds.addColumn(nm & "_One", DMColumnType.ColumnString)
            ds.addColumn(nm & "_Two", DMColumnType.ColumnBoolean)
            For i As Integer = 1 To 100
                ds.addDataRow("Row" & i, (i / 2) = Int(i / 2))
            Next
            My.Application.DataMgr.RegisterDataSet(ds.DataSetName, ds)
        Next
    End Sub

    Private Sub appClose()
        Close()
    End Sub

    Private Sub ToolbarItemClicked(ItemKey As String) Handles jAppForm.ToolbarItemClicked, jAppForm.MenubarItemClicked
        Debug.Print("jAppForm_ToolbarItemClicked({0})", ItemKey)
        Select Case ItemKey
            Case "ANCESTRYCOM"
                jAppForm.ActiveViewKey = "WEB"
            Case "FAMILYSEARCHCOM"
                jAppForm.ActiveViewKey = "MAIN"
            Case "PEOPLEVIEW"
                jAppForm.ActiveViewKey = "PEOPLE"
            Case "PLACESVIEW"
                jAppForm.ActiveViewKey = "PLACES"
            Case "SOURCESVIEW"
                jAppForm.ActiveViewKey = "SOURCES"
            Case "MEDIAVIEW"
                jAppForm.ActiveViewKey = "MEDIA"
            Case "TASKSVIEW"
                jAppForm.ActiveViewKey = "TASKS"
            Case "MESSAGESVIEW"
                jAppForm.ActiveViewKey = "MESSAGES"
            Case Else
        End Select
    End Sub

    Private Sub ApplicationForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        jAppForm.LoadSettings()
        ReloadSettings = False
    End Sub

End Class
