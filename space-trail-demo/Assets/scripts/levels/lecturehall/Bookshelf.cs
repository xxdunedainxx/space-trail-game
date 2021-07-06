using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Assets.scripts.core;
using Assets.scripts.core.objects;

namespace Assets.scripts.levels.lecturehall
{
    public class Bookshelf : MonoBehaviour, IClickable
    {
        public BasicItemCollection books = null;
        [SerializeField]
        public Sprite defaultBookShelf;
        [SerializeField] 
        public Sprite emptyBookShelf;
        public LayerMask interactLayer;
        [SerializeField]
        public Transform body;
        [SerializeField]
        public string name = "bookshelf";

        public IEvent attachedEvent = null;

        List<ObjectObtainedEvent> events;

        public void Awake()
        {
           this.interactLayer = Layers.PLAYER_LAYER;
        }

        public bool CanInteract()
        {
            Debug.unityLogger.Log("Determing if they can click the book shelf...");
            Collider2D interactChecks = Physics2D.OverlapCircle(body.position, 2f, interactLayer);

            if (interactChecks != null)
            {
                Debug.unityLogger.Log($"Colided with :{interactChecks.attachedRigidbody.gameObject.name}");
                return true;
            }
            return false;
        }

        public void click()
        {
            Debug.unityLogger.Log("clicked on bookshelf?");
            if (CanInteract())
            {
                Debug.unityLogger.Log("User interacted with bookshelf");
                if (this.attachedEvent != null)
                {
                    this.attachedEvent.execute();
                }
            }
            else
            {
                Debug.unityLogger.Log("User is not close enough for interaction..");
            }
        }

        public void makeBookshelfEmpty()
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = this.emptyBookShelf;
        }
       
    }
}