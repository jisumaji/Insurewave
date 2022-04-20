# Insurewave
==========================================================
Insurewave is a hassle free tool built to simplify the process of finding the right fit of an insurance policy for different kinds of assets owned by a buyer.
The project is based on ASP .net MVC architecture (.NET 5.0).
It targets 3 kinds of users, viz. buyers, brokers and insurers.
Buyers are the owners of the assets that are to be insured.
Brokers work as a mediator between a buyer and an insurer.
An insurer is the one who accepts a policy contract and provides insurance to the buyer accordingly.
==========================================================
.Net Core in C#, HTML, CSS, Bootstrap, JavaScript, jQuery
==========================================================
#Database Script
drop database Insurewave
go
create database Insurewave
go
--------------
use Insurewave
go
------------------------------
create table [User.Details]
(
	UserId varchar(30) constraint PKUserDetails primary key,
	Password varchar(30) not null,
	FirstName varchar(30) not null,
	LastName varchar(30) not null,
	Gender varchar(10) check (Gender in('male','female','others')),
	Role varchar(10) check (Role in('buyer','broker','insurer')),
	LicenseId int default -1
)
go
--------------------------------------------------------------
create table [CurrencyConversion]
(
	CountryId int constraint PKCurrencyConversion primary key,
	CountryName varchar(25)   not null constraint NameCurrencyConversion unique,
	Rate float not null
)
go
---------------------------------------------------------------------
create table [Buyer.Assets]
(
	AssetId int constraint PKBuyerAssets primary key identity (100000,1),
	UserId varchar(30) constraint FKBuyerAssetsUserId foreign key references [User.Details](UserId)  on delete cascade not null,
	CountryId int constraint FKBuyerAssetsCountryId foreign key references CurrencyConversion(CountryId) on delete set default default 1,
	AssetName varchar(50) not null,
	PriceUSD money not null,
	Type varchar(25) not null,
	Request varchar(5) check (Request in('yes','no')) default 'no'
)
go
----------------------------------------------------------------
create table [Broker.Details]
(
	BrokerId varchar(30) constraint PKBrokerDetails primary key references [User.Details](UserId)  on delete cascade,
	CustomerCount int default 0,
	Commission float default 0.0
)
go
---------------------------------------------------------------------------
create table [Insurer.Details]
(
	InsurerId varchar(30) constraint PKInsurerDetails primary key references [User.Details](UserId)  on delete cascade,
	NoOfProducts int default 0,
	Commission float default 0.0
)
go
---------------------------------------------------------------------------
create table PolicyDetails
(
	PolicyId int constraint PKPolicyDetails primary key identity (100000,1) not null,--auto generated
	AssetId int constraint FKPolicyDetailsAssetId foreign key references [Buyer.Assets](AssetId)  on delete cascade not null,
	InsurerId varchar(30) constraint FKPolicyDetailsInsurerId foreign key references [Insurer.Details](InsurerId) on delete no action not null,
	BrokerId varchar(30) constraint FKPolicyDetailsBrokerId foreign key references [Broker.Details](BrokerId) on delete no action not null,
	Duration int not null,
	Premium money not null,
	LumpSum money not null,
	StartDate date not null,
	PremiumInterval int not null,
	MaturityAmount money not null,
	PolicyStatus varchar(15) check (PolicyStatus in('accepted','rejected','pending')),--policy assigned or not
	ReviewStatus varchar(5) check (ReviewStatus in('yes','no')),--insurer saw it or not
	Feedback varchar(max)
)
go
-----------------------------------------------------
create table BrokerRequests
(
	RequestId int constraint PKBrokerRequests primary key identity(1,1),
	AssetId int constraint FKBrokerRequestsAssetId foreign key references [Buyer.Assets](AssetId)  on delete cascade not null,
	BrokerId varchar(30) constraint FKBrokerRequestsBrokerId foreign key references [Broker.Details](BrokerId) on delete no action not null,
	ReviewStatus varchar(5) check (ReviewStatus in('yes','no'))--broker saw or not
)
go
------------------------------------
create table PaymentBuyer
(
	PolicyId int constraint PKPaymentBuyer primary key references PolicyDetails,
	PaidStatus varchar(5) check (PaidStatus in ('true','false')) default 'false'
)
---------------------------------
============================================================================
