using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject endGameUI;
    [SerializeField] private TextMeshProUGUI endStatusText;


    public void Awake()
    {

        endGameUI.SetActive(false);


    }

    public void Update()
    {
        /*
        if (GameManager.Instance.IsGameOver(out int score))
        {
            endGameUI.SetActive(true);

            if(score <= 0)
            {
                endStatusText.text = "Lose!";
                endStatusText.color = Color.red;
            }
            else if (score >= 100)
            {
                endStatusText.text = "Win!"; 
                endStatusText.color = Color.green;
            }

        }
        */
    }


}
