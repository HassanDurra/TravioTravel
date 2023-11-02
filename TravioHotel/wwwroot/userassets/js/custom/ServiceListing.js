   $(document).ready(function (e) {
        let serviceContainer = $('#serviceContainer');
        let searchingLoader   = $('#searchingLoader');
        let from_city        = $('#from_city');
        let to_city          = $('#to_city');
        let fromCountries    = $("#from_country");
        let toCountries      = $("#to_country");
        let departureDate    = $('input[name="departureDate"]');
        let numberofAdults   = $('input[name="adults"]');
        // Sending Data to our getIata function to retrieve IATA CODE OF AIRPORTS
        // Converting Date Format From dd/mm/yyy To YYYYMMDD for api
            const DatePart    = departureDate.val().split('/'); // This Will Remove the / from the values      
            const month   = DatePart[0];
            const date    = DatePart[1];
            const year    = DatePart[2];
            const formattedMonth = String(month).padStart(2 , '0');
            const formattedDate  = String(date).padStart(2 , '0');
            let formattedDeparture = `${year}-${formattedMonth}-${formattedDate}`;
       e.preventDefault();
    
        // End Of Converting Date Format   
        $.ajax({
            url  : getIataCode , 
            type : 'post' ,
            data: {fromCity : from_city.val() , toCity : to_city.val() , from_country : fromCountries.val() , to_country : toCountries.val()},
            success:function(response){
                if(response.message == "FromCountry")
                {   
                    let fromCountry = response.fromCountryDetails.iataCode ;
                    let toCity      = response.toCityDetails.iataCode;
                    // First we Will Authorize our self to Amadeus.com then we can get a generated access token for api product usage
                    //Authorizing
                    var tokenUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";
                    // Amadeus Client Credientals
                    const clientid = "gOIqKp00iu9rGRDRb893Mwa6YgagLRLz"; //Your App Api Key
                    const clientSecret = "86nyQTrv9AF5SkU4"; // Api Secret Key
                    //Creating A Data Variable to send these credentials
                    const requestData = {
                        grant_type: 'client_credentials',
                        client_id: clientid,
                        client_secret: clientSecret
                    };
                    //Converting Data Request to A url-encode String
                    const requestDataString = Object.keys(requestData).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(requestData[key])).join('&');
                    // Now Sending Ajax Request
                    $.ajax({
                        url: tokenUrl,
                        method: 'Post',
                        data: requestDataString,
                        success: function (clientData) {
                            const apiUrl = 'https://test.api.amadeus.com/v2/shopping/flight-offers'; // The Api Service You Want Use
                            const accessToken = clientData.access_token; // This Is mendatory to authorize user to use the api service
                            const SearchParams = {
                                originLocationCode: fromCountry,
                                destinationLocationCode: toCity,
                                departureDate: formattedDeparture,// Date Format YYYY-MM-DD
                                adults: 4
                            }; // these are the params that we will send to our flight search api
                            const requestDataString = Object.keys(SearchParams).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(SearchParams[key])).join('&');
                            // Now Our Date And Uri is ready we will create a ajax Get request to send data and get data from response
                            $.ajax({
                                url: apiUrl + '?' + requestDataString,
                                method: "Get",
                                headers: {
                                    'Authorization': 'Bearer ' + accessToken,
                                },
                                success: function (response) {
                                    if (response.data == "" || response.data == null) {
                                        alert("no flights found")
                                    }
                                    else {
                                        let uniqueAirlinesIata = [];
                                        // Assuming 'response' is the JSON response object
                                        $(response.data).each(function (index, value) {
                                            $(value.itineraries).each(function (i, itinerary) {
                                                $(itinerary.segments).each(function (j, segment) {
                                                    uniqueAirlinesIata.push(segment.carrierCode);
                                                });
                                            });
                                        });

                                        // To remove duplicate carrier codes, you can use a Set
                                        let allAirlinesIata = [...new Set(uniqueAirlinesIata)];

                                        $.ajax({
                                            url: getAirlines,
                                            type: "Post",
                                            data: { AirlinesIata: allAirlinesIata },
                                            success: function (response) {
                                                $(searchingLoader).fadeOut();
                                                let cardContainer = "";
                                                $(response.data).each(function (index, value) {
                                                    cardContainer += `
                                                             <div class="list-block main-block f-list-block">
                                                        <div class="list-content">
                                                            <div class="main-img list-img f-list-img">
                                                                <a href="flight-detail-left-sidebar.html">
                                                                    <div class="f-img">
                                                                            <img src="${value.airlineImage}" class="img-fluid" alt="flight-img" />
                                                                    </div><!-- end f-list-img -->
                                                                </a>
                                                                <ul class="list-unstyled list-inline offer-price-1">
                                                                    <li class="list-inline-item duration"><i class="fa fa-clock-o"></i><span>6 hours - 30 minutes</span></li>
                                                                    <li class="list-inline-item price">$568.00<span class="divider">|</span><span class="pkg">2 Stay</span></li>
                                                                </ul>
                                                                <ul class="list-unstyled flight-timing">
                                                                    <li><span><i class="fa fa-plane"></i></span><span class="date">Aug, 02-2017 </span>(8:40 PM)</li>
                                                                    <li><span><i class="fa fa-plane"></i></span><span class="date">Aug, 03-2017 </span>(8:40 PM)</li>
                                                                </ul>
                                                            </div><!-- end f-list-img -->

                                                            <div class="list-info f-list-info">
                                                                    <h3 class="block-title"><a href="flight-detail-left-sidebar.html">${from_city.val()} to ${toCountry}</a></h3>
                                                                <p class="block-minor"><span>Fr 5379,</span> Oneway Flight</p>
                                                                <p>Lorem ipsum dolor sit amet, ad duo fugit aeque fabulas, in lucilius prodesset pri. Veniam delectus ei vis. Est atqui timeam mnesarchum at, pro an eros perpetua ullamcorper.</p>
                                                                <a href="flight-detail-left-sidebar.html" class="btn btn-orange">View More</a>
                                                            </div><!-- end f-list-info -->
                                                        </div><!-- end list-content -->
                                                    </div><!-- end f-list-block -->`;
                                                }); // Foreach Ends here
                                                $(serviceContainer).html(cardContainer);
                                            }
                                        }); // Ajax Ends here for Services
                                    }

                                }


                            });
                        }

                    })


                }
                /// ============== ///
                if (response.message == "FromCountryAndCity") {
                    let fromCountry = response.fromCountryDetails.iataCode;
                    let toCity = response.toCountry.iataCode;
                    // First we Will Authorize our self to Amadeus.com then we can get a generated access token for api product usage
                    //Authorizing
                    var tokenUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";
                    // Amadeus Client Credientals
                    const clientid = "gOIqKp00iu9rGRDRb893Mwa6YgagLRLz"; //Your App Api Key
                    const clientSecret = "86nyQTrv9AF5SkU4"; // Api Secret Key
                    //Creating A Data Variable to send these credentials
                    const requestData = {
                        grant_type: 'client_credentials',
                        client_id: clientid,
                        client_secret: clientSecret
                    };
                    //Converting Data Request to A url-encode String
                    const requestDataString = Object.keys(requestData).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(requestData[key])).join('&');
                    // Now Sending Ajax Request
                    $.ajax({
                        url: tokenUrl,
                        method: 'Post',
                        data: requestDataString,
                        success: function (clientData) {
                            const apiUrl = 'https://test.api.amadeus.com/v2/shopping/flight-offers'; // The Api Service You Want Use
                            const accessToken = clientData.access_token; // This Is mendatory to authorize user to use the api service
                            const SearchParams = {
                                originLocationCode: fromCountry,
                                destinationLocationCode: toCity,
                                departureDate: formattedDeparture,// Date Format YYYY-MM-DD
                                adults: numberofAdults.val()
                            }; // these are the params that we will send to our flight search api
                            const requestDataString = Object.keys(SearchParams).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(SearchParams[key])).join('&');
                            // Now Our Date And Uri is ready we will create a ajax Get request to send data and get data from response
                            $.ajax({
                                url: apiUrl + '?' + requestDataString,
                                method: "Get",
                                headers: {
                                    'Authorization': 'Bearer ' + accessToken,
                                },
                                success: function (response) {
                                    if (response.data == "" || response.data == null) {
                                        alert("no flights found")
                                    }
                                    else {
                                        let uniqueAirlinesIata = [];
                                        // Assuming 'response' is the JSON response object
                                        $(response.data).each(function (index, value) {
                                            $(value.itineraries).each(function (i, itinerary) {
                                                $(itinerary.segments).each(function (j, segment) {
                                                    uniqueAirlinesIata.push(segment.carrierCode);
                                                });
                                            });
                                        });

                                        // To remove duplicate carrier codes, you can use a Set
                                        let allAirlinesIata = [...new Set(uniqueAirlinesIata)];

                                        $.ajax({
                                            url: getAirlines,
                                            type: "Post",
                                            data: { AirlinesIata: allAirlinesIata },
                                            success: function (response) {
                                                let cardContainer = "";
                                                $(response.data).each(function (index, value) {
                                                    cardContainer += `
                                                             <div class="list-block main-block f-list-block">
                                                        <div class="list-content">
                                                            <div class="main-img list-img f-list-img">
                                                                <a href="flight-detail-left-sidebar.html">
                                                                    <div class="f-img">
                                                                            <img src="${value.airlineImage}" class="img-fluid" alt="flight-img" />
                                                                    </div><!-- end f-list-img -->
                                                                </a>
                                                                <ul class="list-unstyled list-inline offer-price-1">
                                                                    <li class="list-inline-item duration"><i class="fa fa-clock-o"></i><span>6 hours - 30 minutes</span></li>
                                                                    <li class="list-inline-item price">$568.00<span class="divider">|</span><span class="pkg">2 Stay</span></li>
                                                                </ul>
                                                                <ul class="list-unstyled flight-timing">
                                                                    <li><span><i class="fa fa-plane"></i></span><span class="date">Aug, 02-2017 </span>(8:40 PM)</li>
                                                                    <li><span><i class="fa fa-plane"></i></span><span class="date">Aug, 03-2017 </span>(8:40 PM)</li>
                                                                </ul>
                                                            </div><!-- end f-list-img -->

                                                            <div class="list-info f-list-info">
                                                                    <h3 class="block-title"><a href="flight-detail-left-sidebar.html">${from_city.val()} to ${toCountry}</a></h3>
                                                                <p class="block-minor"><span>Fr 5379,</span> Oneway Flight</p>
                                                                <p>Lorem ipsum dolor sit amet, ad duo fugit aeque fabulas, in lucilius prodesset pri. Veniam delectus ei vis. Est atqui timeam mnesarchum at, pro an eros perpetua ullamcorper.</p>
                                                                <a href="flight-detail-left-sidebar.html" class="btn btn-orange">View More</a>
                                                            </div><!-- end f-list-info -->
                                                        </div><!-- end list-content -->
                                                    </div><!-- end f-list-block -->`;
                                                }); // Foreach Ends here
                                                $(serviceContainer).html(cardContainer);
                                            }
                                        }); // Ajax Ends here for Services
                                    
                                    }

                                }


                            });
                        }

                    })


                }
                ///----------------./////////
                else if (response.message == "toCountry") {
                    let FromCity  = response.fromCityDetails.iataCode;
                    let toCountry = response.toCountry.iataCode;
                    // First we Will Authorize our self to Amadeus.com then we can get a generated access token for api product usage
                    //Authorizing
                    var tokenUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";
                    // Amadeus Client Credientals
                    const clientid = "gOIqKp00iu9rGRDRb893Mwa6YgagLRLz"; //Your App Api Key
                    const clientSecret = "86nyQTrv9AF5SkU4"; // Api Secret Key
                    //Creating A Data Variable to send these credentials
                    const requestData = {
                        grant_type: 'client_credentials',
                        client_id: clientid,
                        client_secret: clientSecret
                    };
                    //Converting Data Request to A url-encode String
                    const requestDataString = Object.keys(requestData).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(requestData[key])).join('&');
                    // Now Sending Ajax Request
                    $.ajax({
                        url: tokenUrl,
                        method: 'Post',
                        data: requestDataString,
                        success: function (clientData) {
                            const apiUrl = 'https://test.api.amadeus.com/v2/shopping/flight-offers'; // The Api Service You Want Use
                            const accessToken = clientData.access_token; // This Is mendatory to authorize user to use the api service
                            const SearchParams = {
                                originLocationCode: FromCity,
                                destinationLocationCode: toCountry,
                                departureDate: formattedDeparture,// Date Format YYYY-MM-DD
                                adults: numberofAdults.val()
                            }; // these are the params that we will send to our flight search api
                            const requestDataString = Object.keys(SearchParams).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(SearchParams[key])).join('&');
                            // Now Our Date And Uri is ready we will create a ajax Get request to send data and get data from response
                            $.ajax({
                                url: apiUrl + '?' + requestDataString,
                                method: "Get",
                                headers: {
                                    'Authorization': 'Bearer ' + accessToken,
                                },
                                success: function (response) {
                                    if (response.data == "" || response.data == null) {
                                        alert("no flights found")
                                    }
                                    else {
                                        let uniqueAirlinesIata = [];
                                        let segementArray =  [] ;
                                        // Assuming 'response' is the JSON response object
                                          let cardContainer = "" ;
                                          var airlineData = [];
                                        $(response.data).each(function (index, value) {
                                            $(value.itineraries).each(function (i, itinerary) {
                                                $(value.validatingAirlineCodes).each(function(a   , airlineCode){
                                                
                                             

                                             
                                                var durationValue = itinerary.duration;
                                                var hour           = durationValue.match(/(\d+)H/);
                                                var minutes        = durationValue.match(/(\d+)M/);
                                                if(hour && minutes){
                                                    var FormattedDuration  = hour[1] + "hours" +"-"+ minutes[1] +"minutes";
                                                }
                                              $(itinerary.segments).each(function( s , segment){
                                                 
                                                
                                                  // Changing Arrival And Departure Format from yyyy-mm-dd 00-00 to mm-dd-yyyy
                                                    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                                                   //Departure Format
                                                   var DepartureDate = segment.departure.at ;
                                                   var dept_date     = new Date(DepartureDate);
                                                   var d_year        = dept_date.getFullYear();
                                                   var d_month       = dept_date.getMonth();
                                                   var d_date        = dept_date.getDate();
                                                   var d_hour        = dept_date.getHours();
                                                   var d_minutes     = dept_date.getMinutes();
                                                   var FormattedDepatures = months[d_month] + ", " + d_date + "-" + d_year + "(" + d_hour + ":" + (d_minutes < 10 ? '0' : '') + d_minutes + (d_hour < 12 ? "AM" : "PM") + ")"; //  // for example Aug, 02-2017 (8:40 PM)
                                                  // Arrival Format
                                                   var ArrivalDate = segment.arrival.at ;
                                                   var arr_date    = new Date(ArrivalDate);
                                                   var a_year            = arr_date.getFullYear();
                                                   var a_month           = arr_date.getMonth();
                                                   var a_date            = arr_date.getDate();
                                                   var a_hour            = arr_date.getHours();
                                                   var a_minutes         = arr_date.getMinutes();
                                                   var FormattedArrival  = months[a_month] + ", " + a_date + "-" + a_year + "(" + a_hour  + ":" + (a_minutes < 10 ? '0' : '') + a_minutes + (a_hour >= 12 ? "AM" : "PM") + ")"; // for example Aug, 02-2017 (8:40 PM)
                                                    var wayType  = "" ;
                                                    var stayType = "" ;
                                                    if(value.oneWay == false){
                                                        wayType  = "Two Way" ;
                                                        stayType = "2 Stay";
                                                    }
                                                    else
                                                    {
                                                        wayType  = "One Way";
                                                        stayType = "1 Stay";
                                                    }

                                              $(value.travelerPricings).each(function(j , pricing){
                                                        
                                                            $.ajax({
                                                                url: getAirlines,
                                                                method: "Post",
                                                                data: { AirlinesIata: airlineCode },
                                                                success: function (airlines) {
                                                                    var airlineInfo = {};
                                                                    $(airlines).each(function (ar, airline) {
                                      
                                                                           var departure_date= months[d_month] + ", " + d_date + "-" + d_year;
                                                                           var departure_time= d_hour + ":" + (d_minutes < 10 ? '0' : '') + d_minutes + (d_hour >= 12 ? "AM" : "PM");
                                                                           var arrival_date= months[a_month] + ", " + a_date + "-" + a_year;
                                                                           var arrival_time= a_hour + ":" + (a_minutes < 10 ? '0' : '') + a_minutes + (a_hour >= 12 ? "AM" : "PM");
                                                            
                                                    cardContainer += `
                                                                 <div class="list-block main-block f-list-block">
                                                            <div class="list-content">
                                                                <div class="main-img list-img f-list-img">
                                                                    <a href="flight-detail-left-sidebar.html">
                                                                        <div class="f-img">
                                                                                    <img src="${airline.airlineImage}" class="img-fluid" alt="flight-img" />
                                                                        </div><!-- end f-list-img -->
                                                                    </a>
                                                                    <ul class="list-unstyled list-inline offer-price-1">
                                                                        <li class="list-inline-item duration"><i class="fa fa-clock-o"></i><span>${FormattedDuration}</span></li>
                                                                            <li class="list-inline-item price">${pricing.price.currency}${pricing.price.total}<span class="divider">|</span><span class="pkg">${stayType}</span></li>
                                                                    </ul>
                                                                    <ul class="list-unstyled flight-timing">
                                                                            <li><span><i class="fa fa-plane"></i></span><span class="date">${FormattedDepatures}</li>
                                                                                <li><span><i class="fa fa-plane"></i></span><span class="date">${FormattedArrival}</li>
                                                                    </ul>
                                                                </div><!-- end f-list-img -->

                                                                <div class="list-info f-list-info">
                                                                        <h3 class="block-title"><a href="flight-detail-left-sidebar.html">${from_city.val()} to ${toCountry}</a></h3>
                                                                    <p class="block-minor"><span>Fr 5379,</span> ${wayType} Flight</p>
                                                                    <p>Lorem ipsum dolor sit amet, ad duo fugit aeque fabulas, in lucilius prodesset pri. Veniam delectus ei vis. Est atqui timeam mnesarchum at, pro an eros perpetua ullamcorper.</p>

                                                                        <form id="bookingFormDetails" action="${FlightBookingUrl}" method="Post">
                                                                                <input type ="hidden" name="airline_name" value="${airline.airlinename}" />
                                                                                <input type ="hidden" name="airline_image" value="${airline.airlineImage}" />
                                                                                <input type ="hidden" name="aircraft_code" value="${segment.aircraft.code}" />
                                                                                <input type ="hidden" name="duration" value="${FormattedDuration}" />
                                                                                <input type ="hidden" name="journey_type" value="${wayType}" />
                                                                                <input type ="hidden" name="departure_date" value="${departure_date}" />
                                                                                <input type ="hidden" name="departure_time" value="${departure_time}" /> 
                                                                                <input type ="hidden" name="arrival_date" value="${arrival_date}" />
                                                                                <input type ="hidden" name="arrival_time" value="${arrival_time}" />
                                                                                <input type ="hidden" name="from" value="${from_city.val()}" />
                                                                                <input type ="hidden" name="to" value="${toCountry}" />
                                                                                <input type="hidden"  name="adults" value="${numberofAdults.val()}" />
                                                                                <input type="hidden"  name="currency" value="${pricing.price.currency}" />
                                                                                <input type="hidden"  name="price" value="${pricing.price.total}" />
                                                                                <input type="hidden"  name="class_type" value="${pricing.fareDetailsBySegment[0].class}" />
                                                                                <input type="hidden"  name="class_name" value="${pricing.fareDetailsBySegment[0].cabin}" />
                                                                        
                                                                        <button type="submit" class="btn btn-orange ticket_booking">Book Ticket</button>
                                                                            </form>
                                                                </div><!-- end f-list-info -->
                                                            </div><!-- end list-content -->
                                                        </div><!-- end f-list-block -->`;
                                                                    });
                                            $(serviceContainer).html(cardContainer);
                                                                }
                                                            });
                                                                }); //  Pricing Ends Here    
                                                });  // Segments Foreach Ends here
                                            }); // Foreach Ends here
                                              
                                            }); // Foreach For Carrier Codes
                                          
                                        });// duration each function
                                   
                                   

                                    }

                                }


                            });
                        }

                    });
                   
                }
                else
                {
                    let FromCity = response.fromCityDetails.iataCode;
                    let toCity = response.toCityDetails.iataCode;
                    var tokenUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";
                    // Amadeus Client Credientals
                    const clientid = "gOIqKp00iu9rGRDRb893Mwa6YgagLRLz"; //Your App Api Key
                    const clientSecret = "86nyQTrv9AF5SkU4"; // Api Secret Key
                    //Creating A Data Variable to send these credentials
                    const requestData = {
                        grant_type: 'client_credentials',
                        client_id: clientid,
                        client_secret: clientSecret
                    };
                    //Converting Data Request to A url-encode String
                    const requestDataString = Object.keys(requestData).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(requestData[key])).join('&');
                    // Now Sending Ajax Request
                    $.ajax({
                        url: tokenUrl,
                        method: 'Post',
                        data: requestDataString,
                        success: function (clientData) {
                            const apiUrl = 'https://test.api.amadeus.com/v2/shopping/flight-offers'; // The Api Service You Want Use
                            const accessToken = clientData.access_token; // This Is mendatory to authorize user to use the api service
                            const SearchParams = {
                                originLocationCode: FromCity,
                                destinationLocationCode: toCity,
                                departureDate: formattedDeparture,// Date Format YYYY-MM-DD
                                adults: 4
                            }; // these are the params that we will send to our flight search api
                            const requestDataString = Object.keys(SearchParams).map(key => encodeURIComponent(key) + "=" + encodeURIComponent(SearchParams[key])).join('&');
                            // Now Our Date And Uri is ready we will create a ajax Get request to send data and get data from response
                            $.ajax({
                                url: apiUrl + '?' + requestDataString,
                                method: "Get",
                                headers: {
                                    'Authorization': 'Bearer ' + accessToken,
                                },
                                success: function (response) {
                                    if (response.data == "" || response.data == null) {
                                        alert("no flights found")
                                    }
                                    else {
                                        let uniqueAirlinesIata = [];
                                        // Assuming 'response' is the JSON response object
                                        $(response.data).each(function (index, value) {
                                            $(value.itineraries).each(function (i, itinerary) {
                                                $(itinerary.segments).each(function (j, segment) {
                                                    uniqueAirlinesIata.push(segment.carrierCode);
                                                });
                                            });
                                        });

                                        // To remove duplicate carrier codes, you can use a Set
                                        let allAirlinesIata = [...new Set(uniqueAirlinesIata)];

                                        $.ajax({
                                            url: getAirlines,
                                            type: "Post",
                                            data: { AirlinesIata: allAirlinesIata },
                                            success: function (response) {
                                                let cardContainer = "";
                                                $(response.data).each(function (index, value) {
                                                    cardContainer += `
                                                             <div class="list-block main-block f-list-block">
                                                        <div class="list-content">
                                                            <div class="main-img list-img f-list-img">
                                                                <a href="flight-detail-left-sidebar.html">
                                                                    <div class="f-img">
                                                                            <img src="${value.airlineImage}" class="img-fluid" alt="flight-img" />
                                                                    </div><!-- end f-list-img -->
                                                                </a>
                                                                <ul class="list-unstyled list-inline offer-price-1">
                                                                    <li class="list-inline-item duration"><i class="fa fa-clock-o"></i><span>6 hours - 30 minutes</span></li>
                                                                    <li class="list-inline-item price">$568.00<span class="divider">|</span><span class="pkg">2 Stay</span></li>
                                                                </ul>
                                                                <ul class="list-unstyled flight-timing">
                                                                    <li><span><i class="fa fa-plane"></i></span><span class="date">Aug, 02-2017 </span>(8:40 PM)</li>
                                                                    <li><span><i class="fa fa-plane"></i></span><span class="date">Aug, 03-2017 </span>(8:40 PM)</li>
                                                                </ul>
                                                            </div><!-- end f-list-img -->

                                                            <div class="list-info f-list-info">
                                                                    <h3 class="block-title"><a href="flight-detail-left-sidebar.html">${from_city.val()} to ${toCountry}</a></h3>
                                                                <p class="block-minor"><span>Fr 5379,</span> Oneway Flight</p>
                                                                <p>Lorem ipsum dolor sit amet, ad duo fugit aeque fabulas, in lucilius prodesset pri. Veniam delectus ei vis. Est atqui timeam mnesarchum at, pro an eros perpetua ullamcorper.</p>
                                                                <a href="flight-detail-left-sidebar.html" class="btn btn-orange">View More</a>
                                                            </div><!-- end f-list-info -->
                                                        </div><!-- end list-content -->
                                                    </div><!-- end f-list-block -->`;
                                                }); // Foreach Ends here
                                                $(serviceContainer).html(cardContainer);
                                            }
                                        }); // Ajax Ends here for Services
                                    
                                    }

                                }


                            });
                        }

                    })
                }
                
                // Now We Will Get Details From Flight Schedules Through Our Api Then Will Show the Airlines Results of avaialble Schedules
              
               
            }
        })



    });