using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JSONtypes {
    [Serializable]
    public class Transform {
        public string positionX;
        public string positionY;
        public string positionZ;
        public string rotationX;
        public string rotationY;
        public string rotationW;
        public string rotationZ;

        public Transform(UnityEngine.Transform unity_transform) {
            // positionX = unity_transform.position.x;
            // positionY = unity_transform.position.y;
            // positionZ = unity_transform.position.z;
            // rotationX = unity_transform.rotation.x;
            // rotationY = unity_transform.rotation.y;
            // rotationZ = unity_transform.rotation.z;
            // rotationW = unity_transform.rotation.w;
            positionX = unity_transform.position.x.ToString();
            positionY = unity_transform.position.y.ToString();
            positionZ = unity_transform.position.z.ToString();
            rotationX = unity_transform.rotation.x.ToString();
            rotationY = unity_transform.rotation.y.ToString();
            rotationZ = unity_transform.rotation.z.ToString();
            rotationW = unity_transform.rotation.w.ToString();
        }

        public Transform(Dictionary<string, string> json) {

        }
    }
};

public static class JSONConvertation {
    public static Dictionary<string, string> TransformToJSON(Transform transform) {
        return new Dictionary<string, string> {
            {"positionX", transform.position.x.ToString()},
            {"positionY", transform.position.y.ToString()},
            {"positionZ", transform.position.z.ToString()},
            {"rotationX", transform.rotation.x.ToString()},
            {"rotationY", transform.rotation.y.ToString()},
            {"rotationZ", transform.rotation.z.ToString()},
            {"rotationW", transform.rotation.w.ToString()},
            // {"positionX", transform.position.x},
            // {"positionY", transform.position.y},
            // {"positionZ", transform.position.z},
            // {"rotationX", transform.rotation.x},
            // {"rotationY", transform.rotation.y},
            // {"rotationZ", transform.rotation.z},
            // {"rotationW", transform.rotation.w},
        };
    }

    public static JSONtypes.Transform JSONToTransform(string json) {
        var position = JsonUtility.FromJson<JSONtypes.Transform>(json);
        return null;
    }
}
