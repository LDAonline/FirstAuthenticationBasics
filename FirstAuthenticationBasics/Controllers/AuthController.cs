using FirstAuthenticationBasics.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirstAuthenticationBasics.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginFormModel model)
		{
			var account = UserManager.Login(model.UserName, model.Password);

			if (account != null)
			{
				//Create a new idenitity based on their permissions and associate it with a cookie name
				var identity = new ClaimsIdentity(account.Claims, Settings.AuthCookieName);



				//Think of idenitys as different means of identifying you e.g Drivers Licence, Passport
				// Principles are the subject i.e you
				//Supports attaching multiple identitys to a single principal	
				var principal = new ClaimsPrincipal(identity);

				var props = new AuthenticationProperties
				{
					//This is hard coded but this is what the "Remember me" functionality uses
					IsPersistent = true,
					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

				};

				//LEarn about the HttpContext more
				await HttpContext.SignInAsync(Settings.AuthCookieName, principal, props); 
			}
			else
			{
				//Basically just does nothing if cannot find account
				 return View(model);
			}

			return RedirectToAction("Index", "Home");	
		} 

		public IActionResult Forbidden()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(Settings.AuthCookieName);

			return RedirectToAction("Index", "Home");
		}
	}
}
