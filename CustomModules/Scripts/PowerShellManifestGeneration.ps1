$manifest = @{
	Path = "D:\Code\PowerShellCmdlets\CustomModules\bin\Release\CustomModules.psd1"
	RootModule = "CustomModules"
	CompanyName = "Ciklum"
	Author = "Ciklum"
	ModuleVersion = "1.0.0"
	Description = "Custom Get-MVSwitch Powershell cmdlet to getting virtual machine switches."
}

New-ModuleManifest @manifest