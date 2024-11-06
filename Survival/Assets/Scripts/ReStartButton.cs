using UnityEngine.SceneManagement;
using UnityEngine;

public class ReStartButton : MonoBehaviour
{
    public void OnReStartButton()
    {
        Debug.Log("¿ÁΩ√¿€");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
