# Dotnet labs

## Setting up Concourse

see https://gist.github.com/kevin-smets/f20afd45a24ab3f88d01b2049ce7744f

1. brew services start postgresql
1. curl -Lo concourse https://github.com/concourse/concourse/releases/download/v3.8.0/concourse_darwin_amd64 && chmod +x concourse && mv concourse /usr/local/bin
1. concourse --version
1. pg_config --version
1. createdb atc;
1. createdb concourse;
1. createuser concourse --pwprompt;
    You are going to be prompted for the password (aka the_password_you_gave_earlier).
1. ssh-keygen -t rsa -f host_key -N '' && ssh-keygen -t rsa -f worker_key -N '' && ssh-keygen -t rsa -f session_signing_key -N ''
1. cp worker_key.pub authorized_worker_keys
1. concourse web   --basic-auth-username concourse   --basic-auth-password <the_password_you_gave_earlier>   --session-signing-key session_signing_key   --tsa-host-key host_key   --tsa-authorized-keys authorized_worker_keys --bind-port=8880

