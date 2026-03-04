dotnet nuget push ./bin/Release/AES_Complete.1.0.0.nupkg \
  --api-key $1 \
  --source https://api.nuget.org/v3/index.json
