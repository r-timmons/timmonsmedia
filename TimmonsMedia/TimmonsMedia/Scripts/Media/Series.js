

$(document).ready(function () {
    $.getJSON(getSeries, DisplaySeries);
});

function DisplaySeries(response) {
    if (response != null) {
        for (var i = 0; i < response.length; i++) {
            $("#seriesDiv").append("<img src='../../Media/Images/" + response[i].Title + ".png' style='margin-right:3px; margin-bottom:5px; width:200px; height88px:' id='series" + (response[i].ID) + "' onclick=\"LoadSeries(" +
                                    response[i].ID + ")\" alt='" + response[i].Title + "'>");
        }
    }
};

function LoadSeries(id) {
    $("#episodeDiv").empty();

    $("img").removeClass("selected");
    $("#series"+id).addClass("selected");

    var getEpisodes = getEpisodesBySeries + id;

    $.getJSON(getEpisodes, DisplayEpisodes);
};

function DisplayEpisodes(response) {
    if (response != null) {
        var seasons = new Array();
        var seasonAdded = false;

        for (var i = 0; i < response.length; i++) {
            var season = response[i].Season;
            for (var x = 0; x < seasons.length; x++) {
                if (season == seasons[x]) {
                    seasonAdded = true;
                }
            }
            if (!seasonAdded) {
                seasons.push(season);
                seasonAdded = false;
            }
        }
        for (var i = 0; i < seasons.length; i++) {
            $("#episodeDiv").append("<br/>");
            $("#episodeDiv").append("<ul id='season" + seasons[i] + "\' ><button class='btn btn-md btn-info' style='margin-bottom:10px;' onclick=\"HideSeason('season" + seasons[i] + "')\"> Season " + seasons[i] + "</button></ul>");
        }
        for (var i = 0; i < response.length; i++) {
            $("#season" + response[i].Season).append("<li style='margin-left:40px; display: none' class='season" + response[i].Season + "' ><button id='episode" + (i + 1) +
                                                     "' class='btn btn-sm btn-primary' onclick='LoadEpisode(" + response[i].ID + ")'>" + response[i].Title + "</button></li>");
        }
    }
};

function HideSeason(season) {
    $("." + season).toggle();
};

function LoadEpisode(id) {
    $("#videoDiv").empty();

    var GetFilename = GetEpisodeById + id;

    $.getJSON(GetFilename, DisplayVideo);
};
function DisplayVideo(response) {
    var series = document.getElementById("series" + response[0].seriesId).alt;
    var filename = "Media/Videos/" + series + "/";
    if (response[0].Season < 10) {
        filename = filename + "[S0" + response[0].Season;
    }
    else {
        filename = filename + "[S" + response[0].Season;
    }

    if (response[0].EpisodeNum < 10) {
        filename = filename + "E0" + response[0].EpisodeNum + "] ";
    }
    else {
        filename = filename + "E" + response[0].EpisodeNum + "] ";
    }

    filename = filename + response[0].Title + ".mp4";
    $("#videoDiv").append("<video id='vidPlayer' src=\"/" + filename + "\" controls='controls' runat='server' type='video/mp4' width='100%' height='100%'></video>");
};