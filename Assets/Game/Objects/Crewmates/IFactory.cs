namespace Assets.Game.Objects.Crewmates
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
