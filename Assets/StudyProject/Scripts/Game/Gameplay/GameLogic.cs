using UnityEngine;

public class GameLogic
{
    private Rigidbody _rb;

    public GameLogic(Rigidbody rigidbody)
    {
        _rb = rigidbody;
    }

    public void DoSomething()
    {
        _rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
}
