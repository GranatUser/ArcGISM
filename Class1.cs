
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Samples.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Extent;
using Esri.GameEngine.Geometry;
using Esri.Unity;
using UnityEngine;
using System;


public class ArcGisMapMy : MonoBehaviour
{


    [ExecuteAlways]
    public string APIKey = "You Api";
    ArcGISMapComponent arcGISMapComponent;
    private ArcGISPoint geographicCoordinates = new ArcGISPoint(36.43740386, 49.93048194, 174, ArcGISSpatialReference.WGS84()); //(-74.054921, 40.691242, 3000
    private ArcGISCameraComponent cameraComponent;

    public delegate void SetLayerAttributesEventHandler(Esri.GameEngine.Layers.ArcGIS3DObjectSceneLayer layer);
    public event SetLayerAttributesEventHandler OnSetLayerAttributes;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    void Start()
    {

        CreateArcGISMapComponent();
        CreateArcGISCamera();
        CreateSkyComponent();
        CreateArcGISMap();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void CreateArcGISMapComponent()
    {
        arcGISMapComponent = FindObjectOfType<ArcGISMapComponent>();

        if (!arcGISMapComponent)
        {
            var arcGISMapGameObject = new GameObject("ArcGISMap");
            arcGISMapComponent = arcGISMapGameObject.AddComponent<ArcGISMapComponent>();
        }

        arcGISMapComponent.OriginPosition = geographicCoordinates;
        arcGISMapComponent.MapType = Esri.GameEngine.Map.ArcGISMapType.Local;
        arcGISMapComponent.MapTypeChanged += new ArcGISMapComponent.MapTypeChangedEventHandler(CreateArcGISMap);
    }
    public void CreateArcGISMap()
    {
        var arcGISMap = new Esri.GameEngine.Map.ArcGISMap(arcGISMapComponent.MapType);
        arcGISMap.Basemap = Esri.GameEngine.Map.ArcGISBasemap.CreateImagery(APIKey);
        arcGISMap.Elevation = new Esri.GameEngine.Map.ArcGISMapElevation
            (new Esri.GameEngine.Elevation.ArcGISImageElevationSource
            ("https://elevation3d.arcgis.com/arcgis/rest/services/WorldElevation3D/Terrain3D/ImageServer", "Elevation", ""));



        var layer_1 = new Esri.GameEngine.Layers.ArcGISImageLayer("https://tiles.arcgis.com/tiles/nGt4QxSblgDfeJn9/arcgis/rest/services/UrbanObservatory_NYC_TransitFrequency/MapServer", "MyLayer_1", 1.0f, true, "");
        arcGISMap.Layers.Add(layer_1);

        var layer_2 = new Esri.GameEngine.Layers.ArcGISImageLayer("https://tiles.arcgis.com/tiles/nGt4QxSblgDfeJn9/arcgis/rest/services/New_York_Industrial/MapServer", "MyLayer_2", 1.0f, true, "");
        arcGISMap.Layers.Add(layer_2);

        var layer_3 = new Esri.GameEngine.Layers.ArcGISImageLayer("https://tiles.arcgis.com/tiles/4yjifSiIG17X0gW4/arcgis/rest/services/NewYorkCity_PopDensity/MapServer", "MyLayer_3", 1.0f, true, "");
        arcGISMap.Layers.Add(layer_3);

        /* var buildingLayer = new Esri.GameEngine.Layers.ArcGIS3DObjectSceneLayer("https://tiles.arcgis.com/tiles/P3ePLMYs2RVChkJx/arcgis/rest/services/Buildings_NewYork_17/SceneServer", "Building Layer", 1.0f, true, "");
         arcGISMap.Layers.Add(buildingLayer);*/
        var buildingLayer = new Esri.GameEngine.Layers.ArcGIS3DObjectSceneLayer("https://tiles.arcgis.com/tiles/gW5JwbgpfVyDiHDk/arcgis/rest/services/3DSceneUnitySuccessful/SceneServer", "3DSceneUnity", 1.0f, true, "");
        arcGISMap.Layers.Add(buildingLayer);



        if (OnSetLayerAttributes != null)
        {
            OnSetLayerAttributes(buildingLayer);
        }


        var extentCenter = new Esri.GameEngine.Geometry.ArcGISPoint(36.437366, 49.930539, 400, ArcGISSpatialReference.WGS84());//-74.054921, 40.691242, 3000,
        var extent = new ArcGISExtentCircle(extentCenter, 100000);



        arcGISMap.ClippingArea = extent;
        arcGISMapComponent.View.Map = arcGISMap;
    }

    private void CreateArcGISCamera()
    {
        cameraComponent = Camera.main.gameObject.GetComponent<ArcGISCameraComponent>();

        if (!cameraComponent)
        {
            var cameraGameObject = Camera.main.gameObject;

            cameraGameObject.transform.SetParent(arcGISMapComponent.transform, false);
            cameraComponent = cameraGameObject.AddComponent<ArcGISCameraComponent>();
            cameraGameObject.AddComponent<ArcGISCameraControllerComponent>();
            cameraGameObject.AddComponent<ArcGISRebaseComponent>();
            cameraGameObject.AddComponent<ArcGISLocationComponent>();
            ArcGISLocationComponent c = cameraGameObject.GetComponent<ArcGISLocationComponent>();



        }

        var cameraLocationComponent = cameraComponent.GetComponent<ArcGISLocationComponent>();

        if (!cameraLocationComponent)
        {
            cameraLocationComponent = cameraComponent.gameObject.AddComponent<ArcGISLocationComponent>();
            // cameraLocationComponent.Position = geographicCoordinates;
            cameraLocationComponent.Position = new ArcGISPoint(36.43740386, 49.93048194, 400, ArcGISSpatialReference.WGS84());
            cameraLocationComponent.Rotation = new ArcGISRotation(55, 58, 0);
        }
    }



    private void CreateSkyComponent()
    {
#if USE_URP_PACKAGE
		/*var currentSky = FindObjectOfType<UnityEngine.Rendering.Volume>();
		if (currentSky)
		{
			ArcGISSkyRepositionComponent skyComponent = currentSky.gameObject.GetComponent<ArcGISSkyRepositionComponent>();

			if (!skyComponent)
			{
				skyComponent = currentSky.gameObject.AddComponent<ArcGISSkyRepositionComponent>();
			}

			if (!skyComponent.arcGISMapComponent)
			{
				skyComponent.arcGISMapComponent = arcGISMapComponent;
			}

			if (!skyComponent.CameraComponent)
			{
				skyComponent.CameraComponent = cameraComponent;
			}
		}*/
#endif
    }

}
