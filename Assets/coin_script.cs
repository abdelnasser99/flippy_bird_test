
using UnityEngine;

public class coin_script : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdge;
    //public AudioClip coindsound;
    //public AudioSource src;
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }
    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManeger.instance.coinCounter();
            //src.clip = coindsound;
            //src.Play();
            Destroy(this.gameObject);
        }
    }
}
