FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# GitHub ပေါ်ရှိ Folder အမည် အကြီး/အသေး အတိုင်း အတိအကျ ရေးပါ
# ဥပမာ- SocialMediaAPI.sln ဖြစ်နိုင်သည် (သို့) Api.sln
COPY SocialMediaAPI.sln ./
COPY SocialMediaAPI/SocialMediaAPI.csproj SocialMediaAPI/
COPY BAL/BAL.csproj BAL/
COPY MODEL/MODEL.csproj MODEL/
COPY REPOSITORY/REPOSITORY.csproj REPOSITORY/

# Project file တစ်ခုချင်းစီကို Restore လုပ်မည့်အစား Solution ဖိုင်ကို Restore လုပ်ခြင်းက ပိုစိတ်ချရသည်
RUN dotnet restore SocialMediaAPI.sln

COPY . .

# ပင်မ Project ရှိရာ Folder သို့ ပြောင်းပါ
WORKDIR "/src/SocialMediaAPI"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# DLL အမည်ကိုလည်း သတိထားပါ (Project name အတိုင်းဖြစ်ရမည်)
ENTRYPOINT ["dotnet", "SocialMediaAPI.dll"]