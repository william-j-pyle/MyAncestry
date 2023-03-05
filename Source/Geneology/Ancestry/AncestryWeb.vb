Public Class AncestryWeb
    Public Const BASEURL As String = "https://www.ancestry.com"

    Private treeID As String
    Private personID As String
    Private srcID As String
    Private collectionID As String
    Private pID As String
    Private rID As String

    Private pagePerson As Boolean
    Private pageGalleryMedia As Boolean
    Private pageGallerySource As Boolean
    Private pageViewerMedia As Boolean
    Private pageViewerSource As Boolean
    Private pageFacts As Boolean
    Private pageStory As Boolean
    Private pageGallery As Boolean
    Private pageHints As Boolean
    Private pageRoot As Boolean
    Private pageTree As Boolean

    Public Sub New()
        init()
    End Sub

    Private Sub init()
        pagePerson = False
        pageGalleryMedia = False
        pageGallerySource = False
        pageViewerMedia = False
        pageViewerSource = False
        pageFacts = False
        pageStory = False
        pageGallery = False
        pageHints = False
        pageRoot = False
        pageTree = False
        treeID = ""
        personID = ""
        srcID = ""
        collectionID = ""
        pID = ""
        rID = ""
    End Sub

    Public ReadOnly Property mediaSrcID() As String
        Get
            Return srcID
        End Get
    End Property
    Public ReadOnly Property mediaPID() As String
        Get
            Return pID
        End Get
    End Property
    Public ReadOnly Property mediaRID() As String
        Get
            Return rID
        End Get
    End Property
    Public ReadOnly Property mediaCollectionID() As String
        Get
            Return collectionID
        End Get
    End Property
    Public Property treeRIN() As String
        Get
            Return treeID
        End Get
        Set(ByVal value As String)
            treeID = value
        End Set
    End Property

    Public Property ID() As String
        Get
            Return personID
        End Get
        Set(ByVal value As String)
            personID = value
        End Set
    End Property

    Public Function isPagePerson() As Boolean
        Return pagePerson
    End Function

    Public Function isPageGalleryMedia() As Boolean
        Return pageGalleryMedia
    End Function

    Public Function isPageViewerMedia() As Boolean
        Return pageViewerMedia
    End Function
    Public Function isPageGallerySource() As Boolean
        Return pageGallerySource
    End Function

    Public Function isPageViewerSource() As Boolean
        Return pageViewerSource
    End Function

    Public Function isPageFacts() As Boolean
        Return pageFacts
    End Function

    Public Function isPageStory() As Boolean
        Return pageStory
    End Function

    Public Function isPageGallery() As Boolean
        Return pageGallery
    End Function

    Public Function isPageHints() As Boolean
        Return pageHints
    End Function

    Public Function isPageRoot() As Boolean
        Return pageRoot
    End Function

    Public Function isPageTree() As Boolean
        Return pageTree
    End Function

    Public Function pageName() As String
        If isPageRoot() Then
            Return "Root"
        End If
        If isPageTree() Then
            Return "Person.TreeView"
        End If
        If isPageGalleryMedia() Then
            Return "Person.Gallery.Media"
        End If
        If isPageGallerySource() Then
            Return "Person.Gallery.Source"
        End If
        If isPageViewerSource() Then
            Return "ImageViewer.Source"
        End If
        If isPageViewerMedia() Then
            Return "ImageViewer.Media"
        End If
        If isPageFacts() Then
            Return "Person.Facts"
        End If
        If isPageStory() Then
            Return "Person.Story"
        End If
        If isPageHints() Then
            Return "Person.Hints"
        End If
        Return "Other"
    End Function

    Public Sub setURI(uri As String)
        '   0   1  2                3              4           5        6         7                     8            9          10                   11                                    12
        'https: / /www.ancestry.com/family-tree   /person     /tree    /65171586 /person               /42139266146 /gallery    ?recordGalleryPage=1
        'https: / /www.ancestry.com/family-tree   /person     /tree    /65171586 /person               /42139266146 /gallery    ?galleryPage=1
        'https: / /www.ancestry.com/family-tree   /person     /tree    /65171586 /person               /42133212057 /facts
        'https: / /www.ancestry.com/family-tree   /person     /tree    /65171586 /person               /42133212057 /story
        'https: / /www.ancestry.com/family-tree   /person     /tree    /65171586 /person               /42133212057 /hints
        'https: / /www.ancestry.com/family-tree   /tree       /65171586/family   ?cfpid=42139266146
        'https: / /www.ancestry.com/mediaui-viewer/collection /1030    /tree     /65171586/person      /42133212057 /media              /8465e021-7737-4f68-a32c-8d9cd82b96f6 ?_phsrc=DLH2526&usePUBJs=true&galleryindex=1&sort=-created
        'https: / /www.ancestry.com/imageviewer   /collections/1265    /images   /sid_26543_1977_0086  ?usePUB=true&_phsrc=DLH2527&usePUBJs=true&sort=-created&pId=664621175
        'https: / /www.ancestry.com

        Dim u() As String
        Dim p() As String
        Dim i As Integer

        init()

        u = uri.Replace("?", "/").Split("/")
        If u.Length <= 4 Then
            pageRoot = True
        Else
            Select Case u(3)
                Case "family-tree"
                    If u(4) = "tree" Then
                        treeID = u(5)
                        pageTree = True
                        Try
                            If u(7).Contains("cfpid") Then
                                p = u(7).Replace("?", "&").Replace("=", "&").Split("&")
                                For i = 0 To UBound(p) - 1
                                    If p(i) = "cfpid" Then
                                        personID = p(i + 1)
                                        Exit For
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            personID = ""
                        End Try
                    ElseIf u(4) = "person" Then
                        treeID = u(6)
                        personID = u(8)
                        Try
                            pageFacts = u(9).StartsWith("facts")
                            pageStory = u(9).StartsWith("story")
                            pageHints = u(9).StartsWith("hints")
                            pageGallery = u(9).StartsWith("gallery")
                        Catch ex As Exception
                            pageFacts = False
                            pageStory = False
                            pageHints = False
                            pageGallery = False
                        End Try
                        Try
                            pageGalleryMedia = u(10).Contains("galleryPage")

                        Catch ex As Exception
                            pageGalleryMedia = False
                        End Try
                        Try
                            pageGallerySource = u(10).Contains("recordGalleryPage")

                        Catch ex As Exception
                            pageGallerySource = False
                        End Try
                    End If
                Case "mediaui-viewer"
                    srcID = "0"
                    Try
                        collectionID = u(5)
                        pID = ""
                        rID = u(11)
                        treeID = u(7)
                        personID = u(9)
                        pageViewerMedia = True
                    Catch ex As Exception
                        collectionID = ""
                        pID = ""
                        rID = ""
                        treeID = ""
                        personID = ""
                        pageViewerMedia = False
                    End Try
                Case "imageviewer"
                    srcID = "1"
                    collectionID = u(5)
                    rID = ""
                    Try
                        If u(8).Contains("pId") Then
                            p = u(8).Replace("?", "&").Replace("=", "&").Split("&")
                            For i = 0 To UBound(p) - 1
                                pID = i
                                If p(i) = "pId" Then
                                    pID = p(i + 1)
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        pID = "ERR" & UBound(u)
                    End Try

                    pageViewerSource = True
                Case Else
                    MsgBox("Unknown WebAPI path: /" & u(3))
            End Select
        End If
    End Sub


    Public Function uriPersonTree(Optional pPersonID As String = "{PERSON}", Optional pTreeID As String = "{TREE}") As String
        Dim uri As String = "{BASE}/family-tree/tree/{TREE}/family?cfpid={PERSON}"
        Return uri.Replace("{BASE}", BASEURL).Replace("{TREE}", pTreeID).Replace("{TREE}", treeID).Replace("{PERSON}", pPersonID).Replace("{PERSON}", personID)
    End Function

    Private Function uriPerson(pPersonID As String, pTreeID As String, pPath As String) As String
        Dim uri As String = "{BASE}/family-tree/person/tree/{TREE}/person/{PERSON}/{PATH}"
        Return uri.Replace("{BASE}", BASEURL).Replace("{TREE}", pTreeID).Replace("{TREE}", treeID).Replace("{PERSON}", pPersonID).Replace("{PERSON}", personID).Replace("{PATH}", pPath)
    End Function

    Public Function uriGalleryRecords(Optional pPersonID As String = "{PERSON}", Optional pTreeID As String = "{TREE}") As String
        Return uriPerson(pPersonID, pTreeID, "gallery?recordGalleryPage=1")
    End Function

    Public Function uriGalleryMedia(Optional pPersonID As String = "{PERSON}", Optional pTreeID As String = "{TREE}") As String
        Return uriPerson(pPersonID, pTreeID, "gallery?galleryPage=1")
    End Function


    Public Function uriPersonHints(Optional pPersonID As String = "{PERSON}", Optional pTreeID As String = "{TREE}") As String
        Return uriPerson(pPersonID, pTreeID, "hints")
    End Function


    Public Function uriPersonFacts(Optional pPersonID As String = "{PERSON}", Optional pTreeID As String = "{TREE}") As String
        Return uriPerson(pPersonID, pTreeID, "facts")
    End Function

    Public Function uriPersonStory(Optional pPersonID As String = "{PERSON}", Optional pTreeID As String = "{TREE}") As String
        Return uriPerson(pPersonID, pTreeID, "story")
    End Function




End Class
