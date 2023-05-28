FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Authentication.Web.Service/Authentication.Web.Service.csproj", "Authentication.Web.Service/"]
RUN dotnet restore "Authentication.Web.Service/Authentication.Web.Service.csproj"
COPY . .
WORKDIR "/src/Authentication.Web.Service"
RUN dotnet build "Authentication.Web.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.Web.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Web.Service.dll"]
