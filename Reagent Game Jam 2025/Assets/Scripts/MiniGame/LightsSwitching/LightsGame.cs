using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.LightsSwitching
{
    public class LightsGame : MonoBehaviour
    {
        [SerializeField] private GameObject light;
        private Vector3 spawnPos;
        private List<GameObject> gameObjects;
        private Light[][] lights;
        private int steps = 5;
        private static LightsGame instance;

        public static LightsGame GetInstance()
        {
            return instance;
        }

        public void ResetLights()
        {
            foreach (GameObject o in gameObjects)
            {
                Destroy(o);
            }
            
            lights = new Light[5][];
            SpawnLights();
        }

        private void Start()
        {
            spawnPos = new Vector3(-2.5f, 2.5f, 0f);
            lights = new Light[5][];
            gameObjects = new List<GameObject>();
            SpawnLights();
            
        }

        private void Awake()
        {
            instance = this;
        }

        public void SwitchLight(int x, int y)
        {
            lights[x][y].SwitchLight();
            if (x > 0)
            {
                lights[x - 1][y].SwitchLight();
            }

            if (x < 4)
            {
                lights[x + 1][y].SwitchLight();
            }

            if (y > 0)
            {
                lights[x][y - 1].SwitchLight();
            }

            if (y < 4)
            {
                lights[x][y + 1].SwitchLight();
            }
        }

        void SpawnLights()
        {
            for (int i = 0; i < 5; i++)
            {
                lights[i] = new Light[5];
                for (int j = 0; j < 5; j++)
                {
                    Vector3 newPos = new Vector3(spawnPos.x + j * 1.25f, spawnPos.y - i * 1.25f, spawnPos.z);
                    GameObject instance = Instantiate(light, newPos, Quaternion.identity);
                    gameObjects.Add(instance);
                    Light l = instance.GetComponent<Light>();
                    l.SetX(i);
                    l.SetY(j);
                    lights[i][j] = l;



                }
            }

            for (int i = 0; i < steps; i++)
            {
                int x = Random.Range(0, 5);
                int y = Random.Range(0, 5);
                SwitchLight(x,y);
            }

        }
    }
}