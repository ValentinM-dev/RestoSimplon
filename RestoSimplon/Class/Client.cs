using Swashbuckle.AspNetCore.Annotations;

namespace RestoSimplon.Class
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set;}
        public required string Prenom { get; set; }
        public required string Adress { get; set; }
        public required string PhoneNumber { get; set; }
        
    }

    public class ClientDTO
    {
        [SwaggerSchema("Identifiant unique de l'article")]
        public int Id { get; set; }

        [SwaggerSchema("Nom du client")]
        public required string Name { get; set; }

        [SwaggerSchema("Prénom du client")]
        public required string Prenom { get; set; }

        [SwaggerSchema("Adresse du client")]
        public required string Adress { get; set; }

        [SwaggerSchema("Numéro de Téléphone du client")]
        public required string PhoneNumber { get; set; }

        public ClientDTO() { }

        public ClientDTO(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            Prenom = client.Prenom;
            Adress = client.Adress;
            PhoneNumber = client.PhoneNumber;
        }
    }
}
