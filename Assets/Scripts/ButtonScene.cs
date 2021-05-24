using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    public void ChangeScenne(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
