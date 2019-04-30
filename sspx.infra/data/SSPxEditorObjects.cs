using System;
using System.Collections.Generic;
using System.Data;
using sspx.core.entities;

namespace sspx.infra.data
{
    public static class SSPxEditorObjects
    {
        #region public method

        public static ChecklistItem ItemMapper(IDataRecord row)
        {
            var item = new ChecklistItem
            {
                key = row.GetString(0),
                text = row.GetString(1),
                longText = row.GetString(2),
                hasItems = row.GetInt32(3),
                commentCount = row.GetInt32(4),
                itemType = row.GetNullableInt32(5),
                required = row.GetBoolean(6),
                versionKey = row.GetString(7),
                hidden = row.GetBoolean(8),
                condition = row.GetString(9)
            };

            return item;
        }

        public static ChecklistItem FullItemMapper(IDataRecord row)
        {
            var item = new ChecklistItem
            {
                key = row.GetString(0),
                text = row.GetString(1),
                longText = row.GetString(2),
                hasItems = row.GetInt32(3),
                commentCount = row.GetInt32(4),
                required = row.GetBoolean(5),
                condition = row.GetString(6),
                itemType = row.GetNullableInt32(7),
                comments = row.GetString(8),
                versionKey = row.GetString(9),
                reportText = row.GetString(10),
                answerDataTypeKey = row.GetNullableInt32(11),
                answerMaxReps = row.GetNullableInt16(12),
                answerMaxValue = row.GetNullableDecimal(13),
                answerMinValue = row.GetNullableDecimal(14),
                answerUnits = row.GetNullableInt32(15),
                answerMinReps = row.GetNullableInt16(16),
                hidden = row.GetBoolean(17)
            };

            return item;
        }

        //Zira Id SSP-98
        public static ChecklistItem CaseSummaryItemMapper(IDataRecord row)
        {
            var item = new ChecklistItem
            {
                key = row.IsDBNull(0) ? null : row.GetString(0),
                text = row.IsDBNull(1) ? null : row.GetString(1),
                longText = row.IsDBNull(2) ? null : row.GetString(2),
                hasItems = row.GetInt32(3),
                commentCount = row.GetInt32(4),
                required = row.GetBoolean(5),
                condition = row.IsDBNull(6) ? null : row.GetString(6),
                itemType = row.GetNullableInt32(7),
                comments = row.IsDBNull(8) ? null : row.GetString(8),
                versionKey = row.IsDBNull(9) ? null : row.GetString(9),
                reportText = row.IsDBNull(10) ? null : row.GetString(10),
                answerDataTypeKey = row.GetNullableInt32(11),
                answerMaxReps = row.GetNullableInt16(12),
                answerMaxValue = row.GetNullableDecimal(13),
                answerMinValue = row.GetNullableDecimal(14),
                answerUnits = row.GetNullableInt32(15),
                answerMinReps = row.GetNullableInt16(16),
                hidden = row.GetBoolean(17),
                parentid = row.IsDBNull(21) ? null : row.GetString(21)
            };

            return item;
        }
        //Zira Id SSP-98

        /// <summary>
        /// Added the below Method to Show Title, Cover and Author comments count in badge icon
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static ChecklistVersionCommentsSummary ChecklistVersionCommentsSummaryMapper(IDataRecord row)
        {
            var checklistVersionCommentsSummary = new ChecklistVersionCommentsSummary
            {
                Protocolkey = row.GetInt32(0),
                ProtocolVersionKey = row.GetInt32(1),
                Commenttype = row.IsDBNull(2) ? string.Empty : row.GetString(2),
                CommentCount = row.GetNullableInt32(3)
            };

            return checklistVersionCommentsSummary;
        }
        /// <summary>
        /// SSP 136 Get Protocol Comments
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static ItemComment CommentMapperForAll(IDataRecord row)
        {
            var comment = new ItemComment
            {
                key = row.GetInt32(0),
                commentId = row.GetInt32(1),
                comment = row.GetString(2),
                // title = row.GetString(3),
                firstName = row.GetString(3),
                lastName = row.GetString(4),
                dateAdded = row.GetDateTime(5),
                commentType = row.GetInt32(6),
                roleKey = row.GetInt32(7)
            };
            comment.dateString = comment.dateAdded.ToString("MM-dd-yyyy hh:mm:ss tt");
            return comment;
        }

        public static ChecklistVersion VersionMapper(IDataRecord row)
        {
            var version = new ChecklistVersion
            {
                key = row.GetString(0),
                version = row.GetString(1),
                webPostingDate = row.GetDateTime(2),
                protocolName = row.GetString(3),
                title = row.GetString(4),
                groupKey = row.GetString(5),
                groupName = row.GetString(6),
                cover = row.GetString(7)
            };
            version.webPostingDateString = version.webPostingDate.ToShortDateString();
            return version;
        }


        public static VersionComments VersionCommentMapper(IDataRecord row)
        {
            var versionComment = new VersionComments();
            versionComment.Version = row.GetString(2);
            versionComment.ReviewerID = row.GetNullableInt32(3);
            versionComment.ReviewerName = row.IsDBNull(4) ? null : row.GetString(4);
            versionComment.CommentsCount = row.GetInt32(5);
            versionComment.ProtocolVersionsKey = row.GetInt32(1);
            return versionComment;
        }

        public static EditorUser UserMapper(IDataRecord row)
        {
            var user = new EditorUser
            {
                userId = row.GetInt32(0),
                firstName = row.GetString(1),
                middleName = row.GetString(2),
                lastName = row.GetString(3),
                qualification = row.GetString(4),
                role = row.GetString(5)
            };

            return user;
        }

        public static ExplanatoryNote NoteMapper(IDataRecord row)
        {
            var note = new ExplanatoryNote
            {
                key = row.GetString(0),
                number = row.GetString(1),
                title = row.GetString(2),
                details = row.GetString(3),
                commentCount = row.GetInt32(4)
            };

            return note;
        }

        public static ExplanatoryNote NoteKeyMapper(IDataRecord row)
        {
            var note = new ExplanatoryNote
            {
                key = row.GetString(0),
                number = row.GetString(1),
                title = row.GetString(2)
            };

            return note;
        }

        public static ExplanatoryNote CaseSummaryNoteMapper(IDataRecord row)
        {
            var note = new ExplanatoryNote
            {
                key = row.GetString(0),
                number = row.GetString(1),
                title = row.GetString(2),
                details = row.IsDBNull(3) ? null : row.GetString(3)
            };
            return note;
        }

        public static ItemComment CommentMapper(IDataRecord row)
        {
            var comment = new ItemComment
            {
                key = row.GetInt32(0),
                commentId = row.GetInt32(1),
                comment = row.GetString(2),
                title = row.GetString(3),
                firstName = row.GetString(4),
                lastName = row.GetString(5),
                dateAdded = row.GetDateTime(6),
                commentType = row.GetInt32(7)
            };
            comment.dateString = comment.dateAdded.ToString("MM-dd-yyyy hh:mm:ss tt");
            return comment;
        }

        public static ItemReference ReferenceMapper(IDataRecord row)
        {
            var reference = new ItemReference
            {
                key = row.GetInt32(0),
                referenceId = row.GetString(1),
                referenceNumber = row.GetInt32(2),
                reference = row.GetString(3),
                noteTitle = row.GetString(4)
            };

            return reference;
        }

        public static ChecklistItemNode ChecklistItemNodeMapper(IDataRecord row)
        {
            var item = new ChecklistItemNode
            {
                id = row.GetString(0),
                parentId = row.GetString(1),
                commentCount = row.GetInt32(2),
                itemType = row.GetInt32(3),
                required = row.GetBoolean(4),
                titleHtml = row.GetString(5),
                hasItems = row.GetBoolean(6),
                version = row.GetString(7),
                hidden = row.GetBoolean(8),
                condition = row.GetString(9)
            };
            item.@checked = !item.hidden;
            return item;
        }

        public static KeyValuePair<String, String> KeyValueMapper(IDataRecord row)
        {
            var item = new KeyValuePair<String, String>(row.GetString(0), row.GetString(1));
            return item;
        }

        #endregion

        #region private data record extension methods

        private static Int32? GetNullableInt32(this IDataRecord row, int i)
        {
            if (row.IsDBNull(i))
            {
                return null;
            }
            Int32? value = row.GetInt32(i);
            return value;
        }

        private static Int16? GetNullableInt16(this IDataRecord row, int i)
        {
            if (row.IsDBNull(i))
            {
                return null;
            }
            Int16? value = row.GetInt16(i);
            return value;
        }

        private static Decimal? GetNullableDecimal(this IDataRecord row, int i)
        {
            if (row.IsDBNull(i))
            {
                return null;
            }
            Decimal? value = row.GetDecimal(i);
            return value;
        }

        private static Boolean? GetNullableBoolean(this IDataRecord row, int i)
        {
            if (row.IsDBNull(i))
            {
                return null;
            }
            Boolean? value = row.GetBoolean(i);
            return value;
        }

        #endregion
    }
}