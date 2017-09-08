$(document).ready(function(){
    var defaultRepo = "";
    var currentFile = "";

    var HFMInt = docCookies.getItem("HFMInt");
    var HFMIntMS = HFMInt + "000";
    if (HFMInt != null ) {
        $("#btn_HFM").css("color", "red");
        var HFMTimeout = setTimeout(function() {
            window.location.reload(false);
        }, HFMIntMS);
    }

    $("#overlay_Click").click(function(e) {
        e.preventDefault();
        getFile();
    });

    $("#btn_RepoSwitch").click(function(e) {
        e.preventDefault();
        showPanel(true, "RepoSwitch");
    });

    $("#btn_StartHFM").on("click", function() {
        var int = $("#input_HFMInt").val();
        docCookies.setItem("HFMInt", int);
        window.location.reload(false);
    });

    $("#actions > a").click(function(e) {
        e.preventDefault();
        var clickedItem = $(this).attr("id").slice(4);

        switch(clickedItem) {
            case "RandomImage":
                getFile();
                break;
            case "HFM":
                if (HFMInt == null ) {
                    showPanel(true, "HFM");
                } else {
                    docCookies.removeItem("HFMInt");
                    clearTimeout(HFMTimeout);
                    $("#btn_HFM").css("color", "");
                }
                break;
            case "Direct":
                window.location = window.location.protocol + currentFile;
                break;
            case "About":
                showPanel(true, "About");
                break;
            default:
                console.info("Function Not Implemented!");
                break;
        }
    });

    $(".panel-close > a").on("click", function(e) {
        e.preventDefault();
        showPanel(false, "all");
    });

    $(window).on('hashchange',function(){ 
        getFile();
    });

    function getFile() {
        var repo = "";

        var hash = window.location.hash;
        hash = hash.slice(2);

        if(hash === "") {
            repo = defaultRepo;
        } else {
            repo = hash;
        }

        $("#label_RepoSwitch").text(repo);
        $("#label_RepoSwitch_2").text(repo);

        $.ajax({
            url: '/api/v1/random/' + repo.toLowerCase(),
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data)
            {
                document.body.style.backgroundImage = "url('" + data.url + "')";
                currentFile = data.url;
            },
            failure: function (data)
            {
                // handle error
            }
        });
    }

    function getSite() {
        $.ajax({
            url: '/api/v1/site',
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data)
            {
                setupSite(data);
            }
        })
    }

    function setupSite(data) {
        defaultRepo = data.default;

        if(data.showNav === "False") {
            $("#overlay_Panel").css("display", "none");
        }

        var repos = data.repos.split("|");

        $.each(repos, function(index) {
            $("#panel_RepoSwitch ul").append(
                $("<li>", {}).append(
                    $("<a>", { href: "#/" + repos[index].toLowerCase() }).text(
                        repos[index]
                    )
                )
            );
        });

        getFile();

        $("#loadingOverlay").css("display", "none");
    }

    function showPanel(show, name) {
        if(show == true) {
            $("#overlay_Panel").css("display", "block");
            $("#panel_" + name).css("display", "block");
        } else {
            if(name === "all") {
                $("#overlay_Panel").css("display", "");
                $(".panel").css("display", "");
            } else {
                $("#overlay_Panel").css("display", "");
                $("#panel_" + name).css("display", "");
            }
        }
    }

    getSite();
});