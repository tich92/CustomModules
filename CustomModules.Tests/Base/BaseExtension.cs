namespace CustomModules.Tests.Base
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Management.Automation;

    public static class BaseExtension
    {
        public static void GivenCommand(this CmdletTestContext context, string command) => 
            context.Powershell.AddCommand(command);

        public static void WhenAddParameters(this CmdletTestContext context, IDictionary<string, string> parameters)
        {
            foreach (var parameter in parameters)
            {
                context.Powershell.AddParameter(parameter.Key, parameter.Value);
            }
        }

        public static void WhenInvokeCommand(this CmdletTestContext context, out Collection<PSObject> result)
        {
            result = context.Powershell.Invoke();
        }
    }
}