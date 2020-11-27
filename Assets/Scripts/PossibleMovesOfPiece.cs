using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PossibleMovesOfPiece : MonoBehaviour
{
    [SerializeField] private GameObject pieces_object;
    [SerializeField] private GameObject marker_example;
    
    private void Start()
    {
        this.transform.position = pieces_object.transform.position;
    }

    private void OnEnable()
    {
        pieces_object.GetComponent<Pieces>().OnChoosingPiece += HandleChoosingPiece;
        pieces_object.GetComponent<Pieces>().OnUnchoosingPiece += HandleUnchoosingPiece;
    }

    private void OnDisable()
    {
        pieces_object.GetComponent<Pieces>().OnChoosingPiece -= HandleChoosingPiece;
        pieces_object.GetComponent<Pieces>().OnUnchoosingPiece -= HandleUnchoosingPiece;
    }

    private void HandleUnchoosingPiece(object sender, EventArgs e)
    {
        ClearMarkers();
    }

    private void HandleChoosingPiece(object sender, Pieces.ChoosingPieceEventArgs e)
    {
        DrawMarkers(e.possible_moves);
    }

    private void DrawMarkers(List<Vector2Int> markers_positions)
    {
        ClearMarkers();
        foreach (Vector2Int position in markers_positions)
        {
            DrawMarker(position);
        }

    }

    private void DrawMarker(Vector2Int marker_position)
    {
        var marker_object = Instantiate(marker_example);
        marker_object.SetActive(true);
        marker_object.transform.SetParent(this.transform);
        marker_object.transform.localPosition = new Vector3(marker_position.x, marker_position.y, 2);
        
    }

    private void ClearMarkers()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
}
