using FluentValidation;

namespace WebAPI.Application.GenreOperations.Command.DeleteGenre
{
  public class DeleteGenreCommandValidation : AbstractValidator<DeleteGenreCommand>
  {
    public DeleteGenreCommandValidation()
    {
        RuleFor(command => command.GenreId).GreaterThan(0);
    }
  }
}


