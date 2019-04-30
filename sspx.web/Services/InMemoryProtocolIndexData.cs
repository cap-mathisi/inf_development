using System;
using System.Collections.Generic;
using System.Linq;
using sspx.core.entities;
using sspx.web.Models;

namespace sspx.web.Services
{
    public class InMemoryProtocolIndexData : IProtocolIndexData
    {
        private List<UserProtocolIndexModel> _lookup;

        private class UserProtocolIndexModel
        {
            public int UserKey;
            public ProtocolIndexModel ProtocolIndexModel;
        }

        public InMemoryProtocolIndexData()
        {
            _lookup = new List<UserProtocolIndexModel>
            {
                #region admin user - some "author", some "reviewer"

                new UserProtocolIndexModel
                {
                    UserKey = 194,
                    ProtocolIndexModel = new ProtocolIndexModel
                    {
                        // TODO CS2:
                        AllProtocolsCount = 5,
                        PrimaryAuthorCount = 2,
                        ReviewerPanelCount = 2,
                        AuthorPanelCount = 1,
                        Items = new List<ProtocolIndexModelItem>()
                        {
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Breast Invasive",
                                ProtocolKey = 5,
                                ProtocolGroupName = "Breast",
                                ProtocolVersionKey = 205,
                                ProtocolVersion = "3.3.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 194,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Author
                                    }
                                },
                                CurrentUserRole = RoleTypes.Author,
                                CommentsCount = 2,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-21)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Colon and Rectum",
                                ProtocolKey = 7,
                                ProtocolGroupName = "Gastrointestinal",
                                ProtocolVersionKey = 207,
                                ProtocolVersion = "3.4.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 194,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Author
                                    },
                                    new UserRole
                                    {
                                        UserKey = 116,
                                        FirstName = "Astor",
                                        LastName = "Piazolla",
                                        Role = RoleTypes.Author
                                    },
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Igor",
                                        LastName = "Stravinski",
                                        Role = RoleTypes.Author
                                    }
                                },
                                CurrentUserRole = RoleTypes.Author,
                                CommentsCount = 0,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-4)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Urethra",
                                ProtocolKey = 52,
                                ProtocolGroupName = "Genitourinary",
                                ProtocolVersionKey = 124,
                                ProtocolVersion = "3.2.1.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Igor",
                                        LastName = "Stravinski",
                                        Role = RoleTypes.Author
                                    },
                                    new UserRole
                                    {
                                        UserKey = 4,
                                        FirstName = "Will",
                                        LastName = "Ferrell",
                                        Role = RoleTypes.Reviewer
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 3,
                                ProtocolVersionLastUpdatedDt = DateTime.Now
                              },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Nasal Cavity and Paranasal Sinuses",
                                ProtocolKey = 27,
                                ProtocolGroupName = "Nasal Cavity",
                                ProtocolVersionKey = 97,
                                ProtocolVersion = "3.2.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 194,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    },
                                    new UserRole
                                    {
                                        UserKey = 5,
                                        FirstName = "Spike",
                                        LastName = "Jones",
                                        Role = RoleTypes.Author
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 0,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-21)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Pharynx",
                                ProtocolKey = 36,
                                ProtocolGroupName = "Pharynx",
                                ProtocolVersionKey = 107,
                                ProtocolVersion = "3.3.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 194,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Editor
                                    },
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Igor",
                                        LastName = "Stravinski",
                                        Role = RoleTypes.Author
                                    },
                                },
                                CurrentUserRole = RoleTypes.Editor,
                                CommentsCount = 7,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddMonths(-6)
                            }
                        }
                    }
                },
                #endregion

                #region test user - "reviewer" for all
    
                new UserProtocolIndexModel
                {
                    UserKey = 116,
                    ProtocolIndexModel = new ProtocolIndexModel
                    {
                        // TODO CS2:
                        AllProtocolsCount = 5,
                        PrimaryAuthorCount = 0,
                        ReviewerPanelCount = 5,
                        AuthorPanelCount = 0,
                        Items = new List<ProtocolIndexModelItem>()
                        {
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Breast Invasive",
                                ProtocolKey = 5,
                                ProtocolGroupName = "Breast",
                                ProtocolVersionKey = 205,
                                ProtocolVersion = "3.3.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 116,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 2,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-21)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Colon and Rectum",
                                ProtocolKey = 7,
                                ProtocolGroupName = "Gastrointestinal",
                                ProtocolVersionKey = 207,
                                ProtocolVersion = "3.4.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 116,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    },
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Igor",
                                        LastName = "Stravinski",
                                        Role = RoleTypes.Author
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 0,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-4)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Urethra",
                                ProtocolKey = 52,
                                ProtocolGroupName = "Genitourinary",
                                ProtocolVersionKey = 124,
                                ProtocolVersion = "3.2.1.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 116,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    },
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Igor",
                                        LastName = "Stravinski",
                                        Role = RoleTypes.Author
                                    },
                                    new UserRole
                                    {
                                        UserKey = 4,
                                        FirstName = "Will",
                                        LastName = "Ferrell",
                                        Role = RoleTypes.Reviewer
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 3,
                                ProtocolVersionLastUpdatedDt = DateTime.Now
                              },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Nasal Cavity and Paranasal Sinuses",
                                ProtocolKey = 27,
                                ProtocolGroupName = "Nasal Cavity",
                                ProtocolVersionKey = 97,
                                ProtocolVersion = "3.2.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 116,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    },
                                    new UserRole
                                    {
                                        UserKey = 5,
                                        FirstName = "Spike",
                                        LastName = "Jones",
                                        Role = RoleTypes.Author
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 0,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-21)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Pharynx",
                                ProtocolKey = 36,
                                ProtocolGroupName = "Pharynx",
                                ProtocolVersionKey = 107,
                                ProtocolVersion = "3.3.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 116,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    },
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Igor",
                                        LastName = "Stravinski",
                                        Role = RoleTypes.Author
                                    },
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 7,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddMonths(-6)
                            }
                        }
                    }
                },
                #endregion

                #region staff user - "reviewer" for all
    
                new UserProtocolIndexModel
                {
                    UserKey = 203,
                    ProtocolIndexModel = new ProtocolIndexModel
                    {
                        // TODO CS2:
                        AllProtocolsCount = 5,
                        PrimaryAuthorCount = 0,
                        ReviewerPanelCount = 5,
                        AuthorPanelCount = 0,
                        Items = new List<ProtocolIndexModelItem>()
                        {
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Breast Invasive",
                                ProtocolKey = 5,
                                ProtocolGroupName = "Breast",
                                ProtocolVersionKey = 205,
                                ProtocolVersion = "3.3.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 2,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-21)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Colon and Rectum",
                                ProtocolKey = 7,
                                ProtocolGroupName = "Gastrointestinal",
                                ProtocolVersionKey = 207,
                                ProtocolVersion = "3.4.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 0,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-4)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Urethra",
                                ProtocolKey = 52,
                                ProtocolGroupName = "Genitourinary",
                                ProtocolVersionKey = 124,
                                ProtocolVersion = "3.2.1.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    },
                                    new UserRole
                                    {
                                        UserKey = 4,
                                        FirstName = "Will",
                                        LastName = "Ferrell",
                                        Role = RoleTypes.Reviewer
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 3,
                                ProtocolVersionLastUpdatedDt = DateTime.Now
                              },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Nasal Cavity and Paranasal Sinuses",
                                ProtocolKey = 27,
                                ProtocolGroupName = "Nasal Cavity",
                                ProtocolVersionKey = 97,
                                ProtocolVersion = "3.2.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    },
                                    new UserRole
                                    {
                                        UserKey = 5,
                                        FirstName = "Spike",
                                        LastName = "Jones",
                                        Role = RoleTypes.Author
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 0,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddDays(-21)
                            },
                            new ProtocolIndexModelItem()
                            {
                                ProtocolName = "Pharynx",
                                ProtocolKey = 36,
                                ProtocolGroupName = "Pharynx",
                                ProtocolVersionKey = 107,
                                ProtocolVersion = "3.3.0.0",
                                Authors = new List<UserRole>
                                {
                                    new UserRole
                                    {
                                        UserKey = 203,
                                        FirstName = "Test Admin",
                                        LastName = "User",
                                        Role = RoleTypes.Reviewer
                                    }
                                },
                                CurrentUserRole = RoleTypes.Reviewer,
                                CommentsCount = 7,
                                ProtocolVersionLastUpdatedDt = DateTime.Now.AddMonths(-6)
                            }
                        }
                    }
                }
                #endregion

            };
        }

        public ProtocolIndexModel GetForUser(int userKey)
        {
            var query = _lookup.Where(u => u.UserKey == userKey);

            if (query.Any())
            {
                return query.First().ProtocolIndexModel;
            }

            return new ProtocolIndexModel();
        }

        public ProtocolIndexModel Get()
        {
            // TODO CS2
            throw new NotImplementedException("To be implement when we draft All Protocols screen");
        }
    }
}
