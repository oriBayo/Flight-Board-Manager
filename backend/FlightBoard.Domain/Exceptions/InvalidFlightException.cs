namespace FlightBoard.Domain.Exceptions;

public class ValidationError
{
    public string PropertyName { get; set; } = default!;
    public string ErrorMessage { get; set; } = default!;
}
public class InvalidFlightException : Exception
{
    public List<ValidationError> Errors { get; }
    public InvalidFlightException(IEnumerable<ValidationError> errors) : base("Validation failed")
    {
        Errors = errors.ToList();
    }
}