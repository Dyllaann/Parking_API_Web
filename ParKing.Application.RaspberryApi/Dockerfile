FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 49909
EXPOSE 44362

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ParKing.Application.RaspberryApi/ParKing.Application.RaspberryApi.csproj ParKing.Application.RaspberryApi/
COPY ParKing.Utils.Configuration/ParKing.Utils.Configuration.csproj ParKing.Utils.Configuration/
RUN dotnet restore ParKing.Application.RaspberryApi/ParKing.Application.RaspberryApi.csproj
COPY . .
WORKDIR /src/ParKing.Application.RaspberryApi
RUN dotnet build ParKing.Application.RaspberryApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ParKing.Application.RaspberryApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ParKing.Application.RaspberryApi.dll"]
