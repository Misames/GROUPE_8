using System.Collections.Generic;
using UnityEngine;

public class MDP : MonoBehaviour
{
    public GameObject player;
    public GameObject objectif;
    public Material matplayer;
    public Material matDefault;

    [SerializeField]
    private List<State> states;
    private Dictionary<State, float> V;
    private Dictionary<State, float> Vprime;
    private Dictionary<State, Action> policy;

    private void Start()
    {
        ValueIteration();
        foreach (var state in policy)
        {
            Debug.Log(state.Key.name +" : " + state.Value.direction);   
        }
    }

    private void ValueIteration(uint maxIteration = 1000, float gamma = 0.9f)
    {
        // Init Value Function
        V = new Dictionary<State, float>();
        Vprime = new Dictionary<State, float>();
        policy = new Dictionary<State, Action>();
        foreach (State s in states)
        {
            V[s] = 0f;
            Vprime[s] = 0f;
        }

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
                    State sNext = null;
                    switch (a.direction)
                    {
                        case Direction.TOP:
                            sNext = states[indexState + 4];
                            break;
                        case Direction.LEFT:
                            sNext = states[indexState - 1];
                            break;
                        case Direction.RIGHT:
                            sNext = states[indexState + 1];
                            break;
                        case Direction.BOTTOM:
                            sNext = states[indexState - 4];
                            break;
                        case Direction.LOOP:
                            sNext = states[indexState];
                            break;
                        default:
                            break;
                    }

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
}