using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public float speed = 100f;
    private void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime); 
    }
}
