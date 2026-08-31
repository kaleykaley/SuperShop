namespace SuperShop.Data.Entities
{
    // generic interface for all entities in the database,
    // like Product, Client, etc
    public interface IEntity
    {
        int Id { get; set;  } 

        // for soft delete
        // bool WasDeleted { get; set; }

    }
}
