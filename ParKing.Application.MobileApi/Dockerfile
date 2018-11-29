FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 58949
EXPOSE 44396

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ParKing.Application.MobileApi/ParKing.Application.MobileApi.csproj ParKing.Application.MobileApi/
COPY ParKing.Utils.Configuration/ParKing.Utils.Configuration.csproj ParKing.Utils.Configuration/
COPY ParKing.Data/ParKing.Data.csproj ParKing.Data/
RUN dotnet restore ParKing.Application.MobileApi/ParKing.Application.MobileApi.csproj
COPY . .
WORKDIR /src/ParKing.Application.MobileApi
RUN dotnet build ParKing.Application.MobileApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ParKing.Application.MobileApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ParKing.Application.MobileApi.dll"]
