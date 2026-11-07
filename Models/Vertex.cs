
namespace CampusNet.Models
{
    public class Vertex
    {
        // Identificador único del usuario
        public string Id { get; set; }

        // Nombre del usuario
        public string Name { get; set; }

        // Rol académico: estudiante, profesor o egresado
        public string Role { get; set; }

        // Constructor
        public Vertex(string id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        // Información del usuario
        public override string ToString()
        {
            return $"{Id} - {Name} ({Role})";
        }
    }
}
