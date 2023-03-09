using Assets.Game.Objects.Crewmates;

namespace Assets.Game.Objects.Rosters
{
    public interface IRoster : ISelectable<ICrewmate>
    {
        void Add(ICrewmate newCrewmate);
        ICrewmate Remove(int index);
        int Count();
        bool IsFull();
        bool IsEmpty();
    }
}
