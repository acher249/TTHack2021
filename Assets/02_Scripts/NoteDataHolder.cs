using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteDataHolder : MonoBehaviour
{

    [Header("Connect to Front End")]
    public Text titleText;
    public Text authorText;
    public Text NoteTypeText;
    public Text IdentifierText;
    public Text FrequencyText;
    public Text NotesText;


    [Header("Data")]
    public string _id;
    public string noteId;
    public string noteTitle;
    public string noteData;
    public string mapId;
    public string authorId;
    public string nodeType;
    public string identifier;
    public string lastMaintained;
    public string maintainceFrequency;
    public string dateCreated;
    public string notes;

}
