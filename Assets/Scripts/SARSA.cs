using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SARSA : MonoBehaviour
{
    [SerializeField]
    private List<State> states;
    [SerializeField]
    private float gamma = 0.9f;
    [SerializeField]
    private float alpha = 0.5f;

    private Dictionary<State, float> Q;
    private Dictionary<State, Action> policy;

    private void Start()
    {
        RunEpisode(1000);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
    }

    // Init Value Function (Q)
    private void InitValueFunction()
    {
        Q = new Dictionary<State, float>();
        policy = new Dictionary<State, Action>();
        foreach (State currentState in states)
            Q[currentState] = 0f;
    }

    private void UpdateQ()
    {
        int currentState = 0;

        // Choisir une action a basée sur une politique (pour Sprime)
        // /!\ j'ai choisi une action random
        Action currentAction = new Action(Direction.TOP);

        while (true)
        {
            // Effectuer l'action a, 
            // observer la récompense r
            // atteindre le nouvel état s'
            State nextState = currentAction.GetNextState(states, currentState);

            // Cast State in index
            for (int j = 0; states[j].name != nextState.name; j++) currentState = j;
            currentState += 1;

            // Choisir une action a basée sur une politique (pour new currentState)
            // /!\ j'ai choisi une action random
            Action nextAction = new Action(Direction.RIGHT);
            nextState = nextAction.GetNextState(states, currentState);

            // Cast State in index
            int indexNextState = 0;
            for (int p = 0; states[p].name != nextState.name; p++) indexNextState = p;

            // Update Q
            float delta = currentAction.reward + gamma * Q[nextState] - Q[states[currentState]];
            Q[states[currentState]] += alpha * delta;

            // Mettre à jour l'état actuel s <- s'
            currentState = indexNextState;
            // Mettre à jour l'action actuelle a <- a'
            currentAction = nextAction;

            if (states[currentState].name == "Objectif") break;
        }
    }

    private void RunEpisode(uint runNumber = 0)
    {
        InitValueFunction();
        for (int i = 0; i <= runNumber; i++) UpdateQ();
    }
}