﻿using System.Linq;
using CustomModules.Models;
using FluentAssertions;

namespace CustomModules.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Management.Automation;
    using Base;

    public static class GetInternalSwitchesExtension
    {
        public static void GivenParameters(this CmdletTestContext context, out Dictionary<string, string> parameters)
        {
            var dictionary = new Dictionary<string, string> {{"SwitchType", "Internal"}};

            parameters = dictionary;
        }

        public static void ThenAllSwitchesShouldBeInternal(this CmdletTestContext context, ICollection<PSObject> result)
        {
            result.Should().NotContainNulls();
            result.Select(r => ((VMSwitch) r.BaseObject).SwitchType).Should().NotContain(new [] { "Private", "External"});
        }
    }
}
