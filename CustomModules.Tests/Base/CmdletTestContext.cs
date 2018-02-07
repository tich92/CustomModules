namespace CustomModules.Tests.Base
{
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;

    /// <summary>
    /// Test context for Cmdlets.
    /// </summary>
    public abstract class CmdletTestContext
    {
        /// <summary>
        /// Gets instance of the Runspace.
        /// <see cref="Runspace"/>
        /// </summary>
        private Runspace Space { get; set; }

        /// <summary>
        /// Gets instance of the Powershell.
        /// <see cref="PowerShell"/>
        /// </summary>
        public PowerShell Powershell { get; private set; }

        /// <summary>
        /// Name of testable command.
        /// </summary>
        protected abstract string Command { get; }

        /// <summary>
        /// Prepearing pipeline for testing Cmdlet. Invoke on startup your test.
        /// </summary>
        /// <typeparam name="TCmdlet">Type of Cmdlet.</typeparam>
        public void PrepeareCmdlet<TCmdlet>()
            where TCmdlet : Cmdlet
        {
            var initialSessionState = InitialSessionState.CreateDefault();

            initialSessionState.Commands.Add(new SessionStateCmdletEntry(Command,
                typeof(TCmdlet), null));

            Space = RunspaceFactory.CreateRunspace(initialSessionState);
            Space.Open();

            Powershell = PowerShell.Create();
            Powershell.Runspace = Space;
        }

        /// <summary>
        /// Disposing all members which created on prepearing.
        /// </summary>
        public void Stop()
        {
            Space.Dispose();
            Powershell.Dispose();
        }
    }
}