using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_app.Pages
{
	[AllowAnonymous]
	public class IndexModel : PageModel
	{
		public IndexModel()
		{
		}

		public void OnGet()
		{
		}
	}
}
