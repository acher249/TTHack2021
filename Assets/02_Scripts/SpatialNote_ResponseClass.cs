using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace spatialNotes
{

    [Serializable]
    public class SpatialNote_ResponseClass
    {
        public SpatialNoteRoot SpatialNotes;
    }

    [Serializable]
   public class SpatialNoteRoot
    {
       public Note[] Notes;
   }

   [Serializable]
   public class Note
    {
        public string _id;
        public string noteId;
        public string noteTitle;
        public string mapId;
        public string authorId;
        public string nodeType;
        public string identifier;
        public string lastMaintained;
        public string dateCreated;
        public string notes;

   }

}