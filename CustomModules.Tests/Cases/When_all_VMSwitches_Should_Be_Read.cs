namespace CustomModules.Tests.Cases
{
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using Base;
    using Extensions;
    using NUnit.Framework;
    using TestStack.BDDfy;

    [TestFixture]
    public class When_all_VMSwitches_Should_Be_Read : CmdletTestContext, ITestRunner
    {
        protected override string Command => "Get-CustomVMSwitch";

        [OneTimeSetUp]
        public void Setup()
        {
            base.PrepeareCmdlet<CustomGetVmSwitch>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            base.Stop();
        }

        /// <summary>
        /// Test case for getting all VM Switches on current machine.
        /// </summary>
        [Test]
        public void Run()
        {
            Collection<PSObject> result = null;
            this.Given(x => this.GivenCommand(Command))
                .When(x => this.WhenInvokeCommand(out result))
                .Then(x => this.ThenCommandResultShouldNotBeNull(result))
                .And(x => this.ThenCommandResultCountGreaterThanOne(result))
                .BDDfy<When_all_VMSwitches_Should_Be_Read>();
        }
    }
}