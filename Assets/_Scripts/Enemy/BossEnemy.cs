using UnityEngine;

namespace IdleRPG.Enemys
{
    public class BossEnemy : BaseEnemy
    {
        [SerializeField] private float _bossSpeed = 0.5f;

        private void Start()
        {
            _speed *= _bossSpeed;
            maxHealth = _health;
            isBoss = true;
        }

        public override void Moving()
        {
            base.Moving();
            isDead = false;
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
