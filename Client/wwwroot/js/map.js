
var listMap = null;
var map = null;
var markerGroup;
var markerModal = null;

function CreateListMap(mapModel, zoom, SRC) {
    var Lat_TopRight = mapModel.map_TopRight_Lat;
    var Lon_TopRight = mapModel.map_TopRight_Lon;

    var Lat_BottomLeft = mapModel.map_BottomLeft_Lat;
    var Lon_BottomLeft = mapModel.map_BottomLeft_Lon;


    listMap = null;

    listMap = L.map('listMap', {
        center: L.latLng(getCenterCoordinates(Lat_BottomLeft, Lat_TopRight), getCenterCoordinates(Lon_BottomLeft, Lon_TopRight)),
        zoom: zoom
    });


    var imageUrl = SRC;
    var imageBounds = [[Lat_TopRight, Lon_TopRight], [Lat_BottomLeft, Lon_BottomLeft]];

    L.imageOverlay(imageUrl, imageBounds).addTo(listMap);


    setTimeout(function () {
        listMap.invalidateSize();
    }, 100);

    return 1;
}

function OpenModalMap(mapModel, zoom, SRC, _data) {

    var Lat_TopRight = mapModel.map_TopRight_Lat;
    var Lon_TopRight = mapModel.map_TopRight_Lon;

    var Lat_BottomLeft = mapModel.map_BottomLeft_Lat;
    var Lon_BottomLeft = mapModel.map_BottomLeft_Lon;

    document.getElementById('div-modalmap').innerHTML = "<div id='modalmap'></div>";

    map = null;

    map = L.map('modalmap', {
        center: L.latLng(_data.lat, _data.lon),
        zoom: zoom
    });

    var imageUrl = SRC;
    var imageBounds = [[Lat_TopRight, Lon_TopRight], [Lat_BottomLeft, Lon_BottomLeft]];

    L.imageOverlay(imageUrl, imageBounds).addTo(map);



    if (markerModal != null)
        markerModal.clearLayers();

    markerModal = L.layerGroup().addTo(map);

    L.marker([_data.lat, _data.lon], {
        title: _data.data + " " + _data.datetime,
    })
        .addTo(markerModal)
        .bindPopup(_data.info + "<p style='display:none' id='markerDatetime'>" + _data.datetime + "</p>" + "<p style='display:none' id='markerLatitude'>" + _data.lat + "</p>" + "<p style='display:none' id='markerLongitude'>" + _data.lon + "</p>" + "<p style='display:none' id='markerAltitude'>" + _data.altitude + "</p>")
        .on('click', clickZoom);

    setTimeout(function () {
        map.invalidateSize();
    }, 100);

    return 1;
}

function UpdatePoints(data) {
    clearMarkers();

    markerGroup = L.layerGroup().addTo(listMap);

    var i = 1;
    data.forEach(function (_data) {
        L.marker([_data.lat, _data.lon], {
            title: _data.data + " " + _data.datetime,
        })
            .addTo(markerGroup)
            .bindPopup(_data.info + "<p style='display:none' id='markerDatetime'>" + _data.datetime + "</p>" + "<p style='display:none' id='markerLatitude'>" + _data.lat + "</p>" + "<p style='display:none' id='markerLongitude'>" + _data.lon + "</p>" + "<p style='display:none' id='markerAltitude'>" + _data.altitude + "</p>")
            .on('click', clickZoom);

        i += 1;
    });

    return 1;
}

function getStringDate(data) {
    var date = new Date(Date.parse(data));
    return (date.getUTCDate()) + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
}

function getStringTime(data) {
    var date = new Date(Date.parse(data));
    return (date.getHours()) + ":" + date.getMinutes() + ":" + date.getSeconds() + "";
}

function clickZoom(e) {

    if (listMap != null) {
        listMap.setView(e.target.getLatLng(), 20);

        var popup = e.target.getPopup();
        var content = popup.getContent();

        var markerDetail = document.getElementById('markerDetail');
        markerDetail.innerHTML = `<h5 class='my-3'>Details</h5>
                                <div class='col-12 row mb-1'>
                                    <h6 class="col-auto">Data</h6>
                                    <p class="col" style='font-size: 2vh;' id='detailMarker_data'></p>
                                </div>
                                <div class='col-12 row mb-1'>
                                    <h6 class="col-auto">Time</h6>
                                    <p class="col" style='font-size: 2vh;' id='detailMarker_time'></p>
                                </div>
                                <div class='col-12 row mb-1'>
                                    <h6 class="col-auto">Latitude</h6>
                                    <p class="col" style='font-size: 2vh;' id='detailMarker_latitude'></p>
                                </div>
                                <div class='col-12 row mb-1'>
                                    <h6 class="col-auto">Longitude</h6>
                                    <p class="col" style='font-size: 2vh;' id='detailMarker_longitude'></p>
                                </div>
                                <div class='col-12 row mb-1'>
                                    <h6 class="col-auto">Altitude</h6>
                                    <p class="col" style='font-size: 2vh;' id='detailMarker_altitude'></p>
                                </div>`;

        var detailMarker_data = document.getElementById('detailMarker_data');
        detailMarker_data.innerHTML = content;

        var detailMarker_time = document.getElementById('detailMarker_time');
        detailMarker_time.innerHTML = getStringDate(document.getElementById('markerDatetime').innerHTML) + " " + getStringTime(document.getElementById('markerDatetime').innerHTML);

        var detailMarker_latitude = document.getElementById('detailMarker_latitude');
        detailMarker_latitude.innerHTML = document.getElementById('markerLatitude').innerHTML;

        var detailMarker_longitude = document.getElementById('detailMarker_longitude');
        detailMarker_longitude.innerHTML = document.getElementById('markerLongitude').innerHTML;

        var detailMarker_altitude = document.getElementById('detailMarker_altitude');
        detailMarker_altitude.innerHTML = document.getElementById('markerAltitude').innerHTML + "cm";

    }

    if (map != null)
        map.setView(e.target.getLatLng(), 20);


}

function clearMarkers() {
    if (markerGroup != null)
        markerGroup.clearLayers();

    clearDetails();

    return 1
}

function clearDetails() {
    var markerDetail = document.getElementById('markerDetail');
    markerDetail.innerHTML = "";

    return 1
}





function getCenterCoordinates(x1, x2) {
    var y = (x1 + x2) / 2;
    return y;
}


//function GetWidthImage(urlImg) {
//    const img = new Image();
//    img.src = "../" + urlImg;

//    return img.width;
//};

//function GetHeightImage(urlImg) {
//    const img = new Image();
//    img.src = "../" + urlImg;

//    return img.height;
//};