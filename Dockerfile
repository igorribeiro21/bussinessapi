#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BussinessApi/BussinessApi.csproj", "BussinessApi/"]
RUN dotnet restore "BussinessApi/BussinessApi.csproj"
COPY . .
WORKDIR "/src/BussinessApi"
RUN dotnet build "BussinessApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BussinessApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet BussinessApi.dll
ENTRYPOINT ["dotnet", "BussinessApi.dll"]
#RUN dotnet run