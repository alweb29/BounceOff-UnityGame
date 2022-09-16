using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField]
    public GameMenager gamemenager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
