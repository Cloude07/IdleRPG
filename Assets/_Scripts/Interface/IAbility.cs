namespace IdleRPG.Interface
{
    public interface IAbility 
    {
        public float CoolDown { get; }
        public void Speel();
    }
}
