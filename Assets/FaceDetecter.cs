using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.VisualRecognition.v3;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Connection;

using UnityEngine.UI;

public class FaceDetecter : MonoBehaviour {

    public Text dataOutput;

    private VisualRecognition _visualRecognition;
    //private string picURL = "C:\\Users\\Vasanth\\Downloads\\Hassan.jpg";

    // Use this for initialization
    void Start () {
        Credentials credentials = new Credentials("6b7e212e6ea3425fc358e6f795ec0f99e1b3b558",
            "https://gateway-a.watsonplatform.net/visual-recognition/api");
        _visualRecognition = new VisualRecognition(credentials);
        _visualRecognition.VersionDate = "2016-05-20";
    }

    public void DetectFaces(string path) {
        //  Classify using image url
        //if (!_visualRecognition.DetectFaces(picURL, OnDetectFaces, OnFail))
        //    Debug.Log("ExampleVisualRecognition.DetectFaces(): Detect faces failed!");

        //  Classify using image path
        if (!_visualRecognition.DetectFaces(OnDetectFaces, OnFail, path)) {
            Debug.Log("ExampleVisualRecognition.DetectFaces(): Detect faces failed!");
        } else {
            Debug.Log("Calling Watson");
            dataOutput.text = "";
        }
    }

    private void OnDetectFaces(FacesTopLevelMultiple multipleImages, Dictionary<string, object> customData) {
        var data = multipleImages.images[0].faces[0]; //assume 1
        dataOutput.text = "Age : " + data.age.min + "-" + 
            data.age.max + " PROBABILITY: " + data.age.score + "\n" +
            "Gender: " + data.gender.gender + " PROBABILITY: " + data.gender.score + "\n";
        Debug.Log("ExampleVisualRecognition.OnDetectFaces(): Detect faces result: " + customData["json"].ToString());
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData) {
        Debug.LogError("ExampleVisualRecognition.OnFail(): Error received: " +  error.ToString());
    }
}
