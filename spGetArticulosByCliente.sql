CREATE PROCEDURE spGetArticulosByCliente
    @clienteId INT
AS
BEGIN
    SELECT 
        ca.articuloId,
        a.codigo,
        a.descripcion,
        a.precio,
        a.imagenUrl,
        ca.cantidad
    FROM tblClienteArticulo ca
    INNER JOIN tblArticulo a ON a.articuloId = ca.articuloId
    WHERE ca.clienteId = @clienteId
      AND ca.isDeleted = 0
      AND ca.isActive = 1
END
