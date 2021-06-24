using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;

namespace Assets.scripts.core.objects
{
    public class Bookshelf : ObtainableObject, IClickable
    {
        [SerializeField]
        public ItemCollection books = null;
        [SerializeField]
        public List<Sprite> bookShelfImages = null;
        [SerializeField]
        public LayerMask interactLayer;
        [SerializeField]
        public Transform body;
        [SerializeField]
        public string name = "bookshelf";

        List<ObjectObtainedEvent> events;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public bool CanInteract()
        {
            Collider2D interactChecks = Physics2D.OverlapCircle(body.position, .5f, interactLayer);

            if (interactChecks != null)
            {
                Debug.unityLogger.Log($"Colided with :{interactChecks.attachedRigidbody.gameObject.name}");
                return true;
            }
            return false;
        }

        public void click()
        {
            if (CanInteract())
            {
                if (this.books.items.Count > 0)
                {
                    for (int i = 0; i < this.books.items.Count; i++)
                    {
                        SingleItem bookToReturn = this.books.items[i];
                        Debug.unityLogger.Log($"User obtained item from bookshelf {bookToReturn.name()}");
                        
                        if (this.bookShelfImages.Count > 0)
                        {
                            this.gameObject.GetComponent<SpriteRenderer>().sprite = this.bookShelfImages[0];
                            this.bookShelfImages.RemoveAt(0);
                        }
                        
                    }
                    this.associatedAnimation.disableAnimation();
                    this.books.items.Clear();
                }
                else
                {
                    Debug.unityLogger.Log("No more books to give :(");
                }
            }
            else
            {
                Debug.unityLogger.Log("USer is not close enough for interaction..");
            }
        }
    }
}
