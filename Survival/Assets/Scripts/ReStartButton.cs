using UnityEngine.SceneManagement;
using UnityEngine;

public class ReStartButton : MonoBehaviour
{
    public void OnClickButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
