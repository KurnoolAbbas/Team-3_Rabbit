using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceAnimation : MonoBehaviour
{
    public float bounceHeight = 1.2f; // Height multiplier for the bounce
    public float animationDuration = 0.3f; // Duration of each bounce animation

    private Vector3 originalScale; // Original scale of the GameObject

    void Start()
    {
        originalScale = transform.localScale; // Store the original scale of the GameObject
    }

    void Update()
    {
        // Bounce animation
        float bounceScale = Mathf.PingPong(Time.time / animationDuration, 1.0f) * bounceHeight;
        transform.localScale = originalScale * (1 + bounceScale);
    }
}
