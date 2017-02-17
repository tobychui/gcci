Imports System.IO

Public Class Form1
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    Dim gccloc As String = "C:\MinGW\bin\gcc.exe"
    Dim apppath As String = Application.StartupPath & "\"
    Dim logfile As String = Application.StartupPath & "\log.txt"
    Dim p() As Process
    Dim Errormsg As Boolean = False
    Public LaunchfromNpp As Boolean = False


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
        Dim strArg() As String
        strArg = Command().Split(" ")
        If strArg(0) = "" Then
            'There is no argument passed in
            Console.WriteLine("No starting argument, starting interface...")
        Else
            Console.WriteLine("Argument received. Compile in progress.")
            Console.WriteLine(Command())
            NewCompile(Command())
            Threading.Thread.Sleep(1500)
            Console.WriteLine("Compile Finished.")
            LaunchfromNpp = True
            Timer1.Enabled = True
            Me.Opacity = 0
            Label1.Text = "CloseMe"
        End If
    End Sub
    Private Sub Form1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop, Label3.DragDrop
        Errormsg = False
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each lpath In files
            'MsgBox(path)
            NewCompile(lpath)
            'MsgBox(lpath)
            Label3.Text = "Compile Finished, saved as: " & vbNewLine & apppath & Path.GetFileName(lpath).Replace(".c", ".exe")
        Next
        If Errormsg = True Then
            Label3.Text += vbNewLine & "Seems there is some error during compilation :("
        End If
    End Sub

    Private Sub Form1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter, Label3.DragEnter
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
        'MsgBox("""" & filedir & """" & " " & "-o" & " " & """" & apppath & Path.GetFileName(filedir).Replace(".c", ".exe") & """" & " > " & """" & logfile & """" & "2>&1")
        'TextBox1.Text = """" & filedir & """" & " " & "-o" & " " & """" & apppath & Path.GetFileName(filedir).Replace(".c", ".exe") & """" & " > " & """" & logfile & """" & "2>&1"
        Dim compiler As New ProcessStartInfo
        compiler.FileName = gccloc
        'compiler.Arguments = """" & filedir & """" & " " & "-o" & " " & """" & apppath & Path.GetFileName(filedir).Replace(".c", ".exe") & """"
        compiler.Arguments = """" & filedir & """" & " " & "-o" & " " & """" & apppath & Path.GetFileName(filedir).Replace(".c", ".exe") & """" & " > " & """" & logfile & """" & "2>&1"
        compiler.UseShellExecute = True
        'compiler.RedirectStandardOutput = True
        compiler.WindowStyle = ProcessWindowStyle.Normal
        Dim proc As New Process()
        proc.StartInfo = compiler
        proc.Start()
        'MsgBox(sOutput)
    End Sub

    Private Sub NewCompile(filedir As String)
        'Remove the old log.txt file before it can read
        Dim pnumber As Integer = GetProcessNumber()
        Dim exportpath As String = Application.StartupPath & "\log.txt"
        If My.Computer.FileSystem.FileExists(exportpath) Then
            My.Computer.FileSystem.DeleteFile(exportpath)
        End If
        Shell("cmd.exe /c" & gccloc & " " & """" & filedir & """" & " " & "-o" & " " & """" & apppath & Path.GetFileName(filedir).Replace(".c", ".exe") & """" & " > " & """" & logfile & """" & "2>&1")
        While GetProcessNumber() <> pnumber
            'While the process is not yet ended, keep waiting
            Threading.Thread.Sleep(300)
            Application.DoEvents()
        End While
        If My.Computer.FileSystem.FileExists(exportpath) Then
            If (File.ReadAllText(exportpath).Length <> 0) Then
                'There is error in the compilation process
                Dim Dc As New DebugConsole
                Dc.Show()
                Errormsg = True
            Else

                'Everthing works fine
                My.Computer.FileSystem.DeleteFile(exportpath)
            End If
        End If

    End Sub
    Private Function GetProcessNumber()
        p = Process.GetProcessesByName("cmd")
        Return p.Count
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Label1.Text = "CloseMe" Then
            Me.Close()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
