CREATE PROCEDURE spGetTiendaById
    @tiendaId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @message varchar(MAX)

    BEGIN TRY
        SELECT TOP 1  
            tiendaId,
            sucursal,
            direccion,
            isActive,
            isDeleted
        FROM tblTienda (NOLOCK)
        WHERE tiendaId=@tiendaId
    END TRY
    BEGIN CATCH
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
        RAISERROR(@message,16,1);
    END CATCH
END
