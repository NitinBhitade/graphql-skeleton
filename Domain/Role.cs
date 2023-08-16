using Domain.Entities.Base;

namespace Domain
{
    
    public class Role : Entity
    {
        public virtual string Name { get; set; } = null!;

    }
}