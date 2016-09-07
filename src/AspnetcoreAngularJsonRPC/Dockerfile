FROM microsoft/dotnet:latest
MAINTAINER SOORAJ KIZHAKKE KARALA
COPY . /app
WORKDIR /app
RUN ["dotnet","restore"]
EXPOSE 5050
ENTRYPOINT ["dotnet","run"]