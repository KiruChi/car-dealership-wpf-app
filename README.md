# car-dealership-wpf-app
A desktop management system for a used car dealership, built with C#, WPF, and MySQL. Features role-based access, inventory filtering, and sales automation.

## 🛠 Tech Stack
* **Programming Language:** C#
* **Frontend:** WPF (Windows Presentation Foundation)
* **Database:** MySQL Server
* **ORM/Database Connectivity:** MySQL Data Provider for .NET
* **IDE:** Visual Studio 2022

## ✨ Key Features
* **Vehicle Inventory Management:** Add, edit, delete, and view detailed information about available cars, including full technical specs and status.
* **Client & Employee Records:** Secure storage and management of personal data for buyers, sellers, and dealership staff.
* **Sales Automation:** Process transactions, automatically calculate sums, and generate sales records.
* **Advanced Search & Filtering:** Multi-parameter filtering (brand, price, year, mileage, fuel type, etc.) to quickly find specific vehicles.
* **Data Integrity:** Mechanisms to check the correctness of entered data and compliance with legal requirements for documents.

## 👥 User Roles

### Administrator (Manager)
* Full access to all system functions.
* Can add, edit, and delete vehicles, employees, and client records.
* Access to detailed sales statistics and branch analytics.
* Can restore mistakenly deleted records from the "Deleted" category.

### Sales Consultant
* Access to view inventory and search for vehicles/clients.
* Can register new customers and process sales transactions.
* No permissions to edit or delete existing critical data.

## 📸 Screenshots
### App
<img width="337" height="459" alt="image" src="https://github.com/user-attachments/assets/ed474809-187f-41c4-a9bb-17911cae6aff" />
<img width="1033" height="457" alt="image" src="https://github.com/user-attachments/assets/34b9d7b9-e42d-4696-ba5e-419eec3b0995" />
<img width="932" height="415" alt="image" src="https://github.com/user-attachments/assets/e3e24d70-0701-4ec1-a177-58f7df2509c0" />
<img width="934" height="413" alt="image" src="https://github.com/user-attachments/assets/c0ee8ed6-9b6e-4c56-affc-de5692bb96e5" />
<img width="899" height="401" alt="image" src="https://github.com/user-attachments/assets/f931076f-af51-4249-9688-57f9633ed18a" />
<img width="908" height="404" alt="image" src="https://github.com/user-attachments/assets/031810fb-3902-4a98-a082-9dba6baa2f70" />
<img width="306" height="455" alt="image" src="https://github.com/user-attachments/assets/5b047663-6c7c-4542-8718-2e62b35b943d" />

### Database Schema
<img width="892" height="1024" alt="image" src="https://github.com/user-attachments/assets/c25f9f76-27e5-44b7-bbaa-b1e8e36bf07e" />

## ⚙️ Requirements
Before building the project, ensure you have:
* Visual Studio 2022 (with .NET desktop development workload)
* .NET Framework 4.8 or .NET 6.0+
* MySQL Community Server 8.0+
* MySQL Workbench

## 🔧 Build & Setup Instructions
### 1. Download folder "app" and "database"
### 2. Database Setup(MySQL Workbench)
Execute the SQL scripts provided in the `database/` folder to create the schema and tables:
1. Run `CompanyDB.sql` to create the structure.
2. (Optional) Run `CompanyDB(2).sql` to fill the tables with test data.

The database name will be: `Company_DB`.

**Insert test users for authentication:**
```sql
USE Company_DB;

INSERT INTO Users (username, password, role) VALUES 
('admin', 'admin123', 'Administrator'),
('consultant', 'user123', 'Sales Consultant');
```
### 3. Configure Connection
Update the connection string in the Helper->DBHelper.cs file to match your local MySQL credentials:
```C#
builder.Server = "127.0.0.1";
builder.UserID = "root";
builder.Password = "YOUR_PASSWORD"; // This should be your password to your MySQL connection
```

### 4. Build and Run(Visual Studio 2022)
1. Open the solution file Kursova.sln in Visual Studio 2022.
2. Build the solution (Ctrl + Shift + B).
3. Press F5 to start the application.
