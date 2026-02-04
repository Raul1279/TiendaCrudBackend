CREATE PROCEDURE spDeleteCliente(
--DECLARE 
    @clienteId INT
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int = 0
    DECLARE @message varchar(MAX)

    BEGIN TRY
        
        IF EXISTS(
            SELECT 1
            FROM tblcliente (NOLOCK)
            WHERE clienteId=@clienteId
        )
        BEGIN
            UPDATE tblcliente
            SET isDeleted = 1,
                isActive=0
            WHERE clienteId=@clienteId

            SET @result = 1
            SET @message= 'Cliente eliminado exitosamente'
        END
        
        ELSE
        BEGIN
            SET @result=0
            SET @message='Id de cliente invalido. Por favor contacte a un administrador'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;