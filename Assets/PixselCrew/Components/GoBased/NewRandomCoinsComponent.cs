using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRandomCoinsComponent : MonoBehaviour
{
    [SerializeField] private int _count = 1;
    [SerializeField] [Range(0, 100)] private int _percentSilver = 50;
    [SerializeField] [Range(0, 100)] private int _percentGold = 50;
    [SerializeField] private GameObject _coinSilver;
    [SerializeField] private GameObject _coinGold;

    public void CreateRandomCoins()
    {
        if (_count < 1)
            return;

        int countGold = _count * _percentGold / 100;
        int countSilver = _count * _percentSilver / 100;

        if (countGold + countSilver < _count)
            countGold++;

        Debug.Log(string.Format("CreateRandomCoins {0} s:{1} g:{2}...", _count, countSilver, countGold));
        CreateCoins(countSilver, _coinSilver);
        CreateCoins(countGold, _coinGold);
    }

    private void CreateCoins(int count, GameObject go)
    {
        for (var i = 0; i < count; i++)
        {
            Spawn(go);
           // var instante = Instantiate(go, SetRandomPosition(transform.position), Quaternion.identity);
           // instante.transform.localScale = transform.lossyScale;
        }
    }

    private Vector3 SetRandomPosition(Vector3 vp)
    {
        var x = Random.Range(-0.9f, 0.9f);
        var y = Random.Range(0, 0.9f);
        return new Vector3(vp.x + x, vp.y + y, vp.z);
    }

    private void Spawn(GameObject go)
    {
        var instante = Instantiate(go, SetRandomPosition(transform.position), Quaternion.identity);
        instante.transform.localScale = transform.lossyScale;
    }
    public void DropImmediate(GameObject[] items)
    {
        foreach (var item in items)
            Spawn(item);
    }
}
