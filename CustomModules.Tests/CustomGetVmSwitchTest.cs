using System.Management.Automation;

namespace CustomModules.Tests
{
    using NUnit.Framework;
    using System.Management.Automation.Runspaces;

    [TestFixture]
    public class CustomGetVmSwitchTest
    {
        [Test]
        public void GetVmSwitchWithoutParametersTest()
        {
            const string command = "Get-CustomVMSwitch";

            var initialSessionState = InitialSessionState.CreateDefault();

            initialSessionState.Commands.Add(new SessionStateCmdletEntry(command,
                typeof(CustomGetVmSwitch), null));

            using (var runspace = RunspaceFactory.CreateRunspace(initialSessionState))
            {
                runspace.Open();

                using (var powershell = PowerShell.Create())
                {
                    powershell.Runspace = runspace;

                    powershell.Commands.AddCommand(command);

                    var results = powershell.Invoke();

                    Assert.AreNotEqual(results.Count, 0);
                }
            }
        }

        [Test]
        public void GetVmSwitchWithParameters()
        {
            const string command = "Get-CustomVMSwitch";

            var initialSessionState = InitialSessionState.CreateDefault();

            initialSessionState.Commands.Add(new SessionStateCmdletEntry(command,
                typeof(CustomGetVmSwitch), null));

            using (var runspace = RunspaceFactory.CreateRunspace(initialSessionState))
            {
                runspace.Open();

                using (var powershell = PowerShell.Create())
                {
                    powershell.Runspace = runspace;

                    powershell.Commands.AddCommand(command);

                    var results = powershell.Invoke();

                    Assert.AreNotEqual(results.Count, 0);
                }
            }
        }
    }
}
