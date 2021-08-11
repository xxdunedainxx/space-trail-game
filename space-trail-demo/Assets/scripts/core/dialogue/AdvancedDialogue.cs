using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core.dialogue
{

    public class AdvancedDialogue
    {
        public float dialogueTime = 0;
        public Queue<AdvancedDialogueSentence> sentences;

        public AdvancedDialogue(Queue<AdvancedDialogueSentence> sentences, float dialogueTime = 0)
        {
            this.sentences = sentences;
            this.dialogueTime = dialogueTime;
        }

        public bool RunNextDialogue()
        {
            if (this.sentences.Count > 0)
            {
                AdvancedDialogueSentence nextSentence = this.sentences.Dequeue();
                nextSentence.ExecuteAdvancedDialogue();
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    public class AdvancedDialogueSentence {

        protected string sentence;
        protected float printTime;

        public AdvancedDialogueSentence(string sentence, float time = 0)
        {
            this.sentence = sentence;
            this.printTime = time;
        }

        public virtual void ExecuteAdvancedDialogue()
        {
            if (this.printTime == 0)
            {
                DialogManager.instance.PrintSentence(this.sentence);
            }
            else
            {
                DialogManager.instance.PrintSentence(this.sentence, this.printTime);
            }
        }
    }
}
