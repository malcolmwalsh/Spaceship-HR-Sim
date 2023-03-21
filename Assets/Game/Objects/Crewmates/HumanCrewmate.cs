namespace Assets.Game.Objects.Crewmates
{
    public class HumanCrewmate : Crewmate, IPrey
    {
        public void Die()
        {
            Destroy(this.gameObject);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
