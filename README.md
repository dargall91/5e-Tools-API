# 5e-Tools-API

## Setup

### Install .NET SDK/Runtime:

Execute the following commands:

1. `wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb`
2. `sudo dpkg -i packages-microsoft-prod.deb`
3. `rm packages-microsoft-prod.deb`
4. `sudo apt-get install -y dotnet-sdk-8.0` -- should only be necessary on development machine
5. `sudo apt-get install -y aspnetcore-runtime-8.0`
6. `echo fs.inotify.max_user_instances=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p`

### Sound Player Dependencies

If you get this error: "Unable to load shared library 'runtimes/linux-x64/native/libminiaudio.so' or one of its dependencies"

1. Create a new folder `runtimes` in the 5eTools.API project folder.
2. Copy the neccessary runtime folders from `.nuget/packages/soundflow/runtimes` to the new folder

### Publish to Production Machine

1. `dotnet publish -c Release -o ./publish`
2. `scp -r ./publish/* username@server:./5e-Tools/5e-Tools-API`

### Run Production Build:

`dotnet 5eTools.API.dll`
