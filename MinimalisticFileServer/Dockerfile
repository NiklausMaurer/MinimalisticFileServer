# Build stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /MinimalisticFileServer

# restore
COPY MinimalisticFileServer.sln ./
COPY MinimalisticFileServer/MinimalisticFileServer.csproj ./MinimalisticFileServer/
COPY MinimalisticFileServerTest/MinimalisticFileServerTest.csproj ./MinimalisticFileServerTest/

RUN dotnet restore MinimalisticFileServer.sln

# # copy src
COPY . ./

# # test
RUN dotnet test MinimalisticFileServerTest/MinimalisticFileServerTest.csproj

# # publish
RUN dotnet publish MinimalisticFileServer/MinimalisticFileServer.csproj -o /publish

# Runtime stage
FROM  mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /publish /publish

ENV MINIMALISTICFILESERVER_PATH /publishedfiles

WORKDIR /publish
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "MinimalisticFileServer.dll"]
