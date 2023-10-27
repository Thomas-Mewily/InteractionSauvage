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

    public override string ToString()
    {
        return Categorie.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != typeof(Categories)) return false;
        if (((Categories)obj).Categorie == this.Categorie) return true;

        return false;
    }
}
