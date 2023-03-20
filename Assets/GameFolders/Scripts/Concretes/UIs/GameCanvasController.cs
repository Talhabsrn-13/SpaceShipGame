using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Space.UIs
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] Image[] _bulletLevelImages;
        [SerializeField] TMP_Text _currentScore;

        public void SetBulletLevel()
        {
            Debug.Log(GameManager.Instance.BulletLvl);
        
            for (int i = 0; i < GameManager.Instance.BulletLvl; i++)
            {
                Debug.Log(_bulletLevelImages[i]);
                _bulletLevelImages[i].color = Color.white;
            }
        }
        public void SetScore()
        {
            _currentScore.text = GameManager.Instance.Score.ToString();
        }
    }

}
