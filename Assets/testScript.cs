using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    private void Start()
    {
        var s = "34";
        s += "45";
        Debug.Log(s);
        StartCoroutine(SomeCoroutine()); 
    }
    private IEnumerator SomeCoroutine()
    {
        Debug.Log("Start Coroutine");
        while (enabled)
        {
            Debug.Log("2 second");
            yield return new WaitForSeconds(1f);
            Debug.Log("is done");
        }
    }
}
