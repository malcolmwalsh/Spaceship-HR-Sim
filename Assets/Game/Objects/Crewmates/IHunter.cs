using System.Collections.Generic;
using Assets.Game.Objects.Hunts;

namespace Assets.Game.Objects.Crewmates
{
    public interface IHunter
    {
        Hunt Hunt { set; }

        IList<IPrey> GoHunting();
        void Escape();
    }
}
