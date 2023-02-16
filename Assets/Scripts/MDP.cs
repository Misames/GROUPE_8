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
    private int indexState = 0;

    private void Start()
    {
        ValueIteration();
    }

    private void ValueIteration(uint maxIteration = 1000, float gamma = 0.9f, float delta = 0f)
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
        while (iteration < maxIteration && delta < 0.005f)
        {
            delta = 0f;
            indexState = 0;
            foreach (var s in states)
            {
                // Find best action desicion
                float maxV = float.MinValue;
                Action maxA = null;
                foreach (var a in s.lstAction)
                {
                    // Fetch each transitions for one action
                    float value = 0f;
                    foreach (KeyValuePair<State, float> pair in GetTransitions(s, a))
                    {
                        State sNext = pair.Key;
                        float prob = pair.Value;
                        value += prob * (a.reward + gamma * V[sNext]);
                    }

                    if (value > maxV)
                    {
                        maxV = value;
                        maxA = a;
                    }
                }

                policy[s] = maxA;
                Vprime[s] = maxV;
                delta = Mathf.Max(delta, Mathf.Abs(Vprime[s] - V[s]));
                indexState++;
            }

            // Update Value Function
            foreach (var s in states) V[s] = Vprime[s];

            iteration++;
        }
    }

    // Get list of action for the next move
    private Dictionary<State, float> GetTransitions(State s, Action a)
    {
        Dictionary<State, float> probTransitions = new Dictionary<State, float>();
        switch (a.moveDirection)
        {
            case 0:
                probTransitions.Add(states[indexState + 4], a.probabiliy);
                break;
            case 1:
                probTransitions.Add(states[indexState - 1], a.probabiliy);
                break;
            case 2:
                probTransitions.Add(states[indexState + 1], a.probabiliy);
                break;
            case 3:
                probTransitions.Add(states[indexState - 4], a.probabiliy);
                break;
            default:
                break;
        }
        return probTransitions;
    }
}
