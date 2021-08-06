using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private MovementController _moveCloudController;
    private readonly string CLOUD = "cloud-shadow";
    private Vector3 originalCloudPosition = new Vector3(1.81f, -4.56f, 0);
    private GameObject cloud;
    // Start is called before the first frame update
    void Start()
    {
        this.cloud = GameObject.Find(this.CLOUD);
        this.cloud.AddComponent<MovementController>();
        this._moveCloudController = this.cloud.GetComponent<MovementController>();
        this._moveCloudController.objectToControl = this.cloud;
        this.MoveCloud();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCloud()
    {
        StartCoroutine(this._moveCloudController.Move(5, -3.53f, new Vector2(0.15f, 0.15f), endMovementCallBack: this.ResetCloud));
    }

    public void ResetCloud()
    {
        this.cloud.transform.position = this.originalCloudPosition;
        this.MoveCloud();
    }
}
