CREATE PROCEDURE spGetAllClientes
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @message varchar(MAX)

    BEGIN TRY
        SELECT
            clienteId,
            nombre,
            apellidos,
            direccion,
            email,
            isActive,
            isDeleted,
            passwordHash
        FROM tblCliente (NOLOCK)
        WHERE isDeleted=0
    END TRY
    BEGIN CATCH
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
        RAISERROR(@message,16,1);
    END CATCH
END
