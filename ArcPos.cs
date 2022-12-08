using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.Geometry;
using Esri.HPFramework;
using System;
using Unity.Mathematics;

public class ArcPos : MonoBehaviour
{
    public ArcGISPoint position;
    public HPTransform hpTransform;
    public double3 universePosition;
    private void Awake()
    {

        this.gameObject.AddComponent<HPTransform>();
        hpTransform = GetComponent<HPTransform>();
        hpTransform.LocalPosition = new double3(4056193.24362, 174, 6434245.230809);
        this.gameObject.transform.position = new Vector3(0, 0, 0);

    }

}
