

function OpenMap(mapModel, SRC) {

    document.getElementById('div-map').innerHTML = "<div id='map' class='map map-home'  style='width: 100%; min-height: 20rem;'></div>";

    //var map = L.map('map');

    var Lat_TopRight = mapModel.map_TopRight_Lat;
    var Lon_TopRight = mapModel.map_TopRight_Lon;

    var Lat_BottomLeft = mapModel.map_BottomLeft_Lat;
    var Lon_BottomLeft = mapModel.map_BottomLeft_Lon;

    var Lat_Data = mapModel.data_Lat;
    var Lon_Data = mapModel.data_Lon;

    var map = L.map('map', {
        center: L.latLng(Lat_TopRight, Lon_TopRight),
        zoom: 18
    });

    var imageUrl = SRC,
        imageBounds = [[Lat_TopRight, Lon_TopRight], [Lat_BottomLeft, Lon_BottomLeft]];

    L.imageOverlay(imageUrl, imageBounds).addTo(map);


    L.marker([Lat_Data, Lon_Data]).addTo(map)
        .openPopup();

    var center = map.getCenter();
    map.panTo([Lat_Data, Lon_Data]);


    return 1;
}

function GetWidthImage(urlImg) {
    const img = new Image();
    img.src = "../" + urlImg;

    return img.width;
};

function GetHeightImage(urlImg) {
    const img = new Image();
    img.src = "../" + urlImg;

    return img.height;
};