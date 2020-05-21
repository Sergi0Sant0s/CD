$(document).ready(function () {
    GetFolderByPath("\\");
    var atualPath;

    $('#folder-new').click(function () {
        NewFolder('teste1', '\\teste\\teste');
    });

    $('#folder-rename').click(function () {
        alert('qwerty 2');
    });

    $('#folder-delete').click(function () {
        alert('qwerty 3');
    });

    $('#file-rename').click(function () {
        alert('qwerty 4');
    });

    $('#file-delete').click(function () {
        alert('qwerty 5');
    });

    $('#file-download').click(function () {
        alert('qwerty 6');
    });

    $('#file-upload').click(function () {
        NewFile('novo ficheiro.jpg', 'este é o path');
    });

    function NewFolder(name, path) {
        var li = $('<li></li>');
        var button = $('<button></button>');
        button.attr('rel', path);
        var img = $('<img>').attr('src', 'img/ftp/folder.svg');
        var span = $('<span></span>').text(name);

        //Appends
        li.append(button);
        button.append(img);
        button.append(span);

        $('#ftp-folders ul').append(li);
        li.dblclick(function () {
            GetFolderByPath(path);
        });
        button.click(function () {
            $('#ftp-folders button').removeClass('selected');
            $(this).addClass('selected');
        });
    }

    function NewFile(name, path) {
        var li = $('<li></li>');
        var button = $('<button></button>');
        var img = $('<img>').attr('src', 'img/ftp/file.png');
        var span = $('<span></span>').text(name);

        //Appends
        li.append(button);
        button.append(img);
        button.append(span);

        $('#ftp-files ul').append(li);
        button.click(function () {
            $('#ftp-files button').removeClass('selected');
            $(this).addClass('selected');
        });
    }

    function GetFolderByPath(path) {
        var link = "http://" + uri + "/getbypath?path=" + path;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            success: function (result) {
                //Reset
                $('#ftp-folders ul').empty();
                $('#ftp-files ul').empty();

                var countFolders = result.folders.length;
                var countFiles = result.files.length;

                //Folders
                for (let i = 0; i < countFolders; i++) {
                    NewFolder(result.folders[i].name, result.folders[i].path);
                }
                $('#ftp-folders button:eq(1)').first().addClass('selected');

                //Files
                for (let i = 0; i < countFiles; i++) {
                    NewFile(result.files[i].name, result.files[i].path);
                }
                $('#ftp-files button').first().addClass('selected');
            },
            error: function (xhr, status, error) {
            }
        });
    }



});
