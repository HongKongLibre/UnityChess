using UnityEngine;

public class UserController : MonoBehaviour
{
    [SerializeField] private GameObject pieces_object;
    [SerializeField] private Camera camera;
    [SerializeField] private Pieces pieces;
    [SerializeField] private Vector3 mouse_position_vector;

    private void Start()
    {
        pieces = pieces_object.GetComponent<Pieces>();
        this.transform.position = pieces_object.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pieces.Click(TransformMousePositionInBoardPosition(Input.mousePosition));
        }

    }

    private Vector2Int TransformMousePositionInBoardPosition(Vector3 mouse_position)
    {
        mouse_position = camera.ScreenToWorldPoint(mouse_position);
        mouse_position = pieces_object.transform.InverseTransformPoint(mouse_position);
        Vector2Int board_position = new Vector2Int((int)(mouse_position.x + 0.45), (int)(mouse_position.y + 0.45));
        return board_position;
    }

}
