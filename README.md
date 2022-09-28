


# SistemaVentas - Transacciones - Caso Factura / Detalle Factura

## 1. Clonar Repositorio (Clone/Checkout)

https://github.com/utn-frc-pav1-3k5-2022/transacciones_factura_detalle

**1.3. Crear un nuevo branch (rama)**


## 2. Transacciones

Una transacción es una propagación de uno o más cambios en la base de datos, ya sea cuando se crea, se modifica o se elimina un registro. En la práctica suele consistir en la agrupación de consultas SQL y su ejecución como parte de una transacción.

Las transacciones siguen cuatro propiedades básicas, bajo el acrónimo ACID (Atomicity, Consistency, Isolation, Durability):

* Atomicidad: aseguran que todas las operaciones dentro de la secuencia de trabajo se completen satisfactoriamente. Si no es así, la transacción se abandona en el punto del error y las operaciones previas retroceden a su estado inicial.
* Consistencia: aseguran que la base de datos cambie estados en una transacción exitosa.
* Aislamiento: permiten que las operaciones sean aisladas y transparentes unas de otras.
* Durabilidad: aseguran que el resultado o efecto de una transacción completada permanezca en caso de error del sistema.

**2.1. Manejo de Transacciones**

Para llevar a cabo una transacción, siga estos pasos.
1. Llame al método **BeginTransaction** del objeto SqlConnection para marcar el comienzo de la transacción. El método BeginTransaction devuelve una referencia a la transacción. Esta referencia se asigna a los objetos SqlCommand que están inscritos en la transacción.
2. Asigne el objeto Transaction a la propiedad Transaction del objeto SqlCommand que se va a ejecutar. Si el comando se ejecuta en una conexión con una transacción activa y el objeto Transaction no se ha asignado a la propiedad Transaction del objeto Command, se inicia una excepción.
3. Ejecute los comandos necesarios.
4. Llame al método Commit del objeto SqlTransaction para completar la transacción, o al método Rollback para finalizarla. Si la conexión se cierra o elimina antes de que se hayan ejecutado los métodos Commit o Rollback, la transacción se revierte.

**2.2. Un ejemplo en MSSQL**

```sql
USE [SistemaVentas]

BEGIN TRY
    BEGIN TRANSACTION
		
		PRINT 'INSERT [Facturas]'

        INSERT INTO [dbo].[Facturas]
					([nro_factura],[fecha],[cliente],[tipoFactura],[subtotal],[descuento],[borrado])
		VALUES (458,GETDATE(),11,1,100,0,0)


		PRINT 'INSERT [FacturasDetalle]'

		INSERT INTO [dbo].[FacturasDetalle]
					([id_factura],[id_producto],[precio_unitario],[cantidad],[borrado])
		VALUES (@@IDENTITY,1,100, 4001,0)

		PRINT 'COMMIT'

        COMMIT
END TRY
BEGIN CATCH
	PRINT 'ROLLBACK'
    ROLLBACK
END CATCH
```

## 3. Ejecutar Script Base de datos
**3.1. Iniciar la aplicación `Sql Server Management Studio`**

Solicitará ingresar los datos de la base de datos para generar una conexión, completar los datos y hacer click en **Connect**. 
 
**3.2. Abrir archivo `SistemaVentas_Crear_BaseDatos.sql`**
 Ir a la opción `Archivo -> Abrir -> Archivo` (o combinación de teclas `Ctrl + O`) y buscar el archivo SistemaVentas_Crear_BaseDatos.sql en el disco local.
  

**3.3. Ejecutar Script** 
Para ejecutar el script hacer click sobre el botón `Ejecutar` (o usar la tecla `F5`)

## 4. Actividad
**4.1. Desarrollar Alta de Factura**
Siguiendo el diseño del formulario `frmFactura` agregar la funcionalidad necesaria para insertar una factura y su detalle en la base de datos.

**![](https://github.com/utn-frc-pav1-3k5-2020/actividad_transacciones_factura/raw/master/resources/frmFactura.png)**





## 5. Versionar en GitHub los cambios locales (add / commit / push)

> A continuación vamos a crear el **commit** y subir los cambios al servidor GitHub.

5.1. **Status**. Verificamos los cambios pendientes de versionar.

```sh
$ git status
```

5.2. **Add**. Agregamos todos los archivos nuevos no versionados.

```sh
$ git add -A
```

5.3. **Commit**. Generamos un commit con todos los cambios y agregamos un comentario.

```sh
$ git commit -a -m "Comentario"
```

5.4. **Push**. Enviamos todos los commits locales a GitHub

```sh
$ git push
```

5.5. **Status**. Verificar que no quedaron cambios pendientes de versionar

```sh
$ git status
```
