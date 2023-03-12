using UnityEngine;
using UnityEngine.Animations;

public class Tool : MonoBehaviour
{

    // tool scripts
    public BubblegumBlaster bubblegumBlaster;
    public PeanutBrittleShotty peanutBrittleShotty;

    public GameObject player;
    public Vector3 positionOffset;
    private string targetTool;
    public string activeTool;
    public string[] tools;
    public int activeToolIndex = 0;

    public AudioClip[] audioClips; // audio clips to play when the tool is used

    // basically just teleports the tool to the player and positions it right
    void Start()
    {
        // get all the direct children (tools) and set their position to the player's position
        // this is so we can set the tools really far away in the editor so they are not in the way
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.name != "ProjectileSpawn")
            {
                child.localPosition = Vector3.zero;
            }
        }
        // set the "Tools" GameObject to the player's position + an offset
        transform.position = player.transform.position + positionOffset;
    }

    public void SwitchTool(int num)
    {
        // if the number is over or under the index, for use with scroll wheel
        if (num < 1)
        {
            num = tools.Length;
        }
        if (num > tools.Length)
        {
            num = 1;
        }

        // switches the tool
        switch (num)
        {
        case 1:
            targetTool = "Hands";
            break;
        case 2:
            targetTool = "BubblegumBlaster";
            break;
        case 3:
            targetTool = "PeanutBrittleShotty";
            break;
        default:
            // do nothing
            break;
        }
        GameObject currentToolObject = transform.Find(activeTool).gameObject;
        GameObject targetToolObject = transform.Find(targetTool).gameObject;
        if (currentToolObject?.name != targetToolObject?.name)
        {
            currentToolObject.SetActive(false);
            targetToolObject.SetActive(true);
            activeTool = targetTool;
        }
    }

    // method to use the tool
    public void Use(bool key)
    {
        switch (activeTool)
        {
        case "BubblegumBlaster":
            bubblegumBlaster.isKeyDown = key;
            break;
        case "PeanutBrittleShotty":
            peanutBrittleShotty.isKeyDown = key;
            break;
        default:
            // do nothing, maybe hands in the future?
            break;
        }
    }
}
