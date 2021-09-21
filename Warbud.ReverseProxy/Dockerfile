FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Warbud.ReverseProxy/Warbud.ReverseProxy.csproj", "Warbud.ReverseProxy/"]
RUN dotnet restore "Warbud.ReverseProxy/Warbud.ReverseProxy.csproj"
COPY . .
WORKDIR "/src/Warbud.ReverseProxy"
RUN dotnet build "Warbud.ReverseProxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Warbud.ReverseProxy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warbud.ReverseProxy.dll"]
