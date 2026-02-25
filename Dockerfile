FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Api/Api.csproj", "Api/"]
COPY ["BAL/BAL.csproj", "BAL/"]
COPY ["MODEL/MODEL.csproj", "MODEL/"]
COPY ["REPOSITORY/REPOSITORY.csproj", "REPOSITORY/"]

RUN dotnet restore "Api/Api.csproj"

COPY . .
WORKDIR "/Api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
