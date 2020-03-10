#!/usr/bin/env bash

docker build -t nmreg.azurecr.io/minimalisticfileserver .
docker build -f Nginx.Dockerfile -t nmreg.azurecr.io/nginx .
