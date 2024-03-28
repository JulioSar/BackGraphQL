FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ["BackGraphQL.Web.csproj", "BackGraphQL/"]
RUN dotnet restore "BackGraphQL/BackGraphQL.Web.csproj"
WORKDIR "/src/BackGraphQL"
COPY . .
RUN dotnet build "BackGraphQL.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackGraphQL.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","BackGraphQL.Web.dll"]



