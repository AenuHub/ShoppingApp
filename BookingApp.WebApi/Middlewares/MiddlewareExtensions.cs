namespace ShoppingApp.WebApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMaintenanceMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>();
        }
    }
}
