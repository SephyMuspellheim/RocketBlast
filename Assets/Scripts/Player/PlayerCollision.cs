using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    /********************************************************************************************/
    /** Components                                                                              */
    /********************************************************************************************/

    // Define the movement component so a reference to it can be stored
    Movement movementComponent;
    // Define the audio source component so the audio source so it can be interacted with via code
    AudioSource AudioSourceComp;

    /********************************************************************************************/
    /** Parameters                                                                              */
    /********************************************************************************************/

    [SerializeField] float restartTimer = 3.0f;
    [SerializeField] AudioClip LevelComplete;
    [SerializeField] AudioClip RocketExplode;

    [SerializeField] ParticleSystem SuccessFX;
    [SerializeField] ParticleSystem CrashFX;

    /********************************************************************************************/
    /** Variables                                                                               */
    /********************************************************************************************/

    int currentSceneIndex;
    int nextSceneIndex;
    bool isTransitioning = false;

    void Start() 
    {
        // Store references to movement and audio components 
        movementComponent = GetComponent<Movement>();
        AudioSourceComp = GetComponent<AudioSource>();

        // store reference to current scene index and calculate next scene index for level load and reload
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex+1;
        // if current scene is the last, loop back to initial level
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is a friendly!");
                break;
            case "Finish":
                Debug.Log("This is the finish!");
                HandleLevelComplete();
                break;
            default:
                Debug.Log("You blew up");
                HandleRocketCrash();
                break;
        }
    }

    void HandleRocketCrash()
    {
        isTransitioning = true;
        AudioSourceComp.Stop();
        CrashFX.Play();
        AudioSourceComp.PlayOneShot(RocketExplode);
        movementComponent.enabled = false;
        Invoke(nameof(ReloadLevel), restartTimer);
    }

    void HandleLevelComplete()
    {
        isTransitioning = true;
        AudioSourceComp.Stop();
        SuccessFX.Play();
        AudioSourceComp.PlayOneShot(LevelComplete);
        movementComponent.enabled = false;
        Invoke(nameof(LoadNextLevel), restartTimer);

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
