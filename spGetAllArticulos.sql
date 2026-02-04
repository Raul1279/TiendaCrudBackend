CREATE PROCEDURE spGetAllArticulos
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @message varchar(MAX)

    BEGIN TRY
        SELECT
            a.articuloId,
            a.tiendaId,
            t.sucursal,
            a.codigo,
            a.descripcion,
            a.precio,
            a.imagenUrl,
            a.stock,
            a.isActive,
            a.isDeleted
        FROM tblarticulo (NOLOCK) a
        INNER JOIN tblTienda t (NOLOCK) on t.tiendaId=a.tiendaid
        WHERE a.isDeleted=0
    END TRY
    BEGIN CATCH
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
        RAISERROR(@message,16,1);
    END CATCH
END
