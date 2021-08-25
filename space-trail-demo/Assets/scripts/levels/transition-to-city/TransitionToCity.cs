using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.ui.asset_loaders;

namespace Assets.scripts.levels
{
    public class TransitionTocity : Level
    {
        readonly private string flyerTag = "flyer";

        public TransitionTocity() : base("TransitionTocity", false)
        {
            Debug.unityLogger.Log("TransitionTocity constructor");
        }

        public override void startLevel()
        {
            base.startLevel();
            Debug.unityLogger.Log("start transition to city...");
            this.DetermineFlyers();
        }

        private void DetermineFlyers()
        {
            
            string storyLineChose = GameState.getGameState().gsStore.STORY_LINE_CHOSEN;

            Debug.unityLogger.Log($"story chose {storyLineChose}");

            if (storyLineChose != "")
            {
                GameObject[] flyerObjects = GameObject.FindGameObjectsWithTag(this.flyerTag);

                foreach(GameObject flyer in flyerObjects)
                {
                    SpriteRenderer flyersprite = flyer.GetComponent<SpriteRenderer>();
                    flyersprite.sprite = SpriteLoader.FetchSprite(storyLineChose);
                    flyersprite.enabled = true;
                }
            }
            else
            {
                Debug.unityLogger.Log("no story chosen yet..");
            }
        }
    }
}
