
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using AmiPets.Application.Interfaces;
using AmiPets.Domain.Entities;
using AmiPets.Application.Features.Usuarios.DTOs;

namespace AmiPets.API.Controllers;

// Controlador para gestionar usuarios
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioRepository _repository;
    private readonly IValidator<CreateUsuarioRequestDto> _validator;

    public UsuariosController(
        IUsuarioRepository repository,
        IValidator<CreateUsuarioRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    // Obtener usuarios y devolver únicamente los datos permitidos
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _repository.GetAllAsync();

        var response = usuarios.Select(u => new UsuarioResponseDto
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Email = u.Email,
            Rol = u.Rol.ToString(),
            Estado = u.Estado.ToString(),
            Verificado = u.Verificado
        });

        return Ok(response);
    }

    // Crear un usuario utilizando el DTO de entrada
    [HttpPost]
    public async Task<IActionResult> CrearUsuario(
        [FromBody] CreateUsuarioRequestDto request)
    {
        // Validar los datos recibidos
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // Convertir el DTO en una entidad Usuario
        var nuevoUsuario = new Usuario
        {
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = request.Password
        };

        // Guardar el usuario en la base de datos
        var usuarioCreado = await _repository.AddAsync(nuevoUsuario);

        // Convertir la entidad en DTO para evitar devolver PasswordHash
        var response = new UsuarioResponseDto
        {
            Id = usuarioCreado.Id,
            Nombre = usuarioCreado.Nombre,
            Email = usuarioCreado.Email,
            Rol = usuarioCreado.Rol.ToString(),
            Estado = usuarioCreado.Estado.ToString(),
            Verificado = usuarioCreado.Verificado
        };

        return StatusCode(201, response);
    }
}
