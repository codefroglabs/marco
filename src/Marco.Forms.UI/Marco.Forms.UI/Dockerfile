FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Marco.Forms.UI/Marco.Forms.UI/Marco.Forms.UI.csproj", "Marco.Forms.UI/Marco.Forms.UI/"]
COPY ["Marco.Forms.UI/Marco.Forms.UI.Client/Marco.Forms.UI.Client.csproj", "Marco.Forms.UI/Marco.Forms.UI.Client/"]
RUN dotnet restore "Marco.Forms.UI/Marco.Forms.UI/Marco.Forms.UI.csproj"
COPY . .
WORKDIR "/src/Marco.Forms.UI/Marco.Forms.UI"
RUN dotnet build "./Marco.Forms.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Marco.Forms.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marco.Forms.UI.dll"]
