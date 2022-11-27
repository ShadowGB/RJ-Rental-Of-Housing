using Microsoft.AspNetCore.Mvc;

namespace RJRentalOfHousing.Infrastructure
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> HandleRequest<T>(T request,Func<T,Task> handler, Serilog.ILogger log)
        {
            try
            {
                log.Debug("正在处理类型为{type}的HTTP请求", typeof(T).Name);
                await handler(request);
                return new OkResult();
            }
            catch (Exception e)
            {
                log.Error("处理请求失败", e);
                return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }

        public static async Task<IActionResult> HandleQuery<TModel>(Func<Task<TModel>> query, Serilog.ILogger log)
        {
            try
            {
                return new OkObjectResult(await query());
            }
            catch (Exception e)
            {
                log.Error(e, "查询时发生错误");
                return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });

            }
        }
    }
}
