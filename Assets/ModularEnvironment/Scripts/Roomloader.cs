using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.Android;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Roomloader : MonoBehaviour
{

    public Material wallMaterial;
    void Start()
    {
        //Loading the room from the device, most recent sae
        MRUK.Instance.LoadSceneFromDevice();

        if (Permission.HasUserAuthorizedPermission("com.oculus.permission.USE_SCENE"))
        {
            print("Permission Granted");
        }   
    }
    /// <summary>
    /// void FindRoom()
    ///{
        ///foreach (MRUKRoom room in MRUK.Instance.Rooms)
        ///{
            ///foreach (MRUKAnchor childAnchor in room.Anchors)
            ///{
                // each child is a scene object belonging to this room, such as a floor, ceiling, or couch
            ///}   
        ///}
    ///}
    /// </summary>
    
    public void RecreateRoomWithPrimitives()
    {
        //This is for putting primitives as shapes into the walls so that I can manipulate the textures of the room
        //This gets the room that is currently on display

        var currentRoom = MRUK.Instance.GetCurrentRoom();

        //Vector2 wallScale;

        foreach (var anchor in currentRoom.Anchors)
        {
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

            plane.transform.position = anchor.transform.position;
            plane.transform.rotation = anchor.transform.rotation;

            plane.transform.localScale = new Vector3(anchor.transform.localScale.x, anchor.transform.localScale.y, 1f);

            MaterialChangeWalls(wallMaterial, plane);
        }
        //Gets the scale of the wall so i can adjust the scale of the wall
        //var keywall = currentRoom.GetKeyWall(out wallScale); //this comes out normal and seems correct
        //print("Position: " + keywall.transform.position + "Rotation" + keywall.transform.rotation);
        //This part creates a quad or plane for the faces. Will probably seperate into planes and cubes
        //Testing with keywall first
        //Make foreach function for each seperate wall that is planes and probably for each cubed object the room scanner has
        //How to get each room anchor??????

    }

    void MaterialChangeWalls(Material material, GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material = material;
    }
}
