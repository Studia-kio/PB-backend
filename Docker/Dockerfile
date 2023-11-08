FROM alpine:3.14

WORKDIR /app

COPY . .

RUN dotnet build -c Release

CMD dotnet run -c Release