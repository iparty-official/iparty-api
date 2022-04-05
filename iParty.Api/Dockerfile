#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["iParty.Api/iParty.Api.csproj", "iParty.Api/"]
COPY ["iParty.Business/iParty.Business.csproj", "iParty.Business/"]
COPY ["iParty.Data/iParty.Data.csproj", "iParty.Data/"]
RUN dotnet restore "iParty.Api/iParty.Api.csproj"
COPY . .
WORKDIR "/src/iParty.Api"
RUN dotnet build "iParty.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "iParty.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iParty.Api.dll"]
