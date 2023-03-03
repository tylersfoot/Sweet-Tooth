using UnityEngine;

public class Tool : MonoBehaviour
{
    // the position of the tool relative to the player
    public Vector3 defaultPosition = new Vector3(1f, -0.45f, 1f);

    // the rotation of the tool relative to the player
    public Vector3 defaultRotation = new Vector3(0f, 0f, 0f);

    private Transform playerTransform;

    public float damage; // damage the tool inflicts
    public float attackSpeed; // speed at which the tool can attack
    public float range; // range of the tool
    public AudioClip[] audioClips; // audio clips to play when the tool is used

    // basically just teleports the tool to the player and positions it right
    private void Start()
    {
        // get all the child transforms of this object
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        // iterate through all the child transforms and set their position to zero
        foreach (Transform child in children)
        {
            child.localPosition = Vector3.zero;
        }

        // get the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(playerTransform);

        // set the tool's position relative to the player
        transform.position = playerTransform.position + playerTransform.TransformVector(defaultPosition);
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
