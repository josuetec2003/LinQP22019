Public Class Form1


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataAccess.cargarContactos(dgvContactos)
    End Sub

    Private Sub txtBuscar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar = ChrW(13) Then
            DataAccess.cargarContactos(dgvContactos, txtBuscar.Text)
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim c As New Contacto With
        {
            .Nombre = txtNombre.Text,
            .Apellido = txtApellido.Text,
            .Direccion = txtDireccion.Text
        }

        DataAccess.agregarContacto(c)
        DataAccess.cargarContactos(dgvContactos)

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim id As Integer = dgvContactos.CurrentRow.Cells(0).Value
        If DataAccess.eliminarContacto(id) Then
            MsgBox("Contacto eliminado")
        Else
            MsgBox("El contacto tiene numeros registrados, elimine los numeros primero")
        End If
        DataAccess.cargarContactos(dgvContactos)
    End Sub
End Class
