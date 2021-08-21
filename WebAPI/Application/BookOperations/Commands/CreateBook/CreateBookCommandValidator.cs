using System;
using FluentValidation;

namespace WebAPI.Application.BookOperations.Commands.CreateBook
{
  public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
  {
      public CreateBookCommandValidator()
      {
          // GenreId'nin 0 dan buyuk olmasini garanti eder
          RuleFor(command => command.Model.GenreId).GreaterThan(0);
          RuleFor(command => command.Model.PageCount).GreaterThan(0);
          RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
          RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
      }
  }
}