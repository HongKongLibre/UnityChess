﻿using System.Collections.Generic;
using UnityEngine;

public class Bishop : MovePattern, IMovePattern
{
    public Bishop(Dictionary<Vector2Int, Piece> board, Piece this_piece) : base(board, this_piece)
    {
        moving_vectors = new List<Vector2Int>()
        {
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1)
        };
    }

}
