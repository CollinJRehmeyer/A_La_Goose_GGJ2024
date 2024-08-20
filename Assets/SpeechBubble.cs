using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    string[] quips = { "I can feed my whole with this job!",
        "Hey boss man!",
        "You smell REALLY nice!",
        "I really needed this job, thanks again!",
        "What can I do for you?",
        "I needed a break, what is it?",
        "Rise and grind! What can ya do?",
        "I didn't get enough sleep last night...?",
        "My department smells like fish and cream cheese",
        "I could REALLY use a transfer to another department",
        "What time is it? Can I clock out yet?",
        "You look like you could really use some air",
        "What the F&@K do you want?",
        "Where the H#!L are we? Where's my family?",
        "Did anyone ever tell you how paper thin your skin looks?",
        "This must be the wrong floor",
        "I only eat icecream in Tuesdays",
        "I can't wait to see my family again!",
        "Thanks for the invite boss! But the princess is in another department",
        "I used to be like you...",
        "This isn't the break room",
        "Business... Business never changes",
        "Do a Pay-roll!",
        "My old boss died of dysentery",
        "I like shorts! They're comfy and easy to wear!",
        "It ain't no secret I didn't get these scars falling over in church",
        "Waka Waka Waka",
        "The donuts were a lie",
        "I was told to get a silk bag from the breakroom duck to live longer",
        "Lightning bolt Lightning bolt Lightning bolt",
        "The NUMBERS Boss. What do the NUMBERS mean?",
        "This job is a real trial by FIRE!",
        ">.>",
        "Thanks for putting up with me!",
        "Thanks for being the Boss. Without you, I'd probably be out of a job!",
        "Would you look at that, I'm 2D!",
        "Look how high up we are!",
        ":3",
        ">:3",
        "I think this is just the beginning...",
        "Would you like to play a game?",
        "I'm not bad! I'm was just generated this way!",
        "This world is so fuckin fucked up!",
        "I found this badass store called Dan Flashes, that's my exact style",
        "I'm not worried about this, I'm not worried about anything!",
        "Could we run over my brother-in-law's office next?",
        "Wow Boss I never realized how handsome/beautiful you are",
        "This one time, a massive videogame busniess ran over my company HQ",
        "What I do? I took your shoe!",
        "This Ain't Big Enough!",
        "Let's run over A La Goose next!",
        "I have an idea. Let's take our business... and move it over there!",
        "Oh, now give me a second. I tied my shoes together in the elevator",
        "My goodness what a bumpy road. Maybe its something trying to force it's way into our world",
        "To be honest with you boss, I didn't take my medication. I'm having difficulty seeing right now",
        "My previous company sold 100% pure ice. That was something people could count on",
        "Me and a few colleagues how come up with a few new dipping sauce ideas",
        "Have you seen that Chicken that asks everyone for gum? A person can only give out so much gum",
        "You got any gum? I'm not a chicken",
        "Business! Am I right?",
        "THUNDER! THUNDER! THUNDER BOSS!",
        "I could write a book about what you don't know" };

    public TMP_Text quipText;
    public StudioEventEmitter sfx;

    public void GenerateQuip()
    {
        string q = quips[Random.Range(0, quips.Length)];
        quipText.text = q;
        sfx.Play();
    }

    public void SetQuip(string quip)
    {
        quipText.text = quip;
        sfx.Play();
    }

    private void Awake()
    {
       sfx = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        //GenerateQuip();
    }
}
