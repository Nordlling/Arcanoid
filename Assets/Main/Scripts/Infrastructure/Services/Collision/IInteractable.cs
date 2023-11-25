namespace Main.Scripts.Infrastructure.Services.Collision
{
    public interface IInteractable
    {
        void Interact();
    }
    
    public interface ICollisionInteractable : IInteractable
    {
    }
    
    public interface ITriggerInteractable : IInteractable
    {
    }
}