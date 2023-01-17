using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public static class Utility
	{
		public static int GetUserId(ClaimsPrincipal User)
		{
			return int.Parse(User.FindFirst("NameIdentifaier").Value);
		}
	}
}
