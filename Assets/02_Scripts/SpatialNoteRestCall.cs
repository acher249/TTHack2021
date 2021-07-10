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

        public void Start()
        {

            StartCoroutine(SequenceStart());

            //chain coroutines so that they execute sequentially
            IEnumerator SequenceStart()
            {
                Debug.Log("Start Sequence to get note data: ");

                //Checking for DEBUG mode
                // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
                string URL = "https://ar-points.herokuapp.com/api/note/get-my-notes";
                yield return StartCoroutine(GetRequest_SpatialNoteUtility(URL, (SpatialNoteRoot) =>
                {
                    Debug.Log("notes object returned: ");
                    Debug.Log(SpatialNoteRoot);
                    LogObjectToConsole(SpatialNoteRoot);

                    //for (var i = 0; i < SpatialNoteRoot.Notes.Length; i++)
                    //{
                    //    Debug.Log(SpatialNoteRoot.Notes[i].noteId);
                    //    Debug.Log(SpatialNoteRoot.Notes[i].mapId);

                    //}

                    // now do things with data



                }));


            }
        }

        IEnumerator GetRequest_SpatialNoteUtility(string uri, System.Action<SpatialNoteRoot> callback)
        {

           // pass these arguments
            string postData = @"
            {
                ""mapId"": ""myMap"",
              ""authorId"": ""elcin""
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
