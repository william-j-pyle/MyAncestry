Imports System.IO

Public Class Properties

    Private m_Properties As Hashtable
    Private m_Filename As String

    Public Sub New()
        m_Properties = New Hashtable()
        m_Filename = ""
    End Sub

    Public Sub New(ByVal filename As String)
        m_Properties = New Hashtable
        Load(filename)
    End Sub

    Public Sub Save()
        If m_Filename = "" Then Exit Sub
        Save(m_Filename)
    End Sub

    Public Sub Save(ByVal filename As String)
        Dim sw As StreamWriter
        If File.Exists(filename) Then
            File.Delete(filename)
        End If
        sw = New StreamWriter(filename)
        Dim m_Keys As ArrayList = New ArrayList
        For Each key As String In m_Properties.Keys
            m_Keys.Add(key)
        Next
        m_Keys.Sort()
        For Each key As String In m_Keys
            sw.WriteLine(key & "=" & m_Properties.Item(key))
        Next
        sw.Flush()
        sw.Close()
    End Sub

    Public Sub Load(ByVal filename As String)
        Dim sr As StreamReader
        Dim line As String
        Dim key As String
        Dim value As String

        m_Filename = filename
        If File.Exists(filename) Then
            sr = New StreamReader(filename)

            Do While sr.Peek <> -1
                line = sr.ReadLine
                If line = Nothing OrElse line.Length = 0 OrElse line.StartsWith("#") Then
                    Continue Do
                End If

                key = line.Split("=")(0)
                value = line.Split("=")(1)

                PutProperty(key, value)

            Loop

            sr.Close()
        End If

    End Sub

    Public Sub Clear()
        m_Properties.Clear()
    End Sub

    Public Function Keys() As ArrayList
        Dim m_Keys As ArrayList = New ArrayList
        For Each key As String In m_Properties.Keys
            m_Keys.Add(key)
        Next
        Return m_Keys
    End Function

    Public Sub RemoveProperty(ByVal key As String)
        If m_Properties.ContainsKey(key.ToUpper) Then
            m_Properties.Remove(key.ToUpper)
        End If
    End Sub

    Public Sub PutProperty(ByVal key As String, ByVal value As String)
        RemoveProperty(key.ToUpper)
        m_Properties.Add(key.ToUpper, value)
    End Sub

    Public Function GetProperty(ByVal key As String) As String
        Dim rtn As String = Nothing
        If m_Properties.ContainsKey(key.ToUpper) Then
            rtn = m_Properties.Item(key.ToUpper)
            If rtn.Contains("%") Then
                rtn = variableReplace(rtn)
            End If
        End If
        Return rtn
    End Function

    Private Function variableReplace(ByVal value As String) As String
        Dim rtn As String = value
        Dim v As String
        Dim r As String
        Dim s As Integer, e As Integer
        s = value.IndexOf("%")
        e = value.IndexOf("%", s + 1)
        If e > s Then
            v = value.Substring(s + 1, e - s - 1)
            r = GetProperty(v)
            rtn = rtn.Replace("%" & v & "%", r)
        End If
        Return rtn
    End Function

    Public Function GetProperty(ByVal key As String, ByVal defValue As String) As String
        If Not m_Properties.ContainsKey(key.ToUpper) Then
            PutProperty(key.ToUpper, defValue)
        End If
        Return GetProperty(key.ToUpper)
    End Function

    Public Function CnvBoolTrueYorN(value As Boolean) As String
        If value Then Return "Y" Else Return "N"
    End Function

    Public Function CnvYtoBoolTrue(value As String) As Boolean
        Return value.ToLower.Equals("y")
    End Function

End Class