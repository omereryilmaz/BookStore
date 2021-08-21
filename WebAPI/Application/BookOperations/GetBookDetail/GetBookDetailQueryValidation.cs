using FluentValidation;

namespace WebAPI.Application.BookOperations.GetBookDetail
{
  public class GetBookDetailQueryValidation : AbstractValidator<GetBookDetailQuery>
  {
    public GetBookDetailQueryValidation()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
  }
}