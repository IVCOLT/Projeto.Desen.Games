using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float minX, maxX = 33.9f;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        if (player == null)
            return;

        // Obtém a posição alvo da câmera na horizontal
        float targetX = Mathf.Clamp(player.position.x, minX, maxX);
        
        // Calcula a posição alvo da câmera
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Suaviza o movimento da câmera usando a função SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}