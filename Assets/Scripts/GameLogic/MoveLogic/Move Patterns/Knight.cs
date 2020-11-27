
using System.Collections.Generic;
using UnityEngine;

public class Knight : MovePattern, IMovePattern
{
    public Knight(Dictionary<Vector2Int, Piece> board, Piece this_piece) : base(board, this_piece)
    {
        moving_vectors = new List<Vector2Int>()
        {
            new Vector2Int(2, 1),
            new Vector2Int(1, 2),
            new Vector2Int(2, -1),
            new Vector2Int(-1, 2),
            new Vector2Int(-2, 1),
            new Vector2Int(1, -2),
            new Vector2Int(-2, -1),
            new Vector2Int(-1, -2)
        };

    }

    protected override List<Vector2Int> CountPossibleMoves()
    {
        return base.CountPossibleMovesFinite();
    }

}

