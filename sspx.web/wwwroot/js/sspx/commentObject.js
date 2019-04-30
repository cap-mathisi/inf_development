var commentObject = {
    _comments: [],
    _protocolVersionkey: '',
    _currentNode: '',
    init: function (comments, protocolVersionkey) {
        _comments = comments;
        _protocolVersionkey = protocolVersionkey;
    },
    setCurrentNode: function (nodeCkey) {
        _currentNode = nodeCkey;
    },
    getCommentById: function (id) {
        var data = $.grep(_comments, function (c) { return c.commentId == id; });
        return data;
    },
    refreshComment: function () {
        var url = "../SSPxItem/" + _protocolVersionkey + "/" + _currentNode + "/getComment";
        var that = this;
        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            dataType: "json",
            success: function (result) {
                _comments = result;                
                that.populateComment();
                //Commented the below lines to refresh the Title, cover and Author comments count in badge icon
                //if (_currentNode.indexOf("_I") > 0) {
                    that.updateCommentBadge();
                //}
            },
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }
        });
    },
    populateComment: function (comments) {
        var initComments = comments ? comments : _comments;        
        var comHtml = "";
        var title = "";
        $('#commentMenu .badge').html(initComments.length);
        for (var i = 0; i < initComments.length; i++) {
            var com = initComments[i];
            if (com.title.toUpperCase() != title) {
                title = com.title.toUpperCase();
                comHtml += "<div class='title' id='" + com.key + "'>" + title + "</div>";                
            }
            comHtml += "<div style='margin-bottom:10px;margin-top:5px'>";
            comHtml += "<div class='ComName' datakey='" + com.commentId + "'>" + com.firstName.substring(0, 1) + com.lastName.substring(0, 1) + "</div>";
            comHtml += "<div class='ComTitle'>" + com.firstName + " " + com.lastName.substring(0, 1) + " | " +
                com.dateString + "<br />" + com.comment;
            comHtml += "</div><div class='ReferControl'><span data-toggle='modal' data-target='#EditMetaData' data-type='C' data-id='" + com.commentId +
                "'><i class='fas fa-pencil-alt' style='color:#808080'></i></span><br /><span data-toggle='modal' data-target='#DeleteMetaData' data-type='C' data-id='" +
                com.commentId + "'><i class='fas fa-times-circle' style='color:red'></i></span></div ><div style='clear:both'></div></div>";
        }
        $('#Comment #commentSection').html(comHtml);
    },
    saveComment: function (cid, metadata) {        
        metadata = metadata.replace(/"/g, "'");
        var node = '0';
        if (_currentNode && _currentNode !== '') {
            node = _currentNode;
        }
        var url = "../SSPxItem/" + node + "/saveComment";
        var data = { commentId: cid, comment: metadata, protocolVersionkey: _protocolVersionkey };
        var that = this;
        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            data: data,
            dataType: "json",
            success: function (result) {
                $('#EditMetaData').modal('hide');
                that.refreshComment();
            },
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }
        });
    },
    removeComment: function (cid) {
        var url = "../SSPxItem/" + _currentNode + '/' + cid + '/deleteComment';
        var that = this;
        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            dataType: "json",
            success: function (result) {
                $('#DeleteMetaData').modal('hide');
                that.refreshComment();
            },
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }
        });
    },
    updateCommentBadge: function () {
        var treeNodes = $("#protocolNodes").data("kendoTreeView");
        var data = treeNodes.dataSource.get(_currentNode);
        if (data != undefined) {
            var commentText = data.commentCount > 0 ? data.commentCount : '';
            //alert("commentText : " + commentText);
            var badgeText = '<span class="badge">' + commentText + '</span>';
            data.commentCount = _comments.length;
            var node = treeNodes.findByUid(data.uid);
            var nodeText = node[0].innerHTML;
            var nextCommentText = data.commentCount > 0 ? data.commentCount : '';
            var newText = nodeText.replace(badgeText, '<span class="badge">' + nextCommentText + '</span>');
            node[0].innerHTML = newText;
        }     
    }
}