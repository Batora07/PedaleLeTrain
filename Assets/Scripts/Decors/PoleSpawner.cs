using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// will spawn the pole elements randomly in the scene, 
// with random heights to add some spicy challenge(s) *wink wink ;)*
public class PoleSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] poles = new GameObject[7];
    public float beginTimeRandom = 2f;
    public float endTimeRandom = 3f;
    public float changeZIndex = -2f;
    private int firstPole = 0;

    [SerializeField]
    public GameObject[] lines = new GameObject[7];
    
    GameObject lastPole = null;

    //private List<GameObject> polesSpawning = new List<GameObject>();

    public void PoleDisappeared(GameObject pole)
    {
        //mettre lastPole a droite et replacer sa ligne

        ElectricalLine el = lines[firstPole==poles.Length-1?0:firstPole].GetComponent<ElectricalLine>();

         el.leftPole = poles[firstPole-1>=0?firstPole-1:poles.Length-1].transform;
         el.rightPole = poles[firstPole].transform;
        //random
        float randX = Random.Range(4f, 5f);// Random.Range(Camera.main.gameObject.GetComponent<BoxCollider2D>().size.x/2f, Camera.main.gameObject.GetComponent<BoxCollider2D>().size.x/1.5f);
        float randY = Random.Range(1f, 4f);
        lastPole.transform.position = poles[firstPole - 1 >= 0 ? firstPole - 1 : poles.Length - 1].transform.position + new Vector3(randX, 0f, 0f);
        lastPole.transform.position = new Vector3(lastPole.transform.position.x,randY,0f);
        lastPole = pole;

        firstPole++;
        if (firstPole >= poles.Length)
        {
            lines[lines.Length - 1] = lines[0];
            for (int i = 0; i < lines.Length-1; i++)
            {
                lines[i] = lines[i + 1];
            }
            firstPole = 0;
        }
        el.ResetLine();
    }

    private void Awake()
    {
        InitializePoles();
        InitializeLines();
        lastPole = poles[0];
    }

    void Start ()
    {
        //StartCoroutine(SpawnRandomPoles());
    }

    void InitializeLines()
    {
        for(int i = 0; i < lines.Length-1; i++)
        {
            if (lines [i] == null)
            {
                // random
                GameObject obj = Instantiate(lines[0],Vector3.zero, Quaternion.identity) as GameObject;
                obj.SetActive(true);
                lines[i] = obj;
            }
            ElectricalLine el = lines[i].GetComponent<ElectricalLine>();
            el.leftPole = poles[i].transform;
            el.rightPole = poles[i+1].transform;
            //el.ResetLine();
        }
        lines[lines.Length - 1] = lines[0];
    }
    

    void InitializePoles()
    {
        float lastPosX = transform.position.x;
        float lastPosY = transform.position.y;
        // random

        for (int i = 0; i < poles.Length; i++)
        {
            if (poles[i] == null)
            {
                lastPosX = lastPosX + Random.Range(4f, 5f);
                lastPosY = lastPosY + Random.Range(lastPosY, 1.5f);
                GameObject obj = Instantiate(poles[0], new Vector3(lastPosX, lastPosY, changeZIndex), Quaternion.identity) as GameObject;
                float randomScale = Random.Range(.5f, 2f);
                obj.transform.localScale = new Vector3(randomScale, randomScale * 1.5f);
                obj.SetActive(true);
                poles[i] = obj;

            }
            else
            {
                lastPosX = poles[i].transform.position.x;
            }/*
            polesSpawning.Add(obj);
            polesSpawning[i].SetActive(false); // get element i from the list and only activates the decor element when we want it
            index++;
            Debug.Log("Adding a pole");
            if (index == poles.Length)
            {
                index = 0;
            }
            //*/
        }
    }
    /*
    IEnumerator SpawnRandomPoles()
    {
        yield return new WaitForSeconds(Random.Range(beginTimeRandom, endTimeRandom));

        int index = Random.Range(0, polesSpawning.Count);

        while (true)
        {
            if (!polesSpawning[index].activeInHierarchy) {
                polesSpawning[index].SetActive(true);
                polesSpawning[index].transform.position = new Vector3(transform.position.x, transform.position.y, changeZIndex);
                float random = Random.Range(1.5f, 3.5f);
                polesSpawning[index].transform.localScale = new Vector3(random, random);
                break;
            }
            else
            {
                index = Random.Range(0, polesSpawning.Count);
            }
        }

        StartCoroutine(SpawnRandomPoles());
    }//*/
}
