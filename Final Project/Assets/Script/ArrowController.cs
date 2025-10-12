using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject midPointVisual;   // Your MidPointVisualObject
    [SerializeField] private Transform arrowPrefab;       // Arrow prefab (optional for spawning)
    [SerializeField] private Transform arrowSpawnPoint;   // Where to spawn or attach arrow
    [SerializeField] private float arrowSpeed = 50f;      // Base arrow speed

    private Transform currentArrow;

    // Called when the bowstring is grabbed
    public void PrepareArrow()
    {
        // Make sure the midpoint visual is visible
        if (midPointVisual != null)
            midPointVisual.SetActive(true);

        // If no arrow is currently loaded, create one
        if (arrowPrefab != null && arrowSpawnPoint != null && currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            currentArrow.SetParent(arrowSpawnPoint);
        }
    }

    // Called when the string is released
    public void ReleaseArrow(float strength)
    {
        if (midPointVisual != null)
            midPointVisual.SetActive(false);

        if (currentArrow != null)
        {
            // Detach and fire the arrow
            currentArrow.SetParent(null);

            Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                // Apply force proportional to bow pull strength
                float finalSpeed = arrowSpeed * Mathf.Clamp01(strength);
                rb.velocity = currentArrow.forward * finalSpeed;
            }

            // Log the strength (for testing)
            Debug.Log($"🎯 Bow strength: {strength}");

            currentArrow = null;
        }
    }
}
