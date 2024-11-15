using Thunder.Project.Domain.Entities;
using FluentValidation;

namespace Thunder.Project.Domain.Validators
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(entity => entity.Title)
                .NotEmpty().WithMessage("O título da tarefa deve ser informado")
            ;

            RuleFor(entity => entity.Description)
                .NotEmpty().WithMessage("A descrição da tarefa deve ser informada")
            ;

        }
    }
}
