CREATE PROCEDURE spPagarCarritoByClienteId
    @clienteId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @result INT = 0
    DECLARE @message VARCHAR(MAX)

    BEGIN TRY

    IF NOT EXISTS(
            SELECT 1
            FROM tblClienteArticulo (NOLOCK)
            WHERE clienteId=@clienteId
            and isDeleted=0    
    )
    BEGIN
        SET @result=0
        SET @message='No hay nada para pagar en el carrito de este cliente. Por favor verifique'
    END

    ELSE
    BEGIN
        UPDATE a
        SET a.stock = a.stock - ca.cantidad
        FROM tblArticulo a
        INNER JOIN tblClienteArticulo ca
            ON a.articuloId = ca.articuloId
        WHERE ca.clienteId = @clienteId
          AND ca.isDeleted = 0
          AND ca.isActive = 1

        UPDATE tblClienteArticulo
        SET isActive = 0,
            isDeleted = 1
        WHERE clienteId = @clienteId

        SET @result = 1
        SET @message = 'Pago realizado correctamente'
    END
     
    END TRY
    BEGIN CATCH
        SET @result = 0
        SET @message=CONCAT('Something went wrong: ', ERROR_MESSAGE(),' **Error Line** ',ERROR_LINE())
    END CATCH

    SELECT @result AS Result, @message AS Message
END
