using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShoppingApp.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var now = DateTime.Now.TimeOfDay;
            StartTime = "11:00";
            EndTime = "11:59";

            if (now >= TimeSpan.Parse(StartTime) && now <= TimeSpan.Parse(EndTime))
            {
                context.Result = new ContentResult
                {
                    Content = $"You cannot edit or update the orders between {StartTime} and {EndTime}. Please try again later.",
                    StatusCode = 403
                };

                base.OnActionExecuting(context);
            }
        }
    }
}
