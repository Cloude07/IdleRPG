using IdleRPG.PlayerLogic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdleRPG.UI
{
    public class DeadPlayerShowDisplay : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Text _txKill;
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            _player.OnKillChange += PlayerOnKillChange;
            _player.OnIsDeadChange += PlayerOnDeadShowDisplay;
        }

        private void PlayerOnKillChange(int value)
        {
            _txKill.text = $"Ты Умер!!!\n Убито: {value}";
        }

        private void PlayerOnDeadShowDisplay()
        {
            _animator.Play("IsDead");
            Invoke("Respawn", 2f);
        }

        private void Respawn()
        {
            SceneManager.LoadScene(0);
        }

        private void OnDisable()
        {
            _player.OnKillChange -= PlayerOnKillChange;
            _player.OnIsDeadChange -= PlayerOnDeadShowDisplay;
        }
    }
}
