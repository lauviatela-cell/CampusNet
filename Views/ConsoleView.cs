
using System;
using System.Collections.Generic;
using CampusNet.Models;

namespace CampusNet.Views
{
    public class ConsoleView
    {
        // Mostrar el encabezado del programa
        public void ShowHeader()
        {
            Console.WriteLine("================================");
            Console.WriteLine("       CAMPUSNET");
            Console.WriteLine("   RED SOCIAL ACADEMICA");
            Console.WriteLine("================================");
        }

        // Mostrar todos los usuarios y sus relaciones
        public void ShowAdjacencyList(Graph graph)
        {
            Console.WriteLine("\n========== LISTA DE ADYACENCIA ==========\n");

            Dictionary<string, Vertex> vertices = graph.GetVertices();

            Dictionary<string, List<string>> adjacency =
                graph.GetAdjacencyList();

            foreach (var vertex in vertices)
            {
                Console.WriteLine(
                    $"{vertex.Value.Id} - {vertex.Value.Name} ({vertex.Value.Role})"
                );

                List<string> connections = adjacency[vertex.Key];

                if (connections.Count == 0)
                {
                    Console.WriteLine("   Sigue a: Ninguno");
                }
                else
                {
                    Console.WriteLine(
                        $"   Sigue a: {string.Join(", ", connections)}"
                    );
                }

                Console.WriteLine();
            }
        }

        // Mostrar un mensaje general
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        // Mostrar el resultado de una operación
        public void ShowOperationResult(string operation, bool success)
        {
            if (success)
            {
                Console.WriteLine($"[CORRECTO] {operation}");
            }
            else
            {
                Console.WriteLine($"[ERROR] No se pudo realizar: {operation}");
            }
        }
    }
}
