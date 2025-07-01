# Utiliza la imagen oficial de .NET 6 para construir la app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia los archivos de proyecto y restaura dependencias
COPY *.sln ./
COPY PrimeCine.csproj ./
RUN dotnet restore PrimeCine.csproj

# Copia el resto del código y publica la app
COPY . ./
RUN dotnet publish PrimeCine.csproj -c Release -o out

# Imagen de runtime para producción
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/out .

# Puerto por defecto para Railway
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PrimeCine.dll"] 