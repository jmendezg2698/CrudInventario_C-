delimiter $$
CREATE PROCEDURE uspEliminarProducto(IN nCodigoProductouspListadoProductouspListadoProducto int)
BEGIN
	UPDATE tbproductos SET activo = 0 
	WHERE codProducto = nCodigoProducto;
END
$$