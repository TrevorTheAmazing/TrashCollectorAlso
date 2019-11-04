"use strict"
function initMap(tempLat, tempLng) {
    // The location of Uluru
    //var junk = { lat: 43.034, lng: -87.911 };
    var junk = { lat: tempLat, lng: tempLng };
    
    //var trash = { lat: -25.944, lng: 131.936 };

    // The map, centered at Uluru
    var map = new google.maps.Map(
        document.getElementById('map'), { zoom: 14, center: junk });

    var mapOptions = {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        styles: [{ featureType: 'all', stylers: [{ saturation: -100 }, { brightness: 5 }] }],
        scrollwheel: false
    };

    // The marker, positioned at Uluru
    var marker = new google.maps.Marker({ position: junk, map: map });
    var trash = new google.maps.Marker({ position: trash, map: map });
}