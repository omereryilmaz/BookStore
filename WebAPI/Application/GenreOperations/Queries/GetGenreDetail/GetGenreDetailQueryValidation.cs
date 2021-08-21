using FluentValidation;

namespace WebAPI.Application.GenreOperations.Queries.GetGenreDetail
{
  public class GetGenreDetailQueryValidation : AbstractValidator<GenreDetailViewModel>
  {
    public GetGenreDetailQueryValidation()
    {
        RuleFor(query => query.Id).GreaterThan(0);
    }
  }
}