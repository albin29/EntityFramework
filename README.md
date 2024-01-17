https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli\

dotnet tool install --global dotnet-ef\

dotnet add package Microsoft.EntityFrameworkCore.Sqlite\
dotnet add package Microsoft.EntityFrameworkCore.Design\

dotnet ef migrations add initialcreate\
dotnet ef database update\
