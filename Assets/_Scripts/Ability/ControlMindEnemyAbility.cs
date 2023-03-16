using IdleRPG.Components;
using IdleRPG.Enemys;
using IdleRPG.Interface;
using UnityEngine;

namespace IdleRPG.Ability
{
    public class ControlMindEnemyAbility : MonoBehaviour, IAbility
    {
        public float CoolDown => _cooldown;

        [SerializeField] private float _radius = 30;
        [SerializeField] private float _cooldown = 5;

        [SerializeField] private CheckingRadiusAttacking _checkingRadius;

        [SerializeField] private new Collider2D[] _collider;

        [ContextMenu("Speel")]
        public void Speel()
        {
            _checkingRadius.IsCheckingIsRadiusAtacking(_radius, out _collider);
            for (int i = 0; i < _collider.Length; i++)
            {
                _collider[i].TryGetComponent<BaseEnemy>(out BaseEnemy enemy);
                if (enemy.isBoss) continue;

                enemy.AttackEnemy(_cooldown);
                Debug.Log("Speel: ControlMindEnemyAbility");
                return;
            }

        }
    }
}
