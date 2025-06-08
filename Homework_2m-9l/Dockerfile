# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore Homework_2m-9l.csproj
RUN dotnet publish -c Release -o /out
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app/Homework_2m-9l
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Homework_2m-9l.dll"]
