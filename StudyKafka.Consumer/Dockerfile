FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StudyKafka.Consumer/StudyKafka.Consumer.csproj", "StudyKafka.Consumer/"]
RUN dotnet restore "StudyKafka.Consumer/StudyKafka.Consumer.csproj"
COPY . .
WORKDIR "/src/StudyKafka.Consumer"
RUN dotnet build "StudyKafka.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StudyKafka.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyKafka.Consumer.dll"]
