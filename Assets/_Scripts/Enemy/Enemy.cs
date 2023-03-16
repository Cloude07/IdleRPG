namespace IdleRPG.Enemys
{
    public class Enemy : BaseEnemy
    {
        public override void Moving()
        {
            base.Moving();
            isDead = false;
            isBoss = false;
        }

        public override void Attack()
        {
            base.Attack();
        }

        public override void ApplyDamage(float damage)
        {
            base.ApplyDamage(damage);
        }

    }
}
