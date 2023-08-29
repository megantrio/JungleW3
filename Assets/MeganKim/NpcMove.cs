using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NpcMove : MonoBehaviour
{
    private Vector2 deskPos = new Vector2 (0,-2);

    private void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {       
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, deskPos, 0.02f);
            yield return null;
        }
    }

    //UIManager 통해서 이벤트 수행하는 로직
}
