using IdleRPG.Components;
using IdleRPG.Interface;
using UnityEngine;

namespace IdleRPG.Ability
{
    public class MeteoritAbility : MonoBehaviour, IAbility
    {
        public float CoolDown => _cooldown;

        [SerializeField] private float _radius = 30;
        [SerializeField] private int _damage = 100;
        [SerializeField] private float _cooldown = 5;
        private int multSpelAttack = 10;

        [SerializeField] private CheckingRadiusAttacking _checkingRadius;

        [SerializeField] private new Collider2D[] _collider;

        [ContextMenu("Speel")]
        public void Speel()
        {
            _checkingRadius.IsCheckingIsRadiusAtacking(_radius, out _collider);

            multSpelAttack *= _collider.Length;
            _damage += multSpelAttack;

            for (int i = 0; i < _collider.Length; i++)
            {
                _collider[i].TryGetComponent<IDamagable>(out IDamagable damagable);
                damagable.ApplyDamage(_damage);
                Debug.Log("Speel: MeteoritAbility");
            }
        }
    }
}
