CREATE PROCEDURE spGetArticuloById
    @articuloId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @message varchar(MAX)

    BEGIN TRY
        SELECT TOP 1
            articuloId,
            codigo,
            descripcion,
            precio,
            imagenUrl,
            stock,
            isActive,
            isDeleted
        FROM tblarticulo (NOLOCK)
        WHERE articuloId=@articuloId
    END TRY
    BEGIN CATCH
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
        RAISERROR(@message,16,1);
    END CATCH
END
