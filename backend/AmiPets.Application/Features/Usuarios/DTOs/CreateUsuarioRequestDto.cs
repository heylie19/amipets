
namespace AmiPets.Application.Features.Usuarios.DTOs;

// Datos permitidos para crear un usuario
public class CreateUsuarioRequestDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
