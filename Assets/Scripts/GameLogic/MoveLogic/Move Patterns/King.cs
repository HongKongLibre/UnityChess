using System.Collections.Generic;
using UnityEngine;

public class King : MovePattern, IMovePattern
{
    public King(Dictionary<Vector2Int, Piece> board, Piece this_piece) : base(board, this_piece)
    {
        moving_vectors = new List<Vector2Int>()
        {
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1)
        };
    }

    protected override List<Vector2Int> CountPossibleMoves()
    {
        var possible_moves = base.CountPossibleMovesFinite();

        //Check for castling
        //possible_moves.AddRange(Castiling());

        return possible_moves;
    }

    //private List<Vector2Int> Castiling()
    //{
    //    var possible_moves = new List<Vector2Int>();
    //    if (piece.MoveCount == 0)
    //    {
    //        var this_position = piece.GetBoardPosition();
    //        if (pieces.ContainsKey(this_position + new Vector2Int(3, 0)))
    //            if (pieces[this_position + new Vector2Int(3, 0)].MoveCount == 0)
    //            {
    //                if (Utils.IsLineEmpty(pieces, this_position + new Vector2Int(1, 0), this_position + new Vector2Int(2, 0)))
    //                    possible_moves.Add(new Vector2Int(7, 1));
    //            }

    //        if (pieces.ContainsKey(this_position - new Vector2Int(4, 0)))
    //            if (pieces[this_position - new Vector2Int(4, 0)]?.MoveCount == 0)
    //            {
    //                if (Utils.IsLineEmpty(pieces, this_position - new Vector2Int(1, 0), this_position - new Vector2Int(3, 0)))
    //                    possible_moves.Add(new Vector2Int(3, 1));
    //            }
    //    }
    //    return possible_moves;
    //}
}
