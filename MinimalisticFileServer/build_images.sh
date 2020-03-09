#!/usr/bin/env bash

docker build -t mfs .
docker build -f Nginx.Dockerfile -t nginx .
