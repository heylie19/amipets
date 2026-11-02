
namespace AmiPets.Application.Features.Usuarios.DTOs;

// Datos del usuario que la API puede devolver al frontend
public class UsuarioResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public bool Verificado { get; set; }
}
