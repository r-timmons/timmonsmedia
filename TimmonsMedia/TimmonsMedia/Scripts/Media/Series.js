$(document).ready(function () {
    var getSeries = '@Url.Action("GetSeries", "Home")';
    $.getJSON(getSeries, DisplaySeries);
});