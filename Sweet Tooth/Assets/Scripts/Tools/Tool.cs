using UnityEngine;
using UnityEngine.Animations;

public class Tool : MonoBehaviour
{
    //! doesn't work
    // the position of the tool relative to the player
    // public Vector3 defaultPosition = new Vector3(0f, 0f, 0f);
    // the rotation of the tool relative to the player
    // public Vector3 defaultRotation = new Vector3(0f, 0f, 0f);

    // initialize tool scripts
    public BubblegumBlaster BubblegumBlasterScript;

    private Transform playerTransform;

    public string activeTool = "BubblegumBlaster";

    public float damage; // damage the tool inflicts
    public float attackSpeed; // speed at which the tool can attack
    public float range; // range of the tool
    public AudioClip[] audioClips; // audio clips to play when the tool is used

    // basically just teleports the tool to the player and positions it right
    void Start()
    {
        // initialize all tool scripts 
        BubblegumBlasterScript = GameObject.Find("BubblegumBlaster").GetComponent<BubblegumBlaster>();

        // get all the direct children (tools) and set their position to zero
        // this is so we can set the tools really far away in the editor so they are not in the way
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.name != "ProjectileSpawn")
            {
                child.localPosition = Vector3.zero;
            }
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
    public void Use(bool key)
    {
        if (activeTool == "BubblegumBlaster")
        {
            BubblegumBlasterScript.isKeyDown = key;
        }
        // play audio clip
        // if (audioClips.Length > 0)
        // {
        //     int clipIndex = Random.Range(0, audioClips.Length);
        //     AudioSource.PlayClipAtPoint(audioClips[clipIndex], transform.position);
        // }
    }
}
