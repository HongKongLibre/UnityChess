using System;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private Team team;
    [SerializeField] private PieceType type;

    public Team Team { get { return team; } }
    public PieceType Type { get { return type; } }

    public Vector2Int GetBoardPosition()
    {
        Vector2Int local_position = new Vector2Int(
                         (int)this.transform.localPosition.x,
                         (int)this.transform.localPosition.y);

        return local_position;
    }

    public IMovePattern GetMovePattern(Dictionary<Vector2Int, Piece> pieces)
    {
        IMovePattern move_pattern;
        switch (type)
        {
            case PieceType.king:
                move_pattern = new King(pieces, this);
                break;
            case PieceType.queen:
                move_pattern = new Queen(pieces, this);
                break;
            case PieceType.bishop:
                move_pattern = new Bishop(pieces, this);
                break;
            case PieceType.knight:
                move_pattern = new Knight(pieces, this);
                break;
            case PieceType.rook:
                move_pattern = new Rook(pieces, this);
                break;
            case PieceType.pawn:
                move_pattern = new Pawn(pieces, this);
                break;
            default:
                throw new NotImplementedException();
        }
        return move_pattern;
    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (type == PieceType.king)
            Debug.Log($"{team.ToString()[0].ToString().ToUpper() + team.ToString().Substring(1)} King has fallen");
    }
}
public enum Team{
    white,
    black
}

public enum PieceType{
    king,
    queen,
    bishop,
    knight,
    rook,
    pawn
}
