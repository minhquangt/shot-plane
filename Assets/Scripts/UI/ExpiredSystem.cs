using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpiredSystem : MonoBehaviour
{
  public float expiredTime;
  void Start()
  {
    Destroy(gameObject, expiredTime);
  }
}
