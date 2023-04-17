using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] GameManager gm;

    [SerializeField] private GameObject center;

    [SerializeField] private float[] circleR;
    [SerializeField] private int switchCircleR;
    [SerializeField] private float deg;
    [SerializeField] private float spd;

    [SerializeField] private GameObject[] switchImage;
    [SerializeField] private ParticleSystem switchPc;

    [SerializeField] private Map mapMG;
   

    private void Start()
    {
        mapMG.Spawn();
    }

    void Update()
    {
        SwitchMove();
        PlayerMove();
    }
    [SerializeField] float t = 0;
    private void PlayerMove()
    {
        t += Time.deltaTime;

        if (t < spd)
        {
            deg = Mathf.Lerp(0,360, t / spd);
            var rad = Mathf.Deg2Rad * (deg);
            var x = circleR[switchCircleR] * Mathf.Sin(rad);
            var y = circleR[switchCircleR] * Mathf.Cos(rad);

            transform.position = center.transform.position + new Vector3(x, y);
        }
        else
        {
            t = 0;
            deg = 0;
            mapMG.Spawn();
        }
    }

    private void SwitchMove()
    {
        if (Input.anyKeyDown)
        {
            switchCircleR = (switchCircleR == 0) ? 1 : 0;

            switchPc.Play();
            StartCoroutine(SwitchImage());
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
             gm.Die();
        }
    }

}
