# Dotnet labs

## Setting up Concourse
1. `brew cask install docker`
1. start Docker if it doesn’t automatically
1. follow all steps on http://concourse.ci/docker-repository.html to start concourse
    look at the example docker-compose.yml and remember to change the "changeme"

### restarting conconcourse
1. export CONCOURSE_EXTERNAL_URL=http://`ipconfig getifaddr en0`:8080
1. docker-compose up

## Setting up Minio
    minio provides an s3 bucket for storing the artifacts
1. brew install minio/stable/minio
1. cd ~/workspace/concourse-docker
1. mkdir -p shared/artifacts
1. minio server shared<BR>
    It will print out your access keys and secret

might need to delete-pipeline and set-pipeline if the ip address changed
since cannot use localhost to access minio
