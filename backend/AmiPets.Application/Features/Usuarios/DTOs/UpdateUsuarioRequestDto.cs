/* Solo permite enviar el nombre: ni Id, 
ni Email, ni Rol, ni PasswordHash pueden modificarse desde este endpoint (evita over-posting): */


namespace AmiPets.Application.Features.Usuarios.DTOs;

public class UpdateUsuarioRequestDto
{
    public string Nombre { get; set; } = string.Empty;
}