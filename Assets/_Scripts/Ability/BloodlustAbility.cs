using IdleRPG.Interface;
using IdleRPG.PlayerLogic;
using UnityEngine;

namespace IdleRPG.Ability
{
    public class BloodlustAbility :MonoBehaviour, IAbility
    {
        public float CoolDown => _cooldown;

        [SerializeField] private float _cooldown = 4;
        [SerializeField] private Player _player;

        public void Speel()
        {
            _player.MultDamage(_cooldown);
            Debug.Log("Speel: BloodlustAbility");
        }
    }
}
