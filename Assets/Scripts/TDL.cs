using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDL : MonoBehaviour
{
    float alpha = 0.5f;

    private void Start()
    {
        TemporalDifference(0);
    }

    private void TemporalDifference(uint x = 0)
    {
        // for le current state
        float targetErrorEstimate = alpha * (1.5f - 1f);
    }

}
