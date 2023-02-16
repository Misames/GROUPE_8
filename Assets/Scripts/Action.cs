using UnityEngine;

enum Direction
{
    TOP,
    LEFT,
    RIGHT,
    BOT
}

public class Action : MonoBehaviour
{
    public int reward = 0;
    public float probabiliy = 0f;
    public int moveDirection;
}
