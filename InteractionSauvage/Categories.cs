namespace InteractionSauvage;

public class Categories
{
    public enum CategorieEnum
    {
        Plante,
        Herbivore,
        Carnivore,
        Homnivore,
    }

    public CategorieEnum Categorie;
    public List<CategorieEnum>  CategoriesNouritures;

    public Categories(CategorieEnum categorie) 
    { 
        Categorie = categorie;
        CategoriesNouritures = new List<CategorieEnum>();
    }

    public override string ToString() => Categorie.ToString();

    public static bool operator ==(Categories a, Categories b) => a.Categorie == b.Categorie;
    public static bool operator !=(Categories a, Categories b) => a.Categorie != b.Categorie;

    public override int GetHashCode() => (int)Categorie;
    public override bool Equals(object? obj) => obj != null && obj is Categories i && i == this;
}
