using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class GachaScene : BaseScene
    {
        [SerializeField]
        GachaRoot _root;

        [SerializeField]
        GachaUIRoot _uiRoot;

        void Awake()
        {
            App.FadeManager.FadeIn(0.3f);
        }

        // Start is called before the first frame update
        void Start()
        {
            _uiRoot.Init();
        }

        // Update is called once per frame
        void Update()
        {
            _uiRoot.UpdateUIRoot();
        }
    }
}
