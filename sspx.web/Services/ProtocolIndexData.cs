using Microsoft.AspNetCore.Mvc;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;
using sspx.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Services
{
    public class ProtocolIndexData : IProtocolIndexData
    {
        private ISSPxConfig _config;

        public ProtocolIndexData([FromServices] ISSPxConfig config)
        {
            _config = config;
        }

        public ProtocolIndexModel GetForUser(int userKey)
        {
            var protocolIndexData = SSPxDBHelper.GetProtocolIndexForUser(_config.SSPxConnectionString, userKey);
            protocolIndexData = protocolIndexData ?? new List<ProtocolIndexDataRow>();

            var protocolUserData = SSPxDBHelper.GetProtocolVersionsUsersIncludingInactive(_config.SSPxConnectionString);
            protocolUserData = protocolUserData ?? new List<ProtocolVersionUserData>();

            return buildProtocolIndex(protocolIndexData, protocolUserData);
        }

        public ProtocolIndexModel Get()
        {
            throw new NotImplementedException("To be implement when we draft All Protocols screen");
        }
        //SSP 136 
        SSPxEditorDBHelper sspxEditorDBHelper = new SSPxEditorDBHelper();
        private ProtocolIndexModel buildProtocolIndex(List<ProtocolIndexDataRow> protocolIndexDataRows, List<ProtocolVersionUserData> protocolUserData)
        {
            var protocolIndex = new ProtocolIndexModel();
            var authors = protocolUserData.Where(u => (RoleTypes)u.RoleKey == RoleTypes.Author);

            protocolIndex.AllProtocolsCount = protocolIndexDataRows.Count;
            protocolIndex.PrimaryAuthorCount = protocolIndexDataRows.Where(d => (RoleTypes)d.RoleKey == RoleTypes.Author).Count();
            protocolIndex.ReviewerPanelCount = protocolIndexDataRows.Where(d => (RoleTypes)d.RoleKey == RoleTypes.Reviewer).Count();

            // TODO CS2:
            // confirm this is correct with CAP per DR in https://trello.com/c/DSQKeIKy
            protocolIndex.AuthorPanelCount = protocolIndexDataRows.Where(d => (RoleTypes)d.RoleKey == RoleTypes.Editor).Count();

            protocolIndex.Items = new List<ProtocolIndexModelItem>();
            foreach (var protocolIndexDataItem in protocolIndexDataRows)
            {
                //SSP 136 
                var comments = sspxEditorDBHelper.GetProtocolVersionCommentsForAll(_config.SSPxConnectionString, Convert.ToString(protocolIndexDataItem.ProtocolVersionKey));
                int commentsCount = CommentsCount(comments, protocolIndexDataItem.CommentsCount);

                var newItem = new ProtocolIndexModelItem
                {
                    ProtocolKey = protocolIndexDataItem.ProtocolKey,
                    ProtocolName = protocolIndexDataItem.ProtocolName,
                    ProtocolGroupName = protocolIndexDataItem.ProtocolGroupName,
                    ProtocolVersionKey = protocolIndexDataItem.ProtocolVersionKey,
                    ProtocolVersion = protocolIndexDataItem.ProtocolVersion,
                    Authors = new List<UserRole>(),
                    CurrentUserRole = (RoleTypes)protocolIndexDataItem.RoleKey,
                    //CommentsCount = protocolIndexDataItem.CommentsCount,
                    CommentsCount = commentsCount,
                    ProtocolVersionLastUpdatedDt = protocolIndexDataItem.ProtocolVersionLastUpdatedDt
                };

                var authorsForCurrentProtocol = authors
                    .Where(a => a.ProtocolVersionKey == protocolIndexDataItem.ProtocolVersionKey);

                foreach (var author in authorsForCurrentProtocol)
                {
                    newItem.Authors.Add(new UserRole
                    {
                        UserKey = author.UserKey,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        Role = RoleTypes.Author
                    });
                }

                protocolIndex.Items.Add(newItem);
            }

            return protocolIndex;
        }
        /// <summary>
        /// SSP 136 - Get totoal number of count for a protocol
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="protocolIndexDataItemCommentsCount"></param>
        /// <returns></returns>
        private int CommentsCount(List<ItemComment> comments, int protocolIndexDataItemCommentsCount)
        {
            int commentsCount = 0;
            if (protocolIndexDataItemCommentsCount != 0)
            {
                for (int i = 0; i < comments.Count; i++)
                {
                    if (comments[i].roleKey == Convert.ToInt32(RoleTypes.Author))
                        commentsCount += 1;
                    else if (comments[i].roleKey == Convert.ToInt32(RoleTypes.Reviewer))
                        commentsCount += 1;
                }
                return commentsCount;
            }
            else
                return 0;

        }
    }
}
