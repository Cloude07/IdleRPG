using UnityEngine;
using IdleRPG.Interface;
using IdleRPG.Components;

namespace IdleRPG.Ability
{
    public class ChainLightningAbility : MonoBehaviour, IAbility
    {
        public float CoolDown => _cooldown;

        [SerializeField] private float _radius = 20f;
        [SerializeField] private int _damage = 7;
        [SerializeField] private float _cooldown = 5;
        private int countSpelAttack = 5;

        [SerializeField] private CheckingRadiusAttacking _checkingRadius;

        [SerializeField] private new Collider2D[] _collider;

        [ContextMenu("Speel")]
        public void Speel()
        {
            _checkingRadius.IsCheckingIsRadiusAtacking(_radius, out _collider);

            countSpelAttack = _collider.Length;
            if (countSpelAttack > 5) 
                countSpelAttack = 5;

            for (int i = 0; i < countSpelAttack; i++)
            {
                _collider[i].TryGetComponent<IDamagable>(out IDamagable damagable);
                damagable.ApplyDamage(_damage);
                Debug.Log("Speel: ChainLightningAbility");
            }
        }
    }
}
