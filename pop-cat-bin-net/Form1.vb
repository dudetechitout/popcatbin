Imports Microsoft.Win32
Imports System.IO
Imports System.ComponentModel

Public Class Form1
    Dim icos_path As String = Environment.GetEnvironmentVariable("UserProfile") & "\popcat\"
    Dim iconFileNames As String() = {
            "atmIwDw04nJ3buvDDEXo.ico",
            "e9tCRkrA0yoN4ddiJEwf.ico",
            "Ks7NG6jZaTfeILNIVqx9.ico",
            "Z9Ka9hceumSD7L8fBWHW.ico",
            "Szs348wYMsMv0kEzRF2F.ico",
            "ZHfTZbFjP4duJXeUluCE.ico"
        }

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not System.IO.Directory.Exists(icos_path) Then
            System.IO.Directory.CreateDirectory(icos_path)
        End If

        For Each icon_file_name As String In iconFileNames
            If Not File.Exists(icos_path & icon_file_name) Then
                Try
                    Dim icon_name() As String = icon_file_name.Split(".")
                    Dim icon_resource As Icon = DirectCast(My.Resources.ResourceManager.GetObject(icon_name(0)), Icon)

                    Dim filePath As String = Path.Combine(icos_path, icon_file_name)

                    Using fileStream As New FileStream(filePath, FileMode.Create)
                        icon_resource.Save(fileStream)
                    End Using

                Catch ex As Exception
                    MessageBox.Show("Unable to write resources to directory. Check permissions.")
                End Try
            End If
        Next
    End Sub

    Private Sub SetRecycleBinIcon(ByVal name As String, ByVal iconIndex As Integer, ByVal iconFilePath As String)
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{645FF040-5081-101B-9F08-00AA002F954E}\DefaultIcon", True)
        regKey.SetValue(name, iconFilePath & "," & iconIndex)
        regKey.Close()
        NativeMethods.SHChangeNotify(&H8000000, &H1000, IntPtr.Zero, IntPtr.Zero)
    End Sub


    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        If PictureBox1.Visible Then
            PictureBox1.Hide()
        End If
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        If Not PictureBox1.Visible Then
            PictureBox1.Show()
        End If
    End Sub

    Private Sub PictureBox4_MouseHover(sender As Object, e As EventArgs) Handles PictureBox4.MouseHover
        If PictureBox4.Visible Then
            PictureBox4.Hide()
        End If
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        If Not PictureBox4.Visible Then
            PictureBox4.Show()
        End If
    End Sub

    Private Sub PictureBox3_MouseHover(sender As Object, e As EventArgs) Handles PictureBox3.MouseHover
        If PictureBox3.Visible Then
            PictureBox3.Hide()
        End If
    End Sub

    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox6.MouseLeave
        If Not PictureBox3.Visible Then
            PictureBox3.Show()
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        SetRecycleBinIcon("", 0, icos_path + iconFileNames(0))
        SetRecycleBinIcon("empty", 0, icos_path + iconFileNames(0))
        SetRecycleBinIcon("full", 0, icos_path + iconFileNames(2))
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        SetRecycleBinIcon("", 0, icos_path + iconFileNames(5))
        SetRecycleBinIcon("empty", 0, icos_path + iconFileNames(5))
        SetRecycleBinIcon("full", 0, icos_path + iconFileNames(4))
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        SetRecycleBinIcon("", 0, icos_path + iconFileNames(1))
        SetRecycleBinIcon("empty", 0, icos_path + iconFileNames(1))
        SetRecycleBinIcon("full", 0, icos_path + iconFileNames(3))
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://dudetechitout.com/")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        SetRecycleBinIcon("", -55, "%SystemRoot%\System32\imageres.dll")
        SetRecycleBinIcon("empty", -55, "%SystemRoot%\System32\imageres.dll")
        SetRecycleBinIcon("full", -54, "%SystemRoot%\System32\imageres.dll")
    End Sub
End Class

Public Class NativeMethods
    <System.Runtime.InteropServices.DllImport("shell32.dll")>
    Public Shared Sub SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, ByVal dwItem1 As IntPtr, ByVal dwItem2 As IntPtr)
    End Sub
End Class
