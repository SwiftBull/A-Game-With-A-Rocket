using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("I'm Friendly!");
                break;
            case "Finish":
                Debug.Log("You Won!");
                break;
            case "Fuel":
                Debug.Log("You got some fuel");
                break;
            default:
                Respawn();
                break;
        }
    }

    void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
