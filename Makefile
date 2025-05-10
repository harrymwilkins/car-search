build_database:
	docker-compose build database
build_worker:
	make build_database
	docker-compose build worker
migrate:
	make build_worker
	docker-compose build migrator --no-cache
run:
	make build_worker
	make migrate
	docker-compose up
stop_all:
	docker-compose down