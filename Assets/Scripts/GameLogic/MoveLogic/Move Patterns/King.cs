﻿using System.Collections.Generic;
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

    public override void Move(Vector2Int new_position)
    {
        if (CastlingMoves().Contains(new_position))
            Castling(new_position);
        else base.Move(new_position);
    }

    private void Castling(Vector2Int new_position)
    {
        if (new_position == piece_position + new Vector2Int(2,0))
        {
            IMovePattern rook_move_pattern = pieces[piece_position + new Vector2Int(3, 0)].GetMovePattern(pieces);
            rook_move_pattern.Move(piece_position + new Vector2Int(1, 0));
        }
        if (new_position == piece_position - new Vector2Int(2, 0))
        {
            IMovePattern rook_move_pattern = pieces[piece_position - new Vector2Int(4, 0)].GetMovePattern(pieces);
            rook_move_pattern.Move(piece_position - new Vector2Int(1, 0));
        }
        Move(new_position);
    }

    protected override List<Vector2Int> CountPossibleMoves()
    {
        var possible_moves = base.CountPossibleMovesFinite();

        //Check for castling
        possible_moves.AddRange(CastlingMoves());

        return possible_moves;
    }

    private List<Vector2Int> CastlingMoves()
    {
        var possible_moves = new List<Vector2Int>();
        if (piece.MoveCount == 0)
        {
            if (pieces.ContainsKey(piece_position + new Vector2Int(3, 0)))
                if (pieces[piece_position + new Vector2Int(3, 0)].MoveCount == 0)
                {
                    if (Utils.IsLineEmpty(pieces, piece_position + new Vector2Int(1, 0), piece_position + new Vector2Int(2, 0)))
                        possible_moves.Add(piece_position + new Vector2Int(2,0));
                }

            if (pieces.ContainsKey(piece_position - new Vector2Int(4, 0)))
                if (pieces[piece_position - new Vector2Int(4, 0)]?.MoveCount == 0)
                {
                    if (Utils.IsLineEmpty(pieces, piece_position - new Vector2Int(1, 0), piece_position - new Vector2Int(3, 0)))
                        possible_moves.Add(piece_position - new Vector2Int(2,0));
                }
        }
        return possible_moves;
    }


}
