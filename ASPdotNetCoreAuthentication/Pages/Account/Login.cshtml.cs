using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPdotNetCoreAuthentication.Pages.Account
{
	public class LoginModel : PageModel
    {
        // Create a model we use to communicate between front and back
        [BindProperty] // this give you feeling to a databinding
        public Credential Credential { get; set; }

        public void OnGet()
        {
            this.Credential = new Credential { Username = "admin" };
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Verify the credential
            if (Credential.Username == "admin" && Credential.Password == "password")
            {
                // Creating the security context
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("Manager", "true"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com")
                };

                // Add claims to identity, also specify authentication name (anyname)
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                // principal contains the security context, and can have many identity
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                // serialize claims principal into a string
                // then encrypt that string, save that as cookie in the HttpContext
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }
            return Page();
        }
    }

    public class Credential
    {
        [Required] // to make it required
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)] // to assign datatype
        public string Password { get; set; }
    }
}
