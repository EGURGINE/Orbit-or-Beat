using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
public struct MapObjSpawnSet
{
    public GameObject[] obj;
    public bool isTrue;
    public bool Inside;
}


[System.Serializable]
public class SpawnList
{
    public List<MapObjSpawnSet> nextSpawnSet = new List<MapObjSpawnSet>(); 
}
public class Map : MonoBehaviour
{
    [SerializeField] SpawnList spList;

    public List<SpawnList> NextSpawnSetting = new List<SpawnList>();

    public int spNum;

    [SerializeField] PostProcessVolume volume;
    [SerializeField] Vignette vignette;
    [SerializeField] Color[] randColor;

    private void Awake()
    {
        volume.profile.TryGetSettings(out vignette);
    }


    public void Spawn()
    {

        if (NextSpawnSetting.Count <= spNum) 
        { 
            StageProduction();
            return; 
        }

        MapSpawn(spNum);

        spNum ++;
    }

    private void MapSpawn(int stage)
    {
        foreach (MapObjSpawnSet s in NextSpawnSetting[stage].nextSpawnSet)
        {
            if (s.isTrue == true)
            {
                s.obj[0].SetActive(!s.Inside);
                s.obj[1].SetActive(s.Inside);
            }
            else
            {
                for (int i = 0; i < 2; i++) s.obj[i].SetActive(false);
            }

        }
    }

    private void StageProduction()
    {
        int repeatStageNum = Random.Range(7, 9);

        MapSpawn(repeatStageNum);



        StartCoroutine(CameraSizeChange());
        ColorChange();
    }

    private IEnumerator CameraSizeChange()
    {

        float t = 0;
        float startSize = Camera.main.orthographicSize;
        float endSize = (startSize > 0) ? -5 : 5;
        while (t <= 0.5f)
        {
            yield return null;
            t += Time.deltaTime;


            Camera.main.orthographicSize = Mathf.Lerp(startSize, endSize, t / 0.5f);
        }
    }
    private void ColorChange()
    {
        vignette.color.value = randColor[Random.Range(0, randColor.Length - 1)];
    }
}
