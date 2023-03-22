using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Space.UIs
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TMP_Text _timerText;
        float timer = 0f;
        void Start()
        {
            timer = 0f;
        }

      
        void Update()
        {
            if (!GameManager.Instance.Playability()) return;
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);


            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            _timerText.text = timerString;
        }
    }

}
