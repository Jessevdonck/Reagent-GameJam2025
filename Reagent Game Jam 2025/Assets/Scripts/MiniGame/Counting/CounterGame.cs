using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame
{
    public class CounterGame : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabs;
        private List<GameObject> gameObjects;
        private Vector2 startPosition = new Vector2(-3.125f, 0.625f);
        public float xSpacing = 1.25f;
        public float ySpacing = 1.25f;
        [SerializeField] private GameObject wrongScreen;
        [SerializeField] private Transform boardParent;
        
        private static CounterGame instance;
        private int count;
        
        private int minigameID = 0;
        
        
        public int getCount()
        {
            return count;
        }
        
        private bool canClick;
            
        public bool getCanClick()
        {
            return canClick;
        }

        private void Update()
        {
            if (count == 12)
            {
                Debug.Log("finish");
                LevelManager.Instance.MarkMinigameComplete(0);
            }
        }


        void positiveFeedback()
        {
            
        }

        void negativeFeedback()
        {
            
        }

        public void destroyAllButtons()
        {
            foreach (GameObject go in gameObjects)
            {
                Destroy(go);
            }
        }
        public IEnumerator SwitchEverySeconds()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                destroyAllButtons();
                spawnCounterComponents();
            }
            
        }

        void Start()
        {
            gameObjects = new List<GameObject>();
            count = 0;
            spawnCounterComponents();
            canClick = true;
            StartCoroutine(SwitchEverySeconds());
            
        }

        void spawnCounterComponents()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                numbers.Add(i);
            }

            Shuffle(numbers);
            
           
            

            
            for (int i = 0; i < 12; i++)
            {
                int row = i / 6; // 0 or 1
                int col = i % 6;

                Vector2 spawnPos = new Vector2(
                    startPosition.x + col * xSpacing,
                    startPosition.y - row * ySpacing
                );
                
                GameObject instance = Instantiate(prefabs[numbers[i]], spawnPos, Quaternion.identity, boardParent);
                gameObjects.Add(instance);
                CounterComponent counter = instance.GetComponent<CounterComponent>();
                
            }
        }
        
        void Shuffle(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = Random.Range(i, list.Count);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }

        private void Awake()
        {
            instance = this;
            var position = this.transform.position;
            startPosition = new Vector2(position.x + startPosition.x, position.y + startPosition.y);
        }

        public static CounterGame getInstance()
        {
            return instance;
        }

        public void nextCount()
        {
            count += 1;
        }

        public void wrongCount()
        {
            StartCoroutine(DisableClickTemporarily());
        }
        
        public IEnumerator DisableClickTemporarily()
        {
            
            canClick = false;
            GameObject wrong = Instantiate(wrongScreen, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Destroy(wrong);
            canClick = true;
            
        }

        
    }
}
