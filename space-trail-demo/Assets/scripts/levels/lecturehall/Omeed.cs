using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.core;

namespace Assets.scripts.levels.lecturehall
{
    public class Omeed : npc, IClickable
    {
        public static string THE_BOOK_OMEEDS_NOTE_IS_IN = "Intro to metereology";

        public override void click()
        {
            Debug.unityLogger.Log("Omeed was clicked?");
            if (CanInteract())
            {
                Debug.unityLogger.Log("Omeed was clicked");
                Collider2D collision = Physics2D.OverlapCircle(body.position, 0.5f, interactLayer);
                player p = collision.gameObject.GetComponent<player>();
                List<IItem> playerInv = p.getInventory();
                foreach (IItem i in playerInv)
                {
                    Debug.unityLogger.Log("Looping using inventory");
                    if (i.name() == Omeed.THE_BOOK_OMEEDS_NOTE_IS_IN)
                    {
                        Dialog d = new Dialog(new List<string> { "Oh... it looks like you found my note in the book, i'll be taking that" });
                        this.interact(d);
                        LectureHall l = GameObject.Find("LectureHall").GetComponent<LectureHall>();
                        l.completeLevel();
                        return;
                    }
                }
                this.interact(this.dialog);
            }
        }

        private void interact(Dialog d)
        {
            DialogManager manager = DialogManager.instance;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = this.interactImage;
            manager.StartDialogue(d);
        }
    }
}