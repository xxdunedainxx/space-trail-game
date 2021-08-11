using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    public static ClickManager instance { get; private set; }

    [SerializeField]
    public Camera cam;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            ClickManager.instance = this;
        }
    }

        private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            Debug.unityLogger.Log("ClickManager is detecting if a clickable was clicked...");

            if(hit)
            {
                Debug.unityLogger.Log("something hit?");
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                Debug.unityLogger.Log($"Clicked {clickable} { hit.collider.gameObject}");
                clickable?.click();
            }
        }
    }
}
