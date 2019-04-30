using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class ChecklistItem
    {
        public string key = String.Empty;
        public string text = String.Empty;
        public string longText = String.Empty;
        public Int32 commentCount = 0;
        public Int32 hasItems = 0;
        public Boolean required = false;
        public string condition = string.Empty;
        public Int32? itemType = 0;
        public string comments = String.Empty;
        public string noteKeys = String.Empty;
        public string versionKey = String.Empty;
        public Boolean hidden = false;
        public List<ExplanatoryNote> notes = new List<ExplanatoryNote>();

        public string reportText = String.Empty;
        public Int32? answerDataTypeKey = null;
        public Int16? answerMaxReps = null;
        public Int16? answerMinReps = null;
        public Decimal? answerMaxValue = null;
        public Decimal? answerMinValue = null;
        public Int32? answerUnits = null;
        //Zira Id SSP-98
        public string parentid = String.Empty;
        public string notesText = String.Empty;
        public string protocolName = String.Empty;
        public List<ChecklistItem> Items = new List<ChecklistItem>();
        //Zira Id SSP-98
        public ChecklistItemNode ToChecklistItemNode(String parentId)
        {
            var item = new ChecklistItemNode
            {
                id = key,
                parentId = parentId,
                itemType = itemType.Value,
                hasItems = hasItems == 1,
                required = required && (itemType == 4 || itemType == 17 || itemType == 23),
                titleHtml = longText.Replace("\"", "&quot;"),
                commentCount = commentCount,
                version = versionKey,
                hidden = hidden,
                @checked = !hidden,
                condition = condition
            };
            foreach (var note in notes)
            {
                item.noteNumber.Add(note.number);
            }
            return item;
        }
    }

    public class ChecklistItemNode
    {
        public String id = String.Empty;
        public String parentId = String.Empty;
        public Int32 commentCount = 0;
        public Int32 itemType = 0;
        public Boolean required = false;
        public String titleHtml = String.Empty;
        public Boolean hasItems = false;
        public String version = String.Empty;
        public Boolean hidden = false;
        public Boolean @checked = false;
        public String condition = string.Empty;
        public List<String> noteNumber = new List<string>();
    }
}
