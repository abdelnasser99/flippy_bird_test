using UnityEngine;

public class player : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 directiontemp;
    private SpriteRenderer spriteRenderer;
    public float gravity = -9.8f;
    public float strength = 5f;
    public Sprite[] sprites;
    private int sprite_index = 0;
    private void Awake()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    private void animateSprites()
    {
        sprite_index++;
        if(sprite_index >= sprites.Length){
            sprite_index=0;
        }
        spriteRenderer.sprite = sprites[sprite_index];
    }
    private void Start(){
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
            GameManeger singleton = GameManeger.Instance;
            singleton.GameOver();
        }
        else if(other.gameObject.tag == "scoring")
        {
            //FindObjectOfType<GameManeger>().increaseScore();
            GameManeger singleton = GameManeger.Instance;
            singleton.increaseScore();
        }
        else if(other.gameObject.tag == "obstecle_pipe")
        {
            GameManeger singleton = GameManeger.Instance;
            singleton.GameOver();
        }
    }
   
}
