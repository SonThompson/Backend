# DataBaseShop
Backend for the store's database
# Content
This project represents my training in creating and working with a database.  
__Implemented:__
```
1. CRUD for all tables in database
2. Calling functions and procedures in the database
3. Execution of a query to the database
4. Logging
```
__Technologies used:__
```
Backend: C#.NET 6.0 + EF Core
Database: ProstgreSQL
```
__Database table connection diagram__  
![dbdiagram](https://github.com/SonThompson/Database-Shop/blob/main/Images/DBDiagram.png)  
  
__Table Contents__

_Categories(Category)_  
The table of product categories (types) consists of two columns: a category identifier column and a category name column.  
_Example table contents:_
category_id | category_name
------------|-----------------------
1           | Компьютерная техника
2           | Офис и канцелярия
3           | Мелкая бытовая техника  

_Manufacturers of goods (Manufacture)_  
The table contains information about the product manufacturer identifier and its name.  
_Example table contents:_  
manufacturer_id | manufacturer_name
----------------|------------------
1               | Calve
2               | TESCOMA
3               | Haier
4               | Nescafe
5               | Be quiet

_Products (Product)_
The table contains the product identifier, its name, a link to the supplier identifier (foreign key), and a link to the category identifier (foreign key) to which the product belongs.  
_Example table contents:_  
product_id | product_name                        | manufacturer_id | category_id
-----------|-------------------------------------|-----------------|------------
1          | Кухонный комбайн KitchenAid 5KSM156 | 71              | 3
2          | Видеокарта Asus GeForce GT 1030     | 29              | 1
3          | Ноутбук HP ENVY 13-ad000            | 486             | 1
4          | Фен Dewal 03-401                    | 124             | 3
5          | Кофеварка Gastrorag CM-717          | 225             | 3

_Changes in prices for goods (PriceChange)_  
Product prices are subject to change. To account for changes in prices for goods, the price_change table is used.
The table contains a link to the product (foreign key), the date the product price changed and the new price. Thus, in order to find 
out the price of a product on a given date, it is necessary to find the nearest date (in the past) of changes in its price.  
_Example table contents:_  
price_change_id |product_id | date_price_change | new_price
----------------|-----------|--------------------------|----------
1               |1          | 2023-11-14T15:53:26.757Z | 58399
2               |3          | 2018-10-14T25:43:22.621Z | 5717.8
3               |2          | 2023-11-14T05:19:14.701Z | 54890
4               |4          | 2023-11-14T16:24:17.532Z | 2632.3
5               |5          | 2023-11-14T49:07:19.421Z | 32854.8

_Branches (Store)_  
The table contains branch identifiers and their names.  
_Example table contents:_  
store_id | store_name
---------|-----------
1        | Филиал №1
2        | Филиал №2
3        | Филиал №3
4        | Филиал №4
  
_Supplies (Delivery)_  
The table contains the identifiers of the goods delivered, the branch where the goods were delivered, the delivery date and the quantity of goods delivered.  
_Example table contents:_  
delivery_id |product_id | store_id | delivery_date | product_count
------------|-----------|----------|---------------|--------------
1           |0          | 0        | 1546300800    | 5
2           |0          | 0        | 1556125138    | 9
3           |1          | 0        | 1546300800    | 5
4           |1          | 0        | 1575852670    | 9
5           |2          | 3        | 1546300800    | 5

_Clients (Customer)_
The table contains identifiers and names of clients (buyers).  
_Example table contents:_  
customer_id | customer_fname    | customer_lname
------------|-------------------|---------------
1           | Митофан Демидович | Дорофеев
2           | Софрон            | Панов
3           | Демьян            | Мартынов
4           | Гостомысл         | Белоусов  
To simplify, the first and last names of the client (buyer) are stored in one column  

_Purchases (Purchase)_
The purchase table can be represented as a table of invoices for goods purchased as part of one purchase (the purchase is characterized by a unique combination: date, buyer, branch).
The table contains the purchase ID, the ID of the customer who made the purchase, the ID of the branch where the purchase was made, and the date of purchase.  
_Example table contents:_  
purchase_id | customer_id | store_id | purchase_date
------------|-------------|----------|-------------------------
1           | 5           | 3        | 2023-11-14T15:53:26.757Z
2           | 8           | 2        | 2018-10-14T25:43:22.621Z
3           | 10          | 1        | 2023-11-14T05:19:14.701Z
4           | 7           | 2        | 2023-11-14T16:24:17.532Z
5           | 9           | 3        | 2023-11-14T49:07:19.421Z

_Invoice entry (PurchaseItem)_
The table contains information about goods purchased as part of one purchase (items on the invoice). To simplify the analysis of information about purchases,
a product price field has been introduced into the table, which is filled in automatically based on the price of the product at the time of purchase.  
_Example table contents:_  
purchase_item_id|purchase_id | product_id | product_count | product_price
----------------|------------|------------|---------------|--------------
1               |1           | 26         | 1             | 27929
2               |1           | 8          | 1             | 20879.1
3               |2           | 9          | 1             | 4939
4               |2           | 36         | 1             | 33000
5               |3           | 41         | 1             | 6356.9


__Starting the program__
When you run the program, you will see the following:  
  
![ScreenMethods](https://github.com/SonThompson/Database-Shop/blob/main/Images/ScreenMethods.png)
![ScreenGET](https://github.com/SonThompson/Database-Shop/blob/main/Images/ScreenGET.png)
![ScreenPut](https://github.com/SonThompson/Database-Shop/blob/main/Images/ScreenPUT.png)
