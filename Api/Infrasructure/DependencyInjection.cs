namespace ToDo.Api.Infrasructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITodosRepository, TodosRepository>();
        return services;
    }
}
