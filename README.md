# 5e-Tools-API

## Setup

`wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb`
`sudo dpkg -i packages-microsoft-prod.deb`
`rm packages-microsoft-prod.deb`
`sudo apt-get install -y dotnet-sdk-8.0` -- should only be neccessary on development machine
`sudo apt-get install -y aspnetcore-runtime-8.0`
`echo fs.inotify.max_user_instances=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p`
