using UnityEngine;

public class Shootables : MonoBehaviour
{
    private Rigidbody rb;
    private RandomVasePicker randomVasePicker;

    private void Awake()
    {
        randomVasePicker = RandomVasePicker.instance;
    }

    public void Shoot()
    {
        if (!GetComponent<Rigidbody>())
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.GetComponent<Rigidbody>())
                {
                    rb = transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    rb.AddRelativeForce(170 * i, 170 * i, 170 * i);
                }
            }
        }
        randomVasePicker.RemovePart(gameObject);

        randomVasePicker.GetChildCount();
        Destroy(gameObject, 2);
    }
}
