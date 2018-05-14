# To check if Makefile has correct syntax (all must be tabs (^I), no spaces)
# cat -e -t -v Makefile

# Dependencies
install:
	composer install
docker-install:
	docker run -v $(shell pwd):/home/codium -u=$(shell id -u):$(shell id -g) composer bash -c "cd /home/codium && composer install"

# Tests
.PHONY: tests
tests:
	./vendor/bin/phpunit
docker-tests:
	docker run -v $(shell pwd):/home/codium php:7.1 bash -c "cd /home/codium && make tests"

# Complexity
complexity:
	./vendor/bin/phpmetrics src/
docker-complexity:
	docker run -v $(shell pwd):/home/codium php:7.1 bash -c "cd /home/codium && make complexity"
