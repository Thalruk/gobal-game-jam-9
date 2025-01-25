using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBubble : Bubble
{
    protected int numberOfSpikes = 10;

    [SerializeField] protected GameObject glassSpikesPrefab;
    public override void Init()
    {
        type = 2;
        ammoCost = 3;
        damage = 3;
        hp = 2;
        speed = 3;
        windSensitivity = 0.5f;
        base.Init();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void NormalAttack(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetDamage(damage);

        Destroy(gameObject);
    }
    protected override void ChargedAttack(GameObject enemy)
    {
        base.ChargedAttack(enemy);
        enemy.GetComponent<Enemy>().GetDamage(damage);
        Explode();

        Destroy(gameObject);
    }

    protected void Explode() // create spikes around bubble that fly away and deal damage
    {
        Vector2 position = new Vector2(transform.position.x - GetComponent<Rigidbody2D>().velocity.x / 5f, transform.position.y);
        for (int i = 0; i < numberOfSpikes; i++)
        {
            GameObject spike = Instantiate(glassSpikesPrefab, position, Quaternion.identity);
            spike.transform.rotation = Quaternion.Euler(0f, 0f, 360f / numberOfSpikes * i);
        }
    }

    protected override void ShieldParry(GameObject spike)
    {
        base.ShieldParry(spike);
        Vector2 pos = (Vector2)transform.position - new Vector2(0f, 0.7f);
        GameObject glassSpikeObj = Instantiate(glassSpikesPrefab, pos, Quaternion.identity);
        glassSpikeObj.GetComponent<GlassSpike>().longDist = 5f;
        float direction = spike.GetComponent<Rigidbody2D>().velocity.x;
        direction = direction < 0f ? -1f : 1f;
        glassSpikeObj.transform.rotation = Quaternion.Euler(0f, 0f, 90f * direction);
        glassSpikeObj.GetComponent<Rigidbody2D>().velocity = spike.GetComponent<Rigidbody2D>().velocity * -1;
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
