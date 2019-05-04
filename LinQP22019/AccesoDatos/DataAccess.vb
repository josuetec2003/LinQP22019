' Instancias (Objetos): Usar la palabra New
' Usar Shared para no crear objetos (no usar la palabra New)

Public Class DataAccess
    Shared ctx As New ContactosDBEntities

#Region "Formulario Contacto"
    Shared Sub cargarContactos(grid As DataGridView, Optional buscar As String = "")
        ' Consulta
        ' select * from Contacto
        ' select nombre, apellido from contacto
        Dim data ' Crear una variable sin un tipo de dato

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
        'grid.Columns(4).Visible = False


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

    Shared Function actualizarContacto(c As Contacto) As Boolean
        Try
            ' #1: Crear el objeto que sera actualizado
            Dim con = (From cont In ctx.Contacto
                       Where cont.IdContacto = c.IdContacto
                       Select cont).SingleOrDefault()

            ' #2: Actualizar los atributos del objeto del paso #1
            con.Nombre = c.Nombre
            con.Apellido = c.Apellido
            con.Direccion = c.Direccion

            ' #3: Guardar los cambios en la base de datos (sql server)
            ctx.SaveChanges()
            Return True ' Indicador de que se actualizo con exito

        Catch ex As Exception
            Return False ' Indicador de que ocurrio un error al actualizar
        End Try
    End Function

    Shared Sub cargarNumeros(grid As DataGridView, idc As Integer)
        Dim data = From nums In ctx.Numeros
                   Where nums.IdContacto = idc
                   Select nums.IdNumero, nums.Numero, nums.Operadoras.Nombre

        grid.DataSource = data.ToList()
        grid.Columns(0).Visible = False

    End Sub
#End Region

#Region "Formulario Numero"

    Shared Sub cargarOperadoras(cbo As ComboBox)
        Dim data = From o In ctx.Operadoras
                   Select o

        cbo.DataSource = data.ToList()
        cbo.DisplayMember = "Nombre"
        cbo.ValueMember = "IdOperadora"
        cbo.SelectedIndex = -1
    End Sub

    Shared Function agregarNumero(num As Numeros) As Boolean
        Try
            ctx.Numeros.Add(num)
            ctx.SaveChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Shared Sub eliminarNumero(idn As Integer)
        ' 1: Crear el objeto
        Dim num = (From n In ctx.Numeros
                   Where n.IdNumero = idn
                   Select n).SingleOrDefault()

        ' 2: Eliminar de memoria
        ctx.Numeros.Remove(num)

        ' 3: Guardar los cambios
        ctx.SaveChanges()
    End Sub

#End Region



End Class
