
# Guide

## For development environment:
Run SQL Server Docker image:
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=1Secure*Password1" -p 1440:1433 --name fptbook -d mcr.microsoft.com/mssql/server:2019-latest
```