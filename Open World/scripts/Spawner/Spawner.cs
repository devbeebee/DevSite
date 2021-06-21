using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject ToClone;
   
    // Start is called before the first frame update
    IEnumerator Start()
    {
        int c = 100;
        while (c > 0)
        {
           GameObject go= ToClone.CloneObject(transform.position,Quaternion.identity);
            c--;
            yield return new WaitForSeconds(3);
        }
    }


}
