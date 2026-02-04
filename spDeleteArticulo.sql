CREATE PROCEDURE spDeleteArticulo(
--DECLARE 
    @articuloId INT
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int = 0
    DECLARE @message varchar(MAX)

    BEGIN TRY
        
        IF EXISTS(
            SELECT 1
            FROM tblArticulo (NOLOCK)
            WHERE articuloId=@articuloId
        )
        BEGIN
            UPDATE tblArticulo
            SET isDeleted = 1,
                isActive=0
            WHERE articuloId=@articuloId

            SET @result = 1
            SET @message= 'Articulo eliminado exitosamente'
        END
        
        ELSE
        BEGIN
            SET @result=0
            SET @message='Id de articulo invalido. Por favor contacte a un administrador'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;