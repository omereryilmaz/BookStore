using FluentValidation;

namespace WebAPI.BookOperations.GetBookDetail
{
  public class GetBookDetailQueryValidation : AbstractValidator<GetBookDetailQuery>
  {
    public GetBookDetailQueryValidation()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
  }
}