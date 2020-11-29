using System.Collections.Generic;
using UnityEngine;

public class Pawn : MovePattern, IMovePattern
{
    private List<Vector2Int> hit_vectors;
    protected List<Vector2Int> enpassant_moves;
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

        if (!MovePatternUtils.HasPieceMoved(piece))
            moving_vectors.Add(new Vector2Int(0, 2));
        if (piece.Team == Team.black)
        {
            MovePatternUtils.MirrorVectors(ref moving_vectors);
            MovePatternUtils.MirrorVectors(ref hit_vectors);
        }
        enpassant_moves = EnPassantMoves();
    }
    public override void Move(Vector2Int new_position)
    {
        if (enpassant_moves.Contains(new_position))
            EnPassant(new_position);
        else if (new_position.y == 1 || new_position.y == 8)
            Promotion(new_position);
        else base.Move(new_position);
    }

    protected override List<Vector2Int> CountPossibleMoves()
    {
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
        possible_moves.AddRange(enpassant_moves);
        return possible_moves;
    }
    private void EnPassant(Vector2Int new_position)
    {
        RemovePieceAt(new Vector2Int(new_position.x, piece.GetBoardPosition().y));
        base.Move(new_position);
    }

    private List<Vector2Int> EnPassantMoves()
    {
        var last_move = MoveHistory.Instance.GetLastTurn();
        if (last_move.Item3 != null)
            if (last_move.Item3.Type == PieceType.pawn && last_move.Item3.Team != piece.Team)
                if (Mathf.Abs((last_move.Item2 - last_move.Item1).y) == 2)
                    if (Mathf.Abs((last_move.Item2 - piece_position).x) == 1 && (last_move.Item2 - piece_position).y == 0)
                        return new List<Vector2Int>(){ (last_move.Item2 + last_move.Item1) / 2 };
        Debug.Log("Done");
        return new List<Vector2Int>();
    }

    private void Promotion(Vector2Int new_position)
    {
        Debug.Log("Promotion at " + new_position.ToString());
        base.Move(new_position);
    }
}
