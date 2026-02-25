FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# GitHub ပေါ်ရှိ Folder နာမည်အမှန်များအတိုင်း ကူးယူခြင်း
COPY Api.sln ./
COPY Api/Api.csproj Api/
COPY BAL/BAL.csproj BAL/
COPY MODEL/MODEL.csproj MODEL/
COPY REPOSITORY/REPOSITORY.csproj REPOSITORY/

# Dependency Restore လုပ်ခြင်း
RUN dotnet restore Api.sln

# ကျန်ရှိသော Source code အားလုံးကို ကူးယူခြင်း
COPY . .

# ပင်မ Project ရှိရာ Folder (Api) သို့ ပြောင်းပါ
WORKDIR "/src/Api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# DLL အမည်သည် .csproj အမည်အတိုင်း Api.dll ဖြစ်ရပါမည်
ENTRYPOINT ["dotnet", "Api.dll"]