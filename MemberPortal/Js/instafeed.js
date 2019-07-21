(function($) {
    'use strict';

    var userFeed = new Instafeed({
        get: 'user',
        userId: '4713066548',
        accessToken: '4713066548.8d3ef49.6942ccf6893b404185d88e24f7d56229',
        resolution: 'standard_resolution',
        template: '<a href="{{link}}" target="_blank" id="{{id}}"><img src="{{image}}" /></a>',
        sortBy: 'most-recent',
        limit: 3,
        links: false
    });
    userFeed.run();

})(jQuery);
