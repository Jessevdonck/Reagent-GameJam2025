using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace MiniGame.CockRoach
{
    public class CockroachGame : MonoBehaviour
    {
        [SerializeField] Transform topLeft;
        [SerializeField] Transform bottomRight;
        [SerializeField] int amount = 5;
        [SerializeField] private GameObject cockroach;
        private List<GameObject> cockroaches;
        public bool isGameCompleted;
        private void Start()
        {
            isGameCompleted = false;
            cockroaches = new List<GameObject>();
            for (int i = 0; i < amount; i++)
            {
                Instantiate(cockroach, PickRandomTarget(), quaternion.identity);
                
            }
        }

        public void HitRoach()
        {
            amount--;
            if (amount == 0)
            {
                GameCompleted();
            }
        }

        private void GameCompleted()
        {
            isGameCompleted = true;
        }
        public Transform[] GetTransforms()
        {
            return new [] { topLeft, bottomRight };
        }
        private static CockroachGame instance;

        public static CockroachGame GetInstance()
        {
            return instance;
        }
        
        Vector2 PickRandomTarget()
        {
            var position = bottomRight.position;
            var position1 = topLeft.position;
            float x = Random.Range(position1.x, position.x);
            float y = Random.Range(position.y, position1.y); // Y is reversed if top is higher
            
            return new Vector2(x, y);
        }

        private void Awake()
        {
            instance = this;
        }
    }
}