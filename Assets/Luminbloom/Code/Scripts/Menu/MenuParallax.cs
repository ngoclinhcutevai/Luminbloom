using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    // A list to hold all the background layers you want to move.
    // Assign these in the Inspector.
    public List<RectTransform> parallaxLayers;

    // How much the layers should move. A smaller number means less movement.
    [SerializeField]
    private float parallaxEffectMultiplier = 0.05f;

    // How smoothly the layers follow the mouse. A smaller number is snappier.
    [SerializeField]
    private float smoothing = 0.1f;

    // We'll store the starting positions of the layers here.
    private List<Vector3> initialPositions;

    void Start()
    {
        // Initialize the list to store the starting positions.
        initialPositions = new List<Vector3>();

        // Loop through each layer assigned in the Inspector.
        foreach (var layer in parallaxLayers)
        {
            // Record its initial anchoredPosition.
            initialPositions.Add(layer.anchoredPosition);
        }
    }

    void Update()
    {
        // Calculate the mouse position's offset from the center of the screen.
        // The center is our "neutral" point where nothing moves.
        Vector2 mouseOffset = new Vector2(
            Input.mousePosition.x - Screen.width / 2,
            Input.mousePosition.y - Screen.height / 2
        );

        // Loop through all the layers.
        for (int i = 0; i < parallaxLayers.Count; i++)
        {
            // Determine how much this specific layer should move.
            // We multiply by (i + 1) so that layers further down the list move more.
            // The movement is in the *opposite* direction of the mouse offset.
            float amountToMoveX = -mouseOffset.x * (i + 1) * parallaxEffectMultiplier;
            float amountToMoveY = -mouseOffset.y * (i + 1) * parallaxEffectMultiplier;

            // Calculate the target position by adding the movement amount to the layer's initial position.
            Vector3 targetPosition = new Vector3(
                initialPositions[i].x + amountToMoveX,
                initialPositions[i].y + amountToMoveY,
                initialPositions[i].z
            );

            // Smoothly move the layer towards the target position using Lerp.
            // This prevents jerky movement.
            parallaxLayers[i].anchoredPosition = Vector3.Lerp(
                parallaxLayers[i].anchoredPosition,
                targetPosition,
                smoothing
            );
        }
    }
}
