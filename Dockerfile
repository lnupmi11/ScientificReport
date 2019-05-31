FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app
### Copy and restore dependencies ###
COPY . /app
RUN dotnet restore
RUN dotnet publish ScientificReport/ScientificReport.csproj -c Release -o /app/publish
RUN rm /app/publish/wwwroot/Rotativa/*
COPY deploy/wkhtmltopdf-xvfb /app/publish/wwwroot/Rotativa/wkhtmltopdf
RUN chmod 755 /app/publish/wwwroot/Rotativa/wkhtmltopdf

FROM microsoft/dotnet:2.2-aspnetcore-runtime

RUN ["apt-get", "update"]
RUN ["apt-get", "-y", "install", "libgdiplus", "wkhtmltopdf", "xvfb"]

WORKDIR /app
COPY --from=build-env /app/publish .

ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://0.0.0.0:80
EXPOSE 80
CMD dotnet ScientificReport.dll

