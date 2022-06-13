# To check if Makefile has correct syntax (all must be tabs (^I), no spaces)
# cat -e -t -v Makefile

default: docker-tests

help:
	@printf "$$HELP"

dependencies:
	composer install

.PHONY: tests
tests:
	./vendor/bin/phpunit

.PHONY: coverage
coverage:
	./vendor/bin/phpunit --coverage-html coverage
	@printf "\nOpen coverage/index.html to see the code coverage\n"


.PHONY: docker-build
docker-build:
	docker build -t codiumteam/legacy-training-php:weather -f ../Dockerfile .

.PHONY: docker-tests
docker-tests:
	docker run --rm -v ${PWD}:/code codiumteam/legacy-training-php:weather make tests

.PHONY: docker-coverage
docker-coverage:
	@docker run --rm -v ${PWD}:/code codiumteam/legacy-training-php:weather make coverage


define HELP
# Local commands
	- make dependencies\tInstall the dependencies using composer
	- make tests\t\tRun the tests
# Docker commands
	- make docker-build\tInstall the dependencies
	- make docker-tests\t\tRun the tests on docker
# Extra commands (not required for the training)
	- make complexity\t\tCalculate code complexity
	- make docker-complexity\tCalculate code complexity on docker
 Please execute "make <command>". Example make help

endef

export HELP
