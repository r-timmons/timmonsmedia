$(document).ready(function () {
    //var getSeries = '@Url.Action("GetSeries", "Media")';
    var getSeries = "http://localhost:52701/Media/GetSeries";
    $.getJSON(getSeries, DisplaySeries);
});

function DisplaySeries(response) {
    if (response != null) {
        for (var i = 0; i < response.length; i++) {
            $("#seriesDiv").append("<button class='btn btn-large btn-success' style='margin-right:5px' id='series" + (i + 1) + "' onclick=\"LoadSeries(" + response[i].ID + ")\">" + response[i].Title + "</button>");
        }
    }
};

function LoadSeries(id) {
    $("#episodeDiv").empty();

    //var getEpisodes = '@Url.Action("GetEpisodes", "Home")/' + id;
    var getEpisodes = "http://localhost:52701/Media/GetEpisodes/" + id;

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
            $("#episodeDiv").append("<ul id='season" + seasons[i] + "\' ><button class='btn btn-md btn-info' style='margin-bottom:10px' onclick=\"HideSeason('season" + seasons[i] + "')\"> Season " + seasons[i] + "</button></ul>");
        }
        for (var i = 0; i < response.length; i++) {
            $("#season" + response[i].Season).append("<li style='margin-left:40px' class='season" + response[i].Season + "' ><button id='episode" + (i + 1) +
                                                     "' class='btn btn-sm btn-primary' onclick='LoadEpisode(" + response[i].ID + ")'>" + response[i].Title + "</button></li>");
        }
    }
};

function HideSeason(season) {
    $("." + season).toggle();
};

function LoadEpisode(id) {
    $("#videoDiv").empty();

    var GetFilename = '@Url.Action("GetFilename", "Home")/' + id;

    $.getJSON(GetFilename, DisplayVideo);
};
function DisplayVideo(response) {
    var season = document.getElementById("series" + response[0].seriesId);
    var filename = "Media/Videos/" + season.innerHTML + "/";
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