
//@*Codigo para encontrar punto Inicio y punto Final.*@

//Codigo para mostrar direccion de kilometros y horas de punto a punto
//para calcular kilometraje.
//

var Origen;
var Destino;

function initMap() {




    //////////////////////

    //Servicios de google para mostrar la ruta de camino

    var directionsService = new google.maps.DirectionsService;
    var directionsDisplay = new google.maps.DirectionsRenderer;
    //Mostrar el mapa y estilo
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 9,
        mapTypeId: 'roadmap',
        center: { lat: 13.6893500, lng: -89.1871800 }
    });
    directionsDisplay.setMap(map);



    //Muestra el mapa cuando se pasan los valores
    document.getElementById("btnBuscar").addEventListener("click", function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);



    });

    //Guarda la reservacion
    document.getElementById("btnGuardar").addEventListener("click", function () {
        guardarReservacion();
    });




    //document.getElementById('start').addEventListener('change', onChangeHandler);
    //document.getElementById('end').addEventListener('change', onChangeHandler);

}

function moverseMapa(idDelElemento) {
    location.hash = "#" + idDelElemento;
}

function Comprobar() {
    var valor;
    if (document.getElementById('waypoints').value !== "") {

        valor = true;
        return valor
    }
    else {
        valor = false;
        return valor;
    }
}

function Buscar() {



    initMap();
}




var Kilometers;

//metodo para enviar parametros de origen a la base de datos por medio de google y ajax


function calculateAndDisplayRoute(directionsService, directionsDisplay) {

    if (Comprobar() === true) {

        var checkboxArray = [];
        checkboxArray.push({
            'location': document.getElementById('waypoints').value,
            stopover: true
        });

        directionsService.route({
            origin: document.getElementById('start').value,
            destination: document.getElementById('end').value,
            waypoints: checkboxArray,
            //optimizeWaypoints: true,
            travelMode: 'DRIVING'
        }, function (response, status) {
            if (status === 'OK') {
                directionsDisplay.setDirections(response);
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
    }

    directionsService.route({
        origin: document.getElementById('start').value,
        destination: document.getElementById('end').value,
        //  waypoints: checkboxArray,
        //optimizeWaypoints: true,
        travelMode: 'DRIVING'
    }, function (response, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });







    var service = new google.maps.DistanceMatrixService();

    var origins = document.getElementById('start').value;
    var destination = document.getElementById('end').value;



    //Autocompletado





    //Calculo de distancia
    service.getDistanceMatrix(
      {
          origins: [origins],
          destinations: [destination],
          travelMode: 'DRIVING',
          //     drivingOptions: DrivingOptions,
          unitSystem: google.maps.UnitSystem.METRIC,
          avoidHighways: false,
          avoidTolls: false
      },
      function (response, status) {
          if (status !== 'OK') {
              alert('Error was: ' + status);

          } else {
              var originList = response.originAddresses;
              var destinationList = response.destinationAddresses;
              var outputDiv = document.getElementById('output');
              outputDiv.innerHTML = '';

              //var showGeocodedAddressOnMap = function (asDestination) {
              //    var icon = asDestination ? destinationIcon : originIcon;
              //    return function (results, status) {
              //        if (status === 'OK') {
              //            map.fitBounds(bounds.extend(results[0].geometry.location));
              //            markersArray.push(new google.maps.Marker({
              //                map: map,
              //                position: results[0].geometry.location,
              //                icon: icon
              //            }));
              //        } else {
              //            alert('Geocode was not successful due to: ' + status);
              //        }
              //    };
              //};


              Origen = originList;
              Destino = destinationList;

              var destinationIcon = 'https://chart.googleapis.com/chart?' + 'chst=d_map_pin_letter&chld=D|FF0000|000000';
              var originIcon = 'https://chart.googleapis.com/chart?' + 'chst=d_map_pin_letter&chld=O|FFFF00|000000';

              var geocoder = new google.maps.Geocoder;

              for (var i = 0; i < originList.length; i++) {
                  var results = response.rows[i].elements;
                  geocoder.geocode({ 'address': originList[i] }
                      //showGeocodedAddressOnMap(false)
                      );
                  for (var j = 0; j < results.length; j++) {
                      geocoder.geocode({ 'address': destinationList[j] }
                          //showGeocodedAddressOnMap(true)
                          );
                      outputDiv.innerHTML += originList[i] + ' a ' + destinationList[j] +
                          ': ' + results[j].distance.text + ' en ' +
                          results[j].duration.text + '<br>';
                      //Es un boton oculto donde se deposita los kilometros
                      $("#KMS").val(results[j].distance.text);
                      //  alert(results[j].distance.text);
                  }


              }


          }



      }


      );


    //function callback(response, status) {
    //    // See Parsing the Results for
    //    // the basics of a callback function.
    //    console.log(response, status);
    //}



}

function guardarReservacion() {

    var loading = swal({
        title: 'Cargando!',
        text: 'proceso...',
        timer: 5000,
        onOpen: function () {
            swal.showLoading()
        }
    });




    var ID = $("#txtIdCar").val();
    var Origin = Origen;
    var Destination = Destino;
    var EmployeeID = $("#txtIdEmpleado").val();
    var Date = $("#txtDate").val();
    var Kilometers = $("#KMS").val();
    var reservation = {
        CarID: ID,
        Date: Date,
        Origin: Origin,
        Kilometers: Kilometers,
        Destination: Destination,
        EmployeeID: EmployeeID
    };
    debugger
    $.ajax({
        type: "POST",
        url: '/Map/GuardarReservacion',
        data: reservation,
        beforeSend: function () { loading; },
        success: function (data) {
            console.log(data);


            if (data == true) {
                swal({
                    title: 'Reservación Exitosa',
                    text: "Reservacion confirmada!",
                    type: 'success',
                    confirmButtonColor: '#6a1b9a',
                    confirmButtonText: 'Ok!'
                }).then(function () {
                    window.locationf = "http://localhost:52730/Catalogo";
                });
            }
            else {
                swal(
                    'Oops...',
                    'No se registro!',
                    'error'
                    );
            }
            //$('#row_' + Usuario.UsuarioID).remove();



        }, error: function () {
            swal(
              'Error...',
              'Ocurrio un Error!',
              'error'
              );
        }
    });

}




//

////Codigo para mostrar direccion de kilometros y horas de punto a punto
////para calcular kilometraje.
////
//function initMap() {
//    var directionsService = new google.maps.DirectionsService;
//    var directionsDisplay = new google.maps.DirectionsRenderer;
//    var map = new google.maps.Map(document.getElementById('map'), {
//        zoom: 7,
//        center: { lat: 13.6893500, lng: -89.1871800 }
//    });
//    directionsDisplay.setMap(map);

//    var onChangeHandler = function () {
//        calculateAndDisplayRoute(directionsService, directionsDisplay);
//    };
//    document.getElementById('start').addEventListener('change', onChangeHandler);
//    document.getElementById('end').addEventListener('change', onChangeHandler);
//}

//function calculateAndDisplayRoute(directionsService, directionsDisplay) {
//    directionsService.route({
//        origin: document.getElementById('start').value,
//        destination: document.getElementById('end').value,
//        travelMode: 'DRIVING'
//    }, function (response, status) {
//        if (status === 'OK') {
//            directionsDisplay.setDirections(response);
//        } else {
//            window.alert('Directions request failed due to ' + status);
//        }
//    });

//    var service = new google.maps.DistanceMatrixService();

//    var origins = document.getElementById('start').value;
//    var destination = document.getElementById('end').value;
//    service.getDistanceMatrix(
//      {
//          origins: [origins],
//          destinations: [destination],
//          travelMode: 'DRIVING',
//     //     drivingOptions: DrivingOptions,
//          unitSystem: google.maps.UnitSystem.METRIC,
//          avoidHighways: false,
//          avoidTolls: false,
//      },
//      function(response, status) {
//          if (status !== 'OK') {
//              alert('Error was: ' + status);
//          } else {
//              var originList = response.originAddresses;
//              var destinationList = response.destinationAddresses;
//              var outputDiv = document.getElementById('output');
//              outputDiv.innerHTML = '';

//              var showGeocodedAddressOnMap = function(asDestination) {
//                  var icon = asDestination ? destinationIcon : originIcon;
//                  return function(results, status) {
//                      if (status === 'OK') {
//                          map.fitBounds(bounds.extend(results[0].geometry.location));
//                          markersArray.push(new google.maps.Marker({
//                              map: map,
//                              position: results[0].geometry.location,
//                              icon: icon
//                          }));
//                      } else {
//                          alert('Geocode was not successful due to: ' + status);
//                      }
//                  };
//              };


//              var destinationIcon = 'https://chart.googleapis.com/chart?' + 'chst=d_map_pin_letter&chld=D|FF0000|000000';
//              var originIcon = 'https://chart.googleapis.com/chart?' +  'chst=d_map_pin_letter&chld=O|FFFF00|000000';

//              var geocoder = new google.maps.Geocoder;

//              for (var i = 0; i < originList.length; i++) {
//                  var results = response.rows[i].elements;
//                  geocoder.geocode({'Address': originList[i]},
//                      showGeocodedAddressOnMap(false));
//                  for (var j = 0; j < results.length; j++) {
//                      geocoder.geocode({ 'Address': destinationList[j] },
//                          showGeocodedAddressOnMap(true));
//                      outputDiv.innerHTML += originList[i] + ' a ' + destinationList[j] +
//                          ': ' + results[j].distance.text + ' en ' +
//                          results[j].duration.text + '<br>';
//                  }
//              }
//          }
//      });

//    function callback(response, status) {
//        // See Parsing the Results for
//        // the basics of a callback function.
//        console.log(response,status);
//    }

//}






//@*Codigo para marcadores*@
//@*<script>
//          function initMap() {
//              var uluru = {lat: -25.363, lng: 131.044};
//              var map = new google.maps.Map(document.getElementById('map'), {
//                  zoom: 4,
//                  center: uluru
//              });
//              var marker = new google.maps.Marker({
//                  position: uluru,
//                  map: map
//              });
//          }
//    </script>*@



//@*Codigo para encontrar la ubicacion*@

//@*<script>
//          // Note: This example requires that you consent to location sharing when
//          // prompted by your browser. If you see the error "The Geolocation service
//          // failed.", it means you probably did not give permission for the browser to
//          // locate you.

//          function initMap() {
//              var map = new google.maps.Map(document.getElementById('map'), {
//                  center: {
//                      lat: 13.736830, lng: -89.708776
//                  },
//                  zoom: 17
//              });
//              var infoWindow = new google.maps.InfoWindow({map: map});

//              // Try HTML5 geolocation.
//              if (navigator.geolocation) {
//                  navigator.geolocation.getCurrentPosition(function(position) {
//                      var pos = {
//                          lat: position.coords.latitude,
//                          lng: position.coords.longitude
//                      };


//                      var uluru = pos;

//                      var marker = new google.maps.Marker({
//                          position: uluru,
//                          animation: google.maps.Animation.DROP,
//                          map: map,
//                          title: "Mi Posición."
//                      });




//                      //infoWindow.setPosition(pos);

//                      //infoWindow.setContent('Localizacion Encontrada.');
//                      map.setCenter(pos);
//                  }, function() {
//                      handleLocationError(true, infoWindow, map.getCenter());
//                  });
//              } else {
//                  // Browser doesn't support Geolocation
//                  handleLocationError(false, infoWindow, map.getCenter());
//              }
//          }

//function handleLocationError(browserHasGeolocation, infoWindow, pos) {
//    infoWindow.setPosition(pos);
//    infoWindow.setContent(browserHasGeolocation ?
//                          'Error: Fallo la geolocalizacion.' :
//                          'Error: Your browser doesn\'t support geolocation.');
//}



//</script>*@





//@*Codigo para trazados vehiculares.*@
//@*<script>
//      function initMap() {
//          var chicago = {lat: 41.85, lng: -87.65};
//          var indianapolis = {lat: 39.79, lng: -86.14};

//          var map = new google.maps.Map(document.getElementById('map'), {
//              center: chicago,
//              scrollwheel: false,
//              zoom: 7
//          });

//          var directionsDisplay = new google.maps.DirectionsRenderer({
//              map: map
//          });

//          // Set destination, origin and travel mode.
//          var request = {
//              destination: indianapolis,
//              origin: chicago,
//              travelMode: 'DRIVING'
//          };

//          // Pass the directions request to the directions service.
//          var directionsService = new google.maps.DirectionsService();
//          directionsService.route(request, function(response, status) {
//              if (status == 'OK') {
//                  // Display the route on the map.
//                  directionsDisplay.setDirections(response);
//              }
//          });
//      }

//</script>*@
//@*<script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&callback=initMap"
//async defer></script>*@






//function initMap() {
//  var directionsService = new google.maps.DirectionsService;
//  var directionsDisplay = new google.maps.DirectionsRenderer;
//  var map = new google.maps.Map(document.getElementById('map'), {
//      zoom: 8,
//      center: { lat: 13.6893500, lng: -89.1871800 }
//  });
//  directionsDisplay.setMap(map);

//  var onChangeHandler = function() {
//    calculateAndDisplayRoute(directionsService, directionsDisplay);
//  };
//  document.getElementById('start').addEventListener('change', onChangeHandler);
//  document.getElementById('end').addEventListener('change', onChangeHandler);
//}

//function calculateAndDisplayRoute(directionsService, directionsDisplay) {
//    directionsService.route({
//        origin: document.getElementById('start').value,
//        destination: document.getElementById('end').value,
//        travelMode: 'DRIVING'
//    }, function(response, status) {
//        if (status === 'OK') {
//            directionsDisplay.setDirections(response);
//        } else {
//            window.alert('Directions request failed due to ' + status);
//        }
//    });


//}

//}

