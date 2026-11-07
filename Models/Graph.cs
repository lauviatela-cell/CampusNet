
using System;
using System.Collections.Generic;

namespace CampusNet.Models
{
    public class Graph
    {
        private readonly Dictionary<string, Vertex> vertices;
        private readonly Dictionary<string, List<string>> adjacencyList;

        public Graph()
        {
            vertices = new Dictionary<string, Vertex>();
            adjacencyList = new Dictionary<string, List<string>>();
        }

        // CREAR: agregar un usuario
        public bool AddVertex(Vertex vertex)
        {
            if (vertex == null || string.IsNullOrWhiteSpace(vertex.Id))
                return false;

            if (vertices.ContainsKey(vertex.Id))
                return false;

            vertices.Add(vertex.Id, vertex);
            adjacencyList.Add(vertex.Id, new List<string>());

            return true;
        }

        // CREAR: agregar una relación dirigida
        public bool AddEdge(string originId, string destinationId)
        {
            if (!vertices.ContainsKey(originId) ||
                !vertices.ContainsKey(destinationId))
                return false;

            if (adjacencyList[originId].Contains(destinationId))
                return false;

            adjacencyList[originId].Add(destinationId);

            return true;
        }

        // CONSULTAR: obtener un usuario
        public Vertex? GetVertex(string id)
        {
            if (vertices.TryGetValue(id, out Vertex? vertex))
                return vertex;

            return null;
        }

        // CONSULTAR: obtener todos los usuarios
        public Dictionary<string, Vertex> GetVertices()
        {
            return vertices;
        }

        // CONSULTAR: obtener lista de adyacencia
        public Dictionary<string, List<string>> GetAdjacencyList()
        {
            return adjacencyList;
        }

        // ACTUALIZAR: modificar nombre y rol
        public bool UpdateVertex(string id, string newName, string newRole)
        {
            if (!vertices.ContainsKey(id))
                return false;

            if (string.IsNullOrWhiteSpace(newName) ||
                string.IsNullOrWhiteSpace(newRole))
                return false;

            vertices[id].Name = newName;
            vertices[id].Role = newRole;

            return true;
        }

        // ELIMINAR: usuario y sus relaciones
        public bool RemoveVertex(string id)
        {
            if (!vertices.ContainsKey(id))
                return false;

            vertices.Remove(id);
            adjacencyList.Remove(id);

            foreach (var connections in adjacencyList.Values)
            {
                connections.Remove(id);
            }

            return true;
        }

        // ELIMINAR: relación dirigida
        public bool RemoveEdge(string originId, string destinationId)
        {
            if (!adjacencyList.ContainsKey(originId))
                return false;

            return adjacencyList[originId].Remove(destinationId);
        }

        // RECORRIDO BFS
        public List<string> BFS(string startId)
        {
            List<string> visited = new List<string>();
            Queue<string> queue = new Queue<string>();

            if (!vertices.ContainsKey(startId))
                return visited;

            visited.Add(startId);
            queue.Enqueue(startId);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                foreach (string neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return visited;
        }

        // RECORRIDO DFS desde un usuario
        public List<string> DFS(string startId)
        {
            List<string> visited = new List<string>();

            if (!vertices.ContainsKey(startId))
                return visited;

            DFSRecursive(startId, visited);

            return visited;
        }

        private void DFSRecursive(string current, List<string> visited)
        {
            visited.Add(current);

            foreach (string neighbor in adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                    DFSRecursive(neighbor, visited);
            }
        }

        // DFS COMPLETO: visita todos los usuarios y detecta ciclos
        public List<string> DFSComplete(out bool hasCycle)
        {
            List<string> visitedOrder = new List<string>();

            HashSet<string> visited = new HashSet<string>();
            HashSet<string> recursionStack = new HashSet<string>();

            hasCycle = false;

            foreach (string vertexId in vertices.Keys)
            {
                if (!visited.Contains(vertexId))
                {
                    DFSCompleteRecursive(
                        vertexId,
                        visited,
                        recursionStack,
                        visitedOrder,
                        ref hasCycle
                    );
                }
            }

            return visitedOrder;
        }

        // Método auxiliar para DFS completo
        private void DFSCompleteRecursive(
            string current,
            HashSet<string> visited,
            HashSet<string> recursionStack,
            List<string> visitedOrder,
            ref bool hasCycle)
        {
            visited.Add(current);
            recursionStack.Add(current);
            visitedOrder.Add(current);

            foreach (string neighbor in adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    DFSCompleteRecursive(
                        neighbor,
                        visited,
                        recursionStack,
                        visitedOrder,
                        ref hasCycle
                    );
                }
                else if (recursionStack.Contains(neighbor))
                {
                    hasCycle = true;
                }
            }

            recursionStack.Remove(current);
        }

        // GRADO DE ENTRADA: cantidad de seguidores
        public int GetInDegree(string userId)
        {
            int count = 0;

            foreach (var connections in adjacencyList.Values)
            {
                if (connections.Contains(userId))
                    count++;
            }

            return count;
        }

        // GRADO DE SALIDA: cantidad de personas que sigue
        public int GetOutDegree(string userId)
        {
            if (!adjacencyList.ContainsKey(userId))
                return 0;

            return adjacencyList[userId].Count;
        }

        // USUARIOS SIN SEGUIDORES
        public List<Vertex> GetUsersWithoutFollowers()
        {
            List<Vertex> result = new List<Vertex>();

            foreach (var vertex in vertices.Values)
            {
                if (GetInDegree(vertex.Id) == 0)
                    result.Add(vertex);
            }

            return result;
        }

        // USUARIO MÁS INFLUYENTE
        public Vertex? GetMostInfluentialUser()
        {
            Vertex? mostInfluential = null;
            int highestDegree = -1;

            foreach (var vertex in vertices.Values)
            {
                int degree = GetInDegree(vertex.Id);

                if (degree > highestDegree)
                {
                    highestDegree = degree;
                    mostInfluential = vertex;
                }
            }

            return mostInfluential;
        }

        // USUARIO MÁS ACTIVO
        public Vertex? GetMostActiveUser()
        {
            Vertex? mostActive = null;
            int highestDegree = -1;

            foreach (var vertex in vertices.Values)
            {
                int degree = GetOutDegree(vertex.Id);

                if (degree > highestDegree)
                {
                    highestDegree = degree;
                    mostActive = vertex;
                }
            }

            return mostActive;
        }

        // CONSULTAR ALCANZABILIDAD
        public bool CanReach(string originId, string destinationId)
        {
            if (!vertices.ContainsKey(originId) ||
                !vertices.ContainsKey(destinationId))
                return false;

            List<string> reachableUsers = BFS(originId);

            return reachableUsers.Contains(destinationId);
        }
    }
}
