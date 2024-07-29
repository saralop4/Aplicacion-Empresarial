FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base build-env
WORKDIR /src
COPY . .
WORKDIR /src/Ecommerce.Services.WebApi
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build-env /src/Ecommerce.Services.WebApi/out ./

ENTRYPOINT ["dotnet", "Ecommerce.Services.WebApi.dll"]


#$ docker image build -t ecommerce.aplicacion:1.0 -f ./Ecommerce.Services.WebApi/Dockerfile .
#$ docker container run --name ecommerce.aplicacion -d -p 8050:80 0eb009918c22
