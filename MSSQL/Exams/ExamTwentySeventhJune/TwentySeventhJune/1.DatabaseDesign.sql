CREATE TABLE Clients (
						ClientId INT IDENTITY PRIMARY KEY,
						FirstName VARCHAR(50) NOT NULL,
						LastName VARCHAR(50) NOT NULL,
						Phone CHAR(12) NOT NULL
					);

CREATE TABLE Mechanics (
						MechanicId INT IDENTITY PRIMARY KEY,
						FirstName VARCHAR(50) NOT NULL,
						LastName VARCHAR(50) NOT NULL,
						[Address] VARCHAR(255) NOT NULL
					);

CREATE TABLE Models (
						ModelId INT IDENTITY PRIMARY KEY,
						[Name] VARCHAR(50) UNIQUE NOT NULL ,
					)

CREATE TABLE Jobs (
						JobId INT IDENTITY PRIMARY KEY,
						ModelId INT REFERENCES Models(ModelId) NOT NULL,
						[Status] VARCHAR(11) DEFAULT 'Pending' CHECK([Status] IN ('Pending','In Progress','Finished')),
						ClientId INT REFERENCES Clients(ClientId) NOT NULL,
						MechanicId INT REFERENCES Mechanics(MechanicId),
						IssueDate DATE NOT NULL,
						FinishDate DATE 
					)

CREATE TABLE Orders (
						OrderId	INT IDENTITY PRIMARY KEY,
						JobId INT REFERENCES Jobs (JobId),
						IssueDate DATE,
						Delivered BIT DEFAULT 0 
					)


CREATE TABLE Vendors (
						VendorId	INT IDENTITY PRIMARY KEY,
						[Name] VARCHAR(50) UNIQUE NOT NULL,
					)

CREATE TABLE Parts (
						PartId	INT IDENTITY PRIMARY KEY,
						SerialNumber VARCHAR(50) UNIQUE NOT NULL,
						[Description] VARCHAR(255),
						Price DECIMAL(6,2) NOT NULL CHECK(Price > 0),
						VendorId	INT REFERENCES Vendors(VendorId) NOT NULL,
						StockQty INT NOT NULL DEFAULT 0 CHECK(StockQty >=0)
					)

CREATE TABLE OrderParts (
							OrderId	INT REFERENCES Orders(OrderId),
							PartId	INT REFERENCES Parts(PartId),
							Quantity INT NOT NULL DEFAULT 1 CHECK(Quantity >=1)
							PRIMARY KEY(OrderId,PartId)
						)

CREATE TABLE PartsNeeded (
							JobId	INT REFERENCES Jobs(JobId),
							PartId	INT REFERENCES Parts(PartId),
							Quantity INT NOT NULL DEFAULT 1 CHECK(Quantity >=1)
							PRIMARY KEY(JobId,PartId)
						)