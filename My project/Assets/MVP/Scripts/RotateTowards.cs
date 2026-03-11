using UnityEngine;

public class RotateTowards : MonoBehaviour
{

    [SerializeField]
    private Transform objectToRotateTowards;

    private void Update()
    {
        Vector3 targetDirection = objectToRotateTowards.position - transform.position;

        Vector3 direction = Vector3.RotateTowards(transform.forward, targetDirection, 1 * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(direction);
    }

}
