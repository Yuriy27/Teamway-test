#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WorkShifts.Api/WorkShifts.Api.csproj", "WorkShifts.Api/"]
COPY ["WorkShifts.Application/WorkShifts.Application.csproj", "WorkShifts.Application/"]
COPY ["WorkShifts.Domain/WorkShifts.Domain.csproj", "WorkShifts.Domain/"]
COPY ["WorkShifts.Infrastructure/WorkShifts.Infrastructure.csproj", "WorkShifts.Infrastructure/"]
RUN dotnet restore "WorkShifts.Api/WorkShifts.Api.csproj"
COPY . .
WORKDIR "/src/WorkShifts.Api"
RUN dotnet build "WorkShifts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkShifts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkShifts.Api.dll"]
