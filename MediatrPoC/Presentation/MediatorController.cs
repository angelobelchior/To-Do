namespace MediatrPoC.Presentation;

public abstract class MediatorController : ControllerBase
{
    private readonly IMediator _mediator;

    protected MediatorController(IMediator mediator)
        => _mediator = mediator;

    protected async Task<IActionResult> Send<T>(T request, CancellationToken cancellationToken)
        where T : class
    {
        try
        {
            var response = await _mediator.Send(request, cancellationToken);
            var result = response as Result;
            return result?.Status switch
            {
                ResultStatus.Success => Ok(response),
                ResultStatus.EntityNotFound => NotFound(response),
                ResultStatus.EntityAlreadyExists => Conflict(response),
                ResultStatus.HasValidation => BadRequest(response),
                ResultStatus.HasError => StatusCode(StatusCodes.Status500InternalServerError, response),
                ResultStatus.NoContent => NoContent(),
                _ => throw new InvalidCastException($"Cannot cast {response?.GetType().Name} to {nameof(Result)}"),
            };
        }
        catch (ValidationException validationException)
        {
            var validations = validationException.Errors.Select(e => new Validation(e.PropertyName, e.ErrorMessage));
            return BadRequest(Result.WithValidations(validations.ToArray()));
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.WithError(exception));
        }
    }
}