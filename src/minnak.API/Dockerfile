#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /API
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["minnak.API.csproj", "API/"]
RUN dotnet restore "API/minnak.API.csproj"
COPY . API/
RUN ls -la API
RUN dotnet build "API/minnak.API.csproj" -c Release -o /API/build

FROM build AS publish
RUN dotnet publish "API/minnak.API.csproj" -c Release -o /API/publish

FROM base AS final
WORKDIR /API
COPY --from=publish /API/publish .
ENTRYPOINT ["dotnet", "minnak.API.dll"]