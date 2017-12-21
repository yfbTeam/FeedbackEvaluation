(function ($) {
    $(function () {
        WebUploader.create({
            pick: '#filePicker',
            formData: {
                uid: 123
            },
            auto: true,
            //dnd: '#dndArea',
            //paste: '#uploader',
            chunked: false,
            chunkSize: 512 * 1024,
            server: '/Scripts/Webuploader/Upload.ashx',
            // 禁掉全局的拖拽功能。这样不会出现图片拖进页面的时候，把图片打开。
            disableGlobalDnd: true,
            fileNumLimit: 300,
            fileSizeLimit: 200 * 1024 * 1024,    // 200 M
            fileSingleSizeLimit: 50 * 1024 * 1024    // 50 M
        })
    });
})(jQuery);