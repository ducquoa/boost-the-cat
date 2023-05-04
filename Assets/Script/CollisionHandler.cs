using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeTime = 1f;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip pass;
    [SerializeField] ParticleSystem deathPar;
    [SerializeField] ParticleSystem passPar;

    Rigidbody rb;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }
    
    void Update()
    {
        ResponseToKey();
    }
    
    void ResponseToKey() {
        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;     //toggle collision
        }
    }

    void OnCollisionEnter(Collision other) {
        
        if (isTransitioning || collisionDisabled) { return; }
        switch (other.gameObject.tag) {
            case "Respawn":
                Debug.Log("You are home");
                break;
        
            case "Finish":
                Debug.Log("You're done.");
                LevelDone();
                break;

            default: 
                Debug.Log("You hit an obstacle");
                GameOver();
                break;
        
        }
    }
  
    void LevelDone() {
        
        //todo add particle fx onCrash
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(pass);
        passPar.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", invokeTime);
    }
    
    void GameOver() {
       
        //todo add particle fx onCrash
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathPar.Play();
        Invoke("ReloadLevel", invokeTime);
    }
    
    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
        }

    void NextLevel() {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    
}


