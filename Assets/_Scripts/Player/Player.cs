using IdleRPG.Components;
using IdleRPG.Interface;
using System;
using System.Collections;
using UnityEngine;

namespace IdleRPG.PlayerLogic
{
    public class Player : MonoBehaviour, IAttacking, IDamagable
    {
        public float ForceAttack => _forceAttack;
        public float Rate => _rate;
        public float Health => _health;
        public bool IsDead => isDead;

        [Header("Dynamic")]
        [SerializeField] private float _health;
        [SerializeField] private float _forceAttack;
        [Range(0, 20)][SerializeField] private float _rate;
        [Range(0, 20)][SerializeField] private float _radius;

        [SerializeField] private CheckingRadiusAttacking _checkingRadius;

        private float defultForceAttack;
        private float maxHealth;
        private float timer;

        private bool isDead = false;
        private bool isMultTime = false;

        [SerializeField] private bool _enemyAtack = false;

        [SerializeField] private Collider2D _hit;

        private int kills = 0;
        private float mult;

        public event Action<float, float> OnHealthChange;
        public event Action<int> OnKillChange;
        public event Action OnIsDeadChange;

        private void Start()
        {
            maxHealth = _health;
            defultForceAttack = _forceAttack;
        }
        private void Update()
        {
            if(isDead) return;

            if (_enemyAtack == false)
            {
                _enemyAtack = Checking();
            }
            else if (_enemyAtack == true)
                Attack();

            if(isMultTime)
            {
                _health += mult;
                if(_health > maxHealth) _health = maxHealth;
            }
        }

        private bool Checking()
        {
            return _checkingRadius.IsCheckingIsRadiusAtacking(_radius, out _hit);
        }

        public void Attack()
        {
            _hit.TryGetComponent(out IDamagable damagable);

            mult= (damagable.Health * 0.05f);
            if (damagable.IsDead == true)
            {
                _enemyAtack = false;
                kills++;
                OnKillChange?.Invoke(kills);
                _hit = null;
                return;
            }

            timer += Time.deltaTime;
            if (timer >= _rate)
            {
                damagable.ApplyDamage(_forceAttack);
                timer = 0;
            }

        }

        public void ApplyDamage(float damage)
        {
            if (_health > 0)
            {
                _health -= damage;
                OnHealthChange?.Invoke(maxHealth, _health);
            }
            else
            {
                OnIsDeadChange?.Invoke();
            }
        }

        public void MultDamage(float coolDown)
        {
            _forceAttack = mult;
            StartCoroutine(StartMultDamageAbility(coolDown));
        }

        IEnumerator StartMultDamageAbility(float time)
        {
            isMultTime = true;
            yield return new WaitForSeconds(time);
            _forceAttack = defultForceAttack;
            isMultTime = false;
        }
    }
}
