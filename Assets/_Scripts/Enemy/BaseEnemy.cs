using IdleRPG.Components;
using IdleRPG.Interface;
using System;
using System.Collections;
using UnityEngine;

namespace IdleRPG.Enemys
{
    public abstract class BaseEnemy : MonoBehaviour, IMoveable, IAttacking, IDamagable
    {
        private const int PLAYER_INDEX_LAYERMASK = 6;
        private const int ENEMY_INDEX_LAYERMASK = 7;

        public float Speed => _speed;
        public float ForceAttack => _forceAttack;
        public float Rate => _rate;
        public float Health => _health;
        public bool IsDead => isDead;
        public bool isBoss = false;

        [Header("Dynamic")]
        [SerializeField] protected float _health;
        [SerializeField] protected float _forceAttack = 2;
        [Range(0, 20)][SerializeField] protected float _speed = 0.5f;
        [Range(0, 20)][SerializeField] protected float _rate = 1f;
        [Range(0, 20)][SerializeField] protected float _radius;

        [SerializeField] protected Transform _transformHeathBar;

        [SerializeField] protected CheckingRadiusAttacking _checkingRadius;

        protected float maxHealth;
        private float timer;

        private Collider2D hit = new Collider2D();

        protected bool isDead = false;
        private void Start()
        {
            maxHealth = _health;
        }

        private void Update()
        {
            Moving();
        }

        public virtual void Attack()
        {
            _checkingRadius.IsCheckingIsRadiusAtacking(_radius, out hit);
            hit.TryGetComponent(out IDamagable damagable);

            timer += Time.deltaTime;
            if (timer >= _rate)
            {
                damagable.ApplyDamage(_forceAttack);
                timer = 0;
            }
        }


        public virtual void Moving()
        {
            isDead = false;
            if (_checkingRadius.IsCheckingIsRadiusAtacking(_radius, out hit))
            {
                Attack();
            }
            else
                transform.position = Vector2.MoveTowards(this.transform.position, Vector2.zero, _speed * Time.deltaTime);
        }

        public virtual void ApplyDamage(float damage)
        {
            if (_health <= 0)
            {
                isDead = true;
                transform.position = Vector2.left * 50;

                _health = maxHealth;
                this.gameObject.SetActive(false);
            }
            else
            {
                _health -= damage;
                isDead = false;
            }
        }

        public virtual void AttackEnemy(float coolDown)
        {
            StartCoroutine(StartMultDamageAbility(coolDown));
        }

        IEnumerator StartMultDamageAbility(float time)
        {
            _checkingRadius.layerMask.value = ENEMY_INDEX_LAYERMASK;
            this.gameObject.layer = PLAYER_INDEX_LAYERMASK;

            yield return new WaitForSeconds(time);

            _checkingRadius.layerMask.value = PLAYER_INDEX_LAYERMASK;
            this.gameObject.layer = ENEMY_INDEX_LAYERMASK;
        }
    }
}
