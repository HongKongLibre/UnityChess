using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MoveHistory : MonoBehaviour
{
    [SerializeField] private static List<Move> history = new List<Move>();
    private static MoveHistory instance = null;

    public static MoveHistory Instance 
    { get 
        {
            if (instance == null)
                throw new NotSupportedException();
            return instance;
        } 
    }

    private void InitializeHistory()
    {
        FindObjectOfType<Pieces>().OnMovingPiece += HandleMovingPiece;
        instance = this;
    }

    private void Start()
    {
        InitializeHistory();
    }

    public (Vector2Int, Vector2Int, Piece) GetLastTurn()
    {
        if (history.Any())
            return history.Last().Unzip();
        else return (Vector2Int.zero, Vector2Int.zero, null);
    }

    public bool HasPieceMoved(Piece piece)
    {
        return history.Exists(x => x.piece == piece);
    }



    private void HandleMovingPiece(object sender, Pieces.MovingPieceEventArgs e)
    {
        Move move = new Move(e.old_position, e.new_position, e.piece);
        AddMoveToHistory(move);
    }

    private void AddMoveToHistory(Move move)
    {
        history.Add(move);
    }
}


