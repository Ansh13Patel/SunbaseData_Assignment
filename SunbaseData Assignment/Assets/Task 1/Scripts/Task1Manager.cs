using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Task1Manager : MonoBehaviour
{
    public GameObject popUpPanel;

    public void closePopupPanel()
    {
        popUpPanel.SetActive(false);
    }

    public void LoadTask2()
    {
        SceneManager.LoadScene(1);
    }
}
