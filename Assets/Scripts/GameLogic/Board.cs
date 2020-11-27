using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Board (Dictionary<Vector2Int, Piece> _pieces)
    {
        pieces = _pieces;
    }

    private readonly Dictionary<Vector2Int, Piece> pieces;
    
    public List<Vector2Int> SpecificPiecesPositions(PieceType type, Team team)
    {
        var positions = new List<Vector2Int>();
        foreach (Piece piece in pieces.Values)
        {
            if (piece.Type == type && piece.Team == team)
                positions.Add(piece.GetBoardPosition());
        }

        return positions;
    }

    public bool IsSquareEmpty(Vector2Int square)
    {
        return pieces.ContainsKey(square);
    }

    public bool IsLineEmpty(Vector2Int start, Vector2Int end)
    {
        int x_difference = end[0] - start[0];
        int y_difference = end[1] - start[1];
        if (x_difference == 0 || y_difference == 0 || Mathf.Abs(x_difference) == Mathf.Abs(y_difference))
        {
            Vector2Int direction = new Vector2Int(ZeroToOneValue(x_difference), ZeroToOneValue(y_difference));
            while (start != end)
            {
                if (pieces.ContainsKey(start)) return false;
                start += direction;
                
            }
            return true;
        }
        else
        {
            Debug.LogError("Not a line");
            return false;
        }

    }

    private int ZeroToOneValue(int value)
    {
        var new_value = value == 0 ? 0 : Mathf.Abs(value) / value;
        return new_value;
    }
}
