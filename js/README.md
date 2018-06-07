# Using your node installation

## Install the package dependencies
    npm install

## Run the kata
    npm test

# Using docker

## Build the docker image
    docker build -t weather .

## Install the dependencies
    docker run -v "$PWD":/home/weather weather npm install

## Run the kata
    docker run -v "$PWD":/home/weather weather npm test 
