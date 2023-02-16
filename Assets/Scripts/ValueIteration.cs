using System.Collections.Generic;
using UnityEngine;

public class ValueIteration : MonoBehaviour
{
    public float gamma = 0.9f;
    public int numIterations = 100;
    public float tolerance = 0.01f;

    private Dictionary<State, float> V;
    private Dictionary<State, float> Vprime;
    private Dictionary<State, Action> policy;

    public void Solve(MDP mdp)
    {
        // Initialiser la fonction de valeur V
        V = new Dictionary<State, float>();
        Vprime = new Dictionary<State, float>();
        policy = new Dictionary<State, Action>();
        foreach (State s in mdp.GetStates())
        {
            V[s] = 0f;
            Vprime[s] = 0f;
        }

        // Itérer jusqu'à la convergence
        for (int i = 0; i < numIterations; i++)
        {
            float delta = 0f;
            foreach (State s in mdp.GetStates())
            {
                // Trouver la meilleure action
                float maxV = float.MinValue;
                Action maxA = null;
                foreach (Action a in mdp.GetActions(s))
                {
                    float value = 0f;
                    foreach (KeyValuePair<State, float> pair in mdp.GetTransitions(s, a))
                    {
                        State sNext = pair.Key;
                        float prob = pair.Value;
                        float reward = mdp.GetReward(s, a, sNext);
                        value += prob * (reward + gamma * V[sNext]);
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
            }
            // Vérifier la convergence
            if (delta < tolerance)
            {
                break;
            }
            // Mettre à jour la fonction de valeur V
            foreach (State s in mdp.GetStates())
            {
                V[s] = Vprime[s];
            }
        }
    }

    // Exemple de classe pour les états et les actions d'un MDP
    public class State
    {
        public int x;
        public int y;
    }
    public class Action
    {
        public int dx;
        public int dy;
    }

    // Exemple de classe pour un MDP simple
    public class SimpleMDP : MDP
    {
        public override IEnumerable<State> GetStates()
        {
            // Renvoyer une liste d'états
            // ...
            return null;
        }

        public override IEnumerable<Action> GetActions(State s)
        {
            // Renvoyer une liste d'actions pour un état donné
            // ...
            return null;
        }

        public override Dictionary<State, float> GetTransitions(State s, Action a)
        {
            // Renvoyer un dictionnaire de transitions pour un état et une action donnés
            // ...
            return null;
        }

        public override float GetReward(State s, Action a, State sNext)
        {
            // Renvoyer la récompense pour une transition donnée
            // ...
            return 0f;
        }
    }

    // Interface pour un MDP
    public abstract class MDP
    {
        public abstract IEnumerable<State> GetStates();
        public abstract IEnumerable<Action> GetActions(State s);
        public abstract Dictionary<State, float> GetTransitions(State s, Action a);
        public abstract float GetReward(State s, Action a, State sNext);
    }
}