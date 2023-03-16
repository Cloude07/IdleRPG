namespace IdleRPG.Interface
{
    public interface IAttacking
    {
        public float ForceAttack { get; }
        public float Rate { get; }
        public void Attack();
    }
}