
using System.Collections.Generic;
using CampusNet.Models;
using CampusNet.Views;

namespace CampusNet.Controllers
{
    public class GraphController
    {
        private readonly Graph graph;
        private readonly ConsoleView view;

        public GraphController()
        {
            graph = new Graph();
            view = new ConsoleView();
        }

        public void Run()
        {
            view.ShowHeader();

            BuildInitialGraph();

            view.ShowMessage("\nGrafo inicial construido correctamente.");
            view.ShowAdjacencyList(graph);

            ShowTraversals();
            ShowSocialQueries();
            ShowReachability();
            TestCRUDOperations();
        }

        // Construir el grafo inicial
        private void BuildInitialGraph()
        {
            graph.AddVertex(new Vertex("U001", "Ana", "Estudiante"));
            graph.AddVertex(new Vertex("U002", "Carlos", "Profesor"));
            graph.AddVertex(new Vertex("U003", "Laura", "Estudiante"));
            graph.AddVertex(new Vertex("U004", "Andres", "Egresado"));
            graph.AddVertex(new Vertex("U005", "Maria", "Estudiante"));
            graph.AddVertex(new Vertex("U006", "Pedro", "Profesor"));
            graph.AddVertex(new Vertex("U007", "Sofia", "Estudiante"));
            graph.AddVertex(new Vertex("U008", "Daniel", "Egresado"));
            graph.AddVertex(new Vertex("U009", "Camila", "Profesor"));
            graph.AddVertex(new Vertex("U010", "Juan", "Estudiante"));
            graph.AddVertex(new Vertex("U011", "Valentina", "Egresado"));
            graph.AddVertex(new Vertex("U012", "Diego", "Estudiante"));

            graph.AddEdge("U001", "U002");
            graph.AddEdge("U001", "U003");
            graph.AddEdge("U001", "U004");
            graph.AddEdge("U001", "U005");

            graph.AddEdge("U002", "U003");
            graph.AddEdge("U002", "U006");
            graph.AddEdge("U002", "U007");
            graph.AddEdge("U002", "U008");

            graph.AddEdge("U003", "U001");
            graph.AddEdge("U003", "U009");

            graph.AddEdge("U004", "U009");
            graph.AddEdge("U005", "U009");
            graph.AddEdge("U006", "U010");
            graph.AddEdge("U007", "U010");
            graph.AddEdge("U008", "U010");
            graph.AddEdge("U009", "U004");

            graph.AddEdge("U011", "U001");
            graph.AddEdge("U012", "U002");
        }

        // Mostrar recorridos BFS y DFS
        private void ShowTraversals()
        {
            view.ShowMessage("\n========== RECORRIDOS BFS ==========");

            // BFS desde tres usuarios diferentes
            string[] startingUsers = { "U001", "U004", "U006" };

            foreach (string userId in startingUsers)
            {
                List<string> result = graph.BFS(userId);

                view.ShowMessage(
                    $"\nBFS desde {userId}: {string.Join(" -> ", result)}"
                );

                view.ShowMessage(
                    $"Usuarios alcanzados: {result.Count}"
                );
            }

            // DFS completo y detección de ciclos
            view.ShowMessage("\n========== RECORRIDO DFS COMPLETO ==========");

            bool hasCycle;

            List<string> dfsResult = graph.DFSComplete(out hasCycle);

            view.ShowMessage(
                $"DFS completo: {string.Join(" -> ", dfsResult)}"
            );

            view.ShowMessage(
                $"Usuarios visitados: {dfsResult.Count}"
            );

            // Mostrar resultado de la detección de ciclos
            view.ShowMessage("\n========== DETECCION DE CICLOS ==========");

            if (hasCycle)
            {
                view.ShowMessage(
                    "El grafo contiene ciclos dirigidos."
                );
            }
            else
            {
                view.ShowMessage(
                    "No se encontraron ciclos dirigidos."
                );
            }
        }

        // Mostrar consultas sociales
        private void ShowSocialQueries()
        {
            view.ShowMessage("\n========== CONSULTAS SOCIALES ==========");

            view.ShowMessage("\n1. USUARIOS SIN SEGUIDORES:");

            List<Vertex> usersWithoutFollowers =
                graph.GetUsersWithoutFollowers();

            foreach (Vertex user in usersWithoutFollowers)
            {
                view.ShowMessage(
                    $"{user.Id} - {user.Name} ({user.Role})"
                );
            }

            view.ShowMessage("\n2. USUARIO MAS INFLUYENTE:");

            Vertex? influentialUser = graph.GetMostInfluentialUser();

            if (influentialUser != null)
            {
                view.ShowMessage(
                    $"{influentialUser.Id} - {influentialUser.Name}"
                );

                view.ShowMessage(
                    $"Cantidad de seguidores: {graph.GetInDegree(influentialUser.Id)}"
                );
            }

            view.ShowMessage("\n3. USUARIO MAS ACTIVO:");

            Vertex? activeUser = graph.GetMostActiveUser();

            if (activeUser != null)
            {
                view.ShowMessage(
                    $"{activeUser.Id} - {activeUser.Name}"
                );

                view.ShowMessage(
                    $"Personas que sigue: {graph.GetOutDegree(activeUser.Id)}"
                );
            }
        }

        // Consulta de alcanzabilidad
        private void ShowReachability()
        {
            view.ShowMessage("\n========== CONSULTA DE ALCANZABILIDAD ==========");

            string origin = "U001";
            string destination = "U010";

            bool reachable = graph.CanReach(origin, destination);

            if (reachable)
            {
                view.ShowMessage(
                    $"Existe un camino desde {origin} hasta {destination}."
                );
            }
            else
            {
                view.ShowMessage(
                    $"No existe un camino desde {origin} hasta {destination}."
                );
            }
        }

        // Pruebas de operaciones CRUD
        private void TestCRUDOperations()
        {
            view.ShowMessage("\n========== PRUEBAS CRUD ==========");

            // 1. Crear usuario
            view.ShowMessage("\n1. AGREGAR USUARIO:");

            bool added = graph.AddVertex(
                new Vertex("U013", "Isabella", "Estudiante")
            );

            view.ShowOperationResult("Agregar usuario U013", added);

            // 2. Agregar relación
            view.ShowMessage("\n2. AGREGAR RELACION:");

            bool edgeAdded = graph.AddEdge("U013", "U001");

            view.ShowOperationResult(
                "Agregar relacion U013 -> U001",
                edgeAdded
            );

            // 3. Actualizar usuario
            view.ShowMessage("\n3. ACTUALIZAR USUARIO:");

            bool updated = graph.UpdateVertex(
                "U013",
                "Isabella Torres",
                "Egresado"
            );

            view.ShowOperationResult("Actualizar usuario U013", updated);

            // 4. Eliminar relación
            view.ShowMessage("\n4. ELIMINAR RELACION:");

            bool edgeRemoved = graph.RemoveEdge("U013", "U001");

            view.ShowOperationResult(
                "Eliminar relacion U013 -> U001",
                edgeRemoved
            );

            // 5. Eliminar usuario
            view.ShowMessage("\n5. ELIMINAR USUARIO:");

            bool removed = graph.RemoveVertex("U013");

            view.ShowOperationResult("Eliminar usuario U013", removed);

            // Mostrar el grafo después de las operaciones
            view.ShowMessage("\nGRAFO DESPUES DE LAS PRUEBAS CRUD:");
            view.ShowAdjacencyList(graph);
        }
    }
}
