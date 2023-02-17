using UnityEngine;

public class Action : MonoBehaviour
{
    public int reward = 0;
    public Direction moveDirection;
}

public enum Direction
{
    TOP,
    LEFT,
    RIGHT,
    BOT
}
