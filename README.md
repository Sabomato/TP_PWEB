# Groundbnb

## Overview

This project was made in the context of the "WEB Programming" subject, computer engineering course, in [ISEC](https://www.isec.pt/PT/Default.aspx).  
The focus was building a website that allows managing, renting and hosting a property (same concept as Airbnb) with tools such as **Entity Framework Core**, **ASP.NET Core Identity** and **MVC pattern**  in **ASP.NET Core 3.1**.

## Usage

Just download the repository as a zip, extract it, open the solution on **Visual Studio 2019** and run it.  
If there is an error acessing the database, replace the value of the **"DefaultConnection"** key in the file **"appsettings.json"** with the connection string of the database in  **".\AppData\DB_TP_PWEB.mdf"**, after connecting it with your local server.


## Test users

 The following users are already created( Email is the same as the password):  
 
- Admin( manages property managers and clients):
  - Admin1@email.com
  
- Property Manager ( manages properties and employees ):
  - PropertyManager1@email.com
  
- Employee( manages reservations and to-do lists in check-in/:
  - PropertyEmployee1@email.com
- Clients (manages his reservations):
  - Client1@email.com


 
