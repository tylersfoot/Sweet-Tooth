using UnityEngine;

public class Tool : MonoBehaviour
{
    // the position of the tool relative to the player
    public Vector3 defaultPosition = new Vector3(1f, 0.5f, 0f);

    // the rotation of the tool relative to the player
    public Vector3 defaultRotation = new Vector3(0f, 90f, 0f);

    private Transform playerTransform;

    public float damage; // damage the tool inflicts
    public float attackSpeed; // speed at which the tool can attack
    public float range; // range of the tool
    public AudioClip[] audioClips; // audio clips to play when the tool is used

    private void Start()
    {
        // get the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // set the tool's position and rotation relative to the player
        transform.position = playerTransform.position + playerTransform.TransformVector(defaultPosition);
        transform.rotation = playerTransform.rotation * Quaternion.Euler(defaultRotation);
    }

    // method to use the tool
    public virtual void Use()
    {
        // play audio clip
        if (audioClips.Length > 0)
        {
            int clipIndex = Random.Range(0, audioClips.Length);
            AudioSource.PlayClipAtPoint(audioClips[clipIndex], transform.position);
        }
    }
}
