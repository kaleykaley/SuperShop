namespace SuperShop.Data.Entities
{
    // generic interface for all entities in the database,
    // like Product, Client, etc
    public interface IEntity
    {
        int Id { get; set;  }

        // for soft delete
        // bool WasDeleted { get; set; }

        // not good because not all entities have a name, like encomenda
        //string Name { get; set; }

    }
}
