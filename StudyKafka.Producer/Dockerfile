FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StudyKafka.Producer/StudyKafka.Producer.csproj", "StudyKafka.Producer/"]
COPY ["StudyKafka.Application/StudyKafka.Application.csproj", "StudyKafka.Application/"]
COPY ["StudyKafka.Domain/StudyKafka.Domain.csproj", "StudyKafka.Domain/"]
RUN dotnet restore "StudyKafka.Producer/StudyKafka.Producer.csproj"
COPY . .
WORKDIR "/src/StudyKafka.Producer"
RUN dotnet build "StudyKafka.Producer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StudyKafka.Producer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyKafka.Producer.dll"]
