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

            if (result.Status == ResultStatus.EntityNotFound) return NotFound(result);
            if (result.Status == ResultStatus.EntityAlreadyExists) return Conflict(result);
            if (result.Status == ResultStatus.HasValidation) return BadRequest(result);
            if (result.Status == ResultStatus.HasError) return  StatusCode(StatusCodes.Status500InternalServerError, result); 
            if (result.Status == ResultStatus.NoContent) return NoContent();

            return Ok(result);
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