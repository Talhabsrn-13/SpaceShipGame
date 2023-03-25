using Space.Abstract.Entity;
using Space.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Space.UIs
{
    public class MenuScript : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button _exitButton;
        [SerializeField] Button _mapButton;
        [SerializeField] Button _startButton;
        [SerializeField] Button _currentUpgradeOrBuyButton;
        [SerializeField] Button _selectShipButton;

        [Header("MenuPanel")]
        [SerializeField] Image _menuShipImage;

        [Header("Shop")]
        [SerializeField] Image _currentShipImage;
        [SerializeField] TMP_Text _currentTitle;
        [SerializeField] TMP_Text _currentLevel;
        [SerializeField] TMP_Text _currentDamage;
        [SerializeField] TMP_Text _currentPrice;
        [SerializeField] TMP_Text _currentUpgradeOrBuy;
        [SerializeField] ShopItemSO[] _shopItemSO;
        [SerializeField] Image[] _shopItemImg;
        [SerializeField] TMP_Text _currentMoney;

        EventData _eventData;
        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            _exitButton.onClick.AddListener(ExitButton);
            _mapButton.onClick.AddListener(MapButton);
            _startButton.onClick.AddListener(StartButton);
            _currentUpgradeOrBuyButton.onClick.AddListener(UpgradeButton);
            _selectShipButton.onClick.AddListener(SelectShipButton);
        }



        private void Start()
        {
            SoundManager.Instance.Play("Menu");
            SelectButton(GameManager.Instance.LastShipIndex);
            ColorControl();
        }

        private void UpgradeButton()
        {
            
            if (GameManager.Instance.MoneyData >= _shopItemSO[GameManager.Instance.LastShipIndex].price)
            {
                GameManager.Instance.MoneyData -= _shopItemSO[GameManager.Instance.LastShipIndex].price;

                if (!_shopItemSO[GameManager.Instance.LastShipIndex].ownership)
                {
                    _shopItemSO[GameManager.Instance.LastShipIndex].ownership = true;
                }

                _shopItemSO[GameManager.Instance.LastShipIndex].price *= _shopItemSO[GameManager.Instance.LastShipIndex].priceMultiplier;
                _shopItemSO[GameManager.Instance.LastShipIndex].level++;
                _shopItemSO[GameManager.Instance.LastShipIndex].damage += _shopItemSO[GameManager.Instance.LastShipIndex].damageMultiplier;
                SelectButton(GameManager.Instance.LastShipIndex);
                ColorControl();
            }
        }
        public void SelectButton(int index)
        {
            if (_shopItemSO[index].ownership&& GameManager.Instance.LastHaveShipIndex != index)
            { 
                    _selectShipButton.gameObject.SetActive(true);
            }
            else
            {
                _selectShipButton.gameObject.SetActive(false);
            }

            _menuShipImage.sprite = _shopItemSO[GameManager.Instance.LastHaveShipIndex].Image;
            _currentShipImage.sprite = _shopItemSO[index].Image;
            _currentTitle.text = _shopItemSO[index].title;
            _currentLevel.text = "Level " + _shopItemSO[index].level;
            _currentDamage.text = _shopItemSO[index].damage.ToString();
            _currentPrice.text = _shopItemSO[index].price.ToString();

            if (_shopItemSO[index].ownership)
            {
                _currentUpgradeOrBuy.text = "UPGRADE";
                _currentShipImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                _currentShipImage.color = new Color(0, 0, 0, 1);
                _currentUpgradeOrBuy.text = "BUY";
            }

            GameManager.Instance.LastShipIndex = index;
        }
        public void ColorControl()
        {
            _currentMoney.text = GameManager.Instance.MoneyData.ToString();
            for (int i = 0; i < _shopItemSO.Length; i++)
            {
                if (!_shopItemSO[i].ownership)
                {
                    _shopItemImg[i].color = new Color(0, 0, 0, 1);
                }
                else
                {
                    _shopItemImg[i].color = new Color(1, 1, 1, 1);
                }
            }
        }

        #region Buttons
        public void ButtonSound()
        {
            SoundManager.Instance.Play("Click");
        }
        private void StartButton()
        {
            GameManager.Instance.NextLevel(1);
            _eventData?.OnPlay.Invoke();
        }

        private void SelectShipButton()
        {
            if (_shopItemSO[GameManager.Instance.LastShipIndex].ownership)
            {
                GameManager.Instance.LastHaveShipIndex = GameManager.Instance.LastShipIndex;
                SelectButton(GameManager.Instance.LastHaveShipIndex);
            }

        }
        private void MapButton()
        {
            throw new NotImplementedException();
        }

        private void ExitButton()
        {
            Debug.Log("ApplicationQuit");
            Application.Quit();
        }
        #endregion
        #region EventData
        // hepsini dinlemesine gerek yok

        private void OnEnable()
        {
            _eventData.OnIdle += OnIdle;
        }
        private void OnDestroy()
        {

            _eventData.OnIdle -= OnIdle;

        }
        private void OnIdle()
        {
            SelectButton(GameManager.Instance.LastShipIndex);
            ColorControl();
        }
        #endregion
    }


}
