using System.Collections.Generic;

namespace Assets.Game.Objects.Crewmates
{
    public interface IHunter
    {
        ISet<IPrey> GoHunting();
        void Escape();
    }
}
