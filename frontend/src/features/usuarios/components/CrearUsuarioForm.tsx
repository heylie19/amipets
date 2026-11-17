// frontend/src/features/usuarios/components/CrearUsuarioForm.tsx
import { useState, type ChangeEvent, type FormEvent } from 'react'
import type { Usuario, CrearUsuarioDto } from '../types/usuario.types'
import { crearUsuarioAsync, extraerMensajeErrorBackend } from '../api/usuariosApi'

interface CrearUsuarioFormProps {
    onUsuarioCreado: (nuevoUsuario: Usuario) => void;
    onCancelar: () => void;
}

export function CrearUsuarioForm({ onUsuarioCreado, onCancelar }: CrearUsuarioFormProps) {
    const [formulario, setFormulario] = useState<CrearUsuarioDto>({ nombre: '', email: '', password: '' })
    const [guardando, setGuardando] = useState<boolean>(false)
    const [errorServidor, setErrorServidor] = useState<string | null>(null)

    // Actualización inmutable: copiamos el objeto anterior con spread y reemplazamos solo el campo editado
    const manejarCambio = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target
        setFormulario((anterior) => ({ ...anterior, [name]: value }))
    }

    const manejarEnvio = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault() // Evita el F5 nativo del navegador

        try {
            setGuardando(true)
            setErrorServidor(null)

            // ⚠ FASE 1: enviamos el dato crudo sin validar en el Frontend para poner a prueba el Backend
            const usuarioCreado = await crearUsuarioAsync(formulario)

            setFormulario({ nombre: '', email: '', password: '' })
            onUsuarioCreado(usuarioCreado)
        } catch (error) {
            // Extraemos los mensajes exactos del arreglo ValidationFailure[] de .NET
            const mensajeRealDeNet = extraerMensajeErrorBackend(error)
            console.error('Auditoría del rechazo de .NET:', error)
            setErrorServidor(mensajeRealDeNet)
        } finally {
            setGuardando(false)
        }
    }

    return (
        <form onSubmit={manejarEnvio} className="bg-slate-800/90 border border-indigo-500/40 rounded-xl p-5 mb-8 shadow-lg">
            <div className="flex items-center justify-between mb-4">
                <h2 className="text-base font-bold text-white">✨ Registrar Nuevo Usuario</h2>
                <span className="text-xs font-mono text-slate-400 bg-slate-900 px-2 py-1 rounded border border-slate-700">
                    POST /api/usuarios
                </span>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-3 mb-3">
                <input
                    type="text"
                    name="nombre"
                    placeholder="Nombre"
                    value={formulario.nombre}
                    onChange={manejarCambio}
                    disabled={guardando}
                    className="bg-slate-950 border border-slate-700 rounded-lg px-4 py-2.5 text-sm text-white"
                />
                <input
                    type="text"
                    name="email"
                    placeholder="Correo electrónico"
                    value={formulario.email}
                    onChange={manejarCambio}
                    disabled={guardando}
                    className="bg-slate-950 border border-slate-700 rounded-lg px-4 py-2.5 text-sm text-white"
                />
                <input
                    type="password"
                    name="password"
                    placeholder="Contraseña"
                    value={formulario.password}
                    onChange={manejarCambio}
                    disabled={guardando}
                    className="bg-slate-950 border border-slate-700 rounded-lg px-4 py-2.5 text-sm text-white"
                />
            </div>

            <div className="flex gap-3">
                <button type="submit" disabled={guardando} className="bg-indigo-600 hover:bg-indigo-500 text-white font-medium text-sm px-5 py-2.5 rounded-lg cursor-pointer">
                    {guardando ? 'Enviando...' : 'Guardar en PostgreSQL'}
                </button>
                <button type="button" onClick={onCancelar} className="bg-slate-700 text-slate-200 text-sm px-4 py-2.5 rounded-lg cursor-pointer">
                    Cancelar
                </button>
            </div>

            {/* Banner que muestra literalmente las reglas de FluentValidation que fallaron en .NET */}
            {errorServidor && (
                <div className="mt-4 bg-red-950/60 border border-red-500/40 rounded-lg p-3 text-xs text-red-300 flex items-center gap-2">
                    <span className="text-base">🛡️</span>
                    <span><strong>Rechazo del Servidor (.NET FluentValidation):</strong> {errorServidor}</span>
                </div>
            )}
        </form>
    )
}