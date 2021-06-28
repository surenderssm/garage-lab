
# PowerShell Notes

* Course link - https://www.linkedin.com/learning/learning-powershell/
* Case Insensitive (generally sentence case)
* Most of the cmdlets are singular
* Power of the Pipe "|"
  * Output from one command becomes input for the next.
  * Strings together multiple commands.
  * `get-service | out-file c:\services.txt`
  * `Get-Help Get-Service`
  * `get-help get-service -online`
* Discovering Cmdlets and Aliases

  * Get-Command
  * `Get-Service | Where-Object {$_.status -eq "stopped"}`
  * `get-service  | get-member`
  * `Get-ChildItem -Path C:\Test | Sort-Object`
  
* Functions

  * `Get-Service | Stop-Service -WhatIf`
  * `Get-Service | Stop-Service -confirm`
* Working with output

  * `Get-Service | Format-List *`
  * `Get-Service | Format-List *`
  * `Get-Service | Out-File "res.txt"`
  * `Get-Service | Export-Csv "res.csv"`
  * `Get-Service | Out-GridView`
  * `Get-Service | select-object Status`
  * `Get-Service | select-object * |`

* Running Powershell Remotely
  
  * `Get-Service -ComputerName webserver`
  * `Get-Service -ComputerName webserver, webserver1`
  * ISE remote powershell tabs

* Other modules

  * Module : collection of cmdlets for a particular function or application.
  * `Get-Command`
  * `Get-Module -ListAvailable`
  * `Import-Module -name hello`
  * `Get-Command -module hello`
  * Powershell gallery
  * `Get-ExecutionPolicy` : Execution policy
  * Server Cmdlets
    * `Get-WindowsFeature`
    * `Get-WindowsFeature -Name Web-Server | Install-WindowsFeature`
    * async
    * Office 365
      * `Install-Module -Name AzureAD` : Installs the module
      * `Get-Command -module AzureAd`
    * Azure
      * Azure Resource Manager
      * Azure PowerShell
      * Azure Cloud Shell
      * Azure command-line interface
      * Install : `Install-Module az -allowclobber`
      * `get-command -module "az.*"` : to list down the commands
      * `get-command -module "az.websites" : to list commands from az.websites module

# Cmdlets

# References

* Script Center

