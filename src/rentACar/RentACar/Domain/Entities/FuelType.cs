using Core.Persistence.Repositories;

namespace Domain.Entities;

public class FuelType : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<Model> Models { get; set; }

    public FuelType()
    {
        Models = new HashSet<Model>();
    }
    public FuelType(Guid id,string name):this()
    {
        Id = id;
        Name = name;
    }

}
