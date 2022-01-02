using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TImeKeeper : MonoBehaviour
{
    Text timeText;
    [SerializeField]float timeScore;
    float finalScore;
    bool finalScoreOver = false;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6 && finalScoreOver == false)
        {
            UpdateTime();
            finalScore = timeScore;
            timeText.text = "Time: " + finalScore.ToString("F2");
            finalScoreOver = true;
        }
        
        else if(SceneManager.GetActiveScene().buildIndex != 6)
        {
            UpdateTime();
            timeText.text = "Time: " + timeScore.ToString("F2");
        }
    }

    void UpdateTime()
    {
        timeScore = Time.time;
        Debug.Log(timeScore);

    }
}
