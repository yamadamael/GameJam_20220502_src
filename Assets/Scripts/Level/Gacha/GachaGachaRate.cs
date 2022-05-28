using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class GachaGachaRate : MonoBehaviour
    {
        [SerializeField]
        Text _rarityText;
        [SerializeField]
        Text _rateText;

        public void Init(int rarity, int rate)
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

            _rateText.text = string.Format("{0}%", rate);

        }
    }
}
