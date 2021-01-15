using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Options
{
    public class SwaggerOptions
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string JsonSpecificationUrl { get; set; }
        public bool SOAS { get; set; }
        public string BasePath { get; set; }
    }
}
