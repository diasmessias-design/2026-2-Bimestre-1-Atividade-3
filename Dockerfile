FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app
COPY src/csharp/ ./
RUN dotnet build

ENTRYPOINT ["dotnet", "run", "--no-build"]