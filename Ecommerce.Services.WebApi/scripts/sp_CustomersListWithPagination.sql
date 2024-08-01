SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CustomersListWithPagination]
(
    @PageNumber int, --la pagina que desea mostrar 
    @PageSize int --tamaño de la pagina
)
AS
BEGIN

    SELECT CustomerId, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax
    FROM Customers
    ORDER BY CustomerID
    OFFSET (@PageNumber-1)*@PageSize ROWS --offset especifica cuantas filas se omitiran del conjunto de resultado de la consulta
    FETCH NEXT @PageSize ROWS ONLY --fetch nos indica cuantas filas se mostraran en el resultado
END
GO
