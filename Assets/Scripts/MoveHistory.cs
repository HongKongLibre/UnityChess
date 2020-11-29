using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class MoveHistory : MonoBehaviour
{
    [SerializeField] private static List<Move> history = new List<Move>();
    [SerializeField] private TMP_Text history_text;
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
        AddTextMessageToHistoryText();
    }

    private void AddTextMessageToHistoryText()
    {
          history_text.text += GetHistoryStringFromVectors(history.Last());
    }

    private string GetHistoryStringFromVectors(Move move)
    {
        string show_string;
        if (special_history_messages.ContainsKey(move)) 
            show_string = special_history_messages[move];
        else 
            show_string = VToStr(move.old_position) + "-" + VToStr(move.new_position);
        show_string = move.piece.Team == Team.white ? show_string += "   " : show_string += "\n";
        return show_string;
    }

    private string VToStr(Vector2Int vector)
    {
        return (char)(vector.x + (int)'a' - 1) + vector.y.ToString();
    }

    private Dictionary<Move, string> special_history_messages = new Dictionary<Move, string>()
    {

    };

    struct Move
    {
        public readonly Vector2Int old_position;
        public readonly Vector2Int new_position;
        public readonly Piece piece;

        public Move(Vector2Int old_position, Vector2Int new_position, Piece piece)
        {
            this.old_position = old_position;
            this.new_position = new_position;
            this.piece = piece;

        }

        public (Vector2Int, Vector2Int , Piece) Unzip()
        {
            return (this.old_position, this.new_position, this.piece);
        }
    }
}


