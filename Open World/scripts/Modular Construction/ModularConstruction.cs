using System.Collections.Generic;
using UnityEngine;

public class ModularConstruction : MonoBehaviour
{
    [System.Serializable]
    public class Part
    {
        public GameObject Prefab;
        public GameObject Preview;
    }

    //distance at which objects can connect
    public float Range = 50f;
    //distance at which previews hover
    public float HoverDist = 10f;
    //tag for the position markers
    public string Tag = "handle";
    //currently selected part in the list
    //change this from wherever to have the player select an item
    public int Selected = -1;
    //list of all prefabs
    public List<Part> Prefabs = default;

    //private stuffs
    RaycastHit hit;
    GameObject Preview;
    //list of all handles the prefab has
    List<GameObject> Handles = default;
    //currently active handle
    int SelectedHandle = 0;
    //just a position holder
    Vector3 p;

    void Update()
    {
        //if any item is "active"
        if (Selected != -1)
        {
            //check if anything was hit
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2), 0)), out hit, Range))
            {
                //enable preview
                EnableSelected();
                //check if hit object is a positioning handle
                if (hit.transform.CompareTag(Tag))
                {
                    //find correct position
                    p = Handles[SelectedHandle].transform.position - Preview.transform.position;
                    //move preview into position
                    Preview.transform.position = hit.transform.position - p;
                }
            }
            //if not, just spawn it at default distance
            else
            {
                EnableSelected();
                Preview.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * HoverDist);
            }
            // rotating
            AwaitRotation();
            // toggling the active handle
            AwaitHandleSwitch();
            //actually place the object
            AwaitPlacement();
        }
        else if (Preview != null)
        {
            Destroy(Preview);
            Handles.Clear();
        }

    }
    //enables the selected preview
    void EnableSelected()
    {
        if (Preview == null)
        {
            //instantiate if no preview is present
            Preview = Instantiate(Prefabs[Selected].Preview, Camera.main.transform.position + (Camera.main.transform.forward * HoverDist), new Quaternion(0, 0, 0, 0)) as GameObject;
            FindHandles(Preview);
        }
        else if (Preview.transform.tag != Prefabs[Selected].Preview.transform.tag)
        {
            //if wrong preview is present, 
            //delete, then instantiate correct one
            Destroy(Preview);
            Preview = Instantiate(Prefabs[Selected].Preview, Camera.main.transform.position + (Camera.main.transform.forward * HoverDist), new Quaternion(0, 0, 0, 0)) as GameObject;
            FindHandles(Preview);
        }
        else
        {
            //else just move the preview
            Preview.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * HoverDist);
        }
    }
    //waits for the player to confirm placing the prefab 
    void AwaitPlacement()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(Prefabs[Selected].Prefab, Preview.transform.position, Preview.transform.rotation);
        }
    }
    //waits for player to rotate the object
    void AwaitRotation()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Preview.transform.Rotate(90, 0, 0, Space.World);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Preview.transform.Rotate(0, 90, 0, Space.World);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Preview.transform.Rotate(0, 0, 90, Space.World);
        }
    }
    //waits for player to toggle the active handle
    void AwaitHandleSwitch()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            if (Handles.Count > 0)
            {
                SelectedHandle++;
                if (SelectedHandle >= Handles.Count)
                {
                    SelectedHandle = 0;
                }
            }
        }
    }
    //Finds all handles the prefab has 
    void FindHandles(GameObject g)
    {
        Handles.Clear();
        foreach (Transform c in Preview.transform)
        {
            if (c.CompareTag(Tag))
            {
                Handles.Add(c.gameObject);
            }
        }
    }
}