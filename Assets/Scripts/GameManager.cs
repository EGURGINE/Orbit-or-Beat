using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text Score;
    private float score;

    private void Update()
    {
        score += Time.deltaTime;

        Score.text = ((int)score).ToString();
    }

    public void Die()
    {
        if (((int)score) > PlayerPrefs.GetInt("HIGH"))
            PlayerPrefs.SetInt("HIGH", ((int)score));
        SceneManager.LoadScene("Title");
    }
}
