create database Insurewave
go
--------------
use Insurewave
go
------------------------------
create table [User.Details]
(
	--UserId int constraint PKUserDetails primary key identity (10000,1) not null,
	UserId varchar(30) constraint PKUserDetails primary key,
	Password varchar(30) not null,
	Role varchar(10) check (Role in('buyer','broker','insurer')),
	FirstName varchar(30) not null,
	LastName varchar(30) not null,
	Gender varchar(10) check (Gender in('male','female','others'))
)
go
--------------------------------------------------------------
create table [CurrencyConversion]
(
	CountryId int constraint PKCurrencyConversion primary key,
	CountryName varchar(25) not null unique,
	Rate float not null
)
go
---------------------------------------------------------------------
create table [Buyer.Assets]
(
	AssetId int constraint PKBuyerAssets primary key identity (100000,1),--auto generated
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
	LicenseId int not null unique,
	CustomerCount int default 0,
	Commission float default 0.0
)
go
---------------------------------------------------------------------------
create table [Insurer.Details]
(
	InsurerId varchar(30) constraint PKInsurerDetails primary key references [User.Details](UserId)  on delete cascade,
	LicenseId int not null unique,--cant give not null else problem later
	NoOfProducts int default 0,
	Commission float default 0.0
)
go
---------------------------------------------------------------------------
create table PolicyDetails
(
	PolicyId int constraint PKPolicyDetails primary key identity (100000,1),--auto generated
	AssetId int constraint FKPolicyDetailsAssetId foreign key references [Buyer.Assets](AssetId)  on delete cascade,
	InsurerId varchar(30) constraint FKPolicyDetailsInsurerId foreign key references [Insurer.Details](InsurerId) on delete no action not null,
	BrokerId varchar(30) constraint FKPolicyDetailsBrokerId foreign key references [Broker.Details](BrokerId) on delete no action not null,
	Duration int not null,
	Premium money not null,
	LumpSum money not null,
	StartDate date not null,
	PremiumInterval int not null,
	MaturityAmount money not null,
	PolicyStatus varchar(15) check (PolicyStatus in('accepted','rejected','pending')),
	ReviewStatus varchar(5) check (ReviewStatus in('yes','no')),
	Feedback varchar(max)
)
go
--if policystatus 
-----------------------------------------------------