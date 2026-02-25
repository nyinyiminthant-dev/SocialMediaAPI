FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Project ဖိုင်များကို ကူးယူခြင်း
COPY Api.sln ./
COPY Api/Api.csproj Api/
COPY BAL/BAL.csproj BAL/
COPY MODEL/MODEL.csproj MODEL/
COPY REPOSITORY/REPOSITORY.csproj REPOSITORY/

# Restore လုပ်ခြင်း (Solution ဖိုင်အတိုင်း)
RUN dotnet restore Api.sln

# ကျန်ရှိသော Source code အားလုံးကို ကူးယူခြင်း
COPY . .

# အဓိက အပြောင်းအလဲ- WORKDIR ကို /src မှာပဲ ထားပြီး 
# စတင်ရမည့် Project ဖိုင် (.csproj) ကို တိုက်ရိုက် ညွှန်ပြပြီး Build လုပ်ပါ
RUN dotnet build "Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# DLL အမည်ကို သေချာစစ်ဆေးပါ (Api.dll ဖြစ်ရပါမည်)
ENTRYPOINT ["dotnet", "Api.dll"]