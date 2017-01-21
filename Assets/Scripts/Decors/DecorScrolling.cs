using UnityEngine;
using System.Collections;

public class DecorScrolling : MonoBehaviour
{

    public float speed = -3f;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }
}
