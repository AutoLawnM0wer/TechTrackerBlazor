# TechTrackerBlazor
TechTrackerBlazor is a Blazor WebAssembly application designed to help users track and manage their technology inventory. It provides features for adding, editing, and deleting technology items, as well as categorizing them for better organization.
Uses C#, .NET, HTML and Chart.js. The framework is "Blazor Web App," which can be found in Visual Studio 2022.


https://github.com/AutoLawnM0wer/TechTrackerBlazor

## HOWTO
This project should be capable of running out-of-the-box, excluding connection credentials needed for mongodb. Connection to primary DBMongo database is created through "...\TechTrackerBlazor\connection.env".
If this submission, for some reason, does not contain "connection.env", create "connection.env" inside project folder and insert the following data:

```
MongoDbSettings__ConnectionString=""
MongoDbSettings__DatabaseName=""
```

Insert your  MongoDB connectionstring and database name in their respective locations.
Program can be started through Visual Studio 2022, once opened with "...\TechTrackerBlazor\TechTrackerBlazor.sln"
Program should automatically insert 25 repeating sample documents for each collection on initial startup. Before or after initial startup, if 1 or more document aleady exists in expected collection, then no example documents will be created.

## Features
- Full CRUD operations for all primary collections
- Dashboard displaying business metrics and analytics
- Monthly revenue calculations
- Transaction status management (paid, unpaid, refunded)
- Receipt preview and printing support
- Inventory tracking
- Repair order management
- Sample data generation on startup

## Advanced Features

### Receipt Generation System
Creates printable receipts using transaction data and automatically opens the browser print dialog using `window.print()`.

### Analytics Dashboard
Uses MongoDB aggregation pipelines to calculate:
- Monthly revenue
- Repair status counts
- Average turnaround time

Chart.js is used to visually display repair and device statistics.

### Environment-Based Configuration
MongoDB credentials are loaded through `connection.env`, which is excluded from GitHub to prevent sensitive credentials from being committed.

## Database Setup 
See "## HOWTO and ## Database Connection"

Create a MongoDB Database and insert generated connectionstring and Database name into "connection.env", using the expected formatting.


Provided Documents on startup were generated with ChatGPT
More data can be loaded into MongoDB database through functional CRUD buttons found on each page of the website.
Given data on startup can be modified with CRUD to prove expected functionality.

## Database Connection

Use 'connection.env' to store your database connection string. This file should be located in the root directory of the project and should contain the following line:
```
MongoDbSettings__ConnectionString=""
MongoDbSettings__DatabaseName=""
```

## Team Contribution Summary

Luke 
```
-
```
AJ 
```
-
```
Alex
```
-
```
Vanay 
```
-
```
