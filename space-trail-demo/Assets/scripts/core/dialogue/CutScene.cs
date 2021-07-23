using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core.dialogue
{

    class CutScene
    {
        public Dialog dialogue;
        private player playerRef;

        public CutScene()
        {
            this.GetPlayerReference();
        }

        public CutScene(Dialog d)
        {
            this.dialogue = d;
            this.GetPlayerReference();
        }

        private void GetPlayerReference()
        {
            this.playerRef = GameState.getGameState().playerReference;
        }

        public void RunCutScene()
        {
            this.FreezePlayer();
            DialogManager dialogue = DialogManager.instance;
            dialogue.StartDialogue(this.dialogue, this.UnfreezePlayer);
        }


        private void FreezePlayer()
        {
            this.playerRef.FreezePlayer();
        }

        public void UnfreezePlayer()
        {
            Debug.unityLogger.Log("unfreezing player");
            this.playerRef.UnFreezePlayer();
        }
    }
}
