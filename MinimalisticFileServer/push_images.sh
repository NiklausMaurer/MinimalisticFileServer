#!/usr/bin/env bash

az acr login -n nmreg
docker push nmreg.azurecr.io/minimalisticfileserver
docker push nmreg.azurecr.io/nginx
