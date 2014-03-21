using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WePayMVC5Example.IoC
{
    /// <summary>
    /// Simple class to contain 'global variables' and injected using 'Ninject' nuget-package
    /// </summary>
    public class GlobalVariables : IGlobalVariables
    {
        public string hostUrl { get; set; }
    }
}