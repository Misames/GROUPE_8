using System.Collections.Generic;
using UnityEngine;

public class MDP : MonoBehaviour
{
    [SerializeField]
    private List<State> states;
    [SerializeField]
    private float gamma = 0.9f;
    [SerializeField]
    private float alpha = 0.5f;

    private Dictionary<State, float> V;
    private Dictionary<State, float> Vprime;
    private Dictionary<State, Action> policy;

    private void Start()
    {
        ValueIteration();
        foreach (var state in policy)
        {
            Debug.Log(state.Key.name + " : " + state.Value.direction);
        }
    }

    // Give the next state for one action on a state
    // @todo rendre les actions génériques ou ajouter des action d'autre type que TOP LEFT RIGHT BOT
    private State GetNextState(Action a, int indexState)
    {
        State nextState = null;

        switch (a.direction)
        {
            case Direction.TOP:
                nextState = states[indexState + 4];
                break;
            case Direction.LEFT:
                nextState = states[indexState - 1];
                break;
            case Direction.RIGHT:
                nextState = states[indexState + 1];
                break;
            case Direction.BOTTOM:
                nextState = states[indexState - 4];
                break;
            default:
                break;
        }

        return nextState;
    }

    // Init Value Function
    private void InitValueFunction()
    {
        V = new Dictionary<State, float>();
        Vprime = new Dictionary<State, float>();
        policy = new Dictionary<State, Action>();
        foreach (State s in states)
        {
            V[s] = 0f;
            Vprime[s] = 0f;
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
            foreach (var s in states)
            {
                // Find best action desicion
                float maxV = float.MinValue;
                Action maxA = null;
                foreach (var a in s.actionList)
                {
                    // Find next state for the current action
                    float value = 0f;
                    State sNext = GetNextState(a, indexState);
                    value += a.reward + gamma * V[sNext];
                    if (value > maxV)
                    {
                        maxV = value;
                        maxA = a;
                    }
                }

                policy[s] = maxA;
                Vprime[s] = maxV;
                delta = Mathf.Max(delta, Mathf.Abs(V[s] - Vprime[s]));
                indexState++;
            }

            // Update Value Function
            foreach (var s in states) V[s] = Vprime[s];
            iteration++;
        }
    }

    // Create une autre new Policy
    private void SARSA(uint x = 0)
    {
        InitValueFunction();

        int indexState = 0;
        foreach (State s in states)
        {
            for (int i = 0; i <= x; i++)
            {
                foreach (var a in s.actionList)
                {
                    State nextState = GetNextState(a, indexState);
                    float targetError = a.reward + gamma * V[nextState] - V[s];
                    V[s] += alpha * targetError;
                }
            }
            indexState++;
        }
        Debug.Log("fini SARSA !");
    }
}