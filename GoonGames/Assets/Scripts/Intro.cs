using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string text;
    public string emotion;
    public string background; // Example: "park", "room"

    public DialogueLine(string speaker, string text, string emotion = "neutral", string background = "")
    {
        this.speaker = speaker;
        this.text = text;
        this.emotion = emotion;
        this.background = background;
    }
}




public class Intro : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text talkerText;
    public RawImage backgroundImage;
    public Texture parkBackground;
    public Texture roomBackground;
    public Texture lobbyBackground;
    public Texture lobbyBackgroundWithPeople;
    public Texture edgeLightBackground;
    public Texture blackScreen;
    public RawImage recruiterImage;
    public Texture recruiterNeutral;
    public Texture recruiterBlushing;
    public Texture recruiterHappy;
    public Texture recruiterStern;
    public Texture friend;


    private int dialogueIndex = 0;
    private Dictionary<string, Texture> backgrounds;
    private Dictionary<string, Texture> recruiterEmotions;
    private Dictionary<string, Texture> friendEmotions;

    // Replace the string array with an array of DialogueLine objects
    private DialogueLine[] dialogue = new DialogueLine[]
    {
        new DialogueLine("Story", "There was a man named John"),
        new DialogueLine("Story", "John was a gooner"),
        new DialogueLine("Story", "His gooning was getting out of hand"),
        new DialogueLine("Story", "On a quiet night"),
        new DialogueLine("Story", "In a public park"),
        new DialogueLine("Story", "He was gooning"),
        new DialogueLine("John", "What a beautiful night"),
        new DialogueLine("John", "I love gooning"),
        new DialogueLine("Story", "Unbenownst to John there was someone watching him"),
        new DialogueLine("Recruiter", "Hello John"),
        new DialogueLine("John", "What the fuck!"),
        new DialogueLine("Recruiter", "I have noticed you often goon at the park", "blushing"),
        new DialogueLine("John", "How long have you been watching me??"),
        new DialogueLine("Recruiter", "Long enough to know that you have great potential, John."),
        new DialogueLine("John", "Potential for what?"),
        new DialogueLine("Recruiter", "For the Goon Games."),
        new DialogueLine("John", "The Goon Games? What the hell is that?"),
        new DialogueLine("Recruiter", "A competition for the most dedicated gooners. It’s a game where the stakes are high, and the prizes are... very rewarding.","happy"),
        new DialogueLine("John", "Wait, what do you mean by ’rewarding’?"),
        new DialogueLine("Recruiter", "Let’s just say, the winners walk away with more than just bragging rights.","happy"),
        new DialogueLine("Story", "John was intrigued but also skeptical. His gooning was getting out of control, but was he ready for something bigger?"),
        new DialogueLine("John", "And if I say no?"),
        new DialogueLine("Recruiter", "You’ll never get another opportunity like this again.","stern"),
        new DialogueLine("Story", "The Recruiter’s words echoed in John’s mind. Could he resist the lure of the Goon Games?"),
        new DialogueLine("John", "Alright, I’m in."),
        new DialogueLine("Recruiter","Excellent choice, John.","happy"),
        new DialogueLine("Recruiter","Call the number on the card when you get home."),
        new DialogueLine("Recruiter","We are looking forward to seeing you at the games.","happy"),
        new DialogueLine("Story", "And with that, John’s fate was sealed.", "", "black"),
        new DialogueLine("John", "I should call that number she gave me","","room"),
        new DialogueLine("Phone", "Ring ring... ring ring..."),
        new DialogueLine("Voice", "Hello John, we have been expecting your call."),
        new DialogueLine("Voice", "We’ll send a car to pick you up. Meet us at the park where you met her. Be ready."),
        new DialogueLine("John", "I guess I really am going to the Goon Games."),
        new DialogueLine("John", "I hope I see that lady with the big boobs again."),
        new DialogueLine("Story", "John waited at the park, wondering what he had gotten himself into.", "", "black"),
        new DialogueLine("John", "Why the fuck did I have to go home just for them to tell me to come back","","park"),
        //siia oleks vaja pilti vanist
        new DialogueLine("Story","A grey van stopped in front of John"),
        //siia vaja pilti vanist uks avanemas
        new DialogueLine("Story", "The doors opened slowly"),
        new DialogueLine("Story","Inside there were several people sleeping in the back and mysterious men in costumes sitting in front"),
        new DialogueLine("John", "Are you the ’goon guys’"),
        new DialogueLine("Mysterious men in costumes","..."),
        new DialogueLine("John","..."),
        new DialogueLine("Mysterious men in costumes","..."),
        new DialogueLine("John", "I will take that as a yes."),
        new DialogueLine("Story", "John gets in the car and suddenly falls asleep","","black"),
        new DialogueLine("Story", ""),
        new DialogueLine("Story", "John wakes up in a strange room full of beds, he is confused and a bit disoriented", "","lobby"),
        new DialogueLine("John", "Where the hell am I?"),
        new DialogueLine("Story", "John looks around and sees other people waking up as well"),
        new DialogueLine("John", "What is this place?"),
        new DialogueLine("Story", "John gets out of bed to look around","","lobbyWithPeople"),
        new DialogueLine("Story", "Other people are doing the same"),
        //Siia lisada lugu kus John saab kokku oma sõbraga






        new DialogueLine("Announcer", "Welcome to the Goon Games!"),
        new DialogueLine("Announcer", "Please everyone make your way through the doors to the first event area."),
    };

    void Start()
    {
        backgroundImage.texture = parkBackground;
        backgrounds = new Dictionary<string, Texture>
        {
            { "park", parkBackground },
            { "room", roomBackground },
            { "black", blackScreen },
            { "lobby", lobbyBackground },
            { "lobbyWithPeople", lobbyBackgroundWithPeople },
            { "edgeLight", edgeLightBackground }
        };
        recruiterEmotions = new Dictionary<string, Texture>
        {
            { "neutral", recruiterNeutral },
            { "blushing", recruiterBlushing },
            { "happy", recruiterHappy },
            { "stern", recruiterStern }
        };
        friendEmotions = new Dictionary<string, Texture>
        {
            { "neutral", friend }
        };

        UpdateDialogueUI();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            NextLine();
        }
    }

    public void NextLine()
    {
        if (dialogueIndex < dialogue.Length - 1)
        {
            dialogueIndex++;
            UpdateDialogueUI();
            UpdateRecruiterImage();
            UpdateRecruiterImageEmotion();
            UpdateBackground();
        }
        else
        {
            SceneManager.LoadScene("RedLightEdgeLight");
        }
    }

    private void UpdateBackground()
    {
        string bg = dialogue[dialogueIndex].background;
        if (!string.IsNullOrEmpty(bg) && backgrounds.ContainsKey(bg))
        {
            backgroundImage.texture = backgrounds[bg];
        }
    }

    private void UpdateDialogueUI()
    {
        // Update speaker and dialogue text
        talkerText.text = dialogue[dialogueIndex].speaker + ":";
        dialogueText.text = dialogue[dialogueIndex].text;
        if(dialogue[dialogueIndex].speaker == "John")
        {
            talkerText.color = Color.blue;
        }
        else if(dialogue[dialogueIndex].speaker == "Recruiter")
        {
            talkerText.color = Color.red;
        }
        else if(dialogue[dialogueIndex].speaker == "Mysterious men in costumes")
        {
            talkerText.color = Color.magenta;
        }
        else if(dialogue[dialogueIndex].speaker == "Friend")
        {
            talkerText.color = Color.green;
        }
        else
        {
            talkerText.color = Color.white;
        }
    }

    private void UpdateRecruiterImage()
    {
        string bg = dialogue[dialogueIndex].background;

        // Keep the recruiter image active unless the background is black
        if (bg == "black")
        {
            recruiterImage.gameObject.SetActive(false);
        }
        else if (dialogue[dialogueIndex].speaker == "Recruiter" || recruiterImage.gameObject.activeSelf)
        {
            recruiterImage.gameObject.SetActive(true);
        }
    }

    private void UpdateRecruiterImageEmotion()
    {
        if (dialogue[dialogueIndex].speaker == "Recruiter")
        {
            recruiterImage.gameObject.SetActive(true);
            string emotion = dialogue[dialogueIndex].emotion;

            if (recruiterEmotions.ContainsKey(emotion))
            {
                recruiterImage.texture = recruiterEmotions[emotion];
            }
            else
            {
                recruiterImage.texture = recruiterNeutral;
            }
        }
    }

    private void UpdateFriendImageEmotion()
    {
        if (dialogue[dialogueIndex].speaker == "Friend")
        {
            recruiterImage.gameObject.SetActive(true);
            string emotion = dialogue[dialogueIndex].emotion;

            if (friendEmotions.ContainsKey(emotion))
            {
                recruiterImage.texture = friendEmotions[emotion];
            }
            else
            {
                recruiterImage.texture = friend;
            }
        }
    }

    private void UpdateFriendImage()
    {
        string bg = dialogue[dialogueIndex].background;

        // Keep the recruiter image active unless the background is black
        if (bg == "black")
        {
            recruiterImage.gameObject.SetActive(false);
        }
        else if (dialogue[dialogueIndex].speaker == "Friend" || recruiterImage.gameObject.activeSelf)
        {
            recruiterImage.gameObject.SetActive(true);
        }
    }
}
