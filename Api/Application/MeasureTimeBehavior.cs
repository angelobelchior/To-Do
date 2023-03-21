using System.Diagnostics;

namespace ToDo.Api.Application;

public class MeasureTimeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>

{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var response = await next();
        
        stopwatch.Stop();    
        
        Console.WriteLine($"Request {typeof(TRequest).Name} took {stopwatch.ElapsedMilliseconds} ms");
        
        return response;
    }
}