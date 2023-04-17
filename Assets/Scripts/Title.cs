using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject btnsObj;
    [SerializeField] private GameObject rankingWnd;
    [SerializeField] private float startZ;
    [SerializeField] private float endZ;
    [SerializeField] private Text highScoreTxt;

    private Vector3 startPos;
    private Vector3 endPos;
    private void Start()
    {
        StartCoroutine(startProduction());


        if (PlayerPrefs.HasKey("HIGH") == false) PlayerPrefs.SetInt("HIGH", 0);

        startPos = rankingWnd.transform.position;
        endPos = rankingWnd.transform.position + (Vector3.right * 12.6f);
    }

    private IEnumerator startProduction()
    {
        float t = 0;

        while (t < 0.5f) 
        {
            yield return null;
            t += Time.deltaTime;

            float z = Mathf.Lerp(startZ, endZ, t/0.5f);

            btnsObj.transform.rotation = Quaternion.Euler(0,0,z);

        }
    }

    private IEnumerator RankingWndOn()
    {
        float t = 0;

        while (t < 0.5f)
        {
            yield return null;
            t += Time.deltaTime;
            rankingWnd.transform.position = Vector3.Lerp(startPos, endPos, t / 0.5f);
        }
    }


    public void RankingBtn()
    {
        StartCoroutine(RankingWndOn());
        highScoreTxt.text = $"HIGH SCORE : {PlayerPrefs.GetInt("HIGH")}";
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
