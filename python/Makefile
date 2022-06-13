default: docker-tests

.PHONY: tests
tests:
	python -m unittest tests/test_weather.py

.PHONY: coverage
coverage:
	coverage run --source=weather -m unittest
	coverage report
	coverage html
	@printf "To visualize the lines open the report at htmlcov/index.html\n"

.PHONY: docker-build
docker-build:
	@docker build . -t python-coverage

.PHONY: docker-tests
docker-tests:
	@docker run --rm -v "${PWD}:/code" -w /code codiumteam/tdd-training-python:latest make tests

.PHONY: docker-coverage
docker-coverage:
	@docker run --rm -v "${PWD}:/code" -w /code codiumteam/tdd-training-python:latest make coverage