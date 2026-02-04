CREATE PROCEDURE spAddArticuloToCarrito(
--DECLARE 
    @articuloId INT,
    @clienteId INT,
    @cantidad INT
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result int
    DECLARE @message varchar(MAX)

    BEGIN TRY
        
        IF EXISTS(
            SELECT 1
            FROM tblClienteArticulo (NOLOCK)
            WHERE clienteId=@clienteId
            AND articuloId=@articuloId
            AND isDeleted=0
        )
        BEGIN

            UPDATE tblClienteArticulo
            SET cantidad = cantidad + @cantidad
            WHERE clienteId=@clienteId
            AND articuloId=@articuloId

            SET @result=1
            SET @message= 'Cantidad agregada al carrito.'
        END
        
        ELSE
        BEGIN 
            
            INSERT INTO tblClienteArticulo (
                clienteId ,
                articuloId ,
                cantidad,
                fecha ,
                isActive ,
                isDeleted 
            )
            SELECT @clienteId,@articuloId,@cantidad,GETUTCDATE(),1,0

            SET @result=1
            SET @message='Articulo agregado al carrito.'
        END

    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result [Result],
    @message [Message]
END;