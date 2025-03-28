using Arkanoid;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 defaultPlayerScale;

    private void Start()
    {
        SetDefaultPlayerScale();
    }

    private void SetDefaultPlayerScale()
    {
        defaultPlayerScale = transform.lossyScale;
    }

    public void SetPlayerLength()
    {
        transform.localScale = new Vector3(transform.lossyScale.x * BonusManager.Instance.CurrentLengthBonusScaler, transform.lossyScale.y, transform.lossyScale.z);
    }

    public void ResetPlayerLength()
    {
        transform.localScale = defaultPlayerScale;
    }
}
