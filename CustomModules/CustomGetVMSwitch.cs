namespace CustomModules
{
    using System;
    using System.Linq;
    using System.Security.Authentication;
    using System.Management.Automation;
    using Microsoft.Management.Infrastructure;
    using Helpers;
    using Models;

    /// <summary>
    /// Custom Get-VMSwitch cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CustomVMSwitch", DefaultParameterSetName = ParameterSetOne)]
    public class CustomGetVmSwitch : PSCmdlet
    {
        private const string ParameterSetOne = "ParameterSetOne";
        private const string ParameterSetTwo = "ParameterSetTwo";

        private static readonly string[] SwitchTypes = { "Private", "Internal", "External" };

        /// <summary>
        /// Gets or sets Cim Session
        /// </summary>
        [Parameter(Position = 4, ValueFromPipeline = true)]
        public CimSession[] CimSession { get; set; }

        // Input parameters
        /// <summary>
        /// Gets or sets Computer Name.
        /// </summary>
        [Parameter(Position = 5, ValueFromPipeline = true)]
        public string[] ComputerName { get; set; }

        /// <summary>
        /// Gets or sets Credential
        /// </summary>
        [Parameter(Position = 6, ValueFromPipeline = true)]
        public PSCredential[] Credential { get; set; }

        /// <summary>
        /// Gets or sets Id
        /// </summary>
        [Parameter(Position = 1, ValueFromPipeline = true, ParameterSetName = ParameterSetTwo)]
        public Guid[] Id { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        [Parameter(Position = 1, ValueFromPipeline = true, ParameterSetName = ParameterSetOne)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Resource Pool Type
        /// </summary>
        [Parameter(Position = 2, ValueFromPipeline = true)]
        public string[] ResourcePoolName { get; set; }

        /// <summary>
        /// Gets or sets Switch Type
        /// </summary>
        [ValidateSet("Private", "Internal", "External")]
        [Parameter(Position = 3, ValueFromPipeline = true)]
        public string SwitchType { get; set; }

        /// <summary>
        /// Process record
        /// </summary>
        protected override void ProcessRecord()
        {
            using (var powerShell = PowerShell.Create())
            {
                if (!PrincipalHelper.IsAdminRole)
                    throw new AuthenticationException(
                        @"You do not have the required permission to complete this task. Contact the administrator of the
                    authorization policy for the computer");

                var command = powerShell.AddCommand("Get-VMSwitch");

                WriteVerbose($"Current parameter set name {ParameterSetName}");

                this.SetParameters(command);

                var results = command.Invoke();

                foreach (var item in results)
                {
                    // Mess convertion
                    dynamic t = item.BaseObject;

                    var result = new VMSwitch
                    {
                        Name = t.Name,
                        SwitchType = t.SwitchType.ToString(),
                        NetAdapterInterfaceDescription = t.NetAdapterInterfaceDescription
                    };

                    WriteObject(result);
                }
            }
        }

        /// <summary>
        /// Prepeare script using powershell instance.
        /// </summary>
        /// <param name="powerShell">The powershell instance.</param>
        private void SetParameters(PowerShell powerShell)
        {
            if (this.CimSession != null && this.CimSession.Any())
                powerShell.AddParameter(nameof(this.CimSession), this.CimSession);

            if (this.Credential != null && this.Credential.Any())
                powerShell.AddParameter(nameof(this.Credential), this.Credential);

            if (this.ComputerName != null && this.ComputerName.Any())
                powerShell.AddParameter(nameof(this.ComputerName), this.ComputerName);

            if (this.Id != null && this.Id.Any())
                powerShell.AddParameter(nameof(this.Id), this.Id);

            if (!string.IsNullOrWhiteSpace(this.Name))
                powerShell.AddParameter(nameof(this.Name), this.Name);

            if (this.ResourcePoolName != null && this.ResourcePoolName.Any())
                powerShell.AddParameter(nameof(this.ResourcePoolName), this.ResourcePoolName);

            if (!string.IsNullOrWhiteSpace(this.SwitchType))
            {
                if (!SwitchTypes.Contains(this.SwitchType))
                {
                    throw new ArgumentException($"Incorrect {nameof(this.SwitchType)}");
                }

                powerShell.AddParameter(nameof(this.SwitchType), this.SwitchType);
            }
        }
    }
}