using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public GameObject myBlockInstance;
    public GameObject prevBlock;
    bool placing;
    bool previous;

    public GameObject LongBlock, CubeBlock, LBlock, TallBlock, Tree;

    // Use this for initialization
    void Start () {
        placing = false;
        previous = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (previous && OVRInput.GetDown(OVRInput.Button.Two))
        {
            Destroy(prevBlock);
            previous = false;
        }
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            float ypos = this.transform.position.y;
            ypos += 0.075f;
            Vector3 newPos = new Vector3(this.transform.position.x, ypos, this.transform.position.z);
            this.transform.position = newPos;
        }
        else if (OVRInput.Get(OVRInput.Button.Four))
        {
            float ypos = this.transform.position.y;
            ypos -= 0.075f;
            Vector3 newPos = new Vector3(this.transform.position.x, ypos, this.transform.position.z);
            this.transform.position = newPos;
        }
        if (placing) // While user is still deciding where to place the object
        {
            myBlockInstance.transform.position = this.transform.position + this.transform.forward * 5f - this.transform.up * 4f;
            //myBlockInstance.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            myBlockInstance.GetComponent<Rigidbody>().rotation = Quaternion.identity;
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                myBlockInstance.GetComponent<Rigidbody>().useGravity = true;
                myBlockInstance.GetComponent<Rigidbody>().mass = 100;
                prevBlock = myBlockInstance;
                previous = true;
                placing = false;
            }
                
        } 
        else if (!placing && OVRInput.GetDown(OVRInput.Button.One)) {
            print("Input detected");
            placing = true;
            myBlockInstance = Instantiate<GameObject>(chooseObject(), new Vector3(0,-.65f,0), Quaternion.identity);
            myBlockInstance.GetComponent<Rigidbody>().useGravity = false;
        }
	}

    GameObject chooseObject()
    {
        int choice = Random.Range(1, 6);
        if (choice == 1)
            return LongBlock;
        else if (choice == 2)
            return LBlock;
        else if (choice == 3)
            return CubeBlock;
        else if (choice == 4)
            return TallBlock;
        else
            return Tree;
    }
}
