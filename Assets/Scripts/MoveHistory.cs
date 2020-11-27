using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class MoveHistory : MonoBehaviour
{

    [SerializeField] private Stack<Move> history = new Stack<Move>();
    [SerializeField] private TMP_Text history_text;
    private void OnEnable()
    {
        FindObjectOfType<Pieces>().OnMovingPiece += HandleMovingPiece;
    }

    private void OnDisable()
    {
        FindObjectOfType<Pieces>().OnMovingPiece -= HandleMovingPiece;
    }

    public (Vector2Int, Vector2Int, PieceType, Team) GetLastTurn()
    {
        return history.Peek().Unzip();
    }

    private void HandleMovingPiece(object sender, Pieces.MovingPieceEventArgs e)
    {
        Move move = new Move(e.old_position, e.new_position, e.type, e.team);
        AddMoveToHistory(move);
    }

    private void AddMoveToHistory(Move move)
    {
        history.Push(move);
        AddTextMessageToHistoryText();
    }

    private void AddTextMessageToHistoryText()
    {
          history_text.text += GetHistoryStringFromVectors(history.Peek());
    }

    private string GetHistoryStringFromVectors(Move move)
    {
        string show_string;
        if (special_history_messages.ContainsKey(move)) 
            show_string = special_history_messages[move];
        else 
            show_string = VToStr(move.old_position) + "-" + VToStr(move.new_position);
        show_string = move.piece_team == Team.white ? show_string += "   " : show_string += "\n";
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
        public readonly PieceType piece_type;
        public readonly Team piece_team;

        public Move(Vector2Int old_position, Vector2Int new_position, PieceType piece_type, Team piece_team)
        {
            this.old_position = old_position;
            this.new_position = new_position;
            this.piece_type = piece_type;
            this.piece_team = piece_team;

        }

        public (Vector2Int, Vector2Int , PieceType , Team) Unzip()
        {
            return (this.old_position, this.new_position, this.piece_type, this.piece_team);
        }
    }
}


