dotnet clean .
dotnet restore
dotnet build
dotnet publish . --runtime win10-x64 --self-contained -c Release --output "Releases/win10-x64"