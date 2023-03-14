namespace Assets.Game.Objects
{
    public interface ISelectable<out T>
    {
        T ChooseOne();
    }
}
