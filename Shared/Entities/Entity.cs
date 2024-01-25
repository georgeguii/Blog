namespace Blog.Shared.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}
