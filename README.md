# DRPmodifierAPI
API for the upcoming DRPmodifier website 🤗🤗🤗

Only 3 routes at the moment. One for updating (no verification is done of the contents or who is sending them), getting all values 
and getting values for a particular ID.

## Requirements
.NET CORE 3.0
Probably latest preview of Visual Studio.

## Installation 
An Azure SQL database and create a .env File with the following information from azure in the main folder
```
DataSource =
UserID = 
Password = 
InitialCatalog = 
```
And the sql create table file 

```
CREATE TABLE `env` (
  `FILENAMETEXTBOX` varchar(255) DEFAULT NULL,
  `JOINSECRETTEXTBOX` varchar(255) DEFAULT NULL,
  `PARTYIDTEXTBOX` varchar(255) DEFAULT NULL,
  `SMALLIMAGEKEYTEXTBOX` varchar(255) DEFAULT NULL,
  `LARGEIMAGEKEYTEXTBOX` varchar(255) DEFAULT NULL,
  `SMALLIMAGETEXTBOX` varchar(255) DEFAULT NULL,
  `ENDTIMEBOX` varchar(255) DEFAULT NULL,
  `STATETEXTBOX` varchar(255) DEFAULT NULL,
  `CLIENTIDTEXTBOX` varchar(255) NOT NULL,
  `LARGEIMAGETEXTBOX` varchar(255) DEFAULT NULL,
  `DETAILSTEXTBOX` varchar(255) DEFAULT NULL,
  `ID` int PRIMARY KEY AUTO_INCREMENT
) ```
