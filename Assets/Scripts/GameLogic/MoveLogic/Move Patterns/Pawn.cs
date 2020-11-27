using System.Collections.Generic;
using UnityEngine;

public class Pawn : MovePattern, IMovePattern
{
    private List<Vector2Int> hit_vectors;
    
    public Pawn(Dictionary<Vector2Int, Piece> board, Piece this_piece) : base(board, this_piece)
    {
        moving_vectors = new List<Vector2Int>()
        {
            new Vector2Int(0, 1)
        };
        hit_vectors = new List<Vector2Int>()
        {
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
        };
        
    }

    protected override List<Vector2Int> CountPossibleMoves()
    {
        if (piece.MoveCount == 0)
            moving_vectors.Add(new Vector2Int(0, 2));
        if (piece.Team == Team.black)
        {
            Utils.MirrorVectors(ref moving_vectors);
            Utils.MirrorVectors(ref hit_vectors);
        }
        var possible_moves = new List<Vector2Int>();
        foreach (Vector2Int vector in moving_vectors)
        {
            if (IsPossibleToMoveByVector(vector))
                if (HasEnemy(piece_position + vector)) break;
                else possible_moves.Add(piece_position + vector);
        }

        foreach (Vector2Int vector in hit_vectors)
        {
            if (HasEnemy(piece_position + vector))
                possible_moves.Add(piece_position + vector);
        }
        return possible_moves;
    }

    private List<Vector2Int> EnPassant()
    {
        return null;
    }
}
