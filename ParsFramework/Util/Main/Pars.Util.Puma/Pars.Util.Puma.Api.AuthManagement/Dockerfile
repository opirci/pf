FROM microsoft/aspnet:4.7
ARG source
WORKDIR /inetpub/wwwroot
COPY ${source:-obj/Docker/publish} .
HEALTHCHECK CMD powershell -command try {  $response = iwr http://localhost/api/values/get -UseBasicParsing;  if ($response.StatusCode -eq 200) { return 0}   else {return 1};  } catch { return 1 }