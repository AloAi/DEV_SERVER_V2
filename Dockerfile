FROM mcr.microsoft.com/dotnet/sdk:3.1

ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef

COPY ./AloaiApi /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

EXPOSE 80/tcp

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh