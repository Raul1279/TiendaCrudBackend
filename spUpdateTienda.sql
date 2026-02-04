CREATE PROCEDURE spUpdateTienda(
--DECLARE 
    @tiendaId INT,
    @sucursal VARCHAR(100),
    @direccion VARCHAR(500)
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int
    DECLARE @message varchar(MAX)

    SET @sucursal=TRIM(@sucursal)
    SET @direccion=TRIM(@direccion)

    BEGIN TRY
        
        IF NOT EXISTS(
            SELECT 1
            FROM tblTienda (NOLOCK)
            WHERE tiendaId=@tiendaId
            AND isDeleted=0
        )
        BEGIN
            SET @result=0
            SET @message= 'La tienda que intenta actualizar no existe. Por favor contacte a un administrador.'
        END
        
        ELSE IF(ISNULL(@sucursal,'') = '' OR ISNULL(@direccion,'') = '')
        BEGIN
            SET @result=0
            SET @message='Error. El nombre de la sucursal o la direccion estan vacios. Por favor verifique.'
        END

        ELSE
        BEGIN 
            
            UPDATE tblTienda
            SET sucursal = @sucursal,
                direccion = @direccion
            WHERE tiendaId=@tiendaId            

            SET @result=1
            SET @message = 'Tienda actualizada exitosamente'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;