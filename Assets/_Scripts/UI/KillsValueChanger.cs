using IdleRPG.PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

namespace IdleRPG.UI
{
    public class KillsValueChanger : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Text _txtCountKills;

        private void OnEnable()
        {
            _player.OnKillChange += PlayerOnKillChange;
        }

        private void PlayerOnKillChange(int value)
        {
            _txtCountKills.text = $"Убито: {value}";
        }

        private void OnDisable()
        {
            _player.OnKillChange -= PlayerOnKillChange;
        }
    }
}
