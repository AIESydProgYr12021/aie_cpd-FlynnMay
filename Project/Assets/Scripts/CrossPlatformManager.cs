using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlatformManager : MonoBehaviour
{

    public List<GameObject> inactiveOnAndroid = new List<GameObject>();
    public List<GameObject> inactiveOnPC = new List<GameObject>();
    public List<GameObject> inactiveOnWeb= new List<GameObject>();
    
    public List<GameObject> activeOnAndroid = new List<GameObject>();
    public List<GameObject> activeOnPC = new List<GameObject>();
    public List<GameObject> activeOnWeb= new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {

#if UNITY_ANDROID
        foreach (var obj in inactiveOnAndroid)
        {
            obj.SetActive(false);
        }
        foreach (var obj in activeOnAndroid)
        {
            obj.SetActive(true);
        }
#endif

#if UNITY_WEBGL
        foreach (var obj in inactiveOnWeb)
        {
            obj.SetActive(false);
        }
        foreach (var obj in activeOnWeb)
        {
            obj.SetActive(true);
        }
#endif

#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX)
        foreach (var obj in inactiveOnPC)
        {
            obj.SetActive(false);
        }
        
        foreach (var obj in activeOnPC)
        {
            obj.SetActive(true);
        }
#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
