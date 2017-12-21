var Edit_PathObject = {}, Edit_IdObject = {};
function Get_Sys_Document(type, relationid, upobj) { //获取文件信息
    upobj = arguments[2] || '#uploader';
    var ul_file = $(upobj+' .queueList .filelist');
    ul_file.html('');       
    $.ajax({
        url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
        type: "post",
        dataType: "json",
        data: { Func: "Get_Sys_Document", Type: type, RelationId: relationid, IsPage: false },
        success: function (json) {
            if (json.result.errNum == 0) {               
                $(json.result.retData).each(function (i, n) {
                    ul_file.append('<li id="file_' + n.Id + '" pt="' + n.Url + '">' +
                                   '<p class="title1">' + n.Name + '</p>' +
                                 '<div class="file-panel">' +
                                        '<span class="cancel iconfont" onclick="File_Remove(this,\'' + upobj + '\');">&#xe672;</span>' +
                                        '<span class="preview" onclick="File_Viewer(\''+n.Url+'\');">预览</span>' +
                                 '</div>' +
                                        '</li>');
                });
            }
            Edit_PathObject[upobj] = [], Edit_IdObject[upobj] = [];
        }
    });
}
function File_Viewer(filepath) { //预览
    var exten_str = filepath.substr(filepath.lastIndexOf(".")).toLowerCase();//后缀
    if (exten_str=='pdf') {
        window.open("Pdf_View.html?src=" + filepath);
    } else {
        window.open(filepath);
    }
}
function File_Remove(obj, upobj) { //页面删除文件
    var $par_li = $(obj).parents('li');
    Edit_PathObject[upobj].push($par_li.attr('pt'));
    Edit_IdObject[upobj].push($par_li.attr('id').replace('file_', ''));
    $par_li.remove();
}
function Get_AddFile(type,upobj) { //获取新增的文件信息
    type = arguments[0] || 0;
    upobj = arguments[1] || '#uploader';
    var add_path = [];
    $(upobj+" .filelist li.add_file").each(function (i, n) {
        var sub_p = new Object();
        var cpath = $(this).attr('pt');
        sub_p.Type = type;
        sub_p.Name = get_FileName(cpath);
        sub_p.Url = cpath;
        sub_p.CreateUID = GetLoginUser().UniqueNo;
        add_path.push(sub_p);
    });
    return add_path;
}
function Get_EditFileId(upobj) { //获取删除文件的Id
    upobj = arguments[0] || '#uploader';
    if (!Edit_IdObject.hasOwnProperty(upobj)) { //如果Edit_IdObject没有属性upobj
        Edit_IdObject[upobj] = [];
    }
    if(Edit_IdObject[upobj].length > 0)
    {
        return Edit_IdObject[upobj].join(',');
    }else{
        return "";
    }    
}
function Del_Document(upobj) { //删除文件夹中的文件
    upobj = arguments[0] || '#uploader';
    if (!Edit_PathObject.hasOwnProperty(upobj)) {
        Edit_PathObject[upobj] = [];
    }
    if (Edit_PathObject[upobj].length > 0) {
        $.ajax({
            url: "/CommonHandler/UploadHtml5Handler.ashx",
            type: "post",
            dataType: "json",
            data: { Func: "Del_Document", FilePath: JSON.stringify(Edit_PathObject[upobj]) },
            success: function (json) { Edit_PathObject[upobj] = []; }
        });
    }
}
function BindFile_Plugin(wrap, pickid, dndid) { //绑定上传文件插件
    wrap = arguments[0] || '#uploader';
    pickid = arguments[1] || '#filePicker';
    dndid = arguments[2] || '#dndArea';
    var $wrap =$(wrap),
            // 图片容器
            $queue = $wrap.find('.queueList .filelist'),
            // 状态栏，包括进度和控制按钮
            $statusBar = $wrap.find('.statusBar'),
            // 文件总体选择信息。
            $info = $statusBar.find('.info'),
            $progress = $statusBar.find('.progress').hide(),
            // 添加的文件数量
            fileCount = 0,
            // 添加的文件总大小
            fileSize = 0,
            // 优化retina, 在retina下这个值是2
            ratio = window.devicePixelRatio || 1,
            // 可能有pedding, ready, uploading, confirm, done.
            state = 'pedding',
            // 所有文件的进度信息，key为file id
            percentages = {},
            // 判断浏览器是否支持图片的base64
            isSupportBase64 = (function () {
                var data = new Image();
                var support = true;
                data.onload = data.onerror = function () {
                    if (this.width != 1 || this.height != 1) {
                        support = false;
                    }
                }
                data.src = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
                return support;
            })(),
            supportTransition = (function () {
                var s = document.createElement('p').style,
                    r = 'transition' in s ||
                            'WebkitTransition' in s ||
                            'MozTransition' in s ||
                            'msTransition' in s ||
                            'OTransition' in s;
                s = null;
                return r;
            })(),
            queryStr = (function () {
                var url = location.search; //获取url中"?"符后的字串
                var theRequest = new Object();
                if (url.indexOf("?") != -1) {
                    var str = url.substr(1);
                    strs = str.split("&");
                    for (var i = 0; i < strs.length; i++) {
                        theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
                    }
                }
                return theRequest;
            })(),
            // WebUploader实例
            uploader;
        // 实例化
        uploader = WebUploader.create({
            pick: {
                id: pickid,
                label: '点击选择文件'
            },
            formData: { Func: $("#hid_UploadFunc").val() },
            dnd: dndid,
            paste: wrap,
            chunkSize: 512 * 1024,
            server: '/CommonHandler/UploadHtml5Handler.ashx',
            accept: {
                title: 'Images',
                extensions: 'gif,jpg,jpeg,bmp,png,pdf',
                mimeTypes: 'image/gif,image/jpg,image/jpeg,image/bmp,image/png,application/pdf'
            },
            auto: true,
            disableGlobalDnd: true,// 禁掉全局的拖拽功能。这样不会出现图片拖进页面的时候，把图片打开。
            fileNumLimit: 300,
            fileSizeLimit: 200 * 1024 * 1024,    // 200 M
            fileSingleSizeLimit: 50 * 1024 * 1024    // 50 M
        });
        // 拖拽时不接受 js, txt 文件。
        uploader.on('dndAccept', function (items) {
            var denied = false,
                len = items.length,
                i = 0,
                // 修改js类型
                unAllowed = 'text/plain;application/javascript ';

            for (; i < len; i++) {
                // 如果在列表里面
                if (~unAllowed.indexOf(items[i].type)) {
                    denied = true;
                    break;
                }
            }
            return !denied;
        });
        uploader.on('ready', function () {
            window.uploader = uploader;
        });
        // 当有文件添加进来时执行，负责view的创建
        function addFile(file) {
            var $li = $('<li id="' + file.id + '" class="add_file" pt="' + file.return_src + '">' +
                    '<p class="title1">' + file.name + '</p>' +
                    //'<p class="imgWrap"></p>' +
                    //'<p class="progress"><span></span></p>' +
                    '</li>'),
                $btns = $('<div class="file-panel">' +
                    '<span class="cancel iconfont">&#xe672;</span>' +
                    '<span class="preview">预览</span>' +
                    '</div>').appendTo($li),
                $prgress = $li.find('p.progress span'),
                $wrap = $li.find('p.imgWrap'),
                $info = $('<p class="error"></p>');
            file.on('statuschange', function (cur, prev) {
                if (prev === 'progress') {
                    $prgress.hide().width(0);
                } else if (prev === 'queued') {
                    $li.off('mouseenter mouseleave');
                    $btns.remove();
                }
                // 成功
                if (cur === 'progress') {
                    $info.remove();
                    $prgress.css('display', 'block');
                } else if (cur === 'complete') {
                    $li.append('<span class="success"></span>');
                }

                $li.removeClass('state-' + prev).addClass('state-' + cur);
            });
            $btns.on('click', 'span', function () {
                var index = $(this).index(), deg;
                switch (index) {
                    case 0:
                        uploader.removeFile(file);
                        return;
                    case 1:
                        if (!file.type.match(/^image/)) {
                            window.open("Pdf_View.html?src=" + file.return_src);
                        } else {
                            window.open(file.return_src);
                        }
                        break;
                }
            });

            $li.appendTo($queue);
        }
        // 负责view的销毁
        function removeFile(file) {
            var $li = $('#' + file.id);
            //delete percentages[file.id];
            updateTotalProgress();
            $li.off().find('.file-panel').off().end().remove();
            $.ajax({
                url: "/CommonHandler/UploadHtml5Handler.ashx",
                type: "post",
                dataType: "json",
                data: { Func: "Del_Document", FilePath: JSON.stringify([file.return_src]) },
                success: function (json) { }
            });
        }
        function updateTotalProgress() {
            var loaded = 0,
                total = 0,
                spans = $progress.children(),
                percent;

            $.each(percentages, function (k, v) {
                total += v[0];
                loaded += v[0] * v[1];
            });
            percent = total ? loaded / total : 0;
            spans.eq(0).text(Math.round(percent * 100) + '%');
            spans.eq(1).css('width', Math.round(percent * 100) + '%');
            updateStatus();
        }
        function updateStatus() {
            var text = '', stats;
            if (state === 'ready') {
                text = '选中' + fileCount + '个文件，共' +
                        WebUploader.formatSize(fileSize) + '。';
            } else if (state === 'confirm') {
                stats = uploader.getStats();
                if (stats.uploadFailNum) {
                    text = '已成功上传' + stats.successNum + '个文件至资源包，' +
                        stats.uploadFailNum + '个文件上传失败，<a class="retry" href="#">重新上传</a>失败文件或<a class="ignore" href="#">忽略</a>'
                }
            } else {
                stats = uploader.getStats();
                text = '共' + fileCount + '张（' +
                        WebUploader.formatSize(fileSize) +
                        '），已上传' + stats.successNum + '张';

                if (stats.uploadFailNum) {
                    text += '，失败' + stats.uploadFailNum + '张';
                }
            }

            $info.html(text);
        }
        function setState(val) {
            var file, stats;
            if (val === state) {
                return;
            }
            state = val;
            switch (state) {
                case 'pedding':
                    //$queue.hide();
                    $statusBar.addClass('element-invisible');
                    uploader.refresh();
                    break;
                case 'ready':
                    //$('#filePicker2').removeClass('element-invisible');
                    $queue.show();
                    $statusBar.removeClass('element-invisible');
                    uploader.refresh();
                    break;
                case 'confirm':
                    $progress.hide();
                    stats = uploader.getStats();
                    if (stats.successNum && !stats.uploadFailNum) {
                        setState('finish');
                        return;
                    }
                    break;
                case 'finish':
                    stats = uploader.getStats();
                    if (stats.successNum) {
                        //parent.GetComAlbumData(1, 10);                        
                    } else {
                        // 没有成功的图片，重设
                        state = 'done';
                        location.reload();
                    }
                    break;
            }
            updateStatus();
        }
        uploader.onUploadProgress = function (file, percentage) {
            var $li = $('#' + file.id),
                $percent = $li.find('.progress span');

            $percent.css('width', percentage * 100 + '%');
            //percentages[file.id][1] = percentage;
            updateTotalProgress();
        };
        uploader.onFileQueued = function (file) {
            fileCount++;
            fileSize += file.size;
            if (fileCount === 1) {
                $statusBar.show();
            }
            //addFile(file);
            setState('ready');
            updateTotalProgress();
        };
        uploader.onFileDequeued = function (file) {
            fileCount--;
            fileSize -= file.size;
            if (!fileCount) {
                setState('pedding');
            }
            removeFile(file);
            updateTotalProgress();
        };
        uploader.on('all', function (type) {
            var stats;
            switch (type) {
                case 'uploadFinished':
                    setState('confirm');
                    break;
                case 'startUpload':
                    setState('uploading');
                    break;
                case 'stopUpload':
                    setState('paused');
                    break;
            }
        });
        uploader.onError = function (code) {
            switch (code) {
                case 'exceed_size':
                case 'Q_EXCEED_SIZE_LIMIT':
                    layer.msg('文件大小超出');
                    break;
                case 'interrupt':
                    layer.msg('上传暂停');
                    break;
                case 'F_DUPLICATE':
                    layer.msg('已上传该文件');
                    break;
                default:
                    layer.msg('提示: ' + code);
                    break;
            }
        };
        // 文件上传成功，给item添加成功class, 用样式标记上传成功。
        uploader.on('uploadSuccess', function (file, response) {
            var json = $.parseJSON(response._raw);
            $(json.result.retData).each(function (i, n) {
                file["return_src"] = n;
                addFile(file);
            });
            uploader.reset();
        });
        function get_FileName(path) {
            var filename = path.substr(path.lastIndexOf('/') + 1);
            var exten_str = filename.substr(filename.lastIndexOf("."));//后缀
            var charindex = filename.lastIndexOf('_');
            var showname = charindex > -1 ? filename.substring(0, charindex) + exten_str : filename;
            return showname;
        }
        $info.on('click', '.retry', function () {
            uploader.retry();
        });
        updateTotalProgress();
}