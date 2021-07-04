using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.core.dialogue
{
    public sealed class DialogueWriter : MonoBehaviour
    {
        public Text textBox;

        public static DialogueWriter instance { get; private set; }

        public string sentenceToPrint;
        private float timer;
        private float timePerCharacter = 0.1f;
        private int characterIndex = 0;
        private bool isWriting = false;
        private bool writeToEnd = false;
        private bool autoCompletedLast = false;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Debug.unityLogger.Log("setting dialoguewriter instance");
                this.SetInstance();
            }
        }

        private void SetInstance()
        {
            instance = this;
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
                    this.autoCompletedLast = true;
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

        public void UnsetVars()
        {
            Debug.unityLogger.Log("unsetting vars");
            this.characterIndex = 0;
            this.sentenceToPrint = null;
            this.isWriting = false;
            this.writeToEnd = false;
        }

        public void PrintSentence(string sentence, float writeTimer)
        {
            this.UnsetVars();
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
            Debug.unityLogger.Log($"DialogueWriter is writing {this.isWriting}");
            return this.isWriting;
        }

        public bool AutoCompletedLast()
        {
            return this.autoCompletedLast;
        }

        public void UnsetAutoComplete()
        {
            this.autoCompletedLast = false;
            this.UnsetVars();
        }
    }
}
