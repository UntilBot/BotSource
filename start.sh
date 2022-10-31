#!/bin/sh

git pull

cd src/
dotnet build -c Release -o ../build/

cd ../
cp config.json build/

cd build/
dotnet Until.dll
