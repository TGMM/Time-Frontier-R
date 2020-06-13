using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class ButtonDelay : MonoBehaviour
    {
        public Button button;

        public void DelayButton()
        {
            StartCoroutine(nameof(Delay));
        }

        private IEnumerator Delay()
        {
            button.interactable = false;
            yield return new WaitForSeconds(3);
            button.interactable = true;
        }
    }
}
