using TMPro;
using UnityEngine;

public class ResourceSourceUI : MonoBehaviour
{
    public GameObject resourcePanel;
    public TextMeshProUGUI qunatityText;
    public ResourceSource resourceSource;

    private void OnMouseEnter()
    {
        resourcePanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        resourcePanel.SetActive(false);
    }

    public void OnResourceQuantityChange()
    {
        qunatityText.text = resourceSource.quantity.ToString();
    }

}
