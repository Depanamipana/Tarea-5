using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitGameOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Salir del juego cuando se hace clic en el objeto de texto
        Application.Quit();
    }
}
