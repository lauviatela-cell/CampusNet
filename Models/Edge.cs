
namespace CampusNet.Models
{
    public class Edge
    {
        // Usuario que inicia la relación
        public string OriginId { get; set; }

        // Usuario al que sigue
        public string DestinationId { get; set; }

        // Constructor
        public Edge(string originId, string destinationId)
        {
            OriginId = originId;
            DestinationId = destinationId;
        }

        // Información de la relación
        public override string ToString()
        {
            return $"{OriginId} -> {DestinationId}";
        }
    }
}
