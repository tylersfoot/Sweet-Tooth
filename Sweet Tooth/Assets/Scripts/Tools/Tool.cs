using System.Collections;
using System.Collections.Generic;
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
    public string currentAmmoDisplay;
    public string maxAmmoDisplay;
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

    void Update()
    {
        switch (activeTool)
        {
        case "BubblegumBlaster":
            currentAmmoDisplay = bubblegumBlaster.currentAmmo.ToString();
            maxAmmoDisplay = bubblegumBlaster.maxAmmo.ToString();
            break;
        case "PeanutBrittleShotty":
            currentAmmoDisplay = peanutBrittleShotty.currentAmmo.ToString();
            maxAmmoDisplay = peanutBrittleShotty.maxAmmo.ToString();
            break;
        default:
            currentAmmoDisplay = "";
            maxAmmoDisplay = "";
            break;
        }
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
        activeToolIndex = num;

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

    public void Reload()
    {
        if (float.Parse(currentAmmoDisplay) < float.Parse(maxAmmoDisplay))
        {
            switch (activeTool)
            {
            case "BubblegumBlaster":
                bubblegumBlaster.currentAmmo = bubblegumBlaster.maxAmmo;
                break;
            case "PeanutBrittleShotty":
                peanutBrittleShotty.currentAmmo = peanutBrittleShotty.maxAmmo;
                break;
            default:
                break;
            }
        }
    }

    public void Despawn(GameObject obj, float timeSeconds)
    {
        // this function destroys the object after `time` seconds
        StartCoroutine(DespawnCoroutine(obj, timeSeconds));
    }

    private IEnumerator DespawnCoroutine(GameObject obj, float timeSeconds)
    {
        // wait for the specified amount of time
        yield return new WaitForSeconds(timeSeconds);

        // destroy the object
        Destroy(obj);
    }
}
