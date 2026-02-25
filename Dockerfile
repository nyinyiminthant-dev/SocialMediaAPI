FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Project ဖိုင်များကို Lowercase path များအတိုင်း ကူးယူခြင်း
COPY api.sln ./
COPY api/api.csproj api/
COPY bal/bal.csproj bal/
COPY model/model.csproj model/
COPY repository/repository.csproj repository/

# Dependency Restore လုပ်ခြင်း
RUN dotnet restore api/api.csproj

# ကျန်ရှိသော Source code အားလုံးကို ကူးယူခြင်း
COPY . .

# အဓိက အမှားပြင်ဆင်ချက်- /src/api (စာလုံးအသေး) သို့ ပြောင်းပါ
WORKDIR "/src/api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
# DLL ဖိုင်အမည်သည် .csproj အမည်အတိုင်းဖြစ်ရပါမည်
ENTRYPOINT ["dotnet", "api.dll"]