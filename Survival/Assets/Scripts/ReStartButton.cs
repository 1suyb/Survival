using UnityEngine.SceneManagement;
using UnityEngine;

public class ReStartButton : MonoBehaviour
{
    public void OnReStartButton()
    {
        Debug.Log("�����");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
