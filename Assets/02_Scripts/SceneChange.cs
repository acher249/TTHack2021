using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string Scene;

    public void ChangeTheScene()
    {
        SceneManager.LoadScene(Scene);
        Debug.Log("Changed the scene!");
    }
}
