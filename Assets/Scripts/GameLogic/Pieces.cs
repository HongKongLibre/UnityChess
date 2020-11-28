using System;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    [SerializeField] private Dictionary<Vector2Int, Piece> pieces =
                         new Dictionary<Vector2Int, Piece>();

    [SerializeField] private GameObject pieces_prefab_object;
    [SerializeField] private Team current_turn = Team.white;

    [SerializeField] private Piece current_chosen_piece;
    [SerializeField] private IMovePattern current_chosen_piece_move_pattern => current_chosen_piece.GetMovePattern(pieces);
    [SerializeField] private List<Vector2Int> current_piece_possible_moves;

    public EventHandler<ChoosingPieceEventArgs> OnChoosingPiece;
    public EventHandler OnUnchoosingPiece;
    public EventHandler<MovingPieceEventArgs> OnMovingPiece;

    public class MovingPieceEventArgs : EventArgs
    {
        public Vector2Int old_position;
        public Vector2Int new_position;
        public PieceType type;
        public Team team;
    }
    public class ChoosingPieceEventArgs : EventArgs
    {
        public List<Vector2Int> possible_moves;
    }
    private void Awake()
    {
        if (pieces_prefab_object == null)
            pieces_prefab_object = this.gameObject;
        FillDictionary();
    }
    
    public void Click(Vector2Int board_position)
    {
        if (current_chosen_piece == null)
        {
            TryToGetPiece(board_position);
        }
        else
        {
            TryToMakeAMove(board_position);
            ClearCurrentChosenPiece();
        }
    }

    private void TryToGetPiece(Vector2Int vect2)
    {
        if (pieces.ContainsKey(vect2))
        {
            if (pieces[vect2].Team == current_turn)
            {
                current_chosen_piece = pieces[vect2];
                GetPiecePossibleMoves();
                OnChoosingPiece?.Invoke(this, new ChoosingPieceEventArgs
                {
                    possible_moves = current_piece_possible_moves,
                });
            }
        }
    }

    private void GetPiecePossibleMoves()
    {
        if (current_chosen_piece == null)
            Debug.LogError("Unexpected interaction");
        else
        {
            current_piece_possible_moves = current_chosen_piece_move_pattern.GetPossibleMoves();
        }
    }

    private void TryToMakeAMove(Vector2Int new_board_position)
    {
        if (current_chosen_piece != null && current_piece_possible_moves.Contains(new_board_position))
                MovePiece(new_board_position);

    }

    private void ClearCurrentChosenPiece()
    {
        current_chosen_piece = null;
        current_piece_possible_moves = null;
        OnUnchoosingPiece?.Invoke(this, EventArgs.Empty);
    }

    private void MovePiece(Vector2Int new_board_position)
    {
        Vector2Int old_piece_position = current_chosen_piece.GetBoardPosition();
        current_chosen_piece_move_pattern.Move(new_board_position);
        ChangeTurn();
        OnMovingPiece?.Invoke(this, new MovingPieceEventArgs
        {
            old_position = old_piece_position,
            new_position = new_board_position,
            type = current_chosen_piece.Type,
            team = current_chosen_piece.Team
            
        });

    }

    private void FillDictionary()
    {
        pieces.Clear();
        for (int i = 0; i < pieces_prefab_object.transform.childCount; i++)
        {
            Piece temp_piece_object = (pieces_prefab_object.transform.GetChild(i).GetComponent<Piece>());
            pieces.Add(temp_piece_object.GetBoardPosition(),
                       temp_piece_object);
        }
    }
    
    private void ChangeTurn()
    {
        current_turn = current_turn == Team.white ? (Team.black) : (Team.white);
    }

}
