create database Insurewave
------------------------------
create table [User.Details]
(
	UserId int primary key,
	MailId varchar(30) not null unique,
	Password varchar(30) not null,
	Role varchar(10) check (Role in('Buyer','Broker','Insurer')),
	FirstName varchar(30) not null,
	LastName varchar(30) not null,
	Gender varchar(10) not null
)
--------------------------------------------------------------
create table [CurrencyConversion]
(
	CountryId int primary key,
	CountryName varchar(25) not null unique,
	Rate float not null
)
---------------------------------------------------------------------
create table [Buyer.Assets]
(
	AssetId int primary key,
	UserId int foreign key references [User.Details](UserId)  on delete cascade not null,
	CountryId int foreign key references CurrencyConversion(CountryId) on delete set default default 1,
	AssetName varchar(50) not null,
	PriceUSD money not null,
	Type varchar(25) not null,
	Request varchar(5) check (Request in('yes','no')) default 'no'
)
----------------------------------------------------------------
create table [Broker.Details]
(
	BrokerId int primary key,
	UserId int foreign key references [User.Details](UserId)  on delete cascade not null,
	LicenseId int not null unique,
	CustomerCount int ,
	Commission float,
)
---------------------------------------------------------------------------
create table [Insurer.Details]
(
	InsurerId int primary key,
	UserId int foreign key references [User.Details](UserId)  on delete cascade not null,
	LicenseId int not null unique,
	NoOfProducts int
)
---------------------------------------------------------------------------
create table PolicyDetails
(
	PolicyId int primary key,
	AssetId int foreign key references [Buyer.Assets](AssetId)  on delete cascade not null,
	InsurerId int foreign key references [Insurer.Details](InsurerId) default -1 not null,
	Duration int not null,
	Premium money not null,
	LumpSum money not null,
	StartDate date not null,
	PremiumInterval int not null,
	PolicyStatus varchar(15) check (PolicyStatus in('accepted','rejected','pending')),
	MaturityAmount money not null,
	Feedback varchar(max),
)
----------------------------------------------------------------------
create table [Broker.Insurer]
(
	BIId int primary key,
	BrokerId int foreign key references [Broker.Details](BrokerId)  default -1 not null,
	InsurerId int foreign key references [Insurer.Details](InsurerId) default -1 not null,
	AssetId int foreign key references [Buyer.Assets](AssetId) on delete cascade default -1 not null,
	BrokerStatus varchar(15) check (BrokerStatus in('accepted','rejected')),
	PolicyId int foreign key references [PolicyDetails](PolicyId)  default -1 not null,
)
-----------------------------------------------------