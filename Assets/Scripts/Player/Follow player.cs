using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followplayer : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z-10f);
    }
}
