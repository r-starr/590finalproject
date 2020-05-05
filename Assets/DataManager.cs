using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{

    public void LoadWalkDemo() {
        SceneManager.LoadScene("WalkDemo");
        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("WalkDemo"));
        SceneManager.UnloadSceneAsync("AvatarCreation");
        Destroy(GameObject.Find("DoneButton"));
    }
}
