# Dotnet labs


## Setting up Minio
    minio provides an s3 bucket for storing the artifacts
    and MUST be running before Concourse

```
brew install minio/stable/minio
cd ~/workspace/concourse-docker
mkdir -p shared/artifacts
minio server shared
```
    It will print out your access keys and secret

might need to delete-pipeline and set-pipeline if the ip address changed
since cannot use localhost to access minio

## Setting up Concourse
1. `brew cask install docker`
1. start Docker if it doesn’t automatically
1. follow all steps on http://concourse.ci/docker-repository.html to start concourse
    look at the example docker-compose.yml and remember to change the "changeme"
basically:
```
cd workspace/concourse-docker

```
Make a docker-compose.yml file
```
version: '3'

services:
  concourse-db:
    image: postgres:9.6
    environment:
      POSTGRES_DB: concourse
      POSTGRES_USER: concourse
      POSTGRES_PASSWORD: concourse
      PGDATA: /database

  concourse-web:
    image: concourse/concourse
    links: [concourse-db]
    command: web
    depends_on: [concourse-db]
    ports: ["8080:8080"]
    volumes: ["./keys/web:/concourse-keys"]
    restart: unless-stopped # required so that it retries until concourse-db comes up
    environment:
      CONCOURSE_BASIC_AUTH_USERNAME: concourse
      CONCOURSE_BASIC_AUTH_PASSWORD: concourse
      CONCOURSE_EXTERNAL_URL: "${CONCOURSE_EXTERNAL_URL}"
      CONCOURSE_POSTGRES_HOST: concourse-db
      CONCOURSE_POSTGRES_USER: concourse
      CONCOURSE_POSTGRES_PASSWORD: concourse
      CONCOURSE_POSTGRES_DATABASE: concourse

  concourse-worker:
    image: concourse/concourse
    privileged: true
    links: [concourse-web]
    depends_on: [concourse-web]
    command: worker
    volumes: ["./keys/worker:/concourse-keys"]
    environment:
      CONCOURSE_TSA_HOST: concourse-web
      CONCOURSE_BAGGAGECLAIM_DRIVER: naive
```

Make the keys
```
mkdir -p keys/web keys/worker

ssh-keygen -t rsa -f ./keys/web/tsa_host_key -N ''
ssh-keygen -t rsa -f ./keys/web/session_signing_key -N ''

ssh-keygen -t rsa -f ./keys/worker/worker_key -N ''

cp ./keys/worker/worker_key.pub ./keys/web/authorized_worker_keys
cp ./keys/web/tsa_host_key.pub ./keys/worker
```

### restarting conconcourse
1. export CONCOURSE_EXTERNAL_URL=http://`ipconfig getifaddr en0`:8080
1. docker-compose up

### changing the docker compose
`docker-compose rm --all`