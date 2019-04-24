Public Class frmAgregarNumero
    Private Sub frmAgregarNumero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataAccess.cargarOperadoras(cboOperadora)
    End Sub
End Class