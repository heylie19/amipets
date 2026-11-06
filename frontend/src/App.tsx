// frontend/src/App.tsx
import './App.css'
import { UsuarioCard } from './components/UsuarioCard'

function App() {
  return (
    <main>
      <h1>Usuarios de Amipets</h1>
      <p>Explora los usuarios registrados en nuestra base de datos.</p>

      {/* Reutilización del componente cumpliendo estrictamente el contrato */}
      <section className="usuarios-container">

        <UsuarioCard id={1} nombre="Deysi Chaver" email="deysi@correo.com" rol="Cliente" estado="Activo" verificado={true} />
        <UsuarioCard id={2} nombre="Kristian Velasquez" email="kristian@correo.com" rol="Veterinario" estado="Activo" verificado={true} />
        <UsuarioCard id={3} nombre="Jair Velasquez" email="jair@correo.com" rol="Cliente" estado="Activo" verificado={false} />

      </section>
    </main>
  )
}

export default App