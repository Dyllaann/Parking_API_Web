# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "ParKing.Application.RaspberryApi.dll"]
