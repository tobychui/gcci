Imports System.IO

Public Class Form1
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    Dim gccloc As String = "C:\MinGW\bin\gcc.exe"
    Dim apppath As String = Application.StartupPath & "\"


    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseUp
        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseMove
        If IsFormBeingDragged Then
            Dim temp As Point = New Point()
            temp.X = Me.Location.X + (e.X - MouseDownX)
            temp.Y = Me.Location.Y + (e.Y - MouseDownY)
            Me.Location = temp
            temp = Nothing
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AllowDrop = True
    End Sub
    Private Sub Form1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each lpath In files
            'MsgBox(path)
            Compile(lpath)
            'MsgBox(lpath)
            Label3.Text = "Compile Finished, saved as: " & vbNewLine & apppath & Path.GetFileName(lpath).Replace(".c", ".exe")
        Next

    End Sub

    Private Sub Form1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim strm As System.IO.Stream
        OpenFileDialog1.InitialDirectory = "C:\MinGW\bin\"
        OpenFileDialog1.Filter = "EXE files (*.exe)|*.exe"
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strm = OpenFileDialog1.OpenFile()
            gccloc = OpenFileDialog1.FileName.ToString()
        End If
    End Sub

    Private Sub Compile(filedir As String)
        'MsgBox("""" & filedir & """" & " " & "-o" & " " & """" & apppath & Path.GetFileName(filedir).Replace(".c", ".exe") & """")
        Dim compiler As New ProcessStartInfo
        compiler.FileName = gccloc
        compiler.Arguments = """" & filedir & """" & " " & "-o" & " " & """" & apppath & Path.GetFileName(filedir).Replace(".c", ".exe") & """"
        compiler.UseShellExecute = True
        compiler.WindowStyle = ProcessWindowStyle.Normal
        Dim proc As Process = Process.Start(compiler)
    End Sub
End Class
