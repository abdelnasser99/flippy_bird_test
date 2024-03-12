using System;
using UnityEngine;

public class player : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 directiontemp;
    public SpriteRenderer spriteRenderer;
    public float gravity = -9.8f;
    public float strength = 5f;
    public Sprite[] sprites;
    public Sprite[] sprites2;
    public Sprite[] sprites3;
    private int sprite_index = 0;
    public AudioClip coindsound;
    public AudioSource src;
    int selectedCarcter;
    //SaveData saveData;
    binary_highscore binary_Highscore;
    //binarycarcter carcter = new binarycarcter();
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        binary_Highscore = gameObject.AddComponent<binary_highscore>();
        //saveData = binary_Highscore.Load();
        
    }

    
    private void animateSprites()
    {
        sprite_index++;
        switch (selectedCarcter)
        {
            case 1:
                sprite_index++;
                if (sprite_index >= sprites.Length)
                {
                    sprite_index = 0;
                }
                spriteRenderer.sprite = sprites[sprite_index];
                break;
            case 2:
                sprite_index++;
                if (sprite_index >= sprites.Length)
                {
                    sprite_index = 0;
                }
                spriteRenderer.sprite = sprites2[sprite_index];
                break;
            case 3:
                sprite_index++;
                if (sprite_index >= sprites.Length)
                {
                    sprite_index = 0;
                }
                spriteRenderer.sprite = sprites3[sprite_index];
                break;
        }
    }
    private void Start(){
        selectedCarcter = dataManeger.instance.gameData.selected_carcter;
        //selectedCarcter = carcter.LoadSelectedCarcter();
        switch (selectedCarcter)
        {
            case 1:
                spriteRenderer.sprite = sprites[0];
                break;
            case 2:
                spriteRenderer.sprite = sprites2[0];
                break;
            case 3:
                spriteRenderer.sprite = sprites3[0];
                break;
        }
        InvokeRepeating(nameof(animateSprites),0.15f,0.15f);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) 
            {
                direction = Vector3.up * strength;
            }
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
        directiontemp.y = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="obstecle")
        {
            transform.position = directiontemp;
            //FindObjectOfType<GameManeger>().GameOver();
            GameManeger singleton = GameManeger.instance;
            singleton.GameOver();
        }
        else if(other.gameObject.tag == "scoring")
        {
            //FindObjectOfType<GameManeger>().increaseScore();
            GameManeger singleton = GameManeger.instance;
            singleton.increaseScore();
        }
        else if(other.gameObject.tag == "obstecle_pipe")
        {
            GameManeger singleton = GameManeger.instance;
            singleton.GameOver();
        }
        else if (other.gameObject.tag == "coin")
        {
            GameManeger singleton = GameManeger.instance;
            singleton.coinCounter();
            src.clip = coindsound;
            src.Play(); 
            Destroy(other.gameObject);
        }
    }
   
}
