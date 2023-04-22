/**
 * @license
 * Copyright 2019 Google LLC. All Rights Reserved.
 * SPDX-License-Identifier: Apache-2.0
 */
// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">
function initMap() {
    const map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -33.866, lng: 151.196 },
        zoom: 15,
    });
    const request = {
        placeId: "ChIJN1t_tDeuEmsRUsoyG83frY4",
        fields: ["name", "formatted_address", "place_id", "geometry"],
    };
    const infowindow = new google.maps.InfoWindow();
    const service = new google.maps.places.PlacesService(map);

    service.getDetails(request, (place, status) => {
        if (
            status === google.maps.places.PlacesServiceStatus.OK &&
            place &&
            place.geometry &&
            place.geometry.location
        ) {
            const marker = new google.maps.Marker({
                map,
                position: place.geometry.location,
            });

            google.maps.event.addListener(marker, "click", () => {
                const content = document.createElement("div");
                const nameElement = document.createElement("h2");

                nameElement.textContent = place.name;
                content.appendChild(nameElement);

                const placeIdElement = document.createElement("p");

                placeIdElement.textContent = place.place_id;
                content.appendChild(placeIdElement);

                const placeAddressElement = document.createElement("p");

                placeAddressElement.textContent = place.formatted_address;
                content.appendChild(placeAddressElement);
                infowindow.setContent(content);
                infowindow.open(map, marker);
            });
        }
    });
}

window.initMap = initMap;




//#Region Pre-loaded Javascript for Google Maps

//var google;

////added coordinates for Centriq
//function init() {
//    // Basic options for a simple Google Map
//    // For more options see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions
//    // var myLatlng = new google.maps.LatLng(40.71751, -73.990922);
//    var myLatlng = new google.maps.LatLng(38.96339501389392, -94.60726785814718);
//    // 39.399872
//    // -8.224454

//    var mapOptions = {
//        // How zoomed in you want the map to start at (always required)
//        zoom: 7,

//        // The latitude and longitude to center the map (always required)
//        center: myLatlng,

//        // How you would like to style the map. 
//        scrollwheel: false,
//        styles: [
//            {
//                "featureType": "administrative.country",
//                "elementType": "geometry",
//                "stylers": [
//                    {
//                        "visibility": "simplified"
//                    },
//                    {
//                        "hue": "#ff0000"
//                    }
//                ]
//            }
//        ]
//    };



//    // Get the HTML DOM element that will contain your map 
//    // We are using a div with id="map" seen below in the <body>
//    var mapElement = document.getElementById('map');

//    // Create the Google Map using out element and options defined above
//    var map = new google.maps.Map(mapElement, mapOptions);

//    var addresses = ['Centriq Career Training'];

//    for (var x = 0; x < addresses.length; x++) {
//        $.getJSON('http://maps.googleapis.com/maps/api/geocode/json?address='+addresses[x]+'&sensor=false', null, function (data) {
//            var p = data.results[0].geometry.location
//            var latlng = new google.maps.LatLng(p.lat, p.lng);
//            new google.maps.Marker({
//                position: latlng,
//                map: map,
//                icon: '~/images/loc.png'
//            });

//        });
//    }

//}
//google.maps.event.addDomListener(window, 'load', init);
//#end region