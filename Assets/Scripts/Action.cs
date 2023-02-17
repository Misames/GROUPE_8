using UnityEngine;

public enum Direction
{
    TOP,
    LEFT,
    RIGHT,
    BOT
}

public class Action : MonoBehaviour
{
    public int reward = 0;
    public Direction moveDirection;
}
