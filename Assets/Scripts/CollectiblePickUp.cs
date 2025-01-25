using UnityEngine;

public class CollectiblePickUp : PickUpBase
{
    [SerializeField] public bool optional = false;
    [SerializeField] AudioClip collectOptionalSound;

    private void Start()
    {
        if (optional)
        {
            LevelManager.Instance.scoreMaxOptional++;
        }
        else
        {
            LevelManager.Instance.scoreMax++;
        }
    }
    public override void PickUp()
    {
        if (optional)
        {
            LevelManager.Instance.scoreOptional++;
            Player.Instance.source.clip = collectOptionalSound;
        }
        else
        {
            LevelManager.Instance.score++;
            Player.Instance.source.clip = collectSound;
        }
        Player.Instance.source.Play();
    }
}
