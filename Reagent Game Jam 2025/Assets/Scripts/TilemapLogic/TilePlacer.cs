using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilePlacer : MonoBehaviour
{   
    private bool _isWirePlacing = false;
    private bool _firstClick = false;
    private bool _showPreview = true;
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

    // [SerializeField]
    // private Inventory inv;
    
    void Start()
    {
        _inv = GetComponent<Inventory>();
        _lastPreviewPosition = new Vector3Int(0, 0, 0);
        
        _isWirePlacing = true;
    }

    private void FixedUpdate()
    {
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = _previewTm.WorldToCell(worldPos);
        if (gridPos != _lastPreviewPosition)
        {
            _lastPreviewPosition = gridPos;
            _previewTm.ClearAllTiles();
            
        }
        if (_firstClick && _showPreview)
        {
            PlaceWire(_startLocation, gridPos,_previewTm,_previewTiles[0]);
        }
    }

    void Update()
    {
        if(_inv.getWireAmount() == 0) return;
        
        
        // isWirePlacing = lm.getIsWirePlacing();
        if(!_isComponentPlacing)return;
        
        
        
        
        
        if (!_isWirePlacing) return;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !_firstClick)
        {
            
            _startLocation = _tm.WorldToCell(worldPos);
            _firstClick = true;
            Debug.Log("click 1");
            return;
        }

        
        
        if (Input.GetMouseButtonDown(0) && _firstClick)
        {
            
            _endLocation = _tm.WorldToCell(worldPos);
            _firstClick = false;
            Debug.Log("click 2");
            PlaceWire(_startLocation, _endLocation,_tm,_tiles[0]);
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

        
        int dx = Mathf.Abs(end.x - start.x);
        int dy = Mathf.Abs(end.y - start.y);

       
        if (dx > dy) 
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
