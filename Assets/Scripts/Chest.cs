using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Sprite[] ammoSprites;
    [SerializeField] Sprite ammoBottle;
    [SerializeField] Sprite openChest;
    [SerializeField] int ammoType = 1;
    float showTime = 0f;
    bool isShowing = false;
    bool fading = false;
    GameObject ammo, bottle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            showTime = 0f;
            GetComponent<SpriteRenderer>().sprite = openChest;
            ammo = new GameObject();
            ammo.AddComponent<SpriteRenderer>();
            bottle = new GameObject();
            bottle.AddComponent<SpriteRenderer>();
            ammo.transform.position = transform.position;
            bottle.transform.position = transform.position;
            bottle.transform.SetParent(ammo.transform);
            ammo.transform.position += new Vector3(0f, 0f, -0.1f);
            ammo.GetComponent<SpriteRenderer>().sprite = ammoSprites[ammoType];
            bottle.GetComponent<SpriteRenderer>().sprite = ammoBottle;
            ammo.transform.localScale = Vector2.zero;
            isShowing = true;

            ammo.GetComponent<SpriteRenderer>().sortingOrder = 1;
            bottle.GetComponent<SpriteRenderer>().sortingOrder = 1;

            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        if (isShowing)
        {
            ammo.transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + Vector2.up, showTime);
            ammo.transform.localScale = Vector2.Lerp(Vector2.zero, new Vector2(2f, 2f), showTime);
            ammo.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, showTime);
            bottle.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, showTime);
            showTime += Time.deltaTime;

            if (showTime > 1f)
            {
                showTime = 1f;
                isShowing = false;
                fading = true;
                Player.Instance.ammo[ammoType] = Player.Instance.maxAmmo;
                Player.Instance.ammoBottleImage.sprite = Player.Instance.ammoBottleImages[ammoType];
                Player.Instance.ammoFillImage.sprite = Player.Instance.ammoFillImages[ammoType];
                Player.Instance.ammoSlider.value = Player.Instance.ammo[ammoType];
                Player.Instance.activeBubble = ammoType;

            }
        }
        if (fading)
        {
            ammo.transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + Vector2.up, showTime);
            ammo.transform.localScale = Vector2.Lerp(Vector2.zero, new Vector2(2f, 2f), showTime);
            ammo.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, showTime);
            bottle.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, showTime);
            showTime -= Time.deltaTime;

            if (showTime < 0f)
            {
                fading = false;
                showTime = 0f;
                Destroy(ammo);
            }
        }
    }
}
