Imports System.IO

Public Class DebugConsole
    Dim exportpath As String = Application.StartupPath & "\log.txt"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DebugConsole_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = ""
        Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(exportpath)
        Dim a As String
        Do
            a = reader.ReadLine
            Label1.Text += a & vbNewLine
        Loop Until a Is Nothing
        reader.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Left = Form1.Left + Form1.Width
        Me.Top = Form1.Top
    End Sub
End Class