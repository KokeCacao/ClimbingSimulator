using UnityEngine;

public class BodyCollision : MonoBehaviour {

  [SerializeField] private PlayerManager playerManager;
  
  // on collision
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Sinker") || collision.gameObject.layer == LayerMask.NameToLayer("Floater"))
    {
      playerManager.Hit();
    }
  }
}