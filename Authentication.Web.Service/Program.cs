namespace DeadmanSwitchFailed.Authentication.Web.Service;

public static class Program
{
  private static readonly string s_corsPolicy = "_allowAllOrigins";

  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AllowAllOrigins();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.AllowAllOrigins();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
  }

  private static void AllowAllOrigins(this IServiceCollection services) =>
    services.AddCors(
      options => options.AddPolicy(s_corsPolicy,
        policyBuilder => policyBuilder.WithOrigins("*")));

  private static void AllowAllOrigins(this IApplicationBuilder app) =>
    app.UseCors(s_corsPolicy);
}