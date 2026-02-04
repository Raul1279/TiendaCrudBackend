CREATE PROCEDURE spUpdateArticulo(
--DECLARE 
    @articuloId INT,
    @codigo VARCHAR(50),
    @descripcion VARCHAR(500),
    @precio VARCHAR(50)= '0',
    @imagenUrl VARCHAR(MAX),
    @stock INT,
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
        
       IF NOT EXISTS(
            SELECT 1
            FROM tblArticulo (NOLOCK)
            WHERE articuloId=@articuloId
            AND isDeleted=0
        )
        BEGIN
            SET @result=0
            SET @message= 'El articulo que intenta actualizar no existe. Por favor contacte a un administrador.'
        END
  
        ELSE IF(ISNULL(@codigo,'') = '' OR ISNULL(@descripcion,'') = '' OR ISNULL(@imagenUrl,'') = '')
        BEGIN
            SET @result=0
            SET @message='Error. Todos los campos son mandatorios. Por favor verifique que no haya espacios vacios.'
        END

        ELSE
        BEGIN 
            
            UPDATE tblArticulo
            SET codigo=@codigo,
                descripcion=@descripcion,
                precio=@precio,
                imagenUrl=@imagenUrl,
                stock=@stock,
                tiendaId=@tiendaId
            WHERE articuloId=@articuloId            

            SET @result=1
            SET @message = 'Articulo actualizado exitosamente'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;