FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TTRPGBulletinBoardBot.Host/TTRPGBulletinBoardBot.Host.csproj", "TTRPGBulletinBoardBot.Host/"]
RUN dotnet restore "TTRPGBulletinBoardBot.Host/TTRPGBulletinBoardBot.Host.csproj"
COPY . .
WORKDIR "/src/TTRPGBulletinBoardBot.Host"
RUN dotnet build "TTRPGBulletinBoardBot.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TTRPGBulletinBoardBot.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TTRPGBulletinBoardBot.Host.dll"]
