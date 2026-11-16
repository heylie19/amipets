// frontend/src/features/usuarios/types/usuario.types.ts

// Espejo exacto del UsuarioResponseDto del Backend en C#
export interface Usuario {
    id: number;
    nombre: string;
    email: string;
    rol: string;
    estado: string;
    verificado: boolean;
}