delimiter $$
CREATE PROCEDURE uspListadoProducto(IN texto VARCHAR(80))
BEGIN
SELECT a.codProducto,
a.descProducto,
a.marcaProducto,
b.descMedidas,
c.descCategoria,
a.stockActual,
a.codMedida,
a.codCategoria
FROM 	tbproductos a 
INNER JOIN tbmedidas b ON a.codMedida = b.codMedidas
INNER JOIN tbcategorias c ON a.codCategoria = c.codCategoria
WHERE a.activo = 1 AND
CONCAT(CAST(a.codProducto AS CHAR),
TRIM(a.descProducto),
TRIM(a.marcaProducto))LIKE CONCAT('%',UPPER(TRIM(texto)),'%');
END;
$$