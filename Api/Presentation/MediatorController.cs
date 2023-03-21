namespace ToDo.Api.Presentation;

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
            if (response is not Contracts.IResult result)
                throw new InvalidCastException($"Cannot cast {response?.GetType().Name} to {nameof(Contracts.IResult)}");

            return result.Status switch
            {
                ResultStatus.EntityNotFound => NotFound(result),
                ResultStatus.EntityAlreadyExists => Conflict(result),
                ResultStatus.HasValidation => BadRequest(result),
                ResultStatus.HasError => StatusCode(StatusCodes.Status500InternalServerError, result),
                ResultStatus.NoContent => NoContent(),
                _ => Ok(result)
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