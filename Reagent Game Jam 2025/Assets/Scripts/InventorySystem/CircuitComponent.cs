using UnityEngine;
using UnityEngine.Tilemaps;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "NewComponent", menuName = "Inventory/Component")]
    public class CircuitComponent : ScriptableObject
    {
        public TileBase tile;
        public int amount;
        public Sprite itemIcon;
    }
}