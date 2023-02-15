using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour
{
    public GameObject robot;
    public GameObject objectif;
    public List<GameObject> lstCube;

    private void Start()
    {
        ValueEvaluation();
        PolicyEvaluation();
    }

    private void ValueEvaluation()
    {
        foreach (var cube in lstCube)
        {
            if (cube.tag == "") { }
        }
    }

    private void PolicyEvaluation() { }

    private void PolicyImprovement() { }
}
