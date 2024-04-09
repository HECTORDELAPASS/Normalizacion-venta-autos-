Imports System.Data.SqlClient
Public Class Infor
	Dim query, s As String
	Dim temporal As Integer
	Private Sub Infor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		cboventa.DropDownStyle = ComboBoxStyle.DropDownList
		Label4.Visible = False
		cboventa.Visible = False
		pic1.Visible = False
		lab1.Visible = False

		s = Form1.rows
		query = "select carro.codigovehiculo, count(carro.codigovehiculo)
				from venta
				inner join carro on venta.idcarro = carro.id
				where carro.codigovehiculo ='" & s & "'
				group by carro.codigovehiculo"

		Dim dt As DataTable = Connection.SelectQuery(query)
		temporal = dt.Rows(0)(1)

		Try
			llenado()
			bloqueo()
		Catch ex As Exception

		End Try

	End Sub
	Private Sub bloqueo()
		idtxt.ReadOnly = True
		fechventxt.ReadOnly = True
		mmrtxt.ReadOnly = True
		Ventatxt.ReadOnly = True
		vendedortxt.ReadOnly = True
		modelotxt.ReadOnly = True
		yeartxt.ReadOnly = True
		conditxt.ReadOnly = True
		kmtxt.ReadOnly = True
		agentxt.ReadOnly = True
		Variatxt.ReadOnly = True
		txtcarroceria.ReadOnly = True
		trastxt.ReadOnly = True
		colortxt.ReadOnly = True
		inttxt.ReadOnly = True
		Edtxt.ReadOnly = True
		vintxt.ReadOnly = True

	End Sub
	Private Sub cboventa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboventa.SelectedIndexChanged
		llenado()
	End Sub

	Private Sub llenado()
		If cboventa.SelectedItem Is Nothing Then
			query = "select * from InfoCarro WHERE [Codigovehiculo] = '" & s & " '"
		Else
			query = "Select * From InfoCarro Where [id] = '" & (cboventa.SelectedItem) & "'"
		End If
		Dim dt As DataTable = Connection.SelectQuery(query)

		Dim fila As Integer = dt.Rows.Count
		Dim columna As Integer = dt.Columns.Count

		Dim matriz(fila - 1, columna - 1) As Object

		For i As Integer = 0 To fila - 1
			For j As Integer = 0 To columna - 1
				matriz(i, j) = dt.Rows(i)(j)
			Next
		Next


		If temporal > 1 And cboventa.SelectedItem Is Nothing Then
			MsgBox("Existe mas de una venta, para mas informacion " & vbCrLf & "ir a esquina superior derecha")
			Label4.Visible = True
			cboventa.Visible = True
			pic1.Visible = True
			lab1.Visible = True

			For h = 0 To temporal - 1
				cboventa.Items.Add(matriz(h, 0)).ToString()
			Next

		End If

		idtxt.Text = matriz(0, 0)
		fechventxt.Text = matriz(0, 1)
		mmrtxt.Text = matriz(0, 2)
		Ventatxt.Text = matriz(0, 3)
		vendedortxt.Text = matriz(0, 4)
		modelotxt.Text = matriz(0, 5)
		yeartxt.Text = matriz(0, 6)
		conditxt.Text = matriz(0, 7)
		kmtxt.Text = matriz(0, 8)
		agentxt.Text = matriz(0, 9)
		Variatxt.Text = matriz(0, 10)
		txtcarroceria.Text = matriz(0, 11)
		trastxt.Text = matriz(0, 12)
		colortxt.Text = matriz(0, 13)
		inttxt.Text = matriz(0, 14)
		Edtxt.Text = matriz(0, 15)
		vintxt.Text = matriz(0, 16)

		Dim url As String = "https://logo.clearbit.com/" & agentxt.Text & ".com"

		If LogoExists(url) Then
			piclogo.Load(url)
		End If

	End Sub
	Private Function LogoExists(url As String) As Boolean
		Dim request As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(url), System.Net.HttpWebRequest)
		request.Method = "HEAD"
		Try
			Using response As System.Net.HttpWebResponse = CType(request.GetResponse(), System.Net.HttpWebResponse)
				Return (response.StatusCode = System.Net.HttpStatusCode.OK)
			End Using
		Catch ex As System.Net.WebException
			Return False
		End Try
	End Function
End Class