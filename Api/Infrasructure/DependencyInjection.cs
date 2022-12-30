namespace ToDo.Api.Infrasructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IToDosReadRepository, ToDosRepository>();
        services.AddSingleton<IToDosWriteRepository, ToDosRepository>();
    }
}
