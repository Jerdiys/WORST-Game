using System.Collections;
using UnityEngine;

public class MacheteSwing : MonoBehaviour
{
    [SerializeField] GameObject machete;
    [SerializeField] AudioSource macheteSwing;
    [SerializeField] bool canSwing = true;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (canSwing)
            {
                StartCoroutine(swingingMachete());
            }
        }

    }

    IEnumerator swingingMachete()
    {
        canSwing = false;
        macheteSwing.Play();
        machete.GetComponent<Animator>().Play("MacheteSwing");
        yield return new WaitForSeconds(0.35f);
        machete.GetComponent<Animator>().Play("New State");
        yield return new WaitForSeconds(0.1f);
        canSwing = true;
    }
}