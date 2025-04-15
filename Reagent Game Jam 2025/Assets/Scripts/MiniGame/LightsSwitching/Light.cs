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
            setLightOn();
        }

        private void OnMouseDown()
        {
            LightsGame.getInstance().switchLight(x, y);
            Debug.Log("x :" + x + "y :" + y);
        }

        public void switchLight()
        {
            if (isLight)
            {
                setLightOff();
                
            }
            else
            {
                setLightOn();
            }
        }
        
        public void setLightOn()
        {
            sr.sprite = lightOn;
            isLight = true;
        }
        public void setLightOff()   
        {
            sr.sprite = lightOff;
            isLight = false;
        }

        public void setX(int i)
        {
            this.x = i;
        }
        public void setY(int i)
        {
            this.y = i;
        }
        
    }
}