using System.Collections;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    [SerializeField] AudioSource walk;
    [SerializeField] AudioSource run;
    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        if (isMoving)
        {
            if (!walk.isPlaying)
                walk.Play();
        }
        else
        {
            if (walk.isPlaying)
                walk.Stop();
        }

        if (isRunning)
        {
            walk.Stop();
            if (!run.isPlaying)
                run.Play();
        }
        else
        {
            if (run.isPlaying)
                run.Stop();
        }
    }

}
