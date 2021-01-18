using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public string SceneName;
    private void OnMouseUpAsButton()
    {
        OpenNewScene(SceneName);

    }
    public void OpenNewScene(string name)
    {
        if (name.Length > 0)
            SceneManager.LoadScene(name);
        else Debug.Log("Is don't open scene!");
    }
}
