using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Builder;

namespace sspx.web
{
    public static class Routes
    {
        public static void GetRoutes(IRouteBuilder routes)
        {
            // Default
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=SSPxProtocol}/{pid=7}"
            );

            // Protocol Details
            routes.MapRoute(
                name: "SSPxProtocol",
                template: "Protocols/SSPxProtocol/{pid?}",
                defaults: new { controller = "Home", action = "SSPxProtocol" }
            );

            // TODO CS2:
            routes.MapRoute(
                name: "SSPxProtocol_responsive",
                template: "Protocols/SSPxProtocol_responsive/{pid?}",
                defaults: new { controller = "Home", action = "SSPxProtocol_responsive" }
            );

            routes.MapRoute(
                name: "SSPxCompareVersions",
                template: "Protocols/SSPxCompareVersions/{pid?}",
                defaults: new { controller = "Home", action = "SSPxCompareVersions" }
            );

            routes.MapRoute(
                name: "SSPxImportItems",
                template: "Protocols/SSPxImportItems/{pid?}",
                defaults: new { controller = "Home", action = "SSPxImportItems" }
            );

            routes.MapRoute(
                name: "SSPxItemChildren",
                template: "Protocols/SSPxItem/{pid}/children",
                defaults: new { controller = "Home", action = "SSPxItemChildren" }
            );

            routes.MapRoute(
                name: "SSPxNote",
                template: "Protocols/SSPxNote/{pid}/notes/{noteid}",
                defaults: new { controller = "Home", action = "SSPxNote" }
            );

            routes.MapRoute(
                name: "SSPxVersion",
                template: "Protocols/SSPxVersion/{pid}",
                defaults: new { controller = "Home", action = "SSPxVersion" }
            );

            routes.MapRoute(
                name: "SSPxProtocolNote",
                template: "Protocols/SSPxProtocolNote/{pid?}",
                defaults: new { controller = "Home", action = "SSPxProtocolNote" }
            );

            routes.MapRoute(
                name: "SSPxProtocolPreview",
                template: "Protocols/SSPxProtocolPreview/{nid}",
                defaults: new { controller = "Home", action = "SSPxProtocolPreview" }
            );

            routes.MapRoute(
                name: "SSPxProtocolReader",
                template: "Protocols/SSPxProtocolReader/{vid}",
                defaults: new { controller = "Home", action = "SSPxProtocolReader" }
            );

            routes.MapRoute(
                name: "SSPxSaveNote",
                template: "Protocols/SSPxItem/{id}/notes",
                defaults: new { controller = "Home", action = "SSPxProtocolSaveNote" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxItem",
                template: "Protocols/SSPxItem/{id}/{pid}",
                defaults: new { controller = "Home", action = "SSPxItem" }
            );

            routes.MapRoute(
                name: "SSPxUpdateItem",
                template: "Protocols/SSPxItem/{id}",
                defaults: new { controller = "Home", action = "SSPxItem" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxInsertItem",
                template: "Protocols/SSPxItem/{id}/insert",
                defaults: new { controller = "Home", action = "SSPxProtocolInsertItem" }
            );

            routes.MapRoute(
                name: "SSPxSwitchItem",
                template: "Protocols/SSPxItem/{id}/switch",
                defaults: new { controller = "Home", action = "SSPxProtocolSwitchItem" }
            );

            routes.MapRoute(
                name: "SSPxDeleteItem",
                template: "Protocols/SSPxItem/{id}/delete",
                defaults: new { controller = "Home", action = "SSPxProtocolDeleteItem" }
            );

            routes.MapRoute(
                name: "SSPxDeleteItemComment",
                template: "Protocols/SSPxItem/{itemKey}/{id}/deleteComment",
                defaults: new { controller = "Home", action = "SSPxProtocolDeleteItemComment" }
            );

            routes.MapRoute(
                name: "SSPxItemSaveComment",
                template: "Protocols/SSPxItem/{id}/saveComment",
                defaults: new { controller = "Home", action = "SSPxProtocolSaveComment" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxItemGetComment",
                template: "Protocols/SSPxItem/{pid}/{id}/getComment",
                defaults: new { controller = "Home", action = "SSPxProtocolGetComment" }
            );

            routes.MapRoute(
                name: "SSPxCopyItemNode",
                template: "Protocols/SSPxItem/{id}/copyToAfter/{pid}",
                defaults: new { controller = "Home", action = "SSPxCopyItemNode" }
            );

            routes.MapRoute(
                name: "SSPxRemoveNoteReference",
                template: "Protocols/SSPxItem/{id}/removeReference",
                defaults: new { controller = "Home", action = "SSPxRemoveNoteReference" }
            );

            routes.MapRoute(
                name: "SSPxRemoveNoteComment",
                template: "Protocols/SSPxItem/{id}/removeComment",
                defaults: new { controller = "Home", action = "SSPxRemoveNoteComment" }
            );

            routes.MapRoute(
                name: "SSPxSaveNoteReference",
                template: "Protocols/SSPxItem/{id}/noteReference",
                defaults: new { controller = "Home", action = "SSPxSaveNoteReference" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxSaveNoteComment",
                template: "Protocols/SSPxItem/{id}/noteComment",
                defaults: new { controller = "Home", action = "SSPxSaveNoteComment" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxSaveVersion",
                template: "Protocols/SSPxItem/{id}/saveVersion",
                defaults: new { controller = "Home", action = "SSPxSaveVersion" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxSaveCover",
                template: "Protocols/SSPxItem/{id}/saveCover",
                defaults: new { controller = "Home", action = "SSPxSaveCover" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxSaveAuthors",
                template: "Protocols/SSPxItem/{id}/saveAuthors",
                defaults: new { controller = "Home", action = "SSPxSaveAuthors" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxSaveItemHiddenFlag",
                template: "Protocols/SSPxItem/{id}/saveHiddenFlag",
                defaults: new { controller = "Home", action = "SSPxSaveItemHiddenFlag" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxOutputHtml",
                template: "Protocols/OutputHtml/{nid}",
                defaults: new { controller = "Home", action = "SSPxProtocolPreview" }
            );

            routes.MapRoute(
                name: "SSPxBuildPdf",
                template: "Protocols/OutputPdf/Build/{pid}",
                defaults: new { controller = "Home", action = "SSPxBuildPdf" }
            );

            routes.MapRoute(
                name: "SSPxBuildMSWord",
                template: "Protocols/OutputWord/Build/{pid}",
                defaults: new { controller = "Home", action = "SSPxBuildMSWord" }
            );

            routes.MapRoute(
                name: "SSPxOutputPdf",
                template: "Protocols/OutputPdf/{fileName}",
                defaults: new { controller = "Home", action = "SSPxOutputPdf" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxOutputWord",
                template: "Protocols/OutputWord/{fileName}",
                defaults: new { controller = "Home", action = "SSPxOutputWord" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxDownloadPdf",
                template: "Protocols/DownloadPdf/{fileName}",
                defaults: new { controller = "Home", action = "SSPxDownloadPdf" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
            );

            routes.MapRoute(
                name: "SSPxNoteTitles",
                template: "Protocols/SSPxNote/{pid}/notetitles",
                defaults: new { controller = "Notes", action = "SSPxItemNoteTitles" }
            );

            routes.MapRoute(
                name: "SSPxNoteAdd",
                template: "Protocols/SSPxNote/{pid}/add/{nid}",
                defaults: new { controller = "Notes", action = "SSPxItemNoteAdd" }
            );

            routes.MapRoute(
                name: "SSPxNoteMove",
                template: "Protocols/SSPxNote/{pid}/move",
                defaults: new { controller = "Notes", action = "SSPxItemNoteMove" }
            );

            routes.MapRoute(
                name: "SSPxNoteDelete",
                template: "Protocols/SSPxNote/{pid}/delete/{nid}",
                defaults: new { controller = "Notes", action = "SSPxItemNoteDelete" }
            );

            routes.MapRoute(
                name: "SSPxNoteCopy",
                template: "Protocols/SSPxNote/{pid}/copy/{nid}",
                defaults: new { controller = "Notes", action = "SSPxItemNoteCopy" }
            );

            routes.MapRoute(
               name: "ImportCommentVersions",
               template: "Protocols/SSPxImportComment/ImportComments/{pid?}/{cid?}",
               defaults: new { controller = "Home", action = "ImportCommentVersions" },
               constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
           );

            routes.MapRoute(
              name: "ImportCommentToCurrentVersion",
              template: "Protocols/SSPxImportComment/ImportCommentsVersion/{pid?}/{rid?}/{cid}",
              defaults: new { controller = "Home", action = "ImportCommentToCurrentVersion" },
              constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }
          );
        }
    }
}
