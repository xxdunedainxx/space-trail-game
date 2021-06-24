using Assets.scripts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.levels.lecturehall
{
    public class InvisibleBlock: LevelBlocker
    {
        public bool Blocking = true;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //collision.collider.gameObject.layer
            //Collider2D interactChecks = Physics2D.OverlapCircle(body.position, 0.5f, interactLayer);
            if (Blocking)
            {
                DialogManager manager = DialogManager.instance;
                manager.StartDialogue(this.dialog);
            } else
            {
                Level.levelTransition("hallway");
            }
        }
    }
}
