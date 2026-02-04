CREATE PROCEDURE spCreateCliente(
--DECLARE 
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
        
        IF EXISTS(
            SELECT 1
            FROM tblcliente (NOLOCK)
            WHERE email=@email
            AND isDeleted=0
        )
        BEGIN
            SET @result=0
            SET @message= CONCAT('Cliente con el email (',@email,') ya existe en la base de datos. Por favor verifique o contacte a un admisnitrador')
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
            
            INSERT INTO tblCliente(
                nombre,
                apellidos,
                direccion,
                email,
                isActive,
                isDeleted,
                passwordHash
            )
            VALUES(@nombre,@apellidos,@direccion,@email,1,0,@passwordHash)

            SET @result=1
            SET @message = 'Cliente creado exitosamente.'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;