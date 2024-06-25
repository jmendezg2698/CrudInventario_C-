delimiter $$
CREATE PROCEDURE uspGuardarProducto(
IN nOpcion INT,
IN nCodigoProducto INT,
IN cDescripcionProducto VARCHAR(90),
IN cMarcaProducto VARCHAR(50),
IN nCodigoMedida INT,
IN nCodigoCategoria INT,
IN nStockActual DECIMAL(18,2)
)
BEGIN
IF nOpcion = 1 then 
	INSERT INTO tbproductos(
	descProducto, marcaProducto,
	codMedida, codCategoria, stockActual, activo)
	VALUES( 
	cDescripcionProducto, cMarcaProducto,
	nCodigoMedida, nCodigoCategoria, nStockActual,1);
ELSE 
	UPDATE tbproductos 
	SET 
	descProducto = cDescripcionProducto,
	marcaProducto = cMarcaProducto,
	codMedida = nCodigoMedida,
	codCategoria = nCodigoCategoria,
	stockActual = nStockActual
	WHERE codProducto = nCodigoProducto;
END if;
END
$$