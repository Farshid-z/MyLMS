FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/MyLMS.Web.Host/MyLMS.Web.Host.csproj", "src/MyLMS.Web.Host/"]
COPY ["src/MyLMS.Web.Core/MyLMS.Web.Core.csproj", "src/MyLMS.Web.Core/"]
COPY ["src/MyLMS.Application/MyLMS.Application.csproj", "src/MyLMS.Application/"]
COPY ["src/MyLMS.Core/MyLMS.Core.csproj", "src/MyLMS.Core/"]
COPY ["src/MyLMS.EntityFrameworkCore/MyLMS.EntityFrameworkCore.csproj", "src/MyLMS.EntityFrameworkCore/"]
WORKDIR "/src/src/MyLMS.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/MyLMS.Web.Host", "src/MyLMS.Web.Host"]
COPY ["src/MyLMS.Web.Core", "src/MyLMS.Web.Core"]
COPY ["src/MyLMS.Application", "src/MyLMS.Application"]
COPY ["src/MyLMS.Core", "src/MyLMS.Core"]
COPY ["src/MyLMS.EntityFrameworkCore", "src/MyLMS.EntityFrameworkCore"]
WORKDIR "/src/src/MyLMS.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "MyLMS.Web.Host.dll"]
