' Instancias (Objetos): Usar la palabra New
' Usar Shared para no crear objetos (no usar la palabra New)

Public Class DataAccess
    Shared ctx As New ContactosDBEntities

    Shared Sub cargarContactos(grid As DataGridView, Optional buscar As String = "")
        ' Consulta
        ' select * from Contacto
        ' select nombre, apellido from contacto
        Dim data

        If buscar = "" Then
            data = (From c In ctx.Contacto
                    Select c).ToList
        Else
            data = (From c In ctx.Contacto
                    Where c.Nombre.StartsWith(buscar) OrElse c.Apellido.StartsWith(buscar)
                    Select c).ToList
        End If

        grid.DataSource = data
        grid.Columns(0).Visible = False
        grid.Columns(4).Visible = False


        'Dim data = From c In ctx.Contacto
        '           Select c.Nombre, c.Apellido
    End Sub

    Shared Sub agregarContacto(c As Contacto)
        ctx.Contacto.Add(c)
        ctx.SaveChanges()
    End Sub

    Shared Function eliminarContacto(id As Integer) As Boolean
        ' Primero se crea la entidad contacto usando el id y SingleOrDefault
        Dim con = (From c In ctx.Contacto
                   Where c.IdContacto = id
                   Select c).SingleOrDefault()

        Try
            ' Eliminar el contacto creado en el paso anterior
            ctx.Contacto.Remove(con)
            ctx.SaveChanges()

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class
