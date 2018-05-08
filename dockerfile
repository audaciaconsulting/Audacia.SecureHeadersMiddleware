FROM microsoft/dotnet:1-sdk as builder
WORKDIR /secureHeaders

COPY *.sln ./
COPY ./Audacia.SecureHeadersMiddleware/*.csproj ./Audacia.SecureHeadersMiddleware/
COPY global.json global.json

RUN dotnet restore

COPY Audacia.SecureHeadersMiddleware ./Audacia.SecureHeadersMiddleware
RUN dotnet publish --configuration Release
RUN dotnet pack ./Audacia.SecureHeadersMiddleware/Audacia.SecureHeadersMiddleware.csproj --configuration Release --no-build --output nupkgs
