using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using StickyNotes;

namespace spatialNotes
{
    public class SpatialNoteRestCall : MonoBehaviour
    {
        public GameObject DebugPanel;
        public Text DebugPanelText;

        public GameObject SpatialNoteInputForm;

        // spatial note input fields
        public InputField noteTitle_Inputfield;
        //public InputField noteData_Inputfield;
        //public InputField mapId_Inputfield;
        //public InputField authorId_Inputfield;
        public InputField nodeType_Inputfield;
        public InputField identifier_Inputfield;
        //public InputField lastMaintained_Inputfield;
        public Text maintainceFrequency_DropdownLabel; // switch this to a dropdown
        public InputField notes_Inputfield;

        public NotesManager notesMgr;

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

        // ---------------------- Create Note -------------------------- //

        public void CreateNote()
        {
            // get note data from input fields

            var noteId1 = GameControl.control.CreateNewNote_NoteId; // make sure to clear these
            var mapId1 = GameControl.control.CurrentMapName; // make sure to clear these
            var authorId1 = GameControl.control.userName; // make sure to clear these
            var lastMaintained1 = DateTime.Now.ToString();

            var noteTitle1 = noteTitle_Inputfield.text;
            var nodeType1 = nodeType_Inputfield.text;
            var identifier1 = identifier_Inputfield.text;
            var maintainceFrequency1 = maintainceFrequency_DropdownLabel.text;

            var notes1 = notes_Inputfield.text;


            StartCoroutine(SequenceStart(noteId1, noteTitle1, mapId1, authorId1, nodeType1,
                    identifier1, lastMaintained1, maintainceFrequency1, notes1));

            //chain coroutines so that they execute sequentially
            IEnumerator SequenceStart(string noteId, string noteTitle, string mapId, string authorId, string nodeType,
                string identifier, string lastMaintained, string maintainceFrequency, string notes
                )
            {
                Debug.Log("Start Sequence to create note: ");

                //Checking for DEBUG mode
                // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
                string URL = "https://ar-points.herokuapp.com/api/note/new";
                yield return StartCoroutine(GetRequest_CreateNewNote(

                    // inputs for note data
                    noteId, noteTitle, mapId, authorId, nodeType,
                    identifier, lastMaintained, maintainceFrequency, notes,

                    URL, (SpatialNote) =>
                {
                    Debug.Log("WE SAVED A NOTE:");
                    LogObjectToConsole(SpatialNote);

                    // Now push all of this data to the note on the scene
                    var currentNote = notesMgr.mCurrNote;
                    var noteDataHolder = currentNote.GetComponent<NoteDataHolder>();

                    noteDataHolder.noteId = SpatialNote.noteId;
                    noteDataHolder.noteTitle = SpatialNote.noteTitle;
                    noteDataHolder.mapId = SpatialNote.mapId;
                    noteDataHolder.authorId = SpatialNote.authorId;
                    noteDataHolder.nodeType = SpatialNote.nodeType;
                    noteDataHolder.identifier = SpatialNote.identifier;
                    noteDataHolder.lastMaintained = SpatialNote.lastMaintained;
                    noteDataHolder.maintainceFrequency = SpatialNote.maintainceFrequency;
                    noteDataHolder.notes = SpatialNote.notes;

                    // send to front end of note
                    noteDataHolder.titleText.text = SpatialNote.noteTitle;
                    noteDataHolder.authorText.text = SpatialNote.authorId;
                    noteDataHolder.NoteTypeText.text = SpatialNote.nodeType;
                    noteDataHolder.IdentifierText.text = SpatialNote.identifier;
                    noteDataHolder.FrequencyText.text = SpatialNote.maintainceFrequency;
                    noteDataHolder.NotesText.text = SpatialNote.notes;

                    // now send this note off through sticky notes
                    notesMgr.OnNoteClosed(SpatialNote.noteTitle);

                    // close ui dialogue
                    SpatialNoteInputForm.SetActive(false);


                }));


            }
        }

        // need to pass in all the note data
        IEnumerator GetRequest_CreateNewNote(

            // inputs for note data
            string noteId, string noteTitle, string mapId, string authorId, string nodeType,
            string identifier, string lastMaintained, string maintainceFrequency, string notes,

            string uri, System.Action<Note> callback)
        {

            // pass these arguments
            string postData = @"
            {
                ""noteId"": """ + noteId + @""",
                ""noteTitle"": """ + noteTitle + @""",
                ""noteTitle"": "" Note Data "",
                ""mapId"": """ + mapId + @""",
                ""authorId"": """ + authorId + @""",
                ""nodeType"": """ + nodeType + @""",
                ""identifier"": """ + identifier + @""",
                ""lastMaintained"": """ + lastMaintained + @""",
                ""maintainceFrequency"": """ + maintainceFrequency + @""",
                ""notes"": """ + notes + @"""

            }";

            Debug.Log("postData: ");
            Debug.Log(postData);

            // debug
            DebugPanel.SetActive(true);
            DebugPanelText.text =  "CREATING NOTE: " + postData;

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

                    Debug.Log(resultObject);

                    //var response = JsonUtility.FromJson<Note[]>("{\"Notes\":" + resultObject + "}");
                    var response = JsonUtility.FromJson<Note>(resultObject);

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
