using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour
{
    public float gamma = 0.9f;
    public GameObject robot;
    public GameObject objectif;
    public List<GameObject> lstCube;
    public Material matRobot;
    public Material matDefault;
    private uint iteration = 0;

    private void Start()
    {
        ValueIteration();
        PolicyEvaluation();
        PolicyImprovement();
    }

    private void ValueIteration()
    {
        float delta, tempValue, newValue;
        int indexCube;

        while (true)
        {
            Debug.Log(iteration);
            delta = 0;
            tempValue = 0;
            indexCube = 0;

            foreach (var cube in lstCube)
            {
                State currentState = cube.GetComponent<State>();
                tempValue = currentState.value;
                newValue = 0;

                foreach (var action in currentState.lstAction)
                {
                    GameObject nextCube = null;
                    if (action == "Top") nextCube = lstCube[indexCube + 4];
                    if (action == "Left") nextCube = lstCube[indexCube - 1];
                    if (action == "Right") nextCube = lstCube[indexCube + 1];
                    if (action == "Bot") nextCube = lstCube[indexCube - 4];
                    if (nextCube == null) Debug.Log("fini !");

                    State nextSate = nextCube.GetComponent<State>();
                    float v = currentState.reward + (gamma * (nextSate.value));
                    if (v > newValue) newValue = v;
                }

                currentState.value = newValue;
                delta = Mathf.Max(delta, tempValue - currentState.value);
                indexCube++;
            }
            if (delta < 0.005) break;
            iteration++;
        }
    }

    private void PolicyEvaluation() { }

    private void PolicyImprovement() { }
}
