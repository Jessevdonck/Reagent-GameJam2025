using UnityEngine;
using UnityEngine.Tilemaps;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "NewComponent", menuName = "Inventory/Component")]
    public class Component : ScriptableObject
    {
        public TileBase tile;        
        public int amount;           
        public Sprite itemIcon; 
    }
}