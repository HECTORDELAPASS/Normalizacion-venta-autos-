Imports System.Data.SqlClient
Public Class Connection
    Shared cnx As New SqlConnection

    Private Shared Sub connect()
        Try
            cnx.ConnectionString = "Data Source=DESKTOP-3AGDMLR\SQLEXPRESS; Initial Catalog=ventasautos;Integrated Security=True"
            cnx.Open()
        Catch ex As Exception
            MsgBox("Error al conectar a la base de datos: " & ex.Message)
        End Try
    End Sub

    Private Shared Sub disconect()
        Try
            If cnx.State = ConnectionState.Open Then
                cnx.Close()
            End If
        Catch ex As Exception
            MsgBox("Error al desconectar la base de datos" & ex.Message)
        End Try
    End Sub


    Public Shared Function SelectQuery(ByVal query As String) As DataTable
        Dim dt As New DataTable
        Try
            connect()
            Dim cmd As New SqlCommand(query, cnx)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("Error al ejecutar la consulta: " & ex.Message)
        Finally
            disconect()
        End Try
        Return dt
    End Function
End Class
