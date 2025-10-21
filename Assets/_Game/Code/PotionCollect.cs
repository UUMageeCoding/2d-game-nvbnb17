using UnityEngine;

public class PotionCollect : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSoundClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created




    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Potion Collected");


        if (collision.CompareTag("Player"))
        {
            SoundFXManager.instance.PlaySoundFXClip(pickupSoundClip, transform, 1f);
        }
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}