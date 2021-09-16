create database pizza

use pizza

create table PizzaDetails(
PizzaNumber int identity(1,1) primary key,
PizzaName varchar(30),
PizzaPrice int,
PizzaType varchar(20))

create table Toppings(
ToppingNumber int identity(1,1) primary key,
ToppingName varchar(30),
ToppingPrice int)

create table UserLoginDetails(
UserId int identity(101,1) primary key,
UserMail varchar(40),
UserName varchar(30),
UserPassword varchar(10),
UserAddress varchar(50),
UserPhone varchar(11))

create table Orders(
OrderId int identity(1,1) primary key,
UserId int foreign key references UserLoginDetails(UserId),
TotalAmount float,
DelivaryCharges int,
Status varchar(10))

create table OrderDetails(
ItemNumber int identity(1,1) primary key,
OrderId int foreign key references  Orders(OrderId),
PizzaNumber int foreign key references PizzaDetails(PizzaNumber))

create table OrderItemDetails(
ItemNumber int foreign key references OrderDetails(ItemNumber),
ToppingNumber int foreign key references Toppings(ToppingNumber),primary key(ItemNumber,ToppingNumber))


select * from PizzaDetails
insert into PizzaDetails(PizzaName,PizzaPrice,PizzaType) values ('Margherita',20,'Plain')
insert into PizzaDetails(PizzaName,PizzaPrice,PizzaType) values ('Cheese N Corn',30,'Cheezy')
insert into PizzaDetails(PizzaName,PizzaPrice,PizzaType) values ('Cheese N Tomato',30,'Cheezy')
insert into PizzaDetails(PizzaName,PizzaPrice,PizzaType) values ('Chicken Pepperoni',50,'Spicy')
insert into PizzaDetails(PizzaName,PizzaPrice,PizzaType) values ('Indi Tandoori',45,'Spicy')

select * from Toppings
insert into Toppings(ToppingName,ToppingPrice) values ('Olives',2)
insert into Toppings(ToppingName,ToppingPrice) values ('Tomato',5)
insert into Toppings(ToppingName,ToppingPrice) values ('Onion',4)
insert into Toppings(ToppingName,ToppingPrice) values ('Cheese',6)

select * from UserLoginDetails