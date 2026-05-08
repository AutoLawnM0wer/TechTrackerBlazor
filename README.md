# TechTrackerBlazor
TechTrackerBlazor is a Blazor WebAssembly application designed to help users track and manage their technology inventory. It provides features for adding, editing, and deleting technology items, as well as categorizing them for better organization.
Uses C# and .NET, framework is "Blazor Web App," which can be found in Visual Studio 2022.

## HOWTO
This project should be capable of running out-of-the-box. Connection to primary DBMongo database is created through "...\TechTrackerBlazor\connection.env" 
Credentials should be prefilled. If this submission, for some reason, does not contain valid credentials, create "connection.env" and insert the following data:



## Features

Dashboard containing potentially useful business metrics
monthly revenue calculations - contained in dashboard
CRUD for every available page on website
Ability to mark transaction as paid / refunded / unpaid
Receipt preview button
## Advanced Features

Receipt document creator / receipt printer - 
Website dashboard - Uses aggregation to associate revenue, repair status count and average turnaroudn time for each device / ticket. Utilized chart.js to display device status count
Environment-based config - MongoDB connection settings are loaded from "connection.env" while being hidden from github to keep credentials out of source code

## Database Setup 

Data can be loaded into MongoDB database through functional CRUD buttons found on each page of the website.

## Database Connection

Use 'connection.env' to store your database connection string. This file should be located in the root directory of the project and should contain the following line:
```
MongoDbSettings__ConnectionString=""
MongoDbSettings__DatabaseName=""
