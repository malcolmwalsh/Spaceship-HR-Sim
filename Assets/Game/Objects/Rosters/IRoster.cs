using Assets.Game.Objects.Crewmates;

namespace Assets.Game.Objects.Rosters
{
    public interface IRoster : ISelectable<HumanCrewmate>
    {
        void Add(HumanCrewmate newCrewmate);
        //ICrewmate Remove(int index);
        int Count();
        bool IsFull();
        bool IsEmpty();
        void Remove(HumanCrewmate crewmate);
    }
}
