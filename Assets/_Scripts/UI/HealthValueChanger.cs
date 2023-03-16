using IdleRPG.PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

namespace IdleRPG.UI
{
    public class HealthValueChanger : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Slider _slider;

        private void OnEnable()
        {
            _player.OnHealthChange += Player_OnHealthChange;
        }

        private void Player_OnHealthChange(float maxHealth, float currentHealth)
        {
            _slider.value = currentHealth / maxHealth;
        }

        private void OnDisable()
        {
            _player.OnHealthChange -= Player_OnHealthChange;
        }
    }
}