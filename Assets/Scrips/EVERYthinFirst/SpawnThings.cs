using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    public static SpawnThings Instance { get; private set; }

    public static GameObject weight;
    public static GameObject fish1;
    public static GameObject fish2;
    public static GameObject balance;

    public GameObject goweight;
    public GameObject gofish1;
    public GameObject gofish2;
    public GameObject gobalance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            
           //GameObject goweight = Resources.Load<GameObject>("3dPrefebs/weight/weight_0");//资源
           // GameObject gofish1 = Resources.Load<GameObject>("3dPrefebs/pufferFish/puffer_0");
           // GameObject gofish2 = Resources.Load<GameObject>("3dPrefebs/pufferFish/puffer_0");
           // GameObject gobalance = Resources.Load<GameObject>("3dPrefebs/Balance/The Balance");

            weight= Instantiate(goweight);//实例化后才是场景的物体
            fish1= Instantiate(gofish1);
            fish2 = Instantiate(gofish2);
            balance = Instantiate(gobalance);

            if (weight != null) weight.SetActive(true);
            if (fish1 != null) fish1.SetActive(true);
            if (fish2 != null) fish2.SetActive(true);
            if (balance != null) balance.SetActive(true);

            weight.name = goweight.name;
            fish1.name = gofish1.name;
            fish2.name = gofish2.name;

            //weight.name = "w1";
            //fish1.name = "f1";
            //fish2.name = "f2";

            //StartCoroutine(RenameObjects());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log($"weight name: {weight.name}");
        Debug.Log($"fish1 name: {fish1.name}");
        Debug.Log($"fish2 name: {fish2.name}");
    }

    private IEnumerator RenameObjects()
    {
        yield return new WaitForEndOfFrame(); // 等待一帧

        weight.name = "w1";
        fish1.name = "fishNO!";
        fish2.name = "f2";
    }

}
