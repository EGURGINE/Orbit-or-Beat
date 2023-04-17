using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] GameManager gm;

    [SerializeField] private GameObject center;

    [SerializeField] private float[] circleR;
    [SerializeField] private int switchCircleR;
    [SerializeField] private float deg;
    [SerializeField] private float spd;
    [SerializeField] float durationT = 0;

    [SerializeField] private GameObject[] switchImage;
    [SerializeField] private ParticleSystem switchPc;

    [SerializeField] private Map mapMG;

    [SerializeField] private AudioSource audio;

    private bool isGameOver;

    private bool isCheet;
    [SerializeField] private Text cheetTxt;
    private IEnumerator Start()
    {
        mapMG.Spawn();

        yield return new WaitForSeconds(1.5f);
        StartSET();
    }

    private void StartSET()
    {
        audio.Play();
        isGameOver = false;
        isCheet = false;
        StartCoroutine(SwitchMove());
        StartCoroutine(PlayerMove());
    }

    IEnumerator PlayerMove()
    {
        while (isGameOver == false)
        {
            yield return null;

            durationT += Time.deltaTime;

            if (durationT < spd)
            {
                deg = Mathf.Lerp(0, 360, durationT / spd);
                var rad = Mathf.Deg2Rad * (deg);
                var x = circleR[switchCircleR] * Mathf.Sin(rad);
                var y = circleR[switchCircleR] * Mathf.Cos(rad);

                transform.position = center.transform.position + new Vector3(x, y);
            }
            else
            {
                durationT = 0;
                deg = 0;
                mapMG.Spawn();
            }
        }
    }

    private IEnumerator SwitchMove()
    {
        while(isGameOver == false)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.F1))
            {
                isCheet = (isCheet == true)? false : true;

                string switchOn = (isCheet == true) ? "ON" : "OFF";

                cheetTxt.text = $"Cheet : {switchOn}";
            }


            if (Input.anyKeyDown)
            {
                switchCircleR = (switchCircleR == 0) ? 1 : 0;

                switchPc.Play();
                StartCoroutine(SwitchImage());
            }

        }
    }
    private IEnumerator SwitchImage()
    {
        switchImage[switchCircleR].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        switchImage[switchCircleR].SetActive(false);
    }


  


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (isCheet == true) return;
            isGameOver = true;
            gm.Die();
        }
    }

}
