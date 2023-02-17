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
    public Direction direction;
    public int reward;
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
