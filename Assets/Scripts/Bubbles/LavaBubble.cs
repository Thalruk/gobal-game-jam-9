using UnityEngine;

public class LavaBubble : Bubble
{

    [SerializeField] public GameObject lavaSpillPrefab;
    public override void Init()
    {
        type = 3;
        ammoCost = 4;
        damage = 4;
        hp = 3;
        speed = 3;
        windSensitivity = 0.3f;
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
        SpillOnFloor();

        Destroy(gameObject);
    }

    protected void SpillOnFloor() // spill lava on floor witch deal damage over time
    {
        GameObject lavaSpill = Instantiate(lavaSpillPrefab, transform.position, Quaternion.identity);
    }

    protected override void ShieldParry(GameObject spike)
    {
        base.ShieldParry(spike);
    }

    private void Start()
    {
    }
}
