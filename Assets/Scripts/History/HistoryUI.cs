using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryUI : MonoBehaviour
{
    [SerializeField] private static int turn_number = 0;
    [SerializeField] private GameObject history_textbox_prefab;
    [SerializeField] private float distance_between_textboxes_x;
    [SerializeField] private float distance_between_textboxes_y;
    private int rows_in_window;

    private void Start()
    {
        FindObjectOfType<Pieces>().OnMovingPiece += HandleMovingPiece;
        rows_in_window = (int)((GetComponent<RectTransform>().rect.height - history_textbox_prefab.GetComponent<RectTransform>().rect.height - history_textbox_prefab.transform.localPosition.y) / distance_between_textboxes_y);

    }

    private void HandleMovingPiece(object sender, Pieces.MovingPieceEventArgs e)
    {
        Move move = new Move(e.old_position, e.new_position, e.piece);
        CreateNewTextBox(move);
    }

    private void CreateNewTextBox(Move move)
    {
        var prefab_position = history_textbox_prefab.transform.localPosition;
        if (move.piece.Team == Team.white)
        {
            turn_number++;
            CreateTurnNumberTextBox();
            var new_position = prefab_position + new Vector3(distance_between_textboxes_x, -distance_between_textboxes_y * turn_number, 0);
            CreateNewTextBox(new_position, GetHistoryStringFromVectors(move));
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, GetComponent<RectTransform>().rect.height/2);
            if (turn_number > rows_in_window)
            {
                GetComponent<RectTransform>().offsetMin += new Vector2(0, -distance_between_textboxes_y);
            }
        }
        else
        {
            var new_position = prefab_position + new Vector3(distance_between_textboxes_x * 2, -distance_between_textboxes_y * turn_number, 0);
            CreateNewTextBox(new_position, GetHistoryStringFromVectors(move));
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, GetComponent<RectTransform>().rect.height/2);
        }


    }
    
    private void CreateNewTextBox(Vector3 position, string message)
    {
        var new_text_box = Instantiate(history_textbox_prefab, this.transform);
        new_text_box.GetComponent<RectTransform>().anchoredPosition = position;
        new_text_box.GetComponentInChildren<TMP_Text>().text = message;
    }

    private void CreateTurnNumberTextBox()
    {
        var new_text_box = Instantiate(history_textbox_prefab, this.transform);
        var old_position = new_text_box.transform.localPosition;
        new_text_box.transform.localPosition = new Vector3(old_position.x, old_position.y - turn_number * distance_between_textboxes_y, old_position.z);
        new_text_box.GetComponentInChildren<TMP_Text>().text = $"{turn_number}.";
        Destroy(new_text_box.GetComponent<Button>());
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
}
