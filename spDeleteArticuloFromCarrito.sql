CREATE PROCEDURE spDeleteArticuloFromCarrito(
--DECLARE 
    @clienteId INT,
    @articuloId INT
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int = 0
    DECLARE @message varchar(MAX)

    BEGIN TRY
        
        IF EXISTS(
            SELECT 1
            FROM tblClienteArticulo (NOLOCK)
            WHERE articuloId=@articuloId
            AND clienteId=@clienteId
        )
        BEGIN
            UPDATE tblClienteArticulo
            SET isDeleted = 1,
                isActive=0
            WHERE articuloId=@articuloId
            AND clienteId=@clienteId
            
            SET @result = 1
            SET @message= 'Articulo eliminado exitosamente del carrito'
        END
        
        ELSE
        BEGIN
            SET @result=0
            SET @message='No existe el articulo que se intenta eliminar. Por favor contacte a un administrador'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;