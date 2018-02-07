namespace CustomModules.Tests.Extensions
{
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using Base;
    using FluentAssertions;

    public static class GetAllSwitchesExtension
    {
        public static void ThenCommandResultShouldNotBeNull(this CmdletTestContext context, Collection<PSObject> result)
        {
            result.Should().NotBeNull();
        }

        public static void ThenCommandResultCountGreaterThanOne(this CmdletTestContext context,
            Collection<PSObject> result)
        {
            result.Count.Should().BeGreaterThan(1);
        }
    }
}
