using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    int currentSceneIndex;
    int nextSceneIndex;

    void Start() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex+1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
    }
    
    void Update() 
    {
        Debug.Log("Current Scene Index: " + currentSceneIndex);
        Debug.Log("Next Scene Index: " + nextSceneIndex);    
    }
        void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is a friendly!");
                break;
            case "Fuel":
                Debug.Log("This is fuel!");
                break;
            case "Finish":
                Debug.Log("This is the finish!");
                LoadNextLevel();
                break;
            default:
                Debug.Log("You blew up");
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
