.PHONY: default test docker-test docker-coverage
default: docker-test

test:
	gradle :test

coverage:
	gradle :jacocoTestReport
	@printf "\nOpen build/reports/jacoco/test/html/index.html\n"

docker-build:
	docker build -f ../Dockerfile -t codiumteam/legacy-training-java .

docker-test:
	docker run --rm -v ${PWD}:/opt/project -v ${PWD}/../.gradle:/gradle-cache -w /opt/project codiumteam/legacy-training-java make test

docker-coverage:
	docker run --rm -v ${PWD}:/opt/project -v ${PWD}/../.gradle:/gradle-cache -w /opt/project codiumteam/legacy-training-java make coverage