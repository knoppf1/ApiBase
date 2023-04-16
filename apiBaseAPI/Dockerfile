#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 7200

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["apiBaseAPI/apiBaseAPI.csproj", "apiBaseAPI/"]
COPY ["apiBase.Services/apiBase.Services.csproj", "apiBase.Services/"]
COPY ["apiBase.Views/apiBase.Views.csproj", "apiBase.Views/"]
COPY ["apiBase.Data/apiBase.Data.csproj", "apiBase.Data/"]
COPY ["Tools/Tools.csproj", "Tools/"]
COPY ["apiBase.Core/apiBase.Core.csproj", "apiBase.Core/"]
RUN dotnet restore "apiBaseAPI/apiBaseAPI.csproj"
COPY . .
WORKDIR "/src/apiBaseAPI"
RUN dotnet build "apiBaseAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apiBaseAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apiBaseAPI.dll"]