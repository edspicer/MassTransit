#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ExampleMasstransit.Worker/ExampleMasstransit.Worker.csproj", "src/ExampleMasstransit.Worker/"]
RUN dotnet restore "src/ExampleMasstransit.Worker/ExampleMasstransit.Worker.csproj"
COPY . .
WORKDIR "/src/src/ExampleMasstransit.Worker"
RUN dotnet build "ExampleMasstransit.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleMasstransit.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

ENV TZ=America/Sao_Paulo
ENV LANG pt-BR
ENV LANGUAGE pt-BR
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleMasstransit.Worker.dll"]