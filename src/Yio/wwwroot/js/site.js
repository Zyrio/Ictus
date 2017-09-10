var currentRepo;
var defaultRepo;
var endpoint;
var fileHistory = [];
var hfmEnabled = false;
var hfmTimeout;
var nextFile;

$(document).ready(function() {
    var hash = window.location.hash.substring(2);
    const mobileView = window.matchMedia( "(max-width: 480px)" );

    if(!mobileView.matches) {
        ToggleSidebar();
    }

    ClickEvents();
    RepositorySelectEvent();
    SidebarToggleEvent();

    ResizeTagsBox();

    if(hash === "") {
        SetupSite(true);
    } else {
        SetupSite(false);
    };
});

window.addEventListener('resize', function(event){
    ResizeTagsBox();
});

window.addEventListener("hashchange", function(event) {
    GetFile();
});

function ClickEvents() {
    $("#about-button").click(function(e) {
        e.preventDefault();
        TogglePanel("about");
    });

    $("#about-panel-close").click(function(e) {
        e.preventDefault(0);
        TogglePanel("about");
    });

    $("#apps-button").click(function(e) {
        e.preventDefault(0);
        TogglePanel("apps");
    });

    $("#apps-panel-close").click(function(e) {
        e.preventDefault(0);
        TogglePanel("apps");
    });

    $("#backward-button").click(function(e) {
        e.preventDefault();

        ToggleHfm();

        fileHistory.pop();
        var lastItem = fileHistory.pop();
        
        window.location.hash = "/" + lastItem;
    });

    $("#hfm-button").click(function(e) {
        e.preventDefault();

        if(hfmEnabled) {
            ToggleHfm();
        } else {
            TogglePanel("hfm");
        }
    });

    $("#hfm-panel-close").click(function(e) {
        e.preventDefault();
        TogglePanel("hfm");
    });

    $("#random-button").click(function(e) {
        e.preventDefault();
        ToggleHfm();
        GetRandomFile();
    });

    $("#random-mini-button").click(function(e) {
        e.preventDefault();
        ToggleHfm();
        GetRandomFile();
    });

    $("#start-hfm-button").on("click", function() {
        var int = $("#hfm-interval-input").val();
        ToggleHfm(int);
        TogglePanel("hfm");
    });

    $("#upload-button").click(function(e) {
        e.preventDefault();
        TogglePanel("upload");
    });

    $("#upload-panel-close").click(function(e) {
        e.preventDefault();
        TogglePanel("upload");
    });

    $("#view").click(function() {
        ToggleHfm();
        GetRandomFile();
    });

    $("#upload-submit").click(function() {
        Upload_UploadFile();
    });

    $("#upload-tag-add").click(function() {
        Upload_AddTag();
    });
}

function Upload_AddTag() {
    $("#upload-tag-collection-placeholder").css("display", "none");
    if($("#upload-tag-custom").val()) {
        var tag = $("#upload-tag-custom").val().replace(/ /g, "-")
        if(!$("#upload-tag-selected-" + tag).length) {
            $("#upload-tag-collection").append("<span class='upload-tag-selected' id='upload-tag-selected-" + tag + "'>" + tag + " <a class='upload-tag-remove' id='upload-tag-remove-" + tag + "' href='#'>&times;</a></span>")
        }
    } else {
        var tag = $("#upload-tag-selector").val();
        if(!$("#upload-tag-selected-" + tag).length) {
            $("#upload-tag-collection").append("<span class='upload-tag-selected' id='upload-tag-selected-" + tag + "'>" + tag + " <a class='upload-tag-remove' id='upload-tag-remove-" + tag + "' href='#'>&times;</a></span>")
        }
    }

    $(".upload-tag-remove").click(function(e) {
        e.preventDefault();

        var tag = $(this).attr("id").replace("upload-tag-remove-", "");
        $("#upload-tag-selected-" + tag).remove();
        
        if(!$(".upload-tag-selected").length) {
            $("#upload-tag-collection-placeholder").css("display", "");
        }
    })
}

function Upload_UploadFile() {
    $("#upload-panel-close").css("display", "none");

    $.ajax({
        url: '/api/v2/files',
        type: 'POST',

        data: new FormData($('#upload-form')[0]),

        cache: false,
        contentType: false,
        processData: false,

        success: function(data)
        {
            console.log(data);
        }
    });
}

function GetFile() {
    var currentFile = window.location.hash.substring(2);

    $.ajax({
        url: '/api/v2/files/' + currentFile,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data)
        {
            $("#direct-button").attr("href", data.location);
            $("#view").css("background-image", "url('" + data.location + "')");

            StoreHistory(currentFile);
        },
        failure: function (data)
        {
            // handle error
        }
    });
};

function GetRandomFile() {
    $.ajax({
        url: '/api/v2/random/' + currentRepo.toLowerCase(),
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data)
        {
            if(nextFile) {
                window.location.hash = "/" + nextFile;
                PreloadFile(false);
            } else {
                window.location.hash = "/" + data.id;
                PreloadFile(false);
            }
        },
        failure: function (data)
        {
            // handle error
        }
    });
}

function PreloadFile(keepTrying) {
    $.ajax({
        url: '/api/v2/random/' + currentRepo.toLowerCase(),
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data)
        {
            if(data.location.endsWith(".png") ||
                data.location.endsWith(".jpg") ||
                data.location.endsWith(".gif")) {
                    $("#image-preload").attr("src", data.location);
                    nextFile = data.id;
            } else {
                if(keepTrying) {
                    PreloadFile(true);
                } else {
                    nextFile = "";
                }
            }
        },
        failure: function (data)
        {
            // handle error
        }
    });
}

function RepositorySelectEvent() {
    $("#repository-selector").change(function() {
        currentRepo = $("#repository-selector").val();
        PreloadFile();
        GetRandomFile();
    });
}

function ResizeTagsBox() {
    $("#sidebar-tags-inner").css("height", "0");

    var requiredHeight = $("#sidebar-tags").height() + "px";

    $("#sidebar-tags-inner").css("height", requiredHeight);
};

function SetupSite(loadFile) {
    $.ajax({
        url: '/api/v2/tags/default',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data)
        {
            if(window.location.pathname === "/") {
                currentRepo = data.name;
            } else {
                currentRepo = window.location.pathname.slice(1);
                if(window.location.hash) {
                    history.pushState(null, null, '/' + window.location.hash);
                } else {
                    history.pushState(null, null, '/');
                }
            }

            defaultRepo = data.name;

            $.ajax({
                url: '/api/v2/tags',
                type: 'GET',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (dataTags)
                {
                    $.each(dataTags, function(index) {
                        $("#repository-selector").append("<option value='" + dataTags[index].name + "'>" + dataTags[index].name + " (" + dataTags[index].fileCount + ")" + "</option>");
                        $("#upload-tag-selector").append("<option value='" + dataTags[index].name + "'>" + dataTags[index].name + " (" + dataTags[index].fileCount + ")" + "</option>");
                    });
        
                    $("#repository-selector").val(currentRepo.toLowerCase());
        
                    if(loadFile) {
                        GetRandomFile();
                    } else {
                        GetFile();
                        PreloadFile(true);
                    }
                }
            });
        },
        failure: function (data)
        {
            TogglePanel("error");
        }
    });
}

function SidebarToggleEvent() {
    $("#show-sidebar-button").click(function(e) {
        e.preventDefault(0);
        ToggleSidebar();
    })

    $("#hide-sidebar-button").click(function(e) {
        e.preventDefault(0);
        ToggleSidebar();
    });
};

function StoreHistory(file) {
    fileHistory.push(file);
    
    if(fileHistory.length === 1) {
        $(".backward-button-item").addClass("disabled");
    } else {
        $(".backward-button-item").removeClass("disabled");
    }

    // prunce history if there is 100 items, just so
    // we don't clutter up the user's memory
    if(fileHistory.length === 101) {
        fileHistory.shift();
    }
};

function ToggleHfm(interval) {
    if(interval) {
        if(hfmEnabled) {
            $("#hfm-button .fa").css("color", "inherit");
            hfmEnabled = false;
            clearInterval(hfmTimeout);
        } else {
            $("#hfm-button .fa").css("color", "red");
            hfmEnabled = true;
            hfmTimeout = setInterval(function() {
                GetRandomFile();
            }, interval*1000);
        }
    } else {
        $("#hfm-button .fa").css("color", "inherit");
        hfmEnabled = false;
        clearInterval(hfmTimeout);
    }
} 

function TogglePanel(name) {
    if($("#" + name + "-panel").css("display") === "block") {
        $("#panel-overlay").css("display", "");
        $("#" + name + "-panel").css("display", "");
    } else {
        $("#panel-overlay").css("display", "block");
        $("#" + name + "-panel").css("display", "block");
    }
}

function ToggleSidebar() {
    ResizeTagsBox();

    if($("#sidebar").hasClass("visible")) {
        $("#sidebar-mini").removeClass("hidden");
        $("#sidebar").removeClass("visible");
        $("#view").addClass("full");
    } else {
        $("#sidebar-mini").addClass("hidden");
        $("#sidebar").addClass("visible");
        $("#view").removeClass("full");
    }
};