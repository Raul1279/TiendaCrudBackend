CREATE PROCEDURE spCreateArticulo(
--DECLARE 
    @codigo VARCHAR(50),
    @descripcion VARCHAR(500),
    @precio VARCHAR(50)= '0',
    @imagenUrl VARCHAR(MAX),
    @stock INT = 0,
    @tiendaId INT
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int
    DECLARE @message varchar(MAX)

    SET @codigo=TRIM(@codigo)
    SET @descripcion=TRIM(@descripcion)
    SET @imagenUrl=TRIM(@imagenUrl)

    BEGIN TRY
        
        IF EXISTS(
            SELECT 1
            FROM tblArticulo (NOLOCK)
            WHERE codigo=@codigo
            AND isDeleted=0
        )
        BEGIN
            SET @result=0
            SET @message= CONCAT('El articulo con codigo (',@codigo,') ya existe en la base de datos. Por favor verifique o contacte a un admisnitrador')
        END
        
        ELSE IF(ISNULL(@codigo,'') = '' OR ISNULL(@descripcion,'') = '' OR ISNULL(@imagenUrl,'') = '')
        BEGIN
            SET @result=0
            SET @message='Error. Todos los campos son mandatorios. Por favor verifique que no haya espacios vacios.'
        END

        ELSE
        BEGIN 
            
            INSERT INTO tblArticulo(
                codigo,
                descripcion,
                precio,
                imagenUrl,
                stock,
                tiendaId,
                isActive,
                isDeleted
            )
            VALUES(@codigo,@descripcion,@precio,@imagenUrl,@stock,@tiendaId,1,0)

            SET @result=1
            SET @message = 'Articulo creado exitosamente'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;