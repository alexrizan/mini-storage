--���� ������� �������� ����� �������: Sales: Id, ProductId, CustomerId, DateCreated.
--�� ����� ������, 
--����� ����� �������� ������� ��������� � ��� � �������. 
---�������� ������, ������� ������� ������� � ���������� �������, ����� �� ��� ������ �������� �������.


--������� � ���������� �������, ����� �� ��� ������ �������� �������:
Select ProductId, Count(ProductId) from Sales
Where DateCreated in  
(Select MIN(DateCreated) from Sales
Group by CustomerId)
Group by ProductId

------------------------------------------------------------------
---��� �������� �����������---
------------------------------------------------------------------
Select CustomerId, ProductId, DateCreated from Sales
Where DateCreated in  
(Select MIN(DateCreated)  from Sales
Group by CustomerId)

Select * from Sales

Select CustomerId, MIN(DateCreated)  from Sales
Group by CustomerId
