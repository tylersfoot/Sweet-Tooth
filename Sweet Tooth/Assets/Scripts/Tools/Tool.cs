using UnityEngine;
using UnityEngine.Animations;

public class Tool : MonoBehaviour
{
    // the position of the tool relative to the player
    public Vector3 defaultPosition = new Vector3(1f, 0f, 0f);

    // the rotation of the tool relative to the player
    public Vector3 defaultRotation = new Vector3(0f, 0f, 0f);

    private Transform playerTransform;

    public float damage; // damage the tool inflicts
    public float attackSpeed; // speed at which the tool can attack
    public float range; // range of the tool
    public AudioClip[] audioClips; // audio clips to play when the tool is used

    // basically just teleports the tool to the player and positions it right
    void Start()
    {
        // get all the child transforms of this object
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        // iterate through all the child transforms and set their position to zero
        foreach (Transform child in children)
        {
            child.localPosition = Vector3.zero;
        }


        //! none of this works and I don't know why just change the values in the inspector lol
        // get the parent constraint component on the tool
        // ParentConstraint parentConstraint = GetComponent<ParentConstraint>();
        // set the position offset
        // Debug.Log(parentConstraint.translationOffsets[0]);
        // Debug.Log(defaultPosition);
        // parentConstraint.translationOffsets[0] = new Vector3(1f, 0f, 0f);
        // Debug.Log(parentConstraint.translationOffsets[0]);

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
