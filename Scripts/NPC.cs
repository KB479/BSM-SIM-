using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{

    public void Interact(bool isPositiveInteraction, int creditChangeAmount)
    {

        if (isPositiveInteraction)
        {
            Debug.Log($"Oyuncu HR Görevini Baþarýlý Þekilde Tamamladý. + {creditChangeAmount} kredi!");
            //GameManager.Instance.ChangePlayerCredit(creditChangeAmount);

        }
        else
        {
            Debug.Log($"Oyuncu HR Görevini Tamamlayamadý. {creditChangeAmount} kredi!");
            //GameManager.Instance.ChangePlayerCredit(creditChangeAmount);

        }

    }

}
