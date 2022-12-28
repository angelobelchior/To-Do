namespace MediatrPoC.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(assembly);
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
