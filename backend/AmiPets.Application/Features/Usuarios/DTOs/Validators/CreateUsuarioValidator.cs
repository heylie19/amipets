
using FluentValidation;
using AmiPets.Application.Features.Usuarios.DTOs;

namespace AmiPets.Application.Features.Usuarios.Validators;

// Reglas para validar el registro de usuarios
public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioRequestDto>
{
    public CreateUsuarioValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
    }
}
