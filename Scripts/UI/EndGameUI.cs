using TMPro;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameEndText;


    private void Start()
    {
        GameManager.Instance.OnGameLose += GameManager_OnGameLose;
        Hide();
    }

    private void GameManager_OnGameLose(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }


}
