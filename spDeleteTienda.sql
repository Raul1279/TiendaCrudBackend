CREATE PROCEDURE spDeleteTienda(
--DECLARE 
    @tiendaId INT
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int = 1--success
    DECLARE @message varchar(MAX)

    BEGIN TRY
        
        IF EXISTS(
            SELECT 1
            FROM tblTienda (NOLOCK)
            WHERE tiendaId=@tiendaId
        )
        BEGIN
            UPDATE tblTienda
            SET isDeleted = 1,
                isActive=0
            WHERE tiendaId=@tiendaId

            SET @result = 1
            SET @message= 'Tienda eliminada exitosamente'
        END
        
        ELSE
        BEGIN
            SET @result=0
            SET @message='Id de tienda invalido. Por favor contacte a un administrador'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;