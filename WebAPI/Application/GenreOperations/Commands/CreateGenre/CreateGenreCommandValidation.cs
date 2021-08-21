using FluentValidation;

namespace WebAPI.Application.GenreOperations.Command.CreateGenre
{
  public class CreateGenreCommandValidation : AbstractValidator<CreateGenreCommand>
  {
    public CreateGenreCommandValidation()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
    }
  }
}