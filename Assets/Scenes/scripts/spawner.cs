
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject coin;
    public float spawnRate=1f;
    public float coinspawnRate;
    public float minHight=-1f;
    public float maxHight=1f;
    float x,y;
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn),spawnRate,spawnRate);
        InvokeRepeating(nameof(SpawnCoin), 1.5f, 1.5f);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
        CancelInvoke(nameof(SpawnCoin));
    }
    private void Spawn()
    {
        //x = Random.Range(minHight, maxHight);
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * x;
        /*GameObject Coin = Instantiate(coin, transform.position, Quaternion.identity);
        Coin.transform.position += Vector3.up * x;*/
    }
    private void SpawnCoin()
    {
        //y = Random.Range(-0.7f, 0.3f);
        GameObject Coin = Instantiate(coin, transform.position, Quaternion.identity);
        Coin.transform.position += Vector3.up * x;
    }
    private void Update()
    {
        x = Random.Range(minHight, maxHight);
    }
}
