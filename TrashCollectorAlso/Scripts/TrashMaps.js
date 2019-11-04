"use strict"
function initMap(tempLat, tempLng) {

    var trash = { lat: tempLat, lng: tempLng };    

    var map = new google.maps.Map(
        document.getElementById('map'), { zoom: 14, center: trash });

    var mapOptions = {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        styles: [{ featureType: 'all', stylers: [{ saturation: -100 }, { brightness: 5 }] }],
        scrollwheel: false
    };

    //this will set a marker to clearly indicate the trash
    var marker = new google.maps.Marker({ position: trash, map: map });
}