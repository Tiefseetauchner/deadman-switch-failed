FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Web.Service/Web.Service.csproj", "Web/"]
RUN dotnet restore "Web.Service/Web.Service.csproj"
COPY . .
WORKDIR "/src/Web.Service"
RUN dotnet build "Web.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Service.dll"]
