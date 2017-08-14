using System;
using System.Collections;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.Client;
using TwitchLib.Models.API.v5;
using TwitchLib.Services;
using UnityEngine;
using TwitchLib.Models.API.v5;
using System.Threading.Tasks;
using Assets;

public class Message : MonoBehaviour {

    public Sprite[] sprites;
    public GameObject videoplayer;
    public GameObject head;

    private GameObject clone;
    private TwitchClient client;
    private bool ifSetup = false;
    private bool drop = false;
    private string spriteAddress = "";
    private bool sprite = false;
    private System.Random ran;
    private TwitchLib.Models.API.v5.Channels.ChannelFollowers f;

    void Start()
    {
        ran = new System.Random();
        

    }

    private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        var chat = e.ChatMessage;
        var emotes = chat.EmoteSet;
        Debug.Log(e.ChatMessage.Message);
        EmoteCheck(emotes);
        
    }

    private void EmoteCheck(EmoteSet set)
    {
        var emotes = set.Emotes;
        if (emotes.Count > 0)
        {
            for (int i = 0; i < emotes.Count; i++)
            {

                spriteAddress = emotes[i].ImageUrl;
                sprite = true;
                System.Threading.Thread.Sleep(1500);


            }
        }
    }

    private void Drop()
    {
        var x = ran.Next(6);
        var neg = ran.Next(100);
        if (neg % 2 == 0) x = -x;
        this.GetComponent<Transform>().position = new Vector3(x, 6.6f, 50);
        clone = Instantiate(head, transform.position, Quaternion.identity) as GameObject;
        Destroy(clone, 120);
    }

    IEnumerator LoadSprite()
    {
       
           // Debug.Log("LoadSprite Called");
            if (Application.internetReachability == NetworkReachability.NotReachable)
                yield return null;
            var www = new WWW(spriteAddress);
            yield return www;
            if (string.IsNullOrEmpty(www.text))
                Debug.Log("Download failed");
            else
            {
                Texture2D texture = new Texture2D(1,1);
                www.LoadImageIntoTexture(texture);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one);
                
                head.GetComponent<SpriteRenderer>().sprite = sprite;
                head.GetComponent<CapsuleCollider2D>().size = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
                head.GetComponent<CapsuleCollider2D>().offset = new Vector2(sprite.bounds.size.x / -2, sprite.bounds.size.y / -2);
                this.sprite = false;
                drop = true;
            }
       
    }

    public async Task Test1Async()
    {
        f = await TwitchLib.TwitchAPI.Channels.v5.GetChannelFollowersAsync(TwitchInfo.Channel);
    }

    void Update () {
        if (!ifSetup)
        {
            if(Main.GetTwitchClient() != null)
            {
                Debug.Log("Setting up Client");
                client = Main.GetTwitchClient();
                client.OnMessageReceived += Client_OnMessageReceived;
                ifSetup = true;
            }
        }

        if (drop)
        {
            Drop();
            drop = false;
        }

        if (Input.GetKeyDown("d"))
        {

            Drop();
            drop = false;
        }
        if (sprite)
        {
            //Debug.Log("Got to here");
            StartCoroutine(LoadSprite());
            sprite = false;
        }

        if (f != null) Debug.Log("It worked");
    }

    private void ChatCommands(OnMessageReceivedArgs e)
    {
        var messageBody = e.ChatMessage.Message.ToLower();
        var userName = e.ChatMessage.DisplayName;

        if (messageBody.StartsWith("!"))
        {
            switch (messageBody)
            {
                case "!uptime":
                    Uptime(userName);
                    break;
            }
        }

    }

    private void Uptime(string userName)
    {
        
    }
}
