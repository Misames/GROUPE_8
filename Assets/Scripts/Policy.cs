using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Policy : MonoBehaviour
{
    
    private Action[] _policy;
    private Dictionary<State, float> V;

    private int maxPolicyIteration = 100;
    private int maxValueIteration = 100;
    
    [SerializeField]
    private float gamma = 0.9f;

    [SerializeField]
    private List<State> states;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        PolicyIteration();
        Debug.Log("Policy:");
        for (int i = 0; i < _policy.Length; i++)
        {
            Debug.Log(_policy[i].direction);
        }
        
    }

    private void Init()
    {
        Debug.Log(states.Count);
        _policy = new Action[states.Count];
        V = new Dictionary<State, float>();
        for (int i = 0; i < states.Count; i++)
        {
            V.Add(states[i],0);
            _policy[i] = states[i].actionList[Random.Range(0, states[i].actionList.Length)];
        }

    }

    private void PolicyEvaluation(float tolerance = 0.001f)
    {
        float delta;
        int it = -1;
        do
        {
            delta = 0;
            for(int i=0; i< states.Count; i++)
            {
                float val = _policy[i].reward;
                for (int j = 0; j < states.Count; j++)
                {
                    val += _policy[StatePrime(j,_policy[j])].reward + gamma * V[states[StatePrime(j,_policy[j])]];
                }

                delta = Math.Max(delta, Math.Abs(val - V[states[i]]));
                V[states[i]] = val;

            }
            it++;
        } while (delta > tolerance && it<maxValueIteration);

     }

    private Action[] PolicyImprovement()
    {
        for (int i = 0; i < maxPolicyIteration; i++)
        {Debug.Log("iteration: " + i);
            bool policyStable = true;
            
            for (int j = 0; j < states.Count; j++)
            {
                float valMax = _policy[j].reward;

                for (int k = 0; k < states[j].actionList.Length; k++)
                {
                    float val = states[j].actionList[k].reward;
                    for (int l = 0; l < states.Count; l++)
                    {
                        val += _policy[StatePrime(l,_policy[l])].reward + gamma * V[states[StatePrime(l,_policy[l])]];
                    }

                    if (val > valMax && _policy[j] != states[j].actionList[k])
                    {
                        _policy[j] = states[j].actionList[k];
                        valMax = val;
                        policyStable = false;
                    }
                }
            }

            if (!policyStable)
                PolicyEvaluation();
            else break;
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

}
