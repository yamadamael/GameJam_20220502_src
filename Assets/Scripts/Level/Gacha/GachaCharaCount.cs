using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class GachaCharaCount : MonoBehaviour
    {
        [SerializeField]
        Text _rarityText;
        [SerializeField]
        Text _countText;
        [SerializeField]
        Text _maxText;

        public void Init(int rarity, int count, int max)
        {
            var rarityText = "";
            switch (rarity)
            {
                case 1:
                    rarityText = "R : ";
                    break;
                case 2:
                    rarityText = "SR : ";
                    break;
                case 3:
                    rarityText = "SSR : ";
                    break;
                case 4:
                    rarityText = "SSSR : ";
                    break;
            }
            _rarityText.text = rarityText;

            _countText.text = count.ToString();
            _maxText.text = max.ToString();
        }
    }
}
