using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class goldAnimation : MonoBehaviour
{
    private TextMeshProUGUI inc;
    private Color incColor = Color.white;
    private void Awake()
    {
        inc = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        transform.localPosition += Time.deltaTime * 3 * transform.up;
        incColor.a -= Time.deltaTime * 0.5f;
        inc.color = incColor;

        if(incColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
