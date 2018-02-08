namespace CustomModules.Helpers
{
    using System.Linq;
    using System.Management.Automation;

    internal static class PrincipalHelper
    {
        internal static bool IsAdminRole
        {
            get
            {
                using (var powershell = PowerShell.Create())
                {
                    const string script = 
                        @"$currentPrincipal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
                          $currentPrincipal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)";

                    powershell.AddScript(script);

                    var result = powershell.Invoke().First();
                    
                    return (bool) result.BaseObject;
                }
            }
        }
    }
}
