using System.Collections.Generic;
using UnityEngine;
public abstract class MovePattern : IMovePattern
{

    protected MovePattern(Dictionary<Vector2Int, Piece> board, Piece this_piece)
    {
        pieces = board;
        piece = this_piece;
    }

    protected List<Vector2Int> moving_vectors;
    protected Dictionary<Vector2Int, Piece> pieces;
    protected Piece piece;

    protected Vector2Int piece_position => piece.GetBoardPosition();

    public virtual void Move(Vector2Int new_position)
    {
        UpdateDictionary(piece.GetBoardPosition(), new_position);
        piece.transform.localPosition = new Vector3(new_position.x, new_position.y);
    }

    protected void UpdateDictionary(Vector2Int old_position, Vector2Int new_position)
    {
        pieces.Remove(old_position);
        RemovePieceAt(new_position);
        pieces.Add(new_position, piece);
    }

    protected void RemovePieceAt(Vector2Int position)
    {
        if (pieces.ContainsKey(position))
        {
            pieces[position].gameObject.SetActive(false);
            pieces.Remove(position);
        }
    }

    public List<Vector2Int> GetPossibleMoves()
    {
        return CountPossibleMoves();
    }

    protected virtual List<Vector2Int> CountPossibleMoves()
    {
        var possible_moves = new List<Vector2Int>();
        foreach (Vector2Int moving_vector in moving_vectors)
        {
            var moving_vector_temp = moving_vector;
            while (IsPossibleToMoveByVector(moving_vector_temp))
            {
                possible_moves.Add(piece_position + moving_vector_temp);
                if (HasEnemy(piece_position + moving_vector_temp)) break;
                moving_vector_temp += moving_vector;
            }
        }
        return possible_moves;
    }

    protected List<Vector2Int> CountPossibleMovesFinite()
    {
        var possible_moves = new List<Vector2Int>();
        foreach (Vector2Int moving_vector in moving_vectors)
        {
            if (IsPossibleToMoveByVector(moving_vector))
                possible_moves.Add(piece_position + moving_vector);
        }
        return possible_moves;
    }

    protected virtual bool IsPossibleToMoveToPosition(Vector2Int position)
    {
        return (!pieces.ContainsKey(position) || HasEnemy(position)) && MovePatternUtils.IsPositionOnBoard(position);

    }

    protected bool IsPossibleToMoveByVector(Vector2Int vector)
    {
        return IsPossibleToMoveToPosition(piece_position + vector);
    }

    protected bool HasEnemy(Vector2Int position)
    {
        if (pieces.ContainsKey(position))
            if (pieces[position].Team != piece.Team)
                return true;
        return false;
    }
}
