using FluentValidation;

namespace WebAPI.Application.GenreOperations.Command.UpdateGenre
{
  public class UpdateGenreCommandValidation : AbstractValidator<UpdateGenreCommand>
  {
    public UpdateGenreCommandValidation()
    {
        RuleFor(command => command.Model.Name)
                  .NotEmpty()
                  .MinimumLength(3)
                  .When(x => x.Model.Name.Trim() != string.Empty);
    }
  }
}