using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPdotNetCoreAuthentication.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
	public class HrManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
