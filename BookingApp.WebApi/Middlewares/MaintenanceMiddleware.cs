using ShoppingApp.Business.Operations.Setting;

namespace ShoppingApp.WebApi.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;

        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenanceMode = settingService.GetMaintenanceState();

            if (context.Request.Path.StartsWithSegments("/api/settings") || context.Request.Path.StartsWithSegments("/api/Auth/login"))
            {
                await _next(context);
                return;
            }

            if (maintenanceMode)
            {
                await context.Response.WriteAsync("Maintenance mode is active, we cannot provide service at the moment.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
