using UnityEngine;

public class ServerPanel : MonoBehaviour, IInteractable
{

    public void Interact(bool isPositiveInteraction, int creditChangeAmount)
    {

        if (isPositiveInteraction)
        {
            Debug.Log($"Oyuncu Donaným Görevini Baþarýlý Þekilde Tamamladý. + {creditChangeAmount} kredi!");
            //GameManager.Instance.ChangePlayerCredit(creditChangeAmount);

        }
        else
        {
            Debug.Log($"Oyuncu Donaným Görevini Tamamlayamadý. {creditChangeAmount} kredi!");
            //GameManager.Instance.ChangePlayerCredit(creditChangeAmount);

        }

    }


}
