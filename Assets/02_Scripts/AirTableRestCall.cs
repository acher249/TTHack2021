using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using shop;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


namespace shop
{
    public class AirTableRestCall : MonoBehaviour
    {

        // need new airtable call to get "Guest Codes" list.

        // To do To do

        public void Start()
        {

            //StartCoroutine(SequenceStart());

            //chain coroutines so that they execute sequentially
            //IEnumerator SequenceStart()
            //{
            //    Debug.Log("Start Sequence to get Guest Codes: ");

            //    //Checking for DEBUG mode
            //    // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
            //    string airtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/GuestCodes?api_key=keyHPsjb9CclmOT7i";
            //    yield return StartCoroutine(GetRequest_SHoPUtility(airtableURL, (records) =>
            //    {
            //        //not working

          
            //        //Debug.Log("Guest Codes Response: ");
            //        for( var i=0; i< records.Length; i++)
            //        {
            //            if (records[i].fields.GuestCodeList != null && records[i].fields.GuestCodeList != "")
            //            {
            //                Debug.Log(records[i].fields.GuestCodeList);

            //                // send the list to gamecontrol, then check against this list when entering a code.

            //            }
                        
            //        }

            //        // send data to game control


            //    }));


            //}
        }


        // SHoP utility rest call
        // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
        //IEnumerator GetRequest_SHoPUtility(string uri, System.Action<Record[]> callback)
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
        //            SHoP_PromptMessage.PromptMessage.ErrorPromptText(pages[page] + ": Error: " + webRequest.error, SHoP_PromptMessage.ExceptionTypes.Custom);
        //        }
        //        else
        //        {
        //            resultObject = webRequest.downloadHandler.text;
        //            var response = JsonUtility.FromJson<AirTable_ResponseClasses>(resultObject);

        //            // this populates global variable. Maybe a better way.
        //            //SHoPUtilities.LogObjectToConsole(response);
        //            callback(response.records);
        //        }
        //    }
        //}



        // --- old version using airtable

        //        public GameObject LoadingPanel;
        //        public GameObject ProjectVLG;
        //        public GameObject ProjectButton;
        //        private string clientEmailEndingCheck;
        //        public Text LoadingDebugText;
        //        public GameObject MessagePromptPanel;

        //        public void Start()
        //        {

        //            LoadingPanel.SetActive(true);
        //            StartCoroutine(SequenceStart());

        //            //chain coroutines so that they execute sequentially
        //            IEnumerator SequenceStart()
        //            {
        //                //Checking for DEBUG mode
        //                // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
        //                string airtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/DebugMode?api_key=keyHPsjb9CclmOT7i";
        //                yield return StartCoroutine(GetRequest_SHoPUtility(airtableURL, (records) =>
        //                {
        //                    var DebugMode = records[0].fields.DebugMode;
        //                    GameControl.control.DebugMode = DebugMode;
        //                    LoadingDebugText.gameObject.SetActive(DebugMode);

        //                    if (!DebugMode)
        //                    {
        //                        Debug.Log("DebugMode Is Off");
        //                    }
        //                    else if (DebugMode)
        //                    {
        //                        Debug.Log("DebugMode Is On");
        //                        LoadingDebugText.text = "Debug Mode Is On";
        //                    }
        //                }));

        //                //Checking for app version
        //                airtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/AppVersion?api_key=keyHPsjb9CclmOT7i";
        //        yield return StartCoroutine(GetRequest_SHoPUtility(airtableURL, (records) =>
        //        {
        //            var CurrentAppVersionOnDevice = float.Parse(Application.version);
        //            var MostCurrentAppstoreConnectTeamVersionAvailable = 0f;
        //            var MostCurrentExternalTesterVersionAvailable = 0f;
        //            var ForceAppUpdateVersion = 0f;

        //            // I need a loop here to make sure that I am grabbing the right values.. even if blank
        //            // fields are added accidently in airtable, reduces risk of human error.
        //            for (var i = 0; i < records.Length; i++)
        //            {
        //                if (records[i].fields.VersionType == "MostCurrentAppstoreConnectTeamVersionAvailable")
        //                {
        //                    MostCurrentAppstoreConnectTeamVersionAvailable = records[i].fields.Version;
        //                }
        //                else if (records[i].fields.VersionType == "MostCurrentExternalTesterVersionAvailable")
        //                {
        //                    MostCurrentExternalTesterVersionAvailable = records[i].fields.Version;
        //                }
        //                else if (records[i].fields.VersionType == "ForceAppUpdateVersion")
        //                {
        //                    ForceAppUpdateVersion = records[i].fields.Version;
        //                }
        //            }

        //            Debug.Log("Most Current Appstore Connect Team Version: " + MostCurrentAppstoreConnectTeamVersionAvailable);
        //            Debug.Log("Most Current External Tester Version: " + MostCurrentExternalTesterVersionAvailable);
        //            Debug.Log("Current Version On Device: " + CurrentAppVersionOnDevice);
        //            Debug.Log("Force App Update Version: " + ForceAppUpdateVersion);
        //            // we need to fill a list with approved emails to check against later, when the user signs in..

        //            // Need to check against a list of App Store Connect
        //            string[] AppStoreConnectTeam = {"ajc@shoparc.com", "saa@shoparc.com", "jdc@shoparc.com", "myf@shoparc.com",
        //                    "dlg@shoparc.com","cwm@shoparc.com","ors@shoparc.com","wws@shoparc.com","crs@shoparc.com","tms@shoparc.com","cds@shoparc.com",
        //                    "gap@shoparc.com"};

        //            // If the current signed in user is on the list check their current version on device against
        //            for (var i = 0; i < AppStoreConnectTeam.Length; i++)
        //            {
        //                if (GameControl.control.UserEmail == AppStoreConnectTeam[i])
        //                {
        //                    GameControl.control.UserIsOnAppStoreConnectTeam = true;
        //                }
        //            }

        //            if (GameControl.control.UserIsOnAppStoreConnectTeam == true)
        //            {
        //                checkAppVersion(MostCurrentAppstoreConnectTeamVersionAvailable, CurrentAppVersionOnDevice, ForceAppUpdateVersion);
        //            }
        //            else
        //            {
        //                checkAppVersion(MostCurrentExternalTesterVersionAvailable, CurrentAppVersionOnDevice, ForceAppUpdateVersion);
        //            }
        //        }));

        //        //Checking for verified client email list
        //        airtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/Client?api_key=keyHPsjb9CclmOT7i";
        //        yield return StartCoroutine(GetRequest_SHoPUtility(airtableURL, (records) =>
        //        {
        //            for (var i = 0; i < records.Length; i++)
        //            {
        //                // we need to fill a list with approved emails to check against later, when the user signs in..
        //                if (records[i].fields.ClientEmailEndingForVerification != null &&
        //                    !GameControl.control.ApprovedClientEmailEndingList.Contains(records[i].fields.ClientEmailEndingForVerification))
        //                {
        //                    GameControl.control.ApprovedClientEmailEndingList.Add(records[i].fields.ClientEmailEndingForVerification);
        //                }
        //            }
        //        }));

        //        ////Checking for featured project list
        //        airtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/ProjectPortal?api_key=keyHPsjb9CclmOT7i&fields%5B%5D=FeaturedProject";
        //        yield return StartCoroutine(GetRequest_SHoPUtility(airtableURL, (records) =>
        //        {
        //            for (var i = 0; i < records.Length; i++)
        //            {
        //                // we need to fill a list with Featured Projects, to pull into public page
        //                if (records[i].fields.FeaturedProject != null && records[i].fields.FeaturedProject == "1")
        //                {
        //                    GameControl.control.FeaturedProjectsIDList.Add(records[i].id);
        //                }
        //            }

        //            // only run this if the user is not signed in
        //            if (GameControl.control.UserEmail == "")
        //            {
        //                GetAllFeaturedProjectsFromAirtableThenPopulateUI();
        //            }

        //        }));
        //            }
        //}

        //// when we hit back button from auth back to featured projects, if the panel has already instantiated with featured projects,
        //// don't go intantiate again. if it has instantiated with all projects (user signed in), we do need to go clear that and instantiate again.

        //// also need to clear this when going the other way. if this panel has been populated with featured projects, we need to clear it out before
        //// we instantiate in all of the project buttons when user signs in.


        //public void GetAllFeaturedProjectsFromAirtableThenPopulateUI()
        //        {
        //            // the user is not logged in at this point, we can just go grab the right projects and instantiate
        //            //Debug.Log("getting all featured projects");
        //            string projectsAirtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/ProjectPortal?api_key=keyHPsjb9CclmOT7i";

        //            // using the project button prefab referenced within the scene instead of pulling from directory.

        //            // clear the project list if it was filled prior
        //            if (GameControl.control.ProjectListPopulatedWithClientProjects == true)
        //            {
        //                ClearProjectList();
        //                ProjectButton.SetActive(true);
        //            }

        //            StartCoroutine(GetRequest_AllFeaturedProjects(projectsAirtableURL));

        //        }

        //        // some of this can be abstracted and re-used
        //        IEnumerator GetRequest_AllFeaturedProjects(string uriProjects)
        //        {
        //            Record[] records_Projects = null;

        //            yield return StartCoroutine(GetRequest_SHoPUtility(uriProjects, (records) =>
        //            {
        //                records_Projects = records;
        //            }));

        //            for (var i = 0; i < records_Projects.Length; i++)
        //            {
        //                if (GameControl.control.FeaturedProjectsIDList.Contains(records_Projects[i].id))
        //                {
        //                    //Debug.Log(records_Projects[i].id);

        //                    if (VerifyProjectIsPopulatedCompletelyInAirtable(records_Projects[i]))
        //                    {
        //                        /////// pass project object straight to button info /////
        //                        if (ProjectVLG.transform.childCount < GameControl.control.FeaturedProjectsIDList.Count + 1)
        //                        {
        //                            InstantiateButtonIntoUI(records_Projects[i].fields);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Debug.LogError("THERE IS A PROJECT IN AIRTABLE THAT IS NOT SUFFICIENTLY FILLED OUT");
        //                    }
        //                }
        //            }

        //            ProjectButton.SetActive(false);
        //            GameControl.control.ProjectListPopulatedWithClientProjects = false;
        //            //GameControl.control.ProjectListPopulatedWithFeaturedProjects = true;
        //            LoadingDebugText.text = "";
        //            LoadingPanel.SetActive(false);
        //        }

        //        public void GetAllProjectInformationFromAirtableThenPopulateUI()
        //        {
        //            //var emailCompanyCheckArrayOne = GameControl.control.UserEmail.Split(char.Parse("@"));
        //            //var SplitString = emailCompanyCheckArrayOne[1];
        //            //clientEmailEndingCheck = "@" + SplitString;

        //            ////Debug.Log("inside GetAllProjectInformationFromAirtableThenPopulateUI");

        //            //// clear the project list if it was filled prior or 
        //            ////if (GameControl.control.ProjectListPopulatedWithFeaturedProjects == true)
        //            ////{
        //            //    ClearProjectList();
        //            ////}

        //            //// SECURITY VERIFICATION FIRST BEFORE GOING TO GET ALL PROJECT INFORMATION
        //            //if (GameControl.control.ApprovedClientEmailEndingList.Count > 0)
        //            //{
        //            //    if (GameControl.control.ApprovedClientEmailEndingList.Contains(clientEmailEndingCheck))
        //            //    {
        //            //        Debug.Log("Successful Email Verification. Approved Email Endings List Contains: " + clientEmailEndingCheck + ", now going to get all project information.");
        //            //        LoadingDebugText.text = "Successful Email Verification. Approved Email Endings List Contains: " + clientEmailEndingCheck + ", now going to get all project information.";

        //            //        string projectsAirtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/ProjectPortal?api_key=keyHPsjb9CclmOT7i";
        //            //        string clientAirtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/Client?api_key=keyHPsjb9CclmOT7i";
        //            //        string approvedProjectUsersAirtableURL = "https://api.airtable.com/v0/appsnQkBEHzrtR02H/ApprovedProjectUserList?api_key=keyHPsjb9CclmOT7i";

        //            //        StartCoroutine(GetRequest_AllProjectInformation(projectsAirtableURL, clientAirtableURL, approvedProjectUsersAirtableURL));
        //            //    }
        //            //    else
        //            //    {
        //            //        Debug.Log("This user is either not a SHoP client or does not have any projects in Project Portal");
        //            //        SHoP_PromptMessage.PromptMessage.ErrorPromptText("This user is either not a SHoP client or does not have any projects in Project Portal",SHoP_PromptMessage.ExceptionTypes.Custom);
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    SHoP_PromptMessage.PromptMessage.ErrorPromptText("Airtable rest call too slow.. Did not fill GameControl with ApprovedClientEmail List",SHoP_PromptMessage.ExceptionTypes.Custom);
        //            //}
        //        }


        //        IEnumerator GetRequest_AllProjectInformation(string uriProjects, string uriClients, string uriApprovedUsers)
        //        {
        //            //Debug.Log("getting all GetRequest_AllProjectInformation");
        //            //======================================================================================
        //            // Grabbing all projects info
        //            //======================================================================================
        //            Record[] records_Projects = null;
        //            yield return StartCoroutine(GetRequest_SHoPUtility(uriProjects, (records) =>
        //            {
        //                records_Projects = records;
        //            }));

        //            //======================================================================================
        //            // Grabbing the client list associated with each project
        //            //======================================================================================
        //            Record[] records_Clients = null;
        //            yield return StartCoroutine(GetRequest_SHoPUtility(uriClients, (records) =>
        //            {
        //                records_Clients = records;
        //            }));

        //            //======================================================================================
        //            // Grabbing projects specially approved for the client's individual email address
        //            //======================================================================================

        //            //thl grabbing the list of approved users for all special projects
        //            Record[] records_ApprovedUsers = null;
        //            yield return StartCoroutine(GetRequest_SHoPUtility(uriApprovedUsers, (records) =>
        //            {
        //                records_ApprovedUsers = records;
        //            }));

        //            //thl checking against the list of ApprovedProjectUsers, and grabbing the specially approved projectID's
        //            string[] approved_ProjectIDs = new string[0];
        //            foreach (Record r in records_ApprovedUsers)
        //            {
        //                if (r.fields.Email != null && r.fields.Email.Trim() == GameControl.control.UserEmail.Trim())
        //                {
        //                    approved_ProjectIDs = r.fields.ApprovedProjects;
        //                    break;
        //                }
        //            }


        //            //======================================================================================
        //            // Grabbing projects associated with the client's firm
        //            //======================================================================================
        //            Record ClientRecord = null;
        //            //Grabbing the right client record
        //            foreach (Record client in records_Clients)
        //            {
        //                if (client.fields.ClientEmailEndingForVerification == clientEmailEndingCheck)
        //                {
        //                    ClientRecord = client;
        //                    break;
        //                }
        //            }

        //            //======================================================================================
        //            // Combining projects approved for client's firm and individual email address
        //            //======================================================================================

        //            //thl Grabbing the right Project ID's for that client
        //            List<string> projectIDs = new List<string>(ClientRecord.fields.Projects);

        //            //thl if indeed there are specially approved projects for this user
        //            if (approved_ProjectIDs != null && approved_ProjectIDs.Length > 0)
        //            {
        //                foreach (string projectID in approved_ProjectIDs)
        //                {
        //                    //double checking if that project has already been added to the project list
        //                    if (!projectIDs.Contains(projectID))
        //                        projectIDs.Add(projectID);
        //                }
        //            }

        //            //======================================================================================
        //            // Populating project buttons for all approved projects for this specific client
        //            //======================================================================================
        //            //Iterating through all projects
        //            for (var i = 0; i < records_Projects.Length; i++)
        //            {
        //                if (projectIDs.Contains(records_Projects[i].id))
        //                {
        //                    if (VerifyProjectIsPopulatedCompletelyInAirtable(records_Projects[i]))
        //                    {
        //                        /////// pass project object straight to button info /////
        //                        InstantiateButtonIntoUI(records_Projects[i].fields);
        //                    }
        //                    else
        //                    {
        //                        Debug.LogError("THERE IS A PROJECT IN AIRTABLE THAT IS NOT SUFFICIENTLY FILLED OUT");
        //                    }
        //                }
        //            }

        //            ProjectButton.SetActive(false);
        //            GameControl.control.ProjectListPopulatedWithClientProjects = true;
        //            //GameControl.control.ProjectListPopulatedWithFeaturedProjects = false;
        //            LoadingDebugText.text = "";
        //            LoadingPanel.SetActive(false);
        //        }

        //        /////////////////// Utilities  /////////////////////

        //        public void InstantiateButtonIntoUI(Project project)
        //        {
        //            var newButton = Instantiate(ProjectButton);

        //            newButton.transform.SetParent(ProjectVLG.transform);
        //            newButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        //            var projectNameText = newButton.transform.GetChild(0).gameObject.GetComponent<Text>();
        //            projectNameText.text = project.ProjectName;
        //            var projectAddressText = newButton.transform.GetChild(1).gameObject.GetComponent<Text>();
        //            projectAddressText.text = project.ProjectAddress;

        //            var ButtonInfo = newButton.GetComponent<ProjectButtonInfo>();

        //            // ajc - once further in the migration, switchthis below from project to fieldDataObject

        //            // pass object to button
        //            //ButtonInfo.Project = project;

        //            var ProjectNameText = ButtonInfo.transform.GetChild(0).gameObject;
        //            var StatusUpdate = ProjectNameText.transform.GetChild(0).gameObject;
        //            StatusUpdate.SetActive(ButtonInfo.Project.fieldData.HasConstructionData);

        //            newButton.SetActive(true);
        //        }

        //        // check to make sure that all fields are complete in airtable.
        //        private bool VerifyProjectIsPopulatedCompletelyInAirtable(Record projectRecord)
        //        {
        //            if (projectRecord.fields.AssetBundleVersion != 0 &&
        //                projectRecord.fields.BundleDirectDownloadUrl_And != null &&
        //                projectRecord.fields.BundleDirectDownloadUrl_iOS != null &&
        //                projectRecord.fields.BundleDirectDownloadUrl_OSX != null &&
        //                projectRecord.fields.BundleDirectDownloadUrl_Win != null &&
        //                projectRecord.fields.SectionCutBoxColliderExtents != null &&
        //                projectRecord.fields.OrbitCameraTransformPosition != null &&
        //                projectRecord.fields.OrbitCameraTransformRotation != null &&
        //                projectRecord.fields.PrefabInsideAssetBundleName != null &&
        //                projectRecord.fields.ProjectAddress != null &&
        //                projectRecord.fields.ProjectName != null &&
        //                projectRecord.fields.ShadowDistance != 0 &&
        //                (projectRecord.fields.Client != null || projectRecord.fields.ApprovedProjectUserList != null)
        //                )
        //            {
        //                //Debug.Log(projectRecord.fields.ProjectName + " HAS ALL VALUES IN AIRTABLE");
        //                return true;
        //            }
        //            else
        //            {
        //                //Debug.Log(projectRecord.fields.ProjectName + " DOES NOT HAVE ALL VALUES IN AIRTABLE");
        //                return false;
        //            }
        //        }

        //        public void ClearProjectList()
        //        {
        //            for (var i = 1; i < ProjectVLG.transform.childCount; i++)
        //            {
        //                // we dont want to destroy the button prefab sitting at index[0]
        //                Destroy(ProjectVLG.transform.GetChild(i).gameObject);
        //            }

        //            //GameControl.control.ProjectListPopulatedWithFeaturedProjects = false;
        //            GameControl.control.ProjectListPopulatedWithClientProjects = false;
        //        }

        //        // SHoP utility rest call
        //        // Now SHoPUtility has callback as a parameter to basically return the records from the REST call for async operations 
        //        IEnumerator GetRequest_SHoPUtility(string uri, System.Action<Record[]> callback)
        //        {
        //            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        //            {
        //                // Request and wait for the desired page.
        //                yield return webRequest.SendWebRequest();

        //                string resultObject = "";

        //                string[] pages = uri.Split('/');
        //                int page = pages.Length - 1;

        //                if (webRequest.isNetworkError)
        //                {
        //                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
        //                    SHoP_PromptMessage.PromptMessage.ErrorPromptText(pages[page] + ": Error: " + webRequest.error, SHoP_PromptMessage.ExceptionTypes.Custom);
        //                }
        //                else
        //                {
        //                    resultObject = webRequest.downloadHandler.text;
        //                    var response = JsonUtility.FromJson<AirTable_ResponseClasses>(resultObject);

        //                    // this populates global variable. Maybe a better way.
        //                    callback(response.records);
        //                }
        //            }
        //        }


        //        private void checkAppVersion(float AirtableVersion, float DeviceVersion, float ForceAppUpdateVersion)
        //        {
        //            LoadingDebugText.text = "checked app version";
        //            // For External Testers check against external testers version before showing update.
        //            if (AirtableVersion > DeviceVersion)
        //            {
        //                // Display message to update application
        //                SHoP_PromptMessage.PromptMessage.ErrorPromptText(SHoP_PromptMessage.ErrorShortHands.Update_App.ToString(), SHoP_PromptMessage.ExceptionTypes.Custom);

        //                // ajc - Force app update if below certain version determined by airtable field.
        //                if (DeviceVersion < ForceAppUpdateVersion)
        //                {
        //                    Debug.Log("ajc - Should Force Update...");
        //                    // disable the x button of the prompt dialogue so that the user must update before they can use the applicaiton.
        //                    MessagePromptPanel.GetComponent<Button>().enabled = false;
        //                    MessagePromptPanel.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        //                }
        //            }
        //        }
    }
}
