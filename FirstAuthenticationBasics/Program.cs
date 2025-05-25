namespace FirstAuthenticationBasics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            //Why we need this:
            //User sends request
            //USer gets redirected
            //User gets routed - whats the difference
            // User is authenticated
            // User is authorized
            //Host sends http response

            builder.Services.AddAuthentication()
                .AddCookie(Settings.AuthCookieName,
                options => 
                {
                    //What does this do - if a client attempts to access a secure resource,
                    //and they have not logged in,
                    //this is where they get sent
                    options.LoginPath = "/Auth/Login";
                    //This is for people who is not an admin but tries to go to admin only
                    options.AccessDeniedPath  = "/Auth/Forbidden";

                    options.Cookie.Name = Settings.AuthCookieName;
                });

            builder.Services.AddAuthorization(
                options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireClaim("admin", "true"));
                });
                

            var app = builder.Build();


            //If someone tries to access app using non secure port - will redirect to SSL port
            app.UseHttpsRedirection();
            app.UseRouting();
            
            //Logging in and logging out
            app.UseAuthentication();    

            //What you can do
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
