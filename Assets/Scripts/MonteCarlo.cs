using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonteCarlo : MonoBehaviour
{
    private Dictionary<State,Action> _policy;
    private float[] G;
    private Dictionary<State, float> V;
    private Dictionary<State, int> N;
    private Dictionary<State, int> Returns;
    
    [SerializeField]
    private State firstState;

    [SerializeField]
    private float gamma = 0.75f;

    [SerializeField]
    private List<State> states;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
        MonteCarloES();
        Debug.Log("Policy:");
        for (int i = 0; i < _policy.Count; i++)
        {
            if(_policy[states[i]]!=null) 
                Debug.Log(_policy[states[i]].direction);
            else
                Debug.Log(_policy[states[i]]);
        }
        
    }

    private void Init()
    {
        Debug.Log(states.Count);
        _policy = new Dictionary<State, Action>();
        V = new Dictionary<State, float>();
        N = new Dictionary<State, int>();
        G = new float[100];
        for (int i = 0; i < states.Count; i++)
        {
            V.Add(states[i],0);
            N.Add(states[i],0);
            if (states[i].actionList.Length > 0)
                _policy[states[i]] = states[i].actionList[Random.Range(0, states[i].actionList.Length)];
            else
                _policy[states[i]] = null;
        }

    }

    private void MonteCarloESImprovement(int episodes)
    {
        
    }

    private void MonteCarloFVImprovement(int episodes)
    {
        for (int i = 0; i<states.Count; i++)
        {
            N[states[i]] = 0;
            Returns[states[i]] = 0;
        }

        for (int i = 0; i < episodes; i++)
        {
            G = new float[100];
            EnvReset();
            State currentState = firstState;
            bool done = false;
            while (!done)
            {
                Action a = _policy[currentState];
            }
        }
        
    }
    
    private void MonteCarloEVImprovement(int episodes)
    {
        
    }
    private void MonteCarloES()
    {
        Init();
        
        MonteCarloESImprovement(1000);
    }

    private void MonteCarloFV()
    {
        Init();
        MonteCarloFVImprovement(1000);

    }
    
    private void MonteCarloEV()
    {
        Init();
        MonteCarloEVImprovement(1000);
    }

    private void EnvReset()
    {
        
    }
    private int StatePrime(int s, Action a)
    {
        switch (a.direction)
        {
            case Direction.TOP:
                return s + 4;
            case Direction.RIGHT:
                return s + 1;
            case Direction.LEFT:
                return s - 1;
            case Direction.BOTTOM:
                return s - 4;
            case Direction.LOOP:
                return s;
        }

        return 0;
    }

    private float RewardPrime(int s, Action a, int sPrime)
    {
        int stateAction = StatePrime(s, a);
        if (stateAction == sPrime)
            return a.reward;
        return 0;
    }
    
    
}
