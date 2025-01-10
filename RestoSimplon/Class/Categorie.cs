using Swashbuckle.AspNetCore.Annotations;

namespace RestoSimplon.Class
{
    public class Categorie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }

    public class CategorieDTO
    {
        [SwaggerSchema("Identifiant unique de l'article")]
        public int Id { get; set; }

        [SwaggerSchema("Nom de la catégorie")]
        public required string Name { get; set; }

        public CategorieDTO() { }

        public CategorieDTO(Categorie categorie)
        {
            Id = categorie.Id;
            Name = categorie.Name;
        }
    }
}
