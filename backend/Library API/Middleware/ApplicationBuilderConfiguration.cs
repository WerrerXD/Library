namespace Library_API.Middleware
{
    public static class ApplicationBuilderConfiguration
    {
        public static IApplicationBuilder ErrorHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}
