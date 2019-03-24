using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PointCloudView : MonoBehaviour
{
    //public GameObject ColorSourceManager;
    //public GameObject DepthSourceManager;
    public GameObject MultiSourceManager;

    private KinectSensor _Sensor;
    private CoordinateMapper _Mapper;

    private MultiSourceManager _MultiManager;
    // private ColorSourceManager _ColorManager;
    // private DepthSourceManager _DepthManager;

    private const int _DownsampleSize = 4;
    private const double _DepthScale = 1f;

    private Mesh _Mesh;
    private Vector3[] _Vertices;
    private Color[] _Colors;
    private int[] _Indices;

    // Use this for initialization
    void Start()
    {
        _Sensor = KinectSensor.GetDefault();
        if (_Sensor != null)
        {
            _Mapper = _Sensor.CoordinateMapper;
            var frameDesc = _Sensor.DepthFrameSource.FrameDescription;

            // TODO
            // _Mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _Mesh;
            CreateMesh(frameDesc.Width / _DownsampleSize, frameDesc.Height / _DownsampleSize);

            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }
    }

    void CreateMesh(int width, int height)
    {
        _Mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _Mesh;

        _Vertices = new Vector3[width * height];
        _Colors = new Color[width * height];
        _Indices = new int[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = (y * width) + x;

                _Vertices[index] = new Vector3(0, 0, 0);
                _Colors[index] = new Color(0.5f, 0.5f, 0.5f, 1);
                _Indices[index] = index;
            }
        }

        _Mesh.vertices = _Vertices;
        _Mesh.colors = _Colors;
        // TODO: make indices
        _Mesh.SetIndices(_Indices, MeshTopology.Points, 0); 
    }

    private void Update()
    {
        if (_Sensor == null)
        {
            return;
        }
        if (MultiSourceManager == null)
        {
            return;
        }

        _MultiManager = MultiSourceManager.GetComponent<MultiSourceManager>();
        if (_MultiManager == null)
        {
            return;
        }

        gameObject.GetComponent<Renderer>().material.mainTexture = _MultiManager.GetColorTexture();

        UpdateData(_MultiManager.GetDepthData(),
                   _MultiManager.GetColorTexture(),
                   _MultiManager.ColorWidth,
                   _MultiManager.ColorHeight);
    }

    private void UpdateData(ushort[] depthData, Texture2D colorData, int colorWidth, int colorHeight)
    {
        var frameDesc = _Sensor.DepthFrameSource.FrameDescription;

        ColorSpacePoint[] colorSpace = new ColorSpacePoint[depthData.Length];
        CameraSpacePoint[] cameraSpace = new CameraSpacePoint[depthData.Length];
        _Mapper.MapDepthFrameToColorSpace(depthData, colorSpace);
        _Mapper.MapDepthFrameToCameraSpace(depthData, cameraSpace);
        //_Mapper.MapCameraPointsToColorSpace(cameraSpace, colorSpace);

        for (int y = 0; y < frameDesc.Height; y += _DownsampleSize)
        {
            for (int x = 0; x < frameDesc.Width; x += _DownsampleSize)
            {
                int indexX = x / _DownsampleSize;
                int indexY = y / _DownsampleSize;
                int smallIndex = (indexY * (frameDesc.Width / _DownsampleSize)) + indexX;
                int fullIndex = (y * frameDesc.Width) + x;

                //double avg = GetAvg(depthData, x, y, frameDesc.Width, frameDesc.Height);
                double avg = GetAvg2(cameraSpace, x, y, frameDesc.Width, frameDesc.Height);


                avg = avg * _DepthScale;

                _Vertices[smallIndex].z = cameraSpace[fullIndex].Z - 1;
                //_Vertices[smallIndex].x = cameraSpace[fullIndex].X;
                //_Vertices[smallIndex].y = cameraSpace[fullIndex].Y;
                if (cameraSpace[fullIndex].X > -1000 && cameraSpace[smallIndex].X < 1000)
                    _Vertices[smallIndex].x = cameraSpace[fullIndex].X;
                if (cameraSpace[fullIndex].Y > -1000 && cameraSpace[fullIndex].Y < 1000)
                    _Vertices[smallIndex].y = cameraSpace[fullIndex].Y;
                //_Vertices[smallIndex] = transform.InverseTransformPoint(_Vertices[smallIndex]);
                // TODO: downsampling
                var colorSpacePoint = colorSpace[(y * frameDesc.Width) + x];
                _Colors[smallIndex] = colorData.GetPixel((int) colorSpacePoint.X, (int) colorSpacePoint.Y);

                // Update UV mapping with CDRP
                //var colorSpacePoint = colorSpace[(y * frameDesc.Width) + x];
                //_UV[smallIndex] = new Vector2(colorSpacePoint.X / colorWidth, colorSpacePoint.Y / colorHeight);
            }
        }

        _Mesh.vertices = _Vertices;
        _Mesh.colors = _Colors;
        // TODO: make indices
        _Mesh.SetIndices(_Indices, MeshTopology.Points, 0);
    }

    private double GetAvg2(CameraSpacePoint[] depthData, int x, int y, int width, int height)
    {
        double sum = 0.0;

        int n = _DownsampleSize ^ 2;

        for (int y1 = y; y1 < y + _DownsampleSize; y1++)
        {
            for (int x1 = x; x1 < x + _DownsampleSize; x1++)
            {
                int fullIndex = (y1 * width) + x1;

                if (depthData[fullIndex].Z == 0)
                    sum += 4500;
                else
                    sum += depthData[fullIndex].Z;

            }
        }

        return sum / n;
    }

    private double GetAvg(ushort[] depthData, int x, int y, int width, int height)
    {
        double sum = 0.0;

        int n = _DownsampleSize ^ 2;

        for (int y1 = y; y1 < y + _DownsampleSize; y1++)
        {
            for (int x1 = x; x1 < x + _DownsampleSize; x1++)
            {
                int fullIndex = (y1 * width) + x1;

                if (depthData[fullIndex] == 0)
                    sum += 4500;
                else
                    sum += depthData[fullIndex];

            }
        }

        return sum / n;
    }

    void OnApplicationQuit()
    {
        if (_Mapper != null)
        {
            _Mapper = null;
        }

        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }

            _Sensor = null;
        }
    }
}