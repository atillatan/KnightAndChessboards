dotnet clean .
rm -rf bin obj
dotnet restore
dotnet build
dotnet publish . --runtime osx-x64 --self-contained -c Release --output "Releases/osx-x64"