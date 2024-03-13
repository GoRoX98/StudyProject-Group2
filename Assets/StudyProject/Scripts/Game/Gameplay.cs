using UnityEngine;

public class Gameplay
{
    private Rigidbody _rb;

    public Gameplay(Rigidbody rb)
    {
        _rb = rb; 
    }

    public void DoSomething()
    {
        _rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

}

