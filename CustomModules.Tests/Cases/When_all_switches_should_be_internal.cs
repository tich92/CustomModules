namespace CustomModules.Tests.Cases
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using Base;
    using Extensions;
    using NUnit.Framework;
    using TestStack.BDDfy;

    [TestFixture]
    public class When_all_switches_should_be_internal : CmdletTestContext, ITestRunner
    {
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

        protected override string Command => "Get-CustomVMSwitch";

        [Test]
        public void Run()
        {
            Dictionary<string, string> parameters = null;
            Collection<PSObject> result = null;

            this.Given(x => this.GivenCommand(Command))
                .And(x => this.GivenParameters(out parameters))
                .When(x => this.WhenAddParameters(parameters))
                .And(x => this.WhenInvokeCommand(out result))
                .Then(x => this.ThenAllSwitchesShouldBeInternal(result))
                .BDDfy<When_all_switches_should_be_internal>();
        }
    }
}