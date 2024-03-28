using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemieParamsSO", menuName = "Scriptable Objects/Enemies/Enemy Params")]
public class EnemyParamsSO : ScriptableObject
{
    public float maxVelocity;
    public float maxForce;
    public float seeAheadDistance;
    public float seekTargetReachedDistance = 0.5f;
    public float maxAvoidForce;
    public AudioClip attackSound;
}
