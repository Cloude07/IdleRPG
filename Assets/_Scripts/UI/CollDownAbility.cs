using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IdleRPG.UI
{
    public class CollDownAbility : MonoBehaviour
    {
        [SerializeField] private Image _abilityImage;
        [SerializeField] private float _CoolDown;
        [SerializeField] private UnityEvent _abilityStart;

        private bool isAbilityStarting = false;

        private void Update()
        {
            Colldowm();
        }

        private void Colldowm()
        {
            if (isAbilityStarting == true)
            {
                _abilityImage.fillAmount -= 1 / _CoolDown * Time.deltaTime;

                if (_abilityImage.fillAmount <= 0)
                {
                    isAbilityStarting = false;
                }
            }
        }

        public void StartCooldown()
        {
            if (isAbilityStarting == false)
            {
                isAbilityStarting = true;
                _abilityImage.fillAmount = 1;
                _abilityStart?.Invoke();
            }
        }
    }
}
