# Dotnet labs

## Setting up Concourse
1. `brew cask install docker`
1. start Docker if it doesn’t automatically
1. follow all steps on http://concourse.ci/docker-repository.html to start concourse
    look at the example docker-compose.yml and remember to change the "changeme"

## Setting up Minio
    minio provides an s3 bucket for storing the artifacts
1. brew install minio/stable/minio
1. cd ~/workspace/concourse-docker
1. mkdir -p shared/artifacts
1. minio server shared
    It will print out your access keys and secret
