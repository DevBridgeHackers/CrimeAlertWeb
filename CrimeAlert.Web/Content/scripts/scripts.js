(function($)
{
    $(function()
    {
        $("#contentControls a:has(#controlPhotos)").click(function ()
        {
            $("#contentControls .contentControlActive").removeClass('contentControlActive');
            $(".listAlerts .alertItemSmall .alertItemSmallMap").hide();
            
            $("#contentControls #controlPhotos").addClass('contentControlActive');
            $(".listAlerts .alertItemSmall .alertItemSmallPhoto").show();
        });
        $("#contentControls a:has(#controlMapView)").click(function ()
        {
            $("#contentControls .contentControlActive").removeClass('contentControlActive');
            $(".listAlerts .alertItemSmall .alertItemSmallPhoto").hide();
            
            $("#contentControls #controlMapView").addClass('contentControlActive');
            $(".listAlerts .alertItemSmall .alertItemSmallMap").show();
            
            $(".listAlerts .alertItemSmall .alertItemSmallMap").each(function ()
            {
                var alertItemSmallMap = $(this);
                
                if (alertItemSmallMap.data('initialized') !== true)
                {
                    var div = $('div', alertItemSmallMap);
                    var latitude = (Number)(alertItemSmallMap[0].dataset.latitude);
                    var longitude = (Number)(alertItemSmallMap[0].dataset.longitude);
                    var title = alertItemSmallMap[0].dataset.title;
                    var myLatlng = new google.maps.LatLng(latitude, longitude);
                    var mapOptions = {
                        center: myLatlng,
                        zoom: 8,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };
                    var map = new google.maps.Map(div[0], mapOptions);
                    var marker = new google.maps.Marker({
                        position: myLatlng,
                        map: map,
                        title:title
                    });
                    
                    alertItemSmallMap.data('initialized', true);
                }
            });
        });
    });
})(jQuery);