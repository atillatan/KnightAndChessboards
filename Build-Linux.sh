dotnet clean .
rm -rf bin obj
dotnet restore
dotnet build
dotnet publish . --runtime linux-x64 --self-contained -c Release --output "Releases/linux-x64"