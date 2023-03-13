using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DynamicProgramming : MonoBehaviour
{
    [SerializeField]
    private List<State> states;
    [SerializeField]
    private float gamma = 0.9f;

    private Dictionary<State, float> Q;
    private Dictionary<State, float> Qprime;
    private Dictionary<State, Action> policy;

    private void Start()
    {
        Stopwatch stopwatch = new Stopwatch();

        // Démarrer le chronomètre
        stopwatch.Start();

        // Appeler la fonction à mesurer
        ValueIteration();

        // Arrêter le chronomètre
        stopwatch.Stop();

        // Afficher le temps d'exécution
        UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
    }

    // Init Value Function
    private void InitValueFunction()
    {
        Q = new Dictionary<State, float>();
        Qprime = new Dictionary<State, float>();
        policy = new Dictionary<State, Action>();

        foreach (State s in states)
        {
            Q[s] = 0f;
            Qprime[s] = 0f;
        }
    }

    // Create new Policy
    private void ValueIteration(uint maxIteration = 1000)
    {
        InitValueFunction();

        uint iteration = 0;
        float delta = 1;
        while (iteration < maxIteration && delta > 0.005f)
        {
            delta = 0f;
            int indexState = 0;
            foreach (State currentState in states)
            {
                // Find best action desicion
                float maxV = float.MinValue;
                Action maxA = null;
                foreach (Action currentAction in currentState.actionList)
                {
                    // Find next state for the current action
                    float value = 0f;
                    State sNext = currentAction.GetNextState(states, indexState);
                    value += currentAction.reward + gamma * Q[sNext];
                    if (value > maxV)
                    {
                        maxV = value;
                        maxA = currentAction;
                    }
                }

                policy[currentState] = maxA;
                Qprime[currentState] = maxV;
                delta = Mathf.Max(delta, Mathf.Abs(Q[currentState] - Qprime[currentState]));
                indexState++;
            }

            // Update Value Function
            foreach (State currentState in states) Q[currentState] = Qprime[currentState];
            iteration++;
        }
    }
}