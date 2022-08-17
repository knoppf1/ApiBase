#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 7200

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["eContadiAPI/eContadiAPI.csproj", "eContadiAPI/"]
COPY ["eContadi.Services/eContadi.Services.csproj", "eContadi.Services/"]
COPY ["eContadi.Views/eContadi.Views.csproj", "eContadi.Views/"]
COPY ["eContadi.Data/eContadi.Data.csproj", "eContadi.Data/"]
COPY ["Tools/Tools.csproj", "Tools/"]
COPY ["eContadi.Core/eContadi.Core.csproj", "eContadi.Core/"]
RUN dotnet restore "eContadiAPI/eContadiAPI.csproj"
COPY . .
WORKDIR "/src/eContadiAPI"
RUN dotnet build "eContadiAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eContadiAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eContadiAPI.dll"]