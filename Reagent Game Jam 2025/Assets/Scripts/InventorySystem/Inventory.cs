using System;
using System.Collections.Generic;

using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private Component wire;
        [SerializeField]
        private Component resistor;
        [SerializeField]
        private Component extender;
        [SerializeField]
        private Component splitter;
        [SerializeField]
        private Component combiner;


        public void Start()
        {
            wire = ScriptableObject.CreateInstance<Component>();
            wire.amount = 100;
            
        }

        public int getWireAmount()
        {
            return wire.amount;
        }
        
        public void addWire(int i)
        {
            wire.amount += i;
        }
        public void addResistor(int i)
        {
            resistor.amount += i;
        }
        public void addExtender(int i)
        {
            extender.amount += i;
        }
        public void addSplitter(int i)
        {
            splitter.amount+= i;
        }

        public void addCombiner(int i)
        {
            combiner.amount+= i;
        }

        public void removeWire(int i)
        {
            wire.amount -= i;
        }

    }
}