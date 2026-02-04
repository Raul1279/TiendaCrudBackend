CREATE PROCEDURE spGetClienteById
    @clienteId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @message varchar(MAX)

    BEGIN TRY
          SELECT TOP 1
            clienteId,
            nombre,
            apellidos,
            direccion,
            email,
            isActive,
            isDeleted,
            passwordHash
        FROM tblCliente (NOLOCK)
        WHERE clienteId=@clienteId
    END TRY
    BEGIN CATCH
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
        RAISERROR(@message,16,1);
    END CATCH
END
