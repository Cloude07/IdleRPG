namespace IdleRPG.Interface
{
    public interface IDamagable
    {
        public float Health { get; }
        public bool IsDead { get; }
        public void ApplyDamage(float damage);
    }
}