FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /app

# Copy the project file
COPY *.sln ./
COPY ContainerService.App/*.csproj ./ContainerService.App/
COPY ContainerService.Core/*.csproj ./ContainerService.Core/
COPY ContainerService.Infrastructure/*.csproj ./ContainerService.Infrastructure/

# Restore the packages
RUN dotnet restore

# Copy everything else
COPY . ./
WORKDIR /app/ContainerService.App

FROM build AS publish
# Build the release
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM microsoft/aspnetcore:2.0 AS runtime
WORKDIR /app

# Copy the output from the build env
COPY --from=publish /app/ContainerService.App/out ./

ENTRYPOINT [ "dotnet", "ContainerService.App.dll" ]