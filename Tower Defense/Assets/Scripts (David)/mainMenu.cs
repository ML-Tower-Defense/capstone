using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void floweryMeadow () {
        SceneManager.LoadScene("FloweryMeadow");
    }

    public void toonyPlains()
    {
        SceneManager.LoadScene("ToonyPlains");
    }

    public void closeGame() {
        Debug.Log("Exited Game");
        Application.Quit();
    }
}
