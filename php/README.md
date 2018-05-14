# Weather kata
The goals is to identify the code smells and fix it.

You can check the complexity using

    make complexity
or

    make docker-complexity


# Pre requisites
    PHP 7.1 
or 

    docker
# Run the tests locally
Install dependencies with [composer](https://getcomposer.org):

	make install

Run the tests

	make tests
	
# Run the tests on docker container
Install dependencies with composer:

    make docker-install
	
Run the tests

	make docker-tests