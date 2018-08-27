Imports Nec.Nebula

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim service = NbService.GetInstance()

        service.TenantId = ""
        service.AppId = ""
        service.AppKey = ""
        service.EndpointUrl = "https://baas.example.com/api"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DumpBucket()
    End Sub

    ''' <summary>
    ''' バケットの内容(の一部)を表示する。
    ''' 出力結果は「出力」ウィンドウに表示される
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DumpBucket()
        Dim bucket = New NbObjectBucket(Of NbObject)("TodoTutorial1")

        Dim query = New NbQuery()
        Dim objects = Await bucket.QueryAsync(query)

        For Each obj As NbObject In objects
            Dim description = obj.Get(Of String)("description")
            Debug.WriteLine(description)
        Next
    End Sub
End Class
