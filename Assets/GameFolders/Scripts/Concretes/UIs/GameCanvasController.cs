using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Space.Abstract.Entity;

namespace Space.UIs
{
    public class GameCanvasController : SingletonMonoBehaviourObject<GameCanvasController>
    {
        [SerializeField] Image[] _bulletLevelImages;
        [SerializeField] TMP_Text _currentScore;
        [SerializeField] GameObject _losePanel;

        private void Awake()
        {
            SingletonThisObject(this);
        }
        
        public void SetBulletLevel()
        {
    
            for (int i = 0; i < GameManager.Instance.BulletLvl; i++)
            {
                _bulletLevelImages[i].color = Color.white;
            }


        }
        public void SetScore()
        {
            _currentScore.text = GameManager.Instance.Score.ToString();
        }

        public void LosePanel()
        {
            _losePanel.SetActive(true);

        }
        public void ReturnMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }

}
