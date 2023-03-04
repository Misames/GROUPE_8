using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    TOP,
    RIGHT,
    LEFT,
    BOTTOM
}

[System.Serializable]
public class Action
{
    public int reward;
    public Direction direction;

    public Action(Direction d)
    {
        reward = 1;
        direction = d;
    }

    // Give the next state for one action on a state
    // @todo rendre les actions génériques ou ajouter des action d'autre type que TOP LEFT RIGHT BOT
    public State GetNextState(List<State> states, int indexState)
    {
        State nextState = null;

        switch (direction)
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
};

public class State : MonoBehaviour
{
    [SerializeField]
    public Action[] actionList;

    private void Start()
    {
        foreach (var action in actionList)
        {
            transform.GetChild(0).Find(action.direction.ToString()).GetComponent<Text>().text =
                action.reward.ToString();
        }
    }

}
