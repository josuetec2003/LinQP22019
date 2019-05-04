Public Class frmAgregarNumero
    Public IdContacto As Integer

    Private Sub frmAgregarNumero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataAccess.cargarOperadoras(cboOperadora)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        ' Metodo 1
        'Dim n As New Numeros

        'n.Numero = txtNumero.Text
        'n.IdOperadora = cboOperadora.SelectedValue
        'n.IdContacto = IdContacto

        ' Metodo 2
        'With n
        '    .Numero = txtNumero.Text
        '    .IdOperadora = cboOperadora.SelectedValue
        '    .IdContacto = IdContacto
        'End With

        ' Metodo 3
        Dim num As New Numeros With
        {
            .Numero = txtNumero.Text,
            .IdOperadora = cboOperadora.SelectedValue,
            .IdContacto = IdContacto
        }

        If DataAccess.agregarNumero(num) Then
            DataAccess.cargarNumeros(Form1.dgvNumeros, IdContacto)
            MsgBox("El numero ha sido agregado")
        Else
            MsgBox("Error al agregar el numero")
        End If


    End Sub
End Class