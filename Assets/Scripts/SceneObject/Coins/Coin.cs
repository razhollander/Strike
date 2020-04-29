using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : SuckableObject
{
    [SerializeField] public int MoneyValue = 50;
    [SerializeField] float startingY = 20;
    [SerializeField] float spawnAngularSpeed = 1;
    public override void Collected()
    {
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().AddGameMoney(MoneyValue,transform.position);
        base.Collected();
    }

    protected override void ResetTransform()
    {
        base.ResetTransform();
        float randomRot = Random.Range(0, 360);
        thisRigidBody.angularVelocity = new Vector3(0, 0,1)* spawnAngularSpeed;
        //transform.localRotation = Quaternion.Euler(randomRot, (randomRot + 100) * 2, (randomRot + 200) * 3);
    }
    public override void SetSpawnedPosition(Vector3 spawnedPos)
    {
        transform.position = spawnedPos + startingY * Vector3.up;
    }
}
