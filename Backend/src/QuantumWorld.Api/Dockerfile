FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /dotnetapp
COPY ./bin/Release/net7.0 .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT dotnet QuantumWorld.Api.dll