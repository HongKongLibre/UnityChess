using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour
{
    [SerializeField] private TMP_Text final_message; 
    public void ShowEndScreen(Team winning_team)
    {
        final_message.text = winning_team.ToString()[0].ToString().ToUpper() + winning_team.ToString().Substring(1) + " team won";

        foreach(Transform child in transform)
            child.gameObject.SetActive(true);

        GetComponentInChildren<Button>().onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
