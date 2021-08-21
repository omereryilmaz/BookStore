using FluentValidation;

namespace WebAPI.Application.GenreOperations.Queries.GetGenreDetail
{
  public class GetGenreDetailQueryValidation : AbstractValidator<GetGenreDetailQuery>
  {
    public GetGenreDetailQueryValidation()
    {
        RuleFor(query => query.GenreId).GreaterThan(0);
    }
  }
}