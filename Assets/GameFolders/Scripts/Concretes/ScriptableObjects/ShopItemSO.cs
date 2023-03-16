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
    }

}
