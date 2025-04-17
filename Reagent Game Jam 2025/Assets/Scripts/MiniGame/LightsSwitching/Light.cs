using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.LightsSwitching
{
    public class Light : MonoBehaviour
    {
        private bool isLight;
        [SerializeField]
        private Sprite lightOn;
        [SerializeField]
        private Sprite lightOff;

        private int x;
        private int y;
        
        private SpriteRenderer sr;
        private void Start()
        {
            float random = Random.Range(0, 11);
        }

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            SetLightOn();
        }

        private void OnMouseDown()
        {
            LightsGame.GetInstance().SwitchLight(x, y);
            Debug.Log("x :" + x + "y :" + y);
        }

        public void SwitchLight()
        {
            if (isLight)
            {
                SetLightOff();
                
            }
            else
            {
                SetLightOn();
            }
        }
        
        public void SetLightOn()
        {
            sr.sprite = lightOn;
            isLight = true;
        }
        public void SetLightOff()   
        {
            sr.sprite = lightOff;
            isLight = false;
        }

        public void SetX(int i)
        {
            this.x = i;
        }
        public void SetY(int i)
        {
            this.y = i;
        }

        public bool IsOn()
        {
            return isLight;
        }
        
    }
}