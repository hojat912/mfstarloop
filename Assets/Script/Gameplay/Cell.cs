using UnityEngine;

public class Cell : MonoBehaviour
{

    public void SetHeight(int height)
    {
        transform.localScale = new Vector3(1, height, 1);
    }
}
