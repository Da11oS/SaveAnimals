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
        if (SceneName.Length > 0)
            SceneManager.LoadScene(SceneName);
        else Debug.Log("Is don't open scene!");

    }
}
