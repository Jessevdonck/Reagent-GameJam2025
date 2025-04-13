using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace TilemapLogic
{


    public class PlacingSystem : MonoBehaviour
    {
        private bool _isWirePlacing { get; set; }
        private bool _firstClick;
        private bool _showPreview { get; set; }
        private int previewLength;
        private Inventory _inv;




        [SerializeField] private Tilemap _tm;
        [SerializeField] private List<TileBase> _tiles;
        [SerializeField] private Tilemap _previewTm;
        [SerializeField] private List<TileBase> _previewTiles;

        private Vector3Int _startLocation;
        private Vector3Int _endLocation;


        private Vector3Int _lastPreviewPosition;
        private bool _isComponentPlacing = true;
        private bool _directionSwitch = false;
        private Camera _mainCamera;

        private CircuitComponent _currentComponent;
        
        void Start()
        {
            _inv = GetComponent<Inventory>();
            _lastPreviewPosition = new Vector3Int(0, 0, 0);
            _mainCamera = Camera.main;
            _isWirePlacing = true;
        }

        private void FixedUpdate()
        {
            showPreview();
        }



        void showPreview()
        {
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = _previewTm.WorldToCell(worldPos);
            if (gridPos != _lastPreviewPosition)
            {
                _lastPreviewPosition = gridPos;
                _previewTm.ClearAllTiles();

            }

            if (_firstClick && _showPreview)
            {
                PlaceWire(_startLocation, gridPos, _previewTm, _previewTiles[0]);
            }
        }


        void Update()
        {


            if (_inv.getWireAmount() == 0) return;
            if (!_isComponentPlacing)
            {
                return;
            }

            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (!_isWirePlacing) return;
            wirePlacing(worldPos);



        }


        private void wirePlacing(Vector3 coords)
        {
            // ReSharper disable once Unity.UnknownInputAxes
            if (Input.GetButtonDown("directionswitch"))
            {
                _previewTm.ClearAllTiles();
                _directionSwitch = !_directionSwitch;

            }



            if (Input.GetMouseButtonDown(0) && !_firstClick)
            {

                _startLocation = _tm.WorldToCell(coords);
                _firstClick = true;

                return;
            }

            if (Input.GetMouseButtonDown(0) && _firstClick)
            {

                _endLocation = _tm.WorldToCell(coords);
                _firstClick = false;

                PlaceWire(_startLocation, _endLocation, _tm, _tiles[0]);
                _previewTm.ClearAllTiles();

            }
        }

        void PlaceWire(Vector3Int start, Vector3Int end, Tilemap tm, TileBase tile)
        {

            List<Vector3Int> positions = GetManhattanLine(start, end);
            previewLength = positions.Count;

            if (previewLength > _inv.getWireAmount())
            {


                return;
            }


            // Place the wire tiles along the path
            foreach (Vector3Int position in positions)
            {
                tm.SetTile(position, tile);
            }

            if (tm == _tm)
            {
                _inv.removeWire(previewLength);
            }

        }


        List<Vector3Int> GetManhattanLine(Vector3Int start, Vector3Int end)
        {
            List<Vector3Int> line = new List<Vector3Int>();





            if (_directionSwitch)
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