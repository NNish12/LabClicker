using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
    }
}
