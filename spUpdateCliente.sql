CREATE PROCEDURE spUpdateCliente(
--DECLARE 
    @clienteId INT,
    @nombre VARCHAR(100),
    @apellidos VARCHAR(100),
    @direccion VARCHAR(500),
    @email VARCHAR(100),
    @passwordHash VARCHAR(200)
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int
    DECLARE @message varchar(MAX)

    SET @nombre=TRIM(@nombre)
    SET @apellidos=TRIM(@apellidos)
    SET @direccion=TRIM(@direccion)
    SET @email=TRIM(@email)
    SET @passwordHash=TRIM(@passwordHash)

    BEGIN TRY
        
        IF NOT EXISTS(
            SELECT 1
            FROM tblcliente (NOLOCK)
            WHERE clienteId=@clienteId
            AND isDeleted=0
        )
        BEGIN
            SET @result=0
            SET @message= 'El cliente que intenta actualizar no existe. Por favor contacte a un administrador.'
        END
        
       ELSE IF(ISNULL(@nombre,'') = '' OR ISNULL(@apellidos,'') = ''
            OR ISNULL(@direccion,'') = '' OR ISNULL(@email,'') = '' 
            OR ISNULL(@passwordHash,'') = '')
        BEGIN
            SET @result=0
            SET @message='Error. Nombre, Apellidos, Direccion, Email, y Password son mandatorios. Por favor verifique sus datos.'
        END

        ELSE
        BEGIN 
            
            UPDATE tblCliente
            SET nombre=@nombre,
                apellidos=@apellidos,
                direccion=@direccion,
                email =@email,
                passwordHash = @passwordHash
            WHERE clienteId=@clienteId            

            SET @result=1
            SET @message = 'Cliente actualizado exitosamente'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;