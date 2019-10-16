#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1903 AS build
WORKDIR /src
COPY ["SchoolTripPlanner/SchoolTripPlanner.csproj", "SchoolTripPlanner/"]
COPY ["SchoolTripPlanner.Data/SchoolTripPlanner.Data.csproj", "SchoolTripPlanner.Data/"]
COPY ["SchoolTripPlanner.Domain/SchoolTripPlanner.Domain.csproj", "SchoolTripPlanner.Domain/"]
RUN dotnet restore "SchoolTripPlanner/SchoolTripPlanner.csproj"
COPY . .
WORKDIR "/src/SchoolTripPlanner"
RUN dotnet build "SchoolTripPlanner.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SchoolTripPlanner.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SchoolTripPlanner.dll"]