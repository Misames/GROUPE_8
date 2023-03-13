using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Policy : MonoBehaviour
{
    
    private Action[] _policy;
    private Dictionary<State, float> V;

    private int maxPolicyIteration = 1000;
    private int maxValueIteration = 100;
    
    
    
    [SerializeField]
    private float gamma = 0.75f;

    
    [SerializeField]
    private int startingCube;
    
    [SerializeField]
    private List<State> states;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        PolicyIteration();
        Debug.Log("Policy:");
        for (int i = 0; i < _policy.Length; i++)
        {
            if(_policy[i]!=null) 
                Debug.Log(_policy[i].direction);
            else
                Debug.Log(_policy[i]);
        }
        
    }
    
    private void OnDrawGizmosSelected()
    {
        bool done = false;
        int i = startingCube;
        
    }

    private void Init()
    {
        Debug.Log(states.Count);
        _policy = new Action[states.Count];
        V = new Dictionary<State, float>();
        for (int i = 0; i < states.Count; i++)
        {
            V.Add(states[i],0);
            if (states[i].actionList.Length > 0)
                _policy[i] = states[i].actionList[Random.Range(0, states[i].actionList.Length)];
            else
                _policy[i] = null;
        }

    }

    private void PolicyEvaluation(float tolerance = 0.001f)
    {
        float delta;
        int it = -1;
        delta = 0;

            for(int i=0; i< states.Count; i++)
            {
                if (states[i].actionList.Length == 0)
                    continue;
                
                float v = V[states[i]];
                float val = 0;
                for (int j = 0; j < states.Count; j++)
                {
                    val += RewardPrime(i,_policy[i],j) + gamma * V[states[j]];
                }

                delta = Math.Max(delta, Math.Abs(v - val));
                V[states[i]] = val;

            }
            it++;
            Debug.Log(delta);
    }

    private Action[] PolicyImprovement()
    {
        for (int i = 0; i < maxPolicyIteration; i++){
            bool policyStable = true;
            
            PolicyEvaluation();
            for (int j = 0; j < states.Count; j++)
            {
                if (states[j].actionList.Length == 0)
                    continue;
                float valMax = V[states[j]];

                for (int k = 0; k < states[j].actionList.Length; k++)
                {
                    float val = 0;
                    for (int l = 0; l < states.Count; l++)
                    {
                        val += RewardPrime(j,states[j].actionList[k],l) + gamma * V[states[l]];
                    }

                    if (val > valMax)
                    {
                        Debug.Log("policy changed" + " state: " + j + ", val: " + val +", valMax: " + valMax);
                        _policy[j] = states[j].actionList[k];
                        valMax = val;
                        policyStable = false;
                    }
                }
            }
            if (policyStable)
                break;
        }

        return _policy;
    }

    private void PolicyIteration()
    {
        Init();
        PolicyImprovement();
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
