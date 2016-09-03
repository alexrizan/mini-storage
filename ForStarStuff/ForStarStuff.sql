--Есть таблица хранящая линии покупки: Sales: Id, ProductId, CustomerId, DateCreated.
--Мы хотим понять, 
--через какие продукты клиенты «попадают» к нам в магазин. 
---Напишите запрос, который выводит продукт и количество случаев, когда он был первой покупкой клиента.


--продукт и количество случаев, когда он был первой покупкой клиента:

Select ProductId, Count(ProductId) as [FirstOrderCount] from Sales
JOIN
(Select CustomerId, MIN(DateCreated) as FirstOrder from Sales
Group by CustomerId) FTO
ON Sales.CustomerId = FTO.CustomerId AND Sales.DateCreated = FTO.FirstOrder
GROUP BY ProductId

------------------------------------------------------------------
---Для проверки результатов---
------------------------------------------------------------------
Select CustomerId, ProductId, DateCreated from Sales
Where DateCreated in  
(Select MIN(DateCreated)  from Sales
Group by CustomerId)

Select * from Sales

Select CustomerId, MIN(DateCreated)  from Sales
Group by CustomerId
