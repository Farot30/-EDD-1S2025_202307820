# -EDD-1S2025_202307820

Instalar dependencias

sudo apt-get install libgtk-3-dev

dotnet new console -n NombreProyecto

sudo apt-get install gtk-sharp3

ver la version del gtk
pkg-config --modversion gtk+-3.0

dotnet add package GtkSharp


wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install dotnet-sdk-6.0

## instalar .jSON
dotnet add package Newtonsoft.Json

