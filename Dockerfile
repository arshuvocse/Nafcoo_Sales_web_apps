# Escape=`
# ASP.NET Web Forms .NET Framework 4.8 IIS Container
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2022

WORKDIR /inetpub/wwwroot

# Remove default IIS content
RUN powershell -Command "Remove-Item -Recurse -Force C:\inetpub\wwwroot\*"

# Copy web application content
COPY ./Solution.Web /inetpub/wwwroot

# Expose HTTP port
EXPOSE 80
