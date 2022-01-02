using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelTime = 1.601f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip winSound;

    [SerializeField] ParticleSystem explosionPart;
    [SerializeField] ParticleSystem winPart;

    public int currentSceneIndex;

    AudioSource collisionSound;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        collisionSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) {return;}

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
        
        isTransitioning = true;
        collisionSound.Stop();
        winPart.Play();
        collisionSound.PlayOneShot(winSound, 1);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelTime);
    }

    void CrashSesquence()
    {
        isTransitioning = true;
        collisionSound.Stop();
        explosionPart.Play();
        collisionSound.PlayOneShot(explosionSound, 1);
        GetComponent<Movement>().enabled = false;
        Invoke("Respawn", nextLevelTime);
    }

    void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
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
