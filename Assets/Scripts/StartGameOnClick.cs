using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameOnClick : MonoBehaviour
{
    // El nombre de la escena que se cargará al hacer clic en el texto
    public string firstSceneName;

    private void Start()
    {
        // Obtener una referencia al componente Button del objeto
        Button button = GetComponent<Button>();

        // Agregar un listener al botón que cargará la primera escena al hacer clic en él
        button.onClick.AddListener(() => Time.timeScale = 1f);
        button.onClick.AddListener(() => SceneManager.LoadScene(firstSceneName));
    }
}
