using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    player playerRef;
    // Start is called before the first frame update
    void Start()
    {
        this.playerRef = GameState.getGameState().playerReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Camera.main.transform.position = new Vector3(playerRef.gameObject.transform.position.x, playerRef.gameObject.transform.position.y, Camera.main.transform.position.z);
    }
}
