<#
	.SYNOPSIS
	Creates the file system folder structure along with IIS website and application for the given API.
	.DESCRIPTION
	Creates the file system folder structure for an API.
	Creates the IIS website for the API.
	Creates the application for the specified version of the API.
	.PARAMETER site_name
	The site name for the API. Required.
	.PARAMETER app_name
	The application name for the API. Required.
	.PARAMETER site_hostname
	The hostname of te API website. Required.
	.PARAMETER code_folder
	The folder from which to copy the latest code. Required.
#>

Param(
	[Parameter(mandatory=$true)]
	[string]$site_name = "",
	
	[Parameter(mandatory=$true)]
	[string]$app_name = "",
	
	[Parameter(mandatory=$true)]
	[string]$site_hostname = "",
	
	[Parameter(mandatory=$true)]
	[string]$code_folder = ""
);

## Named constants
New-Variable -Name "CONTENT_ROOT" -Value "D:\Content"			-Option Constant;
New-Variable -Name "SITES_ROOT"   -Value "D:\Content"			-Option Constant;
New-Variable -Name "BACKUP_ROOT"  -Value "D:\backups"			-Option Constant;

function Main() {
	## The WebAdministration module requires elevated privileges.
	$isAdmin = Is-Admin;
	if( $isAdmin ) {
		Write-Host -foregroundcolor 'green' "Starting...";
		Import-Module WebAdministration;
		CreateSite;
	} else {
		Write-Host -foregroundcolor 'red' "This script must be run from an account with elevated privileges.";
	}
}

function Is-Admin {
 $id = New-Object Security.Principal.WindowsPrincipal $([Security.Principal.WindowsIdentity]::GetCurrent());
 $id.IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator);
}

function CreateSite() {
	## Check parameters
	if ($site_name -eq "")
	{
		Write-Host -foregroundcolor 'red' "ERROR: Site Name must not be null.";
		exit;
	}

	if ($app_name -eq "")
	{
		Write-Host -foregroundcolor 'red' "ERROR: App Name must not be null.";
		exit;
	}

	if ($site_hostname -eq "")
	{
		Write-Host -foregroundcolor 'red' "ERROR: Site Host Name must not be null.";
		exit;
	}

	if ($code_folder -eq "")
	{
		Write-Host -foregroundcolor 'red' "ERROR: Code Folder must not be null.";
		exit;
	}

	## Set up folder names for file system folder structure
	$sitePath = $SITES_ROOT + "\" + $site_name;
	$appPath = $sitePath;

	## Make site directories
	## Should check non-existence first
	foreach ( $folder in (
			$CONTENT_ROOT,
			$SITES_ROOT,
			$sitePath,
			$appPath
			)
		)
	{
		if (-not (Test-Path $folder))
		{
			Write-Host "INFO: Creating folder ${folder}.";
			New-Item -ItemType directory -Path $folder;
		}
	}

	## Stop web site so updated files can be copied.
	$webSiteExists = (Get-WebSite -Name $site_name) -ne $null
	if( $webSiteExists -eq $true ){
		Stop-Website -Name $site_name
	}

	
	## Make version directory and copy files
	if (Test-Path $sitePath)
	{
		if(!(Test-Path $code_folder))
		{
			Write-Host -foregroundcolor 'red' "ERROR: Code folder ${code_folder} does not exist.";
			Exit;
		}
		else
		{
		    # Create a backup before overwriting existing files.
			BackupFiles $site_name $sitePath;
		
			Write-Host "INFO: Copying files from ${code_folder} to ${versionAppPath}.";
			
			## Clear contents of version folder (in case there are extraneous files from an old build)
			Get-ChildItem $sitePath -Recurse | Foreach-Object {Remove-Item -Recurse -path $_.FullName }

			## Copy files
			Get-ChildItem $code_folder | Copy-Item -Destination $sitePath -force -Recurse
		}
	}
	else
	{
		Write-Host "INFO: Creating folder ${versionAppPath}.";
		New-Item -ItemType directory -Path $sitePath;
		
		if (!(Test-Path($code_folder)))
		{
			Write-Host -foregroundcolor 'red' "ERROR: Code folder ${code_folder} does not exist.";
			Exit;
		}
		else
		{
			Write-Host "INFO: Copying files from ${code_folder} to ${versionAppPath}.";
			
			## Clear contents of version folder (in case there are extraneous files from an old build)
			Get-ChildItem $sitePath -Recurse | Foreach-Object {Remove-Item -Recurse -path $_.FullName }

			## Copy files
			Get-ChildItem $code_folder | Copy-Item -Destination $sitePath -force -Recurse;
		}
	}

	## Restart web site.
	if( $webSiteExists -eq $true ){
		Start-Website -Name $site_name
	}

	###### Setup Web Site ######

	## Set up app pool/website variables
	$siteAppPool = $site_name;
	$siteIISAppPool = "IIS:\AppPools\" + $siteAppPool;
	$siteIISPath = "IIS:\Sites\" + $site_name;
	$appAppPool = $site_name;
	$appIISAppPool = "IIS:\AppPools\" + $appAppPool;
	$appIISPath = $siteIISPath + "\" + $app_name;
	$appVD = $app_name;

	## Create application pool for website
	if((Test-Path $siteIISAppPool) -eq 0)
	{
		New-WebAppPool -Name $siteAppPool;
		Set-ItemProperty -Path $siteIISAppPool -Name managedRuntimeVersion -Value "v4.0";
	}
	else
	{
		Write-Host "INFO: Application pool ${siteAppPool} already exists. Skipping creation.";
	}

	## Create site and application
	if((Get-Item $siteIISPath -ErrorAction SilentlyContinue) -eq $null)
	{
		## If website doesn't already exist, create it
		New-Website -Name $site_name -Port 443 -HostHeader $site_hostname -PhysicalPath $sitePath -ApplicationPool $siteAppPool -Ssl;
		New-WebBinding -Name $site_name -Protocol "http" -Port 80 -HostHeader $site_hostname;
	}
	else
	{
		Write-Host "INFO: Website ${site_name} already exists. Skipping creation.";
	}
	
}

function BackupFiles($siteName, $sitePath) {
	$datestring = Get-Date -Format "yyyy-MM-dd-HH-mm_s";
	$backupLocation = $BACKUP_ROOT + "\" + $siteName + "\" + $datestring;
	Write-Host "INFO: Backing up to" $backupLocation;

	New-Item -ItemType directory -Path $backupLocation
	Get-ChildItem $sitePath | Copy-Item -Destination $backupLocation -force;
}

Main;
Read-Host -Prompt "Press Enter to exit."
