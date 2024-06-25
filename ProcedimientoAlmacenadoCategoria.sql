delimiter $$
CREATE PROCEDURE uspListadoCategoria()
BEGIN 
SELECT descCategoria, codCategoria FROM tbcategorias
WHERE activo = 1;
END
$$