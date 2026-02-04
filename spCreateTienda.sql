CREATE PROCEDURE spCreateTienda(
--DECLARE 
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
        
        IF EXISTS(
            SELECT 1
            FROM tblTienda (NOLOCK)
            WHERE sucursal=@sucursal
            AND isDeleted=0
        )
        BEGIN
            SET @result=0
            SET @message= CONCAT('La sucursal (',@sucursal,') ya existe en la base de datos. Por favor verifique o contacte a un admisnitrador')
        END
        
        ELSE IF(ISNULL(@sucursal,'') = '' OR ISNULL(@direccion,'') = '')
        BEGIN
            SET @result=0
            SET @message='Error. El nombre de la sucursal o la direccion estan vacios. Por favor verifique.'
        END

        ELSE
        BEGIN 
            
            INSERT INTO tblTienda(
                sucursal,
                direccion,
                isActive,
                isDeleted
            )
            VALUES(@sucursal,@direccion,1,0)

            SET @result=1
            SET @message = 'Tienda creada exitosamente'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;