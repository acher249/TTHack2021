using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;

namespace spatialNotes
{
    public class SpatialNoteRestCall : MonoBehaviour
    {
        public GameObject DebugPanel;
        public Text DebugPanelText;

        public void Start()
        {
            // test calls here

            //GetAllNotesViaMapName("myMap");
        }

        public void GetAllNotesViaMapName(string mapName)
        {

            StartCoroutine(SequenceStart(mapName));

            //chain coroutines so that they execute sequentially
            IEnumerator SequenceStart(string nameOfMap)
            {
                Debug.Log("Start Sequence to get note data: ");

                //Checking for DEBUG mode
                // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
                string URL = "https://ar-points.herokuapp.com/api/note/get-map-notes";
                yield return StartCoroutine(GetRequest_GetAllNotesViaMapName(nameOfMap, URL, (SpatialNoteRoot) =>
                {
                    Debug.Log("notes returned: ");
                    LogObjectToConsole(SpatialNoteRoot);

                    GameControl.control.AllNoteDataForThisMap = SpatialNoteRoot;

                    //display the data to a debug, to make sure that the data is coming in..
                    DebugPanel.SetActive(true);

                    for (var i = 0; i < SpatialNoteRoot.Notes.Length; i++)
                    {
                        //Debug.Log(SpatialNoteRoot.Notes[i].noteId);
                        //Debug.Log(SpatialNoteRoot.Notes[i].mapId);
                        DebugPanelText.text += SpatialNoteRoot.Notes[i].noteId;
                        DebugPanelText.text += SpatialNoteRoot.Notes[i].mapId;
                        DebugPanelText.text += SpatialNoteRoot.Notes[i].noteTitle;
                    }

                    // now do things with data

                }));


            }
        }

        IEnumerator GetRequest_GetAllNotesViaMapName(string mapName, string uri, System.Action<SpatialNoteRoot> callback)
        {

           // pass these arguments
            string postData = @"
            {
                ""mapId"": """ + mapName + @"""
            }";

            using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, postData))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
                webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                webRequest.downloadHandler = new DownloadHandlerBuffer();

                webRequest.SetRequestHeader("Content-Type", "application/json");
                //webRequest.SetRequestHeader("Authorization", "Bearer " + FileMakerAuthToken);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string resultObject = "";

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                    //SHoP_PromptMessage.PromptMessage.ErrorPromptText(pages[page] + ": Error: " + webRequest.error, SHoP_PromptMessage.ExceptionTypes.Custom);
                }
                else
                {
                    resultObject = webRequest.downloadHandler.text;

                    //Debug.Log(resultObject);

                    var response = JsonUtility.FromJson<SpatialNoteRoot>("{\"Notes\":" + resultObject + "}");

                    //LogObjectToConsole(response);
                    //Debug.Log(response.ToString());
                    callback(response);
                }
            }
        }


        // --------------------------------------------------------------------------------

        // utility rest call
        // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
        //IEnumerator GetRequest_Utility(string uri, System.Action<Note[]> callback)
        //{
        //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        //    {
        //        // Request and wait for the desired page.
        //        yield return webRequest.SendWebRequest();

        //        string resultObject = "";

        //        string[] pages = uri.Split('/');
        //        int page = pages.Length - 1;

        //        if (webRequest.isNetworkError)
        //        {
        //            Debug.Log(pages[page] + ": Error: " + webRequest.error);
        //            //SHoP_PromptMessage.PromptMessage.ErrorPromptText(pages[page] + ": Error: " + webRequest.error, SHoP_PromptMessage.ExceptionTypes.Custom);
        //        }
        //        else
        //        {
        //            resultObject = webRequest.downloadHandler.text;
        //            var response = JsonUtility.FromJson<SpatialNote_ResponseClass>(resultObject);

        //            // this populates global variable. Maybe a better way.
        //            //SHoPUtilities.LogObjectToConsole(response);
        //            callback(response.notes);
        //        }
        //    }
        //}


        public void LogObjectToConsole(object obj)
        {
            var output = JsonUtility.ToJson(obj, true);
            Debug.Log(output);
        }

    }
}
