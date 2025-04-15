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
        private Light[][] lights;
        private int steps = 3;
        private static LightsGame instance;

        public static LightsGame getInstance()
        {
            return instance;
        }

        private void Start()
        {
            spawnPos = new Vector3(-20f, 20f, 0f);
            lights = new Light[5][];
            spawnLights();
        }

        private void Awake()
        {
            instance = this;
        }

        public void switchLight(int x, int y)
        {
            lights[x][y].switchLight();
            if (x > 0)
            {
                lights[x - 1][y].switchLight();
            }

            if (x < 4)
            {
                lights[x + 1][y].switchLight();
            }

            if (y > 0)
            {
                lights[x][y - 1].switchLight();
            }

            if (y < 4)
            {
                lights[x][y + 1].switchLight();
            }
        }

        void spawnLights()
        {
            for (int i = 0; i < 5; i++)
            {
                lights[i] = new Light[5];
                for (int j = 0; j < 5; j++)
                {
                    Vector3 newPos = new Vector3(spawnPos.x + j * 10f, spawnPos.y - i * 10, spawnPos.z);
                    GameObject instance = Instantiate(light, newPos, Quaternion.identity);
                    Light l = instance.GetComponent<Light>();
                    l.setX(i);
                    l.setY(j);
                    lights[i][j] = l;



                }
            }

            for (int i = 0; i < steps; i++)
            {
                int x = Random.Range(0, 5); // Assuming 5 rows
                int y = Random.Range(0, 5); // Assuming 6 columns
                switchLight(x,y);
            }

        }
    }
}