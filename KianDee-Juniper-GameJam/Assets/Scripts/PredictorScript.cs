using UnityEngine;

public class PredictorScript : MonoBehaviour
{
    public Vector3 rayDir;

    public GameObject arrow;

    LayerMask IgnoreLayerMask;

    public float scrollSpeedX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, rayDir*2);

        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * -scrollSpeedX, 0);

        arrow.transform.position = transform.position + (rayDir*2);

        Vector3 normDir = Vector3.Normalize(rayDir);
        float angle = Mathf.Atan2(normDir.y, normDir.x) * Mathf.Rad2Deg;
		arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
