using UnityEngine;
using UnityEngine.SceneManagement;

public class Task2Manager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadTask1(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
