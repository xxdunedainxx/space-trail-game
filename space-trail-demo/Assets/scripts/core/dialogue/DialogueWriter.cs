using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.core.dialogue
{
    public class DialogueWriter : MonoBehaviour
    {
        [SerializeField]
        public Text textBox;

        public string sentenceToPrint;
        private float timer;
        private float timePerCharacter = 0.1f;
        private int characterIndex = 0;
        private bool isWriting = false;
        private bool writeToEnd = false;

        public DialogueWriter(Text t)
        {
            this.textBox = t;
        }

        public DialogueWriter()
        {

        }

        private void Update()
        {
            Debug.unityLogger.Log("writing sentence???");
            if (textBox != null && this.sentenceToPrint != null)
            {
                if (writeToEnd == true)
                {
                    this.textBox.text = this.sentenceToPrint;
                    this.UnsetVars();
                }
                else
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                    {
                        this.textBox.text = this.sentenceToPrint.Substring(0, characterIndex);
                        timer += timePerCharacter;
                        characterIndex++;
                    }

                    if (characterIndex == this.sentenceToPrint.Length)
                    {
                        this.UnsetVars();
                    }

                }
            }
        }

        private void UnsetVars()
        {
            this.characterIndex = 0;
            this.sentenceToPrint = null;
            this.isWriting = false;
            this.writeToEnd = false;
        }

        public void PrintSentence(string sentence, float writeTimer)
        {
            this.timePerCharacter = writeTimer;
            Debug.unityLogger.Log("printing sentence???");
            this.sentenceToPrint = sentence;
            this.isWriting = true;
            this.Update();
        }

        public void SetToEnd()
        {
            this.writeToEnd = true;
        }

        public bool IsWriting()
        {
            Debug.unityLogger.Log("DialogueWriter is writing");
            return this.isWriting;
        }
    }
}
