using System.Collections.Generic;
using UnityEngine;

public class Policy : MonoBehaviour
{

    [SerializeField]
    private List<State> states;
    private Action[] policy;

    private void Start() { }

    private void Init() { }

    private void PolicyEvaluation() { }

    private void PolicyImprovement() { }

    private void V(Action[] policy) { }

    private void Q() { }
}
