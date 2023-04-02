using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Space.SO
{
    [CreateAssetMenu(fileName ="ShopMenu", menuName ="Scriptable Objects/New Shop Item", order = 1)]
    public class ShopItemSO : ScriptableObject
    {
        public Sprite Image;
        public string title;
        public int damage;
        public int price;
        public int level;
        public int priceMultiplier;
        public int damageMultiplier;
        public bool ownership;

        public void SaveData()
        {
            PlayerPrefs.SetInt(title+ "ItemDamage", damage);
            PlayerPrefs.SetInt(title+"ItemPrice", price);
            PlayerPrefs.SetInt(title + "ItemLevel", level);
            PlayerPrefs.SetInt(title + "ItemPriceMultiplier", priceMultiplier);
            PlayerPrefs.SetInt(title + "ItemDamageMultiplier", damageMultiplier);
            PlayerPrefs.SetInt(title + "ItemOwnership", ownership ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            damage = PlayerPrefs.GetInt(title + "ItemDamage", damage);
            price = PlayerPrefs.GetInt(title + "ItemPrice", price);
            level = PlayerPrefs.GetInt(title + "ItemLevel", level);
            priceMultiplier = PlayerPrefs.GetInt(title + "ItemPriceMultiplier", priceMultiplier);
            damageMultiplier = PlayerPrefs.GetInt(title + "ItemDamageMultiplier", damageMultiplier);
            ownership = PlayerPrefs.GetInt(title + "ItemOwnership", ownership ? 1 : 0) == 1;
        }
    }
 
}
