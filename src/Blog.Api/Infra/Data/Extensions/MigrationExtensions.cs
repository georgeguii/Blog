using System.Runtime.CompilerServices;
using Blog.Api.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Infra.Data.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using BlogContext context = scope.ServiceProvider.GetRequiredService<BlogContext>();

            context.Database.Migrate();
        }
    }
}
