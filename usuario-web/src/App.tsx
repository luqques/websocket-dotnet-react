import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import connection from "./services/signalr"
import './App.css'

type Usuario = {
  id: number;
  nome: string;
  email: string;
};

function App() {
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);

  useEffect(() => {
    // Conecta ao hub
    connection
      .start()
      .then(() => {
        console.log("Conectado ao SignalR");

        // Escuta evento de alteração
        connection.on("UsuarioAlterado", (tipo: string, usuario: Usuario | number) => {
          console.log("Alteração:", tipo, usuario);

          setUsuarios((prev) => {
            if (tipo === "criado" && typeof usuario !== "number") {
              return [...prev, usuario];
            }

            if (tipo === "atualizado" && typeof usuario !== "number") {
              return prev.map(u => u.id === usuario.id ? usuario : u);
            }

            if (tipo === "removido" && typeof usuario === "number") {
              return prev.filter(u => u.id !== usuario);
            }

            return prev;
          });
        });
      })
      .catch((err) => console.error("Erro na conexão:", err));

    // Limpa o listener ao desmontar
    return () => {
      connection.off("UsuarioAlterado");
    };
  }, []);

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Usuários (em tempo real)</h1>
      <ul className="list-disc pl-4">
        {usuarios.map((usuario) => (
          <li key={usuario.id}>
            <strong>{usuario.nome}</strong> - {usuario.email}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App
