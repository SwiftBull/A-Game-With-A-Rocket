using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelTime = 1.5f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip winSound;

    AudioSource collisionSound;

    bool isTransitioning = false;

    void Start() 
    {
        collisionSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("I'm Friendly!");
                break;
            case "Finish":
                StartSuccessProcess();
                break;
            default: //this is when we die
                CrashSesquence();
                break;
        }
    }

    void StartSuccessProcess()
    {
        collisionSound.PlayOneShot(winSound, 1);
        //also add a particle effect when Success
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelTime);
    }

    void CrashSesquence()
    {
        if(!isTransitioning)
        {
            collisionSound.PlayOneShot(explosionSound, 1);
            GetComponent<Movement>().enabled = false;
            Invoke("Respawn", nextLevelTime);
            //also add a particle effect when crash
        }
        
        
        
    }

    void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
