namespace MediatrPoC.Controllers;

public abstract class MediatorController : ControllerBase
{
    private readonly ILogger<TodosController> _logger;
    private readonly IMediator _mediator;

    public MediatorController(ILogger<TodosController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    protected async Task<IActionResult> Send<T>(T request, CancellationToken cancellationToken)
        where T : class
    {
        try
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new
            {
                Data = response
            });
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new
            {
                Validations = validationException.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                })
            });
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Error = exception.Message
            });
        }
    }
}