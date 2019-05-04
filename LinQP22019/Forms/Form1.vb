Imports System.Net
Imports System.Net.Mail

Public Class Form1
    ' Para almacenar el id del contacto y que sirva en los todos los bloques de
    ' codigo del Form1
    Dim id_contacto As Integer

    ' True: Insert
    ' False: Update
    Dim esNuevo As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataAccess.cargarContactos(dgvContactos)
        esNuevo = True ' Para insertar
    End Sub

    Private Sub txtBuscar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar = ChrW(13) Then
            DataAccess.cargarContactos(dgvContactos, txtBuscar.Text)
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If esNuevo Then ' Insert
            Dim c As New Contacto With
            {
                .Nombre = txtNombre.Text,
                .Apellido = txtApellido.Text,
                .Direccion = txtDireccion.Text
            }

            DataAccess.agregarContacto(c)
            DataAccess.cargarContactos(dgvContactos)

            txtNombre.Text = ""
            txtApellido.Text = ""
            txtDireccion.Text = ""
        Else    ' Update
            Dim c As New Contacto With
            {
                .Nombre = txtNombre.Text,
                .Apellido = txtApellido.Text,
                .Direccion = txtDireccion.Text,
                .IdContacto = id_contacto
            }

            If DataAccess.actualizarContacto(c) Then
                DataAccess.cargarContactos(dgvContactos)
                MsgBox("El contacto ha sido actualizado")
            Else
                MsgBox("Ocurrio un error al actualizar el contacto")
            End If






        End If



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

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        ' Trasladar los datos de la fila seleccionada a las cajas de texto
        id_contacto = dgvContactos.CurrentRow.Cells(0).Value
        txtNombre.Text = dgvContactos.CurrentRow.Cells(1).Value
        txtApellido.Text = dgvContactos.CurrentRow.Cells(2).Value
        txtDireccion.Text = dgvContactos.CurrentRow.Cells(3).Value
        esNuevo = False
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        esNuevo = True
        txtNombre.Text = ""
        txtApellido.Text = ""
        txtDireccion.Text = ""
        txtNombre.Focus()
    End Sub

    Private Sub dgvContactos_Click(sender As Object, e As EventArgs) Handles dgvContactos.Click
        Dim idc As Integer = dgvContactos.CurrentRow.Cells(0).Value

        DataAccess.cargarNumeros(dgvNumeros, idc)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim nombre As String = dgvContactos.CurrentRow.Cells(1).Value
        Dim apellido As String = dgvContactos.CurrentRow.Cells(2).Value
        Dim ID As Integer = dgvContactos.CurrentRow.Cells(0).Value

        Dim nombre_completo As String = String.Format("{0} {1}", nombre, apellido)

        With frmAgregarNumero
            .lblContacto.Text = "Agregar numero a " + nombre_completo
            .IdContacto = ID
            .ShowDialog()
        End With
    End Sub

    Private Sub btnEliminarNumero_Click(sender As Object, e As EventArgs) Handles btnEliminarNumero.Click
        ' Confirmar si se eliminar el numero

        ' Tomar el ID del numero y del contacto
        Dim ID As Integer = dgvNumeros.CurrentRow.Cells(0).Value
        Dim idc As Integer = dgvContactos.CurrentRow.Cells(0).Value

        DataAccess.eliminarNumero(ID)
        DataAccess.cargarNumeros(dgvNumeros, idc)
    End Sub

    Private Sub btnEnviarCorreo_Click(sender As Object, e As EventArgs) Handles btnEnviarCorreo.Click
        Dim para As String = dgvContactos.CurrentRow.Cells(4).Value

        If para = "" Then
            MsgBox("Este contacto no tiene direccion de correo")
        Else
            Dim cuerpo As String = InputBox("Escriba el texto del correo")

            If cuerpo <> "" Then
                Using mensaje As New MailMessage("programacion.uph@gmail.com", para, "Correo de Programacion 4", cuerpo)
                    mensaje.BodyEncoding = System.Text.Encoding.UTF8

                    Using enviador As New SmtpClient("smtp.gmail.com", 587)
                        enviador.Credentials = New NetworkCredential("programacion.uph@gmail.com", "123abcd.")
                        enviador.EnableSsl = True

                        Try
                            enviador.Send(mensaje)
                            MsgBox("El correo ha sido enviado!")
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    End Using
                End Using
            End If


        End If

    End Sub
End Class
