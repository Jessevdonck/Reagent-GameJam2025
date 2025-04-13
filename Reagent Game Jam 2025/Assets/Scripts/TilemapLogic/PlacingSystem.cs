using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace TilemapLogic
{


    public class PlacingSystem : MonoBehaviour
    {
        public static PlacingSystem instance; 
        

        private Vector3Int lastPreviewPosition;
        private bool isWirePlacing;
        private Stack<Vector3Int> points;
        private Wire tempWire;
        
        
        public bool getIsWirePlacing()
        {
            return isWirePlacing;
        }
        
        private bool showPreview { get; set; }
        private int previewLength;
        private Inventory _inv;



        [SerializeField] private List<Wire> wires;
        [SerializeField] private Tilemap _tm;
        [SerializeField] private List<TileBase> _tiles;
        [SerializeField] private Tilemap _previewTm;
        [SerializeField] private List<TileBase> _previewTiles;

       


       
        
        private bool directionSwitch = false;
        private Camera mainCamera;

        // private CircuitComponent _currentComponent;
        
        void Start()
        {
            _inv = GetComponent<Inventory>();
            lastPreviewPosition = new Vector3Int(0, 0, 0);
            mainCamera = Camera.main;
            isWirePlacing = false;
            wires = new List<Wire>();
            showPreview = true;
        }

        private void Awake()
        {
            instance = this;
        }


        private void FixedUpdate()
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = _previewTm.WorldToCell(worldPos);
            if (gridPos != lastPreviewPosition)
            {
                lastPreviewPosition = gridPos;
                _previewTm.ClearAllTiles();
            
            }
            
            if (isWirePlacing && showPreview)
            {
                PlaceWire(points.Peek(), gridPos, _previewTm, _previewTiles[0]);
               
            }
        }




        public void startWireDrawing(Vector3 startPosition)
        {
            this.points = new Stack<Vector3Int>();  
            this.isWirePlacing = true;
            this.tempWire = new Wire();
            points.Push(_tm.WorldToCell(startPosition));

        }

        public void finalizeWireDrawing(Vector3 endPosition)
        {
            this.isWirePlacing = false;
            tempWire.points.Add(_tm.WorldToCell(endPosition));
            wires.Add(tempWire);
        }
        
        void Update()
        {


            if (_inv.getWireAmount() == 0) return;
            if (!isWirePlacing) return;

            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetButtonDown("directionswitch"))
            {
                _previewTm.ClearAllTiles();
                directionSwitch = !directionSwitch;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceWire(points.Peek(), _tm.WorldToCell(worldPos), _tm, _tiles[0]);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (points.Count == 0)
                {
                    isWirePlacing = false;
                    return;
                }
                Vector3Int lastPoint = points.Pop();
                for (int i = tempWire.points.Count - 1; i >= 0; i--)
                {
                    if (tempWire.points[i].Equals(points.Peek())) break;
                    _tm.SetTile(tempWire.points[i], null);

                }

            }
            
            Debug.Log(wires.Count);

            









        }


        void RemoveWire(List<Vector3Int> points, Tilemap tm)
        {
            foreach (Vector3Int point in points)
            {
                tm.SetTile(point, null);
            }
        }
        
        

        void PlaceWire(Vector3Int start, Vector3Int end, Tilemap tm, TileBase tile)
        {

            List<Vector3Int> positions = GetManhattanLine(start, end);
            previewLength = positions.Count;

            if (previewLength > _inv.getWireAmount())return;
            
            
            foreach (Vector3Int position in positions)
            {
                tm.SetTile(position, tile);
            }

            if (tm == _tm)
            {
                _inv.removeWire(previewLength);
                foreach (Vector3Int pos in positions)
                {
                    tempWire.points.Add(pos);
                }
                points.Push(positions[previewLength-1]);
            }

        }


        List<Vector3Int> GetManhattanLine(Vector3Int start, Vector3Int end)
        {
            List<Vector3Int> line = new List<Vector3Int>();





            if (directionSwitch)
            {

                for (int x = start.x; x != end.x; x += (end.x > start.x ? 1 : -1))
                {
                    line.Add(new Vector3Int(x, start.y, 0));
                }


                for (int y = start.y; y != end.y; y += (end.y > start.y ? 1 : -1))
                {
                    line.Add(new Vector3Int(end.x, y, 0));
                }
            }
            else
            {

                for (int y = start.y; y != end.y; y += (end.y > start.y ? 1 : -1))
                {
                    line.Add(new Vector3Int(start.x, y, 0));
                }


                for (int x = start.x; x != end.x; x += (end.x > start.x ? 1 : -1))
                {
                    line.Add(new Vector3Int(x, end.y, 0));
                }
            }


            line.Add(end);

            return line;
        }

    }
}